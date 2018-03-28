using System;
using System.Collections ;

namespace ESFramework.Network
{
	/// <summary>
	/// BaseZipHook ���ڽ��ػ����Ϣ����ѹ��/��ѹ����
	/// zhuweisky 2006.03.09
	/// ֮���Խ�ѹ��Hook�����ܣ�����Ϊ�����ϴ������Ϣ����ѹ���ͽ�ѹ���ǳ���������
	/// IMessageHeader.ZipMe �ֶα������ڲ��ı�ZipHookʵ�ֵ���������ǿ��ԶԲ�����Ϣ����ѹ��/��ѹ�����������ǵ�Ӧ��
	/// ���˸�ϸ���ȵĿ���Ȩ��	
	/// </summary>
	public abstract class BaseZipHook :BaseHook
	{
		public BaseZipHook()
		{			
		}

		protected abstract byte[] Zip(byte[] data ,int offset ,int size) ;
		protected abstract byte[] Unzip(byte[] data ,int offset ,int size) ;
	
		#region INetMessageHook ��Ա

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
