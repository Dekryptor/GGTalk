using System;

namespace ESFramework.Core
{
	/// <summary>
	/// SingleProcesserFactory 包装单个处理器为一个处理器工厂。
	/// </summary>
	public class SingleProcesserFactory :IProcesserFactory
	{
        private IMessageProcesser messageProcesser;

		public SingleProcesserFactory(){}

        public SingleProcesserFactory(IMessageProcesser processer)
		{
            this.messageProcesser = processer;
		}

		#region property
        public IMessageProcesser MessageProcesser
		{
			set
			{
                this.messageProcesser = value;
			}
		}
		#endregion		

		#region IProcesserFactory 成员

        public IMessageProcesser CreateProcesser(int requestType, int serverTypeKey)
		{
            return this.messageProcesser;
		}

		#endregion
	}
}
