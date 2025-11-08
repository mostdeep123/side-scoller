using UnityEngine;

public class Skill : MonoBehaviour
{
    public int totalSpecialItem;

    [Header("Settings of Skill Reached")]
    public int magnetReachSpecialItem;
    public int obstacleReachSpecialItem;
    public int invulnerablityReachSpecialItem;

    [Header("Audio Sounds")]
    public AudioSource magnetSFX;
    public AudioSource obstacleSFX;
    public AudioSource invSFX;

    [Header("Skill Duration")]
    public float skillDuration;
    private float skillStartTimer;

    [HideInInspector] public bool skillActive;
    [HideInInspector] public bool magnetActive;


    private string currentSkillType;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentSkillType = GameState.game.tileManager.characterDatas[0].skillType;
    }

    void Update()
    {
        if(currentSkillType == "magnet") MagnetManager();
    }

    public void SkillTypeManager()
    {
        switch (currentSkillType)
        {
            case "magnet":
                if (totalSpecialItem >= magnetReachSpecialItem && !skillActive)
                {
                    skillActive = true;
                    magnetSFX.Play();
                }
                break;
            case "obs":
                if (totalSpecialItem >= obstacleReachSpecialItem && !skillActive)
                {
                    skillActive = true;
                }
                break;
            case "inv":
                if (totalSpecialItem >= invulnerablityReachSpecialItem && !skillActive)
                {
                    skillActive = true;
                }
                break;
        }
    }

    void MagnetManager ()
    {
        if(skillActive)
        {
            if (skillStartTimer >= skillDuration)
            {
                totalSpecialItem = 0;
                skillStartTimer = 0;
                skillActive = false;
                magnetActive = false;
                magnetSFX.Stop();
            }
            else
            {
                skillStartTimer += Time.deltaTime;
                magnetActive = true;
            }
        }
    }
}
