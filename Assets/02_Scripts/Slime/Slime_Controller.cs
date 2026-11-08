using System.Collections;
using UnityEngine;
/// <summary>
/// 史莱姆数据处理层
/// </summary>
public class Slime_Controller : MonoBehaviour
{
    public Object vfx;//特效
    private PlayerController playerScript;
    private Vector3 size;//大小
    private float duration;//变大持续时间
    public GameObject TextPrefab; //分数显示
    public Canvas mainCanvas;
    void Start()
    {
        size = transform.localScale;
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();//获取玩家控制层
    }

    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerScript.SetSco();//计算分数
            Canvas mainCanvas = FindFirstObjectByType<Canvas>();
            GameObject go = Instantiate(TextPrefab, mainCanvas.transform);
            ScoreText dt = go.GetComponent<ScoreText>();
            dt.Setup(Player_Data.Instance.boostSco, transform.position);
            StartCoroutine(ChangeSize());//启用协程播放效果
            Object.Instantiate(vfx, transform.position, Quaternion.identity,transform);//播放特效
        }
        if (other.CompareTag("Die"))
        {
            Player_Data.Instance.isDead = true;
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
