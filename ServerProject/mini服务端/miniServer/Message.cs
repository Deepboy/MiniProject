using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;

namespace GameServer.miniServer
{
    public class Message
    {
        private byte[] data = new byte[1024];
        private int startIndex = 0;

        //返回存放的数组
        public byte[] Data
        {
            get { return data; }
        }

        //已经存取了多少字节的数据在数组里面
        public int StartIndex
        {
            get { return startIndex; }
        }

        //data数组中剩余存放数据的空间
        public int RemainSize
        {
            get { return data.Length - startIndex; }
        }

        public void ReadMessage(int NewDataAmount, Action<RequestCode, ActionCode, string> processDataCallBack)
        {
            startIndex += NewDataAmount;
            while (true)
            {
                if (startIndex < 4) return;
                int count = BitConverter.ToInt32(data, 0);
                if ((startIndex - 4) >= count)
                {
                    //string s = Encoding.UTF8.GetString(data, 4, count);
                    //Console.WriteLine("解析出来的数据：" + s);
                    RequestCode requestCode = (RequestCode)BitConverter.ToInt32(data, 4);
                    ActionCode actionCode = (ActionCode)BitConverter.ToInt32(data, 8);
                    string s = Encoding.UTF8.GetString(data, 12, count - 8);
                    processDataCallBack(requestCode, actionCode, s);
                    Array.Copy(data, count + 4, data, 0, startIndex - count - 4);
                    startIndex -= count + 4;
                }
                else
                {
                    break;
                }
            }
        }

        //打包数据
        public static byte[] PackData(ActionCode actionCode, string data)
        {
            byte[] requestCodeBytes = BitConverter.GetBytes((int)actionCode);
            byte[] dataBytes = Encoding.UTF8.GetBytes(data);
            int dataAmount = requestCodeBytes.Length + dataBytes.Length;
            byte[] dataAmountBytes = BitConverter.GetBytes(dataAmount);
            return dataAmountBytes.Concat(requestCodeBytes).ToArray().Concat(dataBytes).ToArray();
        }


    }
}
