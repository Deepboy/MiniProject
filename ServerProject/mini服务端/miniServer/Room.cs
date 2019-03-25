using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using System.Threading;
using GameServer.Buff;
using GameServer.Model;
using System.Reflection;

namespace GameServer.miniServer
{
    enum RoomState
    {
        WaitingJoin,
        WaitingBattle,
        Battle,
        End
    }
    public class Room
    {
        private List<Client> clientRoom = new List<Client>();
        private RoomState state = RoomState.WaitingJoin;
        public volatile FightState fightState = FightState.Start;
        private Server server;


        private Client action_client;
        private FightHero action_source;
        private FightHero action_target;
        private string action_data;

        private WordType wordType=WordType.Normal;

        public Room(Server server)
        {
            this.server = server;
        }

        public bool IsWaitingJoin()
        {
            return state == RoomState.WaitingJoin;
        }

        //加入房间
        public void AddClient(Client client)
        {
            lock (clientRoom)
            {
                clientRoom.Add(client);
            }
            //是客户端持有该房间
            client.Room = this;
            //设置房间状态
            if (clientRoom.Count >= 2)
            {
                //clientRoom.Add(clientRoom[0]);
                state = RoomState.WaitingBattle;

                int count_1 = 0;
                foreach (ElementType e in clientRoom[0].Fight_Hero.ChoiceCardType)
                {
                    count_1 += clientRoom[0].Fight_Hero.ElementCardDict[e];
                }
                if (count_1 < 5)
                    GetElementCard(5 - count_1, clientRoom[0].Fight_Hero);
                int count_2 = 0;
                foreach (ElementType e in clientRoom[1].Fight_Hero.ChoiceCardType)
                {
                    count_2 += clientRoom[1].Fight_Hero.ElementCardDict[e];
                }
                if (count_2 < 5)
                    GetElementCard(5 - count_2, clientRoom[1].Fight_Hero);


                string data_1 = string.Format("{0}@{1}@{2}@{3}|{4}@{5}|{6}", clientRoom[0].Fight_Hero.ToString() + "|" + clientRoom[0].Fight_Hero.GetSkillInfo(), clientRoom[1].Fight_Hero.ToString(), clientRoom[0].Fight_Hero.GetCardInfo(), (int)clientRoom[0].Fight_Hero.OrgHp, (int)clientRoom[1].Fight_Hero.OrgHp, clientRoom[0].Fight_Hero.OrgSpeed, clientRoom[1].Fight_Hero.OrgSpeed);
                string data_2 = string.Format("{0}@{1}@{2}@{3}|{4}@{5}|{6}", clientRoom[1].Fight_Hero.ToString() + "|" + clientRoom[1].Fight_Hero.GetSkillInfo(), clientRoom[0].Fight_Hero.ToString(), clientRoom[1].Fight_Hero.GetCardInfo(), (int)clientRoom[1].Fight_Hero.OrgHp, (int)clientRoom[0].Fight_Hero.OrgHp, clientRoom[1].Fight_Hero.OrgSpeed, clientRoom[0].Fight_Hero.OrgSpeed);
                server.SendResquest(clientRoom[0], ActionCode.StartMatching, ((int)ReturnCode.Success).ToString() + "@" + data_1);
                server.SendResquest(clientRoom[1], ActionCode.StartMatching, ((int)ReturnCode.Success).ToString() + "@" + data_2);
                StartTimer();


            }
        }

        //将玩家移出房间
        public void RemoveClient(Client client)
        {
            client.Room = null;
            clientRoom.Remove(client);
            if (clientRoom.Count >= 2)
            {
                state = RoomState.WaitingBattle;
            }
            else if (clientRoom.Count <= 0) {
                server.RemoveRoom(this);
            }
            else
            {
                state = RoomState.WaitingJoin;
            }
        }

        //获得房间所有玩家信息
        public string GetRoomData()
        {
            StringBuilder sb = new StringBuilder();
            foreach (Client client in clientRoom)
            {
                sb.Append(client.GetUserInfo().ToString() + "@");
            }
            if (sb.Length > 0)
            {
                sb.Remove(sb.Length - 1, 1);
            }
            return sb.ToString();
        }

        //获取房间里面的所有客户端列表
        public List<Client> GetRoomClient() {
            return clientRoom;
        }

        //广播
        public void BroadcastMessage(Client excludeClient, ActionCode actionCode, string data)
        {
            foreach (Client client in clientRoom)
            {
                if (client != excludeClient && client.isConnect)
                {
                    server.SendResquest(client, actionCode, data);
                    Console.WriteLine(data);
                }
            }
        }


        //开启游戏同步线程
        public void StartGame()
        {
            new Thread(RunGame).Start();
        }

