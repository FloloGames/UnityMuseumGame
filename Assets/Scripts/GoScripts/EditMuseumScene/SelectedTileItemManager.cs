using Grid;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Managers
{

    /// <summary>
    /// Manages the Current selected Cell and UI with animation
    /// <para>Also opens and closes The <see cref="UIPanelManager.Instance"/> Panel</para>
    /// </summary>
    public class SelectedTileItemManager
    {
        private static GameObject itemPrefab;
        private static GameObject CurrentSpawnedItem;
        private static Vector2Int _index;
        private static int currentMovingId;

        public static Vector2Int Index
        {
            get
            {
                return _index;
            }
            set
            {
                _index = value;
            }
        }
        public static void Init(GameObject prefab)
        {
            itemPrefab = prefab;
        }
        public static void SpawnNewSelectedItemTile(int i, int j)
        {
            if (itemPrefab == null)
                throw new System.Exception("Must Call SelectedItemTile.Init first!");

            _index.Set(i, j);

            Vector3 targetScale = new Vector3(GridBuilder.Instance.Grid.CellSize, GridBuilder.Instance.Grid.CellSize, 1);
            Vector3 position = GridBuilder.Instance.Grid.IndexToWorldPosition(i, j);
            position.y = 0.1f;

            float time = 0.5f;
            if (CurrentSpawnedItem != null)
            {
                LeanTween.scale(CurrentSpawnedItem, new Vector3(), time).setEaseInBack();
                Object.Destroy(CurrentSpawnedItem, time);
            }
            GameObject gameObject = Object.Instantiate(itemPrefab, position, Quaternion.Euler(90, 0, 0)) as GameObject;
            gameObject.transform.localScale = new Vector3();
            LeanTween.scale(gameObject, targetScale, time).setEaseOutBack();
            CurrentSpawnedItem = gameObject;
            UIPanelManager.Instance.OpenPanel(UIPanelManager.ACTION_PANEL_NAME);
        }
        public static void RemoveCurrentSpawnedItemTile()
        {
            float time = 0.5f;
            if (CurrentSpawnedItem != null)
            {
                LeanTween.scale(CurrentSpawnedItem, new Vector3(), time).setEaseInBack();
                Object.Destroy(CurrentSpawnedItem, time);
            }
        }
        public static void ResetIndex()
        {
            _index.Set(-1, -1);
        }
        public static void MoveCurrentTileItem(int i, int j)
        {
            if (CurrentSpawnedItem == null)
                return;

            if (currentMovingId != -1 && LeanTween.isTweening(currentMovingId))
            {
                LeanTween.cancel(currentMovingId);
                currentMovingId = -1;
            }
            _index.Set(i, j);
            float animTime = 0.25f;
            Vector3 targetPosition = GridBuilder.Instance.Grid.IndexToWorldPosition(i, j);
            targetPosition.y = 1f;
            currentMovingId = CurrentSpawnedItem.transform.LeanMove(targetPosition, animTime).setEaseOutCubic().setOnComplete(() => { currentMovingId = -1; }).id;
        }
    }
}