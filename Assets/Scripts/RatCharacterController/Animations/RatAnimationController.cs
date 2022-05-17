using UnityEngine;

using static RimuruDev.Helpers.Tag;

namespace RimuruDev.Mechanics.Character
{
    public sealed class RatAnimationController : MonoBehaviour
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

        public void RatRunAnimation()
        {
            if (motionController.GetPlayerInput().x != 0 || motionController.GetPlayerInput().y != 0)
                characterData.anim.SetBool(Is_Run, true);

            else if (motionController.GetPlayerInput().x == 0 && motionController.GetPlayerInput().y == 0)
                characterData.anim.SetBool(Is_Run, false);
        }

        public void RatStayAnimation() => characterData.anim.SetBool(Is_Run, false);

        public void RatRotate()
        {
            if (motionController.GetPlayerInput().x > 0)
                characterData.ratSprite.flipX = true;

            if (motionController.GetPlayerInput().x < 0)
                characterData.ratSprite.flipX = false;
        }
    }
}