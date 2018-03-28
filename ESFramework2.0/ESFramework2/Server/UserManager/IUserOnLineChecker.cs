using System;
using System.Collections ;
using System.Threading ;
using ESBasic;

namespace ESFramework.Server
{
	/// <summary>
	/// ITcpUserOnLineChecker 用于定时检查用户在线状态，如果在指定的时间间隔内没有收到用户的任何信息，则视为用户掉线。
	/// 作者：朱伟 sky.zhuwei@163.com 
	/// </summary>
	public interface IUserOnLineChecker
	{		
		void Start() ;
		void Stop() ;

		void RegisterOrActivateUser(string userID) ;		
		void UnregisterUser(string userID) ; //被外界调用UnregisterUser时，不触发CheckSomeOneDisConnected事件

		int CheckSpan{get ;set ;} //Minutes

		event CbSimpleStr  SomeConnectionTimeOuted ; //仅是当定时检查出用户掉线时才触发
	}	

	#region UserOnLineChecker
	public class UserOnLineChecker :IUserOnLineChecker
	{
		private Timer timerForCheckOnLine = null ;
		
		private Hashtable htableUserState = new Hashtable() ;
		private bool isStop = true ;

		#region ITcpUserOnLineChecker 成员

		public event CbSimpleStr  SomeConnectionTimeOuted = null ;

		#region CheckSpan
		private int checkUserSpan = 3 ;//minutes
		public  int CheckSpan
		{
			get
			{
				return this.checkUserSpan ;
			}
			set
			{
				this.checkUserSpan = value ;
			}
		}
		#endregion		

		public void Start()
		{
			this.SomeConnectionTimeOuted = null ;	

			if((this.checkUserSpan > 0) && this.isStop)
			{
				this.timerForCheckOnLine = new Timer(new TimerCallback(this.OnLineCheckAction),null ,this.checkUserSpan * 60000 ,this.checkUserSpan * 60000 ) ;
				this.isStop = false ;
			}
		}

		public void Stop()
		{
			if(this.isStop)
			{
				return ;
			}

			this.isStop = true ;

			lock(this.htableUserState)
			{
				this.htableUserState.Clear() ;
			}

			if(this.timerForCheckOnLine != null)
			{
				this.timerForCheckOnLine.Dispose() ;
				this.timerForCheckOnLine = null ;
			}
		}

		public void RegisterOrActivateUser(string userID)
		{
			lock(this.htableUserState)
			{
				if(this.htableUserState[userID] != null)
				{
					this.htableUserState[userID] = true ;
				}
				else
				{
					this.htableUserState.Add(userID ,true) ;
				}
			}
		}
	
		public void UnregisterUser(string userID)
		{
			lock(this.htableUserState)
			{
				if(this.htableUserState[userID] != null)
				{
					this.htableUserState.Remove(userID) ;
				}
			}
		}

		#endregion

		#region OnLineCheckAction
		private void OnLineCheckAction(object state)
		{
			ArrayList list = new ArrayList() ;
			lock(this.htableUserState)
			{
				foreach(string userID in this.htableUserState.Keys)
				{
					if(false == (bool)this.htableUserState[userID])
					{
						list.Add(userID) ;
					}
				}

				foreach(string userID in list)
				{
					this.htableUserState.Remove(userID) ;
				}

				string[] arrayKeys = new string[this.htableUserState.Keys.Count] ;
				this.htableUserState.Keys.CopyTo(arrayKeys ,0) ;
				foreach(string key in arrayKeys)
				{
					this.htableUserState[key] = false ;
				}
			}

			if(this.SomeConnectionTimeOuted != null)
			{
				foreach(string userID in list)
				{				
					this.SomeConnectionTimeOuted(userID ) ;
				}
			}
		}
		#endregion
	}

	#endregion	
}
