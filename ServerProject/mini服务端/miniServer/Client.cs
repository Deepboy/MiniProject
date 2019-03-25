using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using Common;
using MySql.Data.MySqlClient;
using GameServer.Tool;
using GameServer.Model;
using GameServer.Buff;
namespace GameServer.miniServer
{
    public class Client
    {
        //客户端所持有的对象
        public Socket clientSocket;
        public Server server;
        public Message msg = new Message();
        private MySqlConnection mysqlConn;
        private Room room;

        //玩家所持有的信息
        private UserInfo userInfo;
        private FightHero fight_Hero;

        public bool isConnect = true;
        public Client() { }
        public Client(Socket clientSocket, Server server)
        {
            this.clientSocket = clientSocket;
            this.server = server;
            this.mysqlConn = ConnHelper.Connect();
        }

        public Room Room
        {
            set { room = value; }
            get { return room; }
        }
        public FightHero Fight_Hero
        {
            set { fight_Hero = value;fight_Hero.User_Client = this; }
            get { return fight_Hero; }
        }

        public Dictionary<string,CardEffect> CardEffectDict
        {
            private set {}
            get { return server.GetCardEffectDict(); }
        }

        public Dictionary<BuffCode,BaseBuff> BaseInfoDict
        {
            private set { }
            get { return server.GetBuffInfoDict(); }
        }

        public Dictionary<string, HeroCard> HeroCardDict
        {
            private set { }
            get { return server.GetAllHeroCardDict(); }
        }
        public Dictionary<string, Skill> SkillDict
        {
            private set { }
            get { return server.GetSkillDict(); }
        }


        public void Start()
        {
            if (clientSocket.Poll(-1, SelectMode.SelectRead))
            {
                byte[] data = new byte[1];
                int nRead = clientSocket.Receive(data, SocketFlags.Peek); //这里获取的时候 改一下就可以了
                if (nRead == 0)
                {
                    //socket连接已断开
                    isConnect = false;
                    Console.WriteLine("断开一个客户端连接");
                    Close();
                    return;
                }
            }
            if (clientSocket == null || clientSocket.Connected == false)
            {
                isConnect = false;
                Close();
                Console.WriteLine("断开一个客户端连接");
                return;
            }
            clientSocket.BeginReceive(msg.Data, msg.StartIndex, msg.RemainSize, SocketFlags.None, ReceiveCallBack, null);
        }

        private void ReceiveCallBack(IAsyncResult ar)
        {
            try
            {
                if (clientSocket == null) {
                    Console.WriteLine("clientSocket为空");
                }
                if (clientSocket.Connected == false) {
                    Console.WriteLine("clientSocket链接关闭");
                }
                if (clientSocket == null || clientSocket.Connected == false) { return; }
                int count = clientSocket.EndReceive(ar);
                if (count == 0)
                {
                    Close();
                }
                msg.ReadMessage(count, OnProcessMessage);
                Start();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Close();
            }
        }

        private void OnProcessMessage(RequestCode requestCode, ActionCode actionCode, string data)
        {
            server.HandleRequest(requestCode, actionCode, data, this);
        }

        public MySqlConnection MySQLConn
        {
            get { return mysqlConn; }
        }

        public void Send(ActionCode actionCode, string data)
        {
            try
            {
                if (!this.isConnect) return;
                byte[] bytes = Message.PackData(actionCode, data);
                clientSocket.Send(bytes);
                //Console.WriteLine("发送消息：" + data);
            }
            catch (Exception e)
            {
                isConnect = false;
                Console.WriteLine("无法发送消息:" + e);
            }
        }

        public void Close()
        {
            ConnHelper.CloseConnection(mysqlConn);
            if (clientSocket.Connected == false)
            {
                clientSocket.Close();
            }
            server.RemoveClient(this);
        }

        public void SetUserInfo(UserInfo userInfo) {
            this.userInfo = userInfo;
        }

        public UserInfo GetUserInfo() {
            return userInfo;
        }


    }
}
