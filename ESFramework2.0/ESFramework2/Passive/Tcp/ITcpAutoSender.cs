using System;
using System.Threading ;
using System.Collections.Generic ;
using System.Net.Sockets ;
using ESBasic;
using ESBasic.DataStructure;

namespace ESFramework.Passive.Tcp
{
	/// <summary>
	/// ITcpAutoSender Tcp数据自动发送器,
	/// (1)将数据分为"高优先级"、"普通"和"可丢弃的"两种。
	/// (2)自动丢弃过期的"可丢弃的"数据，发送最新数据
	/// (3)当丢弃数据时，触发事件；当发送队列都为空时，触发“缺乏数据”事件
	/// </summary>
	public interface ITcpAutoSender :IDisposable
	{
		void Initialize() ;
		void SendData(byte[] data, DataPriority dataPriority);      
		void ClearQueue(DataPriority queueType) ;
        event CbStream DataDiscarded;
        event CbSimple DataLacked;
		event CbSimple ConnectionInterrupted ;

		int QueueSizeOfDiscarded { get; set;}
		int QueueSizeOfNonDiscarded { get; set;}
		NetworkStream NetworkStream { set;}
	}		
	
	#region TcpAutoSender
	public class TcpAutoSender :ITcpAutoSender
	{
		private NetworkStream netStream;
        private Queue<byte[]> queueOfHigh = new Queue<byte[]>();
        private Queue<byte[]> queueOfCommon = new Queue<byte[]>();
        private Queue<byte[]> queueOfLow = new Queue<byte[]>();
        private FixQueue<byte[]> queueOfCanDiscarded;
		private int queueSizeOfDiscarded = 10;
		private int queueSizeOfNonDiscarded = 3;
		private volatile bool gotoDispose = false ;
		private ManualResetEvent disposeDoneEvent = new ManualResetEvent(false);

		public TcpAutoSender()
		{
            this.DataDiscarded += delegate { };
            this.DataLacked += delegate { };
            this.ConnectionInterrupted += delegate { };
		}	

		#region ITcpAutoSender Members
        #region event
        public event CbStream DataDiscarded;
        public event CbSimple DataLacked;
        public event CbSimple ConnectionInterrupted;        
        #endregion

        #region NetworkStream
        public NetworkStream NetworkStream
        {
            set
            {
                this.netStream = value;
            }
        } 
        #endregion

        #region Initialize
        public void Initialize()
        {
            this.gotoDispose = false;
            this.disposeDoneEvent.Reset();
            this.queueOfCanDiscarded = new FixQueue<byte[]>(this.queueSizeOfDiscarded);
            this.queueOfCanDiscarded.ObjectDiscarded += new CbGeneric<byte[]>(queueOfCanDiscarded_ObjectDiscarded);

            CbSimple cb = new CbSimple(this.WorkThread);
            cb.BeginInvoke(null, null);
        }

        private void queueOfCanDiscarded_ObjectDiscarded(byte[] obj)
        {
            this.DataDiscarded(obj);
        }
        #endregion

		#region SendData
		public void SendData(byte[] data, DataPriority dataPriority)
		{			
			if (dataPriority == DataPriority.High)
			{
				lock(this.queueOfHigh)
				{
					while(this.queueOfHigh.Count >= this.queueSizeOfNonDiscarded)
					{
						Thread.Sleep(20) ;
					}

					this.queueOfHigh.Enqueue(data);
				}
			}
			else if (dataPriority == DataPriority.Common)
			{
				lock(this.queueOfCommon)
				{
					while(this.queueOfCommon.Count >= this.queueSizeOfNonDiscarded)
					{
						Thread.Sleep(20) ;
					}

					this.queueOfCommon.Enqueue(data);
				}
			}
			else if (dataPriority == DataPriority.Low)
			{
				lock(this.queueOfLow)
				{
					while(this.queueOfLow.Count >= this.queueSizeOfNonDiscarded)
					{
						Thread.Sleep(20) ;
					}

					this.queueOfLow.Enqueue(data);
				}
			}
			else
			{
                this.queueOfCanDiscarded.Enqueue(data);
			}			
		} 
		#endregion

