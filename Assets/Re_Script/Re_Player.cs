using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Re_Player : MonoBehaviour
{
    [SerializeField] PlayerManager _manager;
    protected Rigidbody2D _rigidbody;
    protected Animator _animator;
    protected BoxCollider2D _collider;
    protected SpriteRenderer _playerSprite;
    protected SoundManager _soundManager;

    [SerializeField] protected LayerMask _groundLayer;
    [SerializeField] protected LayerMask _ropeLayer;
    [SerializeField] protected LayerMask _ropeEndLayer;

    [SerializeField] protected List<GameObject> itemEffect = new List<GameObject>();

    protected float jumpPower = 23f;
    protected float gravityPower = 10f;
    protected float originXPos;

    protected int maxHp = 3;
    protected int currentHp;
    protected int maxJump = 2;
    protected int currentJump = 0;
    public int CurrentHp
    {
        get { return currentHp; }
        set
        {
            if (value >= 0 && value <= 3)
            {
                currentHp = value;
                Re_PlaySceneManager.Instance.NoticePost(EVENT_TYPE.HP_CHANGED, this);
            }
        }
    }

    public Animator _Animator { get { return _animator; } set { _animator = value; } }

    protected bool onGround = false;
    protected bool onRope = false;
    protected bool onSlide = false;
    protected bool ropeJumped = false;
    protected bool isInvincibility = false;
    protected bool getItemDouble = false;

    protected bool ableRescue = false;
    protected bool ableRope = false;

    protected Vector2 ColOffset;
    protected Vector2 ColSize; 
    protected Vector3 RopePos = Vector3.zero;

    protected WaitForSeconds twinkleDelay = new WaitForSeconds(0.2f);
    protected WaitForSeconds second = new WaitForSeconds(1f);

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _collider = GetComponent<BoxCollider2D>();
        _playerSprite = GetComponent<SpriteRenderer>();
        _soundManager = GameManager.Instance.SoundManager;
    }

    void Start()
    {
        int count = itemEffect.Count;

        for(int i = 0; i < count; i++)
        {
            itemEffect[i].SetActive(false);
        }

        CurrentHp = maxHp;
        _rigidbody.gravityScale = gravityPower;
        originXPos = transform.position.x;

        ColOffset = _collider.offset;
        ColSize = _collider.size;
    }
    
    void Update()
    {
        GroundRay();
        RopeRay();
    }    

    private void ChangeAnimation(string actionName)
    {
        switch (actionName)
        {
            case "Landing":
                _animator.SetBool("OnJump", false);
                _animator.SetBool("OnDoubleJump", false);
                _animator.SetBool("RescueDown", false);
                break;

            case "Jump":
                _animator.SetBool("OnJump", true);
                _animator.SetBool("OnSlide", false);
                break;

            case "DoubleJump":
                _animator.SetBool("OnDoubleJump", true);
                break;

            case "SlideDown":
                _animator.SetBool("OnSlide", true);
                break;

            case "SlideUp":
                _animator.SetBool("OnSlide", false);
                break;

            case "RopeHold":
                _animator.SetBool("OnRope", true);
                break;

            case "RopeRelease":
                _animator.SetBool("OnRope", false);
                break;

            case "RescueUp":
                _animator.SetTrigger("RescueUp");
                break;

            case "RescueDown":
                _animator.SetBool("RescueDown", true);
                break;

            case "Hit":
                _animator.SetTrigger("ObstacleHit");
                break;

            case "Dead":
                _animator.SetBool("OnDead", true);
                break;
        }
    }

    private void ChangeColiider(string actionName)
    {
        switch (actionName)
        {
            case "Landing":
            case "SlideUp":
                _collider.offset = ColOffset;
                _collider.size = ColSize;
                break;

            case "Jump":
                _collider.offset = new Vector2(0, 1f);
                _collider.size = new Vector2(ColSize.x, 1f);
                break;

            case "SlideDown":
                _collider.offset = ColOffset / 2;
                _collider.size = new Vector2(ColSize.x, ColSize.y / 2);
                break;
        }
    }

    private void SpriteTwinkle(SpriteRenderer _spriteRenderer)
    {
        Color temp = _spriteRenderer.color;
        temp.a = temp.a == 1f ? 0.5f : 1f;

        _spriteRenderer.color = temp;
    }

    // INPUT FUNCTION
    #region InputFunc

    public void LeftDown()
    {
        Jump();
    }

    public void LeftStay()
    {

    }

    public void LeftUp()
    {

    }

    public void RightDown()
    {
        RopeHold();
    }

    public void RightStay()
    {
        SlideDown();
    }

    public void RightUp()
    {
        SlideUp();
        RopeRelease();
    }

    #endregion

    // PLAYER ACTION
    #region Action

    private void OnLanding()
    {
        currentJump = 0;
        onGround = true;
        ChangeAnimation("Landing");
        ChangeColiider("Landing");
        _collider.isTrigger = false;

        if(ropeJumped == true)
        {
            ropeJumped = false;
        }
    }

    private void Jump()
    {
        if (currentJump >= maxJump)
            return;

        if(currentJump == 0)
        {
            ChangeAnimation("Jump");
            ChangeColiider("Jump");
            _collider.isTrigger = true;
        }
        else
        {
            ChangeAnimation("DoubleJump");
        }

        _soundManager.PlaySFX("jump");
        onGround = false;
        onSlide = false;
        currentJump++;
        _rigidbody.velocity = new Vector2(0f, jumpPower);
    }

    private void SlideDown()
    {
        if (onGround == false || onSlide == true)
            return;

        _soundManager.PlaySFX("sliding");
        onSlide = true;
        ChangeAnimation("SlideDown");
        ChangeColiider("SlideDown");
    }

    private void SlideUp()
    {
        if (onGround == false || onSlide == false)
            return;

        onSlide = false;
        ChangeAnimation("SlideUp");
        ChangeColiider("SlideUp");
    }

    private void RopeHold()
    {
        if(ableRope == false || ropeJumped == true)
            return;

        _soundManager.PlaySFX("Rope");
        onRope = true;
        _rigidbody.gravityScale = 0f;
        _rigidbody.velocity = Vector2.zero;
        currentJump = maxJump;
        ChangeAnimation("RopeHold");
    }

    private void RopeRelease()
    {
        if (onRope == false)
            return;

        onRope = false;
        ropeJumped = true;
        RopePos = Vector3.zero;
        _rigidbody.gravityScale = gravityPower;
        _rigidbody.velocity = new Vector2(0f, jumpPower);
        ChangeAnimation("RopeRelease");
    }

    private IEnumerator Rescue()
    {
        isInvincibility = true;
        currentJump = maxJump;
        _rigidbody.gravityScale = 0;
        _rigidbody.velocity = new Vector2(0f, 5f);
        ChangeAnimation("RescueUp");

        while (true)
        {
            if (transform.position.y >= -1)
            {
                _rigidbody.gravityScale = gravityPower;
                ableRescue = false;
                ChangeAnimation("RescueDown");
                
                if(itemEffect[2].activeSelf == true)
                {
                    itemEffect[2].SetActive(false);
                }                
            }

            if (onGround == true)
            {
                for (int i = 0; i < 6; i++)
                {
                    SpriteTwinkle(_playerSprite);
                    yield return twinkleDelay;
                }
                isInvincibility = false;
                yield break;
            }
            yield return null;
        }        
    }

    private IEnumerator Hit()
    {
        isInvincibility = true;
        ChangeAnimation("Hit");

        for (int i = 0; i < 6; i++)
        {
            SpriteTwinkle(_playerSprite);
            yield return twinkleDelay;
        }

        isInvincibility = false;
        yield break;
    }

    private IEnumerator Dead()
    {        
        ChangeAnimation("Dead");
        ChangeAnimation("Hit");
        Re_PlaySceneManager.Instance.NoticePost(EVENT_TYPE.PLAYER_DEAD, this, null);
        for (int i = 0; i < 3; i++)
        {
            yield return second;
        }
        Re_PlaySceneManager.Instance.GameOver();
        yield break;
    }

    #endregion

    // ITEM FUNCTION
    #region Item

    private void UseItem(int itemID)
    {
        switch (itemID)
        {
            case 0:
                if (getItemDouble == true) Re_PlaySceneManager.Instance.ClearItem += 2;
                else Re_PlaySceneManager.Instance.ClearItem++;
                break;

            case 1:
                StartCoroutine(Item_Fire());
                break;

            case 2:
                StartCoroutine(Item_Water());
                break;

            case 3:
                Item_Forest();
                break;

            case 4:
                StartCoroutine(Item_Angel());
                break;

            default:
                break;
        }
    }

    private IEnumerator Item_Fire()
    {
        itemEffect[0].SetActive(true);
        isInvincibility = true;
        Re_PlaySceneManager.Instance.SetSpeed(2f);

        yield return new WaitForSeconds(2f);

        itemEffect[0].SetActive(false);
        Re_PlaySceneManager.Instance.SetSpeed(1f);

        for (int i = 0; i < 6; i++)
        {
            SpriteTwinkle(_playerSprite);
            yield return twinkleDelay;
        }

        isInvincibility = false;
    }

    private IEnumerator Item_Water()
    {
        itemEffect[1].SetActive(true);
        _manager.OnMagnet();

        yield return new WaitForSeconds(5f);

        itemEffect[1].SetActive(false);
        _manager.OffMagnet();
    }

    private void Item_Forest()
    {
        itemEffect[2].SetActive(true);
        ableRescue = true;
    }

    private IEnumerator Item_Angel()
    {
        SpriteRenderer _sprite = itemEffect[3].GetComponent<SpriteRenderer>();
        itemEffect[3].SetActive(true);
        isInvincibility = true;
        getItemDouble = true;

        yield return new WaitForSeconds(4f);

        for (int i = 0; i < 6; i++)
        {
            SpriteTwinkle(_sprite);
            yield return twinkleDelay;
        }

        itemEffect[3].SetActive(false);
        getItemDouble = false;

        isInvincibility = false;
    }

    #endregion

    private void GroundRay()
    {
        Debug.DrawRay(transform.position + Vector3.left, Vector2.down * 0.3f, Color.green);
        Debug.DrawRay(transform.position + Vector3.right, Vector2.down * 0.3f, Color.green);

        if (Physics2D.Raycast(transform.position, Vector2.down, 0.6f, _groundLayer))//(Physics2D.Raycast(transform.position + Vector3.left, Vector2.down, 0.6f, _groundLayer) || Physics2D.Raycast(transform.position + Vector3.right, Vector2.down, 0.6f, _groundLayer))
        {
            if (onGround == false && _rigidbody.velocity.y < 0)
            {
                OnLanding();
            }            
        }
        else
        {
            if (onGround == true)
            {
                onGround = false;
                _collider.isTrigger = true;
            }
        }
    }

    private void RopeRay()
    {
        Vector3 startPos = transform.position + new Vector3(-0.5f, 2f, 0f);
        RaycastHit2D raycastHit = Physics2D.Raycast(startPos, Vector2.right, 1f, _ropeLayer);
        Debug.DrawRay(startPos, Vector2.right, Color.green);

        if (raycastHit.collider != null && ableRope == false)
        {
            ableRope = true;
        }
        else if (raycastHit.collider != null && onRope == true)
        {
            if (RopePos == Vector3.zero)
            {
                RopePos = raycastHit.collider.gameObject.transform.position;
            }
            raycastHit.collider.gameObject.transform.position = RopePos;
        }
        else if (raycastHit.collider == null && ableRope == true)
        {
            ableRope = false;
        }

        if (Physics2D.Raycast(startPos, Vector2.right, 1f, _ropeEndLayer))
        {
            if (onRope == true)
            {
                RopeRelease();
                ropeJumped = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Item") == true)
        {
            ItemBase item = collision.gameObject.GetComponent<ItemBase>();
            _soundManager.PlaySFX("item");
            UseItem(item.ItemID);
            collision.gameObject.SetActive(false);
        }

        if (collision.gameObject.gameObject.CompareTag("Obstacle") == true)
        {
            if (itemEffect[0].gameObject.activeSelf == true)
            {
                SpriteRenderer spriteRenderer = collision.gameObject.GetComponent<SpriteRenderer>();
                _manager.AniPool.PlayObstacleAni(collision.transform.position, spriteRenderer.sprite);
                collision.gameObject.SetActive(false);
            }
            else if (isInvincibility == false)
            {
                CurrentHp--;
                _soundManager.PlaySFX("crach");
                if (CurrentHp == 0)
                {
                    _soundManager.PlaySFX("over");
                    _manager.InputOff();
                    StartCoroutine(Dead());
                }
                else
                {
                    StartCoroutine(Hit());
                }
            }
        }

        if (collision.gameObject.CompareTag("DropZone") == true)
        {
            if (ableRescue == true)
            {
                StartCoroutine(Rescue());
            }
            else
            {
                CurrentHp = 0;
                StartCoroutine(Dead());
            }
        }
    }
}
