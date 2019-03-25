using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer.Model
{
    class Challenge
    {
        /// <summary>
        /// 关卡类
        /// </summary>
        /// <param name="id">关卡ID</param>
        /// <param name="challenge_name">关卡名字</param>
        /// <param name="challenge_reward">关卡奖励</param>
        /// <param name="challenge_boss_id">关卡boss对应的ID</param>
        public Challenge(int id, string challenge_name, string challenge_reward, int challenge_boss_id)
        {
            this.ID = id;
            this.Challenge_name = challenge_name;
            this.Challenge_reward = challenge_reward;
            this.Challenge_boss_id = challenge_boss_id;
        }
        public int ID { get; set; }
        public string Challenge_name { get; set; }
        public string Challenge_reward { get; set; }
        public int Challenge_boss_id { get; set; }
    }
}
