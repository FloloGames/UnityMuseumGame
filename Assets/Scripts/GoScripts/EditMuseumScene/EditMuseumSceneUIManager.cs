using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UIElements;

public class EditMuseumSceneUIManager : MonoBehaviour
{
    [Serializable]
    public class PanelContainer
    {
        public static float animationTime = 1.5f;
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
        public void SetActive(bool value)
        {
            PanelTransform.gameObject.SetActive(value);
        }
        public void CloseSkip()
        {
            PanelTransform.anchoredPosition = new Vector3(0, ClosedPos, 0);
        }
        public async Task CloseAsync(bool animation = false)
        {
            if (LeanTween.isTweening(PanelTransform))
            {
                LeanTween.cancel(PanelTransform);
            }
            if (animation)
            {
                TaskCompletionSource<object> taskCompletion = new TaskCompletionSource<object>();

                PanelTransform.LeanMoveY(ClosedPos, animationTime).setEaseInBack().setOnComplete(() => { taskCompletion.SetResult(null); });

                await taskCompletion.Task;
            }
            else
            {
                PanelTransform.anchoredPosition = new Vector3(0, ClosedPos, 0);
            }
        }
        public void OpenSkip()
        {
            PanelTransform.anchoredPosition = new Vector3(0, OpenPos, 0);

        }
        public async Task OpenAsync(bool animation = false)
        {
            if (animation)
            {
                TaskCompletionSource<object> taskCompletion = new TaskCompletionSource<object>();

                PanelTransform.LeanMoveY(OpenPos, animationTime).setEaseOutBack().setOnComplete(() => { taskCompletion.SetResult(null); });

                await taskCompletion.Task;

            }
            else
            {
                PanelTransform.anchoredPosition = new Vector3(0, OpenPos, 0);
            }
        }
    }
    [Serializable]
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
        public bool ContainsPanel(string panelName)
        {
            return GetPanelIndexByName(panelName) != -1;
        }
        public void SetEveryPanelObjectActive(bool value)
        {
            foreach (var panel in Panels)
            {
                panel.SetActive(value);
            }
        }
        public async Task CloseCurrAndOpenNewPanelAsync(string panelName)
        {
            int index = GetPanelIndexByName(panelName);
            if (index == -1)
                return;
            await ClosePanel(CurrSelectedPanel, true);
            CurrSelectedPanel = index;
            await OpenPanel(index);
        }
        public async Task ClosePanel(string panelName, bool animation)
        {
            int index = GetPanelIndexByName(panelName);
            if (index == -1)
                return;
            await ClosePanel(index, animation);
        }
        public async Task ClosePanel(int index, bool animation = false)
        {
            if (index >= Panels.Count)
            {
                Debug.LogError("Index Out of Bounce! " + index);
                return;
            }
            await Panels[index].CloseAsync(animation);
        }
        public async Task OpenPanel(string panelName, bool animation = false)
        {
            int index = GetPanelIndexByName(panelName);
            if (index == -1)
                return;
            await OpenPanel(index, animation);
        }
        public async Task OpenPanel(int index, bool animation = false)
        {
            if (index >= Panels.Count)
            {
                Debug.LogError("Index Out of Bounce! " + index);
                return;
            }
            await Panels[index].OpenAsync(animation);
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
    public PanelContainerManager TopPanelsManager;
    [SerializeField]
    public PanelContainerManager BottomPanelsManager;


    private void Awake()
    {
        _instance = this;
        TopPanelsManager.SetEveryPanelToClosedSkip();
        BottomPanelsManager.SetEveryPanelToClosedSkip();

        TopPanelsManager.SetEveryPanelObjectActive(true);
        BottomPanelsManager.SetEveryPanelObjectActive(true);
        OpenPanel(TOP_PANEL_NAME);
        //OpenPanel(BOTTOM_PANEL_NAME);
    }
    public void OpenPanel(string panelName)
    {
        StartCoroutine(OpenPanelRnumerator(panelName));
    }
    private IEnumerator OpenPanelRnumerator(string panelName)
    {
        Task t1 = TopPanelsManager.CloseCurrAndOpenNewPanelAsync(panelName);
        Task t2 = BottomPanelsManager.CloseCurrAndOpenNewPanelAsync(panelName);
        yield return new WaitUntil(() => t1.IsCompleted && t2.IsCompleted);
    }
    //public void OpenPanels(params string[] panelNames)
    //{
    //    int[] indices = new int[panelNames.Length];
    //    for (int i = 0; i < indices.Length; i++)
    //    {
    //        string panelName = panelNames[i];
    //        int index = GetPanelIndexByName(panelName);
    //        indices[i] = index;
    //    }


    //    ClosePanelsAnim(SelectedPanelIndices, OpenPanelAnim, index);
    //    SelectedPanelIndex = index;


    //    //if (panel == Panel.PLACE)
    //    //{
    //    //    BottomPanel.LeanMoveY(BottomPanelClosedPos, time).setEaseInBack().setOnComplete(() =>
    //    //    {
    //    //        TopPanel.LeanMoveY(TopPanelOpenPos, time).setEaseOutBack();
    //    //    });
    //    //}
    //    //else if (panel == Panel.ACTION)
    //    //{
    //    //    TopPanel.LeanMoveY(TopPanelClosedPos, time).setEaseInBack().setOnComplete(() =>
    //    //    {
    //    //        BottomPanel.LeanMoveY(BottomPanelOpenPos, time).setEaseOutBack();
    //    //    });
    //    //}
    //    //selectedPanel = panel;
    //}
}
