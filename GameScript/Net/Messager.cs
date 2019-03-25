using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Text;
using System.Linq;
using Common;


public class Messager
{
    /// <summary>
    /// 定义：
    /// data：接受数据的存储数组
    /// startIndex：从哪里开始存储的索引，也是已经存取了多少字节的数据在数组里面的标志
    /// 方法：
    /// Data：get方法，获取data
    /// StartIndex：get方法，获取startIndex
    /// RemainSize：get方法，获取data数组中剩余存放数据的空间
    /// ReadMessage：解析服务器传来的数据
    /// PackData:对要发送的消息进行加工打包
    /// 用于：
    /// 数据的解析以及加工打包
    /// </summary>

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

    public void ReadMessage(int NewDataAmount, Action<ActionCode, string> processDataCallBack)
    {
        startIndex += NewDataAmount;
        while (true)
        {
            //一条消息的解析
            if (startIndex < 4) return;
            int count = BitConverter.ToInt32(data, 0);
            //判断剩下的数据是否齐全
            if ((startIndex - 4) >= count)
            {
                //string s = Encoding.UTF8.GetString(data, 4, count);
                //Console.WriteLine("解析出来的数据：" + s);
                ActionCode actionCode = (ActionCode)BitConverter.ToInt32(data, 4);
                //ActionCode actionCode = (ActionCode)BitConverter.ToInt32(data, 8);
                string s = Encoding.UTF8.GetString(data, 8, count - 4);
                processDataCallBack(actionCode, s);
                Array.Copy(data, count + 4, data, 0, startIndex - count - 4);
                startIndex -= count + 4;
            }
            else
            {
                break;
            }
        }
    }

    //数据组拼打包
    public static byte[] PackData(RequestCode requestCode, ActionCode actionCode, string data)
    {
        byte[] requestCodeBytes = BitConverter.GetBytes((int)requestCode);
        byte[] actionCodeBytes = BitConverter.GetBytes((int)actionCode);
        byte[] dataBytes = Encoding.UTF8.GetBytes(data);
        int dataAmount = requestCodeBytes.Length + dataBytes.Length + actionCodeBytes.Length;
        byte[] dataAmountBytes = BitConverter.GetBytes(dataAmount);
        return dataAmountBytes.Concat(requestCodeBytes).ToArray()
            .Concat(actionCodeBytes).ToArray()
            .Concat(dataBytes).ToArray();
    }

}
