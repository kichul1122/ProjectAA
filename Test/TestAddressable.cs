using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

namespace AA
{
    public class TestAddressable : MonoBehaviour
    {
        public SpriteAtlas atlas;

        public Sprite[] sprites;

        public int spritesCount;

        [Button]
        public void DoGet()
        {
            spritesCount = atlas.GetSprites(sprites);
            Debug.Log($"{spritesCount}");
        }
    }
}
