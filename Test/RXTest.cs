using Sirenix.OdinInspector;
using UniRx;
using UnityEngine;

namespace AA
{
	public class RXTest : MonoBehaviour
	{
		ReactiveProperty<bool> m_Enable = new ReactiveProperty<bool>(false);
		Subject<bool> Subject = new Subject<bool>();

		[Button]
		public void Cons()
		{
			m_Enable.Take(1).Subscribe(_ => Debug.Log($"m_Enable:{_}")).AddTo(this);

			Subject.Take(1).Subscribe(_ => Debug.Log($"Subject: {_}")).AddTo(this);
		}

		[Button]
		public void OnNext()
		{
			m_Enable.Value = !m_Enable.Value;
			Subject.OnNext(!m_Enable.Value);
		}













		//ReactiveProperty<double> Attack = new();
		//ReactiveProperty<double> MaxHp = new();

		//[Button]
		//public void Construct()
		//{
		//	Func<double> onCalculate = () => Attack.Value + MaxHp.Value;

		//	Attack.Subscribe(_ => Debug.Log($"Attack: {Attack}")).AddTo(this);
		//	MaxHp.Subscribe(_ => Debug.Log($"MaxHp: {MaxHp}")).AddTo(this);
		//	Observable.Merge(Attack, MaxHp).Select(_ => onCalculate()).ToReadOnlyReactiveProperty(onCalculate()).Subscribe(value => Debug.Log($"Sum: {value}")).AddTo(this);
		//}

		//[Button]
		//public void UpdateAttack()
		//{
		//	Attack.Value += 1;
		//}

		//[Button]
		//public void UpdateMaxHp()
		//{
		//	MaxHp.Value += 4;
		//}












		//CompositeDisposable _disposables;


		//[Button]
		//public void Allocate()
		//{
		//    _disposables = new CompositeDisposable();

		//}

		//private int constructorCount;

		//[Button]
		//public void Constructor()
		//{
		//    _disposables?.Clear();

		//    ++constructorCount;


		//    Observable.Interval(System.TimeSpan.FromSeconds(1d)).Subscribe(_ => Debug.Log($"{constructorCount}")).AddTo(_disposables);
		//}

		//[Button]
		//public void OnDestroy()
		//{
		//    _disposables?.Dispose();
		//    _disposables = null;

		//}

	}
}
