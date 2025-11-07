using UnityEngine;

public class Coin : MonoBehaviour
{
    [HideInInspector] public int coin;

    private void OnTriggerEnter2D(Collider2D Coll)
    {
        if(Coll.transform.tag == "coin")
        {
            coin++;
            Coll.transform.gameObject.SetActive(false);
        }
    }
}
