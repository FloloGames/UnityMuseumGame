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

        private Grid _grid;
        public Grid Grid => _grid;

        private void Awake()
        {
            _instance = this;
        }
        private void Start()
        {
            _grid = new Grid(20, 20, 2f, new Vector3(-10, 0, -10));
            _grid.CreateWorldUI();
            SelectedPlaceItemIndex = GridObjectLoader.Instance.GetItemIndexByType(GridType.EMPTY);
            SelectedItemTile.Init(selectedItemPrefab);
        }

        private void Update()
        {
            if (Input.touchCount <= 0)
                return;

            bool onGUI = EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId);

            if (Input.GetTouch(0).phase == TouchPhase.Ended && !EditMenu_CameraController.Dragging())
            {
                if (onGUI)
                {
                    Debug.Log("Touch is on a UI element");
                    return;
                }
                Vector3 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                _grid.WorldPositionToIndex(worldPoint, out int i, out int j);
                if (!_grid.IndexInGrid(i, j))
                    return;

                //if (GridSaver.Instance.gridObjects[SelectedPlaceItemIndex].type != GridType.OTHER)
                //{
                //    GridNode node = new GridNode(i, j, Color.white, GridSaver.Instance.gridObjects[SelectedPlaceItemIndex]);
                //    _grid.SetValueNotGameObject(i, j, node);
                //    return;
                //}

                if (SelectedItemTile.Index.x == i && SelectedItemTile.Index.y == j)
                {
                    SelectedItemTile.RemoveCurrentSpawnedItemTile();
                    SelectedItemTile.ResetIndex();
                    return;
                }
                SelectedItemTile.SpawnNewSelectedItemTile(i, j);

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
            Instance.SelectedPlaceItemIndex = i;
        }
    }
}