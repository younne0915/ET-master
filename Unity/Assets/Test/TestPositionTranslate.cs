using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPositionTranslate : MonoBehaviour {

    public Camera uiCamera;
    public Transform joystickTransform;
    private Vector3 originVec3;

	// Use this for initialization
	void Start () {
        uiCamera = GetComponent<Camera>();
        Debug.Log("joystickTransform world pos = " + joystickTransform.localPosition);
        originVec3 = joystickTransform.localPosition;
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            if(uiCamera != null)
            {
                //Vector3 screenPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 100);
                //Vector3 worldPos = uiCamera.ScreenToWorldPoint(screenPos);

                //Debug.Log("worldPos = " + worldPos);
                joystickTransform.localPosition = originVec3 + originVec3;
            }
        }
    }
}
