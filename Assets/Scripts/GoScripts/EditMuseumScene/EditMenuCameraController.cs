using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EditMenuCameraController : MonoBehaviour
{
    private static EditMenuCameraController instance;

    [Header("Constants")]
    [SerializeField]
    [Tooltip("Wenn die zeit in sec. die der Finger auf dem Display war kleiner ist als angegebene zahl dann wurde die Cam nicht bewegt")]
    private float notDraggedTime = 0.5f;
    [SerializeField]
    private float moveSpeed = 5f;     // How fast the camera moves when the player drags their finger
    [SerializeField]
    private float zoomSpeed = 0.5f;   // How fast the camera zooms in/out when the player pinches
    [SerializeField]
    private float maxCameraZoom = 10;
    [SerializeField]
    private float minCameraZoom = 40;
    [SerializeField]
    private Camera _targetCamera;

    private Vector3 targetPosition;
    private Vector3 touchStartPos; // The position of the touch when the player first starts dragging
    private Vector3 dragOffset;

    private float touchStartTime; // in seconds

    private float targetSize;
    private float startSize;


    private void Awake()
    {
        targetSize = GetCameraSize();
        instance = this;
    }
    public static bool Dragging()
    {
        if (instance == null)
        {
            Debug.Log("instance null");
            return false;
        }
        return instance.IsDragging();
    }
    private void Update()
    {
        //Debug.Log(EventSystem.current.IsPointerOverGameObject());
        //Debug.Log(GetCameraSize());
        UpdateMoveCamera();
        MoveCamera();
        UpdateCameraSize();
        SizeCamera();
    }
    private void MoveCamera()
    {
        Vector3 camPos = Vector3.Lerp(_targetCamera.transform.position, targetPosition, Time.deltaTime * moveSpeed);
        camPos.y = _targetCamera.transform.position.y;
        _targetCamera.transform.position = camPos;
    }
    private void SizeCamera()
    {
        float camSize = Mathf.Clamp(GetCameraSize(), targetSize, Time.deltaTime * zoomSpeed);
        //_targetCamera.GetComponent<Camera>().orthographicSize = camSize;
    }
    private void UpdateCameraSize()
    {
        Debug.Log(GetPressTime() + " " + Dragging());
    }
    private void UpdateMoveCamera()
    {
        if (Input.touchCount <= 0)
            return;

        Vector3 currTouchPos = GetCenterOfPresses();
        if (Input.GetTouch(0).phase == TouchPhase.Began)
        {
            touchStartTime = Time.time;
            touchStartPos = currTouchPos;
            return;
        }
        if (Input.GetTouch(0).phase == TouchPhase.Moved) // If the player is dragging their finger
        {
            dragOffset = touchStartPos - currTouchPos;
            targetPosition = _targetCamera.transform.position + dragOffset;
            return;
        }
    }
    private float GetCameraSize()
    {
        return _targetCamera.GetComponent<Camera>().orthographicSize;
    }
    private Vector3 GetCenterOfPresses()
    {
        Vector3 center = new Vector3();
        foreach (var touch in Input.touches)
        {
            center += _targetCamera.ScreenToWorldPoint(touch.position);
        }
        center /= Input.touchCount;
        return center;
    }
    private bool IsDragging()
    {
        return GetPressTime() > notDraggedTime;
    }
    private float GetPressTime()
    {
        return Mathf.Abs(Time.time - touchStartTime);
    }
    //private void UpdateInputSystem()
    //{
    //    if (Input.GetMouseButtonDown(0))
    //    {
    //        //is over UI-GameObject
    //        _mousePressed = true;
    //        _uiBetween = EventSystem.current.IsPointerOverGameObject();
    //        pressStartedTime = Time.time;
    //        _startPressPos = _targetCamera.ScreenToWorldPoint(Input.mousePosition);
    //        if (!_uiBetween)
    //            GameObject.CreatePrimitive(PrimitiveType.Cube).transform.position = _startPressPos;
    //    }
    //    if (Input.GetMouseButton(0))
    //        _currPressPos = _targetCamera.ScreenToWorldPoint(Input.mousePosition);

    //    if (Input.GetMouseButtonUp(0))
    //    {
    //        _startPressPos = Vector3.zero;
    //        _currPressPos = Vector3.zero;
    //        _mousePressed = false;
    //        _uiBetween = false;
    //        pressStartedTime = -1;
    //    }
    //}

}
