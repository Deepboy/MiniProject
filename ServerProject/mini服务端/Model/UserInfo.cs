using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer.Model
{
    public class UserInfo
    {
        /// <summary>
        /// 玩家角色信息类
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="user_id">对应账号ID</param>
        /// <param name="username">玩家名</param>
        /// <param name="user_level">玩家等级</param>
        /// <param name="level_limit">最高等级限制（包括卡牌）</param>
        /// <param name="goods">对应物品持有数集合</param>
        /// <param name="card">角色所持有的卡牌集合</param>
        /// <param name="challenge">目前开启的关卡ID</param>
        /// <param name="task_id">目前任务ID</param>
        public UserInfo(int id, int user_id, string username, int user_Exp, int user_ReserveExp, int user_level,int level_limit, string goods,string card, int challenge, int task_id,int user_phase,int minecraft,string pvp_info)

        {
            this.Id = id;
            this.User_id = user_id;
            this.Username = username;
            this.User_Exp = user_Exp;
            this.User_ReserveExp = user_ReserveExp;
            this.User_level = user_level;
            this.Level_limit = level_limit;
            this.Goods = goods;
            this.Card = card;
            this.Challenge = challenge;
            this.Task_id = task_id;
            this.Minecraft = minecraft;
            this.User_phase = user_phase;
            this.Minecraft = minecraft;
            this.PVP_Info = pvp_info;
        }

        public override string ToString()
        {
            return string.Format("{0}|{1}|{2}|{3}|{4}|{5}|{6}|{7}|{8}|{9}|{10}|{11}|{12}|{13}", Id, User_id, Username,User_Exp,User_ReserveExp, User_level, Level_limit, Goods, Card, Challenge, Task_id,User_phase,Minecraft,PVP_Info);

        }

        public int Id { get; set; }
        public int User_id { get; set; }
        public string Username { get; set; }
        public int User_Exp { get; set; }
        public int User_ReserveExp { get; set; }
        public int User_level { get; set; }
        public int Level_limit { get; set; }
        public string Goods { get; set; }
        public string Card { get; set; }
        public int Challenge { get; set; }
        public int Task_id { get; set; }
        public int User_phase { get; set; }
        public int Minecraft { get; set; }
        public string PVP_Info { get; set; }//{[主战ID，等级，助战ID];[元素卡ElementType]}

    }
}
