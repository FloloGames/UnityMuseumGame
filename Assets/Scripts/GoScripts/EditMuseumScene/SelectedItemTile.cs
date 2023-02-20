using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedItemTile
{
    private static GameObject itemPrefab;
    private static GameObject CurrentSpawnedItem;
    private static Vector2 _index;
    public static Vector2 Index
    {
        get
        {
            return _index;
        }
        set
        {
            _index = value;
        }
    }
    public static void Init(GameObject prefab)
    {
        itemPrefab = prefab;
    }
    public static void SpawnNewSelectedItemTile(int i, int j)
    {
        if (itemPrefab == null)
            throw new System.Exception("Must Call SelectedItemTile.Init first!");

        _index.Set(i, j);

        Vector3 targetScale = new Vector3(GridBuilder.Instance.Grid.CellSize, GridBuilder.Instance.Grid.CellSize, 1);
        Vector3 position = GridBuilder.Instance.Grid.IndexToWorldPosition(i, j);
        position.y = 0.1f;

        float time = 0.5f;
        if (CurrentSpawnedItem != null)
        {
            LeanTween.scale(CurrentSpawnedItem, new Vector3(), time).setEaseInBack();
            GameObject.Destroy(CurrentSpawnedItem, time);
        }
        GameObject gameObject = GameObject.Instantiate(itemPrefab, position, Quaternion.Euler(90, 0, 0)) as GameObject;
        gameObject.transform.localScale = new Vector3();
        LeanTween.scale(gameObject, targetScale, time).setEaseOutBack();
        CurrentSpawnedItem = gameObject;
        EditMuseumSceneUIManager.Instance.OpenPanel(EditMuseumSceneUIManager.Panel.ACTION);
    }
    public static void RemoveCurrentSpawnedItemTile()
    {
        float time = 0.5f;
        if (CurrentSpawnedItem != null)
        {
            LeanTween.scale(CurrentSpawnedItem, new Vector3(), time).setEaseInBack();
            GameObject.Destroy(CurrentSpawnedItem, time);
        }
        EditMuseumSceneUIManager.Instance.OpenPanel(EditMuseumSceneUIManager.Panel.PLACE);
    }
    public static void ResetIndex()
    {
        _index.Set(-1, -1);
    }
}
