using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Grid
{
    /// <summary>
    /// Complex Script for <see cref="GridType.MOVING_GUARD"/>
    /// <para>Type: <see cref="GridType.MOVEMENT_POINT"/></para>
    /// </summary>
    public class MovingGuardComplexObject : ComplexGridObjectInterface
    {
        private const string TargetPositionComplexObjectName = "TargetPosition";
        private Vector2Int targetPosition = Vector2Int.zero;
        public Vector2Int TargetPosition
        {
            get { return targetPosition; }
        }
        public override void OnItemSet(int i, int j, GridObject complexObject)
        {
            if (complexObject.name != TargetPositionComplexObjectName)
                return;
            targetPosition.Set(i, j);
        }
    }
}