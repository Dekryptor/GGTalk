using System;

namespace ESFramework.Core
{	
	// Message��DataStream����������ʾ�����ڷ����ͨ����֪��Body�Ľ���������Message�������ڿͻ��ˣ�
	// ��NetMessage��S/C���˶������á�
	public class Message 
	{
		private IMessageHeader header ;
		private IContract body   ;	

		public Message(IMessageHeader theHeader ,IContract theBody)
		{
			this.header = theHeader ;
			this.body   = theBody ;			
		}	
	
		public NetMessage ToNetMessage()
		{
			if(this.body == null)
			{
				return new NetMessage(this.header ,null) ;
			}

			return new NetMessage(this.header ,this.body.ToStream()) ;
		}
	
		#region ToStream ,GetStreamLength
		public int GetStreamLength()
		{
			int len = this.header.GetStreamLength() ;

			if(this.body != null)
			{
				len += this.body.GetStreamLength() ;
			}

			return len ;
		}

		public byte[] ToStream()
		{
			int totalLen = this.GetStreamLength() ;
			byte[] stream = new byte[totalLen] ;

			this.header.ToStream(stream ,0) ;
			if(this.body != null)
			{
				this.body.ToStream(stream ,this.header.GetStreamLength()) ;
			}

			return stream ;
		}

		public void ToStream(byte[] buff, int offset)
		{
			this.header.ToStream(buff ,offset) ;
			if(this.body != null)
			{
				this.body.ToStream(buff ,offset +this.header.GetStreamLength()) ;
			}			
		}
		#endregion

		#region Header ,Body 
		public IMessageHeader Header
		{
			get
			{
				return this.header ;
			}
		}

		public IContract Body
		{
			get
			{
				return this.body ;
			}
		}	
		#endregion	
		
	}
}