        //同步游戏信息
        private void RunGame()
        {
            Thread.Sleep(500);
            FightHero player_1 = null;
            FightHero player_2 = null;
            for (; ; )
            {
                player_1 = clientRoom[0].Fight_Hero;
                player_2 = clientRoom[1].Fight_Hero;
                player_1 = SetFightHeroInfoByBuff(player_1);//更新角色数值
                player_2 = SetFightHeroInfoByBuff(player_2);//更新角色数值

                if (!player_1.User_Client.isConnect && !player_2.User_Client.isConnect && fightState==FightState.Prepare) {
                    fightState = FightState.Intend; 
                }

                StartPhase(player_1, player_2);//战斗开始阶段
                IntendPhase(player_1,player_2);//集气阶段
                ActionPhase(player_1, player_2);
                SkillPhase(player_1, player_2);


                string data_1 = string.Format("{0}@{1}", player_1.ToString() + "|" + player_1.GetSkillInfo(), player_2.ToString());
                string data_2 = string.Format("{0}@{1}", player_2.ToString() + "|" + player_2.GetSkillInfo(), player_1.ToString());

                server.SendResquest(player_1.User_Client, ActionCode.StartGame, data_1);
                server.SendResquest(player_2.User_Client, ActionCode.StartGame, data_2);
                if ((int)player_1.Hp < 1 || (int)player_2.Hp < 1)
                    break;
                Thread.Sleep(10);
            }
            if ((int)player_1.Hp <= 0)
            {
                player_1.User_Client.Send(ActionCode.GameOver, ((int)ReturnCode.Fail).ToString());
                player_2.User_Client.Send(ActionCode.GameOver, ((int)ReturnCode.Success).ToString());
            }
            if ((int)player_2.Hp <= 0)
            {
                player_1.User_Client.Send(ActionCode.GameOver, ((int)ReturnCode.Success).ToString());
                player_2.User_Client.Send(ActionCode.GameOver, ((int)ReturnCode.Fail).ToString());
            }
        }

        //计算角色数值
        private FightHero SetFightHeroInfoByBuff(FightHero fightHero)
        {
            fightHero.OrgFightHeroInfo();
            List<BuffCode> buffDictIndex = new List<BuffCode>();
            buffDictIndex.AddRange(fightHero.BuffDict.Keys);
            foreach (BuffCode key in buffDictIndex)
            {
                if (fightHero.BuffDict.ContainsKey(key)) {
                    BaseBuff buff = fightHero.BuffDict[key];
                    buff.MoreAssistEffect(fightHero);
                }
            }
            return fightHero;
        }

        //行动点累加
        public void AddNumbOfAction(FightHero fightHero,float t) {
            fightHero.RateOfAction += (t / ((200 / fightHero.Speed) + 2));
        }

        //抽牌
        public void GetElementCard(int count,FightHero fightHero) {
            Random r1 = new Random();
            for (int i = 0; i < count; i++) {
                int cardIndex = r1.Next(0,fightHero.ChoiceCardType.Count);
                fightHero.ElementCardDict [fightHero.ChoiceCardType[cardIndex]]+= 1;
            }
        }

        //使用手牌
        public bool UseElementCard(Client client, FightHero source,FightHero target ,string data) {
            //判断是否符合条件
            if (fightState != FightState.Intend) return false;
            fightState = FightState.Prepare;
            if (!CanUserCard(source, data)) {
                fightState = FightState.Intend;
                return false;
            }
            action_client = client;
            action_source = source;
            action_target = target;
            action_data = data;
            //设为行动阶段
            fightState = FightState.Action;
            //扣除对应手牌,行动点和抽牌
            

            if (action_data.Contains("5"))
            {
                string tmp_data = action_data.Replace("5", "4");
                Dictionary<string, CardEffect> cardEffectDict = action_client.CardEffectDict;
                string[] strs = tmp_data.Split('|');
                if (strs.Length != 6) Console.WriteLine("出牌数据错误");
                string card_id = (int.Parse(strs[0] + strs[1] + strs[2] + strs[3] + strs[4] + strs[5])).ToString();
                CardEffect cardeffect = cardEffectDict[card_id];
                if (!action_source.BuffDict.ContainsKey(cardeffect.Buff_name_1))
                {
                    action_data = "2|2|1|0|0|0";
                }
            }
            DeleteAndAddCard(source, data,action_data);
            Console.WriteLine("lalalalala:" + action_data);
            BroadcastMessage(client, ActionCode.OutCard, string.Format("{0}|{1}", ((int)ReturnCode.Waiting).ToString(), action_data.Replace("|", "")));
            return true;
        }


        //使用技能
        public bool UsePlayerSkill(Client client, FightHero source, FightHero target, string data) {
            //判断是否符合条件
            if (fightState != FightState.Intend) return false;
            fightState = FightState.Prepare;
            if (!CanUserSkill(source, data))
            {
                fightState = FightState.Intend;
                return false;
            }
            action_client = client;
            action_source = source;
            action_target = target;
            action_data = data;
            //设为技能阶段
            fightState = FightState.Skill;
            BroadcastMessage(client, ActionCode.UseSkill, string.Format("{0}|{1}", ((int)ReturnCode.Waiting).ToString(), data.Replace("|", "")));
            return true;
        }

