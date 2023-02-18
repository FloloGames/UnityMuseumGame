using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Grid
{
    [CreateAssetMenu(fileName = "New Grid Object", menuName = "Grid")]
    public class GridObject : ScriptableObject
    {
        public new string name = "Empty";
        public GridType type = GridType.EMPTY;
        public Sprite editorPreview = null;

        public GameObject finishedPrefab = null;

        private void OnEnable()
        {
            if (editorPreview == null)
                editorPreview = Sprite.Create(CreateTexture(Color.white), new Rect(0, 0, 1, 1), new Vector2(0.5f, 0.5f), 1);
        }

        private static Texture2D CreateTexture(Color color)
        {
            Texture2D texture = new Texture2D(1, 1, TextureFormat.RGBA32, false);
            texture.SetPixel(0, 0, color);
            texture.Apply();
            return texture;
        }
    }
}
