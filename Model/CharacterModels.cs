using System;
using UniRx;

namespace AA
{
    [System.Serializable]
    public class CharacterModels : IDisposable
    {
        public ReactiveCollection<CharacterModel> Models = new ReactiveCollection<CharacterModel>();

        public void Add(CharacterModel characterData)
        {
            Models.Add(characterData);
        }

        public bool Remove(string seq)
        {
            return Models.RemoveAndDispose(_ => _.Seq == seq);
        }

        public CharacterModel Find(long characterId)
        {
            return Models.Find(_ => _.Meta.CharacterId == characterId);
        }

        public void Dispose()
        {
            Models?.ForEach(data => data.Dispose());
            Models?.Dispose();
            Models?.Clear();
        }
    }
}
