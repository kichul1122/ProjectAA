using MessagePipe;

namespace AA
{
    /// <summary>
    /// https://github.com/Cysharp/MessagePipe#unity
    /// </summary>
    public class MessagePipeManager : ManagerMonobehaviour
    {
        private void Start()
        {
            var builder = new BuiltinContainerBuilder();

            builder.AddMessagePipe(/* configure option */);

            // AddMessageBroker: Register for IPublisher<T>/ISubscriber<T>, includes async and buffered.
            //builder.AddMessageBroker<int>();

            builder.AddMessageBroker<CharacterFollow.TargetTransformEvent>();

            // also exists AddMessageBroker<TKey, TMessage>, AddRequestHandler, AddAsyncRequestHandler

            // AddMessageHandlerFilter: Register for filter, also exists RegisterAsyncMessageHandlerFilter, Register(Async)RequestHandlerFilter
            //builder.AddMessageHandlerFilter<MyFilter<int>>();

            // create provider and set to Global(to enable diagnostics window and global fucntion)
            var provider = builder.BuildServiceProvider();
            GlobalMessagePipe.SetProvider(provider);

            // --- to use MessagePipe, you can use from GlobalMessagePipe.
            // var p = GetPublisher<int>();
            // var s = GetSubscriber<int>();

            // var d = DisposableBag.CreateBuilder();

            //s.Subscribe(x => Debug.Log($"First: {x}")).AddTo(d);
            //s.Subscribe(x => Debug.Log($"Second: {x}")).AddTo(d);

            //p.Publish(10);
            //p.Publish(20);
            //p.Publish(30);
            //
            //var disposable = d.Build();
            //disposable.Dispose();
        }

        public IPublisher<TMessage> GetPublisher<TMessage>() => GlobalMessagePipe.GetPublisher<TMessage>();


        public ISubscriber<TMessage> GetSubscriber<TMessage>() => GlobalMessagePipe.GetSubscriber<TMessage>();
    }
}
