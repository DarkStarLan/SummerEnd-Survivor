using UnityEngine;

public class PlayerWallet : MonoBehaviour
{
    public static PlayerWallet Instance;
    [ReadOnly][SerializeField] private int silver, gold, diamond;

    void Awake() => Instance = this;
    public void Add(CurrencyType type, int amount)
    {
        switch (type)
        {
            case CurrencyType.Silver: silver += amount; break;
            case CurrencyType.Gold: gold += amount; break;
            case CurrencyType.Diamond: diamond += amount; break;
        }
    }
}
