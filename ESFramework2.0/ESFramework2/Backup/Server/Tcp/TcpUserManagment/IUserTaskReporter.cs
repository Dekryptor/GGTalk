using System;
using System.Collections.Generic;

namespace ESFramework.Server.Tcp.UserManagment
{
	/// <summary>
	/// IUserActionReporter 用户在退出时将客户本次登录的请求清单永久存储起来。
	/// IUserActionReporter的实现可将信息写入数据库或文件，甚至可以发送到专门用户服务器的消息队列。
	/// 2005.06.08
	/// </summary>
	public interface IUserTaskReporter
	{
		void RecordTaskList(ITaskMainRecord mainRecord) ;
		ITaskMainRecord    CreateRudeTaskMainRecord() ;
		ITaskDetailRecord  CreateRudeTaskDetailRecord() ;
	}			

	public interface ITaskMainRecord
	{		
		string	 UserID{get ;set ;}
		int		 TotalDataCount{get ;set ;}
		DateTime TimeLogon{get ;set ;}
		DateTime TimeLogout{get ;set ;}		
		int		 RequestCount{get ;set ;}

        IList<ITaskDetailRecord> Details { get;set;}//details中为ITaskDetailRecord
	}

	public interface ITaskDetailRecord
	{
		int      ServiceKey{get ;set ;}
		int		 DataCount{get ;set ;}
		DateTime RequestTime{get ;set ;}		
	}

	#region EmptyUserTaskReporter
	public class EmptyUserTaskReporter :IUserTaskReporter
	{
		#region IUserTaskReporter 成员

		public void RecordTaskList(ITaskMainRecord mainRecord)
		{
			// TODO:  添加 EmptyUserTaskReporter.RecordTaskList 实现
		}

		public ITaskMainRecord CreateRudeTaskMainRecord()
		{
			// TODO:  添加 EmptyUserTaskReporter.CreateRudeTaskMainRecord 实现
			return new EmptyTaskMainRecord() ;
		}

		public ITaskDetailRecord CreateRudeTaskDetailRecord()
		{
			// TODO:  添加 EmptyUserTaskReporter.CreateRudeTaskDetailRecord 实现
			return new EmptyTaskDetailRecord();
		}

		#endregion
	}
	#endregion

	#region EmptyTaskMainRecord
	public class EmptyTaskMainRecord : ITaskMainRecord
	{
		public EmptyTaskMainRecord()
		{
		}	
	
		#region ID
		private int iD = 0 ; 
		/// <summary>
		/// ID 自动编号。 
		/// </summary>
		public int ID
		{
			get
			{
				return this.iD ;
			}
			set
			{
				this.iD = value ;
			}
		}
		#endregion	
	
		#region UserID
		private string userID = "" ; 
		/// <summary>
		/// UserID 用户号码。 通常为手机号
		/// </summary>
		public string UserID
		{
			get
			{
				return this.userID ;
			}
			set
			{
				this.userID = value ;
			}
		}
		#endregion	
	
		#region TotalDataCount
		private int totalDataCount = 0 ; 
		/// <summary>
		/// TotalDataCount 下载数据量。 本次登录期间下载的总的数据量（包括消息头长度）
		/// </summary>
		public int TotalDataCount
		{
			get
			{
				return this.totalDataCount ;
			}
			set
			{
				this.totalDataCount = value ;
			}
		}
		#endregion	
	
		#region TimeLogon
		private DateTime timeLogon = DateTime.Now ; 
		/// <summary>
		/// TimeLogon 登录时间。 
		/// </summary>
		public DateTime TimeLogon
		{
			get
			{
				return this.timeLogon ;
			}
			set
			{
				this.timeLogon = value ;
			}
		}
		#endregion	
	
		#region TimeLogout
		private DateTime timeLogout = DateTime.Now ; 
		/// <summary>
		/// TimeLogout 退出时间。 也有可能是掉线
		/// </summary>
		public DateTime TimeLogout
		{
			get
			{
				return this.timeLogout ;
			}
			set
			{
				this.timeLogout = value ;
			}
		}
		#endregion	
	
		#region RequestCount
		private int requestCount = 0 ; 
		/// <summary>
		/// RequestCount 请求的总次数。 本次登录总共作了多少次请求（包括登录请求）
		/// </summary>
		public int RequestCount
		{
			get
			{
				return this.requestCount ;
			}
			set
			{
				this.requestCount = value ;
			}
		}
		#endregion
	
		#region ToString 
		public override string ToString()
		{
			return this.ID.ToString() ;
		}
		#endregion

		#region ITaskMainRecord 成员
        private IList<ITaskDetailRecord> details = new List<ITaskDetailRecord>();

        public IList<ITaskDetailRecord> Details
		{
			get
			{
				return this.details ;
			}
			set
			{
				this.details = value ;
			}
		}

		#endregion
	}
	#endregion	

	#region EmptyTaskDetailRecord
	public class EmptyTaskDetailRecord : ITaskDetailRecord
	{
		public EmptyTaskDetailRecord()
		{
		}
	
	
		#region ID
		private int iD = 0 ; 
		/// <summary>
		/// ID 自动编号。 
		/// </summary>
		public int ID
		{
			get
			{
				return this.iD ;
			}
			set
			{
				this.iD = value ;
			}
		}
		#endregion
	
	
		#region MainID
		private int mainID = 0 ; 
		/// <summary>
		/// MainID 主记录编号。 TaskMainRecord的主键
		/// </summary>
		public int MainID
		{
			get
			{
				return this.mainID ;
			}
			set
			{
				this.mainID = value ;
			}
		}
		#endregion
	
	
		#region ServiceKey
		private int serviceKey = 0 ; 
		/// <summary>
		/// ServiceKey 服务类型关键字。 
		/// </summary>
		public int ServiceKey
		{
			get
			{
				return this.serviceKey ;
			}
			set
			{
				this.serviceKey = value ;
			}
		}
		#endregion
	
	
		#region DataCount
		private int dataCount = 0 ; 
		/// <summary>
		/// DataCount 数据量。 
		/// </summary>
		public int DataCount
		{
			get
			{
				return this.dataCount ;
			}
			set
			{
				this.dataCount = value ;
			}
		}
		#endregion
	
	
		#region RequestTime
		private DateTime requestTime = DateTime.Now ; 
		/// <summary>
		/// RequestTime 请求时间。 
		/// </summary>
		public DateTime RequestTime
		{
			get
			{
				return this.requestTime ;
			}
			set
			{
				this.requestTime = value ;
			}
		}
		#endregion
	
		#region ToString 
		public override string ToString()
		{
			return this.ID.ToString() ;
		}
		#endregion
	}
	#endregion
}