        #region ClearQueue
        public void ClearQueue(DataPriority queueType)
        {
            if (queueType == DataPriority.High)
            {
                lock (this.queueOfHigh)
                {
                    this.queueOfHigh.Clear();
                }
            }
            else if (queueType == DataPriority.Common)
            {
                lock (this.queueOfCommon)
                {
                    this.queueOfCommon.Clear();
                }
            }
            else if (queueType == DataPriority.Low)
            {
                lock (this.queueOfLow)
                {
                    this.queueOfLow.Clear();
                }
            }
            else
            {
                this.queueOfCanDiscarded.Clear();
            }

        } 
        #endregion		

		#region QueueSizeOfDiscarded
		public int QueueSizeOfDiscarded
		{
			get
			{
				return this.queueSizeOfDiscarded;
			}
			set
			{
				this.queueSizeOfDiscarded = value;
			}
		}       
 
		public int QueueSizeOfNonDiscarded
		{
			get
			{
				return this.queueSizeOfNonDiscarded;
			}
			set
			{
				this.queueSizeOfNonDiscarded = value;
			}
		}       
		#endregion

		#region Dispose
		public void Dispose()
		{           
			this.gotoDispose = true;
			this.disposeDoneEvent.WaitOne(1000, true);	
		} 
		#endregion

		#region WorkThread
		private void WorkThread()
		{
			bool empty = true;

			while (true)
			{
				try
				{
					if (this.gotoDispose)
					{
						break;
					}
					empty = true;

					#region High
					if (this.queueOfHigh.Count > 0)
					{
                        byte[] data = null;
                        lock (this.queueOfHigh)
                        {
                            data = this.queueOfHigh.Dequeue();
                        }
						this.netStream.Write(data, 0, data.Length);
						empty = false;
						continue;
					}

					if (this.gotoDispose)
					{
						break;
					} 
					#endregion

					#region Common
					if (this.queueOfCommon.Count > 0)
					{						
                        byte[] data = null;
                        lock (this.queueOfHigh)
                        {
                            data = this.queueOfCommon.Dequeue();
                        }
						this.netStream.Write(data, 0, data.Length);
						empty = false;
						continue;
					}

					if (this.gotoDispose)
					{
						break;
					} 
					#endregion

					#region Low
					if (this.queueOfLow.Count > 0)
					{
                        byte[] data = null;
                        lock (this.queueOfHigh)
                        {
                            data = this.queueOfLow.Dequeue();
                        }									
						this.netStream.Write(data, 0, data.Length);
						empty = false;
						continue;
					}

					if (this.gotoDispose)
					{
						break;
					} 
					#endregion

					#region CanDiscarded
					if (this.queueOfCanDiscarded.Count > 0)
					{
                        byte[] data = this.queueOfCanDiscarded.Dequeue();	
						this.netStream.Write(data, 0, data.Length);
						empty = false;
						continue;
					}

					if (this.gotoDispose)
					{
						break;
					} 
					#endregion
				
					if (empty)
					{
						if (this.DataLacked != null)
						{
							this.DataLacked();
						}

						Thread.Sleep(20);
					}
				}
				catch(Exception ee)
				{
					#region ConnectionInterrupted
					if(ee is System.IO.IOException) //正在读写流的时候，连接断开
					{
						this.disposeDoneEvent.Set(); //必须设置在ConnectionInterrupted之前
						if(this.ConnectionInterrupted != null)
						{							
							this.ConnectionInterrupted() ;							
						}
						break ;
					}
					#endregion
				}
			}

			this.disposeDoneEvent.Set();
		} 
		#endregion

		#endregion
	}
	#endregion
}