        //开始阶段
        private void StartPhase(FightHero player_1, FightHero player_2) {
            if (fightState != FightState.Start) return;
            //调用出场被动
            UseProssiveSkill(FightPhase.BeforeEnter, player_1, player_2);
            UseProssiveSkill(FightPhase.InEnter, player_1, player_2);
            UseProssiveSkill(FightPhase.AfterEnter, player_1, player_2);
            //调用进入战斗时被动
            UseProssiveSkill(FightPhase.BeforeStart, player_1, player_2);
            UseProssiveSkill(FightPhase.InStart, player_1, player_2);
            UseProssiveSkill(FightPhase.AfterStart, player_1, player_2);
            //设为集气阶段
            fightState = FightState.Intend;
        }

        //集气阶段
        private void IntendPhase(FightHero player_1,FightHero player_2){
            if (fightState != FightState.Intend) return;
            //复制buff列表
            List<BuffCode> player_1_bufflist = new List<BuffCode>();
            List<BuffCode> player_2_bufflist = new List<BuffCode>();
            if (player_1.BuffDict != null)
                player_1_bufflist.AddRange(player_1.BuffDict.Keys);
            if (player_2.BuffDict != null)
                player_2_bufflist.AddRange(player_2.BuffDict.Keys);
            //集气阶段前
            UseProssiveSkill(FightPhase.BeforeIntend, player_1, player_2);
            foreach (BuffCode key in player_1_bufflist)
                if (player_1.BuffDict.ContainsKey(key))
                    player_1.BuffDict[key].IntendPhase(FightPhase.BeforeIntend, player_1,player_2 ,"");
            foreach (BuffCode key in player_2_bufflist)
                if (player_2.BuffDict.ContainsKey(key))
                    player_2.BuffDict[key].IntendPhase(FightPhase.BeforeIntend, player_2,player_1, "");
            //Buff的CD
            foreach (BuffCode key in player_1_bufflist)
                if (player_1.BuffDict.ContainsKey(key))
                    player_1.BuffDict[key].UpdateBuff(0.01f);
            foreach (BuffCode key in player_2_bufflist)
                if (player_2.BuffDict.ContainsKey(key))
                    player_2.BuffDict[key].UpdateBuff(0.01f);
            //技能的CD
            player_1.Hero_Card.UpdataSkill_cd(0.01f);
            player_2.Hero_Card.UpdataSkill_cd(0.01f);
            //集气阶段中效果
            UseProssiveSkill(FightPhase.InIntend, player_1, player_2);
            foreach (BuffCode key in player_1_bufflist)
                if (player_1.BuffDict.ContainsKey(key))
                    player_1.BuffDict[key].IntendPhase(FightPhase.InIntend, player_1,player_2, "");
            foreach (BuffCode key in player_2_bufflist)
                if (player_2.BuffDict.ContainsKey(key))
                    player_2.BuffDict[key].IntendPhase(FightPhase.InIntend, player_2,player_1, "");
            //移除时间归0的buff
            RemoveEmptyBuff(player_1);
            RemoveEmptyBuff(player_2);
            //集气条
            AddNumbOfAction(player_1,0.01f);
            AddNumbOfAction(player_2, 0.01f);
            //集气阶段后效果
            UseProssiveSkill(FightPhase.AfterIntend, player_1, player_2);
            foreach (BuffCode key in player_1_bufflist)
                if (player_1.BuffDict.ContainsKey(key))
                    player_1.BuffDict[key].IntendPhase(FightPhase.AfterIntend, player_1,player_2, "");
            foreach (BuffCode key in player_2_bufflist)
                if (player_2.BuffDict.ContainsKey(key))
                    player_2.BuffDict[key].IntendPhase(FightPhase.AfterIntend, player_2,player_1, "");

        }

        //行动阶段
        private void ActionPhase(FightHero player_1, FightHero player_2) {
            if (fightState != FightState.Action) return;
            fightState = FightState.Prepare;
            //复制buff列表
            List<BuffCode> action_source_bufflist = new List<BuffCode>();
            List<BuffCode> action_target_bufflist = new List<BuffCode>();
            action_source_bufflist.AddRange(action_source.BuffDict.Keys);
            action_target_bufflist.AddRange(action_target.BuffDict.Keys);
            //行动阶段前
            action_source.Hero_Card.Prossive_skill_function(FightPhase.BeforeAction,action_source,action_target);
            action_target.Hero_Card.Prossive_skill_function(FightPhase.BeforeAction, action_target,action_source);
            foreach (BuffCode key in action_source_bufflist)
                if (action_source.BuffDict.ContainsKey(key))
                    action_source.BuffDict[key].ActionPhase(FightPhase.BeforeAction, action_source,action_target, action_data);
            foreach (BuffCode key in action_target_bufflist)
                if (action_target.BuffDict.ContainsKey(key))
                    action_target.BuffDict[key].ActionPhase(FightPhase.BeforeAction, action_target,action_source, action_data);
            //行动阶段中
            action_source.Hero_Card.Prossive_skill_function(FightPhase.InAction, action_source, action_target);
            action_target.Hero_Card.Prossive_skill_function(FightPhase.InAction, action_target, action_source);
            foreach (BuffCode key in action_source_bufflist)
                if(action_source.BuffDict.ContainsKey(key))
                    action_source.BuffDict[key].ActionPhase(FightPhase.InAction, action_source, action_target, action_data);
            foreach (BuffCode key in action_target_bufflist)
                if (action_target.BuffDict.ContainsKey(key))
                    action_target.BuffDict[key].ActionPhase(FightPhase.InAction, action_target, action_source, action_data);
            //调用手牌效果
            GetUseCardEffect(action_client, action_source, action_target, action_data);
            
        }

