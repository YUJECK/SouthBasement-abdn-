using System;
using System.Collections.Generic;
using AYellowpaper.SerializedCollections;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

namespace SouthBasement.Generation
{
    public class Room : MonoBehaviour
    {
        [SerializeField] private SerializedDictionary<Direction, Passage> passages;
        [field: SerializeField] public Vector2 RoomSize { get; private set; }
        
        private void Awake()
        {
            foreach (var passage in passages)
                passage.Value.Factory.Init(this, passage.Key);
            
            OpenAllDoors();
        }


        public void OpenAllDoors()
        {
            foreach (var passage in passages)
                passage.Value.OpenDoor();
        }

        public void CloseAllDoors()
        {
            foreach (var passage in passages)
                passage.Value.CloseDoor();
        }
        
        public Vector2 GetOffCenter(Direction direction)
        {
            return passages[direction].Factory.transform.localPosition;
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
                passages[(Direction)firstToDelete].DisablePassage();
                passages.Remove((Direction)firstToDelete);
            }

            if (firstToDelete != second && !directions.Contains((Direction)second))
            {
                passages[(Direction)second].DisablePassage();
                passages.Remove((Direction) second);
            }
        }

        public void CloseAllFree()
        {
            List<Direction> toRemove = new();

            foreach (var passage in passages)
            {
                if (passage.Value.ConnectedRoom == null)
                {
                    passage.Value.DisablePassage();                    
                    toRemove.Add(passage.Key);
                }
            }

            foreach (var direction in toRemove)
                passages.Remove(direction);
        }

        private void OnValidate()
        {
            if (TryGetComponent<BoxCollider2D>(out BoxCollider2D boxCollider2D))
                boxCollider2D.size = RoomSize;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireCube(transform.position, RoomSize);
        }
    }
}