using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Grid
{
    public enum GridType
    {
        EMPTY,//Boden Flur etc lauf area
        WALL, //Wand zerwtörbar aber nicht am Anfang erst später dann wird es auch noch verhärtete Wände geben 
        GUARD, //Normaler Wächter 
        LAZY_GUARD,//Sleeping guard
        ART,//Gemälde oder statur oder antike shit
    }
}

