using ObservableCollections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniRx;

namespace AA.Assets._Scripts.UI.MVVM
{
    //View 은 ViewModel 을 알지만 ViewModel 은 View를 알 수 없고, ViewModel은 Model을 알지만 Model은 ViewModel을 몰라야한다.
    //https://jjjoonngg.github.io/android%20architecture/MVVM/

    //Model
    //비즈니스로직
    //데이터 캡슐화

    public class Character
    {
        public string Seq { get; set; }
        public int ClickCount { get; set; }
        public int Level { get; set; }
        public int Grade { get; set; }
    }
    
    public class CharacterData : IDisposable
    {
        public string Seq { get; set; }
        public ReactiveProperty<int> ClickCount { get; set; } = new ReactiveProperty<int>();
        public ReactiveProperty<int> Level { get; set; } = new ReactiveProperty<int>();

        public int Grade { get; set; }

        public void Dispose()
        {
            ClickCount?.Dispose();
            Level?.Dispose();
        }

        public void From(Character data)
        {
            Seq = data.Seq;
            ClickCount.Value = data.ClickCount;
            Level.Value = data.Level;
            Grade = data.Grade;
        }


        public Character To()
        {
            return new Character
            {
                Seq = Seq,
                ClickCount = ClickCount.Value,
                Level = Level.Value,
                Grade = Grade
            };
        }
    }

    public class CharacterDatas : IDisposable
    {
        public ReactiveCollection<CharacterData> Datas { get; set; } = new ReactiveCollection<CharacterData>();

        public void Dispose()
        {
            Datas?.Dispose();
        }
    }

    public class DataManager : /*MonoBehaviour*/ IDisposable
    {
        public CharacterDatas Character { get; set; } = new CharacterDatas();
        public ItemDatas Item { get; set; } = new ItemDatas();

        public void Dispose()
        {
            Character?.Dispose();
            Item?.Dispose();
        }
    }

    public class ItemDatas : IDisposable
    {
        public ItemDatas()
        {
        }

        public void Dispose()
        {
            
        }
    }
}
