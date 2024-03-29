using MessagePack;
using Sirenix.OdinInspector;
using System;
using UnityEngine;

namespace AA
{


    /// <summary>
    /// Global Model Manager
    /// </summary>
    public class ModelManager : ManagerMonobehaviour
    {
        public StartUpSceneModel StartUp => Model.StartUpScene;
        public FieldSceneModel FieldScene => Model.FieldScene;
        public CharacterModels Character => Model.Character;

        public PlayerStatModel PlayerStat => Model.PlayerStat;

        public Model Model = new();

        public void OnDestroy()
        {
            Model.Dispose();
        }

        [Button]
        public void DoMessagePack()
        {
            CharacterDB character = new CharacterDB();

            // Call Serialize/Deserialize, that's all.
            byte[] bytes = MessagePackSerializer.Serialize(character);
            CharacterDB mc2 = MessagePackSerializer.Deserialize<CharacterDB>(bytes);

            var dataToJson = MessagePackSerializer.SerializeToJson(character);
            var jsonToBin = MessagePackSerializer.ConvertFromJson(dataToJson);
            CharacterDB mc3 = MessagePackSerializer.Deserialize<CharacterDB>(jsonToBin);
            Console.WriteLine(dataToJson);

            // You can dump MessagePack binary blobs to human readable json.
            // Using indexed keys (as opposed to string keys) will serialize to MessagePack arrays,
            // hence property names are not available.
            // [99,"hoge","huga"]
            var binToJson = MessagePackSerializer.ConvertToJson(bytes);
            Console.WriteLine(binToJson);
        }

        public void Construct()
        {
            var characterModel = Character.Find(AADefine.First.CharacterModelId);
            PlayerStatModel playerStatModel = new PlayerStatModel(characterModel, Model.Item);

            Model.SetStatModel(playerStatModel);
        }

        [Button]
        public void ToJson()
        {
            Debug.Log(Model.ToJson());
        }
    }
}
