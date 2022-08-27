using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private AudioSource jumpSound;
    [SerializeField] private float speedX = -1f;
    [SerializeField] private Animator animator;
    [SerializeField] private Transform playerModelTransform;


    private FixedJoystick _fixedJoystick;
    private Rigidbody2D _rb;
    private Finish _finish;
    private LeverArm _leverArm;

    private float _horizontal = 0f;
    const float _speedXMultiplier = 200f;

 
    private bool _isGround = false;
    private bool _isJump = false;
    private bool _isFacingRight = true;
    private bool _is_finish = false;
    private bool _isLevelArm;
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _finish = GameObject.FindGameObjectWithTag ("Finish").GetComponent<Finish>();
        _fixedJoystick = GameObject.FindGameObjectWithTag ("Fixed Joystick").GetComponent<FixedJoystick>();
        _leverArm = FindObjectOfType<LeverArm>();
    }

    // Update is called once per frame
    void Update()
    {
      //  _horizontal = Input.GetAxis("Horizontal");
        _horizontal = _fixedJoystick.Horizontal;
        animator.SetFloat("speedX", Mathf.Abs(_horizontal));
        if (Input.GetKeyDown(KeyCode.W) && _isGround)
        {
            Jump();
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            Interact();
        }  
    }

    private void FixedUpdate()
    {
        _rb.velocity = new Vector2(speedX * _horizontal * _speedXMultiplier * Time.fixedDeltaTime, _rb.velocity.y);

        if (_isJump)
        {
            _rb.AddForce(new Vector2(0f, 400f));
            _isGround = false;
            _isJump = false;
        }
        if(_horizontal > 0f && !_isFacingRight)
        {
            Flip();
        }
        else if(_horizontal <0f && _isFacingRight)
        {
            Flip();
        }
    }
     void Flip()
    {
        _isFacingRight = !_isFacingRight;
        Vector3 playerScale = playerModelTransform.localScale;
        playerScale.x *= -1;
        playerModelTransform.localScale = playerScale;
    }

    public void Jump()
    {
        if (_isGround)
        {
            _isJump = true;
            jumpSound.Play();
        }
    }
    public void Interact()
    {
        if (_is_finish)
        {
            _finish.FinishLevel();
        }
        if (_isLevelArm)
        {
            _leverArm.ActivateLeverArm();
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            _isGround = true;
        }
        if (collision.gameObject.CompareTag("Finish")){
            
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        LeverArm _leverArmTemp = collision.GetComponent<LeverArm>();
        if (collision.CompareTag("Finish"))
        {
            _is_finish = false;
        }
        if(_leverArm != null)
        {
            _isLevelArm = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        LeverArm _leverArm = collision.GetComponent<LeverArm>();
        if (collision.CompareTag("Finish"))
        {
            _is_finish = true;
        }
        if (_leverArm != null)
        {
            _isLevelArm = true;
        }
    }

}
