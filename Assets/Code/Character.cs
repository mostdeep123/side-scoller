using UnityEngine;

[CreateAssetMenu(fileName = "Character", menuName = "Scriptable Objects/Character")]
public class Character : ScriptableObject
{
    public Animator characterAnimator;
    public int health;
    public float speed;
    public string skillType;
}
