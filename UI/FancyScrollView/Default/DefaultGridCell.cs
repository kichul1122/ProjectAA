using FancyScrollView;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using System;
using Sirenix.OdinInspector;

namespace AA
{
    /// <summary>
    /// Cell Presenter
    /// </summary>
    public class DefaultGridCell : FancyGridViewCell<DefaultItemData, DefaultContext>
    {
        [Serializable]
        public class View
        {
            public Text text;
            
            public void SetText(string value)
            {
                text.text = value;
            }
        }

        public View view;

        [DisableInEditorMode]
        public DefaultItemData model;
        
        private CompositeDisposable disposables = new CompositeDisposable();

        public override void UpdateContent(DefaultItemData itemData)
        {
            model = itemData;
            
            if (disposables.Count > 0)
            {
                disposables.Clear();
            }
            
            model.LevelRP.Subscribe(_ => view.SetText(_.ToString())).AddTo(disposables);
        }

        private void OnDisable()
        {
            disposables.Clear();
        }

        private void OnDestroy()
        {
            disposables.Dispose();
        }
    }
}
