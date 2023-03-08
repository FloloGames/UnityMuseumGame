using System.Collections.Generic;
using UnityEngine;
using System;
using Managers;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Grid
{
    public abstract class GridObject : ScriptableObject
    {
        [HideInInspector]
        public List<Tuple<Vector2Int, GridObject>> childrenObjects = new List<Tuple<Vector2Int, GridObject>>();
        public bool showInPlaceItemsPanel = true;
        public string type = GridType.EMPTY;
        public PaymentType paymentType = PaymentType.ONCE;
        public string displayName = "Empty";
        public new string name = "Empty";
        public int price = 0;//difference between dayly or onetime
        public Sprite editorPreviewUI = null;
        public Sprite editorPreview = null;
        public GameObject finishedPrefab = null;
        /// <summary>
        /// if set to true you can add this object to otherGridObjects 
        /// this will then be shown to the user
        /// </summary>
        public bool isComplexObject = false;

        /// <summary>
        /// other GridObjecs which the user has to move to add the object
        /// </summary>
        public List<GridObject> complexGridObjects;

        public virtual void OnEnable()
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
        /// <summary>
        /// get Called when ActionItem (complexObject) gets dropt
        /// <para><see cref="ActionPanelItem.OnDrop(UnityEngine.EventSystems.PointerEventData)"/></para>
        /// </summary>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <param name="complexObject"></param>
        public virtual void OnItemSet(int i, int j, GridObject complexObject)
        {

        }
        public virtual void DrawChildren()
        {
            for (int i = 0; i < childrenObjects.Count; i++)
            {
                Vector2Int pos = childrenObjects[i].Item1;
                GridObject obj = childrenObjects[i].Item2;
                //GridBuilder.Instance.Grid
            }
        }
        /// <summary>
        /// Normal function Places the GridObject on the given Position on the given <see cref="Grid"/>
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="i"></param>
        /// <param name="j"></param>
        public virtual void PlaceOnGrid(Grid grid, int i, int j)
        {
            GridNode node = new GridNode(i, j, this, Color.white);
            grid.SetValueNotGameObject(i, j, node);
        }
        public override string ToString()
        {
            return $"{displayName} : {type}";
        }
    }

    //#if UNITY_EDITOR //only compile if its in the UnityEditor
    //    /// <summary>
    //    /// Class to manage the <see cref="GridObject"/> Inspector 
    //    /// </summary>
    //    [CustomEditor(typeof(GridObject))]
    //    public class GridObjectEditor : Editor
    //    {
    //        private SerializedProperty showInPlaceItemsPanel;
    //        private SerializedProperty complexGridObjects;
    //        private SerializedProperty isComplexObject;
    //        private SerializedProperty paymentType;
    //        private SerializedProperty finishedPrefab;
    //        private SerializedProperty type;
    //        private SerializedProperty editorPreview;
    //        private SerializedProperty displayName;
    //        private new SerializedProperty name;
    //        private SerializedProperty price;

    //        private bool showEverything = false;

    //        private void OnEnable()
    //        {
    //            showInPlaceItemsPanel = serializedObject.FindProperty("showInPlaceItemsPanel");
    //            complexGridObjects = serializedObject.FindProperty("complexGridObjects");
    //            isComplexObject = serializedObject.FindProperty("isComplexObject");
    //            paymentType = serializedObject.FindProperty("paymentType");
    //            finishedPrefab = serializedObject.FindProperty("finishedPrefab");
    //            type = serializedObject.FindProperty("type");
    //            editorPreview = serializedObject.FindProperty("editorPreview");
    //            displayName = serializedObject.FindProperty("displayName");
    //            name = serializedObject.FindProperty("name");
    //            price = serializedObject.FindProperty("price");
    //        }

    //        override
    //        public void OnInspectorGUI()
    //        {
    //            serializedObject.Update();


    //            showEverything = EditorGUILayout.Toggle("Show everything (for debug)", showEverything);


    //            //Erstell UI
    //            EditorGUILayout.LabelField("ACHTUNG nur weil manche Variablen nicht angezeigt werden heiﬂt es nicht,\n dass sie nicht existieren und einen aderen Wert als vorher haben.", GUILayout.Height(30));

    //            if (showEverything)
    //            {
    //                DrawDefaultInspector();
    //                return;
    //            }
    //            EditorGUILayout.PropertyField(showInPlaceItemsPanel);
    //            EditorGUILayout.PropertyField(type);

    //            if (!CheckEnumEqual(type, GridType.PAINTING) && !CheckEnumEqual(type, GridType.STATUE))
    //                EditorGUILayout.PropertyField(paymentType);

    //            EditorGUILayout.PropertyField(displayName);
    //            EditorGUILayout.PropertyField(name);

    //            //Wenn keine Statur und kein Gem‰lde ist dann kostet was weil es sonst schon gekauft/geklaut wurde
    //            if (!CheckEnumEqual(type, GridType.PAINTING) && !CheckEnumEqual(type, GridType.STATUE) && !CheckEnumEqual(paymentType, PaymentType.NONE))
    //                EditorGUILayout.PropertyField(price);

    //            EditorGUILayout.PropertyField(editorPreview);

    //            if (!CheckEnumEqual(type, GridType.EMPTY))
    //                EditorGUILayout.PropertyField(finishedPrefab);

    //            if (!CheckEnumEqual(type, GridType.PAINTING) && !CheckEnumEqual(type, GridType.STATUE))
    //                EditorGUILayout.PropertyField(isComplexObject);

    //            if (isComplexObject.boolValue)
    //                EditorGUILayout.PropertyField(complexGridObjects);

    //            serializedObject.ApplyModifiedProperties();

    //            //UpdateValues();
    //        }
    //        private void UpdateValues()
    //        {
    //            if (CheckEnumEqual(type, GridType.PAINTING) || CheckEnumEqual(type, GridType.STATUE))
    //                paymentType.enumValueIndex = GetEnumIndex(paymentType, PaymentType.ONCE);
    //            EditorGUILayout.PropertyField(name);

    //            //Wenn keine Statur und kein Gem‰lde ist dann kostet was weil es sonst schon gekauft/geklaut wurde
    //            if (!CheckEnumEqual(type, GridType.PAINTING) && !CheckEnumEqual(type, GridType.STATUE))
    //                EditorGUILayout.PropertyField(price);

    //            EditorGUILayout.PropertyField(editorPreview);

    //            if (!CheckEnumEqual(type, GridType.EMPTY))
    //                EditorGUILayout.PropertyField(finishedPrefab);

    //            if (!CheckEnumEqual(type, GridType.PAINTING) && !CheckEnumEqual(type, GridType.STATUE))
    //                EditorGUILayout.PropertyField(isComplexObject);

    //            if (isComplexObject.boolValue)
    //                EditorGUILayout.PropertyField(complexGridObjects);
    //        }
    //        private bool CheckEnumEqual<T>(SerializedProperty enum_, T targetEnumValue) where T : System.Enum
    //        {
    //            return enum_.enumDisplayNames[enum_.enumValueIndex] == targetEnumValue.ToString().ToUpper();
    //        }
    //        private int GetEnumIndex<T>(SerializedProperty enum_, T targetEnumValue) where T : System.Enum
    //        {
    //            for (int i = 0; i < paymentType.enumDisplayNames.Length; i++)
    //            {
    //                string name = paymentType.enumDisplayNames[i];
    //                if (targetEnumValue.ToString().ToUpper() == name)
    //                {
    //                    return i;
    //                }
    //            }
    //            return -1;
    //        }
    //    }
    //#endif

}
