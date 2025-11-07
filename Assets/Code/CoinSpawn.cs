using System.Collections.Generic;
using UnityEngine;

public class CoinSpawn : MonoBehaviour
{
    [Header("Coins")]
    public GameObject coinObject;
    public List<Transform> coinSpawnTransforms = new List<Transform>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for(int i = 0; i < coinSpawnTransforms.Count; i++)
        {
            GameObject coin = Instantiate(coinObject);
            coin.transform.position = coinSpawnTransforms[i].position;
            coin.transform.SetParent(this.transform);
        }
    }
}
