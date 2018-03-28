using System;
using System.IO ;
using ESBasic ;
using ESBasic.Logger;

namespace ESFramework
{
	/// <summary>
	/// ILogger 用于记录日志信息。通常可以通过ESFramework.Common.AdvancedFunction.SetProperty方法来简化组件的日志记录器
	///			的装配。
	/// 注意，所有需要日志记录的组件请使用名为"EsbLogger"的属性注入。
	/// zhuweisky
	/// </summary>
	public interface IEsbLogger
	{
		void Log(string errorType ,string msg ,string location ,ErrorLevel level) ;
		bool Enabled{set ;}		
		int  LogLevel{set ;} //只有等级大于等于该值的错误才会被记录 (0 - 5)
	}

	[EnumDescription("异常/错误严重级别")]
	public enum ErrorLevel
	{
		[EnumDescription("致命的" ,4)]
		Fatal,
		[EnumDescription("高" ,3)]
		High ,
		[EnumDescription("普通" ,2)]
		Standard ,
		[EnumDescription("低" ,1)]
		Low
	} 

	#region FileEsbLogger
	/// <summary>
	/// FileEsbLogger 将日志记录到文本文件
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

		#region IEsbLogger 成员

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

				string ss = string.Format("\n{0} : {1} －－ {2} 。错误类型:{3}。位置：{4}" ,DateTime.Now.ToString() ,EnumDescription.GetFieldText(level) ,msg ,errorType ,location) ;
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

		#region IDisposable 成员

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
		#region ILogger 成员

		public void Log(string errorType ,string msg, string location, ErrorLevel level)
		{
			
		}		

		public bool Enabled
		{
			set
			{
				// TODO:  添加 EmptyEsbLogger.Enabled setter 实现
			}
		}
		
		public int LogLevel
		{
			set
			{
				// TODO:  添加 EmptyEsbLogger.LogLevel setter 实现
			}
		}

		#endregion
	}
	#endregion



	
}
