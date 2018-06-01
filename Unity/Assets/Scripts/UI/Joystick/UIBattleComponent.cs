using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using ETModel;
using UnityEngine.UI;

namespace Hotfix
{

    [Event(EventIdType.TouchStart)]
    public class TouchStart_UIBattleComponent : AEvent<Vector2>
    {
        public override void Run(Vector2 a)
        {
            throw new NotImplementedException();
        }
    }

    [Event(EventIdType.TouchOffset)]
    public class TouchOffset_UIBattleComponent : AEvent<Vector2, Vector2>
    {
        public override void Run(Vector2 a, Vector2 b)
        {
            throw new NotImplementedException();
        }
    }

    [Event(EventIdType.TouchEnd)]
    public class TouchEnd_UIBattleComponent : AEvent<Vector2>
    {
        public override void Run(Vector2 a)
        {
            throw new NotImplementedException();
        }
    }


    [ObjectSystem]
    public class UIBattleComponentAwakeSystem : AwakeSystem<UIBattleComponent>
    {
        public override void Awake(UIBattleComponent self)
        {
            
        }
    }



    public class UIBattleComponent : ETModel.Component
    {
        //private Transform bgTransform;
        //private Transform thumbTransform;
        //private Image touchRegionImage;
        //private Vector3 orginJoystickPos;
        //private Transform joysticTransform;

        ///// <summary>
        ///// Button
        ///// </summary>
        //private GameObject normalAttackBtn;
        //private GameObject skill1Btn;
        //private GameObject skill2Btn;
        //private GameObject skill3Btn;
        //private GameObject quitBtn;


        //private GeometryTransformComponent geomComponent;

        //private float _lastAngle;

        //private Vector3 _orginVec3;

        //private bool _touchStart = false;

        //public void Awake()
        //{
        //    ReferenceCollector rc = this.GetParent<UI>().GameObject.GetComponent<ReferenceCollector>();
        //    bgTransform = rc.Get<GameObject>("joysticBg").transform;
        //    thumbTransform = rc.Get<GameObject>("thumb").transform;
        //    touchRegionImage = rc.Get<GameObject>("touchRegion").GetComponent<UISprite>();
        //    orginJoystickPos = bgTransform.localPosition;
        //    joysticTransform = rc.Get<GameObject>("JoystickObj").transform;
        //    geomComponent = Game.Scene.GetComponent<GeometryTransformComponent>();

        //    skill1Btn = rc.Get<GameObject>("skill1").GetComponent<UIButton>();
        //    Game.Scene.GetComponent<NEventComponent>().Register(skill1Btn, OnBtnClick);
        //    skill2Btn = rc.Get<GameObject>("skill2").GetComponent<UIButton>();
        //    Game.Scene.GetComponent<NEventComponent>().Register(skill2Btn, OnBtnClick);
        //    skill3Btn = rc.Get<GameObject>("skill3").GetComponent<UIButton>();
        //    Game.Scene.GetComponent<NEventComponent>().Register(skill3Btn, OnBtnClick);
        //    GameObject oj = rc.Get<GameObject>("normalAttack");
        //    normalAttackBtn = oj.GetComponent<UIButton>();
        //    Game.Scene.GetComponent<NEventComponent>().Register(normalAttackBtn, OnBtnClick);
        //    oj = rc.Get<GameObject>("quitBtn");
        //    quitBtn = oj.GetComponent<UIButton>();
        //    Game.Scene.GetComponent<NEventComponent>().Register(quitBtn, OnBtnClick);
        //}

        //private void OnBtnClick()
        //{
        //    if (UIButton.current == skill1Btn)
        //    {
        //        OnSkill1BtnClick();
        //    }
        //    else if (UIButton.current == skill2Btn)
        //    {
        //        OnSkill2BtnClick();
        //    }
        //    else if (UIButton.current == skill3Btn)
        //    {
        //        OnSkill3BtnClick();
        //    }
        //    else if (UIButton.current == normalAttackBtn)
        //    {
        //        OnNormalAttackBtnClick();
        //    }
        //    else if (UIButton.current == quitBtn)
        //    {
        //        OnQuitBtnClick();
        //    }
        //}

        //private void OnQuitBtnClick()
        //{
        //    string account = PlayerComponent.Instance.MyPlayer.Account;

        //    SessionComponent.Instance.Session.CallWithAction(new C2G_PlayerQuit(account), (response) =>
        //    {
        //        OnSendPlayerQuitCallback(response);
        //    });
        //}

