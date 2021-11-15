using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{

    [SerializeField]
    private Rigidbody2D rigidbody2d;

    private Vector2 friction = new Vector2(.1f, 0);

    public float speed;
    public float runSpeedMultplier;

    public float jumpForce = 20f;

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
        if(Input.GetKeyDown(KeyCode.Space)){
            rigidbody2d.velocity = Vector2.up * jumpForce;
        }
    }
}
