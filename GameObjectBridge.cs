using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AA
{
    public class GameObjectBridge : MonoBehaviour
    {
        public GameObject _playerGO;

        public bool IsExistPlayer() => _playerGO != null;

        public void MovePlayerCurrentSceneToBridge(GameObject playerGO)
        {
            Managers.Scene.MoveGameObjectToScene(playerGO, EAdditiveSceneName.Manager);

            _playerGO = playerGO;
        }

        public GameObject MovePlayerBridgeToCurrentScene(ESceneName eSceneName)
        {
            Managers.Scene.MoveGameObjectToScene(_playerGO, eSceneName);

            var returnValue = _playerGO;
            _playerGO = null;

            return returnValue;
        }
    }
}
