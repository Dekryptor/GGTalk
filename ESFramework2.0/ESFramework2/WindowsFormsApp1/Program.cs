using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Windows.Forms;
using ESFramework;
using ESFramework.Core;
using ESFramework.Passive;
using ESFramework.Server.Tcp;

namespace WindowsFormsApp1
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {

            AsynTcp tcp = new AsynTcp();
            ContractHelper contract = new ContractHelper();
            tcp.ContractHelper = contract;

            TcpStreamDispatcher dispatcher = new TcpStreamDispatcher();
            dispatcher.ContractHelper = contract;

            MessageDispatcher messageDispatcher = new MessageDispatcher();

            NakeDispatcher nake = new NakeDispatcher();
            nake.ContractHelper = contract;

            SimplePassiveProcesser simplePassiveProcesser = new SimplePassiveProcesser();

            ProcesserFactory processerFactory = new ProcesserFactory();
            processerFactory.ContractHelper = contract;
            processerFactory.ForeignProcesser = new SelectiveProcesser();

            simplePassiveProcesser.ProcesserFactory = processerFactory;
            simplePassiveProcesser.AllDataDealer = new SelectiveProcesser();
            ResponseManager responseManager = new ResponseManager();
            responseManager.Initialize();
            simplePassiveProcesser.ResponseManager = responseManager;

            nake.ProcesserFactory = new SingleProcesserFactory(simplePassiveProcesser);
            //nake.ProcesserFactory = new SingleProcesserFactory(new SelectiveProcesser());

            messageDispatcher.NakeDispatcher = nake;

            dispatcher.MessageDispatcher = messageDispatcher;

            tcp.Dispatcher = dispatcher;
            tcp.Initialize();
            tcp.Start();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());

        }
    }
}
