using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

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

        public void Awake()
        {
            GameObject obj = GameObject.Find("UICamera");
            if(obj != null)
            {
                uiCamera = obj.GetComponent<Camera>();
            }
        }
    }
}
