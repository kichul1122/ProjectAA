using AA.Meta;
using AA.Meta.Tables;

namespace AA
{
    public class MetaManager : ManagerMonobehaviour
    {
        public MemoryDatabase DB { get; private set; }

        public CharacterTable CharacterTable => DB.CharacterTable;

        public void Load()
        {
            var builder = new DatabaseBuilder();

            //Load Json To Meta (Load MessagePack To Meta)

            builder.Append(new Meta.Character[]
            {
                new Meta.Character(characterId: 1122),
            });

            byte[] data = builder.Build();
            this.DB = new MemoryDatabase(data);
        }
    }
}
