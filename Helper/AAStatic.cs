﻿using AA.Resolvers;
using MessagePack;
using MessagePack.Resolvers;
using MessagePack.Unity;
using MessagePack.Unity.Extension;
using UnityEngine;

namespace AA
{
    public static class Initializer
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        public static void SetupManagers()
        {
            Managers.DoReset();

            Debug.Log(nameof(SetupManagers));
        }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        public static void SetupMessagePackResolver()
        {
            StaticCompositeResolver.Instance = new StaticCompositeResolver();

            StaticCompositeResolver.Instance.Register(new[]
            {
                MasterMemoryResolver.Instance, // set MasterMemory generated resolver

                GeneratedResolver.Instance,    // set MessagePack generated resolver

                BuiltinResolver.Instance,
                UnityResolver.Instance,
                UnityBlitWithPrimitiveArrayResolver.Instance,

                StandardResolver.Instance      // set default MessagePack resolver
            });

            var options = MessagePackSerializerOptions.Standard.WithResolver(StaticCompositeResolver.Instance);
            MessagePackSerializer.DefaultOptions = options;

            Debug.Log(nameof(SetupMessagePackResolver));
        }
    }
}
