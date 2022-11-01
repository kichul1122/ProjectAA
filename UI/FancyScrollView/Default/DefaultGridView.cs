using FancyScrollView;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace AA
{
    [System.Serializable]
    public class DefaultItemData
    {
        public ReactiveProperty<long> LevelRP = new ReactiveProperty<long>();

        [Button]
        private void LevelUp()
        {
            LevelRP.Value++;
        }
    }

    public class DefaultContext : FancyGridViewContext
    {
        public int SelectedIndex = -1;
        public Action<int> OnCellClicked;
    }

    public class DefaultGridView : FancyGridView<DefaultItemData, DefaultContext>
    {
        class CellGroup : DefaultCellGroup { }

        [SerializeField] DefaultGridCell cellPrefab = default;

        protected override void SetupCellTemplate() => Setup<CellGroup>(cellPrefab);
    }
}
