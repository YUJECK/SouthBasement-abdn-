using UnityEngine;

namespace RimuruDev.Mechanics.Character
{
    public sealed class RatBoostSpeed : MonoBehaviour
    {
        public RatCharacterData characterData;

        private void Awake()
        {
            if (characterData == null)
            {
                characterData = GetComponent<RatCharacterData>();

                if (characterData == null)
                    characterData = GameObject.FindObjectOfType<RatCharacterData>();
            }
        }

        public void BoostSpeed(float speedBoost) => characterData.speed += characterData.speed * speedBoost;
    }
}