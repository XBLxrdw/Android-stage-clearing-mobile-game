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
    void Start()
    {
        size = transform.localScale;
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();//获取玩家控制层
        if (mainCanvas == null || mainCanvas.renderMode == RenderMode.WorldSpace)
        {
            mainCanvas = FindTextCanvas();
        }
        childSr.color = Player_Data.Instance.color2;

    }
    /// <summary>
    /// 查找分数飘字使用的叠层画布
    /// </summary>
    private Canvas FindTextCanvas()
    {
        Canvas[] all = FindObjectsByType<Canvas>(FindObjectsSortMode.None);
        GameObject target = GameObject.Find(textCanvasName);
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
