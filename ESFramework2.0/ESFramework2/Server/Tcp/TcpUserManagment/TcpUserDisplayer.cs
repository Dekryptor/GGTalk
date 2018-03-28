using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace ESFramework.Network.Tcp.TcpUserManagment
{
	/// <summary>
	/// TcpUserDisplayer ITcpUserDisplayer�Ĳο�ʵ�֡�
	/// </summary>
	public class TcpUserDisplayer : System.Windows.Forms.UserControl ,ITcpUserDisplayer
	{
		private System.Windows.Forms.ListView listView1;
		private System.Windows.Forms.ColumnHeader columnHeader_userID;
		private System.Windows.Forms.ColumnHeader columnHeader_logTime;
		private System.Windows.Forms.ColumnHeader columnHeader_DownLoad;
		private System.Windows.Forms.ColumnHeader columnHeader_lastServType;

		private IServiceKeyNameMatcher serviceKeyNameMatcher ;


		/// <summary> 
		/// ����������������
		/// </summary>
		private System.ComponentModel.Container components = null;

		public TcpUserDisplayer()
		{
			// �õ����� Windows.Forms ���������������ġ�
			InitializeComponent();

			// TODO: �� InitializeComponent ���ú�����κγ�ʼ��

		}

		public IServiceKeyNameMatcher ServiceKeyNameMatcher
		{
			set
			{
				this.serviceKeyNameMatcher = value ;
			}
		}


		#region Dispose
		/// <summary> 
		/// ������������ʹ�õ���Դ��
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}
		#endregion	

		#region �����������ɵĴ���
		/// <summary> 
		/// �����֧������ķ��� - ��Ҫʹ�ô���༭�� 
		/// �޸Ĵ˷��������ݡ�
		/// </summary>
		private void InitializeComponent()
		{
			this.listView1 = new System.Windows.Forms.ListView();
			this.columnHeader_userID = new System.Windows.Forms.ColumnHeader();
			this.columnHeader_logTime = new System.Windows.Forms.ColumnHeader();
			this.columnHeader_DownLoad = new System.Windows.Forms.ColumnHeader();
			this.columnHeader_lastServType = new System.Windows.Forms.ColumnHeader();
			this.SuspendLayout();
			// 
			// listView1
			// 
			this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
																						this.columnHeader_userID,
																						this.columnHeader_logTime,
																						this.columnHeader_DownLoad,
																						this.columnHeader_lastServType});
			this.listView1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.listView1.Location = new System.Drawing.Point(0, 0);
			this.listView1.Name = "listView1";
			this.listView1.Size = new System.Drawing.Size(584, 264);
			this.listView1.TabIndex = 0;
			this.listView1.View = System.Windows.Forms.View.Details;
			// 
			// columnHeader_userID
			// 
			this.columnHeader_userID.Text = "�û����";
			this.columnHeader_userID.Width = 125;
			// 
			// columnHeader_logTime
			// 
			this.columnHeader_logTime.Text = "��¼ʱ��";
			this.columnHeader_logTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.columnHeader_logTime.Width = 150;
			// 
			// columnHeader_DownLoad
			// 
			this.columnHeader_DownLoad.Text = "����������";
			this.columnHeader_DownLoad.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.columnHeader_DownLoad.Width = 100;
			// 
			// columnHeader_lastServType
			// 
			this.columnHeader_lastServType.Text = "���һ�η�������";
			this.columnHeader_lastServType.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.columnHeader_lastServType.Width = 250;
			// 
			// TcpUserDisplayer
			// 
			this.Controls.Add(this.listView1);
			this.Name = "TcpUserDisplayer";
			this.Size = new System.Drawing.Size(584, 264);
			this.ResumeLayout(false);

		}
		#endregion

		#region ITcpDisplayer ��Ա
		public void SetOrUpdateUserItem(string userID, int justServiceKey, int totalDataLen)
		{
			// TODO:  ��� TcpUserDisplayer.SetOrUpdateUserItem ʵ��
			if(this.InvokeRequired)
			{
				object[] args = {userID, justServiceKey, totalDataLen};
				this.Invoke(new CallBackSetUser(this.SetOrUpdateUserItem),args);
			}
			else
			{
				string serviceName = this.serviceKeyNameMatcher.GetServiceName(justServiceKey) ;
				ListViewItem tmptItem = null;
				foreach(ListViewItem item in this.listView1.Items)
				{
					if(item.SubItems[0].Text.Trim() == userID)
					{
						tmptItem = item;
						break;
					}
				}
				if(tmptItem == null)
				{
					string[] subItems = {userID, DateTime.Now.ToString(), totalDataLen.ToString() ,serviceName};
					ListViewItem newItem = new ListViewItem(subItems);
					this.listView1.Items.Add(newItem);
				}
				else
				{
					int totalBytes = int.Parse(tmptItem.SubItems[2].Text) + totalDataLen;
					tmptItem.SubItems[2].Text = totalBytes.ToString();
					tmptItem.SubItems[3].Text = serviceName;
				}
			}

			
		}

		public void RemoveUser(string userID, DisconnectedCause cause)
		{
			// TODO:  ��� TcpUserDisplayer.RemoveUser ʵ��
			if(this.InvokeRequired)
			{
				object[] args = {userID,cause};
				this.Invoke(new CallBackRemoveUser(this.RemoveUser),args);
			}
			else
			{
				foreach(ListViewItem item in this.listView1.Items)
				{
					if(item.SubItems[0].Text.Trim() == userID)
					{
						this.listView1.Items.Remove(item);
						break;
					}
				}

			}

		}

		public void ClearAll()
		{
			// TODO:  ��� TcpUserDisplayer.ClearAll ʵ��
			if(this.InvokeRequired)
			{
				this.Invoke(new CbSimple(this.ClearAll),null);
			}
			else
			{
				this.listView1.Items.Clear();
			}
		}

		#endregion
	}

	internal delegate void CallBackSetUser(string userID, int justServiceKey, int totalDataLen);
	internal delegate void CallBackRemoveUser(string userID, DisconnectedCause cause);	
}
