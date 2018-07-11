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

        private Animator m_Anim;            // Reference to the player's animator component.
        private Rigidbody2D m_Rigidbody2D;

        private Transform m_GroundCheck;    // A position marking where to check if the player is grounded.
        const float k_GroundedRadius = .01f; // Radius of the overlap circle to determine if grounded
        private bool m_Grounded;            // Whether or not the player is grounded.

        private Transform m_CeilingCheck;   // A position marking where to check for ceilings
        const float k_CeilingRadius = .01f; // Radius of the overlap circle to determine if the player can stand up

        private Transform m_WallCheck;
        private Transform m_WallCheck2;
        private Transform m_WallCheck3;
        const float k_WallJumpRadius = .2f; // Radius of the overlap circle to determine if player can wall jump
        private bool m_TouchingWall;            // Whether or not the player can wall jump.

        private Transform m_ThrowPosition;

        private bool m_CanAirControl = false;
        private float m_AirControlTimerValue = 1f;

        private bool m_FacingRight = true;  // For determining which way the player is currently facing.

        private float m_CurrentSlideDuration = 0f;
        private float m_CurrentSlideSpeed = 1f;

        private void Awake()
        {
            // Setting up references.
            m_GroundCheck = transform.Find("GroundCheck");
            m_CeilingCheck = transform.Find("CeilingCheck");
            m_WallCheck = transform.Find("WallCheck");
            m_WallCheck2 = transform.Find("WallCheck2");
            m_WallCheck3 = transform.Find("WallCheck3");
            m_ThrowPosition = transform.Find("ThrowPosition");
            m_Anim = GetComponent<Animator>();
            m_Rigidbody2D = transform.GetComponent<Rigidbody2D>();
        }

        private void Start()
        {
            m_CurrentSlideDuration = 0f;
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
            if (!m_TouchingWall)
            {
                wallJumpColliders = Physics2D.OverlapCircleAll(m_WallCheck2.position, k_WallJumpRadius, m_WhatIsWall);
                for (int i = 0; i < wallJumpColliders.Length; i++)
                {
                    if (wallJumpColliders[i].gameObject != gameObject)
                    {
                        m_TouchingWall = true;
                    }
                }
            }
            if (!m_TouchingWall)
            {
                wallJumpColliders = Physics2D.OverlapCircleAll(m_WallCheck3.position, k_WallJumpRadius, m_WhatIsWall);
                for (int i = 0; i < wallJumpColliders.Length; i++)
                {
                    if (wallJumpColliders[i].gameObject != gameObject)
                    {
                        m_TouchingWall = true;
                    }
                }
            }
            m_Anim.SetBool("TouchingWall", m_TouchingWall);
            // Set the vertical animation
            m_Anim.SetFloat("vSpeed", m_Rigidbody2D.velocity.y);
        }

        int crouchBuffer = 0;
        int jumpBuffer = 0;

        public void Action(bool crouch, bool throwing, bool jump)
        {
            if (jump)
            {
                jumpBuffer = 15;
            }
            else
            {
                jumpBuffer -= 1;
                if (jumpBuffer > 0)
                {
                    jump = true;
                }
            }
            if (crouch)
            {
                crouchBuffer = 15;
            }
            else
            {
                crouchBuffer -= 1;
                if (crouchBuffer > 0)
                {
                    crouch = true;
                }
            }

            float rightCoef = -1;
            if (m_FacingRight)
            {
                rightCoef = 1;
            }

            if (m_CurrentSlideDuration > 0f)
            {
                return;
            }

            // Crouch
            if (m_Grounded)
            {
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

                if (crouch)
                {
                    m_CurrentSlideDuration = m_SlideDuration;
                    crouchBuffer = 0;
                }
            }

            // Throw
            if (!crouch && throwing)
            {
                m_Anim.SetTrigger("Throw");
                GameObject throwable = Instantiate(ThrowableTemplate, m_ThrowPosition.position, Quaternion.identity);
                float jumpSlideCoef = 1;
                // if wall slide, inverse shoot
                if (!m_Grounded && m_TouchingWall && m_Rigidbody2D.velocity.y < 0f)
                {
                    jumpSlideCoef = -1;
                }
                throwable.GetComponent<Rigidbody2D>().AddForce(new Vector2(jumpSlideCoef * rightCoef * m_ThrowForce, m_ThrowForce * 0.25f));
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
                    jumpBuffer = 0;
                }
                else if (m_TouchingWall)
                {
                    m_Rigidbody2D.velocity = Vector2.zero;
                    m_Rigidbody2D.AddForce(new Vector2(rightCoef * -1f * m_JumpForce, m_JumpForce * 1.1f));

                    ActivateAirControlBlock();
                    Flip();
                    jumpBuffer = 0;
                }
            }
        }

        float previousX;
        float previousY;
        public void Move(float move)
        {

            bool goingRight = Mathf.Sign(move) > 0;
            float rightCoef = -1;
            if (m_FacingRight)
            {
                rightCoef = 1;
            }

            if (m_CurrentSlideDuration > 0f)
            {
                m_CurrentSlideDuration -= Time.deltaTime;
                m_Rigidbody2D.velocity = new Vector2(rightCoef * m_MaxWalkingSpeed * 1.2f, 0);
                return;
            }


            //only control the player if grounded or airControl is turned on
            if (m_Grounded || (m_AirControl && m_CanAirControl))
            {
                // Move the character                
                m_Rigidbody2D.velocity = new Vector2(move * m_MaxWalkingSpeed, m_Rigidbody2D.velocity.y);

                m_Anim.SetFloat("Speed", Mathf.Abs(transform.position.x - previousX));

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

            // Slide on a wall
            if (!m_Grounded && m_TouchingWall && m_Rigidbody2D.velocity.y < 0f)
            {
                m_Rigidbody2D.velocity = new Vector2(m_Rigidbody2D.velocity.x, m_Rigidbody2D.velocity.y * 0.75f);
            }
            m_Rigidbody2D.velocity = new Vector2(Mathf.Clamp(m_Rigidbody2D.velocity.x, -20, 20), Mathf.Clamp(m_Rigidbody2D.velocity.y, -20, 20));
            previousX = transform.position.x;
            previousY = transform.position.y;
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
