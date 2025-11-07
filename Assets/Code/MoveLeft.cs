using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    public TileManager tile;

    // Update is called once per frame
    void Update()
    {
        this.transform.position += Vector3.left * tile.scrollSpeed * Time.deltaTime;
    }
}
