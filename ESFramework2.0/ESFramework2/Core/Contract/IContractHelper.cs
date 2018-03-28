using System;

namespace ESFramework.Core
{
	/// <summary>
	/// IContractHelper ��Э����صľ��ߡ�
    /// 2005.10.07
	/// </summary>
	public interface IContractHelper :IStringEncoder
	{
        /// <summary>
        /// MessageHeaderLength ��Ϣͷ�ĳ���
        /// </summary>
		int MessageHeaderLength{get ;} 

        /// <summary>
        /// ParseMessageHeader ���ֽ�������Ϊ��Ϣͷ
        /// </summary>        
		IMessageHeader ParseMessageHeader(byte[] data ,int offset) ; 		
		
        /// <summary>
        /// CreateMessageHeader �ͻ��˲���������Ϣͷ	
        /// </summary>        
        IMessageHeader CreateMessageHeader(string userID ,int serviceKey ,int bodyLen ,string destUserID) ;

        /// <summary>
        /// ServerType ��ǰ�������ķ��������ͣ��� cityCode��
        /// </summary>
		int ServerType{get ;}		
		
		bool ValidateMessageToken(IMessageHeader header) ;
		bool IsP2PMessage(int serviceKey) ; 

		/// <summary>
        /// VerifyFirstMessage ��֤��Ϣ(��CaptureReceivedMsg֮�����)�������֤ʧ�ܣ����رն�Ӧ������
		/// </summary>		
		bool VerifyFirstMessage(NetMessage msg) ;
		bool VerifyOtherMessage(NetMessage msg ) ;		
	}

	#region IStringEncoder
	/// <summary>
	/// StringEncoder ָ���ַ��������ʽ
	/// </summary>
	public interface IStringEncoder
	{
		string GetStrFromStream(byte[] stream ,int offset ,int len) ;
		byte[] GetBytesFromStr(string ss) ;
	}
	#endregion	
}
