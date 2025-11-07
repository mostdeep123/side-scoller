using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;

public class TileManager : MonoBehaviour
{
    [Header("Tile Prefabs")]
    public List<GameObject> tilePrefabs = new List<GameObject>();

    [Header("Settings")]
    public float scrollSpeed = 5f;
    public int poolSize = 6;
    public float tileWidth = 10f;

    [Header("Spawn Settings")]
    public float startX = 0f;
    public float despawnX = -15f;

    private readonly List<GameObject> activeTiles = new List<GameObject>();

    async void Start()
    {
        // Init tiles to pool size
        for (int i = 0; i < poolSize; i++)
        {
            GameObject prefab = tilePrefabs[Random.Range(0, tilePrefabs.Count)];
            GameObject tile = Instantiate(prefab, transform);
            tile.transform.position = new Vector3(startX + i * tileWidth, 0, 0);
            activeTiles.Add(tile);
        }
        _ = MoveTilesLoop();
    }

    private async UniTask MoveTilesLoop()
    {
        while (this != null && gameObject.activeInHierarchy)
        {
            float delta = Time.deltaTime * scrollSpeed;

            for (int i = 0; i < activeTiles.Count; i++)
            {
                GameObject tile = activeTiles[i];
                if (!tile.activeSelf) continue;

                Vector3 pos = tile.transform.position;
                pos.x -= delta;
                tile.transform.position = pos;

                // if its move in left side
                if (pos.x <= despawnX)
                {
                    tile.SetActive(false);

                    float maxX = GetRightMostX();
                    tile.transform.position = new Vector3(maxX + tileWidth, pos.y, pos.z);
                    
                    // swap tile to different things
                    var randomPrefab = tilePrefabs[Random.Range(0, tilePrefabs.Count)];
                    var sr = tile.GetComponent<SpriteRenderer>();
                    var src = randomPrefab.GetComponent<SpriteRenderer>();
                    if (sr != null && src != null)
                        sr.sprite = src.sprite;

                    tile.SetActive(true);
                }
            }

            await UniTask.Yield(PlayerLoopTiming.Update);
        }
    }

    private float GetRightMostX()
    {
        float maxX = float.MinValue;
        foreach (var t in activeTiles)
        {
            if (t.activeSelf && t.transform.position.x > maxX)
                maxX = t.transform.position.x;
        }
        return maxX;
    }
}
