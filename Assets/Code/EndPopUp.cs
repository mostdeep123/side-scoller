using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System;

public class EndPopUp : MonoBehaviour
{
    [Header("Properties")]
    public TextMeshProUGUI distanceText;
    public TextMeshProUGUI coinText;

    public void ResetRoom ()
    {
        SceneManager.LoadScene(1);
    }

    public void ShowDistanceText (float distanceMeter)
    {
        int distanceMeterReal = Mathf.RoundToInt(distanceMeter);
        distanceText.text = "Distance : " + distanceMeterReal.ToString() + "%";
    }

    public void ShowCoinText(int coin)
    {
        coinText.text = "Coin : " + coin.ToString();
    }
}
