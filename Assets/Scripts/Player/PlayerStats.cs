using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats Instance;
    [SerializeField] public Transform playerTransform;
    [SerializeField] private ExpTableSO expTable;
    [SerializeField] private Upgrade3UI upgradeUI;
    [SerializeField] private Slider expBar;
    [SerializeField] private HealthBarUI healthBarUI;
    [ReadOnly] public int level = 1;
    [ReadOnly] public int exp = 0;

    void Awake() => Instance = this;

    void Start()
    {
        this.expBar.maxValue = this.expTable.GetExpToNext(this.level);
        this.expBar.value = this.exp;
    }

    private void Update()
    {
        this.expBar.value = this.exp;
    }

    public void AddExp(int amount)
    {
        this.exp += amount;
        this.expBar.value = this.exp;
        while (this.exp >= this.expTable.GetExpToNext(this.level))
        {
            this.exp -= this.expTable.GetExpToNext(this.level);
            ++this.level;
            this.expBar.maxValue = this.expTable.GetExpToNext(this.level);
            this.expBar.value = this.exp;
            this.upgradeUI.gameObject.SetActive(true);  //升级时打开升级UI
        }
    }

    public void TakeDamage(int dmg)
    {
        this.healthBarUI.health -= dmg;
    }
}
