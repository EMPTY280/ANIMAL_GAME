using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope : MonoBehaviour
{
    private BoxCollider2D boxCollider;

    private bool isCatch = false;
    private float catchPos;

    private Vector2 originPos;

    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        originPos = transform.position;
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(isCatch)
        {
            transform.position = new Vector3(catchPos, transform.position.y, 0);
        }
    }

    public void CatchRope(float xPos)
    {
        boxCollider.enabled = false;
        catchPos = xPos;
        isCatch = true;
    }

    public void MissRope()
    {
        isCatch = false;
    }

    public void ReturnOrigin()
    {
        transform.position = originPos;
        boxCollider.enabled = true;
    }
}
