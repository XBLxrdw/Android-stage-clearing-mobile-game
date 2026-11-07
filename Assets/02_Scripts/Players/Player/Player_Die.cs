using UnityEngine;

public class Player_Die : MonoBehaviour
{
    public Player_Data data;//玩家数据层
    void Start()
    {
        data = GetComponentInParent<Player_Data>();
    }
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("玩家死亡");
        if (other.CompareTag("Slime"))
        {
            data.isDead = true;
        }
    }
}
