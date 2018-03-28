using System;

namespace ESFramework.Server.Tcp
{
	/// <summary>
	/// IBufferPool ��ժҪ˵����
	/// </summary>
	public interface IBufferPool
	{
		byte[] RentBuffer(int minSize) ;
		void   GivebackBuffer(byte[] buffer) ;
	}

	public class SimpleBufferPool :IBufferPool
	{
		#region IBufferPool ��Ա

		public byte[] RentBuffer(int minSize)
		{
			return new byte[minSize] ;
		}

		public void GivebackBuffer(byte[] buffer)
		{			
		}
		#endregion
	}
}
