using System;
using System.Threading;
using System.Collections;
using System.Collections.Generic ;
using ESBasic;

namespace ESFramework.Server.Tcp.UserManagment
{
	/// <summary>
	/// TcpUserManager ITcpUserManager的默认实现。
	/// </summary>
	public class TcpUserManager :ITcpUserManager
	{
		private Hashtable htableClient    = new Hashtable() ; //userID -- Work	
		private Hashtable htableConnect   = new Hashtable() ; //userID -- connectID	
		private ReaderWriterLock rwLocker = new ReaderWriterLock() ;
		private const int WaitLockerSpan  = 2000 ; //ms	

		private IUserOnLineChecker tcpUserOnLineChecker = new UserOnLineChecker() ;
		private IUserTaskReporter  taskReporter  = new EmptyUserTaskReporter() ;
		private ITcpUserDisplayer  tcpUserDisplayer = new EmptyTcpDisplayer() ;	

		public event CbForUserDisconn    SomeOneDisconnected;	
		public event CbSimpleStr         SomeOneConnected ;
		public event CbSimple            Restarted ;
		
		public TcpUserManager()
		{				
			this.tcpUserOnLineChecker.SomeConnectionTimeOuted += new CbSimpleStr(tcpUserOnLineChecker_SomeConnectionTimeOuted);			
		}

		#region ITcpUserManager 成员		

		#region Start ,Stop
		public void Start()
		{			
			this.tcpUserOnLineChecker.Start() ;
			if(this.Restarted != null)
			{
				this.Restarted() ;
			}
		}

		public void Stop()
		{
			this.tcpUserOnLineChecker.Stop() ;
			this.htableClient.Clear() ; //此处可以执行更复杂的操作，比如将所有用户的任务上报
			this.htableConnect.Clear() ;
			this.tcpUserDisplayer.ClearAll() ;
		}
		#endregion

		#region DisposeOneUser
		public void DisposeOneUser(string userID, DisconnectedCause cause)
		{
			this.tcpUserOnLineChecker.UnregisterUser(userID) ;	

			this.UngisterUser(userID ,cause) ;
		}

		public void DisposeOneConnection(int connectID ,DisconnectedCause cause )
		{
			this.rwLocker.AcquireReaderLock(TcpUserManager.WaitLockerSpan) ;
			string dest = null ;
			foreach(string userID in this.htableConnect.Keys)
			{
				if(((int)this.htableConnect[userID]) == connectID)
				{
					dest = userID ;
					break ;
				}
			}
			this.rwLocker.ReleaseReaderLock() ;

			if(dest != null)
			{
				this.DisposeOneUser(dest ,cause) ;
			}		
		}
		#endregion

		#region ServiceCommited
		public void ServiceCommited(int connectID ,string userID ,int serviceKey ,int dataCount)
		{
			if(userID == NetHelper.SystemUserID)
			{
				return ;
			}

			//激活定时器中的用户
			this.tcpUserOnLineChecker.RegisterOrActivateUser(userID) ;

			//修改任务列表
			ITaskMainRecord mainRecord = this.CreateOrGetMainRecord(connectID ,userID ) ;
			if(((mainRecord.UserID == null) || (mainRecord.UserID.Trim() == "" )) && (userID != null) && (userID != ""))
			{
				mainRecord.UserID	   = userID ;
			}

			ITaskDetailRecord detail   = this.taskReporter.CreateRudeTaskDetailRecord() ;
			detail.DataCount           = dataCount ;
			detail.ServiceKey          = serviceKey ;
			detail.RequestTime         = DateTime.Now ;

			mainRecord.Details.Add(detail) ;

			//更新界面
			if(mainRecord.UserID != null)
			{
				int totalLen = this.GetTotalDataLen(mainRecord) ;
				this.tcpUserDisplayer.SetOrUpdateUserItem(mainRecord.UserID ,serviceKey ,totalLen) ;
			}
		} 
		#endregion

		#region ActivateUser
		public void ActivateUser(string userID)
		{			
			this.tcpUserOnLineChecker.RegisterOrActivateUser(userID) ;
		}
		#endregion	

