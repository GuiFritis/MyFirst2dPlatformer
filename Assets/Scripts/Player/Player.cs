using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    [Header("Components")]
    [SerializeField]
    private Rigidbody2D rigidbody2d;

    [SerializeField]
    private SpriteRenderer sprite;
    
    [SerializeField]
    private HealthBase healthBase;

    [Header("Setup")]
    public SOPlayerSetup soPlayerSetup;

    private bool grounded = true;
    private bool doubleJumped = false;
    private float _curSpeed;
    private Animator _curPlayer;

    void Awake()
    {
        if(healthBase != null){
            healthBase.OnDeath += OnPlayerDeath;
        }

        _curPlayer = Instantiate(soPlayerSetup.player, transform);
    }

    // Update is called once per frame
    void Update()
    {
        Jump();
        Move();
    }

    private void Move(){

        if(!grounded){
            _curSpeed = soPlayerSetup.speed * soPlayerSetup.airSpeedMultiplier;
            _curPlayer.speed = 1;
        } else if(Input.GetKey(KeyCode.LeftShift)){
            _curSpeed = soPlayerSetup.speed * soPlayerSetup.runSpeedMultplier;
            _curPlayer.speed = soPlayerSetup.runSpeedMultplier;
        } else {
            _curSpeed = soPlayerSetup.speed;
            _curPlayer.speed = 1;
        }

        if(Input.GetKey(KeyCode.RightArrow)){
            if(rigidbody2d.transform.localScale.x != 1){
                rigidbody2d.transform.DOScaleX(1, soPlayerSetup.moveAnimationDur).SetEase(soPlayerSetup.moveEase);
            }
            _curPlayer.SetBool(soPlayerSetup.boolRun, true);
            rigidbody2d.velocity = new Vector2(_curSpeed, rigidbody2d.velocity.y);
        } else if(Input.GetKey(KeyCode.LeftArrow)){
            if(rigidbody2d.transform.localScale.x != -1){
                rigidbody2d.transform.DOScaleX(-1, soPlayerSetup.moveAnimationDur).SetEase(soPlayerSetup.moveEase);
            }
            _curPlayer.SetBool(soPlayerSetup.boolRun, true);
            rigidbody2d.velocity = new Vector2(-_curSpeed, rigidbody2d.velocity.y);
        } else if(rigidbody2d.velocity.x > -soPlayerSetup.moveAnimationStop && rigidbody2d.velocity.x < soPlayerSetup.moveAnimationStop){
            _curPlayer.SetBool(soPlayerSetup.boolRun, false);
        }

        if(rigidbody2d.velocity.x > 0){
            rigidbody2d.velocity -= soPlayerSetup.friction;
        } else if (rigidbody2d.velocity.x < 0){
            rigidbody2d.velocity += soPlayerSetup.friction;
        } 
    }

    private void Jump(){
        if(Input.GetKeyDown(KeyCode.Space) && (grounded || !doubleJumped)){
            // DOTween.Kill(rigidbody2d.transform);
            // rigidbody2d.transform.localScale = Vector2.one;
            AnimateJump();

            rigidbody2d.velocity = Vector2.up * soPlayerSetup.jumpForce;

            if(grounded){
                grounded = false;
            } else {
                doubleJumped = true;
            }
        }

        if(rigidbody2d.velocity.y < -rigidbody2d.gravityScale){
            AnimateFall();
        }
    }

    private void OnPlayerDeath(){
        healthBase.OnDeath -= OnPlayerDeath;

        _curPlayer.SetTrigger(soPlayerSetup.triggerDie);
    }

    private void AnimateJump(){
        // rigidbody2d.transform.DOScaleY(jumpScaleY, jumpAnimationDur).SetLoops(2, LoopType.Yoyo).SetEase(jumpEase);
        // rigidbody2d.transform.DOScaleX(jumpScaleX, jumpAnimationDur).SetLoops(2, LoopType.Yoyo).SetEase(jumpEase);
        _curPlayer.SetBool("Jumping", true);
    }

    private void AnimateFall(){
        if(!grounded){
            _curPlayer.SetBool("Falling", true);
            _curPlayer.SetBool("Jumping", false);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Ground") && !grounded){
            // DOTween.Kill(rigidbody2d.transform);
            // rigidbody2d.transform.localScale = Vector2.one;
            AnimateLanding();
            grounded = true;
            doubleJumped = false;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Ground") && grounded){
            grounded = false;
            AnimateFall();
        }
    }

    private void AnimateLanding(){
        // rigidbody2d.transform.DOScaleY(jumpScaleX, landAnimationDur).SetLoops(2, LoopType.Yoyo).SetEase(jumpEase);
        // rigidbody2d.transform.DOScaleX(jumpScaleY, landAnimationDur).SetLoops(2, LoopType.Yoyo).SetEase(jumpEase);
        _curPlayer.SetTrigger("Land");        
        _curPlayer.SetBool("Falling", false);
        _curPlayer.SetBool("Jumping", false);
    }

    public void DestroyMe()
    {
        Destroy(gameObject);
    }

}
