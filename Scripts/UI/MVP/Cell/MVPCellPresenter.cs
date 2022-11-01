using System.Collections;
using UnityEngine;
using UniRx;

namespace AA
{
    public class MVPCellPresenter : MonoBehaviour
    {
        public MVPCellView View;
          
        public void Constructor(MVPCellModel model)
        {
            model.Count.Subscribe(x => View.SetCountText(x.ToString())).AddTo(this);
            View.SetIdText(model.Id.ToString());
        }
    }
}