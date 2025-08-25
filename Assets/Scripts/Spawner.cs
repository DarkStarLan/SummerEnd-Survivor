using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private SpawnerWaveSO wave;
    [SerializeField] private float interval;  //���ɼ��
    //[SerializeField] private float spawnRadius = 1f;  //����Ļ��Ե�İ�ȫ����

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
            //�����ܶ� 0-1 �� 0-100% ����
            float density = wave.densityCurve.Evaluate(waveTimer / wave.waveDuration);
            if (Random.value < density)
                SpawnOne();

            yield return new WaitForSeconds(this.interval);  //�ȴ��´�����
            waveTimer += Time.deltaTime;
        }
    }

    private void SpawnOne()
    {
        Vector3 spawnPos = this.ScreenEdgeRandomPos();

        EnemyBaseSO enemyType = wave.enemyTypes[Random.Range(0, wave.enemyTypes.Length)];
        GameObject go = MultiObjectPool.Instance.Get(enemyType.enemyName, spawnPos);  //�Ӷ���ػ�ȡ����
    }

    private Vector3 ScreenEdgeRandomPos()  //��Ļ��Ե���λ��
    {
        Camera cam = Camera.main;
        float height = 1f * cam.orthographicSize;
        float width = height * cam.aspect;

        // ���������
        int edge = Random.Range(0, 4);
        Vector3 pos = Vector3.zero;
        switch (edge)
        {
            case 0: pos = new Vector3(Random.Range(-width, width), height, 0); break;   // ��
            case 1: pos = new Vector3(Random.Range(-width, width), -height, 0); break;  // ��
            case 2: pos = new Vector3(width, Random.Range(-height, height), 0); break;  // ��
            case 3: pos = new Vector3(-width, Random.Range(-height, height), 0); break; // ��
        }
        return cam.transform.position + pos;
    }
}
