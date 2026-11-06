using UnityEngine;

public class CameraManagementSystem : MonoBehaviour
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