        //技能阶段
        private void SkillPhase(FightHero player_1, FightHero player_2) {
            if (fightState != FightState.Skill) return;
            List<BuffCode> action_source_bufflist = new List<BuffCode>();
            List<BuffCode> action_target_bufflist = new List<BuffCode>();
            action_source_bufflist.AddRange(action_source.BuffDict.Keys);
            action_target_bufflist.AddRange(action_target.BuffDict.Keys);
            //技能阶段前
            action_source.Hero_Card.Prossive_skill_function(FightPhase.BeforeAction, action_source, action_target);
            action_target.Hero_Card.Prossive_skill_function(FightPhase.BeforeOtherAction, action_target, action_source);
            foreach (BuffCode key in action_source_bufflist)
                if (action_source.BuffDict.ContainsKey(key))
                    action_source.BuffDict[key].ActionPhase(FightPhase.BeforeAction, action_source, action_target, action_data);
            foreach (BuffCode key in action_target_bufflist)
                if (action_target.BuffDict.ContainsKey(key))
                    action_target.BuffDict[key].ActionPhase(FightPhase.BeforeAction, action_target, action_source, action_data);
            //技能阶段中
            action_source.Hero_Card.Prossive_skill_function(FightPhase.InAction, action_source, action_target);
            action_target.Hero_Card.Prossive_skill_function(FightPhase.InOtherAction, action_target, action_source);
            foreach (BuffCode key in action_source_bufflist)
                if (action_source.BuffDict.ContainsKey(key))
                    action_source.BuffDict[key].ActionPhase(FightPhase.InAction, action_source, action_target, action_data);
            foreach (BuffCode key in action_target_bufflist)
                if (action_target.BuffDict.ContainsKey(key))
                    action_target.BuffDict[key].ActionPhase(FightPhase.InAction, action_target, action_source, action_data);
            //调用技能效果
            if (action_source.Hero_Card.Action_skill.Skill_id == action_data) action_source.Hero_Card.Action_skill_function(FightPhase.InIntend, action_source, action_target);
            if (action_source.Hero_Card.Staff_skill_other.Skill_id == action_data) action_source.Hero_Card.Staff_skill_function(FightPhase.InIntend, action_source, action_target);
            //技能阶段后
            action_source.Hero_Card.Prossive_skill_function(FightPhase.AfterAction, action_source, action_target);
            action_target.Hero_Card.Prossive_skill_function(FightPhase.AfterOtherAction, action_target, action_source);
            foreach (BuffCode key in action_source_bufflist)
                if (action_source.BuffDict.ContainsKey(key))
                    action_source.BuffDict[key].ActionPhase(FightPhase.AfterAction, action_source,action_target, action_data);
            if(action_source.Hero_Card.Staff_skill_other.Skill_id == action_data)
                fightState = FightState.Intend;
            else
                fightState = FightState.Prepare;
        }


