using Cysharp.Threading.Tasks;
using MonsterLove.StateMachine;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.U2D;
using Random = UnityEngine.Random;

namespace AA.Test
{
    public struct Point3D
    {
        public Point3D(double x, double y, double z)
        {
            _x = x;
            _y = y;
            _z = z;
        }

        private double _x;
        public double X
        {
            readonly get => _x;
            set => _x = value;
        }

        private double _y;
        public double Y
        {
            readonly get => _y;
            set => _y = value;
        }

        private double _z;
        public double Z
        {
            readonly get => _z;
            set => _z = value;
        }

        public readonly double Distance => System.Math.Sqrt(X * X + Y * Y + Z * Z);

        public readonly override string ToString() => $"{X}, {Y}, {Z}";

        private static Point3D origin = new Point3D(0, 0, 0);

        // Dangerous! returning a mutable reference to internal storage
        public static ref readonly Point3D Origin => ref origin;
    }

    public class AATest : MonoBehaviour
    {
        public List<string> values = new List<string>();

        private Point3D point;

        public enum EState
        {
            Idle,
            Play,
            Red,
            Blue
        }

        private StateMachine<EState, StateDriverRunner> fsm;

        [Button]
        public void ChangeState(EState eState)
        {
            fsm.ChangeState(eState);
        }

        [Button]
        public void ChangeStateToRed()
        {
            fsm.ChangeState(EState.Red);
        }

        [Button]
        public void ChangeStateToBlue()
        {
            fsm.ChangeState(EState.Blue);
        }

        [Button]
        public void ChangeStateToRedOverrite()
        {
            fsm.ChangeState(EState.Red, StateTransition.Overwrite);
        }

        [Button]
        public void ChangeStateToBlueOverrite()
        {
            fsm.ChangeState(EState.Blue, StateTransition.Overwrite);
        }


        public GameObject _gameObject;

        private void Start()
        {
            fsm = new StateMachine<EState, StateDriverRunner>(this);

            //var originValue = Point3D.Origin;
            //ref readonly var originReference = ref Point3D.Origin;

            //values = new List<string>() { "Character", "Map", "Texture", "UI" };

            //foreach (var value in values)
            //{
            //	this.UpdateAsObservable().Subscribe(_ => Debug.Log($"{value}"));
            //}

            //매개 변수로 새롭게 생성되는 녀석들?

            //var po = Con(originReference, originValue);

            //ctsPool = new ObjectPool<CancellationTokenSource>(() => new CancellationTokenSource());
        }

        IEnumerator Idle_Enter() => UniTask.ToCoroutine(async () =>
        {
            var time = Time.realtimeSinceStartup;

            Time.timeScale = 0.5f;
            try
            {
                await UniTask.Delay(TimeSpan.FromSeconds(3), ignoreTimeScale: true);

                Debug.Log(_gameObject.name);

                var elapsed = Time.realtimeSinceStartup - time;

                Debug.Log($"Idle elapsed: {elapsed}");

                //Assert.AreEqual(3, (int)Math.Round(TimeSpan.FromSeconds(elapsed).TotalSeconds, MidpointRounding.ToEven));
            }
            finally
            {
                Time.timeScale = 1.0f;
            }
        });

        void Idle_Exit()
        {
            Debug.Log("Idle_Exit");
        }

        void Red_Enter()
        {
            Debug.Log("Red_Enter");
        }

        void Red_Exit()
        {
            Debug.Log("Red_Exit");
        }

        void Blue_Enter()
        {
            Debug.Log("Blue_Enter");
        }

        void Blue_Exit()
        {
            Debug.Log("Blue_Exit");
        }

        public Point3D Con(in Point3D A, in Point3D B)
        {
            return new Point3D(A.X + B.X, A.Y + B.Y, A.Z + B.Z);
        }

        private CompositeDisposable disposables = new CompositeDisposable();

        private ReactiveProperty<long> Level = new ReactiveProperty<long>();

        int subscribeIndex = 0;

