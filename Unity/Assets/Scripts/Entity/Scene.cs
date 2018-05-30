using System;
using System.Collections.Generic;

namespace ETModel
{
	public enum SceneType
	{
		Share,
		Game,
		Login,
		Lobby,
		Map,
		Launcher,
		Robot,
		BehaviorTreeScene,
		RobotClient,

		Realm
	}
	
	public sealed class Scene: Entity
	{
		public Scene Parent { get; set; }

		public string Name { get; set; }

        private static Dictionary<Type, Component> _singletonDic = new Dictionary<Type, Component>();


        public T GetSingletonComponent<T>() where T : Component
        {
            Type t = typeof(T);
            if (!_singletonDic.ContainsKey(t))
            {
                T component = Game.ObjectPool.Fetch<T>();
                Game.EventSystem.Awake(component);
                _singletonDic.Add(t, component);
            }
            return (T)_singletonDic[t];
        }

        public Scene()
		{
		}

		public Scene(long id): base(id)
		{
		}

		public override void Dispose()
		{
			if (this.IsDisposed)
			{
				return;
			}
            foreach (Component component in _singletonDic.Values)
            {
                component.Dispose();
            }
            _singletonDic.Clear();
            base.Dispose();
		}
	}
}