        /// <summary>
        /// 伤害阶段（伤害计算函数）
        /// </summary>
        /// <param name="OrgDamage">初始伤害值</param>
        /// <param name="damageResource">直接伤害/间接伤害</param>
        /// <param name="damageType">伤害类型</param>
        /// <param name="elementType">伤害所属元素类型</param>
        /// <param name="source">发起伤害角色</param>
        /// <param name="target">承受伤害角色</param>
        public void TakeDamage(float OrgDamage,DamageResource damageResource,DamageType damageType,ElementType elementType, FightHero source,FightHero target) {
            wordType = WordType.Normal;
            float damage = OrgDamage;
            List <BuffCode> source_bufflist = new List<BuffCode>();
            List<BuffCode> target_bufflist = new List<BuffCode>();
            source_bufflist.AddRange(source.BuffDict.Keys);
            target_bufflist.AddRange(target.BuffDict.Keys);
            //直接伤害前被动执行
            if (damageResource == DamageResource.DirectDamage) {
                source.Hero_Card.Prossive_skill_function(FightPhase.BeforeDemage, source, target);
                target.Hero_Card.Prossive_skill_function(FightPhase.BeforeHit, target, source);
            }
            //buff伤害阶段前执行
            string data = string.Format("{0}|{1}|{2}|{3}",damage.ToString(),damageResource.ToString(),damageType.ToString(),elementType.ToString());
            foreach (BuffCode key in source_bufflist)
                if (source.BuffDict.ContainsKey(key))
                    source.BuffDict[key].DamagePhase(FightPhase.BeforeDemage, target,source, data);
            foreach (BuffCode key in target_bufflist)
                if (target.BuffDict.ContainsKey(key))
                    target.BuffDict[key].HitPhase(FightPhase.BeforeHit, source,target, data);
            //判断目标是否无敌
            if (target.IsInvincible) return;
            //判断目标是否闪避
            Random r = new Random();
            float i = r.Next(1, 100)/100f;
            if (i < target.Dodge) {
                BroadcastMessage(null, ActionCode.TakeDamage, string.Format("{0}|{1}|{2}", target.User_Client.GetUserInfo().User_id,(int)wordType, damage));
                return;
            }
            //真实伤害
            if (damageType == DamageType.Real) {
                wordType = WordType.RealDamage;
                damage = OrgDamage;
            } 
            //元素伤害
            if (damageType == DamageType.Element && elementType!=ElementType.Chaos) {
                float sourceAttribute = source.GetAttributeByEnum(elementType);
                float targetAttribute = target.GetAttributeByEnum(elementType);
                float sourceAttributeDamage = source.GetAttributeDamageByEnum(elementType);
                float targetAttributeDamage = target.GetAttributeDamageByEnum(elementType);
                float sourceAttributeDefense = source.GetAttributeDefenseByEnum(elementType);
                float targetAttributeDefense = target.GetAttributeDefenseByEnum(elementType);
                damage = ((1 - source.Pass_through) * (OrgDamage + source.Increase_Damage) * ((sourceAttribute / 150) * (sourceAttribute / 150) + 0.4f) * ((1000 - targetAttribute - target.Defense) / (1000 + targetAttribute + target.Defense)) * sourceAttributeDamage * source.All_Element_Damage / targetAttributeDefense / target.All_Element_Defense
                    + source.Pass_through * (OrgDamage + source.Increase_Damage) * ((sourceAttribute / 150) * (sourceAttribute / 150) + 0.4f) * ((1000 - targetAttribute - target.Defense) / (1000 + targetAttribute + target.Defense)) * sourceAttributeDamage * source.All_Element_Damage / targetAttributeDefense / target.All_Element_Defense
                    - target.Parry) * source.Final_Damage / target.Final_Defense;
            }
            //普通伤害
            if (damageType == DamageType.Normal|| elementType == ElementType.Chaos) {
                damage=(OrgDamage + source.Increase_Damage - source.Parry) * source.Final_Damage / target.Final_Defense;
            }
            //判断伤害是否暴击
            float i1 = r.Next(1, 100) / 100f;
            if (i1 < source.Crit && damageType != DamageType.Real) {
                wordType = WordType.CritDamage;
                damage *= source.Crit_Damage;
            } 
            //向上取整
            if(damage%1!=0)
                damage = (int)damage + 1;
            //扣除护盾抵挡，并设置剩余护盾值
            damage -= target.Shield;
            if (damage <= 0)
            {
                target.Shield = -damage;
                damage = 0;
            }
            else {
                target.Shield = 0;
            }
            //直接伤害
            if (damageResource == DamageResource.DirectDamage) {
                //直接伤害时被动执行
                source.Hero_Card.Prossive_skill_function(FightPhase.InDemage, source, target);
                target.Hero_Card.Prossive_skill_function(FightPhase.InHit, target, source);
                //buff伤害阶段时执行
                data = string.Format("{0}|{1}|{2}|{3}", damage.ToString(), damageResource.ToString(), damageType.ToString(), elementType.ToString());
                foreach (BuffCode key in source_bufflist)
                    if (source.BuffDict.ContainsKey(key))
                        source.BuffDict[key].DamagePhase(FightPhase.InDemage, target,source, data);
                foreach (BuffCode key in target_bufflist)
                    if (target.BuffDict.ContainsKey(key))
                        target.BuffDict[key].HitPhase(FightPhase.InHit, source,target, data);
                target.Hp -= damage;
                target.DamageCaculation.Clear();
                target.DamageCaculation.Add(elementType, damage);
                Console.WriteLine("扣血：" + damage);
                Console.WriteLine("剩余血量" + target.Hp);
                source.Hero_Card.Prossive_skill_function(FightPhase.AfterDemage, source, target);
                target.Hero_Card.Prossive_skill_function(FightPhase.AfterHit, target, source);
                foreach (BuffCode key in source_bufflist)
                    if (source.BuffDict.ContainsKey(key))
                        source.BuffDict[key].DamagePhase(FightPhase.AfterDemage, target,source, data);
                foreach (BuffCode key in target_bufflist)
                    if (target.BuffDict.ContainsKey(key))
                        target.BuffDict[key].HitPhase(FightPhase.AfterHit, source,target, data);
            }
            //间接伤害
            if (damageResource == DamageResource.CollateralDamage) target.Hp -= damage;
            BroadcastMessage(null, ActionCode.TakeDamage, string.Format("{0}|{1}|{2}", target.User_Client.GetUserInfo().User_id, (int)wordType, damage));
            //吸血
            if (source.SuckBlood > 0 && damage>0) {
                AddHp(damage*source.SuckBlood,source,source);
            }
            
        }

