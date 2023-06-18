using UnityEngine;

namespace SouthBasement.PlayerServices
{
    [RequireComponent(typeof(Flipper))]
    public sealed class MouserTargetSetter : MonoBehaviour
    {
        private CursorService _cursorService;
        //
        // [Inject]
        // private void Construct(CursorService cursorService)
        // {
        //     _cursorService = cursorService;
        // }
        
        private void Awake()
        {
            FindObjectOfType<CursorService>();
            GetComponent<Flipper>().Target = FindObjectOfType<CursorService>().transform;
        }
    }
}