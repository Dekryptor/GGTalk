using System;

namespace ESFramework.Core
{
	/// <summary>
	/// INakeDispatcher 内层分派器，消息到达INakeDispatcher内部，就不再与Spy或Hook相关。
	/// zhuweisky 2006.05.25
	/// </summary>
	public interface INakeDispatcher
	{
        IProcesserFactory ProcesserFactory { set; }
		NetMessage DispatchMessage(NetMessage msg) ;
	}	

}
