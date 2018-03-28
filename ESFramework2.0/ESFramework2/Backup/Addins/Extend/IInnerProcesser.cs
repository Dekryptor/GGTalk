using System;
using System.Collections.Generic;
using ESFramework.Core;

namespace ESFramework.Addins
{
	/// <summary>
    /// IInnerProcesser 插件内部处理器，针对ServiceItemIndex进行处理。
	/// </summary>
	public interface IInnerProcesser
	{
		IList<int> ServiceIndexCollection{get ;}

		NetMessage ProcessMessage(NetMessage reqMsg) ;
	}
}
