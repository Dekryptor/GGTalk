using System;
using System.Collections.Generic ;

namespace ESFramework.Core
{
	/// <summary>
    /// SelectiveProcesser ѡ���Դ�������������
    /// (1)��������Ҫ���ÿ���������ܴ������Ϣ���ͣ���processerList�Դ���������Ҫ�κ��˽⡣��ֻ��������ô�������
	/// (2)��һ������Ҫ�ظ�����Ϣ��Ҫ���������������ʱ������ʹ��DataDealerList��
	/// zhuweisky 2006.06.08
	/// </summary>
	public class SelectiveProcesser :IMessageProcesser
	{
        private IList<IMessageProcesser> processerList = new List<IMessageProcesser>();

        public SelectiveProcesser()
		{
        }

        #region ProcesserList
        public IList<IMessageProcesser> ProcesserList
		{
			set
			{
				if(value != null)
				{
                    this.processerList = value;
				}
			}
		}
		#endregion		

		#region IMessageProcesser ��Ա
		/// <summary>
		/// ���ε��������еĸ�����������������Ϣ��ֱ����һ�����������طǿ�ʱ���÷������ý������;
		/// ������еĴ�����������null����DataDealerList.DealRequestMessage����null��
		/// </summary>	
        public NetMessage ProcessMessage(NetMessage reqMsg)
		{
            foreach (IMessageProcesser dealer in this.processerList)
			{
				NetMessage response = dealer.ProcessMessage(reqMsg) ;
				if(response != null)
				{
					return response ;
				}
			}

			return null ;
		}

		#endregion
	}
}
