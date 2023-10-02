using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Subtitle : MonoBehaviour
{
    private int line = 0;
    private List<string> subs = new List<string>();
    [SerializeField] Text text = null;
    [SerializeField] Image cover = null;


    private void Awake()
    {
        if (!text || !cover) enabled = false;
        text.text = " ";

        subs.Add("焼爽 神掘 穿, 疑鉢 蟹虞虞壱 災軒澗 員戚 糎仙梅柔艦陥.");
        subs.Add("悦汽 諺敗 せせせせせ 鯵蝦宣 せせせせせ");
        subs.Add("焼巷動 含軒奄蟹 馬虞壱 せせせせせ");
    }

    public void NextLine()
    {
        text.text = subs[line++];
    }

    public void NextScene()
    {
        text.text = " ";
        StartCoroutine(Fadeout());
    }

    IEnumerator Fadeout()
    {
        while(cover.color.a < 1)
        {
            cover.color = new Color(0, 0, 0, cover.color.a + Time.deltaTime * 0.5f);
            yield return null;
        }
    }
}
