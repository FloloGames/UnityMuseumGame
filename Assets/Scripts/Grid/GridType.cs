namespace Grid
{
    public static class GridType
    {
        /// <summary>
        /// delete Object 
        /// </summary>
        public static readonly string EMPTY = "Empty";//Boden Flur etc lauf area

        public static readonly string WALL = "Wall"; //Wand zerwt�rbar aber nicht am Anfang erst sp�ter dann wird es auch noch verh�rtete W�nde geben 
        /// <summary>
        /// When clicked hide other items aka the TopPanel
        /// <para>to select a Tile to see complex Items</para>
        /// </summary>
        public static readonly string SELECT = "Emptyselect";

        public static readonly string SITTING_GUARD = "Sitting_Guard"; //Normaler Sitzender W�chter 
        public static readonly string LAZY_GUARD = "Lazy_Guard";//Sitzender Schlafender w�chter
        public static readonly string MOVING_GUARD = "Moving_Guard"; // Bewegender W�chter zwischen zwei Punkten
        public static readonly string PAINTING = "Painting"; //Gem�lde
        public static readonly string STATUE = "Statue";//statur oder antike shit

        //Not visible Types
        public static readonly string WHERE_TO_PLACE_PAINTING = "Where_To_Place_Painting";
    }
}

