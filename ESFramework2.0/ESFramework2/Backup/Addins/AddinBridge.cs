using System;
using ESFramework.Passive ;
using ESBasic.Addins;
using ESFramework.Core;

namespace ESFramework.Addins
{
	/// <summary>
	/// AddinBridge ���ڽ�����˲���Ͷ�Ӧ�Ŀͻ��˲���Ž����������е��ԡ�
	/// zhuweisky
	/// </summary>
	public class AddinBridge :IServerAgent
	{
		public AddinBridge()
		{			
		}

		#region Property
		#region FunAddinManagement
		private IAddinManagement funAddinManagement = null ; 
		public  IAddinManagement FunAddinManagement
		{
			set
			{
				this.funAddinManagement = value ;
			}
		}

		
		#endregion	

		#region P2PResponseKeys
		private int[] p2PResponseKeys = null ; 
		public  int[] P2PResponseKeys
		{
			set
			{
				this.p2PResponseKeys = value ;
			}
		}
		#endregion

		#region TestMode
		private TestMode testMode = TestMode.Normal ; 
		public TestMode TestMode
		{
			set
			{
				this.testMode = value ;
			}
		}
		#endregion

		#endregion

		#region IServerAgent ��Ա
		public void Initialize()
		{
		}

		public NetMessage CommitRequest(NetMessage requestMsg, DataPriority dataPriority, bool checkRespond)
		{
			if(this.testMode == ESFramework.Addins.TestMode.FSOffline)
			{
				return null ;
			}

			foreach(IFunAddin funAddin in this.funAddinManagement.AddinList)
			{
				if(funAddin.ServiceKey == requestMsg.Header.ServiceKey)
				{
					//�������ܲ������
					return funAddin.ProcessMessage(requestMsg) ;
				}
			}

			return null ;
		}

		public NetMessage CommitRequest(NetMessage requestMsg, DataPriority dataPriority, int expectResServiceKey)
		{
			return this.CommitRequest(requestMsg ,dataPriority ,true) ;
		}		

		public NetMessage PickupResponse(int orginServiceKey ,int corelationID)
		{
			return null ;
		}

		#endregion
	}

	/// <summary>
	/// ����ģʽ
	/// </summary>
	public enum TestMode
	{
		Normal ,FSOffline
	}
}
