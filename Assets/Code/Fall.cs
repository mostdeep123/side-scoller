using UnityEngine;

public class Fall : MonoBehaviour
{
    [HideInInspector] public GameObject currentTilePattern;

    public void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.transform.tag == "dead")
        {
            GameState.game.state = GameState.gameState.Hit;
            GameState.game.UpdateState();
        }
    }

    public void OnCollisionEnter2D (Collision2D coll)
    {
        if(coll.transform.tag == "tile")
        {
            currentTilePattern = coll.gameObject.transform.parent.gameObject;
        }
    }
}
