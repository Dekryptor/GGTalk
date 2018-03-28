using System;
using ESBasic ;
using ESBasic.Helpers ;

namespace ESFramework.Core
{
	/// <summary>
	/// IContract ���ڳ���ͨ��Э���ʽ�Ļ����ӿڡ���һ������/�ظ���Ϣ��װ��һ��ʵ��IContract��Э�����
	/// �����Ҫ��������Ϊ��������Ҫ����ʵ��
    /// zhuweisky 2005.12
	/// </summary>
	public interface IContract
	{		
		int    GetStreamLength() ;

		byte[] ToStream() ;              //������ת��Ϊ��
		void   ToStream(byte[] buff ,int offset);		
	}	
	
	#region BaseSerializeContract
	/// <summary>
	/// BaseSerializeContract ���Э�����.NET���������л��ķ�ʽ���������໥ת�������ʹ�ô���Ϊ���ࡣ
	/// �����Э�������ֻ࣬��Ҫ���Э���Ա���ɡ�
	/// </summary>
	[Serializable]
	public abstract class BaseSerializeContract :IContract 
	{
		public BaseSerializeContract(){}	

		#region IContract ��Ա	
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
