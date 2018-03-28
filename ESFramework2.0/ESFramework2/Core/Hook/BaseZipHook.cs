using System;
using System.Collections ;

namespace ESFramework.Network
{
	/// <summary>
	/// BaseZipHook 用于将截获的消息进行压缩/解压缩。
	/// zhuweisky 2006.03.09
	/// 之所以将压缩Hook引入框架，是因为对网上传输的消息进行压缩和解压缩是常见的需求。
	/// IMessageHeader.ZipMe 字段表明了在不改变ZipHook实现的情况下我们可以对部分消息进行压缩/解压缩，这让我们的应用
	/// 有了更细粒度的控制权。	
	/// </summary>
	public abstract class BaseZipHook :BaseHook
	{
		public BaseZipHook()
		{			
		}

		protected abstract byte[] Zip(byte[] data ,int offset ,int size) ;
		protected abstract byte[] Unzip(byte[] data ,int offset ,int size) ;
	
		#region INetMessageHook 成员

		protected override NetMessage DoTranslateReceived(NetMessage msg)
		{
			if(! msg.Header.ZipMe)
			{
				return msg ;
			}	

			msg.Body = this.Unzip(msg.Body ,msg.BodyOffset ,msg.Header.MessageBodyLength) ;
			msg.BodyOffset = 0 ;
			msg.Header.MessageBodyLength = msg.Body.Length ;

			return msg ;
		}

		protected override NetMessage DoTranslateBeforeSend(NetMessage msg)
		{
			if(! msg.Header.ZipMe)
			{
				return msg ;
			}	

			msg.Body = this.Zip(msg.Body ,msg.BodyOffset ,msg.Header.MessageBodyLength) ;
			msg.BodyOffset = 0 ;
			msg.Header.MessageBodyLength = msg.Body.Length ;

			return msg ;
		}
		#endregion
	}
}
