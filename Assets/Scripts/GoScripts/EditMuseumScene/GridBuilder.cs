using Help;
using Managers;
using UnityEngine;

namespace Grid
{
    public class GridBuilder : MonoBehaviour
    {
        private static GridBuilder _instance;
        public static GridBuilder Instance => _instance;

        public GridObject SelectedPlaceItem { get; private set; }

        [Header("Prefabs")]
        [SerializeField]
        private GameObject selectedItemPrefab;

        private Grid mGrid;
        public Grid Grid => mGrid;

        private bool clickOnGUI = false;

        private void Awake()
        {
            _instance = this;
        }
        private void Start()
        {
            mGrid = new Grid(20, 20, 2f, new Vector3(-10, 0, -10));
            mGrid.CreateWorldUI();
            SelectedPlaceItem = HelperFunctions.CreateNormalGridObject();
            SelectedTileItemManager.Init(selectedItemPrefab);
        }
        private void Update()
        {
            UpdateTouchInput();
        }
        private void UpdateTouchInput()
        {
            if (Input.touchCount <= 0)
                return;


            if (Input.GetTouch(0).phase == TouchPhase.Ended && !EditMenu_CameraController.Dragging())
            {
                DisplayTouched();
            }
            clickOnGUI = HelperFunctions.IsCurrFirstTouchOnGUI();
            Debug.Log("Clicked GUI: " + clickOnGUI);
        }
        private void DisplayTouched()
        {
            if (clickOnGUI)
                return;

            Vector3 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mGrid.WorldPositionToIndex(worldPoint, out int i, out int j);
            if (!mGrid.IndexInGrid(i, j))
                return;

            SelectedPlaceItem.PlaceOnGrid(Grid, i, j);
        }
        public static void SetSelectedPlaceItem(GridObject selectedItem)
        {
            Instance.SelectedPlaceItem = selectedItem;
        }
    }
}