using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using System.Reflection;
using GameServer.miniServer;

namespace GameServer.Controller
{
    class ControllerManager
    {
        private Dictionary<RequestCode, BaseController> controllerDict = new Dictionary<RequestCode, BaseController>();
        private Server server;

        public ControllerManager(Server server)
        {
            this.server = server;
            InitController();
        }

        private void InitController()
        {
            controllerDict.Add(RequestCode.None, new DefaultController());
            controllerDict.Add(RequestCode.User, new UserController());
            controllerDict.Add(RequestCode.Room, new RoomController());
        }

        public void HandleRequest(RequestCode requestCode, ActionCode actionCode, string data, Client client)
        {
            //通过RequestCode找到对应的controller
            BaseController controller;
            //查找到之后赋给out controller，并返回是否成功
            bool isGet = controllerDict.TryGetValue(requestCode, out controller);
            if (isGet == false)
            {
                Console.WriteLine("无法得到" + requestCode + "所对应的controller，无法处理请求");
                return;
            }
            //反射调用using System.Reflection;
            //转换方法名，将枚举类型(值类型)转化为字符串类型
            string methodname = Enum.GetName(typeof(ActionCode), actionCode);
            //.GetType()取得自身的类型，.GetMethod（）取个类中间的某个方法信息
            MethodInfo mi = controller.GetType().GetMethod(methodname);
            if (mi == null)
            {
                Console.WriteLine("警告：在Controller[" + controller.GetType() + "]中没有对应的处理方法：[" + methodname + "]");
                return;
            }
            //mi方法（即actionCode方法）参数合并为object[]
            object[] parameters = new object[] { data, client, server };
            //方法信息.Invoke（哪个controller下的方法（RequestCode对应的controller），object[]参数）
            object o = mi.Invoke(controller, parameters);
            if (o == null || string.IsNullOrEmpty(o as string))
            {
                return;
            }
            server.SendResponse(client, actionCode, o as string);
        }
    }
}
