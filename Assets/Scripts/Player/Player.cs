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

    [Header("Speed Setup")]
    [SerializeField]
    private Vector2 friction = new Vector2(.1f, 0);
    public float speed;
    public float runSpeedMultplier;

    [Header("Jump Setup")]
    public float jumpForce = 20f;

    [Header("Jump Animation Setup")]
    public float jumpScaleY = 1.5f;
    public float jumpScaleX = .7f;
    public float jumpAnimationDur = .2f;
    public float landAnimationDur = .2f;

    public float jumpRotationDur = .5f;

    public Ease jumpRotationEase = Ease.InBack;
    public Ease jumpEase = Ease.OutBack;

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

        _curSpeed = speed * (Input.GetKey(KeyCode.LeftShift)? runSpeedMultplier : 1);

        if(Input.GetKey(KeyCode.RightArrow)){
            // rigidbody2d.MovePosition(rigidbody2d.position + velocity * Time.deltaTime);
            rigidbody2d.velocity = new Vector2(_curSpeed, rigidbody2d.velocity.y);
        } else if(Input.GetKey(KeyCode.LeftArrow)){
            // rigidbody2d.MovePosition(rigidbody2d.position - velocity * Time.deltaTime);
            rigidbody2d.velocity = new Vector2(-_curSpeed, rigidbody2d.velocity.y);
        }

        if(rigidbody2d.velocity.x > 0){
            rigidbody2d.velocity -= friction;
        } else if (rigidbody2d.velocity.x < 0){
            rigidbody2d.velocity += friction;
        }
    }

    private void Jump(){
        if(Input.GetKeyDown(KeyCode.Space) && (grounded || !doubleJumped)){
            DOTween.Kill(rigidbody2d.transform);
            rigidbody2d.transform.localScale = Vector2.one;

            rigidbody2d.velocity = Vector2.up * jumpForce;

            if(grounded){
                AnimateJump();
                grounded = false;
            } else {
                AnimateDoubleJump();
                doubleJumped = true;
            }
        }
    }

    private void AnimateJump(){
        rigidbody2d.transform.DOScaleY(jumpScaleY, jumpAnimationDur).SetLoops(2, LoopType.Yoyo).SetEase(jumpEase);
        rigidbody2d.transform.DOScaleX(jumpScaleX, jumpAnimationDur).SetLoops(2, LoopType.Yoyo).SetEase(jumpEase);
    }

    private void AnimateDoubleJump(){
        rigidbody2d.transform.DORotate(new Vector3(0, 0, 360), jumpRotationDur, RotateMode.FastBeyond360).SetEase(jumpEase).SetRelative();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Ground") && !grounded){
            DOTween.Kill(rigidbody2d.transform);
            rigidbody2d.transform.localScale = Vector2.one;
            AnimateLanding();
            grounded = true;
            doubleJumped = false;
        }
    }

    private void AnimateLanding(){
        rigidbody2d.transform.DOScaleY(jumpScaleX, landAnimationDur).SetLoops(2, LoopType.Yoyo).SetEase(jumpEase);
        rigidbody2d.transform.DOScaleX(jumpScaleY, landAnimationDur).SetLoops(2, LoopType.Yoyo).SetEase(jumpEase);
    }

    public void onDamageTaken(){
        DOTween.Kill(sprite);
        sprite.DOColor(Color.red, dmgDur).SetEase(dmgEase).From();
    }

}
