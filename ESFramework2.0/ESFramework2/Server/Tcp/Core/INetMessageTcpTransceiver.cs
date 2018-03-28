using System;

namespace ESFramework.Network.Tcp
{
	/// <summary>
	/// INetMessageTcpTransceiver NetMessage收发器，系统与网络之间的所有消息（NetMessage）的进出口。
	/// INetMessageTransceiver 中可以内含与应用无关的Hook，比如在应用中对NetMessage做Hook时，只能Hook消息的Body，不能
	/// 对整个NetMessage进行Hook，而在INetMessageTransceiver中，可以Hook整个NetMessage，只要C/S统一使用这个收发器即可。
	/// zhuweisky 2006.03.11
	/// </summary>
	public interface INetMessageTcpTransceiver
	{
		RecieveResult RecieveNetMessage(ISafeNetworkStream stream, byte[] revBuff ,out NetMessage msg);
		void          SendNetMessage(ISafeNetworkStream stream ,NetMessage msg) ;

		int				MaxMessageSize{set ;}
		IContractHelper ContractHelper{set ;}
		IBufferPool     BufferPool{set ;}
	}

	public enum RecieveResult
	{
		Succeed ,MessageTokenInvalid ,MessageSizeOverflow
	}

	public class SimpleNetMessageTcpTransceiver :INetMessageTcpTransceiver
	{
		#region INetMessageTcpTransceiver 成员

		public RecieveResult RecieveNetMessage(ISafeNetworkStream stream, byte[] revBuff ,out NetMessage msg)
		{		
			msg = null;

			int headerLen = this.contractHelper.MessageHeaderLength ;

			NetHelper.RecieveData(stream ,revBuff ,0 ,headerLen) ;
			IMessageHeader header = this.contractHelper.ParseMessageHeader(revBuff ,0) ;	
			if(! this.contractHelper.ValidateMessageToken(header))
			{				
				return RecieveResult.MessageTokenInvalid ;
			}				

			if((headerLen + header.MessageBodyLength) > this.maxMessageSize)
			{
				return RecieveResult.MessageSizeOverflow ;
			}
				
			msg = new NetMessage();
			msg.Header = header;

			if(header.MessageBodyLength >0 )
			{
				if((header.MessageBodyLength + headerLen) <= revBuff.Length)
				{
					NetHelper.RecieveData(stream ,revBuff ,0 ,header.MessageBodyLength) ;
					msg.Body = revBuff ;							
				}
				else
				{						
					byte[] rentBuff = this.bufferPool.RentBuffer(header.MessageBodyLength) ;						

					NetHelper.RecieveData(stream ,rentBuff ,0 ,header.MessageBodyLength) ;
					msg.Body = rentBuff ;							
				}
			}

			return RecieveResult.Succeed;
		}

		public void SendNetMessage(ISafeNetworkStream stream, NetMessage msg)
		{
			byte[] bMsg = msg.ToStream() ;
			stream.Write(bMsg ,0 ,bMsg.Length) ;
		}

		#region property
		#region ContractHelper
		private IContractHelper contractHelper = null ; 
		public IContractHelper ContractHelper
		{
			set
			{
				this.contractHelper = value ;
			}
		}
		#endregion
		
		#region MaxMessageSize
		private int maxMessageSize = 0 ; 
		public int MaxMessageSize
		{
			set
			{
				this.maxMessageSize = value ;
			}
		}
		#endregion

		#region BufferPool
		private IBufferPool bufferPool = null ; 
		public  IBufferPool BufferPool
		{
			set
			{
				this.bufferPool = value ;
			}
		}
		#endregion
		

		#endregion	

		#endregion

	}

}
