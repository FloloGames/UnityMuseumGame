using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Grid
{
    public enum GridType
    {
        EMPTY,//Boden Flur etc lauf area
        WALL, //Wand zerwt�rbar aber nicht am Anfang erst sp�ter dann wird es auch noch verh�rtete W�nde geben 
        GUARD, //Normaler W�chter 
        LAZY_GUARD,//Sleeping guard
        ART,//Gem�lde oder statur oder antike shit
    }
}

