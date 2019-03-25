using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer.Model
{
    class Task
    {
        /// <summary>
        /// 任务类
        /// </summary>
        /// <param name="task_id">任务ID</param>
        /// <param name="task_name">任务名</param>
        /// <param name="task_comment">任务内容</param>
        /// <param name="task_challenge">任务对应的关卡</param>
        /// <param name="task_reward">任务奖励</param>
        /// <param name="task_need">任务所需的进度</param>
        /// <param name="task_rate">玩家完成任务的进度</param>
        /// <param name="task_complete">是否已完成该任务</param>
        public Task(int task_id, string task_name, string task_comment, int task_challenge, string task_reward, int task_need, int task_rate, int task_complete)
        {
            this.Task_id = task_id;
            this.Task_name = task_name;
            this.Task_comment = task_comment;
            this.Task_challenge = task_challenge;
            this.Task_reward = task_reward;
            this.Task_need = task_need;
            this.Task_rate = task_rate;
            this.Task_complete = task_complete;
        }
        public int Task_id { get; set; }
        public string Task_name { get; set; }
        public string Task_comment { get; set; }
        public int Task_challenge { get; set; }
        public string Task_reward { get; set; }
        public int Task_need { get; set; }
        public int Task_rate { get; set; }
        public int Task_complete { get; set; }

    }
}
