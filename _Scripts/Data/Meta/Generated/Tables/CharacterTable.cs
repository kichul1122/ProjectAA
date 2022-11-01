// <auto-generated />
#pragma warning disable CS0105
using AA.Meta;
using MasterMemory.Validation;
using MasterMemory;
using MessagePack;
using System.Collections.Generic;
using System;
using UnityEngine;

namespace AA.Meta.Tables
{
   public sealed partial class CharacterTable : TableBase<Character>, ITableUniqueValidate
   {
        public Func<Character, long> PrimaryKeySelector => primaryIndexSelector;
        readonly Func<Character, long> primaryIndexSelector;


        public CharacterTable(Character[] sortedData)
            : base(sortedData)
        {
            this.primaryIndexSelector = x => x.CharacterId;
            OnAfterConstruct();
        }

        partial void OnAfterConstruct();


        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public Character FindByCharacterId(long key)
        {
            var lo = 0;
            var hi = data.Length - 1;
            while (lo <= hi)
            {
                var mid = (int)(((uint)hi + (uint)lo) >> 1);
                var selected = data[mid].CharacterId;
                var found = (selected < key) ? -1 : (selected > key) ? 1 : 0;
                if (found == 0) { return data[mid]; }
                if (found < 0) { lo = mid + 1; }
                else { hi = mid - 1; }
            }
            return ThrowKeyNotFound(key);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public bool TryFindByCharacterId(long key, out Character result)
        {
            var lo = 0;
            var hi = data.Length - 1;
            while (lo <= hi)
            {
                var mid = (int)(((uint)hi + (uint)lo) >> 1);
                var selected = data[mid].CharacterId;
                var found = (selected < key) ? -1 : (selected > key) ? 1 : 0;
                if (found == 0) { result = data[mid]; return true; }
                if (found < 0) { lo = mid + 1; }
                else { hi = mid - 1; }
            }
            result = default;
            return false;
        }

        public Character FindClosestByCharacterId(long key, bool selectLower = true)
        {
            return FindUniqueClosestCore(data, primaryIndexSelector, System.Collections.Generic.Comparer<long>.Default, key, selectLower);
        }

        public RangeView<Character> FindRangeByCharacterId(long min, long max, bool ascendant = true)
        {
            return FindUniqueRangeCore(data, primaryIndexSelector, System.Collections.Generic.Comparer<long>.Default, min, max, ascendant);
        }


        void ITableUniqueValidate.ValidateUnique(ValidateResult resultSet)
        {
#if !DISABLE_MASTERMEMORY_VALIDATOR

            ValidateUniqueCore(data, primaryIndexSelector, "CharacterId", resultSet);       

#endif
        }

#if !DISABLE_MASTERMEMORY_METADATABASE

        public static MasterMemory.Meta.MetaTable CreateMetaTable()
        {
            return new MasterMemory.Meta.MetaTable(typeof(Character), typeof(CharacterTable), "character",
                new MasterMemory.Meta.MetaProperty[]
                {
                    new MasterMemory.Meta.MetaProperty(typeof(Character).GetProperty("CharacterId")),
                },
                new MasterMemory.Meta.MetaIndex[]{
                    new MasterMemory.Meta.MetaIndex(new System.Reflection.PropertyInfo[] {
                        typeof(Character).GetProperty("CharacterId"),
                    }, true, true, System.Collections.Generic.Comparer<long>.Default),
                });
        }

#endif
    }
}