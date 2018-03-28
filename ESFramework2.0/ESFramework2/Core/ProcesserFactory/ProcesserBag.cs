using System;
using ESBasic;

namespace ESFramework.Core
{
	/// <summary>
    /// ProcesserBag 封装了一个dealer和该dealer能处理的消息Servicekey范围、以及描述＝》方便配置。
    /// (1)MessageProcesser属性或ProcesserFactory属性必须设置一个
    /// (2)KeyScopeStr属性或MessageTypeRoom属性必须设置一个，表明该处理器能处理哪些类型的消息
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
        /// KeyScopeStr 表明该处理器能处理哪些类型的消息 ，可由ESFrameworkPlus.Message.MessageTypeRoom.KeyScopeStr提供
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
        /// Description 方便清晰的说明配置项
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
