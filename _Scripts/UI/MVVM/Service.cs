using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace AA.Assets._Scripts.UI.MVVM
{
    public class Service : MonoBehaviour
    {
        private CharacterData characterData;

        public void Constructor(CharacterData characterData)
        {
            this.characterData = characterData;
        }

        public async UniTask OnClickCountAsync()
        {
            await UniTask.Delay(1000); //Network Delay

            characterData.ClickCount.Value++; //DB Logic
        }
    }
}
