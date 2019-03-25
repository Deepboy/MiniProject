using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using GameServer.miniServer;
using GameServer.Model;

namespace GameServer.Controller
{
    class RoomController:BaseController
    {
        public RoomController()
        {
            requestCode = RequestCode.Room;
        }

        //public string  CreateRoom(string data, Client client, Server server) {
        //    //设置玩家房间
        //    server.CreateRoom(client);
        //    //返回是否成功
        //    return ((int)ReturnCode.Success).ToString();
        //}

        //public string JoinRoom(string data, Client client, Server server)
        //{
        //    int id = int.Parse(data);
        //    Room room = server.GetRoomById(id);
        //    if (room == null)
        //    {
        //        return ((int)ReturnCode.NotFound).ToString();
        //    }
        //    else if (room.IsWaitingJoin() == false)
        //    {
        //        return ((int)ReturnCode.Fail).ToString();
        //    }
        //    else
        //    {
        //        room.AddClient(client);
        //        string roomData = room.GetRoomData();//数据格式：returnCode,roleType-id,username,tc,wc|id,username,tc,wc
        //        room.BroadcastMessage(client, ActionCode.UpdateRoom, roomData);
        //        return ((int)ReturnCode.Success).ToString() + "," + ((int)RoleType.Red).ToString() + "-" + roomData;
        //    }
        //}


        public string StartMatching(string data, Client client, Server server) {
            List<Room> roomList = server.GetRoomList();
            UserInfo userinfo = client.GetUserInfo();
            List<ElementType> choiceCard = new List<ElementType>();
            userinfo.PVP_Info = data;
            string s = data.Replace("[","");
            s = s.Replace("]","");
            string[] strs = s.Split(',');
            client.SetUserInfo(userinfo);
            HeroCard h = new HeroCard(server.GetAllHeroCardDict()[strs[0]]);
            h.Hero_level = int.Parse(strs[1]);
            h.Prossive_skill_1 = new Skill(server.GetSkillDict()[h.Prossive_skill_1_id]);
            h.Prossive_skill_2 = new Skill(server.GetSkillDict()[h.Prossive_skill_2_id]);
            h.Prossive_skill_3 = new Skill(server.GetSkillDict()[h.Prossive_skill_3_id]);
            h.Action_skill = new Skill(server.GetSkillDict()[h.Active_skill_id]);
            h.Staff_skill_other = new Skill(server.GetSkillDict()[server.GetAllHeroCardDict()[strs[2]].Staff_skill_myself_id]);
            for (int i = 3; i <= strs.Length - 1; i++) {
                choiceCard.Add((ElementType)Enum.Parse(typeof(ElementType), strs[i]));
            }
            client.Fight_Hero = new FightHero(h, choiceCard);

            UserInfo userInfo = client.GetUserInfo();
            client.SetUserInfo(userInfo);
            foreach (Room room in roomList) {
                if (room.IsWaitingJoin() == true) {
                    room.AddClient(client);
                    return ((int)ReturnCode.Waiting).ToString();
                }
            }
            server.CreateRoom(client);
            return ((int)ReturnCode.Waiting).ToString();
        }

        public string QuitMatching(string data, Client client, Server server) {
            client.Room.RemoveClient(client);
            return ((int)ReturnCode.Success).ToString();
        }

        public string OutCard(string data, Client client, Server server) {
            Client other_client = null;
            foreach (Client c in client.Room.GetRoomClient())
            {
                if (c != client)
                    other_client = c;
            }
            //bool isUseCard = client.Room.UseElementCard(client, client.Fight_Hero, client.Fight_Hero, data);
            bool isUseCard = client.Room.UseElementCard(client, client.Fight_Hero, other_client.Fight_Hero, data);
            return ((int)ReturnCode.Fail).ToString();
        }

        public string UseSkill(string data, Client client, Server server) {
            Client other_client = null;
            foreach (Client c in client.Room.GetRoomClient())
            {
                if (c != client)
                    other_client = c;
            }
            //bool isUseSkill = client.Room.UsePlayerSkill(client, client.Fight_Hero, client.Fight_Hero, data);
            bool isUseSkill = client.Room.UsePlayerSkill(client, client.Fight_Hero, other_client.Fight_Hero, data);
            if (isUseSkill) {
                return ((int)ReturnCode.Success).ToString();
            }
            else
                return ((int)ReturnCode.Fail).ToString();
        }
        //public void ConfirmStart(string data, Client client, Server server) {
        //    client.IsOK = true;
        //}

        public string EffectOver(string data, Client client, Server server) {
            Client other_client = null;
            foreach (Client c in client.Room.GetRoomClient())
            {
                if (c != client)
                    other_client = c;
            }
            Console.WriteLine("effectOver");
            if (data == "player") {
                client.Room.fightState = FightState.Intend;
            }
            if (data == "enemy") {
                if(!other_client.isConnect)
                    client.Room.fightState = FightState.Intend;
            }
            return ((int)ReturnCode.Success).ToString();
        }
    }
}
