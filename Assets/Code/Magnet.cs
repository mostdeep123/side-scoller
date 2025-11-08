using UnityEngine;

public class Magnet : MonoBehaviour
{
    [Header("Float")]
    public float speed;
    public float magnetDistance;

    private Skill characterSkill;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        characterSkill = GameState.game.playerObj.GetComponent<Skill>();
    }

    // Update is called once per frame
    void Update()
    {
        CoinMagnet();
    }
    
    void CoinMagnet ()
    {
        if(characterSkill.magnetActive)
        {
            Vector2 playerPos = GameState.game.playerObj.transform.position;
            Vector2 thisPos = this.transform.position;
            float dist = Vector2.Distance(thisPos, playerPos);
            if(dist <= magnetDistance)
            {
                this.transform.position = Vector3.MoveTowards(this.transform.position, playerPos, speed);
            }
        }
    }
}
