using System.Collections;
using UnityEngine;

public class HitEffect : MonoBehaviour
{
    private Transform tf;
    private ParticleSystem spark;

    void Awake()
    {
        this.tf = this.transform;
        this.spark = this.GetComponent<ParticleSystem>();
    }

    public IEnumerator PlayAHit(float time)
    {
        this.spark.Play();
        yield return new WaitForSeconds(time);
        this.spark.Stop();
    }

    public void PlayHit() => this.spark.Play();
    public void StopHit() => this.spark.Stop();
}
