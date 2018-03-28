using System;

namespace ESFramework.Core
{
	/// <summary>
	/// INakeDispatcher �ڲ����������Ϣ����INakeDispatcher�ڲ����Ͳ�����Spy��Hook��ء�
	/// zhuweisky 2006.05.25
	/// </summary>
	public interface INakeDispatcher
	{
        IProcesserFactory ProcesserFactory { set; }
		NetMessage DispatchMessage(NetMessage msg) ;
	}	

}
