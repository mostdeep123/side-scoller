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

    void Start()
    {
        _ = IncreaseDistanceLoop();
    }

    private async UniTask IncreaseDistanceLoop()
    {
        while (currentDistance < maxCurrentDistance && this != null && gameObject.activeInHierarchy)
        {
            // Add the distance
            currentDistance += increasePerSecond * Time.deltaTime;

            // Clamp Max
            currentDistance = Mathf.Min(currentDistance, maxCurrentDistance);
            if (content != null)
                content.fillAmount = currentDistance / maxCurrentDistance;

            await UniTask.Yield(PlayerLoopTiming.Update);
        }

        if (currentDistance >= maxCurrentDistance) GameState.game.state = GameState.gameState.End;

        // Make Sure Fill is full
        if (content != null)
            content.fillAmount = 1f;
    }
}
