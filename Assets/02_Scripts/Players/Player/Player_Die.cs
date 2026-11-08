using UnityEngine;
/// <summary>
/// 玩家死亡判断
/// </summary>
public class Player_Die : MonoBehaviour
{
    void Start()
    {
    }
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Slime") && !Player_Data.Instance.isInvincible)
        {
            Player_Data.Instance.isDead = true;
        }
    }
}
