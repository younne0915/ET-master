using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ETModel
{

    public static class GeometryTransformComponentSystem
    {
        public static Vector3 ScreenPointToUGUIWorldPoint(this GeometryTransformComponent self, Vector2 touchPos)
        {
            //Vector3 screenPos = new Vector3(touchPos.x, touchPos.y, 0);
            if (self.uiCamera != null)
            {
                //uguiZ ：Canvas相对于Camera的距离
                Vector3 screenPos = new Vector3(touchPos.x, touchPos.y, self.uguiZ);
                return self.uiCamera.ScreenToWorldPoint(screenPos);
            }
            Log.Error("uiCamera == null");
            return Vector3.zero;
        }

        public static Vector3 ScreenPointToUGUILocalPoint(this GeometryTransformComponent self, Vector2 touchPos, Transform parentTrans)
        {
            if (self.uiCamera != null)
            {
                Vector3 screenPos = new Vector3(touchPos.x, touchPos.y, self.uguiZ);
                Vector3 worldVec3 = self.uiCamera.ScreenToWorldPoint(screenPos);
                return parentTrans.InverseTransformPoint(worldVec3);
            }

            Log.Error("uiCamera == null");
            return Vector3.zero;
        }


        /// <summary>
        /// 如果用NGUI可用这个方法
        /// </summary>
        //public Vector3 ScreenPointToNGUIWorldPoint(Vector2 touchPos)
        //{
        //    Vector3 screenPos = new Vector3(touchPos.x, touchPos.y, 0);
        //    if (UICamera.currentCamera != null)
        //    {
        //        return UICamera.currentCamera.ScreenToWorldPoint(screenPos);
        //    }
        //    Log.Error("UICamera.currentCamera == null");
        //    return Vector3.zero;
        //}

        //public Vector3 ScreenPointToNGUILocalPoint(Vector2 touchPos, Transform localTransform)
        //{
        //    Vector3 screenPos = new Vector3(touchPos.x, touchPos.y, 0);
        //    if (UICamera.currentCamera != null)
        //    {
        //        Vector3 worldVec3 = UICamera.currentCamera.ScreenToWorldPoint(screenPos);
        //        return localTransform.InverseTransformPoint(worldVec3);
        //    }
        //    Log.Error("UICamera.currentCamera == null");
        //    return Vector3.zero;
        //}
    }
}
