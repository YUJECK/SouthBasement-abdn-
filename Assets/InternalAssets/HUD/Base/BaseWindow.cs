using DG.Tweening;
using SouthBasement.HUD.Base;
using UnityEngine;

namespace SouthBasement.HUD
{
    public sealed class BaseWindow : Window
    {
        [SerializeField] private GameObject _object;

        public override void Open()
        {
            transform.DOScale(new Vector3(1, 1, 1), 0.3f);
            _object.gameObject.SetActive(true);
        }

        public override void Close()
        {
            transform.DOScale(new Vector3(1, 0, 1), 0.3f);
        }
    }
}