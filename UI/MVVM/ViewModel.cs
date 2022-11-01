using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.UI;
using UniRx;
using Sirenix.OdinInspector;

namespace AA.Assets._Scripts.UI.MVVM
{
    /// <summary>
    /// Presentation Logic
    /// State
    /// </summary>
    public class ViewModel : IDisposable
    {
        public int ClickCount { get; set; }

        public ReactiveProperty<int> LevelRP = new ReactiveProperty<int>();

        public ReactiveCommand onClickCommand { get; set; } //DoClick;
        public ReactiveCommand onShowToastommand { get; set; } //ShowToast;

        private CompositeDisposable disposables = new CompositeDisposable();

        public ViewModel(Service service, CharacterData characterData)
        {
            onClickCommand.Subscribe(async _ => 
            { 
                await service.OnClickCountAsync(); 
            });

            characterData.ClickCount.Subscribe(_ => ClickCount = _).AddTo(disposables);
        }

        public void Dispose()
        {
            onClickCommand.Dispose();
            onShowToastommand.Dispose();
            disposables.Dispose();
        }

        [Button]
        private void DoLevelUp()
        {
            LevelRP.Value++;
        }

        [Button]
        private void DoShowToast()
        {
            onShowToastommand.Execute();
        }
    }
}
