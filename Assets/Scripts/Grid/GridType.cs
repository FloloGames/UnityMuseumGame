using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Grid
{
    public enum GridType
    {
        EMPTY,//Boden Flur etc lauf area
        WALL, //Wand zerwt�rbar aber nicht am Anfang erst sp�ter dann wird es auch noch verh�rtete W�nde geben 
        SITTING_GUARD, //Normaler Sitzender W�chter 
        LAZY_GUARD,//Sitzender Schlafender w�chter
        MOVING_GUARD, // Bewegender W�chter zwischen zwei Punkten
        PAINTING, //Gem�lde
        STATUE,//statur oder antike shit

        //Not visible Types
        MOVEMENT_POINT,
    }
}

