using System;
using System.Collections.Generic;
using AYellowpaper.SerializedCollections;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace TheRat.Generation
{
    public class Room : MonoBehaviour
    {
        [FormerlySerializedAs("_passages")] [SerializeField] private SerializedDictionary<Direction, Passage> passages;
        [field: SerializeField] public Vector2 RoomSize { get; private set; }
        
        private void Awake()
        {
            foreach (var passage in passages)
                passage.Value.Factory.Init(this, passage.Key);
        }

        public Vector2 GetOffCenter(Direction direction)
        {
            return passages[direction].transform.localPosition;
        }

        public Passage GetPassage(Direction direction)
        {
            if (!passages.ContainsKey(direction))
                return null;
            
            return passages[direction];
        }

        public bool TryGetFree(out Passage freePassage)
        {
            foreach (var passage in passages)
            {
                if (passage.Value.ConnectedRoom == null)
                {
                    freePassage = passage.Value;
                    return true;
                }
            }

            freePassage = null;
            return false;
        }

        public void BuildPassages(List<Direction> directions)
        {
            int firstToDelete = Random.Range(0, 4);            
            int second = Random.Range(0, 4);

            if (!directions.Contains((Direction)firstToDelete))
            {
                Destroy(passages[(Direction)firstToDelete].gameObject);
                passages.Remove((Direction)firstToDelete);
            }

            if (firstToDelete != second && !directions.Contains((Direction)second))
            {
                Destroy(passages[(Direction)second].gameObject);
                passages.Remove((Direction) second);
            }
        }

        public void CloseAllFree()
        {
            foreach (var passage in passages)
            {
                if (passage.Value.ConnectedRoom == null)
                    Destroy(passage.Value.gameObject);                    
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireCube(transform.position, RoomSize);
        }
    }

    public enum Direction
    {
        Up = 0,
        Down = 1,
        Right = 2,
        Left = 3
    }
}