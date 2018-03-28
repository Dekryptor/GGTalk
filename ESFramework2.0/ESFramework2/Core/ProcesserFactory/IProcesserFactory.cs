using System;

namespace ESFramework.Core
{
	/// <summary>
    /// IProcesserFactory 请求处理器工厂，根据请求的类型创建对应的处理器 。
	/// 2005.07.12
	/// </summary>
    public interface IProcesserFactory
	{
        IMessageProcesser CreateProcesser(int serviceKey, int serverTypeKey);//serverTypeKey 比如城市代号		
    }

    #region EmptyProcesserFactory
    public class EmptyProcesserFactory : IProcesserFactory
    {
        #region IProcesserFactory 成员
        public IMessageProcesser CreateProcesser(int requestType, int serverTypeKey)
		{			
			return null;
		}

		#endregion

	}
	#endregion


	
}
