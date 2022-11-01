using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DI
{
    public class ActorView : MonoBehaviour
    {
        public void Log()
		{
            Debug.Log(nameof(ActorView));
		}

        [Button]
        public void LoadAdditiveScene(string sceneName)
		{
            UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(sceneName, UnityEngine.SceneManagement.LoadSceneMode.Additive);
		}
    }
}
