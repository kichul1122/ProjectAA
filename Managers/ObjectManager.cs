﻿using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine;

namespace AA
{
    public class ObjectManager : ManagerMonobehaviour
    {
        public Dictionary<int, Character> Players = new Dictionary<int, Character>();
        public Dictionary<int, Character> Enemies = new Dictionary<int, Character>();

        public GameObjectBridge _gameObjectBridge;
        protected override void Awake()
        {
            base.Awake();

            _gameObjectBridge = this.GetOrAddComponent<GameObjectBridge>();
        }

        public void AddPlayer(Character playerCharacter)
        {
            Players.Add(playerCharacter.ObjectId, playerCharacter);

            playerCharacter.Observable.OnRemoveObservable().Subscribe(RemovePlayer).AddTo(this);
        }

        public void AddEnemy(Character enemyCharacter)
        {
            Enemies.Add(enemyCharacter.ObjectId, enemyCharacter);

            enemyCharacter.Observable.OnRemoveObservable().Subscribe(RemoveEnemy).AddTo(this);
        }

        private void RemovePlayer(Character playerCharacter)
        {
            Players.Remove(playerCharacter.ObjectId);
        }

        private void RemoveEnemy(Character enemyCharacter)
        {
            Enemies.Remove(enemyCharacter.ObjectId);
        }

        public bool IsExistPlayer() => _gameObjectBridge.IsExistPlayer();

        public void MovePlayerCurrentSceneToBridge()
        {
            _gameObjectBridge.MovePlayerCurrentSceneToBridge(Players.Values.First().gameObject);
        }

        public GameObject MovePlayerBridgeToCurrentScene(ESceneName eSceneName)
        {
            return _gameObjectBridge.MovePlayerBridgeToCurrentScene(eSceneName);

        }
    }
}