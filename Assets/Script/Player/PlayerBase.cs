using System.Collections;
using System.Collections.Generic;
using System.Net;
using TMPro;
//using System.Numerics;
using Unity.IO.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerBase : MonoBehaviour
{
    Vector3 originPos;
    Vector2 originColOff;
    Vector2 originColSize;

    [SerializeField] protected MapManager mapManager;
    [SerializeField] protected GameObject magnet;
    [SerializeField] protected GameObject dropPrev;
    [SerializeField] protected TextMeshProUGUI clearItemText;
    [SerializeField] protected TutorialManager manager;

    protected Rigidbody2D rigidBody;
    protected Animator animator;
    protected BoxCollider2D boxCollider;
    protected Rope rope;
    protected SpriteRenderer spriteRenderer;

    protected List<GameObject> effects = new List<GameObject>();

    protected LayerMask groundLayer;
    protected LayerMask ropeLayer;
    protected LayerMask ropeEndLayer;

    protected float jumpPower = 23f;
    protected float gravityPower = 10f;

    protected int maxJump = 2;
    protected int ableJump;
    protected int clearItem;
    public int ClearItem => clearItem;
    protected bool itemDouble = false;

    protected bool onGround = false;
    protected bool onRope = false;
    protected bool isRescue = false;
    protected bool isRush = false;
    protected bool ableRopeAction = false;
    protected bool ableRescue = false;
    protected bool ableObstacleHit = true;

    protected bool obstacleHit = false;
    protected bool itemHit = false;

    protected virtual void Awake()
    {
        int count = transform.childCount;

        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        for (int i = 0; i < count; i++) 
        {
            effects.Add(transform.GetChild(i).gameObject);
            effects[i].gameObject.SetActive(false);
        }

        groundLayer = 1 << LayerMask.NameToLayer("Ground");
        ropeLayer = 1 << LayerMask.NameToLayer("Rope");
        ropeEndLayer = 1 << LayerMask.NameToLayer("RopeEnd");

    }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        clearItem = 0;

        originPos = transform.position;
        originColOff = boxCollider.offset;
        originColSize = boxCollider.size;
        rigidBody.gravityScale = gravityPower;

        LandingSet();

        magnet.SetActive(false);
        dropPrev.SetActive(false);
    }

    protected virtual void Update()
    {
        if (transform.position.x != originPos.x)
        {
            transform.position = new Vector2(originPos.x, transform.position.y);
        }

        if (transform.rotation != Quaternion.Euler(0, 0, 0))
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }

        if (isRescue == true)
        {
            boxCollider.enabled = false;
            rigidBody.gravityScale = 0;
            rigidBody.velocity = new Vector2(0, 5f);
            if (transform.position.y >= -1)
            {
                boxCollider.enabled = true;
                rigidBody.gravityScale = gravityPower;
                isRescue = false;
                ableRescue = false;
                animator.SetBool("RescueDown", true);
                if (effects[2].activeSelf == true)
                {
                    effects[2].SetActive(false);
                }
            }
        }

        if(magnet.activeSelf == true)
        {
            magnet.transform.position = transform.position + Vector3.up;
        }
    }

    protected virtual void FixedUpdate()
    {
        RopeCheck();
        GroundCheck();
    }

    public virtual void LeftButtonAction(string key)
    {
        switch (key)
        {
            case "Down":
                if (ableJump > 0)
                {
                    Jump();
                }
                break;

            case "Stay":
                break;

            case "Up":
                break;
        }
    }

    public virtual void RightButtonAction(string key)
    {
        switch (key)
        {
            case "Down":
                if (ableRopeAction == true)
                {
                    Rope(true);
                }
                break;

            case "Stay":
                if (onGround == true)
                {
                    Slide(true);
                }
                break;

            case "Up":
                if (onGround == true)
                {
                    Slide(false);
                }
                else if (onRope == true)
                {
                    Rope(false);
                }
                break;
        }
    }

    protected void Jump()
    {
        
        Slide(false);
        rigidBody.velocity = new Vector2(0f, jumpPower);
        if(ableJump == maxJump)
        {
            animator.SetBool("OnJump", true);
        }
        else
        {
            animator.SetBool("OnDoubleJump", true);
        }
        onGround = false;
        boxCollider.offset = new Vector2(0f, 1f);
        boxCollider.size = new Vector2(originColSize.x, 1f);
        ableJump--;
    }

    protected void Slide(bool onSlide)
    {
        if (onSlide == true)
        {
            boxCollider.offset = originColOff / 2;
            boxCollider.size = new Vector2(originColSize.x, originColSize.y / 2);
        }
        else
        {
            boxCollider.offset = originColOff;
            boxCollider.size = originColSize;
        }

        animator.SetBool("OnSlide", onSlide);
    }

    protected void Rope(bool _onRope)
    {
        if (_onRope == true)
        {
            rigidBody.gravityScale = 0f;
            rigidBody.velocity = Vector2.zero;
            rope.CatchRope(transform.position.x + 0.6f);
            onRope = true;
            ableJump = 0;
        }
        else
        {
            rigidBody.gravityScale = gravityPower;
            rigidBody.velocity = new Vector2(0f, jumpPower);
            rope.MissRope();
            onRope = false;
        }

        animator.SetBool("OnRope", _onRope);
    }

    protected void LandingSet()
    {
        onGround = true;
        ableJump = maxJump;
        animator.SetBool("OnJump", false);
        animator.SetBool("OnDoubleJump", false);
        animator.SetBool("RescueDown", false);
        boxCollider.offset = originColOff;
        boxCollider.size = originColSize;
    }

    protected void GroundCheck()
    {
        if (rigidBody.velocity.y <= 0 && Physics2D.Raycast(transform.position, Vector2.down, 0.8f, groundLayer) && onGround == false)
        {
            LandingSet();
        }
        else if(onGround == true && Physics2D.Raycast(transform.position, Vector2.down, 0.8f, groundLayer) == false)
        {
            onGround = false;
        }
    }

    protected void RopeCheck()
    {
        Vector2 rayStartPos = (Vector2)transform.position - new Vector2(0.5f, 0);
        RaycastHit2D raycastHit = Physics2D.Raycast(rayStartPos, Vector2.right, 2f, ropeLayer);

        if (raycastHit.collider != null)
        {
            ableRopeAction = true;
            rope = raycastHit.collider.gameObject.GetComponent<Rope>();
        }
        else
        {
            ableRopeAction = false;
        }

        if(Physics2D.Raycast(transform.position, Vector2.right, 1f,ropeEndLayer) && onRope == true)
        {
            Rope(false);
        }
    }

    protected IEnumerator ItemAbility(int itemID)
    {
        WaitForSeconds delay = new WaitForSeconds(0.2f); ;// EventManager.Instance.waitForSeconds;//new WaitForSeconds(0.2f);
        switch (itemID)
        {
            case 1:
                mapManager.SetSpeed(2f);
                ableObstacleHit = false;
                isRush = true;
                effects[0].SetActive(true);
                dropPrev.gameObject.SetActive(true);
                yield return new WaitForSeconds(3f);
                mapManager.ReturnSpeed();
                effects[0].SetActive(false);
                isRush = false;
                for (int i = 0; i < 4; i++) 
                {
                    SpriteTwinkle();
                    yield return delay;
                }
                dropPrev.gameObject.SetActive(false);
                ableObstacleHit = true;
                yield break;

            case 2:
                magnet.gameObject.SetActive(true);
                effects[1].SetActive(true);
                yield return new WaitForSeconds(5f);
                magnet.gameObject.SetActive(false);
                effects[1].SetActive(false);
                yield break;

            case 3:
                effects[2].SetActive(true);
                ableRescue = true;
                yield break;

            case 4:
                ableObstacleHit = false;
                itemDouble = true;
                effects[3].SetActive(true);
                yield return new WaitForSeconds(5f);
                itemDouble = false;
                effects[3].SetActive(false);
                for (int i = 0; i < 4; i++)
                {
                    SpriteTwinkle();
                    yield return delay;
                }
                ableObstacleHit = true;
                yield break;

            default:
                yield break;
        }
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Item") == true)
        {
            ItemBase item = collision.gameObject.GetComponent<ItemBase>();
            int id = item.GetItemID();
            if(id == 0)
            {
                if (itemDouble == true)
                    clearItem += 2;
                else
                    clearItem++;
                clearItemText.text = clearItem.ToString();
            }
            else
            {
                StartCoroutine(ItemAbility(id));
                itemHit = true;
            }
            collision.gameObject.SetActive(false);
        }

        if (collision.gameObject.CompareTag("Obstacle") == true && ableObstacleHit == true)
        {
            obstacleHit = true;
            ableObstacleHit = false;            
            StartCoroutine(ObstacleCrash());
        }

        if (collision.gameObject.CompareTag("Obstacle") == true && isRush == true)
        {
            SpriteRenderer spriteRenderer = collision.gameObject.GetComponent<SpriteRenderer>();
            manager.PlayObstacleAni(collision.transform.position, spriteRenderer.sprite);
            collision.gameObject.SetActive(false);
        }

        if (collision.gameObject.CompareTag("DropZone") == true)
        {            
            if(ableRescue == true)
            {
                isRescue = true;
                animator.SetTrigger("RescueUp");
            }
            else
            {
                obstacleHit = true;
            }
        }
    }

    protected IEnumerator ObstacleCrash()
    {
        WaitForSeconds delay = new WaitForSeconds(0.2f);
        int twinkle = 0;

        animator.SetBool("ObstacleHit", true);
        yield return new WaitForSeconds(0.1f);
        animator.SetBool("ObstacleHit", false);

        SpriteTwinkle();

        //Color temp = spriteRenderer.color;
        //temp.a = 0.5f;
        //spriteRenderer.color = temp;

        while (twinkle < 7)
        {
            yield return delay;
            SpriteTwinkle();
            //if(twinkle == 6)
            //{
            //    temp = spriteRenderer.color;
            //    temp.a = 1f;
            //    spriteRenderer.color = temp;
            //    ableObstacleHit = true;
            //    yield break;
            //}
            //else if (twinkle % 2 == 1)
            //{
            //    temp = spriteRenderer.color;
            //    temp.a = 0.5f;
            //    spriteRenderer.color = temp;
            //}
            //else
            //{
            //    temp = spriteRenderer.color;
            //    temp.a = 1f;
            //    spriteRenderer.color = temp;
            //}
            twinkle++;
        }
        ableObstacleHit = true;
        yield break;
    }

    void SpriteTwinkle()
    {
        Color temp = spriteRenderer.color;
        if( temp.a == 1f )
        {
            temp.a = 0.5f;
        }
        else
        {
            temp.a = 1f;
        }
        spriteRenderer.color = temp;
    }


    IEnumerator TestCoruetine(float time)
    {
        float delay = 0.0f;
        while (delay < time) 
        {
            delay += Time.deltaTime;
            yield return null;
        }
    }
    public bool QuestCheck(int condition)
    {
        bool tempE = obstacleHit;
        bool tempI = itemHit;

        obstacleHit = false;
        itemHit = false;

        switch (condition)
        {
            case 0:
                return true;

            case 1:
                return (tempE == false);

            case 2:
                return (tempE == false && tempI == true);

            default:
                return false;
        }
    }

    public void ItemReset()
    {
        clearItem = 0;
    }
}
