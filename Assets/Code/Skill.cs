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
    [HideInInspector] public bool obstacleActive;
    [HideInInspector] public bool invActive;


    private string currentSkillType;
    private SpriteRenderer sr;
    private Color originalColor;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentSkillType = GameState.game.tileManager.characterDatas[PlayerPrefs.GetInt("select")].skillType;
        sr = this.GetComponent<SpriteRenderer>();
        originalColor = sr.color;
     }

    void Update()
    {
        MagnetManager();
        ObstacleManager();
        InvManager();
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
            case "obstacle":
                if (totalSpecialItem >= obstacleReachSpecialItem && !skillActive)
                {
                    skillActive = true;
                    obstacleSFX.Play();
                }
                break;
            case "inv":
                if (totalSpecialItem >= invulnerablityReachSpecialItem && !skillActive)
                {
                    skillActive = true;
                    invSFX.Play();
                }
                break;
        }
    }

    void MagnetManager()
    {
        if (skillActive && currentSkillType == "magnet")
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

    void ObstacleManager()
    {
        if (skillActive && currentSkillType == "obstacle")
        {
            if (skillStartTimer >= skillDuration)
            {
                totalSpecialItem = 0;
                skillStartTimer = 0;
                skillActive = false;
                obstacleActive = false;
                obstacleSFX.Stop();

                if (sr != null)
                    sr.color = originalColor;
            }
            else
            {
                skillStartTimer += Time.deltaTime;
                obstacleActive = true;

                if (sr != null)
                {
                    Color c = originalColor;
                    c.a = 0.5f; 
                    sr.color = c;
                }
            }
        }
    }


    void InvManager()
    {
        if (skillActive && currentSkillType == "inv")
        {
            if (skillStartTimer >= skillDuration)
            {
                totalSpecialItem = 0;
                skillStartTimer = 0;
                skillActive = false;
                invActive = false;
                invSFX.Stop();

                if (sr != null)
                    sr.color = originalColor;
            }
            else
            {
                skillStartTimer += Time.deltaTime;
                invActive = true;

                if (sr != null)
                {
                    Color c = originalColor;
                    c.a = 0.5f; 
                    sr.color = c;
                }
            }
        }
    }

}
