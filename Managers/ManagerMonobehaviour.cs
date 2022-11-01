using UnityEngine;

namespace AA
{
    public abstract class ManagerMonobehaviour : MonoBehaviour
    {
        protected virtual void Awake()
        {
            Managers.SetManager(this);
        }
    }
}