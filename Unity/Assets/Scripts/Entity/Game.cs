using System;
using System.Collections.Generic;

namespace ETModel
{
	public static class Game
	{
        private static Dictionary<Type, Component> _singletonDic = new Dictionary<Type, Component>();
		private static Scene scene;

		public static Scene Scene
		{
			get
			{
				if (scene != null)
				{
					return scene;
				}
				scene = new Scene();
				scene.AddComponent<TimerComponent>();
				return scene;
			}
		}

		private static EventSystem eventSystem;

		public static EventSystem EventSystem
		{
			get
			{
				return eventSystem ?? (eventSystem = new EventSystem());
			}
		}

		private static ObjectPool objectPool;

		public static ObjectPool ObjectPool
		{
			get
			{
				return objectPool ?? (objectPool = new ObjectPool());
			}
		}

		private static Hotfix hotfix;

		public static Hotfix Hotfix
		{
			get
			{
				return hotfix ?? (hotfix = new Hotfix());
			}
		}

        public static T GetSingletonComponent<T>() where T : Component
        {
            Type t = typeof(T);
            if (!_singletonDic.ContainsKey(t))
            {
                T component = ObjectPool.Fetch<T>();
                EventSystem.Awake(component);
                _singletonDic.Add(t, component);
            }
            return (T)_singletonDic[t];
        }

		public static void Close()
		{
			scene.Dispose();
			eventSystem = null;
			scene = null;
			objectPool = null;
			hotfix = null;
		}
	}
}