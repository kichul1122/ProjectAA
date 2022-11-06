using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UniRx;
using Sirenix.OdinInspector;
using ObservableCollections;
using System.Collections.Specialized;

namespace AA
{
    public class MVPPresenter : MonoBehaviour
    {
        public MVPModel model = new MVPModel();
        public MVPView view;

        public GameObject CellPrefab;
        public Transform CellParent;

        //public void Constructor(MVPModel model, MVPView view)
        //{
        //    this.model = model;
        //    this.view = view;
        //}

        private void Awake()
        {
            Binding();
        }

        public void Binding()
        {
            model.Level.Subscribe(_ => view.SetLevelText(_.ToString())).AddTo(this);
            model.Grade.Subscribe(_ => view.SetGradeText(_.ToString())).AddTo(this);

            model.ObservableCells
                .CreateView(this)
                .OnChangedAsObservable()
                .Subscribe(changedAction =>
                {
                    view.SetDatas(model.ObservableCells);
                }).AddTo(this); ;
        }

        [Button]
        private void AddCell(long level)
        {
            model.ObservableCells.Add(new DefaultItemData() { LevelRP = new ReactiveProperty<long>(level)});
        }

        [Button]
        private void OnDestroy()
        {
            model.Dispose();
        }
    }
}
