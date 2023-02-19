using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Grid;
using UnityEngine.EventSystems;

public class GridBuilder : MonoBehaviour
{
    private static int SelectedPlaceItemIndex;
    [Header("Prefabs")]
    [SerializeField]
    private GameObject selectedItemPrefab;

    [Header("Constants")]
    [SerializeField]
    private float selectedItemMovementSpeed = 3f;


    private GameObject selectedItemInstance;

    private Grid.Grid grid;

    private Vector3 selectedItemTargetPos;
    private Vector2 selectedGridIndex;

    void Start()
    {
        grid = new Grid.Grid(10, 10, 5f, new Vector3(-10, 0, -10));
        grid.CreateWorldUI();
        SelectedPlaceItemIndex = GridSaver.Instance.IndexOfEmpty;
        selectedGridIndex = new Vector2();
        selectedItemInstance = Instantiate(selectedItemPrefab, new Vector3(), Quaternion.Euler(90, 0, 0));
        selectedItemInstance.transform.localScale = new Vector3(grid.CellSize, grid.CellSize, 1);
        selectedItemTargetPos = grid.IndexToWorldPosition(0, 0);
    }

    void Update()
    {
        UpdateSelectedItemInstance();
        if (Input.touchCount <= 0)
            return;

        Debug.Log("Clicked UI: " + EventSystem.current.IsPointerOverGameObject());

        if (Input.GetTouch(0).phase == TouchPhase.Ended && !EditMenuCameraController.Dragging())
        {
            if (EventSystem.current.IsPointerOverGameObject())
                return;
            Vector3 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            grid.WorldPositionToIndex(worldPoint, out int i, out int j);
            if (!grid.IndexInGrid(i, j))
                return;
            selectedGridIndex.Set(i, j);
            selectedItemTargetPos = grid.IndexToWorldPosition(i, j);
            selectedItemTargetPos.y = 1;
            //GridNode node = new GridNode(i, j, GridSaver.Instance.gridObjects[SelectedPlaceItemIndex]);
            //grid.SetValueNotGameObject(i, j, node);
        }
        //else if (Input.GetMouseButtonDown(1))
        //{
        //    Vector3 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //    grid.WorldPositionToIndex(worldPoint, out int i, out int j);
        //    GridNode node = new GridNode(i, j, GridSaver.Instance.gridObjects[1]);
        //    grid.SetValueNotGameObject(i, j, node);
        //}
        //else if (Input.GetMouseButtonDown(2))
        //{
        //    Vector3 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //    GridNode node = grid.GetValue(worldPoint);
        //    Debug.Log(node.ToString());
        //}
    }
    private void UpdateSelectedItemInstance()
    {
        selectedItemInstance.transform.position = Vector3.Lerp(selectedItemInstance.transform.position, selectedItemTargetPos, Time.deltaTime * selectedItemMovementSpeed);
    }
    public static void SetSelectedPlaceIndex(int i)
    {
        SelectedPlaceItemIndex = i;
    }
}
