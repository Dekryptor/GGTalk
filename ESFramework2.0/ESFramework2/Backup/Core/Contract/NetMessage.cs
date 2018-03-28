using System;

namespace ESFramework.Core
{
	/// <summary>
	/// INetMessage �������ϴ��ݵ���Ϣ��    
	/// </summary>
	[Serializable]
	public class NetMessage
	{
        #region Header
        private IMessageHeader header;
        public IMessageHeader Header
        {
            get { return header; }
            set { header = value; }
        } 
        #endregion

        #region Body
        private byte[] body = null; 
        /// <summary>
        /// Body ��Ϣ���壬���Ծ����任
        /// </summary>
        public byte[] Body
        {
            get { return body; }
            set { body = value; }
        } 
        #endregion

        #region BodyOffset
        private int bodyOffset = 0;
        public int BodyOffset
        {
            get { return bodyOffset; }
            set { bodyOffset = value; }
        } 
        #endregion

        #region Tag
        private object tag;
        /// <summary>
        /// Tag �����ڽ�NetMessage����IMessageProcesserʱ���ݶ������Ϣ����Ӱ��ToStream���Һ���ʹ�� 
        /// </summary>
        public object Tag
        {
            get { return tag; }
            set { tag = value; }
        }
        
        #endregion

		public NetMessage()
		{
		}

		#region Ctor ,ToStream		
		/// <summary>
		/// NetMessage ��Ctor˵�����body��Ϊnull����BodyOffsetΪ0����this.Header.MessageBodyLength = this.Body.Length 
		/// </summary>	
		public NetMessage(IMessageHeader header ,byte[] body)
		{
			this.Header = header ;
			this.Body   = body ;
			
			if(this.Body == null)
			{
				this.Header.MessageBodyLength = 0 ;
			}
			else
			{
				this.Header.MessageBodyLength = this.Body.Length ;
			}
			
		}		

		public NetMessage(IContract bodyContract ,IMessageHeader header)
		{
			this.Header = header ;
			if(bodyContract != null)
			{
				this.Body = bodyContract.ToStream() ;
			}
			
			if(this.Body == null)
			{
				this.Header.MessageBodyLength = 0 ;
			}
			else
			{
				this.Header.MessageBodyLength = this.Body.Length ;
			}
			
		}

		public NetMessage(IMessageHeader header ,byte[] body, int bodyOffset ,int bodyLen)
		{
			this.Header = header ;
			this.Body   = body ;
			this.BodyOffset = bodyOffset ;
			this.Header.MessageBodyLength = bodyLen ;			
		}

		public byte[] ToStream()
		{	
			if(this.Body == null)
			{
				return this.Header.ToStream() ;
			}

			int    headerLen = this.Header.GetStreamLength() ;
			byte[] result = new byte[headerLen + this.Header.MessageBodyLength] ;
			this.Header.ToStream(result ,0) ;
			for(int i=0 ;i<this.Header.MessageBodyLength ;i++)
			{
				result[headerLen + i] = this.Body[this.BodyOffset + i] ;				
			}

			return result ;
		}
		#endregion

		#region Length
		public int Length
		{
			get
			{
				return this.Header.GetStreamLength() + this.Header.MessageBodyLength ;
			}
		}
		#endregion

	}

	public delegate void CbNetMessage(NetMessage msg) ;

	[Serializable]
	public class RoundedMessage :NetMessage
	{
		public RoundedMessage(){}

		public RoundedMessage(NetMessage netMsg)
		{
			this.Header = netMsg.Header ;
			this.Body   = netMsg.Body ;
			this.Tag    = netMsg.Tag ;

			this.BodyOffset = netMsg.BodyOffset;
		}

		public bool IsFirstMessage = false ;
		public int  ConnectID      = 0     ; //��������Ӧ��Tcp����	
	}
}
