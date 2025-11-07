using UnityEngine;
using System;
using Cysharp.Threading.Tasks;
using System.Collections.Generic;

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

    public async void UpdateState()
    {
        switch (state)
        {
            case gameState.Run:
                playerObj.SetTrigger("run");
                break;
            case gameState.Hit:
                tileManager.scrollSpeed = 0;
                int currHealth = PlayerPrefs.GetInt("health");
                currHealth -= 1;
                PlayerPrefs.SetInt("health", currHealth);
                playerObj.GetComponent<Health>().HealthUpdate(PlayerPrefs.GetInt("health"));
                state = gameState.Run;
                await Respawn();
                break;
            case gameState.End:
                popUpEnd.SetActive(true);
                break;
        }
    }
    
    async UniTask Respawn ()
    {
        GameObject respawnTransforms = playerObj.GetComponent<Fall>().currentTilePattern;
        Respawn respawnInformation = respawnTransforms.GetComponent<Respawn>();
        List<Transform> respawns = respawnInformation.respawnTiles;
        Transform nearestRespawn = null;
        float nearestDist = float.MaxValue;
        Vector3 playerPos = playerObj.transform.position;

        if(respawns.Count > 0)
        {
            foreach (var r in respawns)
            {
                float dist = Vector3.Distance(playerPos, r.position);
                if (dist < nearestDist)
                {
                    nearestDist = dist;
                    nearestRespawn = r;
                }
            }
            if (nearestRespawn != null)
            {
                playerObj.transform.position = nearestRespawn.position;
            }
        }
        await UniTask.Delay(1500);
        tileManager.scrollSpeed = tileManager.scrollSpeedStart;
    }

}
