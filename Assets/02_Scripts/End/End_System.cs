using UnityEngine;

public class End_System : MonoBehaviour
{
    private void Awake()
    {
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Player_Data.Instance.isWin = true;
        }
    }
}
