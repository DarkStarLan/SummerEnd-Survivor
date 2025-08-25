using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private SpawnerWaveSO wave;
    [SerializeField] private float interval;  //生成间隔
    //[SerializeField] private float spawnRadius = 1f;  //离屏幕边缘的安全距离

    private float waveTimer = 0f;

    void Start()
    {
        StartCoroutine(this.SpawnRoutine());
    }

    private IEnumerator SpawnRoutine()
    {
        waveTimer = 0f;
        while (waveTimer < wave.waveDuration)
        {
            //曲线密度 0-1 → 0-100% 概率
            float density = wave.densityCurve.Evaluate(waveTimer / wave.waveDuration);
            if (Random.value < density)
                SpawnOne();

            yield return new WaitForSeconds(this.interval);  //等待下次生成
            waveTimer += Time.deltaTime;
        }
    }

    private void SpawnOne()
    {
        Vector3 spawnPos = this.ScreenEdgeRandomPos();

        EnemyBaseSO enemyType = wave.enemyTypes[Random.Range(0, wave.enemyTypes.Length)];
        GameObject go = MultiObjectPool.Instance.Get(enemyType.enemyName, spawnPos);  //从对象池获取敌人
    }

    private Vector3 ScreenEdgeRandomPos()  //屏幕边缘随机位置
    {
        Camera cam = Camera.main;
        float height = 1f * cam.orthographicSize;
        float width = height * cam.aspect;

        // 四条边随机
        int edge = Random.Range(0, 4);
        Vector3 pos = Vector3.zero;
        switch (edge)
        {
            case 0: pos = new Vector3(Random.Range(-width, width), height, 0); break;   // 上
            case 1: pos = new Vector3(Random.Range(-width, width), -height, 0); break;  // 下
            case 2: pos = new Vector3(width, Random.Range(-height, height), 0); break;  // 右
            case 3: pos = new Vector3(-width, Random.Range(-height, height), 0); break; // 左
        }
        return cam.transform.position + pos;
    }
}
