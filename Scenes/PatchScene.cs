using UnityEngine;

namespace AA
{
    public class PatchScene : MonoBehaviour/*, IScene*/
    {
        //public ESceneName Name => ESceneName.Patch;

        //public async UniTask StartAsync()
        //{
        //    Debug.Log($"App Version...");
        //    await UniTask.Yield();

        //    Debug.Log($"Bundle Version...");
        //    await UniTask.Yield();

        //    bool isInitialize = await Managers.Resource.InitializeAsync();
        //    if (!isInitialize)
        //    {
        //        //Error Popup
        //        Debug.LogError($"Resource.InitializeAsync is Fail!");
        //        return;
        //    }

        //    //var locations = await Addressables.LoadResourceLocationsAsync(labels, Addressables.MergeMode.Intersection).Task;
        //    //AsyncOperationHandle<long> opHandle = Addressables.GetDownloadSizeAsync(locations);

        //    //await Managers.Resource.UpdateLabelDatasAsync();

        //    //if (Managers.Resource.CanDownload)
        //    //{
        //    //downloadResult.downloadSize

        //    //Ok Cancel Popup

        //    //Managers.Resource.DownloadAsync().Forget();
        //    //}

        //    //await UniTask.WaitUntil(() => Managers.Resource.IsDownloadCompleted());

        //    await LoadMetaDataAsync();

        //    await Managers.Fade.FadeOutAsync();
        //    Managers.Scene.ChangeAsync(ESceneName.Login).Forget();
        //}

        //public async UniTask DisposeAsync()
        //{
        //    await UniTask.Yield();
        //}

        //public async UniTask LoadMetaDataAsync()
        //{
        //    Debug.Log($"Load MasterData...");

        //    Managers.Meta.Load();

        //    await UniTask.Yield();
        //}
    }
}