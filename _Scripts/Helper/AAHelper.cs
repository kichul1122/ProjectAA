using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AA
{
	public abstract class AAFactory<T>
	{
		public static T Create() => default(T);
	}

	public static class AAHelper
	{
		public static void Destroy(UnityEngine.Object obj)
		{
			if (!obj) return;

			if (obj is Transform)
			{
				var t = (obj as Transform);
				var go = t.gameObject;

				go.SetActive(false);
				t.SetParent(null);

				if (Application.isPlaying)
				{
					UnityEngine.Object.Destroy(go);
				}
				else UnityEngine.Object.DestroyImmediate(go);
			}
			else if (obj is GameObject)
			{
				var go = obj as GameObject;
				var t = go.transform;

				go.SetActive(false);
				t.SetParent(null);

				if (Application.isPlaying)
				{
					UnityEngine.Object.Destroy(go);
				}
				else UnityEngine.Object.DestroyImmediate(go);
			}
			else if (Application.isPlaying) UnityEngine.Object.Destroy(obj);
			else UnityEngine.Object.DestroyImmediate(obj);
		}
	}
}
