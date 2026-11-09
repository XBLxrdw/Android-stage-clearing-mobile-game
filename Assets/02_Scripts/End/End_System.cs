using System.Runtime.ConstrainedExecution;
using TMPro;
using UnityEngine;

public class End_System : MonoBehaviour
{
    public GameObject win;
    private void Awake()
    {
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Player_Data.Instance.isWin = true;
            GameObject a = Instantiate(win,new Vector2(transform.position.x-3,transform.position.y), Quaternion.identity);
            GameObject b = Instantiate(win, new Vector2(transform.position.x + 3, transform.position.y), Quaternion.identity);
            Destroy(a, 5);
            Destroy(b, 5);
        }
    }
}
