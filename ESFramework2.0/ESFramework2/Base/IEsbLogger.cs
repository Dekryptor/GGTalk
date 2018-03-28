using System;
using System.IO ;
using ESBasic ;
using ESBasic.Logger;

namespace ESFramework
{
	/// <summary>
	/// ILogger ���ڼ�¼��־��Ϣ��ͨ������ͨ��ESFramework.Common.AdvancedFunction.SetProperty���������������־��¼��
	///			��װ�䡣
	/// ע�⣬������Ҫ��־��¼�������ʹ����Ϊ"EsbLogger"������ע�롣
	/// zhuweisky
	/// </summary>
	public interface IEsbLogger
	{
		void Log(string errorType ,string msg ,string location ,ErrorLevel level) ;
		bool Enabled{set ;}		
		int  LogLevel{set ;} //ֻ�еȼ����ڵ��ڸ�ֵ�Ĵ���Żᱻ��¼ (0 - 5)
	}

	[EnumDescription("�쳣/�������ؼ���")]
	public enum ErrorLevel
	{
		[EnumDescription("������" ,4)]
		Fatal,
		[EnumDescription("��" ,3)]
		High ,
		[EnumDescription("��ͨ" ,2)]
		Standard ,
		[EnumDescription("��" ,1)]
		Low
	} 

	#region FileEsbLogger
	/// <summary>
	/// FileEsbLogger ����־��¼���ı��ļ�
	/// </summary>
	public class FileEsbLogger :IEsbLogger ,IDisposable
	{
        private FileLogger fileLogger;

		#region FilePath
		private string filePath = "" ; 
		public string FilePath
		{
			set
			{
				this.filePath = value ;
			}
		}
		#endregion		

		#region Ctor
		public FileEsbLogger()
		{
		}

		public FileEsbLogger(string file_Path)
		{
			this.filePath = file_Path ;
		}		
		#endregion

        #region FileLogger
        private FileLogger FileLogger
        {
            get
            {
                if (this.fileLogger == null)
                {
                    this.fileLogger = new FileLogger(this.filePath);
                }

                return this.fileLogger;
            }
        } 
        #endregion

		#region IEsbLogger ��Ա

		public void Log(string errorType, string msg, string location, ErrorLevel level)
		{
			try
			{
				if(! this.enabled)
				{
					return ;
				}
			
				int lev = int.Parse(EnumDescription.GetFieldTag(level).ToString()) ;
				if(lev < this.logLevel)
				{
					return ;
				}               

				string ss = string.Format("\n{0} : {1} ���� {2} ����������:{3}��λ�ã�{4}" ,DateTime.Now.ToString() ,EnumDescription.GetFieldText(level) ,msg ,errorType ,location) ;
                this.FileLogger.Log(ss);
			}
			catch(Exception ee)
			{
				ee = ee ;
			}
		}

		#region Enabled
		private bool enabled = true ; 
		public bool Enabled
		{
			set
			{
				this.enabled = value ;
			}
		}
		#endregion
		
		#region LogLevel
		private int logLevel = 0 ; 
		public  int LogLevel
		{
			set
			{
				this.logLevel = value ;
			}
		}
		#endregion

		#endregion

		#region IDisposable ��Ա

		public void Dispose()
		{
            this.fileLogger.Dispose();
		}

		#endregion
	}
	#endregion

	#region EmptyLogger
	public class EmptyEsbLogger :IEsbLogger
	{
		#region ILogger ��Ա

		public void Log(string errorType ,string msg, string location, ErrorLevel level)
		{
			
		}		

		public bool Enabled
		{
			set
			{
				// TODO:  ��� EmptyEsbLogger.Enabled setter ʵ��
			}
		}
		
		public int LogLevel
		{
			set
			{
				// TODO:  ��� EmptyEsbLogger.LogLevel setter ʵ��
			}
		}

		#endregion
	}
	#endregion



	
}
