using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace UnityStandardAssets._2D
{
    [RequireComponent(typeof(PlatformerCharacter2D))]
    public class Platformer2DUserControl : MonoBehaviour
    {
        private PlatformerCharacter2D m_Character;
        private bool m_Jump;
        public bool IsAlive = true;

        private void Awake()
        {
            m_Character = GetComponent<PlatformerCharacter2D>();
        }

        private void Update()
        {
            if (IsAlive == false) //Set to true in PlayerRespawner
                return;

            if (!m_Jump)
            {
                // Read the jump input in Update so button presses aren't missed.
                m_Jump = CrossPlatformInputManager.GetButtonDown("Jump");
            }

            // Read the inputs.
            bool crouch = CrossPlatformInputManager.GetButtonDown("Crouch");
            bool run = CrossPlatformInputManager.GetButton("Run");
            bool throwing = CrossPlatformInputManager.GetButtonDown("Throw");
            // Pass all parameters to the character control script.
            m_Character.Action(crouch, run, throwing, m_Jump);
            m_Jump = false;
        }

        private void FixedUpdate()
        {
            if (IsAlive == false) //Set to true in PlayerRespawner
                return;

            // Read the inputs.
            float h = CrossPlatformInputManager.GetAxis("Horizontal");
            // Pass all parameters to the character control script.
            m_Character.Move(h);
        }
    }
}
