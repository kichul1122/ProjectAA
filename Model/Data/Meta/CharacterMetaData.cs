using MasterMemory;
using MessagePack;
using Sirenix.OdinInspector;
using System.Collections.Generic;

namespace AA
{
    [MemoryTable("character"), MessagePackObject(true)]
    [System.Serializable]
    public class CharacterMetaData
    {
        [PrimaryKey]
        [ShowInInspector]
        public long CharacterId { get; }

        public List<StatModifier> StatModifiers { get; }

        public CharacterMetaData(long characterId, List<StatModifier> statModifiers)
        {
            this.CharacterId = characterId;
            this.StatModifiers = statModifiers;
        }
    }
}
