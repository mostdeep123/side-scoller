using UnityEngine;
using System;
using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class GameState : MonoBehaviour
{
    [System.Serializable]
    public enum gameState
    {
        Start,
        Hit,
        Run,
        End
    };

    public gameState state;

    [Header("Properties")]
    public TileManager tileManager;
    public Animator playerObj;
    public GameObject popUpEnd;


    public static GameState game;

    void Awake()
    {
        game = this;
    }

    void Start ()
    {
        playerObj.runtimeAnimatorController = GameState.game.tileManager.characterDatas[PlayerPrefs.GetInt("select")].characterAnimator;
    }

    public async void UpdateState()
    {
        switch (state)
        {
            case gameState.Run:
                break;
            case gameState.Hit:
                int currHealth = PlayerPrefs.GetInt("health");
                currHealth -= 1;
                PlayerPrefs.SetInt("health", currHealth);
                playerObj.GetComponent<Health>().HealthUpdate(PlayerPrefs.GetInt("health"));
                break;
            case gameState.End:
                popUpEnd.SetActive(true);
                tileManager.scrollSpeed = 0;
                playerObj.gameObject.SetActive(false);
                break;
        }
    }
}
