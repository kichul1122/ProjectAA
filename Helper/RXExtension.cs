using System;
using UniRx;

namespace AA
{
	public static class RXExtension
	{
		public static IObservable<T> TakeOnSubscribe<T>(this IReadOnlyReactiveProperty<T> source)
		{
			return source.HasValue ? source.Take(1) : source;
		}
	}
}
