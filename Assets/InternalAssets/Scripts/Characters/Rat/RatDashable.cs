﻿using System.Collections;
using TheRat.InputServices;
using TheRat.Player;
using UnityEngine;

namespace TheRat.Characters.Rat
{
    public sealed class RatDashable : IDashable
    {
        private readonly IMovable _movable;
        private readonly Transform _transform;
        
        private readonly MonoBehaviour _coroutineRunner;
        private readonly PlayerAnimator _playerAnimator;
        private readonly Rigidbody2D _rigidbody2D;
        private bool _blocked;

        public RatDashable(IInputService inputService, IMovable movable, Transform transform, PlayerAnimator playerAnimator, MonoBehaviour coroutineRunner)
        {
            _movable = movable;
            _transform = transform;
            _rigidbody2D = transform.GetComponent<Rigidbody2D>();
            _coroutineRunner = coroutineRunner;
            _playerAnimator = playerAnimator;
            
            inputService.OnDashed += Dash;
        }

        public void Dash()
        {
            if (_blocked)
                return;

            _coroutineRunner.StartCoroutine(DashCoroutine());
        }

        private IEnumerator DashCoroutine()
        {
            StartDash();
            {
                var dashMove = GetPositionInVector2() + _movable.Movement;
                
                var dashStopTime = Time.time + 0.2;
                
                while (Time.time < dashStopTime)
                {
                    _rigidbody2D.MovePosition(Vector2.MoveTowards(_transform.position, dashMove, Time.deltaTime * 30));
                    yield return new WaitForFixedUpdate();
                }
            }
            ReleaseDash();

            yield return new WaitForSeconds(0.7f);
            _blocked = false;
        }

        private void StartDash()
        {
            _movable.CanMove = false;
            _blocked = true;
            _playerAnimator.PlayDash();
            _transform.gameObject.layer = 11;
        }

        private void ReleaseDash()
        {
            _transform.gameObject.layer = 7;
            _movable.CanMove = true;
        }

        private Vector2 GetPositionInVector2() => new(_transform.position.x, _transform.position.y);
    }
}   