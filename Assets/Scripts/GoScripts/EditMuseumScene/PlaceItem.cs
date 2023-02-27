using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Grid;
using Managers;

public class PlaceItem : MonoBehaviour
{
    [SerializeField]
    private TMP_Text nameText;
    [SerializeField]
    private TMP_Text priceText;
    [SerializeField]
    private Image image;
    private int itemIndex;

    private List<GridObject> gridItems;

    public void SetNameText(string name)
    {
        nameText.text = name;
    }
    public void SetPriceText(string price)
    {
        priceText.text = price;
    }
    public void SetSprite(Sprite sprite)
    {
        image.sprite = sprite;
    }
    public void SetItemIndex(int index)
    {
        itemIndex = index;
    }
    public void SetGridItems(List<GridObject> gridObjects)
    {
        gridItems = gridObjects;
    }
    public void SelectButtonPressed()
    {
        GridObject gridObject = gridItems[itemIndex];
        GridBuilder.SetSelectedPlaceIndex(itemIndex, gridObject);
        SelectedTileItemManager.RemoveCurrentSpawnedItemTile();
        UIPanelManager.Instance.OpenPanel(UIPanelManager.TOP_PANEL_NAME);
    }
}
