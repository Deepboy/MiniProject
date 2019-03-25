using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AssistSkillCtrl : MonoBehaviour
{
    private GameObject skillGo;
    private Text skillText;
    private Text skillTextChild;
    private GameObject skillTextImage;
    public Skill skill;


    /// <summary>
    /// 指针移入事件
    /// </summary>
    /// <param name="go"></param>
    public void OnMouseEnter(GameObject gameObject)
    {
        skillText.gameObject.SetActive(true);
        skillText.text = skill.Skill_describe;
        skillTextChild.text = skillText.text;
        skillGo.GetComponent<RectTransform>().localPosition = new Vector3(200, 550 - 20 * (skill.Skill_describe.Length / 10 - 1), 0);
    }

    /// <summary>
    /// 指针移出事件
    /// </summary>
    /// <param name="go"></param>
    public void OnMouseExit(GameObject gameObject)
    {
        skillGo.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);
        skillText.gameObject.SetActive(false);
    }

    public void UpdateCoolingTime(float curCoolingTime, float maxCoolingTime)
    {
        gameObject.transform.GetChild(0).GetComponent<Image>().fillAmount = 1 - curCoolingTime / maxCoolingTime;
    }

    // Use this for initialization
    void Start()
    {
        skillGo = gameObject.transform.parent.transform.Find("TextSkill").gameObject;
        skillText = skillGo.GetComponent<Text>();
        skillTextChild = skillGo.transform.Find("Text").GetComponent<Text>();
        skillTextImage = skillGo.transform.Find("Image").gameObject;
        EventTriggerListener.GetComponent(this.gameObject).onEnter = OnMouseEnter;
        EventTriggerListener.GetComponent(this.gameObject).onExit = OnMouseExit;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
