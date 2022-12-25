using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace AA
{
	public partial class ResourceManager : ManagerMonobehaviour
	{
		private Dictionary<string, LabelData> labelDatas = new Dictionary<string, LabelData>();
		public static List<string> labels;

		static ResourceManager()
		{
			labels = new List<string>(System.Enum.GetValues(typeof(ELabel)).Cast<ELabel>().Select(_ => _.ToString()));
		}

		private IEnumerable GetLabels()
		{
			return System.Enum.GetValues(typeof(ELabel));
		}

		public async UniTask<bool> InitializeAsync()
		{
			var handle = Addressables.InitializeAsync(autoReleaseHandle: true);

			try
			{
				await handle;
			}
			catch
			{
				Debug.LogError("InitializeAsync Failed");

				return false;
			}

			bool isResult = handle.Status == AsyncOperationStatus.Succeeded;

			return isResult;
		}

		public async UniTask UpdateLabelDatasAsync()
		{
			//if (Application.isEditor) return;

			var labels = GetLabels();

			labelDatas.Clear();

			//foreach (var label in labels)
			//{
			AsyncOperationHandle<long> downloadSizeAsync = Addressables.GetDownloadSizeAsync(labels);

			long downloadSize = await downloadSizeAsync;

			bool canDownload = downloadSizeAsync.Status == AsyncOperationStatus.Succeeded && downloadSize > 0;

			//labelDatas.Add(label.ToString(), new LabelData(canDownload, downloadSize));
			//}
		}

		public bool CanDownload => labelDatas.Any(_ => _.Value.CanDownload);

		public bool IsDownloadCompleted()
		{
			//if (Application.isEditor) return true;

			return labelDatas.All(_ => _.Value.IsComplted());
		}

		public long TotalDownloadSize()
		{
			return labelDatas.Sum(_ => _.Value.DownloadSize);
		}

		public float TotalProgress()
		{
			float totalDownloadedSize = labelDatas.Sum(_ => _.Value.DownloadedSize());

			return totalDownloadedSize / TotalDownloadSize();
		}

		public async UniTask DownloadAsync()
		{
			//if (Application.isEditor) return;

			var labels = GetLabels();

			List<UniTask> tasks = new List<UniTask>();
			foreach (var label in labels)
			{
				var progress = Progress.Create<float>(progress =>
				{
					labelDatas[labels.ToString()].Progress = progress;

					//Debug.Log($"다운 로드 중({percent}) : {(int)(totalDownloadSize * percent)} / {totalDownloadSize}");
					Debug.Log($"다운 로드 중({labels}: {progress})");
				});

				tasks.Add(Addressables.DownloadDependenciesAsync(label).ToUniTask(progress: progress));
			}

			await UniTask.WhenAll(tasks);
		}

		//public const string RootPath = "Assets/_Resources/";

		public new T Instantiate<T>(T prefab) where T : UnityEngine.Object
		{
			return Object.Instantiate(prefab);
		}

		public T LoadAsset<T>(string path, Component owner) where T : UnityEngine.Object
		{
			var handle = Addressables.LoadAssetAsync<T>(path);

			var asset = handle.WaitForCompletion();

			if (asset == null)
			{
				Debug.LogError($"LoadAsset<T> : {path} is null");
				return null;
			}

			owner.OnDestroyAsObservable().Subscribe(_ => Release(handle));

			return asset;
		}

		public async UniTask<T> LoadAssetAsync<T>(string path, Component owner) where T : UnityEngine.Object
		{
			var handle = Addressables.LoadAssetAsync<T>(path);

			var asset = await handle;

			if (asset == null)
			{
				Debug.LogError($"LoadAssetAsync<T> : {path} is null");
				return null;
			}

			owner.OnDestroyAsObservable().Subscribe(_ => Release(handle));

			return asset;
		}

		private void Release<T>(AsyncOperationHandle<T> handle)
		{
			//if (Application.isEditor) return; //Addressables.Release was called on an object that Addressables was not previously aware of.  Thus nothing is being released

			Addressables.Release(handle);
		}

		//public new void Destroy(UnityEngine.Object obj)
		//{
		//	AAHelper.Destroy(obj);
		//}

		public static async UniTask Addressable(string label, string loadObjectKey)
		{
			//Addressable Initialize
			await Addressables.InitializeAsync(true);

			//캐싱된 모든 에셋번들 강제 삭제
			//True when cache clearing succeeded, false if cache was in use.
			//AssetBundle.UnloadAllAssetBundles(true);
			//bool isClear = Caching.ClearCache();

			//다운로드 사이즈 및 다운로드
			string key = label; //LabelName or AddressablePath

			AsyncOperationHandle<long> downloadSizeAsync = Addressables.GetDownloadSizeAsync(labels);
			long downloadSize = await downloadSizeAsync;
			Debug.Log($"DownloadSize: {downloadSize}");

			bool canDownload = downloadSizeAsync.Status == AsyncOperationStatus.Succeeded && downloadSize > 0;

			Addressables.Release(downloadSizeAsync);
			var downloadtasks = new List<UniTask>();
			if (canDownload)
			{
				foreach (var label_ in labels)
				{
					var downloadDependenciesAsync = Addressables.DownloadDependenciesAsync(label_, true);

					downloadtasks.Add(UniTask.Create(async () =>
					{
						var progress = Progress.Create<float>(progress =>
						{
							//labelDatas[labels.ToString()].Progress = progress;

							//Debug.Log($"다운 로드 중({percent}) : {(int)(totalDownloadSize * percent)} / {totalDownloadSize}");
							Debug.Log($"다운 로드 중({label_}: {progress})");

						});

						await downloadDependenciesAsync.ToUniTask(progress: progress);

						if (downloadDependenciesAsync.Status != AsyncOperationStatus.Succeeded)
						{
							//실패
							Debug.Log($"Download Fail: {label_}");
						}
						else
						{
							Debug.Log($"Download Success: {label_}");
						}
					}));
				}

			}
			else
			{
				//다운받을게 없어
				Debug.Log("Not need download");
			}

			await UniTask.WhenAll(downloadtasks);

			//DownLoad + Load Asset
			//var loadAssetAsync = Addressables.LoadAssetAsync<GameObject>(loadObjectKey);
			//var newGO = await loadAssetAsync;
			//if (loadAssetAsync.Status == AsyncOperationStatus.Succeeded)
			//{
			//	bool isSucceded = newGO;

			//	GameObject.Instantiate(newGO);
			//	Debug.Log("Success");
			//}

			//Sync
			//var loadAssetSync = Addressables.LoadAssetAsync<GameObject>(key);
			//var newGO2 = loadAssetSync.WaitForCompletion();

			//Clear
			//AssetBundle.UnloadAllAssetBundles(true);
			//Addressables.ClearDependencyCacheAsync(key);
		}
	}

	public partial class ResourceManager : ManagerMonobehaviour
	{
		[System.Serializable]
		public class LabelData
		{
			public bool CanDownload { get; set; }
			public long DownloadSize { get; set; }
			public float Progress { get; set; }

			public LabelData(bool canDownload, long downloadSize)
			{
				CanDownload = canDownload;
				DownloadSize = downloadSize;
				Progress = 0f;
			}

			public bool IsComplted()
			{
				if (DownloadSize > 0L)
				{
					return Progress >= 1f;
				}

				return true;
			}

			public float DownloadedSize()
			{
				return DownloadSize * Progress;
			}
		}
	}
}