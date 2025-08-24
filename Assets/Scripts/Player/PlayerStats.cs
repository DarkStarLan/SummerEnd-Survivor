using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats Instance;
    [SerializeField] public Transform playerTransform;
    [ReadOnly] public int exp = 0;

    void Awake() => Instance = this;
    public void AddExp(int amount) => exp += amount;
}
