using System;
using ESFramework.Core;

namespace ESFramework.Passive
{
	/// <summary>
	/// IServerAgent �ͻ����������֮�������ͨ�Ŷ��ɾ���IServerAgent������Ҫת����P2P��Ϣ��
	/// ������ҪĿ���ǣ�
	/// (1)���οͻ���������֮���ͨ��Э�飨Tcp/Udp����ITcpServerAgent��IUdpServerAgent
	/// (2)�ɽ��첽����Ϣ����/�ظ�ת��Ϊͬ���ķ������á�	
	/// </summary>
	public interface IServerAgent
	{
		/// <summary>
		/// �����ʱ��Ȼû�лظ������׳���ʱ�쳣
		/// ���dataPriority != DataPriority.CanBeDiscarded ����checkRespondֻ��Ϊfalse
		/// </summary>     
		NetMessage CommitRequest(NetMessage requestMsg ,DataPriority dataPriority , bool checkRespond);	

		/// <summary>
		/// CommitRequest ��ȷ�ƶ���Ҫ����ServiceKeyΪresKey�Ļظ�
		/// </summary>	
		NetMessage CommitRequest(NetMessage requestMsg ,DataPriority dataPriority , int resKey);	
	
		void Initialize();
	}

	public enum DataPriority
	{
		High ,//��������
		Common ,//����ͨ��Ϣ����������Ϣ
		Low ,//���ļ�����
		CanBeDiscarded //����Ƶ���ݡ���Ƶ����
	}
}
