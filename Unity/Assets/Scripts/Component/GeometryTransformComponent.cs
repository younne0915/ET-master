using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace ETModel
{
    [ObjectSystem]
    public class GeometryTransformComponentAwakeSystem : AwakeSystem<GeometryTransformComponent>
    {
        public override void Awake(GeometryTransformComponent self)
        {
            self.Awake();
        }
    }

    public class GeometryTransformComponent : Component
    {

        public Camera uiCamera
        {
            get;
            private set;
        }

        private float _uguiZ;
        public float uguiZ
        {
            get { return _uguiZ; }
        }

        public void Awake()
        {
            GameObject obj = GameObject.Find("UICamera");
            if(obj != null)
            {
                uiCamera = obj.GetComponent<Camera>();
            }

            obj = GameObject.Find("LoginCanvas");
            if(obj != null)
            {
                Canvas loginCanvas = obj.GetComponent<Canvas>();
                _uguiZ = loginCanvas.planeDistance;
            }

        }
    }
}
