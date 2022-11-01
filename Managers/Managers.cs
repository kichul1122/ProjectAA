using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using System;
using UnityEngine;

namespace AA
{
    public class Managers : MonoBehaviour
    {
        [ShowInInspector]
        [ReadOnly]
        public static SceneManager Scene { get; set; }

        [ShowInInspector]
        [ReadOnly]
        public static FadeManager Fade { get; set; }

        [ShowInInspector]
        [ReadOnly]
        public static UIManager UI { get; set; }

        [ShowInInspector]
        [ReadOnly]
        public static ResourceManager Resource { get; set; }

        [ShowInInspector]
        [ReadOnly]
        public static ObjectManager Object { get; set; }

        [ShowInInspector]
        [ReadOnly]
        public static DataManager Data { get; set; }

        [ShowInInspector]
        [ReadOnly]
        public static MetaManager Meta { get; set; }

        [ShowInInspector]
        [ReadOnly]
        public static NetworkManager Network { get; set; }

        [ShowInInspector]
        [ReadOnly]
        public static MessagePipeManager MessagePipe { get; set; }

        public static void SetManager<T>(T manager) where T : ManagerMonobehaviour
        {
            switch (manager)
            {
                case UIManager ui: UI = ui; break;
                case SceneManager scene: Scene = scene; break;
                case FadeManager fade: Fade = fade; break;
                case ResourceManager resource: Resource = resource; break;
                case ObjectManager @object: Object = @object; break;
                case DataManager data: Data = data; break;
                case MetaManager meta: Meta = meta; break;
                case NetworkManager network: Network = network; break;
                case MessagePipeManager messagePipe: MessagePipe = messagePipe; break;

                default: throw new ArgumentException();
            }
        }

        public static void DoReset()
        {
            Scene = default;
            Fade = default;
            UI = default;
            Resource = default;
            Object = default;
            Data = default;
            Meta = default;
            Network = default;
            MessagePipe = default;
        }

        [Button]
        public static async UniTaskVoid RestartApp()
        {
            await Fade.FadeOutAsync();

            await UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(ESceneName.Empty.ToString(), UnityEngine.SceneManagement.LoadSceneMode.Single);

            await UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(ESceneName.Main.ToString(), UnityEngine.SceneManagement.LoadSceneMode.Single);
        }
    }
}