        //回血
        public void AddHp(float count,FightHero source,FightHero target) {
            int recover_count = count % 1 == 0 ? (int)count : (int)count + 1;
            target.Hp += recover_count;
            BroadcastMessage(null, ActionCode.TakeDamage, string.Format("{0}|{1}|{2}", target.User_Client.GetUserInfo().User_id, (int)WordType.Recover, recover_count));
        }

        //使用双方被动技能
        private void UseProssiveSkill(FightPhase fightPhase,FightHero player_1,FightHero player_2) {
            player_1.Hero_Card.Prossive_skill_function(fightPhase, player_1, player_2);
            player_2.Hero_Card.Prossive_skill_function(fightPhase, player_2, player_1);
        }

        //判断是否能出牌
        private bool CanUserCard(FightHero fightHero, string data) {
            //火水土风雷暗
            //判断是否眩晕
            if (fightHero.IsVertigo) return false;
            //判断是否够行动点
            string[] strs = data.Split('|');
            int count = 0;
            foreach (string s in strs)
            {
                count += int.Parse(s);
            }
            if (fightHero.RateOfAction < count) return false;
            if (count <= 0) return false;
            //判断是否是自己手牌的牌
            if (fightHero.ElementCardDict[ElementType.Fire] < int.Parse(strs[0]) ||
                fightHero.ElementCardDict[ElementType.Water] < int.Parse(strs[1]) ||
                fightHero.ElementCardDict[ElementType.Earth] < int.Parse(strs[2]) ||
                fightHero.ElementCardDict[ElementType.Wind] < int.Parse(strs[3]) ||
                fightHero.ElementCardDict[ElementType.Thunder] < int.Parse(strs[4]) ||
                fightHero.ElementCardDict[ElementType.Dark] < int.Parse(strs[5])
                ) return false;
            return true;
        }

        //判断是否使用技能
        private bool CanUserSkill(FightHero fightHero, string data)
        {
            //判断是否被禁
            if (!fightHero.IsOperate)return false;
            //判断是否是主战角色技能并判断是否进入CD
            if (fightHero.Hero_Card.Action_skill.Skill_id == data && fightHero.Hero_Card.Action_skill.Skill_enable == true) return true;
            //判断是否是助战角色技能并判断是否进入CD
            if (fightHero.Hero_Card.Staff_skill_other.Skill_id == data && fightHero.Hero_Card.Staff_skill_other.isActionSkill && fightHero.Hero_Card.Staff_skill_other.Skill_enable == true) return true;
            return false;
        }

        //消耗行动点和卡和抽卡
        private void DeleteAndAddCard(FightHero fightHero, string data,string effectInfo) {
            //火水土风雷暗
            string[] strs = data.Split('|');
            int count = 0;
            foreach (string s in strs)
            {
                count += int.Parse(s);
            }
            fightHero.ElementCardDict[ElementType.Fire] -= int.Parse(strs[0]);
            fightHero.ElementCardDict[ElementType.Water] -= int.Parse(strs[1]);
            fightHero.ElementCardDict[ElementType.Earth] -= int.Parse(strs[2]);
            fightHero.ElementCardDict[ElementType.Wind] -= int.Parse(strs[3]);
            fightHero.ElementCardDict[ElementType.Thunder] -= int.Parse(strs[4]);
            fightHero.ElementCardDict[ElementType.Dark] -= int.Parse(strs[5]);

            fightHero.UsedCardDict[ElementType.Fire] = int.Parse(strs[0]);
            fightHero.UsedCardDict[ElementType.Water] = int.Parse(strs[1]);
            fightHero.UsedCardDict[ElementType.Earth] = int.Parse(strs[2]);
            fightHero.UsedCardDict[ElementType.Wind] = int.Parse(strs[3]);
            fightHero.UsedCardDict[ElementType.Thunder] = int.Parse(strs[4]);
            fightHero.UsedCardDict[ElementType.Dark] = int.Parse(strs[5]);

            fightHero.RateOfAction -= count;
            GetElementCard(count, fightHero);


            server.SendResquest(fightHero.User_Client, ActionCode.OutCard, string.Format("{0}|{1}|#{2}",((int)ReturnCode.Success).ToString(),fightHero.GetCardInfo(), effectInfo));
        }

