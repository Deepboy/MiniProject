using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NewFightConfig;
using UnityEngine.UI;

public class DebugPanelCtrl : MonoBehaviour
{
    private FightHero fightHero;

    //音乐设置滑动条
    private Slider SliderMusic;
    //音效设置滑动条
    private Slider SliderSoundEffect;
    //音乐
    private GameObject TextMusic;
    //音效
    private GameObject TextSoundEffect;

    private Slider HpSlider;
    private Text HpText;
    private Slider SpeedSlider;
    private Text SpeedText;
    private Slider DamageResistSlider;
    private Text DamageResistText;
    private Slider DamageParrySlider;
    private Text DamageParryText;
    private Slider ChaosDamageResistSlider;
    private Text ChaosDamageResistText;
    private Slider ChaosDamageSlider;
    private Text ChaosDamageText;
    private Slider DodgeSlider;
    private Text DodgeText;
    private Slider CriticalSlider;
    private Text CriticalText;
    private Slider CritMultipleSlider;
    private Text CritMultipleText;
    private Slider DamagePierceSlider;
    private Text DamagePierceText;
    private Slider DamageIncrementSlider;
    private Text DamageIncrementText;
    private Slider SuckBloodSlider;
    private Text SuckBloodText;
    private Slider WaterSlider;
    private Text WaterText;
    private Slider FireSlider;
    private Text FireText;
    private Slider ThunderSlider;
    private Text ThunderText;
    private Slider WindSlider;
    private Text WindText;
    private Slider EarthSlider;
    private Text EarthText;
    private Slider DarkSlider;
    private Text DarkText;
    private Slider WaterPercentSlider;
    private Text WaterPercentText;
    private Slider FirePercentSlider;
    private Text FirePercentText;
    private Slider ThunderPercentSlider;
    private Text ThunderPercentText;
    private Slider WindPercentSlider;
    private Text WindPercentText;
    private Slider EarthPercentSlider;
    private Text EarthPercentText;
    private Slider DarkPercentSlider;
    private Text DarkPercentText;
    private Button ExitButton;
    private GameObject content;
    public void Init(FightHero fightHero)
    {
        this.fightHero = fightHero;
    }

    public void UpdateData()
    {
        fightHero.OriginalHp = HpSlider.value * (int)FightHeroMaxDefine.MaxHp;
        fightHero.MaxHp = fightHero.OriginalHp;
        fightHero.CurHp = fightHero.OriginalHp;

        fightHero.OriginalSpeed = SpeedSlider.value * (int)FightHeroMaxDefine.MaxSpeed;

        fightHero.OriginalDamageResist = DamageResistSlider.value * (int)FightHeroMaxDefine.MaxDamageResist;

        fightHero.OriginalDamageParry = DamageParrySlider.value * (int)FightHeroMaxDefine.MaxDamageParry;

        fightHero.OriginalChaosDamageResistCoefficient = ChaosDamageResistSlider.value * (int)FightHeroMaxDefine.MaxChaosResistCoeficient;

        fightHero.OriginalChaosDamageCoefficient = ChaosDamageSlider.value * (int)FightHeroMaxDefine.MaxChaosDamageCoeficient;

        fightHero.OriginalDodgeCoefficient = DodgeSlider.value * (int)FightHeroMaxDefine.MaxDodgeCoeficient;

        fightHero.OriginalCriticalCoefficient = CriticalSlider.value * (int)FightHeroMaxDefine.MaxCriticalCoeficient;

        fightHero.OriginalCritMutipleCoefficient = CritMultipleSlider.value * (int)FightHeroMaxDefine.MaxCritMutipleCoeficient;

        fightHero.OriginalDamagePierceCoefficient = DamagePierceSlider.value * (int)FightHeroMaxDefine.MaxDamagePierceCoeficient;

        fightHero.OriginalDamageIncrementValue = DamageIncrementSlider.value * (int)FightHeroMaxDefine.MaxDamageIncrementValue;

        fightHero.OriginalSuckBloodCoefficient = SuckBloodSlider.value * (int)FightHeroMaxDefine.MaxSuckBloodCoeficient;

        fightHero.OriginalWaterStrength = WaterSlider.value * (int)FightHeroMaxDefine.MaxWaterStrength;

        fightHero.OriginalFireStrength = FireSlider.value * (int)FightHeroMaxDefine.MaxFireStrength;

        fightHero.OriginalThunderStrength = ThunderSlider.value * (int)FightHeroMaxDefine.MaxThunderStrenght;

        fightHero.OriginalWindStrength = WindSlider.value * (int)FightHeroMaxDefine.MaxWindStrength;

        fightHero.OriginalEarthStrength = EarthSlider.value * (int)FightHeroMaxDefine.MaxEarthStrength;

        fightHero.OriginalDarkStrength = DarkSlider.value * (int)FightHeroMaxDefine.MaxDarkStrength;

        fightHero.CardGroup.GetWaterPercent = WaterPercentSlider.value * (int)FightHeroMaxDefine.MaxWaterCardPerent;

        fightHero.CardGroup.GetFirePercent = FirePercentSlider.value * (int)FightHeroMaxDefine.MaxFireCardPerent;

        fightHero.CardGroup.GetThunderPercent = ThunderPercentSlider.value * (int)FightHeroMaxDefine.MaxThunderCardPerent;

        fightHero.CardGroup.GetWindPercent = WindPercentSlider.value * (int)FightHeroMaxDefine.MaxWindCardPerent;

        fightHero.CardGroup.GetEarthPercent = EarthPercentSlider.value * (int)FightHeroMaxDefine.MaxEarthCardPerent;

        fightHero.CardGroup.GetDarkPercent = DarkPercentSlider.value * (int)FightHeroMaxDefine.MaxDarkCardPerent;
    }


