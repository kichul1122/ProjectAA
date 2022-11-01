using MasterMemory;
using MessagePack;
using UnityEngine;

namespace AA.Meta
{
    [MemoryTable("character"), MessagePackObject(true)]
    [System.Serializable]
    public class Character
    {
        [PrimaryKey]
        [field: SerializeField]
        public long CharacterId { get; }

        public Character(long characterId)
        {
            this.CharacterId = characterId;
        }
    }
}
