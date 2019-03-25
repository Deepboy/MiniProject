using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Common;

public class ExitGamePanel : BasePanel {

	// Use this for initialization
	public void Start () {
        transform.Find("SureQuit_Button").GetComponent<Button>().onClick.AddListener(OnSureQuitClick);
        transform.Find("CloseButton").GetComponent<Button>().onClick.AddListener(OnCloseClick);
    }
	
	
    private void OnSureQuitClick() {
        facade.PlayUIAudio("close");
        Application.Quit();
    }

    private void OnCloseClick() {
        facade.PlayUIAudio("close");
        uiMng.PushPanel(UIPanelType.Login);
    }

    public override void OnEnter()
    {
        base.OnEnter();
        gameObject.SetActive(true);
    }

    public override void OnPause()
    {
        base.OnPause();
        gameObject.SetActive(false);
    }

    public override void OnResume()
    {
        base.OnResume();
        gameObject.SetActive(true);
    }

    public override void OnExit()
    {
        base.OnExit();
        gameObject.SetActive(false);
    }

}
