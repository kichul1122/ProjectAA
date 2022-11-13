using MessagePack;
using Sirenix.OdinInspector;
using System;

namespace AA
{


    /// <summary>
    /// Global Model Manager
    /// </summary>
    public class ModelManager : ManagerMonobehaviour
    {
        public long DefaultPlayerId = 1122;

        public StartUpSceneModel StartUp => Model.StartUpScene;

        public FieldSceneModel FieldScene => Model.FieldScene;

        public CharacterModels Character => Model.Character;

        public Model Model = new Model();

        public void OnDestroy()
        {
            Model.Dispose();
        }

        [Button]
        public void DoMessagePack()
        {
            DB.Character character = new DB.Character();

            // Call Serialize/Deserialize, that's all.
            byte[] bytes = MessagePackSerializer.Serialize(character);
            DB.Character mc2 = MessagePackSerializer.Deserialize<DB.Character>(bytes);

            var dataToJson = MessagePackSerializer.SerializeToJson(character);
            var jsonToBin = MessagePackSerializer.ConvertFromJson(dataToJson);
            DB.Character mc3 = MessagePackSerializer.Deserialize<DB.Character>(jsonToBin);
            Console.WriteLine(dataToJson);

            // You can dump MessagePack binary blobs to human readable json.
            // Using indexed keys (as opposed to string keys) will serialize to MessagePack arrays,
            // hence property names are not available.
            // [99,"hoge","huga"]
            var binToJson = MessagePackSerializer.ConvertToJson(bytes);
            Console.WriteLine(binToJson);
        }

        public void LoadStartUp()
        {
            StartUp.Load();
        }
    }
}