        [Button]
        private void LevelSubscribe()
        {
            int index = ++subscribeIndex;

            disposables.Clear();
            Level/*.Where(_ => !disposables.IsDisposed)*/.Subscribe(_ =>
            {
                Debug.Log($"{index} {_}");
            }, () => Debug.Log("OnComplete")).AddTo(disposables);
        }

        [Button]
        private void LevelUp()
        {
            Level.Value++;
        }

        [Button]
        private void DoClear()
        {
            disposables.Clear();

        }

        [Button]
        private void DoDispose()
        {
            disposables.Dispose();
        }

        [Button]
        private void DoLevelRPDispose()
        {
            Level.Dispose();
        }

        public Subject<Unit> OnTestsubject = new Subject<Unit>();

        [Button]
        public void DoOnCompleted()
        {
            OnTestsubject.OnCompleted();
        }

        [Button]
        public void DoNext()
        {
            OnTestsubject.OnNext(Unit.Default);
        }


        [Button]
        public void DoSubSub()
        {
            OnTestsubject.Subscribe(_ => Debug.Log("DoSubSub")).AddTo(disposables);
        }

        CancellationTokenSource cts = null;

        //ObjectPool<CancellationTokenSource> ctsPool;

        [Button]
        public void DoSubSubAsync()
        {

            var action = UniTask.Action(async () =>
            {
                try
                {
                    cts?.Cancel();
                    cts?.Dispose();

                    cts = new CancellationTokenSource();

                    await UniTask.Delay(2000, cancellationToken: cts.Token);

                    Debug.Log($"DoSubSub");
                }
                catch (System.Exception e)
                {
                    Debug.LogWarning(e);
                }
                finally
                {

                }
            });

            OnTestsubject.Subscribe(_ => action()).AddTo(disposables);
        }

        [Button]
        public void DoNextAsync()
        {
            OnTestsubject.OnNext(Unit.Default);
        }

        /// <summary>
        /// https://neuecc.medium.com/patterns-practices-for-efficiently-handling-c-async-await-cancel-processing-and-timeouts-b419ce5f69a4
        /// </summary>
        //class Client : IDisposable
        //{
        //    readonly TimeSpan timeout;
        //    readonly ObjectPool<CancellationTokenSource> timeoutTokenSourcePool;
        //    readonly CancellationTokenSource clientLifetimeTokenSource;

        //    public TimeSpan Timeout { get; }

        //    public Client(TimeSpan timeout)
        //    {
        //        this.Timeout = timeout;
        //        this.timeoutTokenSourcePool = ObjectPool.Create<CancellationTokenSource>();
        //        this.clientLifetimeTokenSource = new CancellationTokenSource();
        //    }

        //    public async Task SendAsync(CancellationToken cancellationToken = default)
        //    {
        //        var timeoutTokenSource = timeoutTokenSourcePool.Get();

        //        CancellationTokenRegistration externalCancellation = default;
        //        if (cancellationToken.CanBeCanceled)
        //        {
        //            externalCancellation = cancellationToken.RegisterWithoutCaptureExecutionContext(static state =>
        //            {
        //                ((CancellationTokenSource)state!).Cancel();
        //            }, timeoutTokenSource);
        //        }

        //        var clientLifetimeCancellation = clientLifetimeTokenSource.Token.RegisterWithoutCaptureExecutionContext(static state =>
        //        {
        //            ((CancellationTokenSource)state!).Cancel();
        //        }, timeoutTokenSource);

        //        timeoutTokenSource.CancelAfter(Timeout);

        //        try
        //        {
        //            await SendCoreAsync(timeoutTokenSource.Token);
        //        }
        //        catch (OperationCanceledException ex) when (ex.CancellationToken == timeoutTokenSource.Token)
        //        {
        //            // exception handling

