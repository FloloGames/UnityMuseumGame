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
    private float zoomSpeed = 1f;   // How fast the camera zooms in/out when the player pinches
    [SerializeField]
    private float maxCameraZoom = 10;
    [SerializeField]
    private float minCameraZoom = 40;
    [SerializeField]
    private Camera _targetCamera;

    private Vector3 targetPosition;
    private Vector3 touchStartPos; // The position of the touch when the player first starts dragging


    private float touchStartTime; // in seconds

    [SerializeField]
    private float targetCamSize;
    private float startWorldDist;

    [SerializeField]
    float multiplyer = 0.5f;

    private void Awake()
    {
        targetCamSize = GetCameraSize();
        instance = this;
    }
    public static bool Dragging()
    {
        if (instance == null)
        {
            return false;
        }
        return instance.IsDragging();
    }
    private void Update()
    {
        //Debug.Log(EventSystem.current.IsPointerOverGameObject());
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
        float dist = Mathf.Abs(GetCameraSize() - targetCamSize);
        if (dist < 0.5f)
            return;

        targetCamSize = Mathf.Clamp(targetCamSize, maxCameraZoom, minCameraZoom);
        float camSize = Mathf.Lerp(GetCameraSize(), targetCamSize, Time.deltaTime * zoomSpeed);
        _targetCamera.GetComponent<Camera>().orthographicSize = camSize;
    }
    private void UpdateCameraSize()
    {
        if (Input.touchCount < 2)
            return;

        var touch1 = Input.GetTouch(0);
        var touch2 = Input.GetTouch(1);

        Vector2 touch1PrevPos = touch1.position - touch1.deltaPosition;
        Vector2 touch2PrevPos = touch2.position - touch2.deltaPosition;

        float currDist = (touch1.position - touch2.position).magnitude;
        float prevDist = (touch1PrevPos - touch2PrevPos).magnitude;

        float diff = currDist - prevDist;


        Debug.Log(currDist + " " + prevDist);
        Debug.Log(diff);

        targetCamSize = GetCameraSize() - diff * multiplyer;
    }
    private void UpdateMoveCamera()
    {
        if (Input.touchCount <= 0)
            return;

        Vector3 currTouchPos = GetCenterOfPresses();
        if (Input.GetTouch(0).phase == TouchPhase.Began) //Press started
        {
            touchStartTime = Time.time;
            touchStartPos = currTouchPos;
            return;
        }
        if (Input.GetTouch(0).phase == TouchPhase.Moved) // If the player is dragging their finger
        {
            Vector3 dragOffset = touchStartPos - currTouchPos;
            targetPosition = _targetCamera.transform.position + dragOffset;
            return;
        }
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
    private float GetCameraSize()
    {
        return _targetCamera.GetComponent<Camera>().orthographicSize;
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
