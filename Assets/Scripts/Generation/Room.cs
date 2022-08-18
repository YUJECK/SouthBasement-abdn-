using UnityEngine;

namespace Generation
{
    public class Room : RoomTemplate
    {
        protected override void OnSpawned()
        {
        }
        protected override void StartSpawning()
        {
            //Спавн комнат
            if (GetPassage(Directions.Up) != null)
            {
                GetPassage(Directions.Up).SetOwnRoom(this);
                GetPassage(Directions.Up).StartSpawnningRoom(ManagerList.GenerationManager.RoomsSpawnOffset + 0.05f);
            }
            if (GetPassage(Directions.Down) != null)
            {
                GetPassage(Directions.Down).SetOwnRoom(this);
                GetPassage(Directions.Down).StartSpawnningRoom(ManagerList.GenerationManager.RoomsSpawnOffset + 0.07f);
            }
            if (GetPassage(Directions.Left) != null)
            {
                GetPassage(Directions.Left).SetOwnRoom(this);
                GetPassage(Directions.Left).StartSpawnningRoom(ManagerList.GenerationManager.RoomsSpawnOffset + 0.09f);
            }
            if (GetPassage(Directions.Right) != null)
            {
                GetPassage(Directions.Right).SetOwnRoom(this);
                GetPassage(Directions.Right).StartSpawnningRoom(ManagerList.GenerationManager.RoomsSpawnOffset + 0.11f);
            }
        }

        //Юнитивские методы
        private void Start() { if (randomizePassagesOnAwake) RandomizePassages(); StartSpawning(); }
        private void OnDestroy() => DefaultOnDestroy();
    }
}