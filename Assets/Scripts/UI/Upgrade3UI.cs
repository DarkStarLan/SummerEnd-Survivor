using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class Upgrade3UI : MonoBehaviour
{
    [SerializeField] WeaponFormation weaponFormation;  //关联武器阵型脚本
    private readonly string[] talents = { "攻击+2", "转速+10", "范围+0.03", "武器数量+1" };

    private void Awake()
    {
        this.gameObject.SetActive(false);
    }

    void OnEnable()
    {
        // 创建根
        var root = GetComponent<UIDocument>().rootVisualElement;
        root.Clear();
        root.style.flexDirection = FlexDirection.Row;
        root.style.justifyContent = Justify.Center;

        Refresh();                       // 首次生成
        root.Add(new Button(Refresh) { text = "刷新", style = { width = 120, height = 80, marginLeft = 10 } });
    }

    void Refresh()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;

        //保留最后一个子（刷新按钮），其余删掉
        for (int i = root.childCount - 2; i >= 0; i--)
            root.RemoveAt(i);

        //重新插入按钮
        string[] three = Random3();
        foreach (var t in three)
            root.Insert(0, new Button(() => Choose(t)) { text = t, style = { width = 120, height = 80, marginTop = 10 } });
    }

    void Choose(string t)
    {
        switch (t)
        {
            case "攻击+2": this.weaponFormation.AddDamage(2); break;
            case "转速+10": this.weaponFormation.AddRotationSpeed(10f); break;
            case "范围+0.03": this.weaponFormation.AddRadius(0.03f); break;
            case "武器数量+1": this.weaponFormation.AddProjectileCount(1); break;
        }
        gameObject.SetActive(false);
    }

    private string[] Random3()
    {
        return talents.OrderBy(_ => Random.value).Take(3).ToArray();
    }
}
