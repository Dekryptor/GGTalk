using System;

namespace ESFramework.Core
{
	/// <summary>
	/// IMessageHeader 消息头，规定了消息头中至少包含的信息。
	/// 朱伟 2005.12.07 
	/// </summary>
    public interface IMessageHeader : IContract, ICloneable
    {
        /// <summary>
        /// MessageBodyLength 本消息主体的长度
        /// </summary>
        int MessageBodyLength { get;set;}

        /// <summary>
        /// TypeKey 请求的服务目录类型
        /// </summary>
        int TypeKey { get;set;}

        /// <summary>
        /// ServiceKey 标志不同类型的服务（消息）的关键字
        /// </summary>
        int ServiceKey { get;set;}

        /// <summary>
        /// ServiceItemIndex 服务（消息）的细分索引
        /// </summary>
        int ServiceItemIndex { get;set;}

        /// <summary>
        /// MessageID 每个消息实例的唯一标志，也可用于将功能请求的回复与请求一一对应起来			
        /// </summary>
        int MessageID { get;set;}

        /// <summary>
        /// DependentMessageID 本消息所依赖的消息编号，比如顺序依赖。通常有两种场景：
        /// (1)发送方必须将编号为DependentMessageID的消息发送成功后，才能发送本消息。或者
        /// (2)接收方必须处理完编号为DependentMessageID的消息后，才能处理本消息
        /// </summary>
        int DependentMessageID { get;set;}

        /// <summary>
        /// Result 服务结果，1表示成功。其它值对应的失败原因，用户可自定义
        /// </summary>
        int Result { get;set;}

        /// <summary>
        /// UserID 发出本消息的用户编号	
        /// </summary>
        string UserID { get;set;}

        /// <summary>
        /// DestUserID 接收消息的目标用户编号		
        /// </summary>
        string DestUserID { get;set;}      

        /// <summary>
        /// OverturnSourceDest 交换消息头中的接受者和发送者
        /// </summary>
        void OverturnSourceDest();

        /// <summary>
        /// ResetMessageID 为本消息重新设置一个新的MessageID
        /// </summary>
        void ResetMessageID();

        /// <summary>
        /// ZipMe 控制对于本条消息是否启用压缩/解压缩，如果有些消息比较短小，则将IMessageHeader.ZipMe设为false。
        /// 可以header.ZipMe = (msgLen > 1000) ;		
        /// </summary>
        //bool ZipMe { get;set;}
    }

	#region ServiceResultType
	/// <summary>
	/// ServiceResultType 服务失败类型。注意：框架返回的服务失败的消息是没有主体的
	/// 1000－2000 为系统定义结果类型
	/// </summary>
	public class ServiceResultType
	{
		public const int ServiceSucceed      = 1 ;	
		public const int FailureByOtherCause = 1100 ;

		/*
		public const int InvalidMessge      = 1001 ;

		public const int ParseFailure       = 1021 ;
		public const int HandleFailure      = 1022 ;
		public const int ServiceStopped     = 1023 ;
		public const int ServiceIsNotExist  = 1024 ;
		public const int ServerIsBusy       = 1025 ;

		//P2p
		public const int DestUserOffLine    = 1031 ;
		public const int DestUserIsNotExist = 1032 ;		

		public const int DbAccessFailure     = 1091 ;		
		
		

		#region MatchServiceResultType		
		public static string MatchServiceResultType(int serviceResultType) 
		{
			string msg = null ;
			switch(serviceResultType)
			{
				case ServiceResultType.ServiceSucceed :
				{
					msg = "服务成功！" ;
					break ;
				}
				case ServiceResultType.InvalidMessge :
				{
					msg = "无效的请求消息！" ;
					break ;
				}
				case ServiceResultType.DestUserOffLine :
				{
					msg = "目标用户不在线！" ;
					break ;
				}
				case ServiceResultType.DestUserIsNotExist :
				{
					msg = "目标用户不存在！" ;
					break ;
				}
				case ServiceResultType.ServiceIsNotExist :
				{
					msg = "没有找到对应的服务，可能是服务已经被卸载！" ;
					break ;
				}
				case ServiceResultType.ParseFailure :
				{
					msg = "无法解析请求消息，可能是消息的格式不正确或无法正确匹配！" ;
					break ;
				}
				case ServiceResultType.ServiceStopped :
				{
					msg = "所请求的服务已暂停！" ;
					break ;
				}
				case ServiceResultType.HandleFailure :
				{
					msg = "在处理请求时发生意外错误！" ;
					break ;
				}
				case ServiceResultType.ServerIsBusy :
				{
					msg = "服务器忙，请稍后再试！" ;
					break ;
				}				
				case ServiceResultType.FailureByOtherCause :
				{
					msg = "服务失败！" ;
					break ;
				}
				case ServiceResultType.DbAccessFailure :
				{
					msg = "数据库访问失败！" ;
					break ;
				}
				default:
				{
					msg = "请求无法被处理，错误原因不明！" ;
					break ;
				}
			}

			return msg ;
		}
		
		#endregion	
		*/	
	}
	#endregion
}
