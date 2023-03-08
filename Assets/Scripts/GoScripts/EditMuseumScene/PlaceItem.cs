using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Grid;
using Managers;

/// <summary>
/// Script for PlaceItemPrefab (Button for GridObjects) not complex ones
/// </summary>
public class PlaceItem : MonoBehaviour
{
    [SerializeField]
    private TMP_Text nameText;
    [SerializeField]
    private TMP_Text priceText;
    [SerializeField]
    private Image image;
    private GridObject item;

    private void SetNameText(string name)
    {
        nameText.text = name;
    }
    private void SetPriceText(string price)
    {
        priceText.text = price;
    }
    private void SetSprite(Sprite sprite)
    {
        image.sprite = sprite;
    }
    public void SelectButtonPressed()
    {
        GridBuilder.SetSelectedPlaceItem(item);
        SelectedTileItemManager.RemoveConstItemTile();
        UIPanelManager.Instance.OpenPanel(UIPanelManager.TOP_PANEL_NAME);
    }
    public void SetItem(GridObject gridObject)
    {
        item = gridObject;
    }
    public void UpdateUI()
    {
        SetNameText(item.displayName);
        SetPriceText(item.price.ToString());
        SetSprite(item.editorPreviewUI);
    }

}
