using Grid;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Grid
{
    [CreateAssetMenu(fileName = "new MovingGuardGridObject", menuName = "Grid Objects/Moving Guard Grid Object")]
    public class MovingGuardGridObject : GridObject
    {
        /// <summary>
        /// Waypoint for moving Objects e.g. Moving Officer
        /// </summary>
        public static readonly string MOVEMENT_POINT = "Movement_Point";
        //[HideInInspector]
        //public new string type = GridType.MOVING_GUARD; //does not work becuase of unity 
        Vector2Int targetPointPosition;

        public override void OnItemSet(int i, int j, GridObject complexObject)
        {
            //ComplexObject can only be TargetPosition 
            if (complexObject.type == MOVEMENT_POINT)
                return;

            targetPointPosition = new Vector2Int(i, j);

        }
    }
}