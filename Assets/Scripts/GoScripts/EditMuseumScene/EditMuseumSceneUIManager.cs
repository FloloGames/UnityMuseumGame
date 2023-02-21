using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EditMuseumSceneUIManager : MonoBehaviour
{
    public class PanelContainerManager
    {
        public int CurrSelectedPanel = 0;
        public List<PanelContainer> Panels;
        public void SetEveryPanelToClosedSkip()
        {
            foreach (var panel in Panels)
            {
                panel.CloseSkip();
            }
        }
    }
    public class PanelContainer
    {
        public string name;
        public RectTransform PanelTransform;
        public float OpenPos;
        public float ClosedPos;

        /*
         public async void MyMethod()
        {
            await LeanTween.moveX(gameObject, 5f, 1f).setEaseOutQuad().setOnComplete(() => {
                Debug.Log("Tween completed.");
            });

            Debug.Log("Next set of instructions.");
        }
         */
        public void CloseSkip(System.Action OnComplete = null)
        {
            if (OnComplete == null)
            {
                PanelTransform.anchoredPosition = new Vector3(0, ClosedPos, 0);
            }
            else
            {
                float time = 0.5f;
                PanelTransform.LeanMoveY(ClosedPos, time).setEaseInBack().setOnComplete(OnComplete);
            }


        }
        public void CloseAnimation(System.Action OnComplete)
        {

        }
        public void OpenSkip()
        {
            PanelTransform.anchoredPosition = new Vector3(0, OpenPos, 0);
        }
        public void OpenAnimation(System.Action OnComplete)
        {
            float time = 0.5f;
            PanelTransform.LeanMoveY(OpenPos, time).setEaseOutBack().setOnComplete(OnComplete);
        }
    }
    public static string ACTION_PANEL_NAME = "ACTION";
    public static string TOP_PANEL_NAME = "TOP";
    public static string BOTTOM_PANEL_NAME = "BOTTOMM";

    private static EditMuseumSceneUIManager _instance;
    public static EditMuseumSceneUIManager Instance => _instance;

    private List<int> SelectedPanelIndices;

    [SerializeField]
    private PanelContainerManager TopPanels;
    [SerializeField]
    private PanelContainerManager BottomPanels;


    private void Awake()
    {
        _instance = this;
        SetEveryPanelToClosed();
        SetEveryPanelObjectActive();
        OpenPanel("");
    }

    private void SetEveryPanelObjectActive()
    {
        foreach (var panel in Panels)
        {
            panel.PanelTransform.gameObject.SetActive(true);
        }
    }
    private void ClosePanelSkip(int index)
    {
        if (index >= Panels.Count)
        {
            Debug.LogError("Index Out of Bounce! " + index);
            return;
        }
        Panels[index].CloseSkip();
    }
    private void ClosePanelAnim<T>(int index, System.Action<T> OnComplete, T value)
    {
        if (index >= Panels.Count)
        {
            Debug.LogError("Index Out of Bounce! " + index);
            return;
        }
        Panels[index].CloseAnimation(() => { OnComplete(value); });
    }
    private int GetPanelIndexByName(string panelName)
    {
        for (int i = 0; i < Panels.Count; i++)
        {
            var panel = Panels[i];
            if (panel.name == panelName)
            {
                return i;
            }
        }
        return -1;
    }
    public void OpenPanelsAnim(int[] indices)
    {
        for (int i = 0; i < indices.Length; i++)
        {
            int index = indices[i];
            if (index >= Panels.Count)
            {
                Debug.LogError("Index Out of Bounce! " + index);
                return;
            }
            Panels[index].OpenAnimation(null);
        }
    }
    public void OpenPanelAnim(int index, System.Action OnComplete)
    {
        if (index >= Panels.Count)
        {
            Debug.LogError("Index Out of Bounce! " + index);
            return;
        }
        Panels[index].OpenAnimation(OnComplete);
    }
    public void OpenPanel(string panelName)
    {
        OpenPanels(new[] { panelName });
    }
    public void OpenPanels(params string[] panelNames)
    {
        int[] indices = new int[panelNames.Length];
        for (int i = 0; i < indices.Length; i++)
        {
            string panelName = panelNames[i];
            int index = GetPanelIndexByName(panelName);
            indices[i] = index;
        }


        ClosePanelsAnim(SelectedPanelIndices, OpenPanelAnim, index);
        SelectedPanelIndex = index;


        //if (panel == Panel.PLACE)
        //{
        //    BottomPanel.LeanMoveY(BottomPanelClosedPos, time).setEaseInBack().setOnComplete(() =>
        //    {
        //        TopPanel.LeanMoveY(TopPanelOpenPos, time).setEaseOutBack();
        //    });
        //}
        //else if (panel == Panel.ACTION)
        //{
        //    TopPanel.LeanMoveY(TopPanelClosedPos, time).setEaseInBack().setOnComplete(() =>
        //    {
        //        BottomPanel.LeanMoveY(BottomPanelOpenPos, time).setEaseOutBack();
        //    });
        //}
        //selectedPanel = panel;
    }
}
