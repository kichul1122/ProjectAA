using Cysharp.Threading.Tasks;
using System.Threading;

namespace AA
{
    public class MetaManager : ManagerMonobehaviour
    {
        public MemoryDatabase DB { get; private set; }

        //public CharacterTable CharacterTable => CharacterDBTable;

        public async UniTask LoadAsync(CancellationToken ct)
        {
            var builder = new DatabaseBuilder();

            //Load Json To Meta (Load MessagePack To Meta)

            await LoadJsonAsync(ct);

            //builder.Append(new Meta.Character[]
            //{
            //    new Meta.Character(characterId: 1122),
            //});

            byte[] data = builder.Build();
            this.DB = new MemoryDatabase(data);
        }

        private async UniTask LoadJsonAsync(CancellationToken ct)
        {
            await UniTask.Delay(100);
        }
    }
}
