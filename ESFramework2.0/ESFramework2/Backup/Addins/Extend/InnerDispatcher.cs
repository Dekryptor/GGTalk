using System;
using System.Reflection ;
using System.Collections.Generic ;

namespace ESFramework.Addins
{
	/// <summary>
	/// InnerDispatcher �����ڲ���ڲ�������Ϣ���ɡ��������Ҫ�������Ϣ���ServiceItemIndex���ر��ʱ��
	/// ʹ��InnerDispatcher���ServiceItemIndex���з��ɡ�
	/// zhuweisky 205.04.25
	/// </summary>
	public interface IInnerDispatcher
	{		
		string AddinAssemblyName{set ;} //��������򼯵�����

		void Initialize() ;
        IInnerProcesser GetProcesser(int serviceIndex);
	}

	public class InnerDispatcher :IInnerDispatcher
	{
        private IDictionary<int, IInnerProcesser> dicDealer = new Dictionary<int, IInnerProcesser>();

		#region AddinAssemblyName ��ǰ������򼯵����ƣ���������׺����
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

		#region IInnerDispatcher ����
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
