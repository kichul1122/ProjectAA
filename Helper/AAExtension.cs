using ObservableCollections;
using System;
using System.Collections.Specialized;
using UniRx;
using UnityEngine;

namespace AA
{
	public static class Extension
	{
		public static bool RemoveAndDispose<T>(this ReactiveCollection<T> collection, Predicate<T> predicate)
		{
			for (int i = collection.Count - 1; i >= 0; i--)
			{
				if (predicate(collection[i]))
				{
					(collection[i] as IDisposable)?.Dispose();

					collection.RemoveAt(i);
					return true;
				}
			}

			return false;
		}
        
		public static bool Remove<T>(this ReactiveCollection<T> collection, Predicate<T> predicate)
		{
            for (int i = collection.Count - 1; i >= 0; i--)
            {
                if (predicate(collection[i]))
                {
					collection.RemoveAt(i);
                    return true;
                }
            }
			
			return false;
		}
        
		public static T Find<T>(this ReactiveCollection<T> collection, Predicate<T> predicate)
        {
			foreach (var element in collection)
			{
				if(predicate(element))
                {
					return element;
				}
			}

			return default;
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
        
		public static IObservable<NotifyCollectionChangedAction> OnChangedAsObservable<T, TView>(this ISynchronizedView<T, TView> view)
        {
			return view.AsObservable().StartWith(NotifyCollectionChangedAction.Reset);
		}

		public static IObservable<NotifyCollectionChangedAction> AsObservable<T, TView>(this ISynchronizedView<T, TView> view)
		{
			return Observable.FromEvent<Action<NotifyCollectionChangedAction>, NotifyCollectionChangedAction>(h => h, h => view.CollectionStateChanged += h, h => view.CollectionStateChanged -= h);
		}

		public static ISynchronizedView<T, Unit> CreateView<T>(this ObservableList<T> observableList, Component component)
        {
			return observableList.CreateView(_ => Unit.Default).AddTo(component);
		}

		public static string ToJson(this object @object)
		{
			return Newtonsoft.Json.JsonConvert.SerializeObject(@object);
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
}