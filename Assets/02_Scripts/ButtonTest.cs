using UnityEngine;

/// <summary>
/// 设置面板控制系统（带存档）
/// </summary>
public class ButtonTest : MonoBehaviour
{
    [Header("设置面板总开关")]
    public GameObject settingsPanel;
    [Header("声音互斥按钮")]
    public GameObject btnSoundOn;
    public GameObject btnSoundOff;
    [Header("鼓励语互斥按钮")]
    public GameObject btnEncourageOn;
    public GameObject btnEncourageOff;

    private bool isShow;

    private void Start()
    {
        settingsPanel.SetActive(false);
        SetButtonsActive(false);
    }
    private void Update()
    {
        if (Player_Data.Instance.isMove)
        {
            settingsPanel.SetActive(false);
            SetButtonsActive(false);
        }
    }
    public void OnClick()
    {
        isShow = !isShow;
        settingsPanel.SetActive(isShow);
        SetButtonsActive(isShow);
    }
    private void SetButtonsActive(bool active)
    {
        btnSoundOn.SetActive(active);
        btnSoundOff.SetActive(active);
        btnEncourageOn.SetActive(active);
        btnEncourageOff.SetActive(active);
    }
    public void OnClickSoundOn()
    {
        ApplySoundState(true, playAudio: true);
        SaveSettings();
    }
    public void OnClickSoundOff()
    {
        ApplySoundState(false, playAudio: true);
        SaveSettings();
    }

    private void ApplySoundState(bool soundOn, bool playAudio)
    {
        SaveData.Instance.isOnSound = soundOn;

        btnSoundOn.SetActive(!soundOn);
        btnSoundOff.SetActive(soundOn);

        if (playAudio)
        {
            if (soundOn) AudioManager.instance.PlayAllAudio();
            else AudioManager.instance.StopAllAudio();
        }
    }
    public void OnClickEncourageOn()
    {
        ApplyEncourageState(true);
        SaveSettings();
    }

    public void OnClickEncourageOff()
    {
        ApplyEncourageState(false);
        SaveSettings();
    }

    private void ApplyEncourageState(bool encourageOn)
    {
        SaveData.Instance.isOnEncourage = encourageOn;

        btnEncourageOn.SetActive(!encourageOn);
        btnEncourageOff.SetActive(encourageOn);
    }
    private void SaveSettings()
    {
        var manager = FindAnyObjectByType<Save_Manager>();
        manager.SaveGame(SaveData.Instance);
    }
    public void RefreshFromSave()
    {
        ApplySoundState(SaveData.Instance.isOnSound, playAudio: false);
        ApplyEncourageState(SaveData.Instance.isOnEncourage);
    }
}