        //调用手牌效果
        private void GetUseCardEffect(Client client, FightHero source,FightHero target, string data) {
            Dictionary<string, CardEffect> cardEffectDict = client.CardEffectDict;
            Dictionary<BuffCode, BaseBuff> buffInfoDict = client.BaseInfoDict;
            string[] strs = data.Split('|');
            if (strs.Length != 6) Console.WriteLine("出牌数据错误");
            string card_id = (int.Parse(strs[0] + strs[1] + strs[2] + strs[3] + strs[4] + strs[5])).ToString();
            if (cardEffectDict.ContainsKey(card_id))
            {
                CardEffect cardeffect = cardEffectDict[card_id];
                Console.WriteLine("挂Buff:"+ cardeffect.Buff_name_1.ToString()+"挂buff:"+ cardeffect.Buff_name_4.ToString());
                if (cardeffect.Buff_name_1 != BuffCode.None)
                    SetCardEffectBuff(source, target, buffInfoDict[cardeffect.Buff_name_1], cardeffect.Add_Buff_1_Tier, cardeffect.Buff_1_Target);
                if (cardeffect.Buff_name_2 != BuffCode.None)
                    SetCardEffectBuff(source, target, buffInfoDict[cardeffect.Buff_name_2], cardeffect.Add_Buff_2_Tier, cardeffect.Buff_2_Target);
                if (cardeffect.Buff_name_3 != BuffCode.None)
                    SetCardEffectBuff(source, target, buffInfoDict[cardeffect.Buff_name_3], cardeffect.Add_Buff_3_Tier, cardeffect.Buff_3_Target);
                if (cardeffect.Buff_name_4 != BuffCode.None)
                    SetCardEffectBuff(source, target, buffInfoDict[cardeffect.Buff_name_4], cardeffect.Add_Buff_4_Tier, cardeffect.Buff_4_Target);
                //行动阶段后
                List<BuffCode> action_source_bufflist = new List<BuffCode>();
                List<BuffCode> action_target_bufflist = new List<BuffCode>();
                action_source_bufflist.AddRange(source.BuffDict.Keys);
                action_target_bufflist.AddRange(target.BuffDict.Keys);
                source.Hero_Card.Prossive_skill_function(FightPhase.AfterAction, source, target);
                target.Hero_Card.Prossive_skill_function(FightPhase.AfterAction, target, source);
                foreach (BuffCode key in action_source_bufflist)
                    if (source.BuffDict.ContainsKey(key))
                        source.BuffDict[key].ActionPhase(FightPhase.AfterAction, source, target, data);
                foreach (BuffCode key in action_target_bufflist)
                    if (target.BuffDict.ContainsKey(key))
                        target.BuffDict[key].ActionPhase(FightPhase.AfterAction, target, source, data);
                if (cardeffect.Damage > 0)
                    TakeDamage(cardeffect.Damage, DamageResource.DirectDamage, DamageType.Element, cardeffect.Element_type, source, target);
            }
            else {
                //行动阶段后
                List<BuffCode> action_source_bufflist = new List<BuffCode>();
                List<BuffCode> action_target_bufflist = new List<BuffCode>();
                action_source_bufflist.AddRange(source.BuffDict.Keys);
                action_target_bufflist.AddRange(target.BuffDict.Keys);
                source.Hero_Card.Prossive_skill_function(FightPhase.AfterAction, source, target);
                target.Hero_Card.Prossive_skill_function(FightPhase.AfterAction, target, source);
                foreach (BuffCode key in action_source_bufflist)
                    if (source.BuffDict.ContainsKey(key))
                        source.BuffDict[key].ActionPhase(FightPhase.AfterAction, source, target, data);
                foreach (BuffCode key in action_target_bufflist)
                    if (target.BuffDict.ContainsKey(key))
                        target.BuffDict[key].ActionPhase(FightPhase.AfterAction, target, source, data);
                if (int.Parse(strs[0]) + int.Parse(strs[1]) + int.Parse(strs[2]) + int.Parse(strs[3]) + int.Parse(strs[4]) + int.Parse(strs[5]) == 4)
                    TakeDamage(45, DamageResource.DirectDamage, DamageType.Normal, ElementType.Chaos, source, target);
                if (int.Parse(strs[0]) + int.Parse(strs[1]) + int.Parse(strs[2]) + int.Parse(strs[3]) + int.Parse(strs[4]) + int.Parse(strs[5]) == 5)
                    TakeDamage(60, DamageResource.DirectDamage, DamageType.Normal, ElementType.Chaos, source, target);
            }
            
        }

        /// <summary>
        /// 将所有非空buff加入对应角色上
        /// </summary>
        /// <param name="source">buff发起者</param>
        /// <param name="target">buff承受者</param>
        /// <param name="buff1">buff数据</param>
        /// <param name="buffcode">buff码</param>
        /// <param name="tier">层数</param>
        /// <param name="buff_target">对敌、对己</param>
        public void SetCardEffectBuff(FightHero source, FightHero target,BaseBuff buffdata,int tier,BuffTarget buff_target) {
            if (buffdata.Buff_Ename != BuffCode.None)
            {
                Assembly assembly = Assembly.GetExecutingAssembly();
                //参数列表，对应buff的构造函数
                object[] parameters = new object[1];
                parameters[0] = buffdata;
                object obj = assembly.CreateInstance("GameServer.Buff.Buff_" + buffdata.Buff_Id.ToString(), true, System.Reflection.BindingFlags.Default, null, parameters, null, null);
                //BaseBuff buff =new BaseBuff(obj as BaseBuff);
                BaseBuff buff = obj as BaseBuff;
                buff.Tier = tier;
                buff.Damage_Type = DamageType.Element;
                buff.BuffSource = source;
                buff.fightRoom = this;
                if (buff_target == BuffTarget.Self) {
                    if (source.IsInmmune == true && buff.Buff_Type == BuffType.NegativeState) return;
                    if (source.BuffDict.ContainsKey(buff.Buff_Ename))
                    {
                        source.BuffDict[buff.Buff_Ename].AddBuffRule(tier);
                    }
                    else {
                        source.BuffDict.Add(buff.Buff_Ename, buff);
                    }
                }
                if (buff_target == BuffTarget.Other) {
                    if (target.IsInmmune == true && buff.Buff_Type == BuffType.NegativeState) return;
                    if (target.BuffDict.ContainsKey(buff.Buff_Ename))
                    {
                        target.BuffDict[buff.Buff_Ename].AddBuffRule(tier);
                    }
                    else
                    {
                        target.BuffDict.Add(buff.Buff_Ename, buff);
                    }
                }
                    
            }
        }

