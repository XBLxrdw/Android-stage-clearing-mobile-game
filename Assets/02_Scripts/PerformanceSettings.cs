using UnityEngine;
/// <summary>
/// 移动端性能初始化。
/// 设备若不加限制会跑满 GPU 导致发热降频（表现为「越玩越卡」），
/// 这里统一锁帧并切到低质量档，保证长时间稳定运行。
/// </summary>
public static class PerformanceSettings
{
    /// <summary>目标帧率。手机 60 足够流畅，比不限帧更省电、更稳定。</summary>
    public const int TargetFrameRate = 60;

    /// <summary>质量档位：0=Very Low，1=Low，2=Medium……2D 手游用 Low 即可。</summary>
    public const int QualityLevel = 1;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void Apply()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = TargetFrameRate;

        if (QualitySettings.names != null && QualitySettings.names.Length > 0)
        {
            int level = Mathf.Clamp(QualityLevel, 0, QualitySettings.names.Length - 1);
            QualitySettings.SetQualityLevel(level, true);
        }
    }
}
