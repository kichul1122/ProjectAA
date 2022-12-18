using AA.Tables;
using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using System.Threading;

namespace AA
{
    public class MetaManager : ManagerMonobehaviour
    {
        public MemoryDatabase DB { get; private set; }

        public CharacterMetaDataTable Character => DB.CharacterMetaDataTable;

        public async UniTask LoadAsync(CancellationToken ct)
        {
            var builder = new DatabaseBuilder();

            //Load Json To Meta (Load MessagePack To Meta)

            await LoadMeta(builder, ct);

            //builder.Append(new Meta.Character[]
            //{
            //    new Meta.Character(characterId: 1122),
            //});

            byte[] data = builder.Build();
            this.DB = new MemoryDatabase(data);
        }

        private async UniTask LoadMeta(DatabaseBuilder builder, CancellationToken ct)
        {
            builder.Append(new CharacterMetaData[]
            {
                new CharacterMetaData(AADefine.First.CharacterModelId, new List<StatModifier>() { new StatModifier(EStat.MoveSpeed, 8d)}),
            });

            await UniTask.Delay(100);
        }

        [Button]
        public void TestDiff()
        {
            var newBuilder = DB.ToImmutableBuilder();
            newBuilder.Diff(new CharacterMetaData[]
            {
                new CharacterMetaData(AADefine.First.CharacterModelId, new List<StatModifier>() { new StatModifier(EStat.MoveSpeed, 16d)}),
            });
        }
    }
}
