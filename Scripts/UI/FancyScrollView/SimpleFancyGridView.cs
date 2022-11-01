using FancyScrollView;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AA
{
    public abstract class SimpleFancyGridView<TItemData, TContext> : FancyGridView<TItemData, TContext> where TContext : class, IFancyGridViewContext, new()
    {
        [SerializeField] SimpleFancyGridCell<TItemData, TContext> cellPrefab = default;

        protected override void SetupCellTemplate() => Setup<DefaultCellGroup>(cellPrefab);
    }
}
