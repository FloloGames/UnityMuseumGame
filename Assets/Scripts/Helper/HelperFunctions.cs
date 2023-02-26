using UnityEngine;

namespace Help
{

    /// <summary>
    /// Generell functions to Help development 
    /// </summary>
    public static class HelperFunctions
    {
        public static float Map(float OldValue, float OldMin, float OldMax, float NewMin, float NewMax)
        {

            float OldRange = OldMax - OldMin;
            float NewRange = NewMax - NewMin;
            float NewValue = (OldValue - OldMin) * NewRange / OldRange + NewMin;

            return NewValue;
        }
        public static GameObject CreateWorldText(string text, Vector3 position = default, Transform parent = null, int fontSize = 40, TextAnchor textAnchor = TextAnchor.MiddleCenter, TextAlignment textAlignment = TextAlignment.Center)
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
    }
}