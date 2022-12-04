using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D m_rigidBody;
    private BoxCollider2D m_boxCollider;
    private Animator m_animator;
    private SpriteRenderer m_spriteRenderer;
    public LayerMask m_ground;
    public float m_moveSpeed;
    public float m_jumpHeight;

    private float dirX;

    private enum AnimationStateEnum
    {
        Idle,
        Running,
        Jumping,
        Falling
    }

    // Start is called before the first frame update
    void Start()
    {
        dirX = 0.0f;
        m_moveSpeed = 7.0f;
        m_jumpHeight = 14.0f;
        m_rigidBody = GetComponent<Rigidbody2D>();
        m_boxCollider = GetComponent<BoxCollider2D>();
        m_animator = GetComponent<Animator>();
        m_spriteRenderer = GetComponent<SpriteRenderer>();
        m_rigidBody.freezeRotation = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (m_rigidBody != null)
        {
            dirX = Input.GetAxis("Horizontal");
            m_rigidBody.velocity = new Vector2(dirX * m_moveSpeed, m_rigidBody.velocity.y);

            if (Input.GetButtonDown("Jump") && IsGrounded())
            {
                m_rigidBody.velocity = new Vector2(m_rigidBody.velocity.x, m_jumpHeight);
            }

            UpdateValuesForAnimationTransition();
        }
    }

    private void UpdateValuesForAnimationTransition()
    {
        AnimationStateEnum animationState;

        if (dirX > 0.0f)
        {
            animationState = AnimationStateEnum.Running;
            m_spriteRenderer.flipX = false;
        }
        else if (dirX < 0.0f)
        {
            animationState = AnimationStateEnum.Running;
            m_spriteRenderer.flipX = true;
        }
        else
        {
            animationState = AnimationStateEnum.Idle;
        }
        if (m_rigidBody.velocity.y > 0.001f)
        {
            animationState = AnimationStateEnum.Jumping;
        }
        if (m_rigidBody.velocity.y < -0.001f)
        {
            animationState = AnimationStateEnum.Falling;
        }

        m_animator.SetInteger("state", (int)animationState);
    }

    private bool IsGrounded()
    {
        return Physics2D.BoxCast(m_boxCollider.bounds.center, m_boxCollider.bounds.size, 0.0f, Vector2.down, 0.1f, m_ground);
    }
}
