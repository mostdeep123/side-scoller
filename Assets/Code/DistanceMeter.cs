using UnityEngine;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;

public class DistanceMeter : MonoBehaviour
{
   [Header("Health Bar")]
    public Image content;
    public float currentDistance;
    public float maxCurrentDistance = 10f;

    [Header("Settings")]
    public float increasePerSecond = 1f;

    bool showDistance;

    void Start()
    {
        _ = IncreaseDistanceLoop();
    }

    private async UniTask IncreaseDistanceLoop()
    {
        while (currentDistance < maxCurrentDistance && this != null && gameObject.activeInHierarchy)
        {
            // Add the distance
            if(GameState.game.state != GameState.gameState.Hit) currentDistance += increasePerSecond * Time.deltaTime;

            // Clamp Max
            currentDistance = Mathf.Min(currentDistance, maxCurrentDistance);
            if (content != null && GameState.game.state != GameState.gameState.End)
                content.fillAmount = currentDistance / maxCurrentDistance;

            //set distance reached
            if(!showDistance && GameState.game.state == GameState.gameState.End)
            {
                showDistance = true;
                EndPopUp endPopUp = GameState.game.popUpEnd.GetComponent<EndPopUp>();
                endPopUp.ShowDistanceText(currentDistance);
            }

            await UniTask.Yield(PlayerLoopTiming.Update);
        }

        if (currentDistance >= maxCurrentDistance)
        {
            EndPopUp endPopUp = GameState.game.popUpEnd.GetComponent<EndPopUp>();
            endPopUp.ShowDistanceText(currentDistance);
            GameState.game.state = GameState.gameState.End;
            GameState.game.UpdateState();
        }

        // Make Sure Fill is full
        if (content != null)
            content.fillAmount = 1f;
    }
}
