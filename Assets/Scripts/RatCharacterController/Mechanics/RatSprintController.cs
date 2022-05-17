using RimuruDev.Helpers;
using UnityEngine;

using static RimuruDev.Helpers.Tag;

namespace RimuruDev.Mechanics.Character
{
    public class RatSprintController : MonoBehaviour
    {
        public RatCharacterData characterData;
        public RatMotionController motionController;

        private void Awake()
        {
            characterData = IsNullHelpers<RatCharacterData>.IsNullHelp(ref characterData, this);
            motionController = IsNullHelpers<RatMotionController>.IsNullHelp(ref motionController, this);

            //if (characterData == null)
            //{
            //    characterData = GetComponent<RatCharacterData>();

            //    if (characterData == null)
            //        characterData = GameObject.FindObjectOfType<RatCharacterData>();
            //}

            //if (motionController == null)
            //{
            //    motionController = GetComponent<RatMotionController>();

            //    if (motionController == null)
            //        motionController = GameObject.FindObjectOfType<RatMotionController>();
            //}
        }

        public void RatSprint()
        {
            if ((!characterData.isSprinting & Input.GetKeyDown(KeyCode.LeftControl)) & (motionController.GetPlayerInput().x != 0 || motionController.GetPlayerInput().y != 0))
                Sprint();

            if (characterData.isSprinting & Input.GetKeyUp(KeyCode.LeftControl))
                Sprint();
        }

        private void Sprint()
        {
            if (!characterData.isSprinting)
            {
                characterData.boostSpeed = characterData.speed + characterData.speed * characterData.sprint;
                characterData.normalSpeed = characterData.speed;
                characterData.speed = characterData.boostSpeed;
                characterData.anim.SetBool(Is_Sprint, true);
                characterData.sprintColl.enabled = true;
                characterData.normalColl.enabled = false;
                characterData.ratAttack.HideMelleweaponIcon(true);
                characterData.isSprinting = true;
            }
            else
            {
                characterData.speed = characterData.normalSpeed;
                characterData.sprintColl.enabled = false;
                characterData.normalColl.enabled = true;
                characterData.anim.SetBool(Is_Sprint, false);
                characterData.ratAttack.HideMelleweaponIcon(false);
                characterData.isSprinting = false;
            }
        }
    }
}