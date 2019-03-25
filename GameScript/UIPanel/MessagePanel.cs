using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessagePanel : BasePanel {

    private string msg = null;
    
    public override void OnEnter()
    {
        uiMng.SetUIMsgPanel(this);
        transform.GetComponent<Text>().enabled=false;
        base.OnEnter();
    }

    private void Update()
    {
        if (msg != null) {
            transform.GetComponent<Text>().enabled = true;
            transform.GetComponent<Text>().text = msg;
            Invoke("Hide",3f);
            msg = null;
        }
    }

    private void Hide() {
        transform.GetComponent<Text>().enabled = false;
    }

    public void ShowMessageSync(string msg) {
        this.msg = msg;
    }
}
