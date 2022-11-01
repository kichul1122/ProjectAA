using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AA
{
    public partial class Enemy : MonoBehaviour
    {
        public class Factory
        {
            public Enemy Create()
            {
                return null;
            }
        }
    }
}
