using UnityEngine;

namespace TheRat.Generation
{
    [CreateAssetMenu]
    public class RoomsContainer : ScriptableObject
    {
        public int RoomsCount;
        
        public Room[] StartRooms;
        public Room[] FightRooms;
        public Room[] NPCRooms;
        public Room[] ExitRooms;

        public Room GetRandomStart()
        {
            int randomRoom = Random.Range(0, StartRooms.Length);
            return StartRooms[randomRoom];
        }
        public Room GetRandomFight()
        {
            int randomRoom = Random.Range(0, FightRooms.Length);
            return FightRooms[randomRoom];
        }
        public Room GetRandomNPC()
        {
            int randomRoom = Random.Range(0, NPCRooms.Length);
            return NPCRooms[randomRoom];
        }
        public Room GetRandomExit()
        {
            int randomRoom = Random.Range(0, ExitRooms.Length);
            return ExitRooms[randomRoom];
        }
    }
}