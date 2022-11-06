using System;
using UniRx;

namespace AA
{
    [System.Serializable]
    public class CharacterDatas : IDisposable
    {
        public ReactiveCollection<CharacterData> Datas { get; set; } = new ReactiveCollection<CharacterData>();

        public void Add(CharacterData characterData)
        {
            Datas.Add(characterData);
        }

        public bool Remove(string seq)
        {
            return Datas.RemoveAndDispose(_ => _.Seq == seq);
        }

        public CharacterData Find(long characterId)
        {
            return Datas.Find(_ => _.Meta.CharacterId == characterId);
        }

        public void Dispose()
        {
            Datas?.ForEach(data => data.Dispose());
            Datas?.Dispose();
            Datas?.Clear();
        }
    }
}
