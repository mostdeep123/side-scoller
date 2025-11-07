using UnityEngine;

public class GameState : MonoBehaviour
{
    public enum gameState
    {
        Run,
        End
    };

    public gameState state;

    [Header("Properties")]
    public Animator playerObj;
    public GameObject popUpEnd;

    
    public static GameState game;

    void Awake()
    {
        game = this;
    }

    public void UpdateState ()
    {
        switch(state)
        {
            case gameState.Run:
                playerObj.SetTrigger("run");
                break;
            case gameState.End:
                break;
        }
    }
}
