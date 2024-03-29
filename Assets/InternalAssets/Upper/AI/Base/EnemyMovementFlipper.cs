﻿using System.Collections.Generic;
using SouthBasement.Characters.Components;
using SouthBasement.Enums;
using UnityEngine;

namespace SouthBasement.AI
{
    [RequireComponent(typeof(IEnemyMovable))]
    public sealed class EnemyMovementFlipper : MonoBehaviour, IFlipper
    {
        public FacingDirections FacingDirection { get; private set; } = FacingDirections.Left;
        
        private IEnemyMovable _enemyMovable;
        public bool Blocked { get; set; } = false;

        private List<IgnoringFlipping> _ignoringFlippings = new();

        private void Awake()
        {
            _enemyMovable = GetComponent<IEnemyMovable>();

            _ignoringFlippings = new List<IgnoringFlipping>(GetComponentsInChildren<IgnoringFlipping>());
        }

        private void Update()
        {
            if(Blocked) return;
            
            if(_enemyMovable.CurrentMovement.x > 0 && FacingDirection == FacingDirections.Left) 
                Flip(FacingDirections.Right);
            else if (_enemyMovable.CurrentMovement.x < 0 && FacingDirection == FacingDirections.Right) 
                Flip(FacingDirections.Left);
        }

        public void Flip(FacingDirections facingDirections) 
        {
            if(facingDirections == FacingDirections.Right && FacingDirection == FacingDirections.Left)
            {
                transform.Rotate(0f, 180f, 0f);
                FacingDirection = FacingDirections.Right;
                
                foreach (var ignore in _ignoringFlippings)
                    ignore.transform.Rotate(0f, 180f, 0f);
            }
            else if (facingDirections == FacingDirections.Left && FacingDirection == FacingDirections.Right)
            {
                transform.Rotate(0f, -180f, 0f);
                FacingDirection = FacingDirections.Left;
                
                foreach (var ignore in _ignoringFlippings)
                    ignore.transform.Rotate(0f, -180f, 0f);
            }
        }
    }
}