using ObservableCollections;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace AA
{
	/// <summary>
	/// Infinity ScrollView일때 생각해보기
	/// </summary>
	public class ObservableCollectionsSampleScript : MonoBehaviour
	{
		public Button prefab;
		public GameObject root;
		ObservableFixedSizeRingBuffer<int> collection;
		ISynchronizedView<int, GameObject> view;

		void Start()
		{
			collection = new ObservableFixedSizeRingBuffer<int>(10);

			view = collection.CreateView(x =>
			{
				var item = GameObject.Instantiate(prefab);
				item.GetComponentInChildren<TextMeshProUGUI>().text = x.ToString();
				return item.gameObject;
			});

			view.AttachFilter(new GameObjectFilter(root));
		}

		int createCount = 0;

		private void Update()
		{
			if (UnityEngine.Input.GetKeyDown(KeyCode.A))
			{
				collection.AddFirst(++createCount);

			}
			if (UnityEngine.Input.GetKeyDown(KeyCode.S))
			{
				collection.AddLast(++createCount);
			}

			if (UnityEngine.Input.GetKeyDown(KeyCode.Q))
			{
				collection.RemoveFirst();
			}

			if (UnityEngine.Input.GetKeyDown(KeyCode.W))
			{
				collection.RemoveLast();
			}
		}

		void OnDestroy()
		{
			view.Dispose();
		}

		public class GameObjectFilter : ISynchronizedViewFilter<int, GameObject>
		{
			readonly GameObject root;

			public GameObjectFilter(GameObject root)
			{
				this.root = root;
			}

			public void OnCollectionChanged(ChangedKind changedKind, int value, GameObject view, in NotifyCollectionChangedEventArgs<int> eventArgs)
			{
				if (changedKind == ChangedKind.Add)
				{
					view.transform.SetParent(root.transform);

                    if (eventArgs.NewStartingIndex == 0)
                    {
                        view.transform.SetAsFirstSibling();
                    }
                    else
                    {
						view.transform.SetAsLastSibling();
                    }
				}
				else if (changedKind == ChangedKind.Remove)
				{
					GameObject.Destroy(view);
				}
			}

			public bool IsMatch(int value, GameObject view)
			{
				return true;
			}

			public void WhenTrue(int value, GameObject view)
			{
				view.SetActive(true);
			}

			public void WhenFalse(int value, GameObject view)
			{
				view.SetActive(false);
			}
		}
	}
}
