using UnityEngine;

[CreateAssetMenu(fileName = "Character", menuName = "Scriptable Objects/Character")]
public class Character : ScriptableObject
{
    public RuntimeAnimatorController characterAnimator;
    public GameObject coin;
    public int health;
    public float speed;
    public string skillType;
}
