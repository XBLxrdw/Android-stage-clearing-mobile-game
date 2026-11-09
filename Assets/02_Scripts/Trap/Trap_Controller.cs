using UnityEngine;

public class Trap_Controller : MonoBehaviour
{
    private Rigidbody2D rig;
    private Vector2 Vector2;
    private void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        if (transform.position.x > 0)
        {
            Vector2 = Quaternion.Euler(0, 0, Random.Range(30, 50)) * Vector2.left;
        }
        else
        {
            Vector2 = Quaternion.Euler(0, 0, Random.Range(-30, -50)) * Vector2.right;
        }
        
    }
    private void Update()
    {
        if(Mathf.Abs(transform.position.x) > 8)
        {
            Destroy(gameObject);
        }
    }
    private void FixedUpdate()
    {
        rig.linearVelocity = Vector2 * Random.Range(3, 7);
    }

}
