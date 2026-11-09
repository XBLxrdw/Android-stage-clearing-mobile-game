using TMPro;
using UnityEngine;

public class ScoreText : MonoBehaviour
{
    public TextMeshProUGUI textMesh;
    public float floatSpeed = 0.2f;

    private Camera mainCam;
    private Vector3 worldPosition;
    private Canvas parentCanvas;

    void Awake()
    {
        mainCam = Camera.main;
        textMesh = GetComponent<TextMeshProUGUI>();
        parentCanvas = GetComponentInParent<Canvas>();
    }

    void Start()
    {
        Destroy(gameObject, 1);
    }

    public void Setup(int damage, Vector3 targetWorldPos)
    {
        textMesh.text = damage.ToString();
        worldPosition = targetWorldPos + new Vector3(0, 0.2f, 0);
    }

    void Update()
    {
        worldPosition += Vector3.up * floatSpeed * Time.deltaTime;
        Vector3 screenPos = mainCam.WorldToScreenPoint(worldPosition);
        if (parentCanvas != null && parentCanvas.renderMode == RenderMode.ScreenSpaceOverlay)
        {
            transform.position = screenPos;
        }
        else
        {
            RectTransform canvasRect = parentCanvas.GetComponent<RectTransform>();
            Vector2 localPos;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRect,screenPos,parentCanvas.worldCamera,out localPos);
            transform.localPosition = localPos;
        }
    }
}