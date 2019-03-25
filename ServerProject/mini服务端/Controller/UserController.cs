using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using GameServer.miniServer;
using GameServer.DAO;
using GameServer.Model;

namespace GameServer.Controller
{
    class UserController : BaseController
    {
        private UserDAO userDAO = new UserDAO();
        public UserController()
        {
            requestCode = RequestCode.User;
        }

        //注册
        public string Register(string data, Client client, Server server)
        {
            string[] strs = data.Split(',');
            string username = strs[0];
            string password = strs[1];
            bool res = userDAO.HasSameUsername(client.MySQLConn, username);
            if (res)
            {
                return ((int)ReturnCode.Fail).ToString();
            }
            else
            {
                userDAO.AddUser(client.MySQLConn, username, password);
                User user = userDAO.VerifyUser(client.MySQLConn, username, password);
                bool res1 = userDAO.AddUserInfo(client.MySQLConn, user.Id, user.Username);
                UserInfo userInfo = userDAO.GetUserInfoByUserId(client.MySQLConn, user.Id);
                client.SetUserInfo(userInfo);
                return string.Format("{0}|{1}", ((int)ReturnCode.Success).ToString(), userInfo.ToString());
            }
        }
        //登陆
        public string Login(string data, Client client, Server server)
        {
            string[] strs = data.Split(',');
            //model类中的User，固定格式（id，用户名，密码）
            User user = userDAO.VerifyUser(client.MySQLConn, strs[0], strs[1]);
            //model类中胡UserInfo，固定格式（id,user_id,username,user_level,level_limit,goods,card,challenge,task_id）
            if (user == null)
            {
                //Enum.GetName(typeof(ReturnCode), ReturnCode.Fail);
                return ((int)ReturnCode.Fail).ToString()+"#账号密码错误";
            }
            else
            {
                UserInfo userInfo = userDAO.GetUserInfoByUserId(client.MySQLConn, user.Id);
                client.SetUserInfo(userInfo);
                if (server.GetClientList() != null) {
                    foreach (Client c in server.GetClientList())
                    {
                        if (c.GetUserInfo() == null) continue;
                            if (c!=client && c.GetUserInfo().Id == userInfo.Id)
                            return ((int)ReturnCode.Fail).ToString() + "#该账号在其他地方登陆";
                    }
                }

                bool isFighting = false;
                if (server.GetRoomList().Count > 0) {
                    foreach (Room r in server.GetRoomList())
                    {
                        for (int i = r.GetRoomClient().Count - 1; i >= 0; i--)
                        {
                            if (r.GetRoomClient()[i].GetUserInfo().User_id == userInfo.User_id)
                            {
                                isFighting = true;
                                userInfo.PVP_Info = r.GetRoomClient()[i].GetUserInfo().PVP_Info;
                                Client c = r.GetRoomClient()[i];
                                client.Room = r;
                                client.Fight_Hero = r.GetRoomClient()[i].Fight_Hero;
                                r.GetRoomClient()[i].Fight_Hero.User_Client = client;
                                r.GetRoomClient()[i] = client;
                            }
                            Console.WriteLine(r.GetRoomClient()[i].GetUserInfo().User_id);
                        }
                    }
                }
               
                if (isFighting) {
                    Console.WriteLine("战斗中");
                    Client other = null;
                    foreach (Client c in client.Room.GetRoomClient()) {
                        if (c != client)
                            other = c;
                    }
                    //if (other.isConnect == false) {
                    //    client.Room.fightState = FightState.Intend;
                    //}
                    string PVPinfo = string.Format("{0}@{1}@{2}@{3}|{4}@{5}|{6}", client.Fight_Hero.ToString() + "|" + client.Fight_Hero.GetSkillInfo(), client.Fight_Hero.ToString(), client.Fight_Hero.GetCardInfo(), client.Fight_Hero.OrgHp, client.Fight_Hero.OrgHp, client.Fight_Hero.OrgSpeed, client.Fight_Hero.OrgSpeed);
                    //string PVPinfo= string.Format("{0}@{1}@{2}@{3}|{4}@{5}|{6}", client.Fight_Hero.ToString() + "|" + client.Fight_Hero.GetSkillInfo(), other.Fight_Hero.ToString(), client.Fight_Hero.GetCardInfo(), client.Fight_Hero.OrgHp, other.Fight_Hero.OrgHp, client.Fight_Hero.OrgSpeed, other.Fight_Hero.OrgSpeed);
                    return string.Format("{0}#{1}#{2}#{3}", ((int)ReturnCode.Waiting).ToString(), userInfo.ToString(), userInfo.PVP_Info,PVPinfo);
                }
                return string.Format("{0}#{1}", ((int)ReturnCode.Success).ToString(),userInfo.ToString() );

            }
        }

        public string Save(string data, Client client, Server server) {
            Console.WriteLine(data);
            string[] strs = data.Split('|');
            client.GetUserInfo().Id = int.Parse(strs[0]);
            client.GetUserInfo().User_id = int.Parse(strs[1]);
            client.GetUserInfo().Username = strs[2];
            client.GetUserInfo().User_Exp = int.Parse(strs[3]);
            client.GetUserInfo().User_ReserveExp = int.Parse(strs[4]);
            client.GetUserInfo().User_level = int.Parse(strs[5]);
            client.GetUserInfo().Level_limit = int.Parse(strs[6]);
            client.GetUserInfo().Goods = strs[7];
            client.GetUserInfo().Card = strs[8];
            client.GetUserInfo().Challenge = int.Parse(strs[9]);
            client.GetUserInfo().Task_id = int.Parse(strs[10]);
            client.GetUserInfo().User_phase = int.Parse(strs[11]);
            client.GetUserInfo().Minecraft = int.Parse(strs[12]);



            userDAO.UpdateUserInfo(client.MySQLConn, client.GetUserInfo());
            return ((int)ReturnCode.Success).ToString();
        }


