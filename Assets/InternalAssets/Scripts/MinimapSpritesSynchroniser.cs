using UnityEngine;

namespace TheRat.Tools
{
    public sealed class MinimapSpritesSynchroniser : MonoBehaviour
    {
        [NaughtyAttributes.Button]
        private void Synchronise()
        {
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
                }
            }
        }
    }
}