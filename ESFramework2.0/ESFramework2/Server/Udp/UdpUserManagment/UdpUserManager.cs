using System;
using System.Net ;
using System.Collections ;
using ESBasic;
using ESFramework.Core;

namespace ESFramework.Server.Udp.UserManagment
{
	/// <summary>
	/// UdpUserManager IUdpUserManager�Ĳο�ʵ�֡�
	/// </summary>
	public class UdpUserManager :IUdpUserManager
	{
		private Hashtable htUser ;//userID -- ipe		
		private IUserOnLineChecker onlineChecker = new UserOnLineChecker();

		public UdpUserManager()
		{
			this.htUser = new Hashtable() ;
			this.onlineChecker.SomeConnectionTimeOuted += new CbSimpleStr(onlineChecker_SomeConnectionTimeOuted);
		}

		#region IUdpUserManager ��Ա
		public void Start() 
		{
			this.onlineChecker.Start() ;

			if(this.Restarted != null)
			{
				this.Restarted() ;
			}
		}

		public void Stop() 
		{
			this.htUser.Clear() ;
			this.onlineChecker.Stop() ;
		}

		public bool IsUserOnLine(string userID)
		{
			return (this.htUser[userID] != null) ;
		}

		public void RegisterUser(string userID, IPEndPoint ipe)
		{
			bool hasExist = (this.htUser[userID] != null) ;
			lock(this.htUser)
			{
				this.htUser.Remove(userID) ;
				this.htUser.Add(userID ,ipe) ;
			}

			this.onlineChecker.RegisterOrActivateUser(userID) ;			
			this.netDisplayer.RegisterUserEx(userID ,ipe.ToString()) ;

			if(! hasExist)
			{
				if(this.SomeOneConnected != null)
				{
					this.SomeOneConnected(userID) ;
				}
			}

			if(this.UserCountChanged != null)
			{
				this.UserCountChanged(this.htUser.Count) ;
			}
		}

		public ICollection GetOnlineUserList()
		{
			return ((Hashtable)(this.htUser.Clone())).Keys;	
		}

		public void ServiceCommitted(string userID ,NetMessage msg)
		{
			this.netDisplayer.RegisterUserServicedInfo(userID ,msg.Header.ServiceKey.ToString() ,msg.Length) ;
		}

		public void UnRegisterUser(string userID)
		{
			lock(this.htUser)
			{
				this.htUser.Remove(userID) ;				
			}

			this.onlineChecker.UnregisterUser(userID) ;	
			this.netDisplayer.UngisterUser(userID) ;

			if(this.SomeOneDisconnected != null)
			{
				this.SomeOneDisconnected(userID ,DisconnectedCause.Logoff) ;
			}

			if(this.UserCountChanged != null)
			{
				this.UserCountChanged(this.htUser.Count) ;
			}			
		}

		public void ActivateUser(string userID)
		{
			this.onlineChecker.RegisterOrActivateUser(userID) ;
		}

		public IPEndPoint GetUserIpe(string userID)
		{
			return (IPEndPoint)this.htUser[userID] ;
		}
	
		public event CbSimpleInt	  UserCountChanged ;
		public event CbForUserDisconn SomeOneDisconnected ;
		public event CbSimpleStr      SomeOneConnected ;//UserID
		public event CbSimple		  Restarted ;

		public int OnLineCheckSpan
		{			
			set
			{
				this.onlineChecker.CheckSpan = value ;
			}
		}	

		#region NetDisplayer
		private INetDisplayer netDisplayer = new EmptyNetDisplayer() ; 
		public INetDisplayer NetDisplayer
		{
			set
			{
				this.netDisplayer = value ;
			}
		}
		#endregion
		#endregion

		private void onlineChecker_SomeConnectionTimeOuted(string userID)
		{
			this.UnRegisterUser(userID) ;			
		}
	}

	#region EmptyNetDisplayer
	internal class EmptyNetDisplayer :INetDisplayer
	{
		#region INetDisplayer ��Ա

		public void ClearAll()
		{
			// TODO:  ��� EmptyNetDisplayer.ClearAll ʵ��
		}

		public void RegisterUser(string userID)
		{
			// TODO:  ��� EmptyNetDisplayer.RegisterUser ʵ��
		}

		public void RegisterUserEx(string phoneID, string affix)
		{
			// TODO:  ��� EmptyNetDisplayer.RegisterUserEx ʵ��
		}

		public void UngisterUser(string userID)
		{
			// TODO:  ��� EmptyNetDisplayer.UngisterUser ʵ��
		}

		public void RegisterUserServicedInfo(string phoneID, string serviceName, int dataLen)
		{
			// TODO:  ��� EmptyNetDisplayer.RegisterUserServicedInfo ʵ��
		}

		#endregion

	}
	#endregion

}
