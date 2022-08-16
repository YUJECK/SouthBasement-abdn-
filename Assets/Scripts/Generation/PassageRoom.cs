using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Generation
{
    public class PassageRoom : RoomTemplate
    {
        protected override void OnSpawned()
        {
        }
        protected override void StartSpawning()
        {
            //Спавн комнат
            if (upPassage != null)
            {
                GetPassage(Directions.Up).SetOwnRoom(this);
                GetPassage(Directions.Up).StartSpawnningRoom(ManagerList.GenerationManager.RoomsSpawnOffset + 0.05f);
            }
            if (downPassage != null)
            {
                GetPassage(Directions.Down).SetOwnRoom(this);
                GetPassage(Directions.Down).StartSpawnningRoom(ManagerList.GenerationManager.RoomsSpawnOffset + 0.07f);
            }
            if (leftPassage != null)
            {
                GetPassage(Directions.Left).SetOwnRoom(this);
                GetPassage(Directions.Left).StartSpawnningRoom(ManagerList.GenerationManager.RoomsSpawnOffset + 0.09f);
            }
            if (rightPassage != null)
            {
                GetPassage(Directions.Right).SetOwnRoom(this);
                GetPassage(Directions.Right).StartSpawnningRoom(ManagerList.GenerationManager.RoomsSpawnOffset + 0.11f);
            }
            Utility.InvokeMethod(OnSpawned, ManagerList.GenerationManager.RoomsSpawnOffset + 0.11f);
        }

        //Юнитивские методы
        private void Start()
        {
            if (randomizePassagesOnAwake) RandomizePassages();
            StartSpawning();
        }
        private void OnDestroy() => DefaultOnDestroy();
    }
}
