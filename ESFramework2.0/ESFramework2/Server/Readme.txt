
ESFramework.Network ���ڼ�ϵͳ����е�����ͨ�Ų��֡�
һ.��Ҫԭ��
(1)ͨ���е�������ϢESFramework.Network.NetMessage = "��ϢͷESFramework.Network.IMessageHeader + ����(����Ϊ��)"��
   �����Ӧ��ϵͳ֮���Э������IMessageHeader������Ϣ����Ľ���/Ӧ������Ӧ��ϵͳ�Լ����(�������ǣ���Ӧ��
   ϵͳ�Ŀͻ������)��
(2)��Hook���Ƶ�֧�֣����е���Ϣ����ϵͳ����Ҫ��HookȻ�󱻷��ɣ�������ϵͳ���͵���Ϣ�ڷ���֮ǰҲҪ��Hook���κ�
  ��Ϣ���������⣬�������Ϣ������Hook����Ӧ��ESFramework.Network.INetMessageHook��ʵ���и��账���������ƹ���
  �ܵ�Hook���ơ�

��.��Ҫ�������
1.ESFramework.Network.Tcp ���ڽ��Tcpͨ�ŵĹǼܡ�
  ����ˣ�
  (1)ʵ��IContractHelper�ӿ�
  (2)��EsbRequestDealerFactoryΪ���ģ�ʵ�ָ���������
  (3)�����Ҫ����Ϣ����Hook���ش�����ʵ��INetMessageHook�ӿڣ���������ӵ�EsbNetMessageHook��Hook�б���
  (4)���Ҫʵ���û�����������ESFramework.Network.Tcp.TcpUserManagment������TcpUserManagerBridge��ITcpUserManager
    ���ܵ����������Ž�������
  (5)���Ҫ֧��P2P��Ϣת����������ESFramework.Network.Tcp.P2PMessage��
  (6)���Ҫʵ���û��ĺ��ѹ����ܣ�������ESFramework.Network.Tcp.Friends��
  (7)���Ҫʵ�ֶ�̬Ⱥ�飬������ESFramework.Network.ActiveGroup

  �ͻ��ˣ�ESFramework.Network.Tcp.Passive ���ַ�ʽ��
  (1)��ITcpPassiveΪ���ģ��ֶ�ʵ�ִ����������͸������������ɸ��÷���˵�Hook
  (2)��ITcpServerAgentΪ���ģ��Կͻ��˽��и��߲�ε�֧�֡�ʵ��IPassiveHelper��ISingleMessageDealer
    ���⣬IFileReceiver��IFileTransmitter��ͨ����������תP2P�ļ��ṩ��֧�֡�
  
2.ESFramework.Network.TcpPool��Tcp���ӳؽ���֧�֣�ESFramework.Network.Tcp.TPBasedFunDealerFactoryʵ���˻������ӳصĴ�������

3.ESFramework.Network.Udp ���ڽ��Udpͨ�ŵĹǼ�
  �����:
  (1)��IEsbUdpΪ���ģ�ʵ���Լ��Ĵ���������(��ʹ��EsbRequestDealerFactory)�ʹ�������ʵ��IContractHelper
  (2)���Ҫʵ���û�����������ESFramework.Network.Udp.UdpUserManagment
  (3)���Ҫʵ�ֻ���NAT��P2Pͨ�ţ�������ESFramework.Network.Udp.NAPT
 
  �ͻ��ˣ���IUdpServerAgentΪ���ģ�ʵ��IPassiveHelper
