using System;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace AA
{
	public partial class UIDefaultPopup : UIPopup
	{
		[System.Serializable]
		public class PopupView
		{
			public Button OkButton;
			public Button CancelButton;
		}
	}
}