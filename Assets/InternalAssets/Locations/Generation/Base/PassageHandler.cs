using System.Collections.Generic;
using AYellowpaper.SerializedCollections;
using UnityEngine;
    
namespace SouthBasement.Generation
{
    [RequireComponent(typeof(Room))]
    public sealed class PassageHandler : MonoBehaviour
    {
        [SerializeField] private SerializedDictionary<Direction, Passage> passages;
        private Room _room;
        
        private void Awake()
        {
            _room = GetComponent<Room>();
            
            foreach (var passage in passages)
                passage.Value.Factory.Init(_room, passage.Key);
            
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
        
        public Passage GetPassage(Direction direction)
            => passages.TryGetValue(direction, out var passage) ? passage : null;

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
    }
}