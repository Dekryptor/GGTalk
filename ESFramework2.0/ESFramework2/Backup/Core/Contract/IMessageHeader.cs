using System;

namespace ESFramework.Core
{
	/// <summary>
	/// IMessageHeader ��Ϣͷ���涨����Ϣͷ�����ٰ�������Ϣ��
	/// ��ΰ 2005.12.07 
	/// </summary>
    public interface IMessageHeader : IContract, ICloneable
    {
        /// <summary>
        /// MessageBodyLength ����Ϣ����ĳ���
        /// </summary>
        int MessageBodyLength { get;set;}

        /// <summary>
        /// TypeKey ����ķ���Ŀ¼����
        /// </summary>
        int TypeKey { get;set;}

        /// <summary>
        /// ServiceKey ��־��ͬ���͵ķ�����Ϣ���Ĺؼ���
        /// </summary>
        int ServiceKey { get;set;}

        /// <summary>
        /// ServiceItemIndex ������Ϣ����ϸ������
        /// </summary>
        int ServiceItemIndex { get;set;}

        /// <summary>
        /// MessageID ÿ����Ϣʵ����Ψһ��־��Ҳ�����ڽ���������Ļظ�������һһ��Ӧ����			
        /// </summary>
        int MessageID { get;set;}

        /// <summary>
        /// DependentMessageID ����Ϣ����������Ϣ��ţ�����˳��������ͨ�������ֳ�����
        /// (1)���ͷ����뽫���ΪDependentMessageID����Ϣ���ͳɹ��󣬲��ܷ��ͱ���Ϣ������
        /// (2)���շ����봦������ΪDependentMessageID����Ϣ�󣬲��ܴ�����Ϣ
        /// </summary>
        int DependentMessageID { get;set;}

        /// <summary>
        /// Result ��������1��ʾ�ɹ�������ֵ��Ӧ��ʧ��ԭ���û����Զ���
        /// </summary>
        int Result { get;set;}

        /// <summary>
        /// UserID ��������Ϣ���û����	
        /// </summary>
        string UserID { get;set;}

        /// <summary>
        /// DestUserID ������Ϣ��Ŀ���û����		
        /// </summary>
        string DestUserID { get;set;}      

        /// <summary>
        /// OverturnSourceDest ������Ϣͷ�еĽ����ߺͷ�����
        /// </summary>
        void OverturnSourceDest();

        /// <summary>
        /// ResetMessageID Ϊ����Ϣ��������һ���µ�MessageID
        /// </summary>
        void ResetMessageID();

        /// <summary>
        /// ZipMe ���ƶ��ڱ�����Ϣ�Ƿ�����ѹ��/��ѹ���������Щ��Ϣ�Ƚ϶�С����IMessageHeader.ZipMe��Ϊfalse��
        /// ����header.ZipMe = (msgLen > 1000) ;		
        /// </summary>
        //bool ZipMe { get;set;}
    }

	#region ServiceResultType
	/// <summary>
	/// ServiceResultType ����ʧ�����͡�ע�⣺��ܷ��صķ���ʧ�ܵ���Ϣ��û�������
	/// 1000��2000 Ϊϵͳ����������
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
					msg = "����ɹ���" ;
					break ;
				}
				case ServiceResultType.InvalidMessge :
				{
					msg = "��Ч��������Ϣ��" ;
					break ;
				}
				case ServiceResultType.DestUserOffLine :
				{
					msg = "Ŀ���û������ߣ�" ;
					break ;
				}
				case ServiceResultType.DestUserIsNotExist :
				{
					msg = "Ŀ���û������ڣ�" ;
					break ;
				}
				case ServiceResultType.ServiceIsNotExist :
				{
					msg = "û���ҵ���Ӧ�ķ��񣬿����Ƿ����Ѿ���ж�أ�" ;
					break ;
				}
				case ServiceResultType.ParseFailure :
				{
					msg = "�޷�����������Ϣ����������Ϣ�ĸ�ʽ����ȷ���޷���ȷƥ�䣡" ;
					break ;
				}
				case ServiceResultType.ServiceStopped :
				{
					msg = "������ķ�������ͣ��" ;
					break ;
				}
				case ServiceResultType.HandleFailure :
				{
					msg = "�ڴ�������ʱ�����������" ;
					break ;
				}
				case ServiceResultType.ServerIsBusy :
				{
					msg = "������æ�����Ժ����ԣ�" ;
					break ;
				}				
				case ServiceResultType.FailureByOtherCause :
				{
					msg = "����ʧ�ܣ�" ;
					break ;
				}
				case ServiceResultType.DbAccessFailure :
				{
					msg = "���ݿ����ʧ�ܣ�" ;
					break ;
				}
				default:
				{
					msg = "�����޷�����������ԭ������" ;
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
