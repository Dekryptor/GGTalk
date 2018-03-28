using System;
using ESFramework.Core;

namespace ESFramework.Server.Tcp
{
	/// <summary>
	/// TcpHookSender 的摘要说明。
	/// </summary>
	public class TcpHookSender :ITcpHookSender
	{
		public TcpHookSender()
		{			
		}

		#region ITcpHookSender 成员

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
