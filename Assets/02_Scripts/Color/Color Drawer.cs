using UnityEngine;
/// <summary>
/// 颜色绘制器
/// </summary>
public class ColorDrawer : MonoBehaviour
{
    //色库
    public static float[] fixedHues = new float[] { 0f, 0.33f, 0.66f, 0.75f, 0.16f };
    //洗牌蒜贩
    public static void ShuffleAlgorithm(float[] copyHue)
    {
        for (int i = 0; i < copyHue.Length; i++)
        {
            float temp = copyHue[i];
            int randomIndex = Random.Range(i, copyHue.Length);
            copyHue[i] = copyHue[randomIndex];
            copyHue[randomIndex] = temp;
        }
    }
    public static void SetRandomColor(float[] fixedHues)
    {
        float[] copyHue = (float[])fixedHues.Clone();
        ShuffleAlgorithm(copyHue);
        Color myColor1 = Color.HSVToRGB(copyHue[0], 0.8f, 0.9f);
        Color myColor2 = Color.HSVToRGB(copyHue[1], 0.8f, 0.9f);
        Color myColor3 = Color.HSVToRGB(copyHue[2], 0.8f, 0.9f);
        Player_Data.Instance.color = myColor1;
        Player_Data.Instance.color2 = myColor2;
        Player_Data.Instance.color3 = myColor3;
    }
}
