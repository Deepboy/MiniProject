using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using GameServer.Buff;

namespace GameServer.Model
{
    public class CardEffect
    {
        //uff_1_target	buff_2_target	buff_3_target	buff_4_target
        public string Card_Id { get; set; }
        public float Damage { get; set; }
        public ElementType Element_type { get; set; }
        public BuffCode Buff_name_1 { get; set; }
        public BuffCode Buff_name_2 { get; set; }
        public BuffCode Buff_name_3 { get; set; }
        public BuffCode Buff_name_4 { get; set; }
        public int Add_Buff_1_Tier { get; set;}
        public int Add_Buff_2_Tier { get; set; }
        public int Add_Buff_3_Tier { get; set; }
        public int Add_Buff_4_Tier { get; set; }
        public BuffTarget Buff_1_Target { get; set; }
        public BuffTarget Buff_2_Target { get; set; }
        public BuffTarget Buff_3_Target { get; set; }
        public BuffTarget Buff_4_Target { get; set; }

        public CardEffect(string card_Id,float damage, ElementType element_type,
            BuffCode buff_name_1, int add_Buff_1_Tier, BuffTarget buff_1_Target,
            BuffCode buff_name_2, int add_Buff_2_Tier, BuffTarget buff_2_Target,
            BuffCode buff_name_3, int add_Buff_3_Tier, BuffTarget buff_3_Target, 
            BuffCode buff_name_4, int add_Buff_4_Tier, BuffTarget buff_4_Target)
        {
            this.Card_Id = card_Id;
            this.Damage = damage;
            this.Element_type = element_type;
            this.Buff_name_1 = buff_name_1;
            this.Add_Buff_1_Tier = add_Buff_1_Tier;
            this.Buff_1_Target = buff_1_Target;

            this.Buff_name_2 = buff_name_2;
            this.Add_Buff_2_Tier = add_Buff_2_Tier;
            this.Buff_2_Target = buff_2_Target;

            this.Buff_name_3 = buff_name_3;
            this.Add_Buff_3_Tier = add_Buff_3_Tier;
            this.Buff_3_Target = buff_3_Target;

            this.Buff_name_4 = buff_name_4;
            this.Add_Buff_4_Tier = add_Buff_4_Tier;
            this.Buff_4_Target = buff_4_Target;



        }
    }
}
