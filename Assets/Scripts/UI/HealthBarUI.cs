using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    [SerializeField] private PlayerStatsSO stats;
    [SerializeField] private Slider slider;
    [ReadOnly] public int health;

    void Start()
    {
        slider.maxValue = stats.maxHP;
        slider.value = stats.currentHP;
        this.health = stats.currentHP;
    }

    void Update()
    {
        slider.value = this.health;
    }
}
