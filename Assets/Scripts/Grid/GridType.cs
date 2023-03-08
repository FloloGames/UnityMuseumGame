namespace Grid
{
    public static class GridType
    {
        /// <summary>
        /// delete Object 
        /// </summary>
        public static readonly string EMPTY = "Empty";//Boden Flur etc lauf area

        public static readonly string WALL = "Wall"; //Wand zerwtörbar aber nicht am Anfang erst später dann wird es auch noch verhärtete Wände geben 
        /// <summary>
        /// When clicked hide other items aka the TopPanel
        /// <para>to select a Tile to see complex Items</para>
        /// </summary>
        public static readonly string SELECT = "Emptyselect";

        public static readonly string SITTING_GUARD = "Sitting_Guard"; //Normaler Sitzender Wächter 
        public static readonly string LAZY_GUARD = "Lazy_Guard";//Sitzender Schlafender wächter
        public static readonly string MOVING_GUARD = "Moving_Guard"; // Bewegender Wächter zwischen zwei Punkten
        public static readonly string PAINTING = "Painting"; //Gemälde
        public static readonly string STATUE = "Statue";//statur oder antike shit

        //Not visible Types
        public static readonly string WHERE_TO_PLACE_PAINTING = "Where_To_Place_Painting";
    }
}

