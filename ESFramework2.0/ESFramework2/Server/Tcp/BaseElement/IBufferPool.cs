using System;

namespace ESFramework.Server.Tcp
{
	/// <summary>
	/// IBufferPool 的摘要说明。
	/// </summary>
	public interface IBufferPool
	{
		byte[] RentBuffer(int minSize) ;
		void   GivebackBuffer(byte[] buffer) ;
	}

	public class SimpleBufferPool :IBufferPool
	{
		#region IBufferPool 成员

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
