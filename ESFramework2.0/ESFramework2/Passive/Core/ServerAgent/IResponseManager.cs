using System;
using System.Threading ;
using System.Collections.Generic ;
using ESBasic.Helpers;
using ESFramework.Core;

namespace ESFramework.Passive
{
	/// <summary>
	/// IResponseManager ���ڹ���ӷ��������յ����лظ���Ϣ��
	/// </summary>
	public interface IResponseManager : IDisposable
	{
		void Initialize() ;

		void PushResponse(NetMessage response) ;
		NetMessage PopRespose(int correlationID ,int serviceKey) ;  //��������
		NetMessage PickupResponse(int serviceKey ,int corelationID) ;//��TimeoutSecʱ���ڲ��ϵ�PopRespose

		/// <summary>
		/// ResponseTTL ���һ���ظ��ڹ������д��ڵ�ʱ�䳬��ResponseTTL����ᱻɾ�������ResponseTTLΪ0�����ʾ�����������ڹ���
		/// </summary>		
		int ResponseTTL{get ;set ;} //s

		/// <summary>
		/// �����TimeoutSec�ڣ���Ȼ���ղ��������Ļظ������׳��쳣��ȡ0ʱ����ʾ�����ó�ʱ
		/// </summary>
		int TimeoutSec{get ;set ; } 	
	}

	/// <summary>
	/// IResponseManager �Ĳο�ʵ��
	/// </summary>
	public class ResponseManager : IResponseManager 
	{
        private IList<ResponseWrap> responseList = new List<ResponseWrap>();
		private int responseTTL		   = 0 ;
		private Timer timerForTTL ;

		public ResponseManager()
		{			
		}	
	
		~ResponseManager()
		{
			if(this.timerForTTL != null)
			{
				this.timerForTTL.Dispose() ;
			}
		}		

		#region IResponseManager ��Ա

		#region Initialize
		public void Initialize()
		{
			if(this.responseTTL <= 0)
			{
				return ;
			}

			this.timerForTTL = new Timer(new TimerCallback(this.CheckTTL) ,null ,this.responseTTL * 500 ,this.responseTTL *500) ;
		}

		#region CheckTTL
		private void CheckTTL(object state)
		{
			lock(this.responseList)
			{
                IList<ResponseWrap> tempList = new List<ResponseWrap>();
				DateTime now	   = DateTime.Now ;
				foreach(ResponseWrap resWrap in this.responseList)
				{
					TimeSpan span = now - resWrap.inTime ;
					if(span.TotalMilliseconds > (this.responseTTL*1000))
					{
						tempList.Add(resWrap) ;
					}					
				}

				foreach(ResponseWrap wrap in tempList)
				{
					this.responseList.Remove(wrap) ;
				}
			}
		}
		#endregion
		#endregion

		#region PushResponse, PopRespose
		public void PushResponse(NetMessage response)
		{
			lock(this.responseList)
			{
				this.responseList.Add(new ResponseWrap(response ,DateTime.Now)) ;
			}
		}

		public NetMessage PopRespose(int correlationID, int serviceKey)
		{				
			lock(this.responseList)
			{
				ResponseWrap destWrap = null ;
				foreach(ResponseWrap resWrap in this.responseList)
				{
					if((resWrap.response.Header.MessageID == correlationID) &&(resWrap.response.Header.ServiceKey == serviceKey))
					{
						destWrap = resWrap ;
						break ;
					}
				}

				if(destWrap != null)
				{
					this.responseList.Remove(destWrap) ;
					return destWrap.response ;
				}

				return null ;				
			}
		}
		#endregion

		#region PickupResponse
		public NetMessage PickupResponse(int serviceKey ,int corelationID)
		{
			bool isUIThraed = ! Thread.CurrentThread.IsBackground ;

			int count = (this.timeoutSec*1000 / 50) ;
			if(this.timeoutSec <= 0)
			{
				count = int.MaxValue ;
			}

			while((count --) > 0)
			{
				NetMessage response = this.PopRespose(corelationID ,serviceKey) ;			

				if(response == null)
				{
					if(isUIThraed)
					{
                        WindowsHelper.DoWindowsEvents();
					}
					System.Threading.Thread.Sleep(50) ;
				}
				else
				{
					return response ;
				}
			}

			throw new Exception("It's timeout for wait response !") ;
		}
		#endregion

		#region ResponseTTL
		public int ResponseTTL
		{
			set
			{
				this.responseTTL = value ;
			}
			get
			{
				return this.responseTTL ;
			}


		}
		#endregion

		#region TimeoutSec
		private int timeoutSec = 3 ; 
		public  int TimeoutSec
		{			
			set
			{
				this.timeoutSec = value ;
			}
			get
			{
				return this.timeoutSec ;
			}
		}
		#endregion

		#endregion		

		#region IDisposable ��Ա

		public void Dispose()
		{
			if(this.timerForTTL != null)
			{
				this.timerForTTL.Dispose() ;
			}

			GC.SuppressFinalize(this) ;
		}

		#endregion
	}

	internal class ResponseWrap
	{
		public NetMessage response ;
		public DateTime   inTime ;

		public ResponseWrap(NetMessage res ,DateTime time)
		{
			this.response = res ;
			this.inTime   = time ;
		}
	}

}
