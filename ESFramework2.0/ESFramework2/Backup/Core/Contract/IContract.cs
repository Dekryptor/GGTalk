using System;
using ESBasic ;
using ESBasic.Helpers ;

namespace ESFramework.Core
{
	/// <summary>
	/// IContract 用于抽象通信协议格式的基础接口。将一个请求/回复消息封装成一个实现IContract的协议对象
	/// 如果需要将流解析为对象，则需要另外实现
    /// zhuweisky 2005.12
	/// </summary>
	public interface IContract
	{		
		int    GetStreamLength() ;

		byte[] ToStream() ;              //将对象转化为流
		void   ToStream(byte[] buff ,int offset);		
	}	
	
	#region BaseSerializeContract
	/// <summary>
	/// BaseSerializeContract 如果协议采用.NET二进制序列化的方式与网络流相互转换，则可使用此作为基类。
	/// 具体的协议主体类，只需要添加协议成员即可。
	/// </summary>
	[Serializable]
	public abstract class BaseSerializeContract :IContract 
	{
		public BaseSerializeContract(){}	

		#region IContract 成员	
		public int GetStreamLength()
		{
			return this.ToStream().Length ;
		}

		public byte[] ToStream()
		{
			return SerializeHelper.SerializeObject(this ,FormatterType.BinaryF) ;
		}	

		public void ToStream(byte[] buff ,int offset)
		{
			PublicHelper.CopyData(this.ToStream() ,buff ,offset) ;
		}

		#endregion
	}
	#endregion
}
