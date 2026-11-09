using System;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdManager : MonoBehaviour, IUnityAdsInitializationListener, IUnityAdsLoadListener, IUnityAdsShowListener
{
    [SerializeField] private string gameId = "3260921"; // 你的 Game ID
    [SerializeField] private string placementId = "Must001"; // 你的 Placement Name
    [SerializeField] private bool testMode = true; // 学习阶段建议开启测试模式

    public static AdManager Instance;

    // ★ 统一用一个带 bool 的回调变量
    private Action<bool> onAdResultCallback;
    private bool isShowing;
    public bool IsShowing { get { return isShowing; } }

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        if (Advertisement.isSupported && !Advertisement.isInitialized)
        {
            Advertisement.Initialize(gameId, testMode, this);
        }
    }

    // ================= 初始化回调 =================
    public void OnInitializationComplete()
    {
        Debug.Log("Unity Ads 初始化完成，开始加载广告");
        Advertisement.Load(placementId, this);
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Debug.LogError($"初始化失败: {error} - {message}");
    }


    // ================= 加载回调 =================
    public void OnUnityAdsAdLoaded(string adUnitId)
    {
        Debug.Log("广告加载成功，等待玩家点击按钮播放");
    }

    public void OnUnityAdsFailedToLoad(string adUnitId, UnityAdsLoadError error, string message)
    {
        Debug.LogError($"广告加载失败: {error} - {message}");
        if (isShowing) return;
        Action<bool> callback = onAdResultCallback;
        onAdResultCallback = null;
        callback?.Invoke(false);
    }


    // ================= 播放与回调 =================
    // 提供给复活按钮调用
    public void ShowRewardedAd(Action<bool> onResult)
    {
        if (isShowing)
        {
            Debug.Log("广告正在播放，忽略重复请求");
            return;
        }
        if (!Advertisement.isInitialized)
        {
            Debug.LogError("广告尚未初始化，无法播放");
            onResult?.Invoke(false);
            return;
        }
        isShowing = true;
        onAdResultCallback = onResult;
        Advertisement.Show(placementId, this);
    }

    public void OnUnityAdsShowComplete(string adUnitId, UnityAdsShowCompletionState showCompletionState)
    {
        isShowing = false;
        bool success = showCompletionState == UnityAdsShowCompletionState.COMPLETED;
        if (success)
        {
            Debug.Log("玩家看完了广告，发放奖励！");
        }
        else
        {
            Debug.Log("玩家没有看完广告（跳过或关闭），不发放奖励。");
        }
        Action<bool> callback = onAdResultCallback;
        onAdResultCallback = null;
        callback?.Invoke(success);
        Advertisement.Load(placementId, this);
    }

    public void OnUnityAdsShowFailure(string adUnitId, UnityAdsShowError error, string message)
    {
        isShowing = false;
        Debug.LogError($"广告展示失败: {error} - {message}");
        Action<bool> callback = onAdResultCallback;
        onAdResultCallback = null;
        callback?.Invoke(false);
    }

    public void OnUnityAdsShowStart(string adUnitId) { }
    public void OnUnityAdsShowClick(string adUnitId) { }
}
