using System.Runtime.CompilerServices;
using Unity.Android.Gradle.Manifest;
using UnityEngine;
/// <summary>
/// 特效生成器
/// </summary>
public class TrailVFXSpawner : MonoBehaviour
{
    public GameObject vfxPrefab; //星星预设体
    public GameObject vfxMove;//转弯烟雾
    private GameObject vfx;
    public float spawnInterval = 0.1f; // 生成间隔
    private float timer;
    private void Start()
    {
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            timer = 0;
            if (Player_Data.Instance.isBoost &&Player_Data.Instance.isMove)
            {
                GameObject vfx2 = Instantiate(vfxPrefab, transform.position, Quaternion.identity);
                Destroy(vfx2, 1f);
            }
        }
        if (vfx != null)
            vfx.transform.position = transform.position;
        if (Player_Data.Instance.isMove&&Player_Data.Instance.isStartInput)
        {
            if (vfx == null)
            {
                vfx = Instantiate(vfxMove, transform.position, Quaternion.identity);
            }
            if (Player_Data.Instance.dir.x > 0)
            {
                vfx.transform.localScale = new Vector3(-1, 1, 1);
            }
            else if (Player_Data.Instance.dir.x < 0)
            {
                vfx.transform.localScale = new Vector3(1, 1, 1);
            }
        }
        else if(Player_Data.Instance.isStopInput)
        {
            if (vfx != null)
            {
                Destroy(vfx);
            }
        }
    }
}
