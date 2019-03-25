using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer.Model
{
    class User
    {
        /// <summary>
        /// 玩家登陆类
        /// </summary>
        /// <param name="id">玩家账号ID</param>
        /// <param name="username">账号</param>
        /// <param name="password">密码</param>
        public User(int id, string username, string password)
        {
            this.Id = id;
            this.Username = username;
            this.Password = password;
        }
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
