using System;
using System.Collections;
using Cysharp.Threading.Tasks;
using TheRat.InputServices;
using TheRat.Player;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace TheRat.Characters.Rat
{
    public sealed class RatDashable : IDashable
    {
        private IMovable _movable;
        private Transform _transform;
        
        private MonoBehaviour _coroutineRunner;
        private readonly PlayerAnimator _playerAnimator;
        private bool _blocked;

        public RatDashable(IInputService inputService, IMovable movable, Transform transform, PlayerAnimator playerAnimator, MonoBehaviour coroutineRunner)
        {
            _movable = movable;
            _transform = transform;
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
            _blocked = true;
            _movable.CanMove = false;
            {
                _playerAnimator.PlayDash();
                _transform.gameObject.layer = 11;
                var dashMove = GetPositionInVector2() + _movable.Movement;
                
                float dashStopTime = Time.time + 0.2f;
                
                while (Time.time < dashStopTime)
                {
                    _transform.GetComponent<Rigidbody2D>().MovePosition(Vector2.MoveTowards(_transform.position, dashMove, Time.deltaTime * 40));
                    yield return new WaitForFixedUpdate();
                }
                
                _transform.gameObject.layer = 7;
            }
            yield return new WaitForSeconds(0.1f);
            _movable.CanMove = true;
            
            yield return new WaitForSeconds(0.7f);
            _blocked = false;
        }
        
        private Vector2 GetPositionInVector2()
        {
            return new Vector2(_transform.position.x, _transform.position.y);
        }
    }
}   