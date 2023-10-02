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

        subs.Add("���� ���� ��, ��ȭ ������ �Ҹ��� ���� �����߽��ϴ�.");
        subs.Add("�ٵ� ���� ���������� ������ ����������");
        subs.Add("�ƹ�ư �޸��⳪ �϶�� ����������");
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
