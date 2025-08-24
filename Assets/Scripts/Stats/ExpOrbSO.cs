using UnityEngine;

[CreateAssetMenu(menuName = "Stats/Drop/ExpOrb")]
public class ExpOrbSO : ScriptableObject
{
    public string orbName = "ExpOrb";
    public float lifeTime = 7f;
    public int expValue = 1;
    public float autoMoveSpeed = 8f;  //飞向玩家的速度
    public float pickupRadius = 1f;
}
