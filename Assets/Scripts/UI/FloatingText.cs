using UnityEngine;
using TMPro;
using System.Collections;

public class FloatingText : MonoBehaviour
{
    public static FloatingText Instance;
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private float timeToLive = 1f;
    private bool show = false;

    public void Show(string msg, Color color)
    {
        this.show = true;
        this.text.gameObject.SetActive(true);
        this.text.text = msg;
        this.text.color = color;
    }

    void Awake() => Instance = this;

    void Start()
    {
        this.text.gameObject.SetActive(false);  //初始时隐藏文本
        StartCoroutine(Hide());  //启动协程隐藏文本
    }

    private IEnumerator Hide()
    {
        while (true)
        {
            yield return new WaitForSeconds(this.timeToLive);
            if (!this.show) this.text.gameObject.SetActive(false);
            else this.show = false;
        }
    }
}
