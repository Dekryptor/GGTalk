using System;
using System.Threading ;
using System.Net;
using System.Net.Sockets ;

namespace ESFramework.Server.Tcp
{
	/// <summary>
	/// INetworkStreamSafe �̰߳�ȫ�������� ��	
	/// ���ߣ���ΰ sky.zhuwei@163.com 
	/// 2005.04.22
	/// </summary>	
	public interface ISafeNetworkStream 
	{		
		void Flush();
		void Close() ;

        void Write(byte[] buffer, int offset, int size);
        int  Read (byte[] buffer, int offset, int size);

		bool DataAvailable{get ;} 	
		NetworkStream NetworkStream{get ;}
	}

    #region SafeNetworkStream
    public class SafeNetworkStream : ISafeNetworkStream
    {
        private NetworkStream stream = null;
        private object lockForRead = new object();
        private object lockForWrite = new object();

        public SafeNetworkStream(NetworkStream netStream)
        {
            this.stream = netStream;
        }

        #region ISafeNetworkStream ��Ա

        #region Write ,Read
        public void Write(byte[] buffer, int offset, int size)
        {
            lock (this.lockForWrite)
            {
                this.stream.Write(buffer, offset, size);
            }
        }

        public int Read(byte[] buffer, int offset, int size)
        {
            lock (this.lockForRead)
            {
                return this.stream.Read(buffer, offset, size);
            }
        }

        #endregion

        #region Flush ,Close
        public void Flush()
        {
            this.stream.Flush();
        }

        public void Close()
        {
            this.stream.Close();
        }
        #endregion

        #region property
        public NetworkStream NetworkStream
        {
            get
            {
                return this.stream;
            }
        }

        public bool DataAvailable
        {
            get
            {
                return this.stream.DataAvailable;
            }
        }
        #endregion

        #endregion
    }    
    #endregion
	

    
	
}
