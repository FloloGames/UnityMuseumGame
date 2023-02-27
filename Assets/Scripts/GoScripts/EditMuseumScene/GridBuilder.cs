using Help;
using Managers;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Grid
{
    public class GridBuilder : MonoBehaviour
    {
        private static GridBuilder _instance;
        public static GridBuilder Instance => _instance;

        public int SelectedPlaceItemIndex;

        [Header("Prefabs")]
        [SerializeField]
        private GameObject selectedItemPrefab;

        private GridObject SelectedItem;

        private Grid _grid;
        public Grid Grid => _grid;

        private bool clickOnGUI = false;

        private void Awake()
        {
            _instance = this;
        }
        private void Start()
        {
            _grid = new Grid(20, 20, 2f, new Vector3(-10, 0, -10));
            _grid.CreateWorldUI();
            SelectedPlaceItemIndex = GridObjectLoader.Instance.GetItemIndexByType(GridType.EMPTY);
            SelectedTileItemManager.Init(selectedItemPrefab);
        }

        private void Update()
        {
            if (Input.touchCount <= 0)
                return;


            if (Input.GetTouch(0).phase == TouchPhase.Ended && !EditMenu_CameraController.Dragging())
            {
                if (clickOnGUI)
                    return;

                Vector3 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                _grid.WorldPositionToIndex(worldPoint, out int i, out int j);
                if (!_grid.IndexInGrid(i, j))
                    return;

                if (SelectedItem.type != GridType.SELECT)
                {
                    GridNode node = new GridNode(i, j, Color.white, SelectedItem);
                    _grid.SetValueNotGameObject(i, j, node);
                    return;
                }



                if (SelectedTileItemManager.Index.x == i && SelectedTileItemManager.Index.y == j)
                {
                    SelectedTileItemManager.RemoveCurrentSpawnedItemTile();
                    SelectedTileItemManager.ResetIndex();
                    return;
                }
                SelectedTileItemManager.SpawnNewSelectedItemTile(i, j);
                GridNode gridNode = Grid.GetValue(i, j);
                CreatePanelItemsManger.Instance.ClearActionPanelItems();
                CreatePanelItemsManger.Instance.CreateActionPanelItems(gridNode.GridObject);
            }
            clickOnGUI = HelperFunctions.IsCurrFirstTouchOnGUI();
        }
        public static void SetSelectedPlaceIndex(int i, GridObject gridObject)
        {
            Instance.SelectedItem = gridObject;
            Instance.SelectedPlaceItemIndex = i;
        }
    }
}