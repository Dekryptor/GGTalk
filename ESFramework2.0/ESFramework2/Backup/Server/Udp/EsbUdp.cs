using System;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using ESBasic;
using ESFramework.Core;

namespace ESFramework.Server.Udp
{  
    /// <summary>
    /// IEsbUdp的参考实现
    /// </summary>
    public class EsbUdp : IEsbUdp
    {
        #region Members
        private int port = 10000;
        private UdpClient udpClient;
        private IMessageDispatcher messageDispatcher;
        private IContractHelper contractHelper;

        private volatile bool goToStop = true;
        private volatile bool goToDispose = false;
        private ManualResetEvent stopEvent = new ManualResetEvent(false);
        public event CbServiceCommitted ServiceCommitted; 
        #endregion

        #region Ctor
        public EsbUdp()
        {

        } 
        #endregion

        #region Initialize
        public void Initialize()
        {
            int count = 0;
        label: try
            {
                #region 选择IP
                IPEndPoint localIPE = null;
                if (this.autoOnPublicIPAddress)
                {
                    string publicIP = NetHelper.GetLocalPublicIp();
                    if (publicIP != null)
                    {
                        localIPE = new IPEndPoint(IPAddress.Parse(publicIP), this.port);
                    }
                }

                if (localIPE == null)
                {
                    this.udpClient = new UdpClient(this.port);
                }
                else
                {
                    this.udpClient = new UdpClient(localIPE);
                }
                #endregion
            }
            catch
            {
                if (count < 10)
                {
                    ++this.port;
                    ++count;
                    goto label;
                }
                else
                {
                    throw;
                }
            }

            this.goToDispose = false;
            this.stopEvent.Reset();
            CbSimple cb = new CbSimple(this.Worker);
            cb.BeginInvoke(null, null);
        }
        #endregion

        #region IEsbUdp 成员
        public event CbInvalidMsg InvalidMsgReceived;

        #region property

        #region AutoOnPublicIPAddress
        private bool autoOnPublicIPAddress = false;
        public bool AutoOnPublicIPAddress
        {
            set
            {
                this.autoOnPublicIPAddress = value;
            }
        }
        #endregion

        public IMessageDispatcher MessageDispatcher
        {
            set
            {
                this.messageDispatcher = value;
            }
        }

        public int Port
        {
            get
            {
                return this.port;
            }
            set
            {
                this.port = value;
            }
        }

        public IContractHelper ContractHelper
        {
            set
            {
                this.contractHelper = value;
            }
        }

        private IEsbLogger esbLogger = new EmptyEsbLogger();
        public IEsbLogger EsbLogger
        {
            set
            {
                if (value != null)
                {
                    this.esbLogger = value;
                }
            }
        }
        #endregion

        #region Method
        public void Start()
        {
            this.goToStop = false;
        }

        public void Stop()
        {
            this.goToStop = true;
        }



        #region Worker
        private void Worker()
        {
            while (!this.goToDispose)
            {
                try
                {
                    if (this.goToStop)
                    {
                        System.Threading.Thread.Sleep(500);
                        continue;
                    }

                    IPEndPoint remoteIPE = null;
                    byte[] data = this.udpClient.Receive(ref remoteIPE);
                    if (data.Length < this.contractHelper.MessageHeaderLength)
                    {
                        this.ActivateInvalidMsg(data, remoteIPE);
                        continue;
                    }

                    IMessageHeader header = this.contractHelper.ParseMessageHeader(data, 0);
                    if (((this.contractHelper.MessageHeaderLength + header.MessageBodyLength) > data.Length) || (!this.contractHelper.ValidateMessageToken(header)))
                    {
                        this.ActivateInvalidMsg(data, remoteIPE);
                        continue;
                    }

                    byte[] body = null;
                    if (header.MessageBodyLength > 0)
                    {
                        body = new byte[header.MessageBodyLength];
                        for (int i = 0; i < body.Length; i++)
                        {
                            body[i] = data[this.contractHelper.MessageHeaderLength + i];
                        }
                    }

                    NetMessage msg = new NetMessage(header, body);
                    msg.Tag = remoteIPE;

                    if (this.goToDispose)
                    {
                        break;
                    }

                    CbNetMessage cb = new CbNetMessage(this.MessageDealing);
                    cb.BeginInvoke(msg, null, null);
                }
                catch (Exception ee)
                {
                    this.esbLogger.Log(ee.GetType().ToString(), ee.Message, "ESFramework.Network.Udp.EsbUdp.Worker", ErrorLevel.High);
                }
            }

            this.stopEvent.Set();
        }
        #endregion

        #region MessageDealing
        //异步处理消息
        private void MessageDealing(NetMessage msg)
        {
            try
            {
                NetMessage resMsg = this.messageDispatcher.DispatchMessage(msg);

                if (resMsg != null)
                {
                    byte[] bRes = resMsg.ToStream();
                    this.udpClient.Send(bRes, bRes.Length, (IPEndPoint)msg.Tag);
                    if (this.ServiceCommitted != null)
                    {
                        this.ServiceCommitted(resMsg.Header.UserID, resMsg);
                    }
                }
            }
            catch (Exception ee)
            {
                this.esbLogger.Log(ee.GetType().ToString(), ee.Message, "ESFramework.Network.Udp.EsbUdp.MessageDealing", ErrorLevel.High);
            }
        }

        private void ActivateInvalidMsg(byte[] data, IPEndPoint remoteIPE)
        {
            if (this.InvalidMsgReceived != null)
            {
                this.InvalidMsgReceived(data, remoteIPE);
            }
        }
        #endregion

        #endregion
        #endregion

        #region IUdpHookSender 成员
        public void HookAndSendNetMessage(IPEndPoint clientIpe, NetMessage msg)
        {
            if (msg == null)
            {
                return;
            }

            NetMessage msgToSended = this.messageDispatcher.BeforeSendMessage(msg);
            byte[] stream = msgToSended.ToStream();
            //udp最大发送缓冲区为65536 = 64k
            this.udpClient.Send(stream, stream.Length, clientIpe);
        }

        #endregion

        #region IDisposable 成员

        public void Dispose()
        {
            this.Stop();
            this.goToDispose = true;

            //发送eof消息给自己，使Receive不再阻塞
            byte[] eof = new byte[4];
            this.udpClient.Send(eof, 4, new IPEndPoint(IPAddress.Parse("127.0.0.1"), this.port));

            this.stopEvent.WaitOne();
            this.udpClient.Close();
        }

        #endregion
    }   
}
