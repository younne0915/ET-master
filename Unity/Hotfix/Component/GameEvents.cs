using UnityEngine;

namespace ETHotfix
{
    public class GameEvent
    {
    }

    public class TouchStartEvent : GameEvent
    {
        public Vector2 touchPos;

        public TouchStartEvent(Vector2 touchScreenPos)
        {
            this.touchPos = touchScreenPos;
        }
    }

    public class TouchMovingEvent : GameEvent
    {
        public Vector2 touchPos;
        public Vector2 deltVec2;

        public TouchMovingEvent(Vector2 touchScreenPos, Vector2 deltVec)
        {
            this.touchPos = touchScreenPos;
            this.deltVec2 = deltVec;
        }
    }

    public class TouchOffsetEvent : GameEvent
    {
        public Vector2 touchPos;
        public Vector2 offsetDelVec;

        public TouchOffsetEvent(Vector2 touchScreenPos, Vector2 deltVec)
        {
            this.touchPos = touchScreenPos;
            this.offsetDelVec = deltVec;
        }
    }

    public class TouchEndEvent : GameEvent
    {
        public Vector2 touchPos;

        public TouchEndEvent(Vector2 touchScreenPos)
        {
            this.touchPos = touchScreenPos;
        }
    }

    public class TouchStationaryEvent : GameEvent
    {
        public Vector2 touchPos;

        public TouchStationaryEvent(Vector2 touchScreenPos)
        {
            this.touchPos = touchScreenPos;
        }
    }

    public class TouchCancledEvent : GameEvent
    {

    }
}