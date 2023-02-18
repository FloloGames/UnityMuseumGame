using Grid;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceItemPanelManger : MonoBehaviour
{
    [SerializeField]
    private Transform PlaceItemsPanel;
    [SerializeField]
    private GameObject PlaceItemPrefab;

    void Awake()
    {
        for (int i = 0; i < GridSaver.Instance.gridObjects.Length; i++)
        {
            var obj = GridSaver.Instance.gridObjects[i];
            GameObject gameObject = Instantiate(PlaceItemPrefab, PlaceItemsPanel);
            PlaceItem placeItem = gameObject.GetComponent<PlaceItem>();
            placeItem.SetNameText(obj.name);
            placeItem.SetPriceText(obj.price.ToString());
            placeItem.SetSprite(obj.editorPreview);
            placeItem.SetItemIndex(i);
        }
    }
}
