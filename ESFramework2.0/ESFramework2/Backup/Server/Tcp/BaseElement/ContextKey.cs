using System;

namespace ESFramework.Server.Tcp
{
	/// <summary>
	/// ContextKey 用于将所有与一个用户TCP连接相关的信息封装起来。	
	/// 作者：朱伟 sky.zhuwei@163.com 
	/// </summary>
	public class ContextKey
	{		
		private byte[]  buffer ;          //封装接收缓冲区
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
