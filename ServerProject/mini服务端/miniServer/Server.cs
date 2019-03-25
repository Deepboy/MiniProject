using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using Common;
using GameServer.Controller;
using GameServer.Model;
using GameServer.Buff;
using GameServer.Tool;
using System.Threading;

namespace GameServer.miniServer
{
    public class Server
    {
        private IPEndPoint ipEndPoint;
        private Socket serverSocket;
        private List<Client> clientList = new List<Client>();
        private List<Room> roomList = new List<Room>();
        private ControllerManager controllerManager;
        private Dictionary<string, CardEffect> cardEffectDict = new Dictionary<string, CardEffect>();
        private Dictionary<BuffCode, BaseBuff> buffInfoDict = new Dictionary<BuffCode, BaseBuff>();
        private Dictionary<string, HeroCard> allHeroCardDict = new Dictionary<string, HeroCard>();
        private Dictionary<string, Skill> skillDict = new Dictionary<string, Skill>();

        public Server() { }
        public Server(string ipStr, int port)
        {
            controllerManager = new ControllerManager(this);
            SetIpAndPort(ipStr, port);
        }

        public void SetIpAndPort(string ipStr, int port)
        {
            ipEndPoint = new IPEndPoint(IPAddress.Parse(ipStr), port);
        }

        public void Start()
        {
            serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            serverSocket.Bind(ipEndPoint);
            serverSocket.Listen(0);
            cardEffectDict = ConnHelper.GetCardEffectDict();
            buffInfoDict = ConnHelper.GetBuffInfoDict();
            allHeroCardDict = ConnHelper.GetAllHeroCardDict();
            skillDict = ConnHelper.GetSkillInfoDict();
            serverSocket.BeginAccept(AcceptCallBack, null);

        }

        private void AcceptCallBack(IAsyncResult ar)
        {
            Console.WriteLine("链接到一个客户端");
            Socket clientSocket = serverSocket.EndAccept(ar);
            Client client = new Client(clientSocket, this);
            new Thread(client.Start).Start();
            lock (clientList)
            {
                clientList.Add(client);
            }
            serverSocket.BeginAccept(AcceptCallBack, null);
        }

        public void HandleRequest(RequestCode requestCode, ActionCode actionCode, string data, Client client)
        {
            Console.WriteLine(((int)requestCode).ToString()+';'+((int)actionCode).ToString()+','+data);
            //将各个客户端解析出来的数据通过server传给contorlerManager
            controllerManager.HandleRequest(requestCode, actionCode, data, client);
        }

        public void SendResquest(Client client, ActionCode actionCode, string data)
        {
            //客户端响应
            client.Send(actionCode, data);
        }

        public void SendResponse(Client client, ActionCode actionCode, string data)
        {
            client.Send(actionCode, data);
        }

        public void RemoveClient(Client client)
        {
            lock (clientList)
            {
                clientList.Remove(client);
            }
        }

        public void CreateRoom(Client client) {
            Room room = new Room(this);
            room.AddClient(client);
            lock (clientList)
            {
                roomList.Add(room);
            }
        }

        public void RemoveRoom(Room room)
        {
            if (roomList != null && room != null)
            {
                roomList.Remove(room);
            }
        }

        public List<Room> GetRoomList()
        {
            return roomList;
        }

        public Dictionary<string, CardEffect> GetCardEffectDict() {
            if (cardEffectDict == null)
                cardEffectDict = ConnHelper.GetCardEffectDict();
            return cardEffectDict;
        }
        public Dictionary<BuffCode, BaseBuff> GetBuffInfoDict()
        {
            if (buffInfoDict == null)
                buffInfoDict = ConnHelper.GetBuffInfoDict();
            return buffInfoDict;
        }
        public Dictionary<string, HeroCard> GetAllHeroCardDict()
        {
            if (allHeroCardDict == null)
                allHeroCardDict = ConnHelper.GetAllHeroCardDict();
            return allHeroCardDict;
        }

        public Dictionary<string, Skill> GetSkillDict()
        {
            if (skillDict == null)
                skillDict = ConnHelper.GetSkillInfoDict();
            return skillDict;
        }
        public List<Client> GetClientList() {
            return clientList;
        }
    }

}

