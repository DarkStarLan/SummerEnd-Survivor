using UnityEngine;

public class EnemyDrop : MonoBehaviour
{
    [SerializeField] private DropTableSO dropTable;
    [SerializeField] private GameObject coinPrefab;  //通用预制体
    [SerializeField] private GameObject expPrefab;  //经验预制体

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
            OnDeath();
    }

    public void OnDeath()
    {
        //掉落货币
        float roll = Random.value;
        if (roll < this.dropTable.diamondChance)
            Spawn(CurrencyType.Diamond, this.dropTable.diamondValue);
        else if (roll < this.dropTable.diamondChance + this.dropTable.goldChance)
            Spawn(CurrencyType.Gold, this.dropTable.goldValue);
        else if (roll < this.dropTable.diamondChance + this.dropTable.goldChance + this.dropTable.silverChance)
            Spawn(CurrencyType.Silver, this.dropTable.silverValue);

        //经验掉落
        if (Random.value < 0.8f)  //80%掉落
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
