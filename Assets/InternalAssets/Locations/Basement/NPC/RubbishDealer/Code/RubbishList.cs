using System;
using System.Collections.Generic;
using SouthBasement.InventorySystem;
using SouthBasement.InventorySystem.ItemBase;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace SouthBasement
{
    public sealed class RubbishList : MonoBehaviour
    {
        [SerializeField] private RubbishDealerLogic rubbishDealerLogic;
        
        private List<Image> _images;
        private Inventory _inventory;

        [Inject]
        private void Construct(Inventory inventory)
        {
            _inventory = inventory;
        }
        
        private void Awake()
            => GetImages();

        private void OnEnable()
        {
            rubbishDealerLogic.OnSold += ClearAllImages;
            
            ClearAllImages();
            UpdateRubbishSprites();
        }

        private void OnDisable()
        {
            rubbishDealerLogic.OnSold -= ClearAllImages;
        }

        private void GetImages()
        {
            var images = GetComponentsInChildren<Image>();
            _images = new List<Image>(images);
            
            _images.Remove(GetComponent<Image>());
        }

        private void UpdateRubbishSprites()
        {
            var rubbish = _inventory.ItemsContainer.GetAllInContainer<RubbishItem>();

            for (int i = 0; i < rubbish.Length; i++)
            {
                _images[i].sprite = rubbish[i].ItemSprite;
                _images[i].color = Color.white;
            }
        }

        private void ClearAllImages()
        {
            foreach (var image in _images)
                image.color = Color.clear;
        }
    }
}
