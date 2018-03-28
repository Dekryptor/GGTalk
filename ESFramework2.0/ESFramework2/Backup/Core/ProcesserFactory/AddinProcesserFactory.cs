using System;
using ESBasic.Addins;
using ESFramework.Addins;

namespace ESFramework.Core
{
	/// <summary>
	/// AddinProcesserFactory 功能插件处理器工厂，每一个功能插件能转换为一个处理器
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

		#region IProcesserFactory 成员
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
