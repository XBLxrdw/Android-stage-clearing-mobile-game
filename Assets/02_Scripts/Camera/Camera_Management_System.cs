using UnityEngine;
/// <summary>
/// 摄像机控制
/// </summary>
public class Camera_Management_System : MonoBehaviour
{
    private Player_Data data;
    private Transform thistransform;
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
        thistransform.position = new Vector3(thistransform.position.x, data.playerTransform.position.y - 3f, -10);
    }
}
