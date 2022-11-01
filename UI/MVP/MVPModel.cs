using ObservableCollections;
using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniRx;

namespace AA
{
    [System.Serializable]
    public class MVPModel : IDisposable
    {
        [ShowInInspector]
        [DisableInPlayMode]
        public ReactiveProperty<long> Level = new ReactiveProperty<long>();
        [ShowInInspector]
        [DisableInPlayMode]
        public ReactiveProperty<long> Grade = new ReactiveProperty<long>();

        public List<DefaultItemData> Cells = new List<DefaultItemData>();

        [ShowInInspector]
        public ObservableList<DefaultItemData> ObservableCells = new ObservableList<DefaultItemData>();
    
        [Button]
        public void LevelUp()
        {
            Level.Value++;

            
        }
        
        [Button]
        public void GradeUp()
        {
            Grade.Value++;
        }

        public void Dispose()
        {
            Level.Dispose();
            Grade.Dispose();
        }
    }
}
