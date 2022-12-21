// <auto-generated />
#pragma warning disable CS0105
using AA;
using MasterMemory.Validation;
using MasterMemory;
using MessagePack;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using System;
using AA.Tables;

namespace AA
{
   public sealed class ImmutableBuilder : ImmutableBuilderBase
   {
        MemoryDatabase memory;

        public ImmutableBuilder(MemoryDatabase memory)
        {
            this.memory = memory;
        }

        public MemoryDatabase Build()
        {
            return memory;
        }

        public void ReplaceAll(System.Collections.Generic.IList<CharacterMetaData> data)
        {
            var newData = CloneAndSortBy(data, x => x.CharacterId, System.Collections.Generic.Comparer<long>.Default);
            var table = new CharacterMetaDataTable(newData);
            memory = new MemoryDatabase(
                table
            
            );
        }

        public void RemoveCharacterMetaData(long[] keys)
        {
            var data = RemoveCore(memory.CharacterMetaDataTable.GetRawDataUnsafe(), keys, x => x.CharacterId, System.Collections.Generic.Comparer<long>.Default);
            var newData = CloneAndSortBy(data, x => x.CharacterId, System.Collections.Generic.Comparer<long>.Default);
            var table = new CharacterMetaDataTable(newData);
            memory = new MemoryDatabase(
                table
            
            );
        }

        public void Diff(CharacterMetaData[] addOrReplaceData)
        {
            var data = DiffCore(memory.CharacterMetaDataTable.GetRawDataUnsafe(), addOrReplaceData, x => x.CharacterId, System.Collections.Generic.Comparer<long>.Default);
            var newData = CloneAndSortBy(data, x => x.CharacterId, System.Collections.Generic.Comparer<long>.Default);
            var table = new CharacterMetaDataTable(newData);
            memory = new MemoryDatabase(
                table
            
            );
        }

    }
}