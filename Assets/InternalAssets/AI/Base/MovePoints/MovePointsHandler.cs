using UnityEngine;

namespace SouthBasement.AI.MovePoints
{
    public sealed class MovePointsHandler
    {
        private MovePoint[] _movePoints;

        public MovePointsHandler(MovePoint[] movePoints)
            => _movePoints = movePoints;

        public MovePoint[] GetAll() => _movePoints;

        public MovePoint GetRandom(MovePoint last)
        {
            var newPoint = _movePoints[Random.Range(0, _movePoints.Length)];

            while (newPoint == last)
                newPoint = _movePoints[Random.Range(0, _movePoints.Length)];   

            return newPoint;
        }

        public MovePoint GetRandom() => _movePoints[Random.Range(0, _movePoints.Length)];

        public MovePoint GetNearest(Transform from)
        {
            var nearest = _movePoints[0];
                
            foreach (var nextPoint in _movePoints)
            {
                if (Vector2.Distance(from.position, nearest.transform.position)
                    > Vector2.Distance(from.position, nextPoint.transform.position))
                    nearest = nextPoint;
            }

            return nearest;    
        }
        public MovePoint GetFurthest(Transform from)
        {
            var nearest = _movePoints[0];
                
            foreach (var nextPoint in _movePoints)
            {
                if (Vector2.Distance(from.position, nearest.transform.position)
                    < Vector2.Distance(from.position, nextPoint.transform.position))
                    nearest = nextPoint;
            }

            return nearest;    
        }
    }
}