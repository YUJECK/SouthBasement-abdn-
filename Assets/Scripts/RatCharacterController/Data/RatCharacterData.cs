using RimuruDev.Helpers;
using System.Collections;
using UnityEngine;

using static RimuruDev.Helpers.Tag;

namespace RimuruDev.Mechanics.Character
{
    public sealed class RatCharacterData : MonoBehaviour
    {
        [Header("Rat of all components")]
        public Transform ratCharacter;
        public Rigidbody2D ratRigidbody2D;
        public SpriteRenderer ratSprite;
        public Animator anim;
        public Collider2D sprintColl; //Коллайдер при спринте
        public Collider2D normalColl; // кеоллайдер при ходьбе
        [Space]
        public RatAttack ratAttack;
        public AudioManager audioManager;
        //public static RatCharacterData instance; // Синглтон

        [Header("Rat Movement")]
        public float speed = 5f; // Скорость игрока

        [Header("Rat Stste")]
        public bool isStopped; // Спринтит ли игрок
        public bool isSprinting; // Спринтит ли игрок
        public bool flippedOnRight = false; // Повернут ли игрок направо

        [Header("Rat Sprint")]
        public float sprint = 0.35f; //Процент увеличения скорости при спринте
        public float boostSpeed; // Новая скорость при спринте
        public float normalSpeed = 5f; // Обычная скорость

        [Header("Rat Dash")]
        public float dashTime = 0f; // Длина рывка
        public Vector2 movementOnDash; // Напрвление рывка
        public float dashDuration = 3f; // Скорость рывка
        public float dashRate = 3f; //Скорость перезарядки рывка
        public float dashNextTime = 0f; //То когда сможет игрок сделать рывок
        public float dashSpeed = 4f; // Скорость рывка\

        [Header("Develop Debug")]
        public bool isDefaultInitializeInAwake = true;

        private void Awake() => InitializeOfAllComponents();

        private void InitializeOfAllComponents()
        {
            if (ratCharacter == null)
                ratCharacter = GameObject.FindGameObjectWithTag(Player).transform;

            if (ratRigidbody2D == null)
                ratRigidbody2D = ratCharacter.GetComponent<Rigidbody2D>();

            if (ratSprite == null)
                ratSprite = ratCharacter.GetComponent<SpriteRenderer>();

            if (anim == null)
            {
                anim = ratCharacter.GetComponent<Animator>();

                if (anim == null)
                    anim = GameObject.FindObjectOfType<Animator>();
            }

            if (sprintColl == null)
                sprintColl = ratCharacter.GetComponent<Collider2D>();

            if (normalColl == null)
                normalColl = ratCharacter.GetComponent<Collider2D>();

            if (ratAttack == null)
                ratAttack = ratCharacter.GetComponent<RatAttack>();

            if (audioManager == null)
                audioManager = ratCharacter.GetComponent<AudioManager>();
        }
    }
}