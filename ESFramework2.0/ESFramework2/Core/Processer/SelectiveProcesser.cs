using System;
using System.Collections.Generic ;

namespace ESFramework.Core
{
	/// <summary>
    /// SelectiveProcesser 选择性处理器（链）。
    /// (1)分派器需要清楚每个处理器能处理的消息类型，而processerList对处理器不需要任何了解。它只是逐个调用处理器。
	/// (2)当一个不需要回复的消息需要被多个处理器处理时，可以使用DataDealerList。
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

		#region IMessageProcesser 成员
		/// <summary>
		/// 依次调用链表中的各个处理器来处理消息，直到有一个处理器返回非空时，该方法将该结果返回;
		/// 如果所有的处理器都返回null，则DataDealerList.DealRequestMessage返回null。
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
