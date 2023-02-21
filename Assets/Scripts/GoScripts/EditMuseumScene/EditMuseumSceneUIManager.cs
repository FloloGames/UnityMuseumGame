using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UIElements;

public class EditMuseumSceneUIManager : MonoBehaviour
{
    public class PanelContainer
    {
        public string name;
        public RectTransform PanelTransform;
        public float OpenPos;
        public float ClosedPos;

        /*
            await LeanTween.moveX(gameObject, 5f, 1f).setEaseOutQuad().setOnComplete(() => {
                Debug.Log("Tween completed.");
            });
            Debug.Log("Next set of instructions.");
        */
        /// <summary>
        /// Closes Panel with or without animation
        /// </summary>
        /// <param name="OnComplete">if not given skip otherwise with animation</param>
        public void Close(Action OnComplete = null)
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
        public void Open(Action OnComplete = null)
        {
            if (OnComplete == null)
            {
                PanelTransform.anchoredPosition = new Vector3(0, OpenPos, 0);
            }
            else
            {
                float time = 0.5f;
                PanelTransform.LeanMoveY(OpenPos, time).setEaseOutBack().setOnComplete(OnComplete);
            }
        }
    }
    public class PanelContainerManager
    {
        public int CurrSelectedPanel = 0;
        public List<PanelContainer> Panels;
        public void SetEveryPanelToClosedSkip()
        {
            foreach (var panel in Panels)
            {
                panel.Close();
            }
        }
        public bool ContainsPanel(string panelName)
        {
            return GetPanelIndexByName(panelName) != -1;
        }
        public void SetEveryPanelObjectActive(bool value)
        {
            foreach (var panel in Panels)
            {
                panel.PanelTransform.gameObject.SetActive(value);
            }
        }
        public async Task CloseCurrAndOpenNewPanelAsync(string panelName)
        {
            int index = GetPanelIndexByName(panelName);
            if (index == -1)
                return;
            await ClosePanel(CurrSelectedPanel, () => { });
        }
        public void ClosePanel(string panelName, Action OnComplete = null)
        {
            int index = GetPanelIndexByName(panelName);
            if (index == -1)
                return;
            ClosePanel(index, OnComplete);
        }
        public async Task ClosePanel(int index, Action OnComplete = null)
        {
            if (index >= Panels.Count)
            {
                Debug.LogError("Index Out of Bounce! " + index);
                return;
            }
            if (OnComplete == null)
            {
                Panels[index].Close();
            }
            else
            {
                Panels[index].Close(OnComplete);
            }
        }
        public void OpenPanel(string panelName, Action OnComplete = null)
        {
            int index = GetPanelIndexByName(panelName);
            if (index == -1)
                return;
            OpenPanel(index, OnComplete);
        }
        public void OpenPanel(int index, Action OnComplete = null)
        {
            if (index >= Panels.Count)
            {
                Debug.LogError("Index Out of Bounce! " + index);
                return;
            }
            if (OnComplete == null)
                Panels[index].Open();
            else
                Panels[index].Open(OnComplete);

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
    }

    public static string ACTION_PANEL_NAME = "ACTION";
    public static string TOP_PANEL_NAME = "TOP";
    public static string BOTTOM_PANEL_NAME = "BOTTOM";

    private static EditMuseumSceneUIManager _instance;
    public static EditMuseumSceneUIManager Instance => _instance;

    [SerializeField]
    private PanelContainerManager TopPanelsManager;
    [SerializeField]
    private PanelContainerManager BottomPanelsManager;


    private void Awake()
    {
        _instance = this;
        TopPanelsManager.SetEveryPanelToClosedSkip();
        BottomPanelsManager.SetEveryPanelToClosedSkip();

        TopPanelsManager.SetEveryPanelObjectActive(true);
        BottomPanelsManager.SetEveryPanelObjectActive(true);
        OpenPanel("");
    }
    public void OpenPanel(string panelName)
    {
        if (TopPanelsManager.ContainsPanel(panelName))
        {
            TopPanelsManager.OpenPanel(ACTION_PANEL_NAME, );
            return;
        }
        if (BottomPanelsManager.ContainsPanel(panelName))
        {

        }
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
