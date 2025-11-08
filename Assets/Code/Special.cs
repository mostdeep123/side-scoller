using UnityEngine;

public class Special : MonoBehaviour
{
    [Header("Sound")]
    public AudioSource specialSFX;

    public void OnTriggerEnter2D (Collider2D Coll)
    {
        Skill playerSkill = GameState.game.playerObj.GetComponent<Skill>();

        if(Coll.transform.tag == "special" && !playerSkill.skillActive)
        {
            playerSkill.totalSpecialItem++;
            playerSkill.SkillTypeManager();
            specialSFX.Play();
            Coll.gameObject.SetActive(false);
        }
    }
}
