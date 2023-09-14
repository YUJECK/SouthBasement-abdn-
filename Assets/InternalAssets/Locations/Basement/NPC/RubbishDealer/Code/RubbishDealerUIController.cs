using UnityEngine;

namespace SouthBasement
{
    public sealed class RubbishDealerUIController : MonoBehaviour
    {
        [SerializeField] private Transform uiMaster;

        public void EnableUI()
        {
            uiMaster.gameObject.SetActive(true);
        }

        public void DisableUI()
        {
            uiMaster.gameObject.SetActive(false);
        }
    }
}