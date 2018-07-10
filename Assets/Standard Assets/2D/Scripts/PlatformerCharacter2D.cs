using System;
using UnityEngine;

namespace UnityStandardAssets._2D
{
    public class PlatformerCharacter2D : MonoBehaviour
    {
        [SerializeField] private float m_MaxWalkingSpeed = 10f;                    // The fastest the player can travel in the x axis.
        [SerializeField] private float m_MaxRunningSpeed = 15f;                    // The fastest the player can travel in the x axis.
        [SerializeField] private float m_JumpForce = 400f;                  // Amount of force added when the player jumps.
        [SerializeField] private float m_ThrowForce = 50f;                  // Amount of force added when the player jumps.
        [Range(0, 4)] [SerializeField] private float m_CrouchSpeed = .36f;  // Amount of maxSpeed applied to crouching movement. 1 = 100%
        [SerializeField] private bool m_AirControl = false;                 // Whether or not a player can steer while jumping;
        [SerializeField] private LayerMask m_WhatIsGround;                  // A mask determining what is ground to the character
        [SerializeField] private LayerMask m_WhatIsWall;                  // A mask determining what is a wall to the character
        [SerializeField] private float m_SlideDuration = 2f;
        [SerializeField] private GameObject ThrowableTemplate;
        [Range(0, 1)] [SerializeField] private float m_AirControlBlockAfterWallJump = 1f;
        private Transform m_GroundCheck;    // A position marking where to check if the player is grounded.
        const float k_GroundedRadius = .2f; // Radius of the overlap circle to determine if grounded
        private bool m_Grounded;            // Whether or not the player is grounded.
        private Transform m_CeilingCheck;   // A position marking where to check for ceilings
        const float k_CeilingRadius = .01f; // Radius of the overlap circle to determine if the player can stand up
        private Animator m_Anim;            // Reference to the player's animator component.
        private Rigidbody2D m_Rigidbody2D;
        private bool m_FacingRight = true;  // For determining which way the player is currently facing.
        private float m_CurrentSlideDuration = 1f;
        private float m_CurrentSlideSpeed = 1f;
        private Transform m_WallCheck;
        const float k_WallJumpRadius = .2f; // Radius of the overlap circle to determine if player can wall jump
        private bool m_TouchingWall;            // Whether or not the player can wall jump.
        private bool m_CanAirControl = false;
        private float m_AirControlTimerValue = 1f;

        private void Awake()
        {
            // Setting up references.
            m_GroundCheck = transform.Find("GroundCheck");
            m_CeilingCheck = transform.Find("CeilingCheck");
            m_WallCheck = transform.Find("WallCheck");
            m_Anim = GetComponent<Animator>();
            m_Rigidbody2D = GetComponent<Rigidbody2D>();
        }


        private void FixedUpdate()
        {
            if (!m_CanAirControl)
            {
                m_AirControlTimerValue -= Time.deltaTime;
                if (m_AirControlTimerValue < 0f)
                {
                    m_CanAirControl = true;
                }
            }
            m_Grounded = false;
            m_TouchingWall = false;
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


            Collider2D[] wallJumpColliders = Physics2D.OverlapCircleAll(m_WallCheck.position, k_WallJumpRadius, m_WhatIsWall);
            for (int i = 0; i < wallJumpColliders.Length; i++)
            {
                if (wallJumpColliders[i].gameObject != gameObject)
                {
                    m_TouchingWall = true;
                }
            }
            m_Anim.SetBool("TouchingWall", m_TouchingWall);
            // Set the vertical animation
            m_Anim.SetFloat("vSpeed", m_Rigidbody2D.velocity.y);
        }


        public void Move(float move, bool crouch, bool run, bool throwing, bool jump)
        {
            bool goingRight = Mathf.Sign(move) > 0;
            if (m_TouchingWall && !jump && m_Grounded)
            {
                if ((!m_FacingRight && !goingRight) || (m_FacingRight && goingRight))
                {
                    m_Anim.SetFloat("Speed", 0f);
                    return;
                }
            }
            // If crouching, check to see if the character can stand up
            if (!crouch && m_Anim.GetBool("Crouch"))
            {
                // If the character has a ceiling preventing them from standing up, keep them crouching
                if (Physics2D.OverlapCircle(m_CeilingCheck.position, k_CeilingRadius, m_WhatIsGround))
                {
                    crouch = true;
                }
            }

            // Set whether or not the character is crouching in the animator
            if (m_Anim.GetBool("Crouch") != crouch)
            {
                m_Anim.SetBool("Crouch", crouch);
            }

            if (throwing)
            {
                m_Anim.SetTrigger("Throw");
                GameObject throwable = Instantiate(ThrowableTemplate, m_WallCheck.position, Quaternion.identity);
                float isRightJump = -1;
                if (m_FacingRight)
                {
                    isRightJump = 1;
                }
                throwable.GetComponent<Rigidbody2D>().AddForce(new Vector2(isRightJump * m_ThrowForce, m_ThrowForce));
            }

            //only control the player if grounded or airControl is turned on
            if (m_Grounded || (m_AirControl && m_CanAirControl))
            {
                if (crouch)
                {
                    m_CurrentSlideDuration += Time.deltaTime;
                    m_CurrentSlideSpeed = m_CurrentSlideSpeed * ((m_SlideDuration - m_CurrentSlideDuration) / m_SlideDuration);
                    if (m_CurrentSlideSpeed < .1)
                    {
                        m_CurrentSlideSpeed = 0;
                    }
                    // Reduce the speed if crouching by the crouchSpeed multiplier
                    move = move * m_CurrentSlideSpeed;
                }
                else
                {
                    m_CurrentSlideSpeed = m_CrouchSpeed;
                    m_CurrentSlideDuration = 0;
                }

                // The Speed animator parameter is set to the absolute value of the horizontal input.
                m_Anim.SetFloat("Speed", Mathf.Abs(move));

                // Move the character
                float currentSpeed = run ? m_MaxRunningSpeed : m_MaxWalkingSpeed;
                m_Rigidbody2D.velocity = new Vector2(move * currentSpeed, m_Rigidbody2D.velocity.y);

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
            }
            // If the player should jump...
            if (jump)
            {
                // Add a vertical force to the player.
                if (m_Grounded && m_Anim.GetBool("Ground"))
                {
                    m_Grounded = false;
                    m_Anim.SetBool("Ground", false);
                    m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
                }
                else if (m_TouchingWall)
                {
                    float isThrowingRight = -1;
                    if (m_FacingRight)
                    {
                        isThrowingRight = 1;
                    }

                    m_Rigidbody2D.velocity = Vector2.zero;
                    m_Rigidbody2D.AddForce(new Vector2(isThrowingRight * -1f * m_JumpForce, m_JumpForce * 1.1f));

                    ActivateAirControlBlock();
                    Flip();
                }
            }
            // Slide on a wall
            if (!m_Grounded && m_TouchingWall)
            {
                m_Rigidbody2D.velocity = new Vector2(m_Rigidbody2D.velocity.x, m_Rigidbody2D.velocity.y * 0.75f);
            }
            m_Rigidbody2D.velocity = new Vector2(Mathf.Clamp(m_Rigidbody2D.velocity.x, -20, 20), Mathf.Clamp(m_Rigidbody2D.velocity.y, -20, 20));
        }

        private void ActivateAirControlBlock()
        {
            m_CanAirControl = false;
            m_AirControlTimerValue = m_AirControlBlockAfterWallJump;
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

        public bool IsFacingRight()
        {
            return m_FacingRight;
        }
    }
}
