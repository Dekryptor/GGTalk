using System;
using ESFramework.Passive ;
using ESBasic.Addins;

namespace ESFramework.Addins
{
	/// <summary>
	/// IPassiveAddin 用于客户端的插件。通常一个PassiveAddin对应着一个服务端的功能插件FunAddin
	/// zhuweisky 2006.03.13
	/// </summary>
	public interface IPassiveAddin : IAddin
	{
		Type AddinFormType{get ;} //AddinFormType必须实现IPassiveAddinForm接口
	}

	public interface IPassiveAddinForm
	{
		/// <summary>
        /// Initialize 将IServerAgent传递给Passive插件，PassiveAddin通过IServerAgent发送请求并获取结果
		/// </summary>	
		void Initialize(IServerAgent serverAgent ,string userID) ;
	}
}