        //移除持续时间归0的buff,触发移除效果
        private void RemoveEmptyBuff(FightHero target) {
            List<BuffCode> target_buffList = new List<BuffCode>();
            target_buffList.AddRange(target.BuffDict.Keys);
            foreach (BuffCode key in target_buffList)
            {
                if (target.BuffDict.ContainsKey(key) && target.BuffDict[key].DurationTime <= 0)
                {
                    target.BuffDict[key].LastDamageEffect(target);
                    target.BuffDict[key].LastAssistEffect(target);
                    target.BuffDict.Remove(key);
                }
            }
        }

        //移除持续时间未归0buff,触发移除效果
        private void RemoveBuffUseRemoveEffect(FightHero target, BuffCode buffCode) {
            target.BuffDict[buffCode].LastAssistEffect(target);
            target.BuffDict[buffCode].LastDamageEffect(target);
            target.BuffDict.Remove(buffCode);
        }

        //移除持续时间未归0buff,不触发移除效果
        private void RemoveBuffNotUseRemoveEffect(FightHero target, BuffCode buffCode) {
            target.BuffDict.Remove(buffCode);
        }


        






        //public void Close()
        //{
        //    foreach (Client client in clientRoom)
        //    {
        //        client.Room = null;
        //    }
        //    server.RemoveRoom(this);
        //}

        //public void QuitRoom(Client client)
        //{
        //    if (client == clientRoom[0])
        //    {
        //        Close();
        //    }
        //    else
        //    {
        //        clientRoom.Remove(client);
        //    }
        //}

        //开启倒计时线程
        public void StartTimer()
        {
            new Thread(RunTimer).Start();
        }

        //等待时间，游戏开始倒计时
        private void RunTimer()
        {
            Thread.Sleep(1000);
            for (int i = 3; i > 0; i--)
            {
                BroadcastMessage(null, ActionCode.ShowTimer, i.ToString());
                Thread.Sleep(1000);
            }
            BroadcastMessage(null, ActionCode.ShowTimer, "fight");
            StartGame();
        }


        public float EffectToEnemyCaculation(float effectValue, ElementType elementType,
                                               FightHero attacker, FightHero defender)
        {
            switch (elementType)
            {
                case ElementType.Water:
                    effectValue = effectValue * (attacker.Water / defender.Water)
                                  / defender.Water_Defense / defender.All_Element_Defense;
                    break;
                case ElementType.Fire:
                    effectValue = effectValue * (attacker.Fire / defender.Fire)
                                  / defender.Fire_Defense / defender.All_Element_Defense;
                    break;
                case ElementType.Thunder:
                    effectValue = effectValue * (attacker.Thunder / defender.Thunder)
                                  / defender.Thunder_Defense / defender.All_Element_Defense;
                    break;
                case ElementType.Wind:
                    effectValue = effectValue * (attacker.Wind / defender.Wind)
                                  / defender.Wind_Defense / defender.All_Element_Defense;
                    break;
                case ElementType.Earth:
                    effectValue = effectValue * (attacker.Earth / defender.Earth)
                                  / defender.Earth_Defense / defender.All_Element_Defense;
                    break;
                case ElementType.Dark:
                    effectValue = effectValue * (attacker.Dark / defender.Dark)
                                  / defender.Dark_Defense / defender.All_Element_Defense;
                    break;
                case ElementType.Chaos:
                    break;
            }
            return effectValue;
        }

        /// <summary>
        /// 对己效果计算
        /// </summary>
        /// <param name="effectValue">效果数值</param>
        /// <param name="elementType">效果元素类型</param>
        /// <param name="attacker">自己</param>
        public float EffectToSelfCaculation(float effectValue, ElementType elementType, FightHero attacker)
        {
            switch (elementType)
            {
                case ElementType.Water:
                    effectValue = effectValue * (attacker.Water / 80);
                    break;
                case ElementType.Fire:
                    effectValue = effectValue * (attacker.Fire / 80);
                    break;
                case ElementType.Thunder:
                    effectValue = effectValue * (attacker.Thunder / 80);
                    break;
                case ElementType.Wind:
                    effectValue = effectValue * (attacker.Wind / 80);
                    break;
                case ElementType.Earth:
                    effectValue = effectValue * (attacker.Earth / 80);
                    break;
                case ElementType.Dark:
                    effectValue = effectValue * (attacker.Dark / 80);
                    break;
            }
            return effectValue;
        }


    }




}
