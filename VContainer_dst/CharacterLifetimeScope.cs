using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace DI
{
    public class CharacterLifetimeScope : LifetimeScope
    {
        public string Name;

        protected override void Configure(IContainerBuilder builder)
        {
            Debug.Log("CharacterLifetimeScope");

            builder.Register<CharacterView>(Lifetime.Scoped);

   //         if(Name.Equals("1"))
			//{
   //             builder.Register<Character>(Lifetime.Transient);
			//}

            builder.RegisterEntryPoint<Character>();
        }
    }

    public class Character : IStartable, ITickable
	{
        private CharacterView view;

        public Character(CharacterView view)
		{
            this.view = view;
		}

		public void Start()
		{
            
		}

		void ITickable.Tick()
		{
			Debug.Log("Character TIck");
		}
	}

    public class CharacterView : ITickable
	{
        public string Name { get; set; }

        public CharacterView()
		{
			Name = "1111";
		}

		void ITickable.Tick()
		{
			Debug.Log($"{Name}");
		}
	}
}