		#region IsUserOnLine ,GetUserConnectID ,GetOnlineUserList
		public bool IsUserOnLine(string userID)
		{
			return (this.htableClient[userID] != null) ;
		}

		public int GetUserConnectID(string userID)
		{
			if(this.htableConnect[userID] == null)
			{
				return -1 ;
			}

			return (int)this.htableConnect[userID] ;
		}

		public ICollection GetOnlineUserList()
		{				
			return ((Hashtable)(this.htableClient.Clone())).Keys;	
		}
		#endregion

		#endregion

		#region CreateOrGetMainRecord ,UngisterUser
		private ITaskMainRecord CreateOrGetMainRecord(int connectID ,string userID)
		{
			if(this.htableClient[userID] == null)
			{
				ITaskMainRecord mainRecord = this.taskReporter.CreateRudeTaskMainRecord() ;			
				mainRecord.TimeLogon	  = DateTime.Now ;
				mainRecord.RequestCount   = 0 ;
				mainRecord.TotalDataCount = 0 ;			
				mainRecord.UserID		  = userID ;
				mainRecord.Details		  = new List<ITaskDetailRecord>() ;			
			
				this.rwLocker.AcquireWriterLock(TcpUserManager.WaitLockerSpan) ;
				this.htableClient.Add(userID ,mainRecord) ;
				this.htableConnect.Add(userID ,connectID) ;
				this.rwLocker.ReleaseWriterLock() ;			

				this.ActivateConnectedEvent(userID) ;				
			}

			return (ITaskMainRecord)this.htableClient[userID] ;
		}		


		//将指定连接从列表中移除，并将用户任务上报
		private void UngisterUser(string userID ,DisconnectedCause cause)
		{
			if(this.htableClient[userID] == null)
			{
				return ;
			}

			//上报任务
			ITaskMainRecord mainRecord = (ITaskMainRecord)this.htableClient[userID] ;
			mainRecord.TimeLogout = DateTime.Now ;
			mainRecord.RequestCount = mainRecord.Details.Count ;				
			mainRecord.TotalDataCount = this.GetTotalDataLen(mainRecord) ;
			this.taskReporter.RecordTaskList(mainRecord) ;

			this.rwLocker.AcquireWriterLock(TcpUserManager.WaitLockerSpan) ;
			this.htableClient.Remove(userID) ;
			this.htableConnect.Remove(userID) ;
			this.rwLocker.ReleaseWriterLock() ;			

			//更新显示
			this.tcpUserDisplayer.RemoveUser(userID ,cause) ;

			//触发事件
			this.ActivateDisConnectedEvent(userID ,cause) ;			
		}
		
		private int GetTotalDataLen(ITaskMainRecord mainRecord)
		{
			int totalCount = 0 ;
			foreach(ITaskDetailRecord detail in mainRecord.Details)
			{
				totalCount += detail.DataCount ;
			}

			return totalCount ;
		}			

		private void ActivateDisConnectedEvent(string userID ,DisconnectedCause cause)
		{
			if(this.SomeOneDisconnected != null)
			{
				this.SomeOneDisconnected(userID ,cause) ;
			}
		}

		private void ActivateConnectedEvent(string userID)
		{
			if(this.SomeOneConnected != null)
			{
				this.SomeOneConnected(userID) ;
			}
		}

		private void tcpUserOnLineChecker_SomeConnectionTimeOuted(string userID)
		{
			this.UngisterUser(userID ,DisconnectedCause.CheckerTimeOut) ;			
		}
		#endregion

		#region property

		public ITcpUserDisplayer TcpUserDisplayer
		{
			set
			{
				if(value != null)
				{
					this.tcpUserDisplayer = value ;
				}
			}
		}

		public IUserTaskReporter UserTaskReporter
		{
			set
			{
				if(value != null)
				{
					this.taskReporter = value ;
				}
			}
		}

		public int OnLineCheckSpan
		{
			get
			{
				return this.tcpUserOnLineChecker.CheckSpan ;
			}
			set
			{
				this.tcpUserOnLineChecker.CheckSpan = value ;
			}
		}

		#endregion

	
	}
}
