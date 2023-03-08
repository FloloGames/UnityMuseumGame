using UnityEngine;

namespace Grid
{
    public class GridNode
    {
        public readonly GridObject GridObject;
        public GameObject GameObject { get; set; }
        public Color TintColor { get; set; }
        public int I { get; }
        public int J { get; }

        public GridNode(int i, int j, Color color)
        {
            I = i;
            J = j;
            TintColor = color;
            GridObject = Help.HelperFunctions.CreateNormalGridObject();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <param name="gridObject"></param>
        /// <param name="color">default value is <see cref="Color.white"/></param>
        public GridNode(int i, int j, GridObject gridObject, Color? color = null)
        {
            color ??= Color.white;
            I = i;
            J = j;
            TintColor = (Color)color;
            GridObject = gridObject;
        }
        public void UpdateGameObject()
        {
            SpriteRenderer renderer = GameObject.GetComponent<SpriteRenderer>();
            renderer.sprite = GridObject.editorPreview;
            renderer.color = TintColor;
        }
        override
        public string ToString()
        {
            return $"{I}:{J} {TintColor} {GridObject.displayName} {GridObject.type}";
        }
    }
}
