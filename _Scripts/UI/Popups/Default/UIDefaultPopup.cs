using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace AA
{
	public partial class UIDefaultPopup : UIPopup
	{
		public PopupView View;

		private void Awake()
		{
			View.OkButton.OnClickAsObservable().Subscribe(_ =>
			{
				Debug.Log("Ok");
				Hide();
			}).AddTo(this);

			View.CancelButton.OnClickAsObservable().Subscribe(_ =>
			{
				Debug.Log("Cancel");
				Hide();
			}).AddTo(this);
		}

		private void Hide()
		{
			gameObject.SetActive(false);
		}
	}
}