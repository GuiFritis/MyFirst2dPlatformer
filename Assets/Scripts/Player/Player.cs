using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{

    [SerializeField]
    private Rigidbody2D rigidbody2d;

    [SerializeField]
    private SpriteRenderer sprite;

    [Header("Animation")]
    [SerializeField]
    private Animator animator;
    public string boolRun = "Running";


    [Header("Speed Setup")]
    [SerializeField]
    private Vector2 friction = new Vector2(.1f, 0);
    public float speed;
    public float runSpeedMultplier;
    public float airSpeedMultiplier;

    [Header("Jump Setup")]
    public float jumpForce = 20f;

    [Header("Jump Animation Setup")]
    public float jumpScaleY = 1.5f;
    public float jumpScaleX = .7f;
    public float jumpAnimationDur = .2f;
    public float landAnimationDur = .2f;
    public Ease jumpEase = Ease.OutBack;

    [Header("Move Animation Setup")]
    public float moveAnimationDur = .2f;
    public Ease moveEase = Ease.OutBack;

    [Header("Damage")]
    public float dmgDur = .2f;
    public Ease dmgEase = Ease.InBack;

    private bool grounded = true;
    private bool doubleJumped = false;
    private float _curSpeed;

    // Update is called once per frame
    void Update()
    {
        Jump();
        Move();
    }

    private void Move(){

        if(!grounded){
            _curSpeed = speed * airSpeedMultiplier;
            animator.speed = 1;
        } else if(Input.GetKey(KeyCode.LeftShift)){
            _curSpeed = speed * runSpeedMultplier;
            animator.speed = runSpeedMultplier;
        } else {
            _curSpeed = speed;
            animator.speed = 1;
        }

        if(Input.GetKey(KeyCode.RightArrow)){
            if(rigidbody2d.transform.localScale.x != 1){
                rigidbody2d.transform.DOScaleX(1, moveAnimationDur).SetEase(moveEase);
            }
            animator.SetBool(boolRun, true);
            rigidbody2d.velocity = new Vector2(_curSpeed, rigidbody2d.velocity.y);
        } else if(Input.GetKey(KeyCode.LeftArrow)){
            if(rigidbody2d.transform.localScale.x != -1){
                rigidbody2d.transform.DOScaleX(-1, moveAnimationDur).SetEase(moveEase);
            }
            animator.SetBool(boolRun, true);
            rigidbody2d.velocity = new Vector2(-_curSpeed, rigidbody2d.velocity.y);
        } else {
            animator.SetBool(boolRun, false);
        }

        if(rigidbody2d.velocity.x > 0){
            rigidbody2d.velocity -= friction;
        } else if (rigidbody2d.velocity.x < 0){
            rigidbody2d.velocity += friction;
        }
    }

    private void Jump(){
        if(Input.GetKeyDown(KeyCode.Space) && (grounded || !doubleJumped)){
            // DOTween.Kill(rigidbody2d.transform);
            // rigidbody2d.transform.localScale = Vector2.one;
            AnimateJump();

            rigidbody2d.velocity = Vector2.up * jumpForce;

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

    private void AnimateJump(){
        // rigidbody2d.transform.DOScaleY(jumpScaleY, jumpAnimationDur).SetLoops(2, LoopType.Yoyo).SetEase(jumpEase);
        // rigidbody2d.transform.DOScaleX(jumpScaleX, jumpAnimationDur).SetLoops(2, LoopType.Yoyo).SetEase(jumpEase);
        animator.SetBool("Jumping", true);
    }

    private void AnimateFall(){
        if(!grounded){
            animator.SetBool("Falling", true);
            animator.SetBool("Jumping", false);
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
        animator.SetTrigger("Land");        
        animator.SetBool("Falling", false);
        animator.SetBool("Jumping", false);
    }

    public void onDamageTaken(){
        DOTween.Kill(sprite);
        sprite.DOColor(Color.red, dmgDur).SetEase(dmgEase).From();
    }

}
