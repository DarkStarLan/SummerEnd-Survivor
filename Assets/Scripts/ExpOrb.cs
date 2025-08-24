using System.Collections;
using UnityEngine;

public class ExpOrb : MonoBehaviour
{
    [SerializeField] private ExpOrbSO data;

    private Transform player;
    private bool isMoving = false;

    void OnEnable()
    {
        isMoving = false;
        StartCoroutine(DestroySelf());
    }

    void Update()
    {
        this.player = PlayerStats.Instance.playerTransform;
        if (!isMoving && player != null)
        {
            float dist = Vector2.Distance(this.transform.position, this.player.position);
            if (dist <= this.data.pickupRadius)
            {
                isMoving = true;
            }
        }

        if (isMoving)
        {
            Vector2 dir = (this.player.position - this.transform.position).normalized;
            this.transform.position += (Vector3)(dir * data.autoMoveSpeed * Time.deltaTime);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            PlayerStats.Instance.AddExp(this.data.expValue);
            MultiObjectPool.Instance.Return(this.data.orbName, gameObject);
        }
    }

    private IEnumerator DestroySelf()
    {
        yield return new WaitForSeconds(this.data.lifeTime);
        MultiObjectPool.Instance.Return(this.data.orbName, gameObject);
    }
}
