using System;
using System.Collections.Generic;
using ESFramework.Core;

namespace ESFramework.Addins
{
	/// <summary>
    /// IInnerProcesser ����ڲ������������ServiceItemIndex���д���
	/// </summary>
	public interface IInnerProcesser
	{
		IList<int> ServiceIndexCollection{get ;}

		NetMessage ProcessMessage(NetMessage reqMsg) ;
	}
}
