using FancyScrollView;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AA
{
    public abstract class SimpleFancyGridCell<TItemData, TContext> : FancyGridViewCell<TItemData, TContext> where TContext : class, IFancyGridViewContext, new()
    {
        
    }
}
