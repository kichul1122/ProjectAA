using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

namespace AA.Assets._Scripts.UI.MVVM
{
    internal class View : MonoBehaviour
    {
        //UI

        //UI로직
        
        public TMPro.TextMeshProUGUI levelText;

        public Button btn;

        public void Binding(ViewModel viewModel)
        {
            viewModel.LevelRP.Subscribe(_ => SetLevelText(_.ToString())).AddTo(this);

            viewModel.onClickCommand.BindTo(btn).AddTo(this);

            viewModel.onShowToastommand.Subscribe(_ => ShowToast()).AddTo(this);
        }

        public void SetLevelText(string level)
        {
            levelText.text = level;
        }

        public void ShowToast()
        {
            Debug.Log("Show Toast");
        }
    }
}
