using System.Collections;
using System.Collections.Generic;

namespace ETModel
{
    [ObjectSystem]
    public class GameEventComponentDestroySystem : DestroySystem<GameEventComponent>
    {
        public override void Destroy(GameEventComponent self)
        {
            self.Destroy();
        }
    }

    public class GameEventComponent : Component
{

        public delegate void EventHandler<T>(T e) where T : GameEvent;
        private delegate void EventHandler(GameEvent e);

        private Dictionary<System.Type, EventHandler> delegates = new Dictionary<System.Type, EventHandler>();
        private Dictionary<System.Delegate, EventHandler> delegateLookup = new Dictionary<System.Delegate, EventHandler>();

        public void AddListener<T>(EventHandler<T> del) where T : GameEvent
        {
            // Early-out if we've already registered this delegate
            if (delegateLookup.ContainsKey(del))
                return;

            // Create a new non-generic delegate which calls our generic one.
            // This is the delegate we actually invoke.
            EventHandler internalDelegate = (e) => del((T)e);
            delegateLookup[del] = internalDelegate;

            EventHandler tempDel;
            if (delegates.TryGetValue(typeof(T), out tempDel))
            {
                delegates[typeof(T)] = tempDel += internalDelegate;
            }
            else
            {
                delegates[typeof(T)] = internalDelegate;
            }
        }

        public void RemoveListener<T>(EventHandler<T> del) where T : GameEvent
        {
            EventHandler internalDelegate;
            if (delegateLookup.TryGetValue(del, out internalDelegate))
            {
                EventHandler tempDel;
                if (delegates.TryGetValue(typeof(T), out tempDel))
                {
                    tempDel -= internalDelegate;
                    if (tempDel == null)
                    {
                        delegates.Remove(typeof(T));
                    }
                    else
                    {
                        delegates[typeof(T)] = tempDel;
                    }
                }

                delegateLookup.Remove(del);
            }
        }

        public void Trigger(GameEvent e)
        {
            EventHandler del;
            if (delegates.TryGetValue(e.GetType(), out del))
            {
                del.Invoke(e);
            }
        }

        public void Destroy()
        {
            delegates.Clear();
            delegateLookup.Clear();
        }

    }
}