using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EditMenuCameraMovement : MonoBehaviour
{
    [SerializeField]
    private Camera _targetCamera;

    private Vector3 _startPressPos;
    private Vector3 _currPressPos;

    /// <summary>
    /// value in Seconds. 
    /// Value is -1 when not pressed and
    /// the time when mouse got pressed 
    /// </summary>
    private float pressStartedTime;

    /// <summary>
    /// true if an UI-Object (eg. button) got clicked
    /// </summary>
    private bool _uiBetween = false;
    private bool _mousePressed = false;


    private void Awake()
    {
        //if (_targetCamera = null)
        //{
        //    Debug.LogError("Camera not set to an Object");
        //    _targetCamera = Camera.main;
        //}
    }

    private void Update()
    {
        UpdateInputSystem();
    }

    private void UpdateInputSystem()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //is over UI-GameObject
            _mousePressed = true;
            _uiBetween = EventSystem.current.IsPointerOverGameObject();
            pressStartedTime = Time.time;
            _startPressPos = _targetCamera.ScreenToWorldPoint(Input.mousePosition);
            if (!_uiBetween)
                GameObject.CreatePrimitive(PrimitiveType.Cube).transform.position = _startPressPos;
        }
        if (Input.GetMouseButton(0))
            _currPressPos = _targetCamera.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetMouseButtonUp(0))
        {
            _startPressPos = Vector3.zero;
            _currPressPos = Vector3.zero;
            _mousePressed = false;
            _uiBetween = false;
            pressStartedTime = -1;
        }
    }

}
