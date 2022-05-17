using UnityEngine;

namespace RimuruDev.Mechanics.Character
{
    public sealed class RatDashingController : MonoBehaviour
    {
        public RatCharacterData characterData;
        public RatMotionController motionController;

        private void Awake()
        {
            if (characterData == null)
            {
                characterData = GetComponent<RatCharacterData>();

                if (characterData == null)
                    characterData = GameObject.FindObjectOfType<RatCharacterData>();
            }

            if (motionController == null)
            {
                motionController = GetComponent<RatMotionController>();

                if (motionController == null)
                    motionController = GameObject.FindObjectOfType<RatMotionController>();
            }
        }

        public void GetDashingInput()
        {
            if (Input.GetMouseButtonDown(1) && !characterData.isSprinting && characterData.dashTime == 0f && characterData.dashNextTime <= Time.time)
            {
                characterData.dashTime = characterData.dashDuration;
                characterData.movementOnDash = motionController.GetPlayerInput();
                characterData.dashNextTime = Time.time + characterData.dashRate;
            }
        }

        public bool IsGetDashingInput()
        {
            if (Input.GetMouseButtonDown(1) && !characterData.isSprinting && characterData.dashTime == 0f && characterData.dashNextTime <= Time.time)
            {
                characterData.dashTime = characterData.dashDuration;
                characterData.movementOnDash = motionController.GetPlayerInput();
                characterData.dashNextTime = Time.time + characterData.dashRate;

                return true;
            }
            else
                return false;
        }

        public void IsCallDashing()
        {
            if (characterData.dashTime > 0f)
                Dashing();
            else
                characterData.dashTime = 0f;
        }

        public void Dashing()
        {
            characterData.ratRigidbody2D.velocity = characterData.movementOnDash * (characterData.speed + characterData.dashSpeed);
            characterData.dashTime -= 0.1f;
        }
    }
}