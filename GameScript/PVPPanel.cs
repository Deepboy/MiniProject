using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PVPPanel : BasePanel
{

    private Transform choiceFightPabel;
    private Transform canvas;

    private void Start()
    {
        facade.PlayBGM("pvp");
        canvas = GameObject.Find("Canvas").transform;
        choiceFightPabel = canvas.Find("ChoiceFightPanel").transform;
        transform.Find("BG/ExitButton").GetComponent<Button>().onClick.AddListener(OnExitBtnClick);
        transform.Find("BG/ChoiceHeroButton").GetComponent<Button>().onClick.AddListener(OnChoiceCardBtnClick);
    }

    private void OnExitBtnClick() {
        facade.PlayUIAudio("close");
        LoadAsyncScene.nextSceneName = "Main_Scene";
        SceneManager.LoadScene("Middle_Scene");
    }

    private void OnChoiceCardBtnClick() {
        facade.PlayUIAudio("open");
        choiceFightPabel.gameObject.SetActive(true);
    }

}
