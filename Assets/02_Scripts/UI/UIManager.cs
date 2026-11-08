using TMPro;
using Unity.Android.Gradle.Manifest;
using UnityEngine;
/// <summary>
/// UI控制组件
/// </summary>
public class UIManager : MonoBehaviour
{
    private static UIManager uIManager;
    public static UIManager Instance
    {
        get
        {
            if (uIManager == null)
            {
                uIManager = FindFirstObjectByType<UIManager>();
            }
            return uIManager;
        }
    }
    public TextMeshProUGUI scoreText;// 分数文字
    public TextMeshProUGUI[] level;// 关卡文字
    public GameObject reviveAdPanel;// 看广告复活面板
    public TextMeshProUGUI reviveAdPanelText;// 看广告复活面板文字
    public GameObject Settings;// 设置面板
    public GameObject gameplayHint;// 游戏提示面板
    private void Awake()
    {
        SetInitialUI();
        SetLevel();
    }
    private void Update()
    {
        if (Player_Data.Instance.isMove)
        {
            scoreText.text = Player_Data.Instance.currentSco.ToString();
        }
    }
    private void SetLevel()//设置关卡数字
    {
        level[0].text = SaveData.Instance.Level.ToString();
        level[1].text = (SaveData.Instance.Level + 1).ToString();
        level[2].text = SaveData.Instance.Level.ToString();
    }
    /// <summary>
    /// 设置初始UI
    /// </summary>
    private void SetInitialUI()
    {
        reviveAdPanel.SetActive(false);
    }
    /// <summary>
    /// 显示看广告复活面板
    /// </summary>
    public void ShowReviveAdPanel()
    {
        reviveAdPanel.SetActive(true);
    }
    /// <summary>
    /// 关闭看广告复活面板
    /// </summary>
    public void CloseReviveAdPanel()
    {
        reviveAdPanel.SetActive(false);
        reviveAdPanelText.text = "";
    }
    /// <summary>
    /// 广告倒计时
    /// </summary>
    /// <param 数字="digit"></param>
    public void Countdown(int digit)
    {
        reviveAdPanelText.text = digit.ToString();
    }
    /// <summary>
    /// 开启游戏提示面板
    /// </summary>
    public void ShowGameplayHint()
    {
        gameplayHint.SetActive(true);
    }
    /// <summary>
    /// 关闭游戏提示面板
    /// </summary>
    public void CloseGameplayHint()
    {
        gameplayHint.SetActive(false);
    }

}
