using System;
using ESFramework.Passive ;
using ESBasic.Addins;

namespace ESFramework.Addins
{
	/// <summary>
	/// IPassiveAddin ���ڿͻ��˵Ĳ����ͨ��һ��PassiveAddin��Ӧ��һ������˵Ĺ��ܲ��FunAddin
	/// zhuweisky 2006.03.13
	/// </summary>
	public interface IPassiveAddin : IAddin
	{
		Type AddinFormType{get ;} //AddinFormType����ʵ��IPassiveAddinForm�ӿ�
	}

	public interface IPassiveAddinForm
	{
		/// <summary>
        /// Initialize ��IServerAgent���ݸ�Passive�����PassiveAddinͨ��IServerAgent�������󲢻�ȡ���
		/// </summary>	
		void Initialize(IServerAgent serverAgent ,string userID) ;
	}
}
