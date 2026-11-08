using TMPro;
using UnityEngine;

public class ScoreText : MonoBehaviour
{
    public TextMeshProUGUI textMesh;
    public float floatSpeed = 0.2f;//速度
    private Camera mainCam;
    private Vector3 worldPosition;//世界坐标位置
    private Canvas parentCanvas;

    void Start()
    {
        mainCam = Camera.main;
        textMesh = GetComponent<TextMeshProUGUI>();
        parentCanvas = GetComponentInParent<Canvas>();
        Destroy(gameObject, 1);
    }
    /// <summary>
    /// 生成分数函数
    /// </summary>
    /// <param 分数="damage"></param>
    /// <param 史莱姆位置="targetWorldPos"></param>
    // 外部调用：传入伤害值和怪物的世界坐标
    public void Setup(int damage, Vector3 targetWorldPos)
    {
        textMesh.text = damage.ToString();
        worldPosition = targetWorldPos + new Vector3(0, 0.2f, 0);
        worldPosition += new Vector3(Random.Range(-0.3f, 0.3f), Random.Range(0f, 0.3f), 0);
    }

    void Update()
    {
        worldPosition += Vector3.up * floatSpeed * Time.deltaTime;
        Vector2 screenPos = mainCam.WorldToScreenPoint(worldPosition);
        RectTransform canvasRect = parentCanvas.GetComponent<RectTransform>();
        Vector2 localPos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRect,screenPos,parentCanvas.renderMode == RenderMode.ScreenSpaceOverlay ? null : mainCam,out localPos);
        transform.localPosition = localPos;
    }
}
