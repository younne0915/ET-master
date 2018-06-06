using System;
using UnityEngine;
using ETModel;
using UnityEngine.UI;

namespace ETHotfix
{

    [Event(ETModel.EventIdType.TouchStart)]
    public class TouchStart_UIBattleComponent : AEvent<Vector2>
    {
        public override void Run(Vector2 a)
        {
            throw new NotImplementedException();
        }
    }

    [Event(ETModel.EventIdType.TouchOffset)]
    public class TouchOffset_UIBattleComponent : AEvent<Vector2, Vector2>
    {
        public override void Run(Vector2 a, Vector2 b)
        {
            throw new NotImplementedException();
        }
    }

    [Event(ETModel.EventIdType.TouchEnd)]
    public class TouchEnd_UIBattleComponent : AEvent<Vector2>
    {
        public override void Run(Vector2 a)
        {
            throw new NotImplementedException();
        }
    }


    [ObjectSystem]
    public class UIBattleComponentStartSystem : StartSystem<UIBattleComponent>
    {
        public override void Start(UIBattleComponent self)
        {
            self.Start();
        }
    }

    [ObjectSystem]
    public class UIBattleComponentDestroySystem : DestroySystem<UIBattleComponent>
    {
        public override void Destroy(UIBattleComponent self)
        {
            self.Destroy();
        }
    }


    public class UIBattleComponent : Component
    {
        private RectTransform bgRectTransform;
        private Transform thumbTransform;
        private Image touchRegionImage;
        private Vector3 orginJoystickPos;
        private Transform joysticTransform;

        ///// <summary>
        ///// Button
        ///// </summary>
        private GameObject normalAttackBtn;
        private GameObject skill1Btn;
        private GameObject skill2Btn;
        private GameObject skill3Btn;


        private GeometryTransformComponent geomComponent;

        private float _lastAngle;

        private Vector3 _orginVec3;

        private bool _touchStart = false;

        public void Start()
        {
            ReferenceCollector rc = this.GetParent<UI>().GameObject.GetComponent<ReferenceCollector>();
            bgRectTransform = rc.Get<GameObject>("joysticBg").GetComponent<RectTransform>();
            orginJoystickPos = bgRectTransform.localPosition;
            thumbTransform = rc.Get<GameObject>("thumb").transform;
            touchRegionImage = rc.Get<GameObject>("touchRegion").GetComponent<Image>();
            joysticTransform = rc.Get<GameObject>("JoystickObj").transform;

            geomComponent = ETModel.Game.Scene.GetComponent<GeometryTransformComponent>();

            skill1Btn = rc.Get<GameObject>("skill1");
            skill1Btn.GetComponent<Button>().onClick.Add(OnSkill1BtnClick);
            skill2Btn = rc.Get<GameObject>("skill2");
            skill2Btn.GetComponent<Button>().onClick.Add(OnSkill2BtnClick);
            skill3Btn = rc.Get<GameObject>("skill3");
            skill3Btn.GetComponent<Button>().onClick.Add(OnSkill3BtnClick);
            normalAttackBtn = rc.Get<GameObject>("normalAttack");
            normalAttackBtn.GetComponent<Button>().onClick.Add(OnNormalAttackBtnClick);
            Game.Scene.GetComponent<GameEventComponent>().AddListener<TouchStartEvent>(TouchStart);
            Game.Scene.GetComponent<GameEventComponent>().AddListener<TouchOffsetEvent>(TouchOffset);
            Game.Scene.GetComponent<GameEventComponent>().AddListener<TouchEndEvent>(TouchEnded);


        }


        private void OnSkill1BtnClick()
        {
            Log.Debug("OnSkill1BtnClick");
        }

        private void OnSkill2BtnClick()
        {
            Log.Debug("OnSkill2BtnClick");

        }

        private void OnSkill3BtnClick()
        {
            Log.Debug("OnSkill3BtnClick");
        }

        private void OnNormalAttackBtnClick()
        {
            Log.Debug("OnNormalAttackBtnClick");
        }

        private bool CheckIfInTouchRegion(Vector2 touchPos)
        {
            Vector3 touchUguiLocalPos = geomComponent.ScreenPointToUGUILocalPoint(touchPos, joysticTransform);
            Vector2 pivotOffset = touchRegionImage.rectTransform.pivot;
            Vector3 pos = touchRegionImage.transform.localPosition;
            Vector2 rect = new Vector2(touchRegionImage.rectTransform.rect.width, touchRegionImage.rectTransform.rect.height);

            float minX = pos.x - rect.x * pivotOffset.x;
            float maxX = pos.x + rect.y * pivotOffset.x;

            float minY = pos.y - rect.y * pivotOffset.y;
            float maxY = pos.y + rect.y * pivotOffset.y;
            Log.Debug("pos = " + pos);
            Log.Debug("minX = " + minX + ", maxX = " + maxX + ", minY = " + minY + ", maxY = " + maxY);
            Log.Debug("touchUguiPos = " + touchUguiLocalPos);

            if (touchUguiLocalPos.x > minX && touchUguiLocalPos.x < maxX && touchUguiLocalPos.y > minY && touchUguiLocalPos.y < maxY)
            {
                return true;
            }
            return false;
        }

        public void TouchStart(TouchStartEvent ev)
        {
            Vector2 touchPos = ev.touchPos;
            if (!CheckIfInTouchRegion(touchPos)) return;
            _touchStart = true;
            bgRectTransform.localPosition = geomComponent.ScreenPointToUGUILocalPoint(touchPos, joysticTransform);
            thumbTransform.localPosition = geomComponent.ScreenPointToUGUILocalPoint(touchPos, joysticTransform);
        }

        public void TouchOffset(TouchOffsetEvent ev)
        {
            Vector2 touchPos = ev.touchPos;
            Vector2 offsetVec2 = ev.offsetDelVec;
            if (!_touchStart) return;

            Vector3 touchUguiPos = geomComponent.ScreenPointToUGUILocalPoint(touchPos, joysticTransform);
            Vector3 directRealVec3 = touchUguiPos - bgRectTransform.localPosition;
            float radius = bgRectTransform.rect.width / 2;
            if (directRealVec3.magnitude >= radius)
            {
                Vector3 directInsideCicleVec3 = directRealVec3.normalized * radius;
                Vector3 edgeCirclePos = bgRectTransform.localPosition + directInsideCicleVec3;
                thumbTransform.localPosition = edgeCirclePos;

            }
            else
            {
                thumbTransform.localPosition = touchUguiPos;
            }

            //得到弧度
            float angle = Mathf.Atan2(offsetVec2.x, offsetVec2.y) * Mathf.Rad2Deg;
            //Quaternion quaternion = Quaternion.Euler(new Vector3(0, angle + _orginVec3.y, 0));

            if (Mathf.Abs(angle - _lastAngle) > 0.5f)
            {
                //Log.Debug("send command");
            }
            _lastAngle = angle;
        }

        public void TouchEnded(TouchEndEvent ev)
        {
            Vector2 touchPos = ev.touchPos;
            if (!_touchStart) return;
            _touchStart = false;
            bgRectTransform.localPosition = orginJoystickPos;
            thumbTransform.localPosition = orginJoystickPos;

        }

        public void Destroy()
        {
            Game.Scene.GetComponent<GameEventComponent>().RemoveListener<TouchStartEvent>(TouchStart);
            Game.Scene.GetComponent<GameEventComponent>().RemoveListener<TouchOffsetEvent>(TouchOffset);
            Game.Scene.GetComponent<GameEventComponent>().RemoveListener<TouchEndEvent>(TouchEnded);
        }
    }
}
