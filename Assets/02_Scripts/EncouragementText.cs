using System.Collections;
using TMPro;
using UnityEngine;

/// <summary>
/// 鼓励语生成器
/// </summary>
public class EncouragementText : MonoBehaviour
{
    private static readonly string[] DefaultPhrases =
    {
        "干得好！", "厉害！", "漂亮！", "帅气！", "精彩！",
        "超厉害！", "大师级操作！", "完美无缺！", "惊天动地！", "简直不可能!",
        "雪地之神！", "不可超越！", "你征服了大山！", "神之手！", "萌王附体！"
    };

    private static EncouragementText instance;
    public static EncouragementText Instance
    {
        get
        {
            if (instance == null) instance = FindFirstObjectByType<EncouragementText>();
            return instance;
        }
    }

    [Tooltip("显示鼓励语的 Text")]
    [SerializeField] private TMP_Text targetText;

    [Tooltip("自定义鼓励语")]
    [SerializeField] private string[] phrases;

    [SerializeField] private float showDuration = 1.2f;
    [Tooltip("同一句不连续出现两次")]
    [SerializeField] private bool avoidRepeat = true;
    [Tooltip("两次播放的最小间隔")]
    [SerializeField] private float minInterval = 0.25f;
    [Tooltip("开始时先显示一句")]
    [SerializeField] private bool showOnStart = false;

    [Header("开关")]
    [Tooltip("当前是否开启鼓励语")]
    [SerializeField] private bool enableEncouragement = true;
    [Tooltip("勾选后开关状态会被记住，下次启动保持上次的选择")]
    [SerializeField] private bool rememberSetting = true;
    private const string PrefsKey = "EncouragementEnabled";

    private int lastIndex = -1;
    private float lastShowTime = -999f;
    private Coroutine hideCoroutine;

    /// <summary>当前是否开启</summary>
    public bool IsEnabled => enableEncouragement;

    private void Awake()
    {
        if (instance == null) instance = this;
        if (phrases == null || phrases.Length == 0) phrases = DefaultPhrases;
        if (rememberSetting)
        {
            enableEncouragement = PlayerPrefs.GetInt(PrefsKey, enableEncouragement ? 1 : 0) == 1;
        }
        Clear();
    }

    private void Start()
    {
        if (showOnStart) ShowRandom();
    }

    /// <summary>
    ///开启鼓励语
    /// </summary>
    public void OpenEncouragement()
    {
        enableEncouragement = true;
        SetTextVisible(true);
        Clear();

        if (rememberSetting)
        {
            PlayerPrefs.SetInt(PrefsKey, 1);
            PlayerPrefs.Save();
        }
    }

    /// <summary>
    ///关闭鼓励语
    /// </summary>
    public void CloseEncouragement()
    {
        enableEncouragement = false;
        StopHide();
        Clear();
        SetTextVisible(false);

        if (rememberSetting)
        {
            PlayerPrefs.SetInt(PrefsKey, 0);
            PlayerPrefs.Save();
        }
    }

    /// <summary>
    ///一个按钮做切换
    /// </summary>
    public void ToggleEncouragement()
    {
        if (enableEncouragement) CloseEncouragement();
        else OpenEncouragement();
    }

    /// <summary>随机挑一句显示</summary>
    public void ShowRandom()
    {
        if (!enableEncouragement) return;
        if (Time.time - lastShowTime < minInterval) return;
        lastShowTime = Time.time;
        Show(phrases[RandomIndex()]);
    }

    /// <summary>显示指定的一句</summary>
    public void Show(string phrase)
    {
        if (!enableEncouragement) return;
        if (targetText == null || string.IsNullOrEmpty(phrase)) return;

        targetText.text = phrase;
        SetTextVisible(true);

        if (hideCoroutine != null) StopCoroutine(hideCoroutine);
        if (showDuration > 0f) hideCoroutine = StartCoroutine(HideAfter(showDuration));
    }

    public void Clear()
    {
        if (targetText != null) targetText.text = string.Empty;
        hideCoroutine = null;
    }

    private void SetTextVisible(bool visible)
    {
        if (targetText != null) targetText.gameObject.SetActive(visible);
    }

    private void StopHide()
    {
        if (hideCoroutine != null)
        {
            StopCoroutine(hideCoroutine);
            hideCoroutine = null;
        }
    }

    private int RandomIndex()
    {
        if (phrases.Length == 1) return 0;
        int index = Random.Range(0, phrases.Length);
        if (avoidRepeat && index == lastIndex) index = (index + 1) % phrases.Length;
        lastIndex = index;
        return index;
    }

    private IEnumerator HideAfter(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        Clear();
    }
}