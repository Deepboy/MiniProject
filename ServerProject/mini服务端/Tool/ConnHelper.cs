using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using Common;
using GameServer.Buff;
using System.Reflection;
using GameServer.Model;

namespace GameServer.Tool
{
    class ConnHelper
    {
        public const string CONNECTIONSTRING = "datasource=127.0.0.1;port=3306;database=test;user=root;pwd=a15626122728.;";

        public static MySqlConnection Connect()
        {
            MySqlConnection conn = new MySqlConnection(CONNECTIONSTRING);
            try
            {
                conn.Open();
                return conn;
            }
            catch (Exception e)
            {
                Console.WriteLine("连接数据库的时候出现异常：" + e);
                return null;
            }
        }

        public static void CloseConnection(MySqlConnection conn)
        {
            if (conn == null)
            {
                Console.WriteLine("MySqlConnection不能为空");
            }
            else
            {
                conn.Close();
            }
        }

        public static Dictionary<BuffCode, BaseBuff> GetBuffInfoDict() {
            MySqlConnection conn = Connect();
            Dictionary<BuffCode, BaseBuff> BuffInfoDict = new Dictionary<BuffCode, BaseBuff>();
            MySqlDataReader reader = null;
            try
            {
                MySqlCommand cmd = new MySqlCommand("select * from buff", conn);
                reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read()) {
                        int buff_id = reader.GetInt32("buff_id");
                        string buff_name = reader.GetString("buff_name");
                        string buff_ename = reader.GetString("buff_ename");
                        string element_type = reader.GetString("element_type");
                        string dispel = reader.GetString("dispel");
                        string type = reader.GetString("type");
                        string flag = reader.GetString("flag");

                        float v1 = reader.GetFloat("v1");
                        float v2 = reader.GetFloat("v2");
                        float v3 = reader.GetFloat("v3");
                        float v4 = reader.GetFloat("v4");
                        float v5 = reader.GetFloat("v5");
                        float v6 = reader.GetFloat("v6");
                        float v7 = reader.GetFloat("v7");
                        float v8 = reader.GetFloat("v8");

                        float duration = reader.GetFloat("duration");
                        float action_time = reader.GetFloat("action_time");
                        int max_tier = reader.GetInt32("max_tier");


                        BaseBuff buff = new BaseBuff(buff_id,buff_name, (BuffCode)Enum.Parse(typeof(BuffCode), buff_ename), (ElementType)Enum.Parse(typeof(ElementType), element_type),
                            (BuffDispelType)Enum.Parse(typeof(BuffDispelType), dispel), (BuffType)Enum.Parse(typeof(BuffType), type), (BuffFlag)Enum.Parse(typeof(BuffFlag), flag),
                            v1,v2,v3,v4,v5,v6,v7,v8, duration, action_time, max_tier);

                        BuffInfoDict.Add((BuffCode)Enum.Parse(typeof(BuffCode), buff_ename), buff);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("在GetBuffInfoDict的时候出现异常：" + e);
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
                CloseConnection(conn);
            }
            return BuffInfoDict;
        }

        public static Dictionary<string, CardEffect> GetCardEffectDict()
        {
            MySqlConnection conn = Connect();
            Dictionary<string, CardEffect> CardEffectDict = new Dictionary<string, CardEffect>();
            MySqlDataReader reader = null;
            try
            {
                MySqlCommand cmd = new MySqlCommand("select * from card_effect", conn);
                reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        string card_id = reader.GetString("card_id");
                        float damage = reader.GetFloat("damage");
                        string element_type = reader.GetString("element_type");

                        string buff_name_1 = reader.GetString("buff_name_1");
                        int add_buff_1_tier = reader.GetInt32("add_buff_1_tire");
                        string buff_1_target = reader.GetString("buff_1_target");

                        string buff_name_2 = reader.GetString("buff_name_2");
                        int add_buff_2_tier = reader.GetInt32("add_buff_2_tire");
                        string buff_2_target = reader.GetString("buff_2_target");

                        string buff_name_3 = reader.GetString("buff_name_3");
                        int add_buff_3_tier = reader.GetInt32("add_buff_3_tire");
                        string buff_3_target = reader.GetString("buff_3_target");

                        string buff_name_4 = reader.GetString("buff_name_4");
                        int add_buff_4_tier = reader.GetInt32("add_buff_4_tire");
                        string buff_4_target = reader.GetString("buff_4_target");

                        CardEffect cardEffect = new CardEffect(card_id,damage,(ElementType)Enum.Parse(typeof(ElementType),element_type),
                                                               (BuffCode)Enum.Parse(typeof(BuffCode), buff_name_1), add_buff_1_tier, (BuffTarget)Enum.Parse(typeof(BuffTarget), buff_1_target),
                                                               (BuffCode)Enum.Parse(typeof(BuffCode), buff_name_2), add_buff_2_tier, (BuffTarget)Enum.Parse(typeof(BuffTarget), buff_2_target),
                                                               (BuffCode)Enum.Parse(typeof(BuffCode), buff_name_3), add_buff_3_tier, (BuffTarget)Enum.Parse(typeof(BuffTarget), buff_3_target),
                                                               (BuffCode)Enum.Parse(typeof(BuffCode), buff_name_4), add_buff_4_tier, (BuffTarget)Enum.Parse(typeof(BuffTarget), buff_4_target));
                        
                        CardEffectDict.Add(card_id, cardEffect);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("在GetCardEffectDict的时候出现异常：" + e);
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
                CloseConnection(conn);
            }
            return CardEffectDict;
        }