        //private void OnSendPlayerQuitCallback(AResponse response)
        //{
        //    G2C_PlayerQuit g2C_PlayerQuit = (G2C_PlayerQuit)response;
        //    if (g2C_PlayerQuit.Error != ErrorCode.ERR_Success)
        //    {
        //        Log.Error(g2C_PlayerQuit.Error.ToString());
        //        return;
        //    }

        //    //await Game.Scene.GetComponent<SceneChangeComponent>().ChangeSceneAsync(Model.SceneType.Init);

        //    //Hotfix.Scene.GetComponent<UIComponent>().Remove(UIType.UIBattle);
        //    //Hotfix.Scene.GetComponent<UIComponent>().Create(UIType.UILogin);

        //    //Game.Scene.RemoveComponent<SessionComponent>();
        //    //Game.Scene.RemoveComponent<HeartBeatComponent>();
        //    Application.Quit();

        //}

        //private void OnSkill1BtnClick()
        //{
        //    Log.Debug("OnSkill1BtnClick");
        //}

        //private void OnSkill2BtnClick()
        //{
        //    Log.Debug("OnSkill2BtnClick");

        //}

        //private void OnSkill3BtnClick()
        //{
        //    Log.Debug("OnSkill3BtnClick");
        //}

        //private void OnNormalAttackBtnClick()
        //{
        //    Hotfix.Scene.GetComponent<CommandSendComponent>().AttempSendNormalAttack();
        //}

        //private bool CheckIfInTouchRegion(Vector2 touchPos)
        //{
        //    Vector3 touchNguiPos = geomComponent.ScreenPointToNGUIWorldPoint(touchPos);
        //    Vector2 pivotOffset = touchRegionImage.pivotOffset;
        //    Vector3 pos = touchRegionImage.transform.position;
        //    Vector2 rect = new Vector2(touchRegionImage.width, touchRegionImage.height);

        //    float minX = pos.x - pivotOffset.x;
        //    float maxX = pos.x + pivotOffset.x;

        //    float minY = pos.y - pivotOffset.y;
        //    float maxY = pos.y + pivotOffset.y;

        //    if (touchNguiPos.x > minX && touchNguiPos.x < maxX && touchNguiPos.y > minY && touchNguiPos.y < maxY)
        //    {
        //        return true;
        //    }
        //    return false;
        //}

        //public void TouchStart(Vector2 touchPos)
        //{
        //    if (!CheckIfInTouchRegion(touchPos)) return;
        //    _touchStart = true;
        //    bgTransform.localPosition = geomComponent.ScreenPointToNGUILocalPoint(touchPos, joysticTransform);
        //    thumbTransform.localPosition = geomComponent.ScreenPointToNGUILocalPoint(touchPos, joysticTransform);
        //}

        //public void TouchOffset(Vector2 touchPos, Vector2 offsetVec2)
        //{
        //    if (!_touchStart) return;

        //    Vector3 touchNguiPos = geomComponent.ScreenPointToNGUILocalPoint(touchPos, joysticTransform);
        //    Vector3 directRealVec3 = touchNguiPos - bgTransform.localPosition;
        //    float radus = bgTransform.GetComponent<UISprite>().localSize.x / 2;
        //    if (directRealVec3.magnitude >= radus)
        //    {
        //        Vector3 directInsideCicleVec3 = directRealVec3.normalized * radus;
        //        Vector3 edgeCirclePos = bgTransform.localPosition + directInsideCicleVec3;
        //        thumbTransform.localPosition = edgeCirclePos;

        //    }
        //    else
        //    {
        //        thumbTransform.localPosition = touchNguiPos;
        //    }

        //    //得到弧度
        //    float angle = Mathf.Atan2(offsetVec2.x, offsetVec2.y) * Mathf.Rad2Deg;
        //    //Quaternion quaternion = Quaternion.Euler(new Vector3(0, angle + _orginVec3.y, 0));

        //    if (Mathf.Abs(angle - _lastAngle) > 0.5f)
        //    {
        //        Hotfix.Scene.GetComponent<CommandSendComponent>().AttempSendMoveCmd(offsetVec2);
        //    }
        //    _lastAngle = angle;
        //}

        //public void TouchEnded(Vector2 touchPos)
        //{
        //    if (!_touchStart) return;
        //    _touchStart = false;
        //    bgTransform.localPosition = orginJoystickPos;
        //    thumbTransform.localPosition = orginJoystickPos;

        //    Hotfix.Scene.GetComponent<CommandSendComponent>().AttempSendMoveEnd();
        //}
    }
}
