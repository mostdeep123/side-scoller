using System.Collections.Generic;
using UnityEngine;

public class SpecialSpawn : MonoBehaviour
{
    [Header("Special Properties")]
    public GameObject specialSpawn;
    public List<Transform> specialSpawnTransforms = new List<Transform>();
    private List<GameObject> specials = new List<GameObject>();


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for (int i = 0; i < specialSpawnTransforms.Count; i++)
        {
            GameObject special = Instantiate(specialSpawn);
            special.transform.position = specialSpawnTransforms[i].position;
            special.transform.SetParent(this.transform);
            specials.Add(special);
        }
    }
    
     void OnDisable ()
    {
        for(int i = 0; i < specials.Count; i++)
        {
            specials[i].SetActive(true);
        }
    }
}
