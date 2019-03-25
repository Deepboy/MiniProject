using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
public class BGPanel : BasePanel {
   // private Image BG;
    public Sprite[] LogoSprites;
    public Button SkipButton;
    private VideoPlayer videoPlayer;
    private RawImage rawImage;

    void Start () {

        this.GetComponent<Button>().onClick.AddListener(ButtonSkipOnClick);
        videoPlayer = this.GetComponent<VideoPlayer>();
        rawImage = this.GetComponent<RawImage>();
        StartCoroutine("LoginShowLogo");
    }
    
   

    private void ButtonSkipOnClick()
    {
        StopAllCoroutines();
        videoPlayer.Prepare();
        videoPlayer.clip = Resources.Load<VideoClip>("Opening/opening");
        uiMng.PushPanel(UIPanelType.Login);
        facade.PlayBGM("Login");
        this.GetComponent<Button>().interactable = false;
    }

    public IEnumerator LoginShowLogo()
    {
        while (videoPlayer.isPlaying)
        {
            yield return null;
        }
        videoPlayer.Prepare();
        videoPlayer.clip = Resources.Load<VideoClip>("Opening/opening");
        videoPlayer.Play();
        yield return new WaitForSeconds(2);
        uiMng.PushPanel(UIPanelType.Login);
        facade.PlayBGM("Login");
        this.GetComponent<Button>().interactable = false;

    }
   
    
    void Update () {
        if (videoPlayer.texture == null)
        {
            return;
        }
        rawImage.texture = videoPlayer.texture;
    }
}

