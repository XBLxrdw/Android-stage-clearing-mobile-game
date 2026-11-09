using TMPro;
using UnityEngine;
using UnityEngine.UI;
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
    public Slider progressBar;//路程进度条
    public Slider JourneyProgressBar;//历史路程进度条
    public TextMeshProUGUI progressBarText;//路程进度条文字
    public TextMeshProUGUI JourneyProgressBarText;//历史路程进度条文字
    public GameObject progressBarTextPanel;//路程进度条文字面板
    [Header("需要统一改色的图片 UI")]
    public Image[] imageUIs;
    public Image[] imageUIs2;//淡色

    [Header("需要统一改色的文字 UI")]
    public TextMeshProUGUI[] textUIs;
    private void Awake()
    {
        SetInitialUI();
        SetLevel();
    }
    // 缓存上一次的值，只有真正变化时才刷新 UI，避免每帧分配字符串并重建 TMP 网格
    private int lastScore = int.MinValue;
    private int lastLevel = int.MinValue;
    private int lastPercent = int.MinValue;
    private int lastBestPercent = int.MinValue;
    private bool lastJourneyVisible;
    private bool journeyInitialized;

    private void Update()
    {
        Player_Data data = Player_Data.Instance;
        if (data == null) return;

        // 分数：移动中显示本局分数，停下显示历史最高分
        int score = data.isMove ? data.currentSco : data.lastSco;
        if (score != lastScore)
        {
            scoreText.text = score.ToString();
            lastScore = score;
        }

        // 关卡数字：仅在关卡变化时刷新
        int level = SaveData.Instance.Level;
        if (level != lastLevel)
        {
            lastLevel = level;
            SetLevel();
        }

        // 当前进度：滑条每帧更新（无分配），文字只在整数百分比变化时刷新
        float cur = data.total > 0f
            ? Mathf.Clamp01(data.totalDistance / data.total)
            : 0f;
        progressBar.value = cur;

        int percent = Mathf.RoundToInt(cur * 100f);
        if (percent != lastPercent)
        {
            progressBarText.text = percent.ToString() + "%";
            lastPercent = percent;
        }

        // 历史最远进度
        float best = Mathf.Clamp01(data.maxDistancePercent);
        bool hasRecord = best > 0f;
        bool covered = data.isMove && cur >= best;
        bool show = hasRecord && !covered;

        // 只在显隐状态变化时 SetActive
        if (!journeyInitialized || show != lastJourneyVisible)
        {
            JourneyProgressBar.gameObject.SetActive(show);
            JourneyProgressBarText.gameObject.SetActive(show);
            lastJourneyVisible = show;
            journeyInitialized = true;
        }

        if (show)
        {
            JourneyProgressBar.value = best;

            int bestPercent = Mathf.RoundToInt(best * 100f);
            if (bestPercent != lastBestPercent)
            {
                JourneyProgressBarText.text = bestPercent.ToString() + "%";
                lastBestPercent = bestPercent;
            }
        }
    }
    private void SetLevel()//设置关卡数字
    {
        level[0].text = SaveData.Instance.Level.ToString();
        level[1].text = (SaveData.Instance.Level + 1).ToString();
        level[2].text = SaveData.Instance.Level.ToString();
    }
    /// <summary>
    /// 统一修改 UI 颜色
    /// </summary>
    /// <param name="newColor">目标颜色</param>
    public void SetAllUIColor(Color newColor)
    {
        foreach (Image img in imageUIs)
        {
            img.color = newColor;
        }
        foreach (Image img in imageUIs2)
        {
            Color c = newColor;
            c.r = Mathf.Clamp01(c.r + 0.3f);
            c.g = Mathf.Clamp01(c.g + 0.3f);
            c.b = Mathf.Clamp01(c.b + 0.3f);
            img.color = c;
        }
        foreach (TextMeshProUGUI txt in textUIs)
        {
            txt.color = newColor;
        }
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
    /// <summary>
    /// 广告复活按钮
    /// </summary>
    /// using UnityEngine;
}
