using Cysharp.Threading.Tasks;
using System.Collections;
using UnityEngine;

namespace AA
{
	public partial class StartUpScene : MonoBehaviour
	{
		IEnumerator LoadStartUpData_Enter() => UniTask.ToCoroutine(async () =>
		{
			await UniTask.Yield();

			_stateMachine.ChangeState(EStartUpSceneState.LoadPopup);
		});
	}
}
