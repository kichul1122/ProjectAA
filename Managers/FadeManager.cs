using Cysharp.Threading.Tasks;
using UnityEngine;

namespace AA
{
	public class FadeManager : ManagerMonobehaviour
	{
		public bool IsFading { get; set; }

		public async UniTask FadeInAsync()
		{
			IsFading = true;

			await UniTask.Yield();

			IsFading = false;

			Debug.Log("Finish FadeInAsync");
		}

		public async UniTask FadeOutAsync()
		{
			IsFading = true;

			await UniTask.Yield();

			IsFading = false;

			Debug.Log("Finish FadeOutAsync");
		}
	}
}