using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.Sockets;
using System.Threading;
using System;
using Common;
using System.Net;

public class ClientManager : BaseManager
{
    /// <summary>
    /// 定义：
    /// IP：服务器IP地址，用于链接服务器
    /// PORT：对应的IP地址中的
    /// clientSocket：建立自身的socket，用于与服务器socket连接，发送消息等
    /// msg：Message类，用于处理消息（包括将要发送的消息和接受到的服务器的消息）
    /// 方法：
    /// ClientManager：持有facade，方便调用其他Manager的函数
    /// OnInit：初始化函数，建立socket，并连接服务器
    /// Start：开始函数，启动客户端socket接受消息
    /// ReceiveCallBack：收到消息的回调函数
    /// OnProcessDataCallBack：msg读取消息后的回调函数，通过facade传给其他Manager处理
    /// SendRequest：发送请求
    /// OnDestroy：关闭客户端
    /// 用于：
    /// 管理跟服务器端的Socket连接
    /// </summary>

    private const string IP = "127.0.0.1";



    private const int PORT = 6688;
    private Socket clientSocket;
    private Messager msg = new Messager();

    public ClientManager(GameFacade facade) : base(facade) { }

    //建立连接
    public override void OnInit()
    {
        base.OnInit();

        clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        try
        {
            Thread th = new Thread(waiteClient);
            th.Start();

        }
        catch (Exception e)
        {
            Debug.LogWarning("无法连接到服务器 ，请检查您的网络！！" + e);
        }
    }

    private void waiteClient()
    {
        IAsyncResult connResult = clientSocket.BeginConnect(IP, PORT, null, null);
        connResult.AsyncWaitHandle.WaitOne(20000, true);  //等待2秒
        if (!clientSocket.Connected)
        {
            Debug.Log("网络连接失败！");
            clientSocket.Close();
            //处理连接不成功的动作
        }
        else
        {
            //处理连接成功的动作
            Start();
        }
    }

    private void Start()
    {
        clientSocket.BeginReceive(msg.Data, msg.StartIndex, msg.RemainSize, SocketFlags.None, ReceiveCallBack, null);
    }

    //接收数据
    private void ReceiveCallBack(IAsyncResult ar)
    {
        try
        {
            if (clientSocket == null || clientSocket.Connected == false) return;
            int count = clientSocket.EndReceive(ar);

            msg.ReadMessage(count, OnProcessDataCallBack);

            Start();
        }
        catch (Exception e)
        {
            Debug.Log(e);
        }
    }

    //回调方法
    private void OnProcessDataCallBack(ActionCode actionCode, string data)
    {
        //转给RequestManager处理
        facade.HandleResponse(actionCode, data);
    }

    //发送
    public void SendRequest(RequestCode requestCode, ActionCode actionCode, string data)
    {
        Debug.Log("发送");
        byte[] bytes = Messager.PackData(requestCode, actionCode, data);
        clientSocket.Send(bytes);
    }

    public override void OnDestroy()
    {
        base.OnDestroy();
        try
        {
            clientSocket.Close();
        }
        catch (Exception e)
        {
            Debug.LogWarning("无法关闭连接" + e);
        }
    }
}
