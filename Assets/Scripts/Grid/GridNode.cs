using System.Collections;
using System.Collections.Generic;
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
            GridObject = ScriptableObject.CreateInstance<GridObject>();
        }
        public GridNode(int i, int j, Color color, GridObject gridObject)
        {
            I = i;
            J = j;
            TintColor = color;
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
            return $"{I}:{J} {TintColor} {GridObject.name} {GridObject.type}";
        }
    }
}
