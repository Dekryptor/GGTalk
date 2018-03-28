using System;
using System.Threading ;
using System.Collections ;
using ESBasic;

namespace ESFramework.Server.Tcp
{
	/// <summary>
	/// IContextKeyManager ���ڹ������е�ContextKey ,IContextKeyManager�е����г�Ա���������̰߳�ȫ�ġ�
	/// ���ߣ���ΰ sky.zhuwei@163.com 
	/// </summary>
	public interface IContextKeyManager
	{
		void InsertContextKey(ContextKey context_key) ;
		void DisposeAllContextKey() ;		
		void RemoveContextKey(int streamHashCode) ;
		ISafeNetworkStream GetNetStream(int streamHashCode) ;

		int			ConnectionCount {get ;}
		ICollection ContextKeyList{get ;}

		event CbSimpleInt StreamCountChanged ;		
	}	

	/// <summary>
	/// ContextKeyManager IContextKeyManager��Ĭ��ʵ�֡�
	/// </summary>
	public class ContextKeyManager :IContextKeyManager
	{
		private Hashtable contextKeyTable = new Hashtable() ; //connectID -- ContextKey		
		private ReaderWriterLock rWlock	  = new ReaderWriterLock() ;
		private int timeOut = 2000 ;
		public event CbSimpleInt StreamCountChanged ; 

		public ContextKeyManager()
		{			
		}

		public ICollection ContextKeyList
		{
			get
			{				
				return ((Hashtable)(this.contextKeyTable.Clone())).Values;				
			}
		}

		#region ActiveCountChangeEvent //�������������仯
		private void ActiveCountChangeEvent()
		{			
			if(this.StreamCountChanged != null)
			{
				this.StreamCountChanged(this.ConnectionCount) ;
			}
		}
		#endregion

		#region InsertContextKey
		public void InsertContextKey(ContextKey context_key)
		{
			this.rWlock.AcquireWriterLock(this.timeOut) ;
			this.contextKeyTable.Add(context_key.NetStream.GetHashCode() ,context_key) ;				
			this.rWlock.ReleaseWriterLock() ;

			this.ActiveCountChangeEvent() ;			
		}
		#endregion

		#region DisposeAllContextKey
		public void DisposeAllContextKey()
		{
			this.rWlock.AcquireWriterLock(this.timeOut) ;

			foreach(ContextKey key in this.contextKeyTable.Values)
			{
				key.NetStream.Close() ;
			}
			
			this.contextKeyTable.Clear() ;
					
			this.rWlock.ReleaseWriterLock() ;	
			
			this.ActiveCountChangeEvent() ;	
		}
		#endregion	

		#region RemoveContextKey
		//[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.Synchronized)]
		public void RemoveContextKey(int streamHashCode)
		{			
			if(this.contextKeyTable.Count == 0)
			{
				return ;
			}

			this.rWlock.AcquireWriterLock(this.timeOut) ;
			this.contextKeyTable.Remove(streamHashCode) ;				
			this.rWlock.ReleaseWriterLock() ;
			
			this.ActiveCountChangeEvent() ;			
		}
		#endregion

		#region ConnectionCount
		public int ConnectionCount
		{
			get
			{				                    
				return this.contextKeyTable.Count ;				
			}
		}
		#endregion

		#region GetNetStream
		public ISafeNetworkStream GetNetStream(int streamHashCode)
		{
			ContextKey key = (ContextKey)this.contextKeyTable[streamHashCode] ;
			if(key == null)
			{
				return null ;
			}

			return key.NetStream ;		
		}
		#endregion
	}

	#region ContextKeyManager_Old	
	public class ContextKeyManager2 :IContextKeyManager
	{
		private ArrayList contextKeyList = new ArrayList() ;
		private ArrayList contextKeyListClone = new ArrayList() ;
		private ReaderWriterLock rWlock = new ReaderWriterLock() ;
		private int timeOut = 2000 ;
		public event CbSimpleInt StreamCountChanged ; 

		public ContextKeyManager2()
		{			
		}

		public ICollection ContextKeyList
		{
			get
			{
				return this.contextKeyListClone ;
			}
		}

		#region ActiveCountChangeEvent //�������������仯
		private void ActiveCountChangeEvent()
		{
			this.contextKeyListClone = (ArrayList)this.contextKeyList.Clone() ;
			if(this.StreamCountChanged != null)
			{
				this.StreamCountChanged(this.ConnectionCount) ;
			}
		}
		#endregion

		#region InsertContextKey
		public void InsertContextKey(ContextKey context_key)
		{
			this.rWlock.AcquireWriterLock(this.timeOut) ;
			this.contextKeyList.Add(context_key) ;				
			this.rWlock.ReleaseWriterLock() ;

			this.ActiveCountChangeEvent() ;			
		}
		#endregion

		#region DisposeAllContextKey
		public void DisposeAllContextKey()
		{
			this.rWlock.AcquireWriterLock(this.timeOut) ;

			foreach(ContextKey key in this.contextKeyList)
			{
				key.NetStream.Close() ;
			}
			
			this.contextKeyList.Clear() ;
					
			this.rWlock.ReleaseWriterLock() ;	
			
			this.ActiveCountChangeEvent() ;	
		}
		#endregion	

		#region RemoveContextKey
		//[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.Synchronized)]
		public void RemoveContextKey(int streamHashCode)
		{
			
			if(this.contextKeyList.Count == 0)
			{
				return ;
			}

			this.rWlock.AcquireWriterLock(this.timeOut) ;

			foreach(ContextKey key in this.contextKeyList)
			{
				if(key.NetStream.GetHashCode() == streamHashCode)
				{
					key.NetStream.Close() ;
					this.contextKeyList.Remove(key) ;
					break ;
				}
			}
				
			this.rWlock.ReleaseWriterLock() ;
			
			this.ActiveCountChangeEvent() ;			
		}
		#endregion

		#region ConnectionCount
		public int ConnectionCount
		{
			get
			{				                    
				return this.contextKeyList.Count ;				
			}
		}
		#endregion

		#region GetNetStream
		public ISafeNetworkStream GetNetStream(int streamHashCode)
		{
			ISafeNetworkStream tar = null ;
			this.rWlock.AcquireReaderLock(this.timeOut) ;

			foreach(ContextKey key in this.contextKeyList)
			{
				if(key.NetStream.GetHashCode() == streamHashCode)
				{
					tar = key.NetStream ;
					break ;
				}
			}

			this.rWlock.ReleaseReaderLock() ;

			return tar ;
		}
		#endregion
	}
	#endregion
}
