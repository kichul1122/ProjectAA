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
        // ?Ù¥?âÍé©ñ¼ìýîÜ?ßÚ
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
            // ?Ëï?ÖÇé»Ðï
            var container = new QFrameworkContainer();

            // ñ¼??úþ
            container.Register<ServiceA>();

            container.Register<IService, ServiceB>();

            container.RegisterInstance(new ServiceC());

            container.Register<ServiceC>("New");

            // ñ¼ìý?ßÚ£¨?í»??? Inject AtrributetîÜ?ßÚ)
            container.Inject(this);

            // ñ¼ìýñýý¨£¬ö¦Ê¦ì¤òÁïÈÞÅéÄ A ?ßÚÖõ
            A.Say();
            B.Say();
            C.Say();
            C2.Say();
            newC.Say();
        }
    }
}