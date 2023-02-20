using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditMuseumSceneUIManager : MonoBehaviour
{
    public class PanelContainer
    {
        public string name;
        public RectTransform PanelTransform;
        public float OpenPos;
        public float ClosedPos;

        public void CloseSkip()
        {
            PanelTransform.anchoredPosition = new Vector3(0, ClosedPos, 0);
        }
        public void CloseAnimation()
        {
            float time = 0.5f;
            PanelTransform.LeanMoveY(ClosedPos, time).setEaseInBack();
        }
        public void CloseSkip()
        {

        }
    }

    private static EditMuseumSceneUIManager _instance;
    public static EditMuseumSceneUIManager Instance => _instance;

    private int SelectedPanelIndex;

    [SerializeField]
    private List<PanelContainer> Panels;



    private void Awake()
    {
        _instance = this;
        SetEveryPanelToClosed();
        SetEveryPanelObjectActive();
        //TopPanel.gameObject.SetActive(true);
        //BottomPanel.gameObject.SetActive(true);
        //TopPanel.anchoredPosition = new Vector3(0, TopPanelClosedPos, 0);
        //BottomPanel.anchoredPosition = new Vector3(0, BottomPanelClosedPos, 0);
        //selectedPanel = Panel.ACTION;
        OpenPanel("");
    }
    private void SetEveryPanelToClosed()
    {
        foreach (var panel in Panels)
        {
            panel.CloseSkip();
        }
    }
    private void SetEveryPanelObjectActive()
    {
        foreach (var panel in Panels)
        {
            panel.PanelTransform.gameObject.SetActive(true);
        }
    }
    private void ClosePanel(int index)
    {
        if (index >= Panels.Count)
        {
            Debug.LogError("Index Out of Bounce! " + index);
            return;
        }
        Panels[index].CloseSkip();
    }
    public void OpenPanel(string panelName)
    {
        float time = 0.5f;
        ClosePanel(SelectedPanelIndex);
        //Close curr selected
        //When finished open next Panel
        if (panel == Panel.PLACE)
        {
            BottomPanel.LeanMoveY(BottomPanelClosedPos, time).setEaseInBack().setOnComplete(() =>
            {
                TopPanel.LeanMoveY(TopPanelOpenPos, time).setEaseOutBack();
            });
        }
        else if (panel == Panel.ACTION)
        {
            TopPanel.LeanMoveY(TopPanelClosedPos, time).setEaseInBack().setOnComplete(() =>
            {
                BottomPanel.LeanMoveY(BottomPanelOpenPos, time).setEaseOutBack();
            });
        }
        selectedPanel = panel;
    }
}
