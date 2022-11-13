using MessagePipe;
using UniRx;
using UniRx.Triggers;

namespace AA
{
    public class InputManager : ManagerMonobehaviour
    {
        public readonly struct HorizontalMsg
        {
            public float Value { get; }

            public HorizontalMsg(float value)
            {
                Value = value;
            }
        }

        public readonly struct VerticalMsg
        {
            public float Value { get; }

            public VerticalMsg(float value)
            {
                Value = value;
            }
        }

        private IPublisher<HorizontalMsg> _horizontalPublisher;
        private IPublisher<VerticalMsg> _verticalPublisher;

        private void Start()
        {
            _horizontalPublisher = Managers.MessagePipe.GetPublisher<HorizontalMsg>();
            _verticalPublisher = Managers.MessagePipe.GetPublisher<VerticalMsg>();

            this.UpdateAsObservable()
                .Select(_ => UnityEngine.Input.GetAxisRaw(AAString.Horizontal))
                .Subscribe(value => _horizontalPublisher.Publish(new HorizontalMsg(value))).AddTo(this);

            this.UpdateAsObservable()
                .Select(_ => UnityEngine.Input.GetAxisRaw(AAString.Vertical))
                .Subscribe(value => _verticalPublisher.Publish(new VerticalMsg(value))).AddTo(this);

        }
    }
}