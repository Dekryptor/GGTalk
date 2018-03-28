
ESFramework.Network 用于简化系统编程中的网络通信部分。
一.主要原则：
(1)通信中的所有消息ESFramework.Network.NetMessage = "消息头ESFramework.Network.IMessageHeader + 主体(可以为空)"，
   框架与应用系统之间的协议在于IMessageHeader，而消息主体的解析/应用则由应用系统自己完成(更灵活的是，由应用
   系统的客户端完成)。
(2)对Hook机制的支持，所有的消息进入系统首先要被Hook然后被分派，所有由系统发送的消息在发送之前也要被Hook，任何
  消息都不得例外，如果有消息不参与Hook，则应在ESFramework.Network.INetMessageHook的实现中给予处理，而不是绕过框
  架的Hook机制。

二.主要解决方案
1.ESFramework.Network.Tcp 用于解决Tcp通信的骨架。
  服务端：
  (1)实现IContractHelper接口
  (2)以EsbRequestDealerFactory为中心，实现各个处理器
  (3)如果需要对消息进行Hook拦截处理，则实现INetMessageHook接口，并将其添加到EsbNetMessageHook的Hook列表中
  (4)如果要实现用户管理，则引入ESFramework.Network.Tcp.TcpUserManagment，并用TcpUserManagerBridge将ITcpUserManager
    与框架的其它部分桥接起来。
  (5)如果要支持P2P消息转发，则引入ESFramework.Network.Tcp.P2PMessage。
  (6)如果要实现用户的好友管理功能，则引入ESFramework.Network.Tcp.Friends。
  (7)如果要实现动态群组，则引入ESFramework.Network.ActiveGroup

  客户端：ESFramework.Network.Tcp.Passive 两种方式：
  (1)以ITcpPassive为中心，手动实现处理器工厂和各处理器，并可复用服务端的Hook
  (2)以ITcpServerAgent为中心，对客户端进行更高层次的支持。实现IPassiveHelper、ISingleMessageDealer
    另外，IFileReceiver和IFileTransmitter对通过服务器中转P2P文件提供了支持。
  
2.ESFramework.Network.TcpPool对Tcp连接池进行支持，ESFramework.Network.Tcp.TPBasedFunDealerFactory实现了基于连接池的处理器。

3.ESFramework.Network.Udp 用于解决Udp通信的骨架
  服务端:
  (1)以IEsbUdp为中心，实现自己的处理器工厂(可使用EsbRequestDealerFactory)和处理器，实现IContractHelper
  (2)如果要实现用户管理，则引入ESFramework.Network.Udp.UdpUserManagment
  (3)如果要实现基于NAT的P2P通信，则引入ESFramework.Network.Udp.NAPT
 
  客户端：以IUdpServerAgent为中心，实现IPassiveHelper
