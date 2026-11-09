using UnityEngine;
/// <summary>
/// 音频管理器
/// </summary>
public class AudioManager : MonoBehaviour
{
    private static AudioManager audioManager;

    public static AudioManager instance
    {
        get
        {
            // 2. 判断私有变量，不会死循环
            if (audioManager == null)
            {
                audioManager = FindFirstObjectByType<AudioManager>();
            }
            return audioManager;
        }
    }

    public AudioSource bgmSource;
    public AudioSource sfxSource;
    public AudioSource skiSource;
    public AudioSource minskiSource;
    public AudioClip bgm;//背景音乐
    public AudioClip dead;//死亡
    public AudioClip fever;// 激励
    public AudioClip winSound;//胜利
    public AudioClip perfect;//完美
    public AudioClip ski;//滑雪

    private void Awake()
    {
        if (audioManager != null && audioManager != this)
        {
            Destroy(gameObject);
            return;
        }
        audioManager = this;
        DontDestroyOnLoad(gameObject); // 切换场景时音乐不断
    }

    /// <summary>
    /// 播放背景音乐
    /// </summary>
    public void PlayBGM(AudioClip clip, bool loop = true)
    {
        if (bgmSource.clip == clip) return; // 如果正在放这首歌，就不打断

        bgmSource.clip = clip;
        bgmSource.loop = loop;
        bgmSource.Play();
    }
    /// <summary>
    /// 开始播放持续滑雪音效
    /// </summary>
    public void PlayLoopSFX(AudioClip clip)
    {
        if (clip != null && skiSource != null)
        {
            if (skiSource.clip == clip && skiSource.isPlaying) return;

            skiSource.clip = clip;
            skiSource.Play();
        }
    }
    /// <summary>
    /// 开始播放按下滑雪音效
    /// </summary>
    public void PlayLoopSFX2(AudioClip clip)
    {
        if (clip != null && minskiSource != null)
        {
            if (minskiSource.clip == clip && minskiSource.isPlaying) return;

            minskiSource.clip = clip;
            minskiSource.Play();
        }
    }
    /// <summary>
    /// 停止持续音效
    /// </summary>
    public void StopLoopSFX()
    {
        if (skiSource != null && skiSource.isPlaying)
        {
            skiSource.Stop();
        }
    }
    public void StopLoopSFX2()
    {
        if (minskiSource != null && minskiSource.isPlaying)
        {
            minskiSource.Stop();
        }
    }

    /// <summary>
    /// 播放一次音效
    /// </summary>
    public void PlaySFX(AudioClip clip, float volume = 1f)
    {
        if (clip != null && sfxSource != null)
        {
            sfxSource.PlayOneShot(clip, volume);
        }
    }

    /// <summary>
    /// 停止背景音乐
    /// </summary>
    public void StopBGM()
    {
        bgmSource.Stop();
    }
    /// <summary>
    /// 关闭所有音乐和音效
    /// </summary>
    public void StopAllAudio()
    {
        AudioListener.volume = 0f;
    }
    public void PlayAllAudio()
    {
        AudioListener.volume = 1f;
    }
}