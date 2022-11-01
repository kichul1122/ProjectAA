using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.U2D;
using UnityEngine.UI;

namespace AA
{
    public class KCPopup : MonoBehaviour
    {
        public Image image_0;
        public Image image_1;
        public Image image_2;
        public Image image_3;
        public Image image_4;

        [Button]
        public async UniTask DoStart()
        {
            var atlas = await Addressables.LoadAssetAsync<SpriteAtlas>($"Assets/_Resources/Sprite/KCAtlas.spriteatlasv2");
            image_0.sprite = atlas.GetSprite("icons8-apple-logo-100");
            image_1.sprite = atlas.GetSprite("icons8-chrome-100");
            image_2.sprite = atlas.GetSprite("icons8-discord-100");
            image_3.sprite = atlas.GetSprite("money");
        }

        [Button]
        public async UniTask DoLoadSprite()
        {
            var atlas = await Addressables.LoadAssetAsync<Sprite>($"Assets/_Resources/Sprite/KCAtlas.spriteatlasv2[icons8-apple-logo-100]");
            image_0.sprite = atlas;
            image_1.sprite = atlas;
            image_2.sprite = atlas;
            image_3.sprite = atlas;
            image_4.sprite = atlas;
        }

    }
}
