using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace AA
{
    public class ProjectLifetimeScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            Debug.Log("ProjectLifetimeScope");
        }
    }
}
