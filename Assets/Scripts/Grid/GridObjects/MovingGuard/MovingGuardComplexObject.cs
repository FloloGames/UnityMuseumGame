using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Grid
{
    [CreateAssetMenu(fileName = "new MovingGuardComplexObject", menuName = "Grid Objects/Moving Guard Complex Object")]
    /// <summary>
    /// Complex Script for <see cref="GridType.MOVING_GUARD"/>
    /// <para>Type: <see cref="GridType.MOVEMENT_POINT"/></para>
    /// </summary>
    public class MovingGuardComplexObject : GridObject
    {
        //public new string type = GridType.MOVEMENT_POINT; does not work idk why
        private const string TargetPositionComplexObjectName = "TargetPosition";
        private Vector2Int mTargetPosition = Vector2Int.zero;
        public Vector2Int TargetPosition
        {
            get { return mTargetPosition; }
        }
        public override void OnItemSet(int i, int j, GridObject complexObject)
        {
            if (complexObject.name != TargetPositionComplexObjectName)
                return;
            mTargetPosition.Set(i, j);
        }
    }
}