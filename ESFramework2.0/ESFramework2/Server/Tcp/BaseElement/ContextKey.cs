using System;

namespace ESFramework.Server.Tcp
{
	/// <summary>
	/// ContextKey ���ڽ�������һ���û�TCP������ص���Ϣ��װ������	
	/// ���ߣ���ΰ sky.zhuwei@163.com 
	/// </summary>
	public class ContextKey
	{		
		private byte[]  buffer ;          //��װ���ջ�����
		private ISafeNetworkStream netStream = null ;			
		private volatile bool      isDataManaging = false ;
		
		public ContextKey(ISafeNetworkStream net_Stream ,int buffSize)
		{
			this.netStream = net_Stream ;			
			this.buffer    = new byte[buffSize] ;			
		}

		#region NetStream  
		public ISafeNetworkStream NetStream
		{
			get
			{
				return this.netStream ;
			}
		}			

		public byte[] Buffer
		{
			get
			{
				return this.buffer ;
			}			
		}		

		public bool IsDataManaging
		{
			get
			{
				return this.isDataManaging ;
			}
			set
			{
				this.isDataManaging = value ;
			}
		}

		private bool firstMessageExist = false ;
		public  bool FirstMessageExist 
		{
			get
			{
				return this.firstMessageExist ;
			}
			set
			{
				this.firstMessageExist = value ;
			}
		}
		#endregion			
	}	
}
