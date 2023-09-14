using UnityEngine;
using UnityEngine.UI;

namespace SouthBasement
{
    [RequireComponent(typeof(Button))]
    public sealed class SoldButton : MonoBehaviour
    {
        [SerializeField] private RubbishDealerLogic logic;
        
        private void Awake()
        {
            GetComponent<Button>().onClick.AddListener(SoldAll);
        }

        private void SoldAll()
        {
            logic.SoldAll();
        }
    }
}