using System;
using System.Reflection ;
using System.Collections.Generic ;

namespace ESFramework.Addins
{
	/// <summary>
	/// InnerDispatcher 用于在插件内部进行消息分派。当插件需要处理的消息类别（ServiceItemIndex）特别多时，
	/// 使用InnerDispatcher针对ServiceItemIndex进行分派。
	/// zhuweisky 205.04.25
	/// </summary>
	public interface IInnerDispatcher
	{		
		string AddinAssemblyName{set ;} //本插件程序集的名称

		void Initialize() ;
        IInnerProcesser GetProcesser(int serviceIndex);
	}

	public class InnerDispatcher :IInnerDispatcher
	{
        private IDictionary<int, IInnerProcesser> dicDealer = new Dictionary<int, IInnerProcesser>();

		#region AddinAssemblyName 当前插件程序集的名称（不包含后缀名）
		private string addinAssemblyName = "" ; 
		public  string AddinAssemblyName
		{
			set
			{
				this.addinAssemblyName = value ;
			}
		}
		#endregion		

		#region Ctor
		public InnerDispatcher()
		{			
		}

		public InnerDispatcher(string addinAssName)
		{			
			this.addinAssemblyName = addinAssName ;
		}
		#endregion

		#region IInnerDispatcher 方法
        public IInnerProcesser GetProcesser(int serviceIndex)
        {
            if (this.dicDealer.ContainsKey(serviceIndex))
            {
                return this.dicDealer[serviceIndex];
            }

            return null;
        }


		#region Initialize
		public void Initialize()
		{
			Assembly[] asses = AppDomain.CurrentDomain.GetAssemblies();
			Type supType = typeof(IInnerProcesser)  ;

			foreach (Assembly ass in asses)
			{
				string[] names = ass.FullName.Split(',') ;
				if(names[0].Trim() == this.addinAssemblyName)
				{
					foreach(Type t in ass.GetTypes())
					{
						if(supType.IsAssignableFrom(t) && (!t.IsAbstract) && (! t.IsInterface) )
						{
							IInnerProcesser dealer = (IInnerProcesser)Activator.CreateInstance(t) ;
							if(dealer.ServiceIndexCollection != null)
							{
								foreach(int serKey in dealer.ServiceIndexCollection)
								{
                                    this.dicDealer.Add(serKey, dealer);
								}
							}
						}
					}

					break ;
				}
			}
		}
		#endregion

		#endregion
	}
}
