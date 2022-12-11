using System;
using UniRx;

namespace AA
{
	public class CharacterObservable
	{
		private Subject<Character> _onDieSubject;

		public IObserver<Character> OnDieAsObserver() => _onDieSubject ??= new Subject<Character>();
		public IObservable<Character> OnDieAsObservable() => _onDieSubject ??= new Subject<Character>();

		public void Dispose()
		{
			_onDieSubject?.Dispose();
		}
	}
}
