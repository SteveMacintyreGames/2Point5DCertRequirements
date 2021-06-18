using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
  //speed
  //gravity  
  //jumpHeight
  [SerializeField] private float _speed = 8f, _gravity =1f ,_jumpHeight = 15f;
  [SerializeField] private int _collectibles;
  //direction
  private Vector3 _direction;
  private Vector3 _velocity;
  private float _yVelocity;
  private bool _canDoubleJump;
  private bool _jumping = false;

  private CharacterController _controller;
  private Animator _anim;
  private Vector3 facing;
  private float horizontalInput, verticalInput;
  private bool _isHanging;
  private bool _climbingLadder;
  public bool facingRight;

  [SerializeField] private float _climbSpeed = 4f;
  [SerializeField] private Transform _rollPos;




    void Start()
    {
        _controller = GetComponent<CharacterController>();
        _anim = GetComponentInChildren<Animator>();
        facing = transform.localEulerAngles;
        _collectibles = 0;
    }


    void Update()
    {
        CheckController();
        FlipCharacter();
        CheckOnLedge();
        CheckOnLadder();
        CheckDirection();
        CheckRoll();

    }

    private void CheckDirection()
    {
        if (transform.rotation.y == 0)
        {
            facingRight = true;
        }
        else
        {
            facingRight = false;
        }
    }
    private void CheckRoll()
    {
        if(Input.GetKeyDown(KeyCode.LeftShift))
        {
            _controller.enabled = false;
            _anim.SetTrigger("Roll");
            StartCoroutine(RollPlayer());
        }
    }
    public void EndRoll()
    {
     
        Vector3 newPos;
        newPos = _rollPos.transform.position;
        //transform.position = newPos;
        _controller.enabled = true;


    }

    IEnumerator RollPlayer()
    {
        float distance = 9f; 
        if(facingRight)
        {   
            distance = distance;    
        }
        else
        {
            distance = -distance;
        }

       
        Vector3 newPos = transform.position;
        newPos.z += distance;
        if(facingRight)
        {
             while(transform.position.z < newPos.z)
            {
                transform.position = Vector3.MoveTowards(transform.position, newPos, 5f * Time.deltaTime);
                yield return 0;
            }
        }else
        {
              while(transform.position.z > newPos.z)
            {
                transform.position = Vector3.MoveTowards(transform.position, newPos, 5f * Time.deltaTime);
                yield return 0;
            }
        }
       
        yield return 0;
    }

    private void CheckController()
    {
        if(_controller.isGrounded)
        {
            _anim.SetBool("Jump", false);
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

       
        if(!_climbingLadder)
            { 
            _velocity.y  = _yVelocity;
            }
        _controller.Move(_velocity * Time.deltaTime);
    }

    void CheckOnLadder()
    {   if(_climbingLadder)
        {   
            verticalInput = Input.GetAxisRaw("Vertical");
            _direction = new Vector3(0f,verticalInput,0f);
            _velocity = _direction * _climbSpeed;
    

            if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow) && _climbingLadder == true)
            {
            
                _anim.speed = 1.0f;

            }else if(Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow) && _climbingLadder == true)
            {
                
                _anim.speed = 1.0f;

            }
            
            else if(Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.UpArrow) && _climbingLadder == true)
            {
                
                _anim.speed = 0;
            }
            else if(Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.DownArrow) && _climbingLadder == true)
            {
                _anim.speed = 0;
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                _anim.speed = 1f;
                _anim.SetBool("ClimbingLadder", false);
                _anim.SetBool("Jump", true);
                _anim.SetTrigger("JumpOffLadder");
                _climbingLadder=false;
                _yVelocity=_jumpHeight;
            }

        }
    }
    private void FlipCharacter()
    {
        if (horizontalInput != 0)
        {
            facing.y = _direction.z >0 ? 0:180;
            transform.localEulerAngles = facing;
        }
    }

    private void CheckOnLedge()
    {
        if(_isHanging && Input.GetKeyDown(KeyCode.E))
        {
            _anim.SetTrigger("ClimbUp");
            _isHanging = false;
        }
    }

    public void LedgeGrab(Vector3 handPos)
    {
        _controller.enabled = false;
        _anim.SetBool   ("GrabLedge" , true);
        _anim.SetFloat ("Speed",0);
        _anim.SetBool("Jump", false);
        _isHanging = true;

        transform.position = handPos;     
    }

    public void FinishedClimbing()
    {
        //Debug.Log("FinishedClimbing");
        _anim.SetBool("GrabLedge", false);
        transform.position += new Vector3 (0f, 6.68f, 1.08f);
        _controller.enabled=true;
        transform.parent = null;
        
    }

    public void GetCollectible()
    {
        _collectibles++;
        UIManager.Instance.UpdateCollectibles(_collectibles);
    }

    public void ClimbLadder()
    {
        if (horizontalInput == 0)
        {
            _anim.SetBool("ClimbingLadder", true);
            transform.position -= new Vector3(0f,-2f,0f);
            _climbingLadder = true;
            facing.y = 180;
            _anim.speed = 0;    
        }

        
    }

    public void ReachTheTop()
    {
        _climbingLadder = false;
        _controller.enabled = false;
        _anim.speed = 1f;
        Vector3 tempPos = transform.position += new Vector3(0f,-.3f,0f);
        _anim.SetBool("ClimbLadderToTop",true);
        _anim.SetBool("ClimbingLadder", false);
    }
    public void FinishedClimbingLadder()
    {
        Debug.Log("Finished Climbing Ladder");
        _anim.SetBool("ClimbLadderToTop",false);  
        _controller.enabled = true;
        transform.parent = null;
        transform.position += new Vector3(0f,3f,2f);        
    }

}
