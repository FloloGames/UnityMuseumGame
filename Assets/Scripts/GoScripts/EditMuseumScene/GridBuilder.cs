using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Grid;
using UnityEngine.EventSystems;

public class GridBuilder : MonoBehaviour
{
    private Grid.Grid grid;
    private static int SelectedPlaceItemIndex;

    void Start()
    {
        grid = new Grid.Grid(10, 10, 5f, new Vector3(-10, 0, -10));
        grid.CreateWorldUI();
        SelectedPlaceItemIndex = GridSaver.Instance.IndexOfEmpty;
    }

    void Update()
    {
        if (Input.touchCount <= 0)
            return;

        if (Input.GetTouch(0).phase == TouchPhase.Ended && !EditMenuCameraController.Dragging())
        {
            if (EventSystem.current.IsPointerOverGameObject())
                return;
            Vector3 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            grid.WorldPositionToIndex(worldPoint, out int i, out int j);
            GridNode node = new GridNode(i, j, GridSaver.Instance.gridObjects[SelectedPlaceItemIndex]);
            grid.SetValueNotGameObject(i, j, node);
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

    public static void SetSelectedPlaceIndex(int i)
    {
        SelectedPlaceItemIndex = i;
    }
}
