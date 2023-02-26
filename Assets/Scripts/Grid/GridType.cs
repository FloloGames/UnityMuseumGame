namespace Grid
{
    public enum GridType
    {
        EMPTY,//Boden Flur etc lauf area
        WALL, //Wand zerwtörbar aber nicht am Anfang erst später dann wird es auch noch verhärtete Wände geben 
        /// <summary>
        /// When clicked hide other items aka the Bottonpanel
        /// <para>to select a Tile to see complex Items</para>
        /// </summary>
        SELECT,

        SITTING_GUARD, //Normaler Sitzender Wächter 
        LAZY_GUARD,//Sitzender Schlafender wächter
        MOVING_GUARD, // Bewegender Wächter zwischen zwei Punkten
        PAINTING, //Gemälde
        STATUE,//statur oder antike shit

        //Not visible Types
        /// <summary>
        /// Waypoint for moving Objects e.g. Moving Officer
        /// </summary>
        MOVEMENT_POINT,
        WHERE_TO_PLACE_PAINTING,
    }
}

