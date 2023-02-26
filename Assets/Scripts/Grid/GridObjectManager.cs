using System.Collections.Generic;
using UnityEngine;

namespace Grid
{
    [CreateAssetMenu(fileName = "New Grid Manager", menuName = "GridManager")]
    public class GridObjectManager : ScriptableObject
    {
        public List<GridObject> TopPanelItemsList;
        public List<GridObject> BottomPanelItemsList;
    }
}