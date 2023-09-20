using System.Collections.Generic;
using SouthBasement.Characters;
using SouthBasement.Characters.Base;
using SouthBasement.Generation;
using UnityEngine;

namespace SouthBasement.Tools
{
    [RequireComponent(typeof(Room))]
    public sealed class MinimapSpritesBinder : MonoBehaviour
    {
        [NaughtyAttributes.ReadOnly, SerializeField] private List<SpriteRenderer> minimapSprites = new();
        [NaughtyAttributes.ReadOnly,SerializeField] private Room room;

        private void Start() => room.PlayerEnterTrigger.OnEntered += OnEntered;
        private void OnDestroy() => room.PlayerEnterTrigger.OnEntered -= OnEntered;

        private void OnEntered(CharacterGameObject character) => EnableMinimapRoom();

        [NaughtyAttributes.Button]
        private void Bind()
        {
            room = GetComponent<Room>();
            minimapSprites.Clear();
            
            var allSpriteRenderers = GetComponentsInChildren<SpriteRenderer>(true);

            foreach (var spriteRenderer in allSpriteRenderers)
            {
                if (spriteRenderer.gameObject.layer == 10)
                {
                    var sprite = spriteRenderer;
                    var parent = spriteRenderer.transform.parent.GetComponent<SpriteRenderer>();

                    sprite.drawMode = SpriteDrawMode.Tiled;
                    sprite.size = parent.size;
                    sprite.transform.localScale = new Vector3(1, 1, 1);
                    
                    minimapSprites.Add(sprite);
                    sprite.gameObject.SetActive(false);
                }
            }
        }

        [NaughtyAttributes.Button]
        private void EnableMinimapRoom()
        {
            foreach (var sprite in minimapSprites)
                sprite.gameObject.SetActive(true);
        }
    }
}