using UnityEngine;

public class EnemyBodyPart : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public EnemyProperties enemyProperties;
    public BodyPartType bodyPartType;

}

public enum BodyPartType
{
    Weakspot,
    Normal,
    Resistant
}
