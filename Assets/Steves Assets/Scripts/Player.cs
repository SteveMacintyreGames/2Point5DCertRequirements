using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
  //speed
  //gravity  
  //jumpHeight
  [SerializeField] private float _speed = 8f, _gravity =1f ,_jumpHeight = 15f;
  //direction
  private Vector3 _direction;
  private Vector3 _velocity;
  private float _yVelocity;
  private bool _canDoubleJump;
  private bool _jumping = false;

  private CharacterController _controller;
  private Animator _anim;
  private Vector3 facing;
  private float horizontalInput;




    void Start()
    {
        _controller = GetComponent<CharacterController>();
        _anim = GetComponentInChildren<Animator>();
        facing = transform.localEulerAngles;
    }


    void Update()
    {

        if(_controller.isGrounded)
        {
            horizontalInput = Input.GetAxisRaw("Horizontal");
            _direction = new Vector3(0f,0f,horizontalInput);
            _velocity = _direction * _speed;

            if(_jumping)
            {
                _jumping = false;
                _anim.SetBool("Jump", _jumping);
            }
            

                
            _anim.SetFloat("Speed", Mathf.Abs(horizontalInput));
            

            //if jump
            if(Input.GetKeyDown(KeyCode.Space))
            {
                //adjust jump height
                _yVelocity = _jumpHeight;
                _canDoubleJump = true;
                _jumping = true;
                _anim.SetBool("Jump", _jumping);
            }

        }
        else
        {
            if(Input.GetKeyDown(KeyCode.Space)&&_canDoubleJump==true)
            {
                _yVelocity += _jumpHeight;
                _canDoubleJump = false;
            }
            _yVelocity -= _gravity * Time.deltaTime;
        }

         
        _velocity.y  = _yVelocity;
        _controller.Move(_velocity * Time.deltaTime);
       
        
        if (horizontalInput != 0)
        {
            facing.y = _direction.z >0 ? 0:180;
            transform.localEulerAngles = facing;
        }


    }

    public void LedgeGrab(Vector3 handPos)
    {
        _controller.enabled = false;
        _anim.SetBool("GrabLedge", true);
        transform.position = handPos;        
    }
}
