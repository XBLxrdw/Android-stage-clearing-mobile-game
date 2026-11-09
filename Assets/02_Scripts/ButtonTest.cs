using UnityEngine;

public class ButtonTest : MonoBehaviour
{
    [Header("设置按钮（常驻，不会被隐藏）")]
    public GameObject settingsPanel;
    [Header("声音互斥按钮")]
    public GameObject btnSoundOn;
    public GameObject btnSoundOff;
    [Header("鼓励语互斥按钮")]
    public GameObject btnEncourageOn;
    public GameObject btnEncourageOff;

    private bool isShow = true;
    private bool lastMoving;

    private void Start()
    {
        if (settingsPanel != null) settingsPanel.SetActive(true);

        lastMoving = Player_Data.Instance != null && Player_Data.Instance.isMove;
        isShow = !lastMoving;

        ApplyOptionsVisibility();
    }

    private void Update()
    {
        if (Player_Data.Instance == null) return;

        bool moving = Player_Data.Instance.isMove;
        if (moving == lastMoving) return;
        lastMoving = moving;

        ApplyOptionsVisibility();
    }

    public void OnClick()
    {
        isShow = !isShow;
        ApplyOptionsVisibility();
    }

    private void ApplyOptionsVisibility()
    {
        bool canShow = isShow && (Player_Data.Instance == null || !Player_Data.Instance.isMove);

        if (canShow)
        {
            RefreshFromSave();
        }
        else
        {
            SetToggleButtonsActive(false);
        }
    }

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
