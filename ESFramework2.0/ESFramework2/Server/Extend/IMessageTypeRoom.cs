using System;
using System.Reflection;
using System.Collections.Generic;
using ESBasic;

namespace ESFramework.Network.Extend
{
	/// <summary>
    /// IMessageTypeRoom ��Ϣ���Ϳռ䣬���ڶ���һ����ͬ���ʵ���Ϣ���ͣ��������е�FTP��Ϣ���͡�
	/// </summary>
	public interface IMessageTypeRoom
	{
		void     Initialize() ; //����StartKey�����ã���ʼ������key��ֵ
		int      StartKey{set;}		
		KeyScope KeyScope{get ;}
		string   KeyScopeStr{get ;}

        IList<int> PushKeyList { get;}
    }

    #region BaseMessageTypeRoom
    /// <summary>
    /// BaseMessageTypeRoom �������Ϣ����Room�Ļ���
	/// �����ʵ����ֻ��Ҫ��������ServiceKeys��PushKeyList
	/// </summary>
    public abstract class BaseMessageTypeRoom : IMessageTypeRoom
	{
		#region IMessageType ��Ա
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
