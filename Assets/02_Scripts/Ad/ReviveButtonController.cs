using UnityEngine;

public class ReviveButtonController : MonoBehaviour
{
    public void OnClickReviveButton()
    {
        if (GameFlow.Instance == null) return;
        if (GameFlow.Instance.IsReviving) return;
        if (AdManager.Instance == null)
        {
            Debug.LogError("ReviveButtonController: 场景中缺少 AdManager");
            return;
        }

        GameFlow.Instance.BeginRevive();

        AdManager.Instance.ShowRewardedAd((isSuccess) =>
        {
            if (isSuccess)
            {
                GameFlow.Instance.OneRevive();
            }
            else
            {
                UIManager.Instance.CloseReviveAdPanel();
                GameFlow.Instance.CancelRevive();
                GameFlow.Instance.RestartGame();
            }
        });
    }
}
