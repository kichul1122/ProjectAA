using System;
using UniRx;

namespace AA
{
    [System.Serializable]
    public class CharacterModels : IDisposable
    {
        public ReactiveCollection<CharacterModel> Models = new();

        public void Add(CharacterModel model)
        {
            if (Models.Find(_ => _.Seq == model.Seq) == null) return;

            Models.Add(model);
        }

        public void Remove(string seq)
        {
            Remove(itemModel => itemModel.Seq == seq);
        }

        public void Remove(Predicate<CharacterModel> predicate)
        {
            for (int i = Models.Count - 1; i >= 0; i--)
            {
                if (predicate(Models[i]))
                {
                    Models[i].Dispose();
                    Models.RemoveAt(i);
                }
            }
        }

        public CharacterModel Find(long characterId)
        {
            return Models.Find(_ => _.Meta.CharacterId == characterId);
        }

        public void Dispose()
        {
            Models.ForEach(data => data.Dispose());
            Models.Clear();
            Models.Dispose();
            Models = null;

        }
    }
}
