using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


    public class Goods
    {/// <summary>
     /// 物品类
     /// </summary>
     /// <param name="goods_id">物品ID</param>
     /// <param name="goods_name">物品名</param>
     /// <param name="good_max">最大持有数，-1为无上限</param>
        public Goods(string data) {
            string[] strs = data.Split('|');
            this.Goods_id = int.Parse(strs[0]);
            this.Goods_name = strs[1];
            this.Goods_describte = strs[2];
            this.Goods_has = int.Parse(strs[3]);
            this.Goods_max = int.Parse(strs[4]);
            this.Goods_ways = strs[5];
            this.Goods_path = strs[6];
        }

        public override string ToString()
        {
            return string.Format("{0}|{1}|{2}|{3}|{4}|{5}|{6}", Goods_id, Goods_name, Goods_describte, Goods_has, Goods_max, Goods_ways, Goods_path);
        }


        public int Goods_id { get; set; }
        public string Goods_name { get; set; }
        public string Goods_describte { get; set; }
        public int Goods_has { get; set; }
        public int Goods_max { get; set; }
        public string Goods_ways { get; set; }
        public string Goods_path { get; set; }


    }

