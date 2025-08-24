using System.Collections;
using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    [SerializeField] private float lifeTime = 8f;
    [HideInInspector] public CurrencyType type;
    [HideInInspector] public int value;
    [HideInInspector] public string poolKey = "Coin";

    public void Setup(CurrencyType t, int v)
    {
        type = t;
        value = v;
        GetComponent<SpriteRenderer>().color = t switch
        {
            CurrencyType.Silver => Color.gray,
            CurrencyType.Gold => Color.yellow,
            CurrencyType.Diamond => Color.cyan,
            _ => Color.white
        };
        StartCoroutine(DestroySelf());  //开始生命周期倒计时
    }

    private IEnumerator DestroySelf()
    {
        yield return new WaitForSeconds(lifeTime);
        MultiObjectPool.Instance.Return(this.poolKey, gameObject);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            PlayerWallet.Instance.Add(type, value);
            MultiObjectPool.Instance.Return(this.poolKey, gameObject);
        }
    }
}
