using System.Collections;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// 摄像机控制
/// </summary>
public class Camera_Management_System : MonoBehaviour
{
    private Transform thistransform;
    public Image image;
    private Coroutine deathCoroutine;
    private int deathSequenceVersion;
    void Awake()
    {
        thistransform = GetComponent<Transform>();
    }
    void Start()
    {
    }
    void Update()
    {
        if (Player_Data.Instance.isMove)
        {
            thistransform.position = new Vector3(thistransform.position.x, Player_Data.Instance.playerTransform.position.y - 3f, -10);
        }
    }
    /// <summary>
    /// 玩家通关后摄像机效果
    /// </summary>
    public void PlayerWinSetCamera()
    {
        thistransform.position = new Vector3(thistransform.position.x, Player_Data.Instance.playerTransform.position.y, -10);
    }
    /// <summary>
    /// 玩家死亡后摄像机效果
    /// </summary>
    public void PlayerDieSetCamera(System.Action onFinished = null)
    {
        StopDeathSequence();
        deathCoroutine = StartCoroutine(DeathSequence(onFinished));
    }
    public void StopDeathSequence()
    {
        deathSequenceVersion++;
        if (deathCoroutine != null)
        {
            StopCoroutine(deathCoroutine);
            deathCoroutine = null;
        }
        if (image != null)
        {
            Color c = image.color;
            c.a = 0f;
            image.color = c;
        }
        if (Player_Data.Instance != null && Player_Data.Instance.playerTransform != null)
        {
            thistransform.position = new Vector3(thistransform.position.x, Player_Data.Instance.playerTransform.position.y - 3f, -10f);
        }
    }
    IEnumerator DeathSequence(System.Action onFinished)
    {
        int version = ++deathSequenceVersion;
        float timer= 0;
        Vector3 startPos = thistransform.position;
        Vector3 targetPos = new Vector3(startPos.x, startPos.y + 5f, startPos.z);
        Color color = image.color;
        while (timer < 2f)
        {
            if (version != deathSequenceVersion) yield break;
            timer += Time.deltaTime;
            float progress = timer / 2;
            thistransform.position = Vector3.Lerp(startPos, targetPos, progress);
            color.a = Mathf.Lerp(0f, 1f, progress);
            image.color = color;
            yield return null;
        }
        if (version != deathSequenceVersion) yield break;
        color.a = 1f;
        image.color = color;
        if (onFinished != null)
        {
            onFinished();
        }
        if (version != deathSequenceVersion) yield break;
        if (Player_Data.Instance != null && Player_Data.Instance.playerTransform != null)
        {
            thistransform.position = new Vector3(startPos.x, Player_Data.Instance.playerTransform.position.y - 3f, -10f);
        }
        timer = 0;
        while (timer < 0.5f)
        {
            if (version != deathSequenceVersion) yield break;
            timer += Time.deltaTime;
            color.a = Mathf.Lerp(1f, 0f, timer / 0.5f);
            image.color = color;
            yield return null;
        }
        color.a = 0f;
        image.color = color;
        if (version == deathSequenceVersion) deathCoroutine = null;
    }
}
