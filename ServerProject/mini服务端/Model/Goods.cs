using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer.Model
{
    class Goods
    {
        /// <summary>
        /// 物品类
        /// </summary>
        /// <param name="goods_id">物品ID</param>
        /// <param name="goods_name">物品名</param>
        /// <param name="good_max">最大持有数，-1为无上限</param>
        public Goods(int goods_id, string goods_name,int goods_has, int goods_max)
        {
            this.Goods_id = goods_id;
            this.Goods_name = goods_name;
            this.Goods_has = goods_has;
            this.Goods_max = goods_max;
        }

        public int Goods_id { get; set; }
        public string Goods_name { get; set; }
        public int Goods_has { get; set; }
        public int Goods_max { get; set; }

    }
}
