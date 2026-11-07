using System.Collections;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// 摄像机控制
/// </summary>
public class Camera_Management_System : MonoBehaviour
{
    private Player_Data data;
    private Transform thistransform;
    public Image image;
    void Awake()
    {
        data = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Data>();
        thistransform = GetComponent<Transform>();
    }
    void Start()
    {
    }
    void Update()
    {
        if (data.isMove)
        {
            thistransform.position = new Vector3(thistransform.position.x, data.playerTransform.position.y - 3f, -10);
        }
    }
    public void PlayerDieSetCamera()
    {
        StartCoroutine(DeathSequence());
    }
    IEnumerator DeathSequence()
    {
        float timer= 0;
        Vector3 startPos = thistransform.position;
        Vector3 targetPos = new Vector3(startPos.x, startPos.y + 5f, startPos.z);
        while (timer < 2f)
        {
            timer += Time.deltaTime;
            float progress = timer / 2;
            thistransform.position = Vector3.Lerp(startPos, targetPos, progress);
            Color color = image.color;
            color.a = Mathf.Lerp(0f, 1f, progress);
            image.color = color;
            yield return null;
        }
    }
}
