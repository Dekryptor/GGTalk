﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ESFramework.Core;

namespace WindowsFormsApp1
{
    class Class1 : IContractHelper
    {
        IMessageHeader mMessageHeader = new MessageHeader();

        public int MessageHeaderLength
        {
            get { return 20; }
        }

        public int ServerType
        {
            get { return 0; }
        }

        public IMessageHeader CreateMessageHeader(string userID, int serviceKey, int bodyLen, string destUserID)
        {
            return mMessageHeader;
        }

        public byte[] GetBytesFromStr(string ss)
        {
            throw new NotImplementedException();
        }

        public string GetStrFromStream(byte[] stream, int offset, int len)
        {
            throw new NotImplementedException();
        }

        public bool IsP2PMessage(int serviceKey)
        {
            throw new NotImplementedException();
        }

        public IMessageHeader ParseMessageHeader(byte[] data, int offset)
        {
            throw new NotImplementedException();
        }

        public bool ValidateMessageToken(IMessageHeader header)
        {
            throw new NotImplementedException();
        }

        public bool VerifyFirstMessage(NetMessage msg)
        {
            throw new NotImplementedException();
        }

        public bool VerifyOtherMessage(NetMessage msg)
        {
            throw new NotImplementedException();
        }
    }
}
