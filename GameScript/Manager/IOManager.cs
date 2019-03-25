using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using System.IO;


public class IOManager:BaseManager{
    public IOManager(GameFacade facade) : base(facade) { }

    //private string CardPath = Application.dataPath + "/Resources/Information/cardInfo.txt";
    //private string GoodsPath = Application.dataPath + "/Resources/Information/goodsInfo.txt";
    public override void OnInit()
    {
        base.OnInit();
        //ReadCard();
        //ReadGoods();
    }



    //public void ReadCard()
    //{
    //    StreamReader sr = new StreamReader(CardPath, Encoding.Default);
    //    String line;
    //    while ((line = sr.ReadLine()) != null)
    //    {
    //        facade.setCardInfoDict(line.ToString());
    //    }
    //}

    //public void ReadGoods() {
    //    StreamReader sr = new StreamReader(GoodsPath, Encoding.Default);
    //    String line;
    //    while ((line = sr.ReadLine()) != null)
    //    {
    //        facade.setGoodsInfoDict(line.ToString());
    //    }
    //}
}

