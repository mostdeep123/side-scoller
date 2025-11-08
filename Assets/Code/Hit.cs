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
    private Skill skill;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        skill = GetComponent<Skill>();
        originalColor = sr.color;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!skill.invActive && !skill.obstacleActive)
        {
            if(collision.CompareTag("obstacle"))
            {
                GameState.game.state = GameState.gameState.Hit;
                GameState.game.UpdateState();
            }

            if (collision.CompareTag("obstacle") && !isHit)
            {
                _ = FlashRedAsync();
            }
        }
    }

    private async UniTask FlashRedAsync()
    {
        isHit = true;
        sr.color = hitColor;
        await UniTask.Delay((int)(hitDuration * 1000));
        sr.color = originalColor;
        isHit = false;
        GameState.game.state = GameState.gameState.Run;
        GameState.game.UpdateState();
    }
}
