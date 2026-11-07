using UnityEngine;

public class Player_XVF_Die : MonoBehaviour
{
    private void Start()
    {
        Destroy(this.gameObject, 1f);
    }
}
