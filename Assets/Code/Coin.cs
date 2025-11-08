using UnityEngine;

public class Coin : MonoBehaviour
{
    [HideInInspector] public int coin;

    [Header("Coin SFX")]
    public AudioSource coinSFX;

    private void OnTriggerEnter2D(Collider2D Coll)
    {
        if(Coll.transform.tag == "coin")
        {
            coin++;
            Coll.transform.gameObject.SetActive(false);
            coinSFX.Play();
            EndPopUp endPopup = GameState.game.popUpEnd.GetComponent<EndPopUp>();
            endPopup.ShowCoinText(coin);
        }
    }
}
