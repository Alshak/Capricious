using Assets.Code.AI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloneController : MonoBehaviour
{
    private Animator m_Anim;            // Reference to the player's animator component.
    private Rigidbody2D m_Rigidbody2D;

    private Transform m_GroundCheck;    // A position marking where to check if the player is grounded.
    const float k_GroundedRadius = .2f; // Radius of the overlap circle to determine if grounded
    private bool m_Grounded;            // Whether or not the player is grounded.

    [SerializeField] private LayerMask m_WhatIsGround;                  // A mask determining what is ground to the character
    [SerializeField] private float m_MaxWalkingSpeed = 0.5f;                    // The fastest the player can travel in the x axis.


    private bool m_FacingRight = true;  // For determining which way the player is currently facing.
    public bool IsFacingRight { get { return m_FacingRight; } }
    private Vector3 stopPoint;
    private bool isClapping;
    public void SetDirection(AIDirection direction, Vector3 stopPoint)
    {
        if (direction == AIDirection.Right)
        {
            m_MaxWalkingSpeed = Mathf.Abs(m_MaxWalkingSpeed);
        }
        else
        {
            m_MaxWalkingSpeed = Mathf.Abs(m_MaxWalkingSpeed) * -1;
        }

        this.stopPoint = stopPoint;
    }

    private void Awake()
    {
        // Setting up references.
        m_GroundCheck = transform.Find("GroundCheck");
        m_Anim = GetComponent<Animator>();
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
        isClapping = false;
    }

    private void Update()
    {
        if (m_FacingRight)
        {
            if (transform.position.x > stopPoint.x && !isClapping)
            {
                m_Anim.SetTrigger("Clap");
                isClapping = true;
                m_Rigidbody2D.velocity = Vector2.zero;
            }
        }
        else
        {
            if (transform.position.x < stopPoint.x && !isClapping)
            {
                m_Anim.SetTrigger("Clap");
                isClapping = true;
                m_Rigidbody2D.velocity = Vector2.zero;
            }
        }
    }
    private void FixedUpdate()
    {
        if (isClapping)
        {
            if (m_FacingRight && transform.position.x > 0.5f)
            {
                Flip();
            }
            else if (!m_FacingRight && transform.position.x < 0.5f)
            {
                Flip();
            }
        }


        m_Grounded = false;
        // The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
        // This can be done using layers instead but Sample Assets will not overwrite your project settings.
        Collider2D[] groundColliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
        for (int i = 0; i < groundColliders.Length; i++)
        {
            if (groundColliders[i].gameObject != gameObject)
            {
                m_Grounded = true;
            }
        }
        m_Anim.SetBool("Ground", m_Grounded);

        float move = 0;

        if (!isClapping)
        {
            move = m_MaxWalkingSpeed;
            m_Rigidbody2D.velocity = new Vector2(move, m_Rigidbody2D.velocity.y);
        }

        // If the input is moving the player right and the player is facing left...
        if (move > 0 && !m_FacingRight)
        {
            // ... flip the player.
            Flip();
        }
        // Otherwise if the input is moving the player left and the player is facing right...
        else if (move < 0 && m_FacingRight)
        {
            // ... flip the player.
            Flip();
        }
        m_Anim.SetFloat("Speed", Mathf.Abs(move));
    }

    private void Flip()
    {
        // Switch the way the player is labelled as facing.
        m_FacingRight = !m_FacingRight;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
