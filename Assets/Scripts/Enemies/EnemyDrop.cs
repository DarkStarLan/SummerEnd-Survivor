using UnityEngine;

public class EnemyDrop : MonoBehaviour
{
    [SerializeField] private DropTableSO dropTable;
    [SerializeField] private GameObject coinPrefab;  //ͨ��Ԥ����
    [SerializeField] private GameObject expPrefab;  //����Ԥ����

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
            OnDeath();
    }

    public void OnDeath()
    {
        //�������
        float roll = Random.value;
        if (roll < this.dropTable.diamondChance)
            Spawn(CurrencyType.Diamond, this.dropTable.diamondValue);
        else if (roll < this.dropTable.diamondChance + this.dropTable.goldChance)
            Spawn(CurrencyType.Gold, this.dropTable.goldValue);
        else if (roll < this.dropTable.diamondChance + this.dropTable.goldChance + this.dropTable.silverChance)
            Spawn(CurrencyType.Silver, this.dropTable.silverValue);

        //�������
        if (Random.value < 0.8f)  //80%����
        {
            var orb = MultiObjectPool.Instance.Get(this.expPrefab.name, transform.position);
        }
    }

    void Spawn(CurrencyType type, int value)
    {
        var pick = MultiObjectPool.Instance.Get(this.coinPrefab.name, this.transform.position);
        pick.GetComponent<CoinPickup>().poolKey = this.coinPrefab.name;
        pick.GetComponent<CoinPickup>().Setup(type, value);
    }
}
