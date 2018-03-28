using System;

namespace ESFramework.Core
{
	/// <summary>
    /// IProcesserFactory ��������������������������ʹ�����Ӧ�Ĵ����� ��
	/// 2005.07.12
	/// </summary>
    public interface IProcesserFactory
	{
        IMessageProcesser CreateProcesser(int serviceKey, int serverTypeKey);//serverTypeKey ������д���		
    }

    #region EmptyProcesserFactory
    public class EmptyProcesserFactory : IProcesserFactory
    {
        #region IProcesserFactory ��Ա
        public IMessageProcesser CreateProcesser(int requestType, int serverTypeKey)
		{			
			return null;
		}

		#endregion

	}
	#endregion


	
}
