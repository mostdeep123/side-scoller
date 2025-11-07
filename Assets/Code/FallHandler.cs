using UnityEngine;
using Cysharp.Threading.Tasks;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerFallHandler : MonoBehaviour
{
    public TileManager tileManager;      
    public float offscreenY = -5f;        
    public float respawnDelay = 1.5f;     
    public Vector2 respawnPosition = new Vector2(-6f, 0f); 

    private bool isRespawning;

    void Update()
    {
        // Cek kalau player jatuh di bawah layar
        if (!isRespawning && transform.position.y < offscreenY)
        {
            _ = HandleRespawnAsync();
        }
    }

    private async UniTask HandleRespawnAsync()
    {
        isRespawning = true;

        // Pause scroll
        float savedSpeed = tileManager.scrollSpeed;
        tileManager.scrollSpeed = 0f;

        var rb = GetComponent<Rigidbody2D>();
        rb.linearVelocity = Vector2.zero;
        rb.simulated = false;

        // Delay
        await UniTask.Delay((int)(respawnDelay * 1000));
        transform.position = respawnPosition;
        rb.simulated = true;
        tileManager.scrollSpeed = savedSpeed;

        isRespawning = false;
    }
}
