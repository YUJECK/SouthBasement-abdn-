using RimuruDev.Helpers;
using UnityEngine;

namespace RimuruDev.Mechanics.Character
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(RatCharacterData))]
    [RequireComponent(typeof(RatMotionController))]
    [RequireComponent(typeof(RatAnimationController))]
    [RequireComponent(typeof(RatBoostSpeed))]
    [RequireComponent(typeof(RatDashingController))]
    [RequireComponent(typeof(RatSprintController))]
    [RequireComponent(typeof(RatMovingTheNextLevel))]
    public sealed class RatCharacterController : MonoBehaviour
    {
        public static RatCharacterController instance;

        public RatCharacterData characterData;
        public RatMotionController motionController;
        public RatAnimationController animationController;
        public RatBoostSpeed boostSpeed;
        public RatDashingController dashingController;
        public RatSprintController sprintController;
        public RatMovingTheNextLevel ratMovingTheNextLevel;

        private void Awake()
        {
            SingletonInitialization();

            InitializationOfAllComponents(characterData.isDefaultInitializeInAwake);
        }

        private void Start()
        {
            DisableAllPanels();

            SetDefaultValueForRatCharacter();
        }

        private void Update()
        {
            if (!GameManager.isActiveAnyPanel)
            {
                animationController.RatRunAnimation();

                dashingController.GetDashingInput();
            }
            else
                animationController.RatStayAnimation();
        }

        private void FixedUpdate()
        {
            if (!GameManager.isActiveAnyPanel)
            {
                motionController.RatMovementOrStopped();

                dashingController.IsCallDashing();

                animationController.RatRotate();

                sprintController.RatSprint();
            }
        }

        public void SingletonInitialization()
        {
            if (instance == null)
                instance = this;
            else
            {
                Destroy(gameObject);
                return;
            }
        }

        public void InitializationOfAllComponents(bool isDefaultInitializeInAwake)
        {
            if (isDefaultInitializeInAwake == false)
            {
                characterData = IsNullHelpers<RatCharacterData>.IsNullHelp(ref characterData, this);
                motionController = IsNullHelpers<RatMotionController>.IsNullHelp(ref motionController, this);
                animationController = IsNullHelpers<RatAnimationController>.IsNullHelp(ref animationController, this);
                boostSpeed = IsNullHelpers<RatBoostSpeed>.IsNullHelp(ref boostSpeed, this);
                dashingController = IsNullHelpers<RatDashingController>.IsNullHelp(ref dashingController, this);
                sprintController = IsNullHelpers<RatSprintController>.IsNullHelp(ref sprintController, this);
                ratMovingTheNextLevel = IsNullHelpers<RatMovingTheNextLevel>.IsNullHelp(ref ratMovingTheNextLevel, this);
            }

            if (isDefaultInitializeInAwake == true)
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

                if (animationController == null)
                {
                    animationController = GetComponent<RatAnimationController>();

                    if (animationController == null)
                        animationController = GameObject.FindObjectOfType<RatAnimationController>();
                }

                if (boostSpeed == null)
                {
                    boostSpeed = GetComponent<RatBoostSpeed>();

                    if (boostSpeed == null)
                        boostSpeed = GameObject.FindObjectOfType<RatBoostSpeed>();
                }

                if (dashingController == null)
                {
                    dashingController = GetComponent<RatDashingController>();

                    if (dashingController == null)
                        dashingController = GameObject.FindObjectOfType<RatDashingController>();
                }

                if (sprintController == null)
                {
                    sprintController = GetComponent<RatSprintController>();

                    if (sprintController == null)
                        sprintController = GameObject.FindObjectOfType<RatSprintController>();
                }

                if (ratMovingTheNextLevel == null)
                {
                    ratMovingTheNextLevel = GetComponent<RatMovingTheNextLevel>();

                    if (ratMovingTheNextLevel == null)
                        ratMovingTheNextLevel = GameObject.FindObjectOfType<RatMovingTheNextLevel>();
                }
            }
        }

        public void DisableAllPanels() => GameManager.isActiveAnyPanel = false;

        public void SetDefaultValueForRatCharacter()
        {
            characterData.normalSpeed = characterData.speed;
        }
    }
}