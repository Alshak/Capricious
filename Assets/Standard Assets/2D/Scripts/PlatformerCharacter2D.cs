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
        [SerializeField] private bool m_AirControl = false;                 // Whether or not a player can steer while jumping;
        [SerializeField] private LayerMask m_WhatIsGround;                  // A mask determining what is ground to the character
        [SerializeField] private LayerMask m_WhatIsWall;                  // A mask determining what is a wall to the character
        [SerializeField] private float m_SlideDuration = 2f;
        [SerializeField] private float m_SlideCoef = 1.2f;
        [SerializeField] private float m_SlideCooldown = 0.2f;
        [SerializeField] private int m_JumpBuffer = 15;
        [SerializeField] private int m_SlideBuffer = 15;
        [SerializeField] private GameObject ThrowableTemplate;
        [Range(0, 1)] [SerializeField] private float m_AirControlBlockAfterWallJump = 1f;

        int slideBuffer = 0;
        int jumpBuffer = 0;
        private Animator m_Anim;            // Reference to the player's animator component.
        private Rigidbody2D m_Rigidbody2D;

        private Transform m_GroundCheck;    // A position marking where to check if the player is grounded.
        const float k_GroundedRadius = .01f; // Radius of the overlap circle to determine if grounded
        private bool m_Grounded;            // Whether or not the player is grounded.

        private Transform m_CeilingCheck;   // A position marking where to check for ceilings
        const float k_CeilingRadius = .05f; // Radius of the overlap circle to determine if the player can stand up

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
        private float slideCooldown = 0f;
        private float timeSinceLastJump = 0f;

        public bool PlayJumpSound = false;

        public void ResetEverything()
        {
            m_CurrentSlideDuration = 0f;
            timeSinceLastJump = 0f;
            slideCooldown = 0f;
            m_AirControlTimerValue = 0f;
            slideBuffer = 0;
            jumpBuffer = 0;
        }
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
                if (m_AirControlTimerValue <= 0f)
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



        public void Action(bool slide, bool throwing, bool jump)
        {
            // Jump buffer
            if (jump)
            {
                jumpBuffer = m_JumpBuffer;
                slideBuffer = 0;
            }
            else
            {
                jumpBuffer -= 1;
                if (jumpBuffer > 0)
                {
                    jump = true;
                }
            }

            // Slide buffer
            if (slide)
            {
                jumpBuffer = 0;
                slideBuffer = m_SlideBuffer;
            }
            else
            {
                slideBuffer -= 1;
                if (slideBuffer > 0)
                {
                    slide = true;
                }
            }

            // Direction
            float rightCoef = -1;
            if (m_FacingRight)
            {
                rightCoef = 1;
            }

            // Sliding
            if (m_CurrentSlideDuration > 0f)
            {
                return;
            }

            // Slide
            slide = Slide(slide);

            // Throw
            if (!slide)
            {
                Throw(throwing, rightCoef);
            }

            // If the player should jump...
            Jump(jump, rightCoef);
        }

        private void Jump(bool jump, float rightCoef)
        {
            if (jump)
            {
                // Add a vertical force to the player.
                if (m_Grounded && m_Anim.GetBool("Ground"))
                {
                    m_Grounded = false;
                    m_Anim.SetBool("Ground", false);
                    m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
                    jumpBuffer = 0;
                    timeSinceLastJump = 0f;
                    PlayJumpSound = true;
                }
                else if (m_TouchingWall)
                {
                    m_Rigidbody2D.velocity = Vector2.zero;
                    m_Rigidbody2D.AddForce(new Vector2(rightCoef * -1f * m_JumpForce * 0.8f, m_JumpForce * 1.3f));

                    ActivateAirControlBlock();
                    Flip();
                    jumpBuffer = 0;
                    timeSinceLastJump = 0f;
                }
            }
        }

        private void Throw(bool throwing, float rightCoef)
        {
            if (throwing)
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
        }

        private bool Slide(bool slide)
        {
            bool forceSlide = false;
            if (m_Grounded)
            {
                // If crouching, check to see if the character can stand up
                if (!slide && m_Anim.GetBool("Crouch"))
                {
                    // If the character has a ceiling preventing them from standing up, keep them crouching
                    if (Physics2D.OverlapCircle(m_CeilingCheck.position, k_CeilingRadius, m_WhatIsGround))
                    {
                        slide = true;
                        forceSlide = true;
                    }
                }

                if (slideCooldown <= 0f || forceSlide)
                {
                    // Set whether or not the character is crouching in the animator
                    if (m_Anim.GetBool("Crouch") != slide)
                    {
                        m_Anim.SetBool("Crouch", slide);
                    }

                    if (slide)
                    {
                        m_CurrentSlideDuration = m_SlideDuration;
                        slideBuffer = 0;
                    }
                }
                else
                {
                    m_Anim.SetBool("Crouch", false);
                }
            }

            return slide;
        }

        float previousX;
        float previousY;
        public void Move(float move)
        {
            timeSinceLastJump += Time.deltaTime;
            if (slideCooldown > 0f)
            {
                slideCooldown -= Time.deltaTime;
            }
            bool goingRight = Mathf.Sign(move) > 0;
            float rightCoef = -1;
            if (m_FacingRight)
            {
                rightCoef = 1;
            }

            if (m_CurrentSlideDuration > 0f)
            {
                m_CurrentSlideDuration -= Time.deltaTime;
                m_Rigidbody2D.velocity = new Vector2(rightCoef * m_MaxWalkingSpeed * m_SlideCoef, 0);
                if (m_CurrentSlideDuration < 0f)
                {
                    slideCooldown = m_SlideCooldown;
                }
                return;
            }



            //only control the player if grounded or airControl is turned on
            if (m_Grounded || (m_AirControl && m_CanAirControl))
            {
                // Move the character
                float verticalSpeed = m_Rigidbody2D.velocity.y;
                if (!m_Grounded && Mathf.Approximately(m_Rigidbody2D.velocity.y,0) && timeSinceLastJump > 2f)
                {
                    verticalSpeed = -10f;
                }
                if (!m_TouchingWall || !(m_FacingRight && goingRight || !m_FacingRight && !goingRight))
                {
                    m_Rigidbody2D.velocity = new Vector2(move * m_MaxWalkingSpeed, verticalSpeed);
                }
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
