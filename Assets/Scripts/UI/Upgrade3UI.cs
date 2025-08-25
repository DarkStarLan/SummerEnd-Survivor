using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class Upgrade3UI : MonoBehaviour
{
    [SerializeField] WeaponFormation weaponFormation;  //�����������ͽű�
    private readonly string[] talents = { "����+2", "ת��+10", "��Χ+0.03", "��������+1" };

    private void Awake()
    {
        this.gameObject.SetActive(false);
    }

    void OnEnable()
    {
        // ������
        var root = GetComponent<UIDocument>().rootVisualElement;
        root.Clear();
        root.style.flexDirection = FlexDirection.Row;
        root.style.justifyContent = Justify.Center;

        Refresh();                       // �״�����
        root.Add(new Button(Refresh) { text = "ˢ��", style = { width = 120, height = 80, marginLeft = 10 } });
    }

    void Refresh()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;

        //�������һ���ӣ�ˢ�°�ť��������ɾ��
        for (int i = root.childCount - 2; i >= 0; i--)
            root.RemoveAt(i);

        //���²��밴ť
        string[] three = Random3();
        foreach (var t in three)
            root.Insert(0, new Button(() => Choose(t)) { text = t, style = { width = 120, height = 80, marginTop = 10 } });
    }

    void Choose(string t)
    {
        switch (t)
        {
            case "����+2": this.weaponFormation.AddDamage(2); break;
            case "ת��+10": this.weaponFormation.AddRotationSpeed(10f); break;
            case "��Χ+0.03": this.weaponFormation.AddRadius(0.03f); break;
            case "��������+1": this.weaponFormation.AddProjectileCount(1); break;
        }
        gameObject.SetActive(false);
    }

    private string[] Random3()
    {
        return talents.OrderBy(_ => Random.value).Take(3).ToArray();
    }
}
