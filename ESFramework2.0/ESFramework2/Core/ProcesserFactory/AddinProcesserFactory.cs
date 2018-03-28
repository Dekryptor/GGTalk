using System;
using ESBasic.Addins;
using ESFramework.Addins;

namespace ESFramework.Core
{
	/// <summary>
	/// AddinProcesserFactory ���ܲ��������������ÿһ�����ܲ����ת��Ϊһ��������
	/// </summary>
	public class AddinProcesserFactory :IProcesserFactory
	{
		private IAddinManagement addinManagement ;

        public AddinProcesserFactory()
		{			
		}

		#region property
		public IAddinManagement AddinManagement
		{
			set
			{
				this.addinManagement = value ;
			}
		}
		#endregion

		#region IProcesserFactory ��Ա
        public IMessageProcesser CreateProcesser(int serviceKey, int serverTypeKey)
		{
			foreach(IAddin addin in this.addinManagement.AddinList)
			{
                if ((addin.ServiceKey == serviceKey) && (addin.Enabled))
				{
                    IFunAddin dealer = addin as IFunAddin;
                    return dealer;
				}
			}

			return null;
		}

		#endregion
	}
}
