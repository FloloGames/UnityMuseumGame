using Grid;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Creates all UI Items
/// <para>uses the <see cref="GridObjectLoader.Instance"/> to get all the Data</para>
/// </summary>
public class CreatePanelItemsManger : MonoBehaviour
{
    [SerializeField]
    private Transform TopPanel;
    [SerializeField]
    private Transform ActionsPanel;
    [SerializeField]
    private Transform BottomPanel;
    [SerializeField]
    private GameObject PlaceItemPrefab;

    private void Awake()
    {
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
            placeItem.SetNameText(obj.name);
            placeItem.SetPriceText(obj.price.ToString());
            placeItem.SetSprite(obj.editorPreview);
            placeItem.SetItemIndex(i);
        }
    }
    private void CreateBottomPanelItemsUI()
    {
        for (int i = 0; i < GridObjectLoader.Instance.GridObjectManager.BottomPanelItemsList.Count; i++)
        {
            var obj = GridObjectLoader.Instance.GridObjectManager.BottomPanelItemsList[i];
            GameObject gameObject = Instantiate(PlaceItemPrefab, BottomPanel);
            PlaceItem placeItem = gameObject.GetComponent<PlaceItem>();
            placeItem.SetNameText(obj.name);
            placeItem.SetPriceText(obj.price.ToString());
            placeItem.SetSprite(obj.editorPreview);
            placeItem.SetItemIndex(i);
        }
    }
}