        //            if (cancellationToken.IsCancellationRequested)
        //            {
        //                throw new OperationCanceledException(ex.Message, ex, cancellationToken);
        //            }
        //            else if (clientLifetimeTokenSource.IsCancellationRequested)
        //            {
        //                throw new OperationCanceledException("Client is disposed.", ex, clientLifetimeTokenSource.Token);
        //            }
        //            else
        //            {
        //                throw new TimeoutException($"The request was canceled due to the configured Timeout of {Timeout.TotalSeconds} seconds elapsing.", ex);
        //            }
        //        }
        //        finally
        //        {
        //            externalCancellation.Dispose();
        //            clientLifetimeCancellation.Dispose();
        //            if (timeoutTokenSource.TryReset())
        //            {
        //                timeoutTokenSourcePool.Return(timeoutTokenSource);
        //            }
        //        }
        //    }

        //    async Task SendCoreAsync(CancellationToken cancellationToken)
        //    {
        //        // snip...
        //    }

        //    public void Dispose()
        //    {
        //        clientLifetimeTokenSource.Cancel();
        //        clientLifetimeTokenSource.Dispose();
        //    }
        //}
        [Button]
        void DoSeelctMany()
        {
            Observable.EveryUpdate().SelectMany(Observable.Timer(System.TimeSpan.FromMilliseconds(10))).Subscribe(_ =>
            {
                Debug.Log(_);
            });
        }

        [Button]
        void MultileChoose()
        {
            List<float> items = new List<float>() { 10f, 20f, 30f, 40f, 50f, 60f, 70f, 80f, 90f };

            int chooseCount = 5;

            List<float> chooseItems = new List<float>(chooseCount);

            for (int i = 0; i < chooseCount; ++i)
            {
                if (items.Count == 0) break;

                int chooseIndex = Choose(items.ToArray());

                chooseItems.Add(items[chooseIndex]);

                items.RemoveAt(chooseIndex);
            }

            Debug.Log(chooseItems.ToJson());
        }

        int Choose(float[] probs)
        {
            float total = 0;

            foreach (float elem in probs)
            {
                total += elem;
            }

            float randomPoint = Random.value * total;

            for (int i = 0; i < probs.Length; i++)
            {
                if (randomPoint < probs[i])
                {
                    return i;
                }
                else
                {
                    randomPoint -= probs[i];
                }
            }
            return probs.Length - 1;
        }

        public string label = "character";
        public string loadObjectKey = "";

        public bool isInitAddressable = false;

        [Button]
        public void DoAddressable()
        {
            DoTask().Forget();
        }

        private async UniTask DoTask()
        {
            await ResourceManager.Addressable(label, loadObjectKey);

            isInitAddressable = true;

            var loadAssetAsync = Addressables.LoadAssetAsync<GameObject>(loadObjectKey);
            var newGO = await loadAssetAsync;
            if (loadAssetAsync.Status == AsyncOperationStatus.Succeeded)
            {
                GameObject.Instantiate(newGO);
                Debug.Log("Success");
            }

            this.OnDestroyAsObservable().Subscribe(_ =>
            {
                Addressables.Release(loadAssetAsync);
            });
        }

        [Button]
        public void DoClearAssetBundles()
        {
            AssetBundle.UnloadAllAssetBundles(true);
            bool isClear = Caching.ClearCache();
        }

        void Awake()
        {
            SpriteAtlasManager.atlasRequested += OnRequestAtlas;
        }

        bool IsRequest = false;

        void OnRequestAtlas(string tag, System.Action<SpriteAtlas> callback)
        {
            if (!isInitAddressable)
            {
                Debug.Log("!isInitAddressable");
                return;
            }

            if (IsRequest)
            {
                Debug.Log("IsRequest");
                return;
            }

            Debug.Log("OnRequestAtlas");

            var address = $"Assets/_Resources/Sprite/KCAtlas.spriteatlasv2";
            var loader = Addressables.LoadAssetAsync<SpriteAtlas>(address);
            loader.WaitForCompletion();

            var spriteAtlas = loader.Result;
            callback?.Invoke(spriteAtlas);

            address = $"Assets/_Resources/Atlas/Icons.spriteatlasv2";
            loader = Addressables.LoadAssetAsync<SpriteAtlas>(address);
            loader.WaitForCompletion();

            spriteAtlas = loader.Result;
            callback?.Invoke(spriteAtlas);

            IsRequest = true;

        }
    }
}