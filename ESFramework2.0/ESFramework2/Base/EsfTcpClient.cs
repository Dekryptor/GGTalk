using System;
using System.Net;
using System.Net.Sockets;

namespace ESFramework
{
	/// <summary>
    /// EsfTcpClient ø…≈‰÷√µƒTcpClient°£
	/// </summary>
	public class EsfTcpClient
	{
		public EsfTcpClient()
		{			
		}

		public EsfTcpClient(string ip ,int thePort)
		{			
			this.serverIP = ip ;
			this.port = thePort ;
		}

		#region ServerIP
		private string serverIP = "" ; 
		public string ServerIP
		{
			get
			{
				return this.serverIP ;
			}
			set
			{
				this.serverIP = value ;
			}
		}
		#endregion		
	
		#region Port
		private int port = 0 ; 
		public int Port
		{
			get
			{
				return this.port ;
			}
			set
			{
				this.port = value ;
			}
		}
		#endregion
		
		public NetworkStream GetNetworkStream()
		{			
			try
			{
				TcpClient client = new TcpClient();
				client.Connect(IPAddress.Parse(this.serverIP), this.port);
				NetworkStream stream = client.GetStream();

				return stream ;
			}
			catch
			{
				return null ;
			}
		}

	}
}
