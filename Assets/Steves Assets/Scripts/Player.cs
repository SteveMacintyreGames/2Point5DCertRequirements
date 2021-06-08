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

  private CharacterController _controller;

    void Start()
    {
        _controller = GetComponent<CharacterController>();

    }


    void Update()
    {

        

        //if grounded
        if(_controller.isGrounded)
        {
            float horizontalInput = Input.GetAxis("Horizontal");
            _direction = new Vector3(0f,0f,horizontalInput);
            _velocity = _direction * _speed;
            //do nothing

            //if jump
            if(Input.GetKeyDown(KeyCode.Space))
            {
                //adjust jump height
                _yVelocity = _jumpHeight;
                _canDoubleJump = true;
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
       
        
        //calculate direction based on user inputs


    }
}
