using System;

namespace ESFramework.Core
{
	/// <summary>
	/// SingleProcesserFactory ��װ����������Ϊһ��������������
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

		#region IProcesserFactory ��Ա

        public IMessageProcesser CreateProcesser(int requestType, int serverTypeKey)
		{
            return this.messageProcesser;
		}

		#endregion
	}
}
