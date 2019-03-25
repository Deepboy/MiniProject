using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : BaseManager
{
    //BGM
    private AudioSource BGM;
    //技能音效
    private AudioSource Skill;
    //出牌特效音效
    private AudioSource DrawCard;
    //UI音效
    private AudioSource UI;

    //BGM音量
    private float BGMVolumn;
    //技能音效音量
    private float SkillVolumn;
    //出牌特效音效音量
    private float DrawCardVolumn;
    //UI音效音量
    private float UIVolumn;

    //BGM路径
    private static string BGMPath = "Audio/BGM/";
    //UI路径
    private static string UIPath = "Audio/UI/";
    //技能特效路径
    private static string SkillEffectPath = "Audio/SkillEffect/";
    //出牌特效路径
    private static string DrawCardPath = "Audio/DrawCard/";

    public AudioManager(GameFacade facade) : base(facade) { }

    public override void OnInit()
    {
        base.OnInit();
        BGM = facade.gameObject.AddComponent<AudioSource>();
        Skill = facade.gameObject.AddComponent<AudioSource>();
        DrawCard = facade.gameObject.AddComponent<AudioSource>();
        UI = facade.gameObject.AddComponent<AudioSource>();
        BGMVolumn = 0.5f;
        SkillVolumn = 0.5f;
        DrawCardVolumn = 0.5f;
        UIVolumn = 0.5f;
    }

    //LoopClip.clip = audioClip;
    //LoopClip.volume = volume;
    //LoopClip.loop = true;
    //LoopClip.pitch = 1f;

    /// <summary>
    /// 播放指定音效
    /// </summary>
    /// <param name="audioSource">音效组件</param>
    /// <param name="audioClipPath">音效路径</param>
    /// <param name="volumn">音量</param>
    /// <param name="loop">是否循环</param>
    /// <param name="delayTime">延迟播放</param>
    private void PlayAudioClip(AudioSource audioSource, string audioClipPath, float volumn, bool loop, float delayTime = 0f)
    {
        if(audioSource == null)
        {
            Debug.Log("音效组件初始化错误！");
            return;
        }
        AudioClip audioClip = Resources.Load<AudioClip>(audioClipPath) == null ? null : Resources.Load<AudioClip>(audioClipPath);
        if (audioClip == null)
        {
            Debug.Log("音效路径错误！");
            return;
        }
        audioSource.clip = audioClip;
        audioSource.volume = volumn;
        audioSource.loop = loop;
        audioSource.pitch = 1f;
        audioSource.PlayDelayed(delayTime);
    }

    /// <summary>
    /// 播放BGM
    /// </summary>
    /// <param name="path">路径</param>
    /// <param name="loop">是否循环</param>
    public void PlayBGM(string path, bool loop = true)
    {
        PlayAudioClip(BGM, BGMPath + path, BGMVolumn, loop);
    }

    /// <summary>
    /// 设置BGM音量
    /// </summary>
    /// <param name="volumn"></param>
    public void SetBGMVolumn(float volumn)
    {
        if(volumn < 0)
        {
            volumn = 0;
        }
        BGMVolumn = volumn;
        if(BGM.isPlaying)
        {
            BGM.volume = volumn;
        }
    }

    /// <summary>
    /// 获得BGM音量
    /// </summary>
    /// <returns></returns>
    public float GetBGMVolumn()
    {
        return BGMVolumn;
    }

    /// <summary>
    /// 暂停BGM
    /// </summary>
    public void BGMPause()
    {
        if(BGM.isPlaying)
        {
            BGM.Pause();
        }
    }

    /// <summary>
    /// BGM恢复播放
    /// </summary>
    public void BGMReplay()
    {
        BGM.Play();
    }

    /// <summary>
    /// BGM停止播放
    /// </summary>
    public void BGMStop()
    {
        BGM.Stop();
    }

    /// <summary>
    /// 返回正在播放的BGM
    /// </summary>
    /// <returns></returns>
    public string BGMName()
    {
        return BGM.clip.name;
    }

    /// <summary>
    /// 播放技能音效
    /// </summary>
    /// <param name="path"></param>
    public void PlaySkillAudio(string path, float delayTime = 0f)
    {
        PlayAudioClip(Skill, SkillEffectPath + path, SkillVolumn, false, delayTime);
    }

    /// <summary>
    /// 设置技能音效音量
    /// </summary>
    /// <param name="volumn"></param>
    public void SetSkillAudioVolumn(float volumn)
    {
        if (volumn < 0)
        {
            volumn = 0;
        }
        SkillVolumn = volumn;
        if (Skill.isPlaying)
        {
            Skill.volume = volumn;
        }
    }

    /// <summary>
    /// 获得技能音效音量
    /// </summary>
    /// <returns></returns>
    public float GetSkillAudioVolumn()
    {
        return SkillVolumn;
    }

    /// <summary>
    /// 暂停技能音效
    /// </summary>
    public void SkillAudioPause()
    {
        if (Skill.isPlaying)
        {
            Skill.Pause();
        }
    }

    /// <summary>
    /// 技能音效恢复播放
    /// </summary>
    public void SkillAudioReplay()
    {
        Skill.Play();
    }

    /// <summary>
    /// 播放出牌特效音效
    /// </summary>
    /// <param name="path"></param>
    public void PlayerDrawCardAudio(string path)
    {
        PlayAudioClip(DrawCard, DrawCardPath + path, DrawCardVolumn, false);
    }

    /// <summary>
    /// 设置出牌特效音效音量
    /// </summary>
    /// <param name="volumn"></param>
    public void SetDrawCardAudioVolumn(float volumn)
    {
        if (volumn < 0)
        {
            volumn = 0;
        }
        DrawCardVolumn = volumn;
        if (DrawCard.isPlaying)
        {
            DrawCard.volume = volumn;
        }
    }

    /// <summary>
    /// 获得出牌特效音效音量
    /// </summary>
    /// <returns></returns>
    public float GetDrawCardAudioVolumn()
    {
        return DrawCardVolumn;
    }

    /// <summary>
    /// 暂停出牌特效音效
    /// </summary>
    public void DrawCardAudioPause()
    {
        if (DrawCard.isPlaying)
        {
            DrawCard.Pause();
        }
    }

    /// <summary>
    /// 出牌特效音效恢复播放
    /// </summary>
    public void DrawCardAudioReplay()
    {
        DrawCard.Play();
    }

    /// <summary>
    /// 播放UI音效
    /// </summary>
    /// <param name="path"></param>
    public void PlayUIAudio(string path)
    {
        PlayAudioClip(UI, UIPath + path, UIVolumn, false);
    }

    /// <summary>
    /// 设置UI音效音量
    /// </summary>
    /// <param name="volumn"></param>
    public void SetUIAudioVolumn(float volumn)
    {
        if (volumn < 0)
        {
            volumn = 0;
        }
        UIVolumn = volumn;
        if (UI.isPlaying)
        {
            UI.volume = volumn;
        }
    }

    /// <summary>
    /// 获得UI音效音量
    /// </summary>
    /// <returns></returns>
    public float GetUIAudioVolumn()
    {
        return UIVolumn;
    }
}
