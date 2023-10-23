using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadePanel : MonoBehaviour
{
    SpriteRenderer sprite;
    public SpriteRenderer Sprite { get { return sprite; } }

    Color _color;

    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        _color = sprite.color;
    }

    public IEnumerator FadeInOut(bool isOn)
    {
        WaitForSeconds delay = new WaitForSeconds(0.01f);
        if (isOn == true && _color.a <= 0f)
        {
            while (_color.a < 1.0f)
            {
                _color.a += 0.01f;
                yield return delay;
                sprite.color = _color;
            }
        }
        else if (isOn == false && _color.a >= 1f)
        {
            while (_color.a > 0f)
            {
                _color.a -= 0.01f;
                yield return delay;
                sprite.color = _color;
            }
        }
    }
}