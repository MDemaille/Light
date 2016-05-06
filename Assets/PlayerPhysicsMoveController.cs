using UnityEngine;
using System.Collections;

public class PlayerPhysicsMoveController : MoveController
{
    public float JumpIntensity = 20.0f;

    private Rigidbody2D _rigidbody;

    private bool _isOnGround = true;

	void Start ()
	{
	    _rigidbody = GetComponent<Rigidbody2D>();
	}

    public override void Update()
    {
        _rigidbody.velocity = new Vector2(Input.GetAxisRaw("Horizontal")* Speed, _rigidbody.velocity.y);

        if (Input.GetButtonDown("Jump") && _isOnGround)
        {
            _isOnGround = false;
            Jump();
        }
    }

    public void Jump()
    {
        _rigidbody.AddForce(new Vector2(0, JumpIntensity), ForceMode2D.Impulse);
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(Tags.Ground))
        {
            _isOnGround = true;
        }
    }

    
}
