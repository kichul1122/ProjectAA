using MasterMemory;
using MessagePack;
using Sirenix.OdinInspector;

namespace AA
{
    [MemoryTable("character"), MessagePackObject(true)]
    [System.Serializable]
    public class CharacterMetaData
    {
        [PrimaryKey]
        [ShowInInspector]
        public long CharacterId { get; }

        public CharacterMetaData(long characterId)
        {
            this.CharacterId = characterId;
        }
    }
}
