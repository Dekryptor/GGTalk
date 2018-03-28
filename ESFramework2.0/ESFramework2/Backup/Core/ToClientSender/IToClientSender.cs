using System;

namespace ESFramework.Core
{
	/// <summary>
    /// IToClientSender �����ݣ�һ�����������󣭣�header��body��ת����Ŀ���û���������Ŀ���û���λ��
	/// Ŀ���û����������������������ڵ���	
	/// ʵ�֣�ESFramework.Server.Udp.UserManagment.ToLocalClientSender
    ///        ESFramework.Server.Tcp.UserManagment.ToLocalClientSender
	///        ESFramework.Architecture.FourTier.ToForeignClientSender
	///        ESFramework.Network.ToClientSender
    /// ����ͨ��ESFramework.Core.ContainerStyleToClientSender��������װ��Sender��
	/// </summary>
	public interface IToClientSender
	{
		int HookAndSendMessage(string userID ,NetMessage msg) ;	//����DataSendResult�ĳ���

		event CbHookAndSendMessage OverdueMessageOccured ; //���Ŀ���û������ߣ����������¼�

        IHookSender HookSender { get; }         
	}

	public delegate void CbHookAndSendMessage(string userID ,NetMessage msg) ;

	//DataSendResult ������Ӧ������չ
	public class DataSendResult
	{
		public const int Succeed		  = 1 ;
		public const int FailByOtherCause = 0 ;
		public const int UserIsOffLine    = 2 ;		
	}
}
