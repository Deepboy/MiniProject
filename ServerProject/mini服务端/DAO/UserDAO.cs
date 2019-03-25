using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using GameServer.Model;

namespace GameServer.DAO
{
    class UserDAO
    {
        //判断是否存在该用户
        public User VerifyUser(MySqlConnection conn, string username, string password)
        {
            MySqlDataReader reader = null;
            try
            {
                MySqlCommand cmd = new MySqlCommand("select * from user where username = @username and password = @password", conn);
                cmd.Parameters.AddWithValue("username", username);
                cmd.Parameters.AddWithValue("password", password);
                reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    int id = reader.GetInt32("id");
                    User user = new User(id, username, password);
                    return user;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("在VerifyUser的时候出现异常：" + e);
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
            }
            return null;
        }
        //是否存在相同角色名
        public bool HasSameUsername(MySqlConnection conn, string username)
        {
            MySqlDataReader reader = null;
            try
            {
                MySqlCommand cmd = new MySqlCommand("select * from user where username = @username", conn);
                cmd.Parameters.AddWithValue("username", username);
                reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("在GetUserByUsername的时候出现异常：" + e);
                return false;
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
            }
        }

        //添加用户
        public bool AddUser(MySqlConnection conn, string username, string password)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand("insert into user set username=@username , password = @password", conn);
                cmd.Parameters.AddWithValue("username", username);
                cmd.Parameters.AddWithValue("password", password);
                cmd.ExecuteNonQuery();//返回受影响的记录数据条数
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("在AddUser的时候出现异常：" + e);
            }
            return false;
        }

        public bool AddUserInfo(MySqlConnection conn, int user_id, string username)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand("insert into user_info set username=@username , user_id = @user_id", conn);
                cmd.Parameters.AddWithValue("username", username);
                cmd.Parameters.AddWithValue("user_id", user_id);
                cmd.ExecuteNonQuery();//返回受影响的记录数据条数
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("在AddUserInfo的时候出现异常：" + e);
            }
            return false;
        }


        //获取角色信息
        public UserInfo GetUserInfoByUserId(MySqlConnection conn, int user_id) {
            MySqlDataReader reader = null;
            try
            {
                MySqlCommand cmd = new MySqlCommand("select * from user_info where user_id = @user_id", conn);
                cmd.Parameters.AddWithValue("user_id", user_id);
                reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    int id = reader.GetInt32("id");
                    string userName = reader.GetString("username");
                    int user_Exp = reader.GetInt32("user_Exp");
                    int user_ReserveExp = reader.GetInt32("user_ReserveExp");
                    int userLevel = reader.GetInt32("user_level");
                    int level_limit = reader.GetInt32("level_limit");
                    string goods = reader.GetString("goods");
                    string card = reader.GetString("card");
                    int challenge = reader.GetInt32("challenge");
                    int task_id = reader.GetInt32("task_id");
                    int minecraft = reader.GetInt32("minecraft");
                    int phase = reader.GetInt32("user_phase");

                    UserInfo userInfo = new UserInfo(id, user_id, userName,user_Exp,user_ReserveExp, userLevel,level_limit, goods, card, challenge, task_id,phase, minecraft, "");

                    return userInfo;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("在GetUserInfo的时候出现异常：" + e);
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
            }
            return null;
        }

        public void UpdateUserInfo(MySqlConnection conn, UserInfo userInfo)
        {
            try
            {

                MySqlCommand cmd = new MySqlCommand("update user_info set minecraft=@minecraft, username=@username ,user_level=@user_level,goods=@goods,challenge=@challenge,task_id=@task_id,card=@card,user_Exp=@user_Exp,user_ReserveExp=@user_ReserveExp,user_phase=@user_phase where user_id=@user_id", conn);

                cmd.Parameters.AddWithValue("username", userInfo.Username);
                cmd.Parameters.AddWithValue("user_level", userInfo.User_level);
                cmd.Parameters.AddWithValue("goods", userInfo.Goods);
                cmd.Parameters.AddWithValue("challenge", userInfo.Challenge);
                cmd.Parameters.AddWithValue("task_id", userInfo.Task_id);
                cmd.Parameters.AddWithValue("card", userInfo.Card);
                cmd.Parameters.AddWithValue("user_Exp", userInfo.User_Exp);
                cmd.Parameters.AddWithValue("user_ReserveExp", userInfo.User_ReserveExp);
                cmd.Parameters.AddWithValue("user_phase", userInfo.User_phase);
                cmd.Parameters.AddWithValue("minecraft", userInfo.Minecraft);
                cmd.Parameters.AddWithValue("user_id", userInfo.User_id);
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Console.WriteLine("在UpdateOrAddResult的时候出现异常：" + e);
            }
        }

    }
}
