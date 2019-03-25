using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using GameServer.Buff;

namespace GameServer.Model
{
    public class CardGroup
    {
        public CardGroup(string cardGroup_id,float cardGroup_damage,ElementType damage_Type,BuffCode buffCode_1, BuffCode buffCode_2,
                         BuffCode buffCode_3, BuffCode buffCode_4,int buff_1_Tire, int buff_2_Tire, int buff_3_Tire, int buff_4_Tire,
                         string buff_1_Target, string buff_2_Target, string buff_3_Target, string buff_4_Target)
        {
            this.CardGroup_ID = cardGroup_id;
            this.CardGroup_Damage = cardGroup_damage;
            this.Damage_Type = damage_Type;
            this.BuffCode_1 = buffCode_1;
            this.BuffCode_2 = buffCode_2;
            this.BuffCode_3 = buffCode_3;
            this.BuffCode_4 = buffCode_4;
            this.Buff_1_Tire = buff_1_Tire;
            this.Buff_2_Tire = buff_2_Tire;
            this.Buff_3_Tire = buff_3_Tire;
            this.Buff_4_Tire = buff_4_Tire;
            this.Buff_1_Target = buff_1_Target;
            this.Buff_2_Target = buff_2_Target;
            this.Buff_3_Target = buff_3_Target;
            this.Buff_4_Target = buff_4_Target;
        }


        public string CardGroup_ID { get; set; }
        public float CardGroup_Damage { get; set; }
        public ElementType Damage_Type { get; set; }
        public BuffCode BuffCode_1 { get; set; }
        public BuffCode BuffCode_2 { get; set; }
        public BuffCode BuffCode_3 { get; set; }
        public BuffCode BuffCode_4 { get; set; }
        public int Buff_1_Tire { get; set; }
        public int Buff_2_Tire { get; set; }
        public int Buff_3_Tire { get; set; }
        public int Buff_4_Tire { get; set; }
        public string Buff_1_Target { get; set; }
        public string Buff_2_Target { get; set; }
        public string Buff_3_Target { get; set; }
        public string Buff_4_Target { get; set; }

    }
}
