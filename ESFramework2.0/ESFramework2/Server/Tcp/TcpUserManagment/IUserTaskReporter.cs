using System;
using System.Collections.Generic;

namespace ESFramework.Server.Tcp.UserManagment
{
	/// <summary>
	/// IUserActionReporter �û����˳�ʱ���ͻ����ε�¼�������嵥���ô洢������
	/// IUserActionReporter��ʵ�ֿɽ���Ϣд�����ݿ���ļ����������Է��͵�ר���û�����������Ϣ���С�
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

        IList<ITaskDetailRecord> Details { get;set;}//details��ΪITaskDetailRecord
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
		#region IUserTaskReporter ��Ա

		public void RecordTaskList(ITaskMainRecord mainRecord)
		{
			// TODO:  ��� EmptyUserTaskReporter.RecordTaskList ʵ��
		}

		public ITaskMainRecord CreateRudeTaskMainRecord()
		{
			// TODO:  ��� EmptyUserTaskReporter.CreateRudeTaskMainRecord ʵ��
			return new EmptyTaskMainRecord() ;
		}

		public ITaskDetailRecord CreateRudeTaskDetailRecord()
		{
			// TODO:  ��� EmptyUserTaskReporter.CreateRudeTaskDetailRecord ʵ��
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
		/// ID �Զ���š� 
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
		/// UserID �û����롣 ͨ��Ϊ�ֻ���
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
		/// TotalDataCount ������������ ���ε�¼�ڼ����ص��ܵ���������������Ϣͷ���ȣ�
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
		/// TimeLogon ��¼ʱ�䡣 
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
		/// TimeLogout �˳�ʱ�䡣 Ҳ�п����ǵ���
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
		/// RequestCount ������ܴ����� ���ε�¼�ܹ����˶��ٴ����󣨰�����¼����
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

		#region ITaskMainRecord ��Ա
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
		/// ID �Զ���š� 
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
		/// MainID ����¼��š� TaskMainRecord������
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
		/// ServiceKey �������͹ؼ��֡� 
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
		/// DataCount �������� 
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
		/// RequestTime ����ʱ�䡣 
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
