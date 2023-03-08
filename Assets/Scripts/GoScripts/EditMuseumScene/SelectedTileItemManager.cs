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
        private static GameObject mItemPrefab;
        private static GameObject mSpawnedConstItem;
        private static GameObject mSpawnedMovingItem;
        private static int mCurrentMovingId;

        private static Vector2Int mMovingIndex;
        private static Vector2Int MovingIndex
        {
            get { return mMovingIndex; }
            set { mMovingIndex = value; }
        }

        private static Vector2Int mConstIndex;
        public static Vector2Int ConstIndex
        {
            get { return mConstIndex; }
            set { mConstIndex = value; }
        }
        public static void Init(GameObject prefab)
        {
            mItemPrefab = prefab;
        }
        public static void SpawnNewSelectedItemTile(int i, int j)
        {
            if (mItemPrefab == null)
                throw new System.Exception("Must Call SelectedItemTile.Init first!");

            mConstIndex.Set(i, j);

            Vector3 targetScale = new Vector3(GridBuilder.Instance.Grid.CellSize, GridBuilder.Instance.Grid.CellSize, 1);
            Vector3 position = GridBuilder.Instance.Grid.IndexToWorldPosition(i, j);
            position.y = 0.1f;

            float time = 0.5f;
            if (mSpawnedConstItem != null)
            {
                LeanTween.scale(mSpawnedConstItem, new Vector3(), time).setEaseInBack();
                Object.Destroy(mSpawnedConstItem, time);
            }
            GameObject gameObject = Object.Instantiate(mItemPrefab, position, Quaternion.Euler(90, 0, 0)) as GameObject;
            gameObject.transform.localScale = new Vector3();
            LeanTween.scale(gameObject, targetScale, time).setEaseOutBack();
            mSpawnedConstItem = gameObject;
            UIPanelManager.Instance.OpenPanel(UIPanelManager.ACTION_PANEL_NAME);
        }
        public static void RemoveConstItemTile()
        {
            float time = 0.5f;
            if (mSpawnedConstItem != null)
            {
                LeanTween.scale(mSpawnedConstItem, new Vector3(), time).setEaseInBack();
                Object.Destroy(mSpawnedConstItem, time);
            }
        }
        public static void RemoveMovingItemTile()
        {
            float time = 0.5f;
            if (mSpawnedConstItem != null)
            {
                LeanTween.scale(mSpawnedConstItem, new Vector3(), time).setEaseInBack();
                Object.Destroy(mSpawnedConstItem, time);
            }
        }
        public static void ResetIndex()
        {
            mConstIndex.Set(-1, -1);
        }
        public static void MoveCurrentTileItem(int i, int j)
        {
            if (mSpawnedConstItem == null)
                return;

            if (mCurrentMovingId != -1 && LeanTween.isTweening(mCurrentMovingId))
            {
                LeanTween.cancel(mCurrentMovingId);
                mCurrentMovingId = -1;
            }
            mMovingIndex.Set(i, j);
            float animTime = 0.25f;
            Vector3 targetPosition = GridBuilder.Instance.Grid.IndexToWorldPosition(i, j);
            targetPosition.y = 1f;
            mCurrentMovingId = mSpawnedConstItem.transform.LeanMove(targetPosition, animTime).setEaseOutCubic().setOnComplete(() => { mCurrentMovingId = -1; }).id;
        }
    }
}