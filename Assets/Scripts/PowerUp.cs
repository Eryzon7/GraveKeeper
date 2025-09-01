using UnityEngine;

[CreateAssetMenu(fileName = "PowerUp", menuName = "Scriptable Objects/PowerUp")]
public class PowerUp : ScriptableObject
{
    public string powerUpName;
    public string description;
    public Sprite icon;
    public float value;


    public enum PowerUpType { AttackDamage, AttackSpeed, Pierce, MoveSpeed, MaxHealth }
    public PowerUpType type;

    [Header("Value Range")]
    public float minValue = 3f;
    public float maxValue = 7f;

    /// <summary>
    /// Returns a randomized value between min and max.
    /// </summary>
    public void GetRandomValue()
    {
        value = Random.Range(minValue, maxValue);
    }
}