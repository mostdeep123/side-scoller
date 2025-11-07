using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [Header("Properties")]
    public List<Image> healthLists = new List<Image>();

    [HideInInspector] public int characterHealth;

    void Start ()
    {
        characterHealth = GameState.game.tileManager.characterDatas[0].health;
        PlayerPrefs.SetInt("health", characterHealth);
    }

    public void HealthUpdate (int currentHealth)
    {
        healthLists[currentHealth].enabled = false;
    }
}
