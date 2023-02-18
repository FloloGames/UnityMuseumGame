using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Grid
{
    public class Grid
    {
        private readonly int _width, _height;
        private GridNode[,] _gridArray;
        private Vector3 _originPosition;
        private float _cellSize;

        public Grid(int width, int height, float cellSize, Vector3 originPosition)
        {
            _width = width;
            _height = height;
            _cellSize = cellSize;
            _originPosition = originPosition;
            _gridArray = new GridNode[width, height];
            for (int i = 0; i < _gridArray.GetLength(0); i++)
            {
                for (int j = 0; j < _gridArray.GetLength(1); j++)
                {

                    _gridArray[i, j] = new GridNode(i, j);
                }
            }
        }
        public void CreateWorldUI()
        {
            Color evenColor = new Color(0.21f, 0.21f, 0.21f, 1);
            Color oddColor = new Color(0.46f, 0.46f, 0.46f, 1);

            for (int i = 0; i < _gridArray.GetLength(0); i++)
            {
                for (int j = 0; j < _gridArray.GetLength(1); j++)
                {
                    Color color = (i + j) % 2 == 0 ? evenColor : oddColor;
                    GameObject go = CreateImageTile(i, j, color);
                    _gridArray[i, j].GameObject = go;
                    //CreateWorldText(i + ":" + j, IndexToWorldPosition(i, j));

                }
            }
        }
        private GameObject CreateImageTile(int i, int j, Color color)
        {
            Vector3 position = IndexToWorldPosition(i, j);
            GameObject gameObject = new GameObject($"ImgTile {i}:{j}", typeof(SpriteRenderer));
            Transform transform = gameObject.transform;
            transform.localScale = new Vector3(_cellSize, _cellSize, 1);
            transform.rotation = Quaternion.Euler(new Vector3(90, 0, 0));
            transform.localPosition = position;
            SpriteRenderer renderer = gameObject.GetComponent<SpriteRenderer>();
            GridNode node = _gridArray[i, j];
            renderer.sprite = node.GridObject.editorPreview;
            renderer.color = color;
            return gameObject;
        }
        private GameObject CreateWorldText(string text, Vector3 position = default, Transform parent = null, int fontSize = 40, TextAnchor textAnchor = TextAnchor.MiddleCenter, TextAlignment textAlignment = TextAlignment.Center)
        {
            GameObject gameObject = new("World_Text: " + text.Substring(0, Mathf.Min(5, text.Length)), typeof(TextMesh));
            Transform transform = gameObject.transform;
            transform.SetParent(parent);
            transform.rotation = Quaternion.Euler(new Vector3(90, 0, 0));
            transform.localPosition = position;
            TextMesh textMesh = gameObject.GetComponent<TextMesh>();
            textMesh.anchor = textAnchor;
            textMesh.alignment = textAlignment;
            textMesh.text = text;
            textMesh.font = default;
            textMesh.fontSize = fontSize;
            textMesh.color = Color.white;
            return gameObject;
        }
        private bool IndexInGrid(int i, int j)
        {
            return i >= 0 && j >= 0 && i < _width && j < _height;
        }
        public bool SetValue(int i, int j, GridNode value)
        {
            if (IndexInGrid(i, j))
            {
                _gridArray[i, j] = value;
                _gridArray[i, j].UpdateGameObject();
                return true;
            }
            return false;
        }
        public bool SetValue(Vector3 worldPos, GridNode value)
        {
            WorldPositionToIndex(worldPos, out int i, out int j);
            return SetValue(i, j, value);
        }
        public bool SetValueNotGameObject(int i, int j, GridNode value)
        {
            if (IndexInGrid(i, j))
            {
                GameObject go = _gridArray[i, j].GameObject;
                value.GameObject = go;
                _gridArray[i, j] = value;
                _gridArray[i, j].UpdateGameObject();
                return true;
            }
            return false;
        }
        public bool SetValueNotGameObject(Vector3 worldPos, GridNode value)
        {
            WorldPositionToIndex(worldPos, out int i, out int j);
            return SetValueNotGameObject(i, j, value);
        }
        public Vector3 IndexToWorldPosition(int i, int j)
        {
            return new Vector3(i, 0, j) * _cellSize + new Vector3(_cellSize / 2, 0, _cellSize / 2) + _originPosition;
        }
        public void WorldPositionToIndex(Vector3 worldPos, out int i, out int j)
        {
            i = Mathf.FloorToInt((worldPos - _originPosition).x / _cellSize);
            j = Mathf.FloorToInt((worldPos - _originPosition).z / _cellSize);
        }
        public GridNode GetValue(int i, int j)
        {
            if (IndexInGrid(i, j))
            {
                return _gridArray[i, j];
            }
            return null;
        }
        public GridNode GetValue(Vector3 worldPos)
        {
            WorldPositionToIndex(worldPos, out int i, out int j);
            return GetValue(i, j);
        }
    }
}