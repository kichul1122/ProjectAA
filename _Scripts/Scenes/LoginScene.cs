using Cysharp.Threading.Tasks;
using UnityEngine;

namespace AA
{
    public class LoginScene : MonoBehaviour, IScene
    {
        public ESceneName Name => ESceneName.Login;

        public async UniTask StartAsync()
        {
            await Managers.Fade.FadeInAsync();

            await LogInAsync();

            await LoadDataAsync();

            await Managers.Fade.FadeOutAsync();

            Managers.Scene.ChangeAsync(ESceneName.Lobby).Forget();
        }

        public async UniTask DisposeAsync()
        {
            await UniTask.Yield();
        }

        public async UniTask LogInAsync()
        {
            Debug.Log($"LogIn");

            await UniTask.Yield();
        }

        public async UniTask LoadDataAsync()
        {
            Debug.Log($"Load Remote Data");

            Server.Character server = new Server.Character();
            server.Seq = System.Guid.NewGuid().ToString();
            server.Id = Managers.Data.DefaultPlayerId;

            Meta.Character meta = Managers.Meta.CharacterTable.FindByCharacterId(server.Id);

            CharacterData playerData = new CharacterData(server, meta);

            Managers.Data.Character.Add(playerData);

            await UniTask.Yield();
        }
    }
}