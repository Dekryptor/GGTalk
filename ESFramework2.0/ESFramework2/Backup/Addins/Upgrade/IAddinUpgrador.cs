using System;
using System.IO ;
using System.Collections ;
using ESBasic.Addins;

namespace ESFramework.Addins.Upgrade
{
	/// <summary>
	/// IAddinUpgrador 插件升级器，解决插件的添加、移除、更新问题。
	/// 只能从Web更新插件
	/// </summary>
	public interface IAddinUpgrador
	{
		IAddinUpgradeHelper  AddinUpgradeHelper{set ;}		
		IAddinManagement     AddinManagement{set ;}		
		string				 LocalAddinDirectory{set ;}

		IAddinUpgradeMsgOutputer AddinUpgradeMsgOutputer{set ;}

		void StartUpgrador() ;
	}

	public interface IAddinUpgradeHelper
	{
		string GetAddinUrl(int addinKey) ;
		AddinVersionInformation[] GetAddinVersionNew() ;
	}

	public interface IAddinUpgradeMsgOutputer
	{
		void PutoutMsg(string msg) ; //用于记录升级成功、异常等信息
	}

    #region AddinUpgrador
    /*
    public class AddinUpgrador : IAddinUpgrador
    {
        private IAddinUpgradeHelper addinUpgradeHelper;
        private IAddinManagement addinManagement;
        private string localAddinDirectory;

        private IAddinUpgradeMsgOutputer addinUpgradeMsgOutputer;

        #region ctor
        public AddinUpgrador()
        {
            this.addinUpgradeMsgOutputer = new EmptyAddinUpgradeMsgOutputer();
        }
        #endregion

        #region property

        public IAddinUpgradeHelper AddinUpgradeHelper
        {
            set
            {
                this.addinUpgradeHelper = value;
            }
        }

        public IAddinManagement AddinManagement
        {
            set
            {
                this.addinManagement = value;
            }
        }

        public string LocalAddinDirectory
        {
            set
            {
                this.localAddinDirectory = value;
            }
        }

        public IAddinUpgradeMsgOutputer AddinUpgradeMsgOutputer
        {
            set
            {
                if (value != null)
                {
                    this.addinUpgradeMsgOutputer = value;
                }
            }
        }

        #endregion

        #region StartUpgrador
        public void StartUpgrador()
        {
            ArrayList list = this.GetUpgradeContents();

            this.addinUpgradeMsgOutputer.PutoutMsg(string.Format("{0} －－ 总共有{1}个插件需要进行更新或升级！", DateTime.Now, list.Count));

            foreach (AddinUpgradeContent content in list)
            {
                try
                {
                    if (content.UpgrageType == AddinUpgradeType.Remove)
                    {
                        this.addinManagement.DynRemoveAddin(content.AddinKey);
                    }
                    else if (content.UpgrageType == AddinUpgradeType.Add)
                    {
                        string addinFilePath = this.localAddinDirectory + "\\" + content.AddinFileName;
                        this.DownLoadAddin(content.AddinKey, addinFilePath);
                        string msg = "";
                        if (this.addinManagement.LoadNewAddin(addinFilePath, out msg))
                        {
                            this.addinUpgradeMsgOutputer.PutoutMsg(string.Format("下载并添加{0}插件成功！", content.AddinFileName));
                        }
                        else
                        {
                            this.addinUpgradeMsgOutputer.PutoutMsg(string.Format("添加{0}插件失败！{1}", content.AddinFileName, msg));

                        }
                    }
                    else if (content.UpgrageType == AddinUpgradeType.Update)//update
                    {
                        string addinFilePath = this.localAddinDirectory + "\\" + content.AddinFileName;
                        this.addinManagement.DynRemoveAddin(content.AddinKey);
                        this.DownLoadAddin(content.AddinKey, addinFilePath);
                        string msg = "";
                        if (this.addinManagement.LoadNewAddin(addinFilePath, out msg))
                        {
                            this.addinUpgradeMsgOutputer.PutoutMsg(string.Format("下载并更新{1}插件成功！", content.AddinFileName));
                        }
                        else
                        {
                            this.addinUpgradeMsgOutputer.PutoutMsg(string.Format("更新{0}插件失败！", content.AddinFileName));
                        }
                    }
                    else
                    {
                    }
                }
                catch (Exception ee)
                {
                    this.addinUpgradeMsgOutputer.PutoutMsg(string.Format("{0}插件升级失败！{1}", content.AddinFileName, ee.Message));
                }
            }
        }
        #endregion

        #region GetUpgradeContents ,DownLoadAddin

        #region GetUpgradeContents
        public ArrayList GetUpgradeContents()
        {
            ArrayList resultList = new ArrayList();

            AddinVersionInformation[] infos = this.addinUpgradeHelper.GetAddinVersionNew();
            if (infos == null)
            {
                return resultList;
            }

            foreach (AddinVersionInformation info in infos)
            {
                AddinUpgradeContent content = new AddinUpgradeContent();
                content.AddinKey = info.AddinKey;
                content.AddinFileName = info.AddinFileName;

                IAddin addin = this.GetDestAddin(info.AddinKey);
                if (addin != null)
                {
                    if (!info.Valid)
                    {
                        content.UpgrageType = AddinUpgradeType.Remove;
                    }
                    else
                    {
                        if (info.NewVersion > addin.Version)
                        {
                            content.UpgrageType = AddinUpgradeType.Update;
                        }
                    }
                }
                else
                {
                    if (info.Valid)
                    {
                        content.UpgrageType = AddinUpgradeType.Add;
                    }
                }

                if (content.UpgrageType != AddinUpgradeType.Keep)
                {
                    resultList.Add(content);
                }
            }

            return resultList;
        }
        #endregion

        #region private
        private IAddin GetDestAddin(int addinKey)
        {
            foreach (IAddin addin in this.addinManagement.AddinList)
            {
                if (addin.ServiceKey == addinKey)
                {
                    return addin;
                }
            }

            return null;
        }
        #endregion

        public void DownLoadAddin(int addinKey, string saveFilePath)
        {
            string url = this.addinUpgradeHelper.GetAddinUrl(addinKey);
            NetHelper.DownLoadFileFromUrl(url, saveFilePath);
        }


        #endregion

    } 
    */
    #endregion

	#region AddinUpgradeContent ,AddinUpgradeType ,AddinVersionInformation
	public class AddinUpgradeContent
	{
		public int AddinKey = -1 ;
		public AddinUpgradeType UpgrageType = AddinUpgradeType.Keep;
		
		public string AddinFileName ;//下载插件后存储于本地的名称
	}

	public enum AddinUpgradeType
	{
		Add ,Remove ,Update ,Keep //keep 表示不需更新
	}

	public class AddinVersionInformation
	{
		public int	 AddinKey ;
		public float NewVersion ;
		public bool  Valid ;
		public string AddinFileName ;
	}
	#endregion

	#region EmptyAddinUpgradeMsgOutputer
	public class EmptyAddinUpgradeMsgOutputer :IAddinUpgradeMsgOutputer
	{
		#region IAddinUpgradeMsgOutputer 成员

		public void PutoutMsg(string msg)
		{
			// TODO:  添加 EmptyAddinUpgradeMsgOutputer.PutoutMsg 实现
		}

		#endregion

	}

	#endregion 

}
