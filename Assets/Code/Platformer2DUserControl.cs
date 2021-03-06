using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace UnityStandardAssets._2D
{
    [RequireComponent(typeof(PlatformerCharacter2D))]
    public class Platformer2DUserControl : MonoBehaviour
    {
        private PlatformerCharacter2D m_Character;
        public bool IsAlive = true;

        private void Awake()
        {
            m_Character = GetComponent<PlatformerCharacter2D>();
        }

        private void Update()
        {
            if (IsAlive == false || Time.timeScale == 0) //Set to true in PlayerRespawner
                return;

            // Read the inputs.
            bool jump = CrossPlatformInputManager.GetButtonDown("Jump");
            bool crouch = CrossPlatformInputManager.GetButtonDown("Crouch");
            bool throwing = CrossPlatformInputManager.GetButtonDown("Throw");
            // Pass all parameters to the character control script.
            m_Character.Action(crouch,  throwing, jump);
        }

        private void FixedUpdate()
        {
            if (IsAlive == false || Time.timeScale == 0) //Set to true in PlayerRespawner
                return;

            // Read the inputs.
            float h = CrossPlatformInputManager.GetAxis("Horizontal");
            // Pass all parameters to the character control script.
            m_Character.CheckColliders();
            m_Character.Move(h);
        }
    }
}
