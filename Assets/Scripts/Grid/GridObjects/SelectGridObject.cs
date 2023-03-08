using Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Grid
{
    [CreateAssetMenu(fileName = "new SelectGridObject", menuName = "Grid Objects/Select Grid Object")]
    public class SelectGridObject : GridObject
    {
        public override void PlaceOnGrid(Grid grid, int i, int j)
        {
            if (SelectedTileItemManager.ConstIndex.x == i && SelectedTileItemManager.ConstIndex.y == j)
            {
                SelectedTileItemManager.RemoveConstItemTile();
                SelectedTileItemManager.ResetIndex();
                UIPanelManager.Instance.OpenPanel(UIPanelManager.TOP_PANEL_NAME);
                return;
            }
            SelectedTileItemManager.SpawnNewSelectedItemTile(i, j);
            GridNode gridNode = grid.GetValue(i, j);
            CreatePanelItemsManger.Instance.ClearActionPanelItems();
            CreatePanelItemsManger.Instance.CreateActionPanelItems(gridNode.GridObject);
        }
    }
}