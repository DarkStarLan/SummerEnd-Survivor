using System.Collections.Generic;
using UnityEngine;

public class MultiObjectPool : MonoBehaviour
{
    public static MultiObjectPool Instance { get; private set; }
    [System.Serializable]
    private class PoolData
    {
        public BulletType type;
        public GameObject prefab;
        public int initialSize = 20;
    }

    [SerializeField] private List<PoolData> pools;  //池列表
    private readonly Dictionary<BulletType, Queue<GameObject>> poolDict = new();  //对象池字典

    void Awake()
    {
        Instance = this;
        foreach (var p in pools)
        {
            poolDict[p.type] = new Queue<GameObject>();
            for (int i = 0; i < p.initialSize; i++)
                Expand(p.type, p.prefab);
        }
    }

    private void Expand(BulletType type, GameObject prefab)
    {
        var obj = Instantiate(prefab, transform);
        obj.SetActive(false);
        poolDict[type].Enqueue(obj);
    }

    public GameObject Get(BulletType type)
    {
        if (!poolDict.ContainsKey(type)) return null;
        if (poolDict[type].Count == 0)
            Expand(type, pools.Find(p => p.type == type).prefab);

        var obj = poolDict[type].Dequeue();
        obj.SetActive(true);
        return obj;
    }

    public void Return(BulletType type, GameObject obj)
    {
        obj.SetActive(false);
        poolDict[type].Enqueue(obj);
    }
}
