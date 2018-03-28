using System;
using System.Reflection;
using System.Collections.Generic;
using ESBasic;

namespace ESFramework.Network.Extend
{
	/// <summary>
    /// IMessageTypeRoom 消息类型空间，用于定义一组相同性质的消息类型，比如所有的FTP消息类型。
	/// </summary>
	public interface IMessageTypeRoom
	{
		void     Initialize() ; //依据StartKey的设置，初始化各个key的值
		int      StartKey{set;}		
		KeyScope KeyScope{get ;}
		string   KeyScopeStr{get ;}

        IList<int> PushKeyList { get;}
    }

    #region BaseMessageTypeRoom
    /// <summary>
    /// BaseMessageTypeRoom 具体的消息类型Room的基类
	/// 具体的实现类只需要加入具体的ServiceKeys和PushKeyList
	/// </summary>
    public abstract class BaseMessageTypeRoom : IMessageTypeRoom
	{
		#region IMessageType 成员
		#region StartKey
		protected int startKey = 0 ;
		public int StartKey
		{
			set
			{	
				this.startKey = value ;
			}
		}
		#endregion

        public abstract IList<int> PushKeyList { get;}

		public KeyScope KeyScope
		{
			get
			{				
				return new KeyScope(this.startKey ,this.maxKeyValue);
			}
		}

		public string KeyScopeStr
		{
			get
			{
				return this.KeyScope.ScopeString ;
			}
		}

		private int maxKeyValue = 0 ;

		public virtual void Initialize()
		{
			this.maxKeyValue = this.startKey ;
			Type curType = this.GetType() ;

			PropertyInfo[] pros = curType.GetProperties(BindingFlags.Default |BindingFlags.Public | BindingFlags.Instance) ;
			foreach(PropertyInfo info in pros)
			{
				if((!info.CanWrite) || (info.PropertyType != typeof(int)) || (info.Name == "StartKey"))
				{
					continue ;
				}
				
				int originVal = (int)info.GetValue(this ,null) ;
				int keyVal = originVal + this.startKey;
				if(keyVal > this.maxKeyValue)
				{
					this.maxKeyValue = keyVal ;
				}
				info.SetValue(this ,keyVal ,null) ;			
			}
		}

		#endregion
	}
	#endregion

}
