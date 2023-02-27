using Grid;
using Help;
using Managers;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// Can drag and drop the Item on the Grid to place it
/// </summary>
public class ActionPanelItem : MonoBehaviour, IDragHandler, IEndDragHandler, IDropHandler, IBeginDragHandler
{
    [SerializeField]
    private TMP_Text nameText;
    [SerializeField]
    private Image image;
    [SerializeField]
    private float animationTime = 0.5f;
    private RectTransform parentPanel;
    private Vector3 startDragPositon;
    private Vector3 normalSize;
    private bool isDragged;
    private bool isOnParentPanel;
    private int itemIndex;

    private void Awake()
    {
        normalSize = transform.localScale;
    }
    private void TestIfUILeftOrEnteredParentPanel()
    {
        bool isCurrOnParentPanel = RectTransformUtility.RectangleContainsScreenPoint(parentPanel, HelperFunctions.GetCenterOfTouches());
        //Debug.Log(isCurrOnParentPanel);
        if (isCurrOnParentPanel == isOnParentPanel)
            return;
        if (isCurrOnParentPanel)
        {
            Debug.Log("EnteredParentPanel");
            EnteredParentPanel();
        }
        else
        {
            Debug.Log("LeftParentPanel");
            LeftParentPanel();
        }
        isOnParentPanel = isCurrOnParentPanel;
    }
    private void EnteredParentPanel()
    {
        if (LeanTween.isTweening(gameObject))
            LeanTween.cancel(gameObject);
        Vector2 touchPosition = HelperFunctions.GetCenterOfTouches();
        SelectedTileItemManager.RemoveCurrentSpawnedItemTile();
        transform.LeanScale(normalSize, animationTime);
    }
    private void LeftParentPanel()
    {
        if (LeanTween.isTweening(gameObject))
            LeanTween.cancel(gameObject);
        Vector2 touchPosition = HelperFunctions.GetCenterOfTouches();
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(touchPosition);

        GridBuilder.Instance.Grid.WorldPositionToIndex(worldPosition, out int i, out int j);
        Vector2Int index = GridBuilder.Instance.Grid.ClampIndexIntoGrid(i, j);

        SelectedTileItemManager.SpawnNewSelectedItemTile(index.x, index.y);

        transform.LeanScale(Vector3.zero, animationTime);
    }
    private void UpdateSelectedItemTilePosition()
    {
        Vector2 touchPosition = HelperFunctions.GetCenterOfTouches();
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(touchPosition);

        GridBuilder.Instance.Grid.WorldPositionToIndex(worldPosition, out int i, out int j);
        Vector2Int index = GridBuilder.Instance.Grid.ClampIndexIntoGrid(i, j);

        SelectedTileItemManager.MoveCurrentTileItem(index.x, index.y);
    }
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
    public void OnBeginDrag(PointerEventData eventData)
    {
        isDragged = true;
        startDragPositon = transform.position;
    }
    public void SetParentPanel(RectTransform rectTransform)
    {
        parentPanel = rectTransform;
    }
    public void OnDrag(PointerEventData eventData)
    {
        transform.position = HelperFunctions.GetCenterOfTouches();
        TestIfUILeftOrEnteredParentPanel();
        UpdateSelectedItemTilePosition();
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        isDragged = false;
        transform.position = startDragPositon;
        EnteredParentPanel();
    }
    public void OnDrop(PointerEventData eventData)
    {

    }
}
