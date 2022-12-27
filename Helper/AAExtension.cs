using Sirenix.Serialization;
using System;
using System.Text;
using UniRx;
using Unity.Mathematics;
using UnityEngine;

namespace AA
{
	public static class Extension
	{
		public static T Find<T>(this ReactiveCollection<T> collection, Predicate<T> predicate)
		{
			foreach (var element in collection)
			{
				if (predicate(element))
				{
					return element;
				}
			}

			return default(T);
		}

		public static void ForEach<T>(this ReactiveCollection<T> collection, Action<T> action)
		{
			if (collection == null) return;

			foreach (var element in collection)
			{
				action?.Invoke(element);
			}
		}

		public static IDisposable SubscribeToText(this IObservable<string> source, TMPro.TextMeshProUGUI text)
		{
			return source.SubscribeWithState(text, (x, t) => t.text = x);
		}

		public static IDisposable SubscribeToText<T>(this IObservable<T> source, TMPro.TextMeshProUGUI text)
		{
			return source.SubscribeWithState(text, (x, t) => t.text = x.ToString());
		}

		public static string ToJson(this object @object)
		{
			return Encoding.Default.GetString(SerializationUtility.SerializeValue(@object, DataFormat.JSON));
			//return JsonConvert.SerializeObject(@object);
		}

		public static T GetOrAddComponent<T>(this MonoBehaviour target) where T : MonoBehaviour
		{
			return target.gameObject.GetOrAddComponent<T>();
		}

		public static T GetOrAddComponent<T>(this GameObject target) where T : MonoBehaviour
		{
			T component = target.GetComponent<T>();

			if (component == null)
			{
				component = target.AddComponent<T>();
			}

			return component;
		}
	}

	public static class TransformExtension
	{
		public static void ResetLocal(this Transform t)
		{
			t.localPosition = Vector3.zero;
			t.localRotation = Quaternion.identity;
			t.localScale = Vector3.one;
		}

		public static void ResetWorld(this Transform t)
		{
			t.position = Vector3.zero;
			t.rotation = Quaternion.identity;
			t.localScale = Vector3.one;
		}
	}

	public static class MathExtension
	{
		public static (float3 start, float3 end, float radius) GetCapsule(this CapsuleCollider capsuleCollider)
		{
			var direction = new Vector3 { [capsuleCollider.direction] = 1 };
			var offset = capsuleCollider.height / 2 - capsuleCollider.radius;
			var start = capsuleCollider.center - direction * offset;
			var end = capsuleCollider.center + direction * offset;

			return (start, end, capsuleCollider.radius);
		}
	}
}