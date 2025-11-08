using UnityEngine;
using Cysharp.Threading.Tasks;

public class Hit : MonoBehaviour
{
    [Header("Hit Effect Settings")]
    public Color hitColor = Color.red;
    public float hitDuration = 0.2f;

    private SpriteRenderer sr;
    private Color originalColor;
    private bool isHit = false;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        originalColor = sr.color;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("obstacle") && !isHit)
        {
            _ = FlashRedAsync();
        }
    }

    private async UniTask FlashRedAsync()
    {
        GameState.game.state = GameState.gameState.Hit;
        GameState.game.UpdateState();
        isHit = true;
        sr.color = hitColor;
        await UniTask.Delay((int)(hitDuration * 1000));
        sr.color = originalColor;
        isHit = false;
    }
}
