using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Grid;

public class GridTesting : MonoBehaviour
{
    Grid.Grid grid;
    void Start()
    {
        grid = new Grid.Grid(10, 10, 5f, new Vector3(-10, 0, -10));
        grid.CreateWorldUI();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            grid.WorldPositionToIndex(worldPoint, out int i, out int j);
            GridNode node = new GridNode(i, j);
            node.TintColor = Color.red;
            grid.SetValueNotGameObject(i, j, node);
        }
        else if (Input.GetMouseButtonDown(1))
        {
            Vector3 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            GridNode node = grid.GetValue(worldPoint);
            Debug.Log(node.ToString());
        }
    }
}
