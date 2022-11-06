using Cysharp.Threading.Tasks;
using MessagePipe;
using System;
using System.Threading;
using UniRx;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace DI
{
	public class GameLifetimeScope : LifetimeScope
	{
		protected override void Configure(IContainerBuilder builder)
		{
			Debug.Log("GameLifetimeScope");

			builder.RegisterEntryPoint<ActorPresenter>();

			builder.Register<CharacterService>(Lifetime.Scoped);
			builder.Register<IRouteSearch, AStartRouteSearch>(Lifetime.Scoped);

			builder.RegisterComponentInHierarchy<ActorView>();

			var options = builder.RegisterMessagePipe();

			builder.RegisterBuildCallback(resolver => GlobalMessagePipe.SetProvider(resolver.AsServiceProvider()));

			builder.RegisterMessageBroker<CharacterMessage>(options);

			//builder.RegisterMessageHandlerFilter<MyFilter<int>>();

			builder.RegisterEntryPoint<MessagePipeDemo>(Lifetime.Singleton);

		}
	}

	public struct CharacterMessage
	{
		public string Name { get; set; }
	}

	public class MessagePipeDemo : VContainer.Unity.IStartable
	{
		readonly IPublisher<CharacterMessage> publisher;
		readonly ISubscriber<CharacterMessage> subscriber;

		public MessagePipeDemo(IPublisher<CharacterMessage> publisher, ISubscriber<CharacterMessage> subscriber)
		{
			this.publisher = publisher;
			this.subscriber = subscriber;
		}

		public void Start()
		{
			var d = DisposableBag.CreateBuilder();
			subscriber.Subscribe(x => Debug.Log("S1:" + x.Name)).AddTo(d);
			subscriber.Subscribe(x => Debug.Log("S2:" + x.Name)).AddTo(d);

			publisher.Publish(new CharacterMessage() { Name = "kichul" });
			publisher.Publish(new CharacterMessage() { Name = "jinsung" });
			publisher.Publish(new CharacterMessage() { Name = "minsoo" });

			var disposable = d.Build();
			disposable.Dispose();
		}
	}

	public class SceneLoader
	{
		readonly LifetimeScope parent;

		public SceneLoader(LifetimeScope lifetimeScope)
		{
			this.parent = lifetimeScope;
		}

		async UniTask LoadSceneAsync()
		{
			using (LifetimeScope.EnqueueParent(parent))
			{
				await UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("", UnityEngine.SceneManagement.LoadSceneMode.Additive);
			}

			using (LifetimeScope.Enqueue(builder =>
			{
				//builder.RegisterInstance(extraInstance);
			}))
			{

			}
		}
	}



	public interface IRouteSearch
	{
		void Log();
	}

	public class AStartRouteSearch : IRouteSearch
	{
		public void Log()
		{
			Debug.Log(nameof(AStartRouteSearch));
		}
	}

	public class CharacterService : IDisposable, ITickable
	{
		private readonly IRouteSearch routeSearch;

		public CharacterService(IRouteSearch routeSearch)
		{
			this.routeSearch = routeSearch;
		}

		public Subject<Unit> onService = new Subject<Unit>();

		public void Log()
		{
			routeSearch.Log();

		}

		void IDisposable.Dispose()
		{
			Debug.Log("CharacterService Dispose");
		}

		void ITickable.Tick()
		{
			Debug.Log("characterservice33");
		}
	}

	public class ActorPresenter : IAsyncStartable, ITickable
	{
		readonly CharacterService service;
		readonly ActorView actorView;

		public ActorPresenter(CharacterService service, ActorView actorView)
		{
			this.service = service;
			this.actorView = actorView;
		}

		public async UniTask StartAsync(CancellationToken cancellation)
		{
			await UniTask.Yield();

			service.Log();
		}

		void ITickable.Tick()
		{
			//service.Log();
			//actorView.Log();
		}
	}
}
