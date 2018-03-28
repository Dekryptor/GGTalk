using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ESFramework.Core;

namespace WindowsFormsApp1
{
    class MessageHeader : IMessageHeader
    {
        public int GetStreamLength()
        {
            return 1;
        }

        public byte[] ToStream()
        {
            throw new NotImplementedException();
        }

        public void ToStream(byte[] buff, int offset)
        {
            throw new NotImplementedException();
        }

        public object Clone()
        {
            throw new NotImplementedException();
        }

        public int MessageBodyLength { get; set; }
        public int TypeKey { get; set; }
        public int ServiceKey { get; set; }
        public int ServiceItemIndex { get; set; }
        public int MessageID { get; set; }
        public int DependentMessageID { get; set; }
        public int Result { get; set; }
        public string UserID { get; set; }
        public string DestUserID { get; set; }
        public void OverturnSourceDest()
        {
            throw new NotImplementedException();
        }

        public void ResetMessageID()
        {
            throw new NotImplementedException();
        }
    }
}
