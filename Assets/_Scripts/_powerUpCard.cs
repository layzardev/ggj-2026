using UnityEngine;

[CreateAssetMenu(menuName = "PowerUp/Card")]
public class _powerUpCard : ScriptableObject
{
    public string cardName;
    [TextArea] public string description;
}
