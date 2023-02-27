using Help;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// Can drag and drop the Item on the Grid to place it
/// </summary>
public class ActionPanelItem : MonoBehaviour, IDragHandler, IEndDragHandler, IDropHandler
{
    [SerializeField]
    private TMP_Text nameText;
    [SerializeField]
    private Image image;
    private int itemIndex;

    public void SetNameText(string name)
    {
        nameText.text = name;
    }
    public void SetSprite(Sprite sprite)
    {
        image.sprite = sprite;
    }
    public void SetItemIndex(int index)
    {
        itemIndex = index;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = HelperFunctions.GetCenterOfTouches();
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        transform.localPosition = Vector3.zero;
    }
    public void OnDrop(PointerEventData eventData)
    {

    }
}
