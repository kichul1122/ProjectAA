using System;
using UniRx;

namespace AA
{
	public class CharacterObservable
	{
		private Subject<Character> _onDieSubject;

		public IObserver<Character> OnDieAsObserver() => _onDieSubject ??= new Subject<Character>();
		public IObservable<Character> OnDieAsObservable() => _onDieSubject ??= new Subject<Character>();

		private Subject<Character> _onRemoveSubject;

		public IObserver<Character> OnRemoveAsObserver() => _onRemoveSubject ??= new Subject<Character>();
		public IObservable<Character> OnRemoveAsObservable() => _onRemoveSubject ??= new Subject<Character>();

		public void Dispose()
		{
			_onDieSubject?.Dispose();

			_onRemoveSubject?.Dispose();
		}
	}
}
