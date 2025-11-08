using System.Collections.Generic;
using UnityEngine;

public class CoinSpawn : MonoBehaviour
{
    [Header("Coins")]
    public GameObject coinObject;
    public List<Transform> coinSpawnTransforms = new List<Transform>();
    private List<GameObject> coinInSpawned = new List<GameObject>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        coinObject = GameState.game.tileManager.characterDatas[PlayerPrefs.GetInt("select")].coin;

        for (int i = 0; i < coinSpawnTransforms.Count; i++)
        {
            GameObject coin = Instantiate(coinObject);
            coin.transform.position = coinSpawnTransforms[i].position;
            coin.transform.SetParent(this.transform);
            coinInSpawned.Add(coin);
        }
    }
    
    void OnDisable ()
    {
        for(int i = 0; i < coinInSpawned.Count; i++)
        {
            coinInSpawned[i].SetActive(true);
        }
    }
}
