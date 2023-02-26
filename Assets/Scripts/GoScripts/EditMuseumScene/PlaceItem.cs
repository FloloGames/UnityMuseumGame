using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Grid;

public class PlaceItem : MonoBehaviour
{
    [SerializeField]
    private TMP_Text nameText;
    [SerializeField]
    private TMP_Text priceText;
    [SerializeField]
    private Image image;
    private int itemIndex;

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
    public void SelectButtonPressed()
    {
        GridBuilder.SetSelectedPlaceIndex(itemIndex);
        SelectedItemTile.RemoveCurrentSpawnedItemTile();
    }
}
