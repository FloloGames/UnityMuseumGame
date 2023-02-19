using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Grid
{
    public enum GridType
    {
        EMPTY,//Boden Flur etc lauf area
        WALL, //Wand zerwtörbar aber nicht am Anfang erst später dann wird es auch noch verhärtete Wände geben 
        SITTING_GUARD, //Normaler Sitzender Wächter 
        LAZY_GUARD,//Sitzender Schlafender wächter
        MOVING_GUARD, // Bewegender Wächter zwischen zwei Punkten
        PAINTING, //Gemälde
        STATUE,//statur oder antike shit

        //Not visible Types
        MOVEMENT_POINT,
    }
}