        #region 抽卡

        /// <summary>
        /// 概率计算
        /// </summary>
        private void ProbabilityCaculation(int randValue, ref string data, Client client)
        {
            Random random = new Random(Guid.NewGuid().GetHashCode());
            int randomValue = 0;
            int value = 0;
            int newValue = 0;
            if (randValue <= 2)
            {
                if (client.GetUserInfo().Card.Contains("10007") || client.GetUserInfo().Card.Contains("10008") || client.GetUserInfo().Card.Contains("10009"))
                {
                    value = int.Parse(client.GetUserInfo().Goods.Replace("9001,", "@").Split('@')[1].Split(']')[0]);
                    newValue = value + 100;
                    client.GetUserInfo().Goods = client.GetUserInfo().Goods.Replace("9001," + value.ToString(), "9001," + newValue.ToString());
                }
                else
                {
                    client.GetUserInfo().Card = client.GetUserInfo().Card.Replace("}", ";[10007,1,4]}");
                }
                data += "SSR";
            }
            else if (randValue <= 7)
            {
                if (client.GetUserInfo().Card.Contains("10004") || client.GetUserInfo().Card.Contains("10005") || client.GetUserInfo().Card.Contains("10006"))
                {
                    value = int.Parse(client.GetUserInfo().Goods.Replace("9001,", "@").Split('@')[1].Split(']')[0]);
                    newValue = value + 70;
                    client.GetUserInfo().Goods = client.GetUserInfo().Goods.Replace("9001," + value.ToString(), "9001," + newValue.ToString());
                }
                else
                {
                    client.GetUserInfo().Card = client.GetUserInfo().Card.Replace("}", ";[10004,1,4]}");
                }
                data += "SR";
            }
            else if (randValue <= 37)
            {
                if (client.GetUserInfo().Card.Contains("10001") || client.GetUserInfo().Card.Contains("10002") || client.GetUserInfo().Card.Contains("10003"))
                {
                    value = int.Parse(client.GetUserInfo().Goods.Replace("9001,", "@").Split('@')[1].Split(']')[0]);
                    newValue = value + 50;
                    client.GetUserInfo().Goods = client.GetUserInfo().Goods.Replace("9001," + value.ToString(), "9001," + newValue.ToString());
                }
                else
                {
                    client.GetUserInfo().Card = client.GetUserInfo().Card.Replace("}", ";[10001,1,4]}");
                }
                data += "R";
            }
            else if (randValue <= 350)
            {
                randomValue = random.Next(1, 21);
                data += "Diamond:" + randomValue.ToString();
                value = int.Parse(client.GetUserInfo().Goods.Replace("9001,", "@").Split('@')[1].Split(']')[0]);
                newValue = value + randomValue;
                client.GetUserInfo().Goods = client.GetUserInfo().Goods.Replace("9001," + value.ToString(), "9001," + newValue.ToString());
            }
            else
            {
                randomValue = random.Next(1, 21);
                value = int.Parse(client.GetUserInfo().Goods.Replace("9002,", "@").Split('@')[1].Split(']')[0]);
                newValue = value + randomValue * 10;
                client.GetUserInfo().Goods = client.GetUserInfo().Goods.Replace("9002," + value.ToString(), "9002," + newValue.ToString());
                data += "Gold:" + (randomValue * 10).ToString();
            }
        }

        /// <summary>
        /// 抽卡
        /// </summary>
        public string GetHeroCard(string data, Client client, Server server)
        {
            Console.WriteLine("抽卡");
            Console.WriteLine("抽卡数据：" + data);
            string[] strs = data.Split(',');
            string times = strs[0];
            int diamond = int.Parse(strs[1]);
            string clientData = "";
            if (times == "Once")
            {
                if (diamond < 50)
                {
                    return ((int)ReturnCode.Fail).ToString();
                }
                client.GetUserInfo().Goods = client.GetUserInfo().Goods.Replace("9001," + diamond.ToString(), "9001," + (diamond - 50).ToString());
                Random random = new Random(Guid.NewGuid().GetHashCode());
                int randValue = 0;
                randValue = random.Next(1, 1001);
                ProbabilityCaculation(randValue, ref clientData, client);
                Console.WriteLine(clientData);
                userDAO.UpdateUserInfo(client.MySQLConn, client.GetUserInfo());
                return string.Format("{0}@{1}@{2}@{3}", ((int)ReturnCode.Success).ToString(), "Once", clientData, client.GetUserInfo().ToString());
            }
            else if (times == "TenTimes")
            {
                if (diamond < 500)
                {
                    return ((int)ReturnCode.Fail).ToString();
                }
                client.GetUserInfo().Goods = client.GetUserInfo().Goods.Replace("9001," + diamond.ToString(), "9001," + (diamond - 500).ToString());
                for (int i = 0; i < 10; ++i)
                {
                    Random random = new Random(Guid.NewGuid().GetHashCode());
                    int randValue = 0;
                    randValue = random.Next(1, 1001);
                    ProbabilityCaculation(randValue, ref clientData, client);
                    if (i < 9)
                    {
                        clientData += "*";
                    }
                }
            }
            Console.WriteLine(clientData);
            userDAO.UpdateUserInfo(client.MySQLConn, client.GetUserInfo());
            return string.Format("{0}@{1}@{2}@{3}", ((int)ReturnCode.Success).ToString(), "TenTimes", clientData, client.GetUserInfo().ToString());
        }

        #endregion

    }
}
