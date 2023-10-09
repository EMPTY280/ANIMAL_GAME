using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TutorialEvent : MonoBehaviour
{
    [SerializeField] TutorialMap map;
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] PlayerBase player;

    int n = 1;

    private void Awake()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        text.gameObject.SetActive(false);
        StartCoroutine(TutorialProgress());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator TutorialProgress()
    {
        float delay = 2f;
        while (true)
        {
            if (map.GetReserveEnd() == true)
            {
                text.gameObject.SetActive(true);
                text.text = n.ToString();
                if(player.GetHit() == true)
                {
                    n++;
                }
                player.RestoreHit();
                map.reserveEnd = 10;
                yield return new WaitForSeconds(delay);
                text.gameObject.SetActive(false);
                yield return new WaitForSeconds(delay);
                map.ReserveSegment();
                if (n == 5)
                {
                    yield break;
                }
            }
            yield return null;
        }

        //if (map.GetReserveEnd() == true)
        //{
        //    text.gameObject.SetActive(true);
        //    text.text = n.ToString();
        //    n++;
        //    yield return new WaitForSeconds(delay);
        //    text.gameObject.SetActive(false);
        //    map.ReserveSegment();
        //}
        //yield return new WaitForSeconds(1f);
    }
}
