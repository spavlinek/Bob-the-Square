using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    private float horizontal;
    public float jumpStrength;
    private Rigidbody2D _rigidbody;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] LayerMask wallLayer;
    private bool isFacingRight = true;

    private BoxCollider2D _boxCollider2D;
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _boxCollider2D = GetComponent<BoxCollider2D>();
        if (_rigidbody == null)
        {
            Debug.LogError(message:"Player does not have rigidbody");
        }
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        if (IsOnWall())
        {
            Debug.Log("on wall right now");
            Vector2 force = new Vector2(horizontal,vertical);
            force = force * speed;
            _rigidbody.AddForce(force);
        }
        else
        {
            Vector2 force = new Vector2(horizontal,0);
            force = force * speed;
            _rigidbody.AddForce(force);
        }
        

        //jump
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            Vector2 currentVelocity = _rigidbody.velocity;
            currentVelocity.y = jumpStrength;
            _rigidbody.velocity = currentVelocity;
        }

        Flip();


    }

    private void OnCollisionEnter(Collision other)
    {
        
        
    }

    private bool IsGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(_boxCollider2D.bounds.center, _boxCollider2D.bounds.size, 0,
            Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null;
    }
    
    private bool IsOnWall()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(_boxCollider2D.bounds.center, _boxCollider2D.bounds.size, 0,
            new Vector2(transform.localScale.x, 0), 0.1f, wallLayer);
        return raycastHit.collider != null;
    }

    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector2 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }
}
