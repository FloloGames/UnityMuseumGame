using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


namespace Grid
{
    public class GridSaver
    {
        private static GridSaver _instance;
        public static GridSaver Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new GridSaver();
                return _instance;
            }
        }

        public readonly GridObjectManager GridObjectManager;

        private GridSaver()
        {
            GridObjectManager = Resources.Load<GridObjectManager>("Grid Objects/GridObjectManager");
        }

        public int GetFirstPlaceItemIndexByType(GridType gridType)
        {
            for (int i = 0; i < GridObjectManager.PlaceItemsPanelList.Count; i++)
            {
                var placeItem = GridObjectManager.PlaceItemsPanelList[i];
                if (placeItem.type == gridType)
                {
                    return i;
                }
            }
            return -1;
        }
    }
}