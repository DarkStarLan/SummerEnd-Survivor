using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    [SerializeField] private PlayerStatsSO stats;
    [SerializeField] private Slider slider;

    void Start()
    {
        slider.maxValue = stats.maxHP;
        slider.value = stats.currentHP;
    }

    void Update()
    {
        slider.value = stats.currentHP;
    }
}
