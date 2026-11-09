using UnityEngine;

/// <summary>
/// 设置面板控制系统（带存档）
/// 同一时刻每对开关只显示一个按钮，显示哪一个完全由存档状态决定。
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
    private bool lastMoving;

    private void Start()
    {
        // 用面板的真实状态初始化 isShow。
        // 之前 isShow 固定为 false，而面板在场景里是打开的，
        // 导致第一次点击只是把 true 又设一遍，看起来"没反应"。
        isShow = settingsPanel.activeSelf;

        lastMoving = Player_Data.Instance != null && Player_Data.Instance.isMove;

        // 按存档决定该显示"开"还是"关"按钮，避免四个按钮同时出现
        RefreshFromSave();
    }

    private void Update()
    {
        if (Player_Data.Instance == null) return;

        // 只在移动状态切换的那一帧处理，避免每帧反复 SetActive
        bool moving = Player_Data.Instance.isMove;
        if (moving == lastMoving) return;
        lastMoving = moving;

        if (moving)
        {
            SetToggleButtonsActive(false);
        }
        else if (isShow)
        {
            // 停下后按存档恢复正确的互斥按钮
            RefreshFromSave();
        }
    }

    public void OnClick()
    {
        isShow = !isShow;

        // 打开面板时按存档刷新两对开关，保证显示的是当前真实状态
        if (isShow) RefreshFromSave();

        settingsPanel.SetActive(isShow);
    }

    /// <summary>
    /// 四个按钮整体显隐，仅用于玩家移动时隐藏。
    /// 注意：不要用它做"初始化"，否则会破坏开/关按钮的互斥显示。
    /// </summary>
    private void SetToggleButtonsActive(bool active)
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

        // 互斥：当前是"开"就显示"关闭"按钮，当前是"关"就显示"开启"按钮
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
    /// <summary>
    /// 按存档刷新两对开关的显示状态（GameFlow 读档后也会调用）
    /// </summary>
    public void RefreshFromSave()
    {
        ApplySoundState(SaveData.Instance.isOnSound, playAudio: false);
        ApplyEncourageState(SaveData.Instance.isOnEncourage);
    }
}
