using UnityEngine;


namespace Grid
{
    /// <summary>
    /// Loads the <see cref="GridObjectManager"/>
    /// <para>which contains all the <see cref="GridObject"/>s data</para>
    /// </summary>
    public class GridObjectLoader
    {
        private static readonly string PathToGridObjectManager = "GridObjectManager";

        private static GridObjectLoader _instance;
        public static GridObjectLoader Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new GridObjectLoader();
                return _instance;
            }
        }

        public readonly GridObjectManager GridObjectManager;

        private GridObjectLoader()
        {
            GridObjectManager = Resources.Load<GridObjectManager>(PathToGridObjectManager);
        }
        /// <summary>
        /// Gets the first Item index by the given Type
        /// </summary>
        /// <param name="gridType"></param>
        /// <returns>-1 if no <see cref="GridObject"/> was found or the <see cref="GridObjectManager"/> was null
        /// <para>otherwise the index of the Type</para>
        /// </returns>
        public int GetItemIndexByType(GridType gridType)
        {
            if (GridObjectManager == null)
                return -1;

            for (int i = 0; i < GridObjectManager.TopPanelItemsList.Count; i++)
            {
                var placeItem = GridObjectManager.TopPanelItemsList[i];
                if (placeItem.type == gridType)
                {
                    return i;
                }
            }
            return -1;
        }
    }
}