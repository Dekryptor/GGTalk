using System;
using ESFramework.Core;

namespace ESFramework.Server.Tcp
{
	/// <summary>
	/// TcpHookSender ��ժҪ˵����
	/// </summary>
	public class TcpHookSender :ITcpHookSender
	{
		public TcpHookSender()
		{			
		}

		#region ITcpHookSender ��Ա

		public void HookAndSendNetMessage(int connectID ,NetMessage msg)
		{
			if(msg == null)
			{
				return ;
			}

			NetMessage msgToBeSended = this.messageDispatcher.BeforeSendMessage(msg) ;

			this.tcpClientsController.SendData(connectID ,msgToBeSended) ; 
		}	
		

		#region TcpClientsController
		private ITcpClientsController tcpClientsController = null ;
		public  ITcpClientsController TcpClientsController
		{
			set
			{
				this.tcpClientsController = value ;
			}
		}
		#endregion

		#region MessageDispatcher
		private IMessageDispatcher messageDispatcher ;
		public  IMessageDispatcher MessageDispatcher
		{
			set
			{
				this.messageDispatcher = value ;
			}
		}
		#endregion

		#endregion
	}
}
