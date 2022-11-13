using MasterMemory;
using MessagePack;
using Sirenix.OdinInspector;

namespace AA.Meta
{
    [MemoryTable("character"), MessagePackObject(true)]
    [System.Serializable]
    public class Character
    {
        [PrimaryKey]
        [ShowInInspector]
        public long CharacterId { get; }

        public Character(long characterId)
        {
            this.CharacterId = characterId;
        }
    }
}
