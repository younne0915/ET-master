using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using ETModel;

namespace ETHotfix
{
    [ObjectSystem]
    public class InputMonitorComponentUpdateSystem : UpdateSystem<InputMonitorComponent>
    {
        public override void Update(InputMonitorComponent self)
        {
            self.Update();
        }
    }

    public class InputMonitorComponent : Component
    {
        private bool _pressed = false;

        private Vector2 _lastMousePos;

        private Vector2 _deltVect2;

        private Vector2 _curVect2 = Vector2.one;

        private float _sensitivity = 0.05f;

        private Vector3 _touchBeganPos;


        public void Update()
        {
#if (UNITY_ANDROID || UNITY_IPHONE) && !UNITY_EDITOR

            //TouchStart
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            {
                _touchBeganPos = new Vector3(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y, 0);
                TouchStart(Input.GetTouch(0).position);
            }

            //TouchMoving
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
            {
            #region TouchMoving
                if (Vector3.SqrMagnitude(Input.GetTouch(0).deltaPosition) > _sensitivity)
                {
                    TouchMoving(Input.GetTouch(0).position, Input.GetTouch(0).deltaPosition);
                }
                else
                {

                    TouchStationary(Input.GetTouch(0).position);
                }
            #endregion


            #region TouchOffset  通过touch点与touchstart点相比较的偏移量，求出旋转角度，对模型进行移动
                Vector3 offset = new Vector3(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y, 0) - _touchBeganPos;

                if (Vector3.SqrMagnitude(offset) > 0.5f)
                {
                    TouchOffset(Input.GetTouch(0).position, offset);
                }
            #endregion
            }

            //TouchEnd
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                TouchEnd(Input.GetTouch(0).position);
            }

            //TouchStationary
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Stationary)
            {
                TouchStationary(Input.GetTouch(0).position);
            }

            //TouchCancled
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Canceled)
            {
                TouchCancled(Input.GetTouch(0).position);
            }
#else

            //TouchStart
            if (Input.GetMouseButtonDown(0))
            {
                #region TouchMoving
                _lastMousePos = Input.mousePosition;
                #endregion

                #region TouchOffset
                _touchBeganPos = Input.mousePosition;
                #endregion

                _pressed = true;
                TouchStart(Input.mousePosition);
            }

            //TouchMoving or TouchOffset
            if (Input.GetMouseButton(0) && _pressed)
            {
                #region TouchMoving
                _curVect2.x = Input.mousePosition.x;
                _curVect2.y = Input.mousePosition.y;
                _deltVect2 = _curVect2 - _lastMousePos;
                _lastMousePos = Input.mousePosition;

                if (Mathf.Abs(_deltVect2.x) > _sensitivity || Mathf.Abs(_deltVect2.y) > _sensitivity)
                {
                    TouchMoving(Input.mousePosition, _deltVect2);
                }
                else
                {

                    TouchStationary(Input.mousePosition);
                }
                #endregion


                #region TouchOffset  通过touch点与touchstart点相比较的偏移量，求出旋转角度，对模型进行移动
                Vector3 offset = Input.mousePosition - _touchBeganPos;

                if (Vector3.SqrMagnitude(offset) > 0.5f)
                {
                    TouchOffset(Input.mousePosition, offset);
                }
                #endregion

            }


            //TouchEnd
            if (!Input.GetMouseButton(0) && _pressed)
            {
                _pressed = false;
                TouchEnd(Input.mousePosition);
            }

#endif

        }

        private void TouchStart(Vector2 touchPos)
        {
            //Game.EventSystem.Run(EventIdType.TouchStart, touchPos);
            Game.Scene.GetComponent<GameEventComponent>().Trigger(new TouchStartEvent(touchPos));
        }

        private void TouchMoving(Vector2 touchPos, Vector2 deltVec2)
        {
            //Game.EventSystem.Run(EventIdType.TouchMoving, touchPos, deltVec2);
            Game.Scene.GetComponent<GameEventComponent>().Trigger(new TouchMovingEvent(touchPos, deltVec2));
        }

        private void TouchOffset(Vector2 touchPos, Vector2 offsetVec3)
        {
            //Game.EventSystem.Run(EventIdType.TouchOffset, touchPos, offsetVec3);
            Game.Scene.GetComponent<GameEventComponent>().Trigger(new TouchOffsetEvent(touchPos, offsetVec3));
        }

        private void TouchEnd(Vector2 touchPos)
        {
            //Game.EventSystem.Run(EventIdType.TouchEnd, touchPos);
            Game.Scene.GetComponent<GameEventComponent>().Trigger(new TouchEndEvent(touchPos));

        }

        private void TouchStationary(Vector2 touchPos)
        {
            //Game.EventSystem.Run(EventIdType.TouchStationary, touchPos);
            Game.Scene.GetComponent<GameEventComponent>().Trigger(new TouchStationaryEvent(touchPos));
        }

        private void TouchCancled(Vector2 touchPos)
        {
            //Game.EventSystem.Run(EventIdType.TouchCancled, touchPos);
        }
    }
}
