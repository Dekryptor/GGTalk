using System;
using System.Net;

namespace ESFramework
{
	/// <summary>
	/// EsfIPEndPoint ø…xml≈‰÷√µƒIPEndPoint°£
	/// </summary>
	public class EsfIPEndPoint
	{
		public EsfIPEndPoint()
		{			
		}

		#region IPAddress
		private string iPAddress = "" ; 
		public string IPAddress
		{
			get
			{
				return this.iPAddress ;
			}
			set
			{
				this.iPAddress = value ;
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

		public IPEndPoint IPEndPoint
		{
			get
			{
				return new IPEndPoint(System.Net.IPAddress.Parse(this.iPAddress) ,this.port) ;
			}
		}

	}
}
