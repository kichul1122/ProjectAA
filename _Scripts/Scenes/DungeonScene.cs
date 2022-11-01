using Cysharp.Threading.Tasks;
using UnityEngine;

namespace AA
{
	public class DungeonScene : MonoBehaviour, IScene
	{
		public ESceneName Name => ESceneName.Dungeon;

		public async UniTask StartAsync()
		{
			await UniTask.Yield();
		}

		public async UniTask DisposeAsync()
		{
			await UniTask.Yield();
		}
	}
}
