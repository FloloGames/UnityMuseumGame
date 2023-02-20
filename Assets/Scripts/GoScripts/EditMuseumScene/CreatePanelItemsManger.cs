using Grid;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    void Awake()
    {
        for (int i = 0; i < GridSaver.Instance.GridObjectManager.PlaceItemsPanelList.Count; i++)
        {
            var obj = GridSaver.Instance.GridObjectManager.PlaceItemsPanelList[i];
            GameObject gameObject = Instantiate(PlaceItemPrefab, TopPanel);
            PlaceItem placeItem = gameObject.GetComponent<PlaceItem>();
            placeItem.SetNameText(obj.name);
            placeItem.SetPriceText(obj.price.ToString());
            placeItem.SetSprite(obj.editorPreview);
            placeItem.SetItemIndex(i);
        }
        for (int i = 0; i < GridSaver.Instance.GridObjectManager.ActionItemsPanelList.Count; i++)
        {
            var obj = GridSaver.Instance.GridObjectManager.ActionItemsPanelList[i];
            GameObject gameObject = Instantiate(PlaceItemPrefab, BottomPanel);
            PlaceItem placeItem = gameObject.GetComponent<PlaceItem>();
            placeItem.SetNameText(obj.name);
            placeItem.SetPriceText(obj.price.ToString());
            placeItem.SetSprite(obj.editorPreview);
            placeItem.SetItemIndex(i);
        }
    }
}
