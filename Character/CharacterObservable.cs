using System;
using UniRx;

namespace AA
{
	public class CharacterObservable
	{
		private Subject<Character> _onDieSubject;

		public IObserver<Character> OnDieObserver() => _onDieSubject ??= new Subject<Character>();
		public IObservable<Character> OnDieObservable() => _onDieSubject ??= new Subject<Character>();

		private Subject<Character> _onRemoveSubject;

		public IObserver<Character> OnRemoveObserver() => _onRemoveSubject ??= new Subject<Character>();
		public IObservable<Character> OnRemoveObservable() => (_onRemoveSubject ??= new Subject<Character>()).Take(1);

		public void Dispose()
		{
			_onDieSubject?.Dispose();

			_onRemoveSubject?.Dispose();
		}
	}
}
