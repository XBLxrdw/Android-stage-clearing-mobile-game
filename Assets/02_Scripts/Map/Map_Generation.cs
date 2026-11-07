using Unity.Mathematics;
using UnityEngine;
/// <summary>
/// 地图生成系统
/// </summary>

public class Map_Generation : MonoBehaviour
{
    public Player_Data data;
    public GameObject slime;//史莱姆预制体
    public GameObject trap;//滚石预制体
    public GameObject end;//终点预制体
    public Transform topLeft;
    public int width;
    public Transform bottomRight;
    public int height;
    [Range(0, 1f)]
    public float rgp;//可通行区域史莱姆生成概率
    [Range(0, 1f)]
    public float nrgp;//不可通行区域史莱姆生成概率
    public int seed;//种子
    public float lacunarity;//粗糙度
    [Range(0,1f)]
    public Color color;//颜色
    [Range(0, 1f)]
    public float wr;//可通行区域比例
    private bool[,] mapData;//地图数据true可通行
    void Start()
    {
        data = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Data>();
    }
    public void GenerateMap(int seed)//地图生成
    {
        bottomRight.position = new Vector3(bottomRight.position.x, UnityEngine.Random.Range(-56f, -69f), bottomRight.position.z);//后续加入关卡的计算
        width = Mathf.Abs((int)(bottomRight.position.x - topLeft.position.x));
        height = Mathf.Abs((int)(bottomRight.position.y - topLeft.position.y));
        mapData = new bool[width, height];
        float randomOffset = UnityEngine.Random.Range(-10000f, 10000f);
        for (int x = 0; x < width; x++)
        {
            for(int y = 0; y < height; y++)
            {
                float noiseValue = Mathf.PerlinNoise(x * lacunarity + randomOffset, y * lacunarity + randomOffset);
                mapData[x, y] = noiseValue < wr ? true:false ;
            }
            
        }
        CarvePath();
        GenerateSlime();
    }
    private void GenerateSlime()//史莱姆生成
    {
        DeleteMap();
        Object.Instantiate(end, new Vector3(0, bottomRight.position.y-3, 0), Quaternion.identity, transform);//终点生成，后续加入关卡的计算
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (mapData[x, y]&& UnityEngine.Random.Range(0f, 1f) < rgp)
                {
                    GameObject slimes = Object.Instantiate(slime, new Vector3(x + UnityEngine.Random.Range(-0.8f, 0.8f) + topLeft.position.x, topLeft.position.y - y + UnityEngine.Random.Range(-1f, 1f), 0), Quaternion.identity, transform);
                    float size = UnityEngine.Random.Range(0.5f, 1f);
                    slimes.transform.localScale = new Vector2(size,size);
                }
                else if (!mapData[x, y] && UnityEngine.Random.Range(0f, 1f) < nrgp)
                {
                    GameObject slimes = Object.Instantiate(slime, new Vector3(x+UnityEngine.Random.Range(-0.8f, 0.8f) + topLeft.position.x, topLeft.position.y - y + UnityEngine.Random.Range(-1f, 1f), 0), Quaternion.identity, transform);
                    float size = UnityEngine.Random.Range(0.5f, 1f);
                    slimes.transform.localScale = new Vector2(size, size);
                }
            }

        }
    }
    public void TrapSpawn()//滚石生成
    {
        if(data.transform.position.y <= (bottomRight.position.y/3))
        {
            int[] myNumbers = { 7, -7};
            int randomIndex = UnityEngine.Random.Range(0, myNumbers.Length);
            int finalNumber = myNumbers[randomIndex];
            GameObject traps = Object.Instantiate(trap, new Vector3(finalNumber + topLeft.position.x, topLeft.position.y, 0), Quaternion.identity, transform);
        }
    }
    private void CarvePath()//通道创建
    {
        int currentX = UnityEngine.Random.Range(1, width - 1);
        for (int y = 0; y < height; y++)
        {
            mapData[currentX, y] = true;
            int leftExpand = UnityEngine.Random.Range(0, 3);
            int rightExpand = UnityEngine.Random.Range(0, 3);
            int leftBound = Mathf.Clamp(currentX - leftExpand, 0, width - 1);
            int rightBound = Mathf.Clamp(currentX + rightExpand, 0, width - 1);
            for (int targetX = leftBound; targetX <= rightBound; targetX++)
            {
                mapData[targetX, y] = true;
            }
            currentX = Mathf.Clamp(currentX + UnityEngine.Random.Range(-1, 2), 1, width - 2); 
        }
    }
    public void DeleteMap()
    {
        for (int i = transform.childCount - 1; i >= 0; i--)
        {
            DestroyImmediate(transform.GetChild(i).gameObject);
        }
    }
}
