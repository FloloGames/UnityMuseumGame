namespace Grid
{
    public enum GridType
    {
        EMPTY,//Boden Flur etc lauf area
        WALL, //Wand zerwt�rbar aber nicht am Anfang erst sp�ter dann wird es auch noch verh�rtete W�nde geben 
        /// <summary>
        /// When clicked hide other items aka the Bottonpanel
        /// <para>to select a Tile to see complex Items</para>
        /// </summary>
        SELECT,

        SITTING_GUARD, //Normaler Sitzender W�chter 
        LAZY_GUARD,//Sitzender Schlafender w�chter
        MOVING_GUARD, // Bewegender W�chter zwischen zwei Punkten
        PAINTING, //Gem�lde
        STATUE,//statur oder antike shit

        //Not visible Types
        /// <summary>
        /// Waypoint for moving Objects e.g. Moving Officer
        /// </summary>
        MOVEMENT_POINT,
        WHERE_TO_PLACE_PAINTING,
    }
}

