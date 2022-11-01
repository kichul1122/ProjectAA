using UnityEngine;

namespace QFramework.Example
{

    public class ServiceA
    {
        public void Say()
        {
            Debug.Log("I am ServiceA:" + this.GetHashCode());
        }
    }

    public interface IService
    {
        void Say();
    }

    public class ServiceB : IService
    {
        public void Say()
        {
            Debug.Log("I am ServiceB:" + this.GetHashCode());
        }
    }

    public class ServiceC : IService
    {
        public void Say()
        {
            Debug.Log("I am ServiceC:" + this.GetHashCode());
        }
    }

    public class IOCExample : MonoBehaviour
    {
        // ?٥?��������?��
        [Inject]
        public ServiceA A { get; set; }

        [Inject]
        public IService B { get; set; }

        [Inject]
        public ServiceC C { get; set; }

        [Inject]
        public ServiceC C2 { get; set; }

        [Inject("New")]
        public ServiceC newC { get; set; }

        void Start()
        {
            // ?��?�����
            var container = new QFrameworkContainer();

            // �??��
            container.Register<ServiceA>();

            container.Register<IService, ServiceB>();

            container.RegisterInstance(new ServiceC());

            container.Register<ServiceC>("New");

            // ���?�ڣ�?�??? Inject Atrributet��?��)
            container.Inject(this);

            // �����������ʦ��������� A ?����
            A.Say();
            B.Say();
            C.Say();
            C2.Say();
            newC.Say();
        }
    }
}