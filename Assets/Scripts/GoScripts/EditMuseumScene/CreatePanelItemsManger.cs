using Grid;
using Help;
using System.Collections.Generic;
using UnityEngine;

namespace Managers
{
    /// <summary>
    /// Creates all UI Items
    /// <para>uses the <see cref="GridObjectLoader.Instance"/> to get all the Data</para>
    /// </summary>
    public class CreatePanelItemsManger : MonoBehaviour
    {
        private static CreatePanelItemsManger _instance;
        public static CreatePanelItemsManger Instance => _instance;

        [Header("Prefabs")]
        [SerializeField]
        private GameObject ActionItemPrefab;
        [SerializeField]
        private GameObject PlaceItemPrefab;

        [Header("References")]
        [SerializeField]
        private Transform TopPanel;
        [SerializeField]
        private Transform ActionPanel;
        [SerializeField]
        private Transform BottomPanel;

        private void Awake()
        {
            _instance = this;
            CreateTopPanelItemsUI();
            CreateBottomPanelItemsUI();
        }
        private void CreateTopPanelItemsUI()
        {
            for (int i = 0; i < GridObjectLoader.Instance.GridObjectManager.TopPanelItemsList.Count; i++)
            {
                var obj = GridObjectLoader.Instance.GridObjectManager.TopPanelItemsList[i];
                GameObject gameObject = Instantiate(PlaceItemPrefab, TopPanel);
                PlaceItem placeItem = gameObject.GetComponent<PlaceItem>();
                placeItem.SetItem(obj);
                placeItem.UpdateUI();
            }
        }
        private void CreateBottomPanelItemsUI()
        {
            for (int i = 0; i < GridObjectLoader.Instance.GridObjectManager.BottomPanelItemsList.Count; i++)
            {
                var obj = GridObjectLoader.Instance.GridObjectManager.BottomPanelItemsList[i];
                GameObject gameObject = Instantiate(PlaceItemPrefab, BottomPanel);
                PlaceItem placeItem = gameObject.GetComponent<PlaceItem>();
                placeItem.SetItem(obj);
                placeItem.UpdateUI();
            }
        }
        public void ClearActionPanelItems()
        {
            HelperFunctions.DestroyAllChildren(ActionPanel.gameObject);
        }
        public void CreateActionPanelItems(GridObject gridObject)
        {
            if (gridObject.complexGridObjects == null)
                return;

            for (int i = 0; i < gridObject.complexGridObjects.Count; i++)
            {
                GridObject complexObject = gridObject.complexGridObjects[i];
                GameObject gameObject = Instantiate(ActionItemPrefab, ActionPanel);
                ActionPanelItem placeItem = gameObject.GetComponent<ActionPanelItem>();
                placeItem.SetNameText(complexObject.displayName);
                placeItem.SetSprite(complexObject.editorPreview);
                placeItem.SetComplexObject(complexObject);
                placeItem.SetParentPanel(ActionPanel.transform as RectTransform);
                placeItem.SetSelectedObjectIndex(SelectedTileItemManager.ConstIndex);
            }
        }
    }
}