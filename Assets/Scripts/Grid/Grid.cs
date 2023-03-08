using Help;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Grid
{
    /// <summary>
    /// The Grid class which holds everything for the grid 
    /// </summary>
    public class Grid
    {
        public readonly Color EvenColor = new Color(0.21f, 0.21f, 0.21f, 1);
        public readonly Color EddColor = new Color(0.46f, 0.46f, 0.46f, 1);

        /// <summary>
        /// List which holds every foatingGameObject
        /// </summary>
        private List<GameObject> mFloatingObjects = new List<GameObject>();

        private readonly int mWidth, mHeight;
        private GridNode[,] mGridArray;
        private Vector3 mOriginPosition;

        private float mCellSize;
        public float CellSize => mCellSize;

        public Grid(int width, int height, float cellSize, Vector3 originPosition)
        {
            mWidth = width;
            mHeight = height;
            mCellSize = cellSize;
            mOriginPosition = originPosition;
            mGridArray = new GridNode[width, height];
            for (int i = 0; i < mGridArray.GetLength(0); i++)
            {
                for (int j = 0; j < mGridArray.GetLength(1); j++)
                {

                    Color color = GetNodeColor(i, j);
                    mGridArray[i, j] = new GridNode(i, j, color);
                }
            }
        }
        public void CreateWorldUI()
        {
            Transform parent = new GameObject("GridNodesHolder").transform;
            for (int i = 0; i < mGridArray.GetLength(0); i++)
            {
                for (int j = 0; j < mGridArray.GetLength(1); j++)
                {
                    GameObject go = CreateImageTile(i, j, parent);
                    mGridArray[i, j].GameObject = go;
                    mGridArray[i, j].UpdateGameObject();
                    //CreateWorldText(i + ":" + j, IndexToWorldPosition(i, j));
                }
            }
        }
        private GameObject CreateImageTile(int i, int j, Transform parent)
        {
            Vector3 position = IndexToWorldPosition(i, j);
            GameObject gameObject = new GameObject($"ImgTile {i}:{j}", typeof(SpriteRenderer));
            Transform transform = gameObject.transform;
            transform.parent = parent;
            transform.localScale = new Vector3(mCellSize, mCellSize, 1);
            transform.rotation = Quaternion.Euler(new Vector3(90, 0, 0));
            transform.localPosition = position;
            return gameObject;
        }
        public bool IndexInGrid(int i, int j)
        {
            return i >= 0 && j >= 0 && i < mWidth && j < mHeight;
        }
        public bool SetValue(int i, int j, GridNode value)
        {
            if (IndexInGrid(i, j))
            {

                if (value.GridObject.type == GridType.EMPTY)
                {
                    Color tintColor = GetNodeColor(i, j);
                    value.TintColor = tintColor;
                }
                mGridArray[i, j] = value;
                mGridArray[i, j].UpdateGameObject();
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
                GameObject go = mGridArray[i, j].GameObject;
                value.GameObject = go;
                if (value.GridObject.type == GridType.EMPTY)
                {
                    Color tintColor = GetNodeColor(i, j);
                    value.TintColor = tintColor;
                }
                mGridArray[i, j] = value;
                mGridArray[i, j].UpdateGameObject();
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
            return new Vector3(i, 0, j) * mCellSize + new Vector3(mCellSize / 2, 0, mCellSize / 2) + mOriginPosition;
        }
        public void WorldPositionToIndex(Vector3 worldPos, out int i, out int j)
        {
            i = Mathf.FloorToInt((worldPos - mOriginPosition).x / mCellSize);
            j = Mathf.FloorToInt((worldPos - mOriginPosition).z / mCellSize);
        }
        public GridNode GetValue(int i, int j)
        {
            if (IndexInGrid(i, j))
            {
                return mGridArray[i, j];
            }
            return null;
        }
        public GridNode GetValue(Vector3 worldPos)
        {
            WorldPositionToIndex(worldPos, out int i, out int j);
            return GetValue(i, j);
        }
        public Color GetNodeColor(int i, int j)
        {
            return (i + j) % 2 == 0 ? EvenColor : EddColor;
        }
        public Vector2Int ClampIndexIntoGrid(int i, int j)
        {
            int _i = Mathf.Clamp(i, 0, mGridArray.GetLength(0) - 1);
            int _j = Mathf.Clamp(j, 0, mGridArray.GetLength(1) - 1);
            return new Vector2Int(_i, _j);
        }
        public void CallComplexGridScript(int i, int j)
        {

        }
        /// <summary>
        /// Creates a "Floating Object" its not saved in the Grid array but shown in the GridUI
        /// </summary>
        public void CreateFloatingObject(Vector2Int pos, GridObject gridObject)
        {
            WorldPositionToIndex(new Vector3(pos.x, pos.y, 0), out int i, out int j);
            GridNode node = new GridNode(i, j, gridObject, Color.white);
        }
        /// <summary>
        /// Creates a line between the given Points
        /// </summary>
        /// <param name="points"></param>
        public void CreateLine(Vector2Int[] points)
        {

        }
        //public Vector2 GetScreenCellSize(Vector3 worldPosition)
        //{
        //    WorldPositionToIndex(worldPosition, out int i, out int j);

        //    Debug.Log("Index: " + i + " " + j);

        //    return GetScreenCellSize(i, j);
        //}
        //public Vector2 GetScreenCellSize(int i, int j)
        //{
        //    if (!IndexInGrid(i, j))
        //        return Vector2.zero;
        //    Camera mainCamera = Camera.main;
        //    Vector3 screenPos = mainCamera.WorldToScreenPoint(_gridArray[i, j].GameObject.transform.position);
        //    // Calculate the pixel width and height of the object
        //    float pixelWidth = 5f * Screen.width / mainCamera.orthographicSize / screenPos.z;
        //    float pixelHeight = 5f * Screen.height / mainCamera.orthographicSize / screenPos.z;

        //    Debug.Log("Pixel size: " + pixelWidth + " x " + pixelHeight);

        //    return new Vector2(pixelWidth, pixelHeight);
        //}
    }
}