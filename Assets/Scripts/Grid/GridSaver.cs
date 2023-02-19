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
        public int IndexOfEmpty { get; }
        public readonly GridObject[] gridObjects;

        private GridSaver()
        {
            gridObjects = Resources.LoadAll<GridObject>("Grid Objects").Where(obj => obj.showInPlaceItemsPanel).ToArray();

            //Debug.Log($"Length {gridObjects.Length}");
            for (int i = 0; i < gridObjects.Length; i++)
            {
                GridObject o = gridObjects[i];
                if (o.type == GridType.EMPTY)
                    IndexOfEmpty = i;
                //Debug.Log(o.ToString());
            }

        }
        public int GetIndexOfGridObject(GridObject gridObject)
        {
            return -1;
        }

    }
}