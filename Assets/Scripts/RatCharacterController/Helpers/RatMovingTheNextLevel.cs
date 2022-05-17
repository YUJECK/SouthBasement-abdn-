using UnityEngine;

using RimuruDev.Mechanics.Character;
using static RimuruDev.Helpers.Tag;

namespace RimuruDev.Helpers
{
    public sealed class RatMovingTheNextLevel : MonoBehaviour
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

        private void OnLevelWasLoaded(int level) => SetPositionForNewLevel();

        public void SetPositionForNewLevel()
        {
            characterData.ratCharacter.position =
                GameObject.FindWithTag(StartPoint).transform.position;
        }
    }
}