    private void OnClick()
    {
        UpdateData();
        FightCtrl.winButton.gameObject.SetActive(true);
        FightCtrl.Tips_Button.gameObject.SetActive(true);
        FightCtrl.SliderMusic.gameObject.SetActive(true);
        FightCtrl.SliderSoundEffect.gameObject.SetActive(true);
        FightCtrl.TextSoundEffect.SetActive(true);
        FightCtrl.TextMusic.SetActive(true);
        this.gameObject.SetActive(false);
    }

    private void Awake()
    {
        content=  GameObject.Find("Canvas/settingPanel").transform.Find("PanelDebug").GetChild(0).GetChild(0).GetChild(0).gameObject;
        HpSlider = content.transform.GetChild(0).GetChild(1).gameObject.GetComponent<Slider>();
        HpText = content.transform.GetChild(0).GetChild(2).gameObject.GetComponent<Text>();

        SpeedSlider = content.transform.GetChild(1).GetChild(1).gameObject.GetComponent<Slider>();
        SpeedText = content.transform.GetChild(1).GetChild(2).gameObject.GetComponent<Text>();

        DamageResistSlider = content.transform.GetChild(2).GetChild(1).gameObject.GetComponent<Slider>();
        DamageResistText = content.transform.GetChild(2).GetChild(2).gameObject.GetComponent<Text>();

        DamageParrySlider = content.transform.GetChild(3).GetChild(1).gameObject.GetComponent<Slider>();
        DamageParryText = content.transform.GetChild(3).GetChild(2).gameObject.GetComponent<Text>();

        ChaosDamageResistSlider = content.transform.GetChild(4).GetChild(1).gameObject.GetComponent<Slider>();
        ChaosDamageResistText = content.transform.GetChild(4).GetChild(2).gameObject.GetComponent<Text>();

        ChaosDamageSlider = content.transform.GetChild(5).GetChild(1).gameObject.GetComponent<Slider>();
        ChaosDamageText = content.transform.GetChild(5).GetChild(2).gameObject.GetComponent<Text>();

        DodgeSlider = content.transform.GetChild(6).GetChild(1).gameObject.GetComponent<Slider>();
        DodgeText = content.transform.GetChild(6).GetChild(2).gameObject.GetComponent<Text>();

        CriticalSlider = content.transform.GetChild(7).GetChild(1).gameObject.GetComponent<Slider>();
        CriticalText = content.transform.GetChild(7).GetChild(2).gameObject.GetComponent<Text>();

        CritMultipleSlider = content.transform.GetChild(8).GetChild(1).gameObject.GetComponent<Slider>();
        CritMultipleText = content.transform.GetChild(8).GetChild(2).gameObject.GetComponent<Text>();

        DamagePierceSlider = content.transform.GetChild(9).GetChild(1).gameObject.GetComponent<Slider>();
        DamagePierceText = content.transform.GetChild(9).GetChild(2).gameObject.GetComponent<Text>();

        DamageIncrementSlider = content.transform.GetChild(10).GetChild(1).gameObject.GetComponent<Slider>();
        DamageIncrementText = content.transform.GetChild(10).GetChild(2).gameObject.GetComponent<Text>();

        SuckBloodSlider = content.transform.GetChild(11).GetChild(1).gameObject.GetComponent<Slider>();
        SuckBloodText = content.transform.GetChild(11).GetChild(2).gameObject.GetComponent<Text>();

        WaterSlider = content.transform.GetChild(12).GetChild(1).gameObject.GetComponent<Slider>();
        WaterText = content.transform.GetChild(12).GetChild(2).gameObject.GetComponent<Text>();

        FireSlider = content.transform.GetChild(13).GetChild(1).gameObject.GetComponent<Slider>();
        FireText = content.transform.GetChild(13).GetChild(2).gameObject.GetComponent<Text>();

        ThunderSlider = content.transform.GetChild(14).GetChild(1).gameObject.GetComponent<Slider>();
        ThunderText = content.transform.GetChild(14).GetChild(2).gameObject.GetComponent<Text>();

        WindSlider = content.transform.GetChild(15).GetChild(1).gameObject.GetComponent<Slider>();
        WindText = content.transform.GetChild(15).GetChild(2).gameObject.GetComponent<Text>();

        EarthSlider = content.transform.GetChild(16).GetChild(1).gameObject.GetComponent<Slider>();
        EarthText = content.transform.GetChild(16).GetChild(2).gameObject.GetComponent<Text>();

        DarkSlider = content.transform.GetChild(17).GetChild(1).gameObject.GetComponent<Slider>();
        DarkText = content.transform.GetChild(17).GetChild(2).gameObject.GetComponent<Text>();

        WaterPercentSlider = content.transform.GetChild(18).GetChild(1).gameObject.GetComponent<Slider>();
        WaterPercentText = content.transform.GetChild(18).GetChild(2).gameObject.GetComponent<Text>();

        FirePercentSlider = content.transform.GetChild(19).GetChild(1).gameObject.GetComponent<Slider>();
        FirePercentText = content.transform.GetChild(19).GetChild(2).gameObject.GetComponent<Text>();

        ThunderPercentSlider = content.transform.GetChild(20).GetChild(1).gameObject.GetComponent<Slider>();
        ThunderPercentText = content.transform.GetChild(20).GetChild(2).gameObject.GetComponent<Text>();

        WindPercentSlider = content.transform.GetChild(21).GetChild(1).gameObject.GetComponent<Slider>();
        WindPercentText = content.transform.GetChild(21).GetChild(2).gameObject.GetComponent<Text>();

        EarthPercentSlider = content.transform.GetChild(22).GetChild(1).gameObject.GetComponent<Slider>();
        EarthPercentText = content.transform.GetChild(22).GetChild(2).gameObject.GetComponent<Text>();

        DarkPercentSlider = content.transform.GetChild(23).GetChild(1).gameObject.GetComponent<Slider>();
        DarkPercentText = content.transform.GetChild(23).GetChild(2).gameObject.GetComponent<Text>();

        ExitButton = transform.GetChild(1).gameObject.GetComponent<Button>();
        ExitButton.onClick.AddListener(OnClick);

    }

