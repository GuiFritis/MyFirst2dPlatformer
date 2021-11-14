using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{

    [SerializeField]
    private Rigidbody2D rigidbody2d;

    public float speed;

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move(){
        if(Input.GetKey(KeyCode.RightArrow)){
            // rigidbody2d.MovePosition(rigidbody2d.position + velocity * Time.deltaTime);
            rigidbody2d.velocity = new Vector2(speed, rigidbody2d.velocity.y);
        } else if(Input.GetKey(KeyCode.LeftArrow)){
            // rigidbody2d.MovePosition(rigidbody2d.position - velocity * Time.deltaTime);
            rigidbody2d.velocity = new Vector2(-speed, rigidbody2d.velocity.y);
        }
    }
}
