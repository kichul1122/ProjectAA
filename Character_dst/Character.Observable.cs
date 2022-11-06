using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace AA
{
	public partial class Character : MonoBehaviour
	{
		private Subject<Character> _onDieSubject;

		public IObservable<Character> OnDieAsObservable() => _onDieSubject ??= new Subject<Character>();

		private void Dispose()
		{
			_onDieSubject?.Dispose();
		}
	}
}
