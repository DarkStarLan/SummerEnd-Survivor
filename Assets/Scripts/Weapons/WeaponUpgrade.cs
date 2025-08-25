using UnityEngine;

[RequireComponent(typeof(WeaponFormation))]
public class WeaponUpgrade : MonoBehaviour
{
    public static WeaponUpgrade Instance { get; private set; }
    [SerializeField] private WeaponLevelSO levelTable;
    [SerializeField] private WeaponBaseSO weapon;

    private int currentLevel;
    [ReadOnly][SerializeField] private int killCount;

    void Awake()
    {
        Instance = this;
        this.GetComponent<WeaponFormation>().runtimeWeapon = Instantiate(this.weapon);  //将weapon复制给WeaponFormation
    }

    public void AddKill()
    {
        ++killCount;
        UpdateLevel();
    }

    public void UpdateLevel()
    {
        this.currentLevel = PlayerStats.Instance.level - 1;  //等级从0开始
        this.ApplyLevel();  //应用当前等级的属性
        //int newLevel = 0;
        //for (int i = 0; i < levelTable.killThreshold.Length; ++i)
        //{
        //    if (killCount >= levelTable.killThreshold[i])
        //        newLevel = i + 1;
        //    else break;
        //}

        //if (newLevel != currentLevel)
        //{
        //    currentLevel = newLevel;
        //    ApplyLevel();
        //}
    }

    void ApplyLevel()  // Apply the current level's stats to the weapon
    {
        this.GetComponent<WeaponFormation>().runtimeWeapon.projectileCount = levelTable.weaponCount[currentLevel];
        this.GetComponent<WeaponFormation>().runtimeWeapon.rotationSpeed = levelTable.rotationSpeed[currentLevel];
        this.GetComponent<WeaponFormation>().runtimeWeapon.radius = levelTable.radius[currentLevel];
        GetComponent<WeaponFormation>()?.RefreshFormation();  //刷新阵型
    }
}
