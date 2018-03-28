using System;
using ESBasic;

namespace ESFramework.Core
{
	/// <summary>
    /// ProcesserBag ��װ��һ��dealer�͸�dealer�ܴ������ϢServicekey��Χ���Լ����������������á�
    /// (1)MessageProcesser���Ի�ProcesserFactory���Ա�������һ��
    /// (2)KeyScopeStr���Ի�MessageTypeRoom���Ա�������һ���������ô������ܴ�����Щ���͵���Ϣ
	/// zhuweisky 2006.08.17
	/// </summary>
	public class ProcesserBag
	{
		private KeyScope keyScope = null ; 

		#region Ctor
		public ProcesserBag(){}
        public ProcesserBag(IMessageProcesser processer, string scopeStr, string desc)
		{
			this.KeyScopeStr = scopeStr ;
            this.messageProcesser = processer;
			this.description = desc ;
		}
		#endregion

        #region KeyScope ,KeyScopeStr
        /// <summary>
        /// KeyScopeStr �����ô������ܴ�����Щ���͵���Ϣ ������ESFrameworkPlus.Message.MessageTypeRoom.KeyScopeStr�ṩ
		/// </summary>
		public string KeyScopeStr
		{
			set
			{
				this.keyScope = new KeyScope(value) ;
			}
		}

		public KeyScope KeyScope
		{
			set
			{
				this.keyScope = value ;
			}
		}
		#endregion

        #region MessageProcesser
        private IMessageProcesser messageProcesser = null;
        public IMessageProcesser MessageProcesser
		{
			get
			{
                return this.messageProcesser;
			}
			set
			{
                this.messageProcesser = value;
			}
		}
		#endregion

        #region ProcesserFactory
        private IProcesserFactory processerFactory = null;
        public IProcesserFactory ProcesserFactory
		{
			get
			{
                return this.processerFactory;
			}
			set
			{
                this.processerFactory = value;
			}
		}
		#endregion

		#region Description
		private string description = "" ; 
        /// <summary>
        /// Description ����������˵��������
        /// </summary>
		public string Description
		{
			get
			{
				return this.description ;
			}
			set
			{
				this.description = value ;
			}
		}
		#endregion

		#region Contains
		public bool Contains(int serviceKey)
		{
			return this.keyScope.Contains(serviceKey) ;
		}	
		#endregion

        #region GetProcesser
        public IMessageProcesser GetProcesser(int serviceKey)
		{
            if (this.messageProcesser != null)
			{
                return this.messageProcesser;
			}

            return this.processerFactory.CreateProcesser(serviceKey, 0);
		}
		#endregion
	}	
}
