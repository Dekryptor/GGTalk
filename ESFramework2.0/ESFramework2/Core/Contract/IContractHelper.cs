using System;

namespace ESFramework.Core
{
	/// <summary>
	/// IContractHelper 与协议相关的决策。
    /// 2005.10.07
	/// </summary>
	public interface IContractHelper :IStringEncoder
	{
        /// <summary>
        /// MessageHeaderLength 消息头的长度
        /// </summary>
		int MessageHeaderLength{get ;} 

        /// <summary>
        /// ParseMessageHeader 将字节流解析为消息头
        /// </summary>        
		IMessageHeader ParseMessageHeader(byte[] data ,int offset) ; 		
		
        /// <summary>
        /// CreateMessageHeader 客户端产生请求消息头	
        /// </summary>        
        IMessageHeader CreateMessageHeader(string userID ,int serviceKey ,int bodyLen ,string destUserID) ;

        /// <summary>
        /// ServerType 当前服务器的服务器类型（如 cityCode）
        /// </summary>
		int ServerType{get ;}		
		
		bool ValidateMessageToken(IMessageHeader header) ;
		bool IsP2PMessage(int serviceKey) ; 

		/// <summary>
        /// VerifyFirstMessage 验证消息(在CaptureReceivedMsg之后进行)，如果验证失败，将关闭对应的连接
		/// </summary>		
		bool VerifyFirstMessage(NetMessage msg) ;
		bool VerifyOtherMessage(NetMessage msg ) ;		
	}

	#region IStringEncoder
	/// <summary>
	/// StringEncoder 指定字符串编码格式
	/// </summary>
	public interface IStringEncoder
	{
		string GetStrFromStream(byte[] stream ,int offset ,int len) ;
		byte[] GetBytesFromStr(string ss) ;
	}
	#endregion	
}
