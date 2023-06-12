using TheRat.Helpers;
using UnityEngine;
using Zenject;

namespace TheRat.Economy
{
    public sealed class CheeseObject : MonoBehaviour
    {
        [SerializeField] private int cheeseAmount = 0;
        private CheeseService _cheeseService;

        [Inject]
        private void Construct(CheeseService cheeseService)
        {
            _cheeseService = cheeseService;
        }
        
        public void SetCheeseValue(int amount)
        {
            cheeseAmount = amount;
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.transform.CompareTag(TagHelper.Player))
            {
                Destroy(gameObject);
                _cheeseService.AddCheese(cheeseAmount);
            }
        }
    }
}