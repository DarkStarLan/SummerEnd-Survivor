using System.Collections.Generic;
using UnityEngine;

public class MultiObjectPool : MonoBehaviour
{
    public static MultiObjectPool Instance { get; private set; }

    [System.Serializable]
    public class PoolItem
    {
        public string key;  //任意字符串/枚举名
        public GameObject prefab;
        public int initialSize = 10;
    }

    [SerializeField] private List<PoolItem> poolItems = new();
    private readonly Dictionary<string, Queue<GameObject>> pools = new();

    void Awake()
    {
        Instance = this;
        foreach (var item in poolItems)
            InitPool(item);
    }

    void InitPool(PoolItem item)
    {
        if (!pools.ContainsKey(item.key))
            pools[item.key] = new Queue<GameObject>();

        for (int i = 0; i < item.initialSize; ++i)
            Expand(item.key, item.prefab);
    }

    void Expand(string key, GameObject prefab)
    {
        var obj = Instantiate(prefab, transform);
        obj.SetActive(false);
        pools[key].Enqueue(obj);
    }

    /* 取对象 */
    public GameObject Get(string key, Vector2? pos = null)
    {
        if (!pools.TryGetValue(key, out var queue)) return null;
        if (queue.Count == 0) Expand(key, poolItems.Find(p => p.key == key).prefab);
        var obj = queue.Dequeue();
        while (obj == null && queue.Count > 0)  //防止队列里有null
            obj = queue.Dequeue();
        if (pos.HasValue)
        {
            obj.transform.position = pos.Value;
        }
        obj.SetActive(true);
        return obj;
    }

    /* 回收对象 */
    public void Return(string key, GameObject obj)
    {
        obj.transform.position = this.transform.position;
        if (obj.GetComponent<Rigidbody2D>() != null) obj.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        obj.SetActive(false);
        if (pools.TryGetValue(key, out var queue))
            queue.Enqueue(obj);
    }
}