    private void OnEnable()
    {
        FightCtrl.DebugSettingPanelInit();

        HpSlider.value = fightHero.CurHp / (int)FightHeroMaxDefine.MaxHp;
        HpText.text = (HpSlider.value * (int)FightHeroMaxDefine.MaxHp).ToString();

        SpeedSlider.value = fightHero.CurSpeed / (int)FightHeroMaxDefine.MaxSpeed;
        SpeedText.text = (SpeedSlider.value * (int)FightHeroMaxDefine.MaxSpeed).ToString();

        DamageResistSlider.value = fightHero.CurDamageResist / (int)FightHeroMaxDefine.MaxDamageResist;
        DamageResistText.text = (DamageResistSlider.value * (int)FightHeroMaxDefine.MaxDamageResist).ToString();

        DamageParrySlider.value = fightHero.CurDamageParry / (int)FightHeroMaxDefine.MaxDamageParry;
        DamageParryText.text = (DamageParrySlider.value * (int)FightHeroMaxDefine.MaxDamageParry).ToString();

        ChaosDamageResistSlider.value = fightHero.CurChaosDamageResistCoefficient / (int)FightHeroMaxDefine.MaxChaosResistCoeficient;
        ChaosDamageResistText.text = (ChaosDamageResistSlider.value * (int)FightHeroMaxDefine.MaxChaosResistCoeficient).ToString();

        ChaosDamageSlider.value = fightHero.CurChaosDamageCoefficient / (int)FightHeroMaxDefine.MaxChaosDamageCoeficient;
        ChaosDamageText.text = (ChaosDamageSlider.value * (int)FightHeroMaxDefine.MaxChaosDamageCoeficient).ToString();

        DodgeSlider.value = fightHero.CurDodgeCoefficient / (int)FightHeroMaxDefine.MaxDodgeCoeficient;
        DodgeText.text = (DodgeSlider.value * (int)FightHeroMaxDefine.MaxDodgeCoeficient).ToString();

        CriticalSlider.value = fightHero.CurCriticalCoefficient / (int)FightHeroMaxDefine.MaxCriticalCoeficient;
        CriticalText.text = (CriticalSlider.value * (int)FightHeroMaxDefine.MaxCriticalCoeficient).ToString();

        CritMultipleSlider.value = fightHero.CurCritMutipleCoefficient / (int)FightHeroMaxDefine.MaxCritMutipleCoeficient;
        CritMultipleText.text = (CritMultipleSlider.value * (int)FightHeroMaxDefine.MaxCritMutipleCoeficient).ToString();

        DamagePierceSlider.value = fightHero.CurDamagePierceCoefficient / (int)FightHeroMaxDefine.MaxDamagePierceCoeficient;
        DamagePierceText.text = (DamagePierceSlider.value * (int)FightHeroMaxDefine.MaxDamagePierceCoeficient).ToString();

        DamageIncrementSlider.value = fightHero.CurDamageIncrementValue / (int)FightHeroMaxDefine.MaxDamageIncrementValue;
        DamageIncrementText.text = (DamageIncrementSlider.value * (int)FightHeroMaxDefine.MaxDamageIncrementValue).ToString();

        SuckBloodSlider.value = fightHero.CurSuckBloodCoefficient / (int)FightHeroMaxDefine.MaxSuckBloodCoeficient;
        SuckBloodText.text = (SuckBloodSlider.value * (int)FightHeroMaxDefine.MaxSuckBloodCoeficient).ToString();

        WaterSlider.value = fightHero.CurWaterStrength / (int)FightHeroMaxDefine.MaxWaterStrength;
        WaterText.text = (WaterSlider.value * (int)FightHeroMaxDefine.MaxWaterStrength).ToString();

        FireSlider.value = fightHero.CurFireStrength / (int)FightHeroMaxDefine.MaxFireStrength;
        FireText.text = (FireSlider.value * (int)FightHeroMaxDefine.MaxFireStrength).ToString();

        ThunderSlider.value = fightHero.CurThunderStrength / (int)FightHeroMaxDefine.MaxThunderStrenght;
        ThunderText.text = (ThunderSlider.value * (int)FightHeroMaxDefine.MaxThunderStrenght).ToString();

        WindSlider.value = fightHero.CurWindStrength / (int)FightHeroMaxDefine.MaxWindStrength;
        WindText.text = (WindSlider.value * (int)FightHeroMaxDefine.MaxWindStrength).ToString();

        EarthSlider.value = fightHero.CurEarthStrength / (int)FightHeroMaxDefine.MaxEarthStrength;
        EarthText.text = (EarthSlider.value * (int)FightHeroMaxDefine.MaxEarthStrength).ToString();

        DarkSlider.value = fightHero.CurDarkStrength / (int)FightHeroMaxDefine.MaxDarkStrength;
        DarkText.text = (DarkSlider.value * (int)FightHeroMaxDefine.MaxDarkStrength).ToString();

        WaterPercentSlider.value = fightHero.CardGroup.GetWaterPercent / (int)FightHeroMaxDefine.MaxWaterCardPerent;
        WaterPercentText.text = (WaterPercentSlider.value * (int)FightHeroMaxDefine.MaxWaterCardPerent).ToString();

        FirePercentSlider.value = fightHero.CardGroup.GetFirePercent / (int)FightHeroMaxDefine.MaxFireCardPerent;
        FirePercentText.text = (FirePercentSlider.value * (int)FightHeroMaxDefine.MaxFireCardPerent).ToString();

        ThunderPercentSlider.value = fightHero.CardGroup.GetThunderPercent / (int)FightHeroMaxDefine.MaxThunderCardPerent;
        ThunderPercentText.text = (ThunderPercentSlider.value * (int)FightHeroMaxDefine.MaxThunderCardPerent).ToString();

        WindPercentSlider.value = fightHero.CardGroup.GetWindPercent / (int)FightHeroMaxDefine.MaxWindCardPerent;
        WindPercentText.text = (WindPercentSlider.value * (int)FightHeroMaxDefine.MaxWindCardPerent).ToString();

        EarthPercentSlider.value = fightHero.CardGroup.GetEarthPercent / (int)FightHeroMaxDefine.MaxEarthCardPerent;
        EarthPercentText.text = (EarthPercentSlider.value * (int)FightHeroMaxDefine.MaxEarthCardPerent).ToString();

        DarkPercentSlider.value = fightHero.CardGroup.GetDarkPercent / (int)FightHeroMaxDefine.MaxDarkCardPerent;
        DarkPercentText.text = (DarkPercentSlider.value * (int)FightHeroMaxDefine.MaxDarkCardPerent).ToString();
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        HpText.text = (HpSlider.value * (int)FightHeroMaxDefine.MaxHp).ToString();

        SpeedText.text = (SpeedSlider.value * (int)FightHeroMaxDefine.MaxSpeed).ToString();

        DamageResistText.text = (DamageResistSlider.value * (int)FightHeroMaxDefine.MaxDamageResist).ToString();

        DamageParryText.text = (DamageParrySlider.value * (int)FightHeroMaxDefine.MaxDamageParry).ToString();

        ChaosDamageResistText.text = (ChaosDamageResistSlider.value * (int)FightHeroMaxDefine.MaxChaosResistCoeficient).ToString();

        ChaosDamageText.text = (ChaosDamageSlider.value * (int)FightHeroMaxDefine.MaxChaosDamageCoeficient).ToString();

        DodgeText.text = (DodgeSlider.value * (int)FightHeroMaxDefine.MaxDodgeCoeficient).ToString();

        CriticalText.text = (CriticalSlider.value * (int)FightHeroMaxDefine.MaxCriticalCoeficient).ToString();

        CritMultipleText.text = (CritMultipleSlider.value * (int)FightHeroMaxDefine.MaxCritMutipleCoeficient).ToString();

        DamagePierceText.text = (DamagePierceSlider.value * (int)FightHeroMaxDefine.MaxDamagePierceCoeficient).ToString();

        DamageIncrementText.text = (DamageIncrementSlider.value * (int)FightHeroMaxDefine.MaxDamageIncrementValue).ToString();

        SuckBloodText.text = (SuckBloodSlider.value * (int)FightHeroMaxDefine.MaxSuckBloodCoeficient).ToString();

        WaterText.text = (WaterSlider.value * (int)FightHeroMaxDefine.MaxWaterStrength).ToString();

        FireText.text = (FireSlider.value * (int)FightHeroMaxDefine.MaxFireStrength).ToString();

        ThunderText.text = (ThunderSlider.value * (int)FightHeroMaxDefine.MaxThunderStrenght).ToString();

        WindText.text = (WindSlider.value * (int)FightHeroMaxDefine.MaxWindStrength).ToString();

        EarthText.text = (EarthSlider.value * (int)FightHeroMaxDefine.MaxEarthStrength).ToString();

        DarkText.text = (DarkSlider.value * (int)FightHeroMaxDefine.MaxDarkStrength).ToString();

        WaterPercentText.text = (WaterPercentSlider.value * (int)FightHeroMaxDefine.MaxWaterCardPerent).ToString();

        FirePercentText.text = (FirePercentSlider.value * (int)FightHeroMaxDefine.MaxFireCardPerent).ToString();

        ThunderPercentText.text = (ThunderPercentSlider.value * (int)FightHeroMaxDefine.MaxThunderCardPerent).ToString();

        WindPercentText.text = (WindPercentSlider.value * (int)FightHeroMaxDefine.MaxWindCardPerent).ToString();

        EarthPercentText.text = (EarthPercentSlider.value * (int)FightHeroMaxDefine.MaxEarthCardPerent).ToString();

        DarkPercentText.text = (DarkPercentSlider.value * (int)FightHeroMaxDefine.MaxDarkCardPerent).ToString();
    }
}
