using System.Collections;
using UnityEngine;
/// <summary>
/// 史莱姆数据处理层
/// </summary>
public class Slime_Controller : MonoBehaviour
{
    public Object vfx;//特效
    private PlayerController playerScript;
    private Vector2 size;//大小
    private float duration;//变大持续时间
    public GameObject TextPrefab; //分数显示
    public Canvas mainCanvas;
    public SpriteRenderer childSr;
    [SerializeField] private string textCanvasName = "TextUI";//分数显示使用的画布
    // 分数飘字画布全局只查找一次，避免每个史莱姆在 Start 里遍历全场景
    private static Canvas s_textCanvas;

    void Start()
    {
        size = transform.localScale;

        // 玩家引用优先取单例，避免 150 个史莱姆各自 GameObject.Find
        playerScript = PlayerController.Instance;
        if (playerScript == null)
            playerScript = FindFirstObjectByType<PlayerController>();

        // 画布：预制体已配置就直接复用，否则仅第一次查找并缓存
        if (mainCanvas == null || mainCanvas.renderMode == RenderMode.WorldSpace)
        {
            if (s_textCanvas == null || s_textCanvas.renderMode == RenderMode.WorldSpace)
                s_textCanvas = FindTextCanvas(textCanvasName);
            mainCanvas = s_textCanvas;
        }
        else
        {
            s_textCanvas = mainCanvas;
        }

        childSr.color = Player_Data.Instance.color2;

    }
    /// <summary>
    /// 查找分数飘字使用的叠层画布（静态，只应调用一次）
    /// </summary>
    private static Canvas FindTextCanvas(string canvasName)
    {
        GameObject target = GameObject.Find(canvasName);
        if (target != null)
        {
            Canvas c = target.GetComponent<Canvas>();
            if (c == null) c = target.GetComponentInParent<Canvas>();
            if (c == null) c = target.GetComponentInChildren<Canvas>();
            if (c != null && c.renderMode != RenderMode.WorldSpace) return c;
        }
        return null;
    }
    void Update()
    {
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            AudioManager.instance.PlaySFX(AudioManager.instance.fever);
            EncouragementText.Instance?.ShowRandom();
            playerScript.SetSco();//计算分数
            if (mainCanvas != null)
            {
                GameObject go = Instantiate(TextPrefab, mainCanvas.transform);
                go.transform.localScale = Vector3.one;
                ScoreText dt = go.GetComponent<ScoreText>();
                if (dt != null)
                {
                    dt.Setup(Player_Data.Instance.boostSco, transform.position);
                }
            }
            StartCoroutine(ChangeSize());//启用协程播放效果
            Object.Instantiate(vfx, transform.position, Quaternion.identity,transform);//播放特效
        }
    }
    IEnumerator ChangeSize()
    {
        duration = 0;
        while (duration < 0.3f)
        {
            duration += Time.deltaTime;
            transform.localScale = Vector3.Lerp(size, size * 1.5f, duration / 0.3f);
            yield return null;
        }
        transform.localScale = size * 1.5f;
        duration = 0;
        while (duration < 0.3f)
        {
            duration += Time.deltaTime;
            transform.localScale = Vector3.Lerp(size * 1.5f, size, duration / 0.3f);
            yield return null;
        }
    }
}
