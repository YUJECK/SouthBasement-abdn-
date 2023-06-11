using UnityEngine;

namespace TheRat.PlayerServices
{
    public class Flipper : MonoBehaviour
    {
        [field: SerializeField ] public bool StartRightFacing { get; private set; }

        private bool _faceRight;
        [field: SerializeField] public Transform Target { get; set; }

        private void Start()
        {
            _faceRight = StartRightFacing;
        }

        private void Update()
        {
            if(Target == null)
                return;
                
            if(Target.position.x > transform.position.x && !_faceRight)
            {
                transform.Rotate(0f, 180f, 0f);
                _faceRight = true;
            }
            else if (Target.position.x < transform.position.x && _faceRight)
            {
                transform.Rotate(0f, -180f, 0f);
                _faceRight = false;
            }
        }
    }
}