        public static Dictionary<string, HeroCard> GetAllHeroCardDict()
        {
            MySqlConnection conn = Connect();
            Dictionary<string, HeroCard> HeroCardDict = new Dictionary<string, HeroCard>();
            MySqlDataReader reader = null;
            try
            {
                MySqlCommand cmd = new MySqlCommand("select * from hero_card", conn);
                reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        string hero_id = reader.GetString("hero_id");
                        string hero_name = reader.GetString("hero_name");
                        int hero_starLevel = reader.GetInt32("hero_starLevel");
                        int hero_level = reader.GetInt32("hero_level");
                        string hero_type = reader.GetString("hero_type");
                        float hero_hp = reader.GetFloat("hero_hp");
                        float hero_hp_growth = reader.GetFloat("hero_hp_growth");
                        float hero_speed = reader.GetFloat("hero_speed");
                        float hero_speed_growth = reader.GetFloat("hero_speed_growth");
                        float wind = reader.GetFloat("wind");
                        float wind_growth = reader.GetFloat("wind_growth");
                        float fire = reader.GetFloat("fire");
                        float fire_growth = reader.GetFloat("fire_growth");
                        float water = reader.GetFloat("water");
                        float water_growth = reader.GetFloat("water_growth");
                        float thunder = reader.GetFloat("thunder");
                        float thunder_growth = reader.GetFloat("thunder_growth");
                        float soil = reader.GetFloat("soil");
                        float soil_growth = reader.GetFloat("soil_growth");
                        float dark = reader.GetFloat("dark");
                        float dark_growth = reader.GetFloat("dark_growth");


                        string prossive_skill_1_id = reader.GetString("prossive_skill_1_id");
                        string prossive_skill_2_id = reader.GetString("prossive_skill_2_id");
                        string prossive_skill_3_id = reader.GetString("prossive_skill_3_id");
                        string active_skill_id = reader.GetString("active_skill_id");
                        string staff_skill_id = reader.GetString("staff_skill_id");



                        HeroCard heroCard = new HeroCard(hero_id, hero_name, hero_starLevel, hero_level, (ElementType)Enum.Parse(typeof(ElementType), hero_type), hero_hp,
                                                        hero_hp_growth, hero_speed, hero_speed_growth, wind, wind_growth, fire, fire_growth, water, water_growth, thunder, thunder_growth,
                                                        soil, soil_growth, dark, dark_growth, prossive_skill_1_id, prossive_skill_2_id, prossive_skill_3_id, 
                                                        active_skill_id, staff_skill_id);
                        HeroCardDict.Add(hero_id, heroCard);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("在GetAllHeroCardDict的时候出现异常：" + e);
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
                CloseConnection(conn);
            }
            return HeroCardDict;
        }

        public static Dictionary<string, Skill> GetSkillInfoDict()
        {
            MySqlConnection conn = Connect();
            Dictionary<string, Skill> SkillInfoDict = new Dictionary<string, Skill>();
            MySqlDataReader reader = null;
            try
            {
                MySqlCommand cmd = new MySqlCommand("select * from skill", conn);
                reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        //Skill_id Skill_name  skill_type skill_phase skill_CD

                        string skill_id = reader.GetString("skill_id");
                        string skill_name = reader.GetString("skill_name");
                        int skill_type = reader.GetInt32("skill_type");
                        string skill_phase = reader.GetString("skill_phase");
                        float skill_cd = reader.GetFloat("skill_cd");

                        Skill skill = new Skill(skill_id,skill_name, skill_type,(FightPhase)Enum.Parse(typeof(FightPhase),skill_phase),skill_cd);

                        SkillInfoDict.Add(skill_id,skill);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("在GetSkillInfoDict的时候出现异常：" + e);
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
                CloseConnection(conn);
            }
            return SkillInfoDict;
        }
    }
}
