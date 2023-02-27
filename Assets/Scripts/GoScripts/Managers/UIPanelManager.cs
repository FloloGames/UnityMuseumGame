using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UIElements;

namespace Managers
{
    public class UIPanelManager : MonoBehaviour
    {
        [Serializable]
        public class PanelContainer
        {
            public static float animationTime = 0.5f;
            public string name;
            public RectTransform PanelTransform;
            public float OpenPos;
            public float ClosedPos;

            public void SetActive(bool value)
            {
                PanelTransform.gameObject.SetActive(value);
            }
            public void CloseSkip()
            {
                PanelTransform.anchoredPosition = new Vector3(0, ClosedPos, 0);
            }
            public IEnumerator CloseAsync(bool animation = false)
            {
                if (LeanTween.isTweening(PanelTransform))
                {
                    LeanTween.cancel(PanelTransform);
                }
                if (animation)
                {
                    TaskCompletionSource<object> taskCompletion = new TaskCompletionSource<object>();
                    PanelTransform.LeanMoveY(ClosedPos, animationTime).setEaseInBack().setOnComplete(() => { taskCompletion.SetResult(null); });
                    yield return new WaitUntil(() => taskCompletion.Task.IsCompleted);
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
            public IEnumerator OpenAsync(bool animation = false)
            {

                if (animation)
                {
                    TaskCompletionSource<object> taskCompletion = new TaskCompletionSource<object>();
                    PanelTransform.LeanMoveY(OpenPos, animationTime).setEaseOutBack().setOnComplete(() => { taskCompletion.SetResult(null); });
                    yield return new WaitUntil(() => taskCompletion.Task.IsCompleted);
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
            [HideInInspector]
            public int CurrSelectedPanel = -1;
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
            public IEnumerator CloseCurrAndOpenNewPanelAsync(string panelName)
            {
                int index = GetPanelIndexByName(panelName);
                if (index != -1 && CurrSelectedPanel != index)
                {
                    yield return CoroutineManager.Instance.StartCoroutine(CloseSelectedCurrPanel(true));
                    CurrSelectedPanel = index;
                    yield return CoroutineManager.Instance.StartCoroutine(OpenPanel(index, true));
                }
            }
            public IEnumerator ClosePanel(string panelName, bool animation)
            {
                int index = GetPanelIndexByName(panelName);
                if (index == -1)
                    yield return null;
                yield return ClosePanel(index, animation);
            }
            public IEnumerator ClosePanel(int index, bool animation = false)
            {
                if (index < Panels.Count && index >= 0)
                {
                    yield return CoroutineManager.Instance.StartCoroutine(Panels[index].CloseAsync(animation));
                }
            }
            public IEnumerator CloseSelectedCurrPanel(bool animation = false)
            {
                if (CurrSelectedPanel != -1)
                {
                    yield return CoroutineManager.Instance.StartCoroutine(ClosePanel(CurrSelectedPanel, animation));
                }
            }
            public IEnumerator OpenPanel(string panelName, bool animation = false)
            {
                int index = GetPanelIndexByName(panelName);
                if (index == -1)
                    yield return null;
                yield return OpenPanel(index, animation);
            }
            public IEnumerator OpenPanel(int index, bool animation = false)
            {
                if (index < Panels.Count && index >= 0)
                {
                    yield return CoroutineManager.Instance.StartCoroutine(Panels[index].OpenAsync(animation));
                }
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

        private static UIPanelManager _instance;
        public static UIPanelManager Instance => _instance;

        [SerializeField]
        public PanelContainerManager TopPanelsManager;
        [SerializeField]
        public PanelContainerManager BottomPanelsManager;

        private void Start()
        {
            _instance = this;
            TopPanelsManager.SetEveryPanelToClosedSkip();
            BottomPanelsManager.SetEveryPanelToClosedSkip();

            TopPanelsManager.SetEveryPanelObjectActive(true);
            BottomPanelsManager.SetEveryPanelObjectActive(true);
            OpenPanel(TOP_PANEL_NAME);
            OpenPanel(BOTTOM_PANEL_NAME);
        }
        public void OpenPanel(string panelName)
        {
            StartCoroutine(TopPanelsManager.CloseCurrAndOpenNewPanelAsync(panelName));
            StartCoroutine(BottomPanelsManager.CloseCurrAndOpenNewPanelAsync(panelName));
        }
    }
}