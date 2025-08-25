using UnityEngine;

public class ComboCounter : MonoBehaviour
{
    public static ComboCounter Instance;
    [ReadOnly][SerializeField] private int combo = 0;
    [SerializeField] private float comboTimeout = 2f;  //2ÃëÄÚÃ»»÷É±¹éÁã
    private float lastKillTime;

    void Awake() => Instance = this;

    public void AddKill()
    {
        if (Time.time - lastKillTime > comboTimeout) combo = 0;
        combo++;
        lastKillTime = Time.time;
        FloatingText.Instance.Show($"+{combo}", Color.blue);
    }

    public int GetCombo() => combo;
}
