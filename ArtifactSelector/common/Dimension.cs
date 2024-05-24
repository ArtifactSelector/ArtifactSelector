namespace ArtifactSelector
{
    public static class Dimension
    {
        // Window size used to define the dimension
        public const double DEFAULT_WIDTH = 1280.0;
        public const double DEFAULT_HEIGHT = 720.0;

        // Size of artifact button
        public const double ARTI_BUTTON_WIDTH = 85.0;
        public const double ARTI_BUTTON_HEIGHT = 105.0;

        // Gap between artifact button
        public const double ARTI_BUTTON_GAP = 15.0;

        // Location of deselect button r elative to center of artifact button
        public const double RELATIVE_DESELECT_X = -30;
        public const double RELATIVE_DESELECT_Y = -42;

        // Location of first artifact
        public const double FIRST_ARTI_X = 40.0;
        public const double FIRST_ARTI_Y = 70.0;

        // Location of lock button
        public const double LOCK_X = 953.0;
        public const double LOCK_Y = 338.0;

        // Dimension of artifact card
        public readonly static RECT ARTI_CARD_RECT = new RECT(655, 120, 983, 666);
        // Dimension of components within artifact card relative to artifact card
        public readonly static RECT ARTI_SLOT_RECT = new RECT(18, 48, 128, 66);
        public readonly static RECT ARTI_MAINSTAT_RECT = new RECT(18, 105, 175, 120);
        public readonly static RECT ARTI_SUBSTAT_RECT = new RECT(35, 238, 264, 342);
        public readonly static RECT ARTI_SET_NAME_RECT = new RECT(16, 318, 280, 367);


        public static double ScaleX(double x, double target)
        {
            return x / DEFAULT_WIDTH * target;
        }

        public static double ScaleY(double y, double target)
        {
            return y / DEFAULT_HEIGHT * target;
        }

        public static RECT ScaleRect(RECT rect, double targetX, double targetY)
        {
            int left = (int)ScaleX(rect.Left, targetX);
            int top = (int)ScaleY(rect.Top, targetY);
            int right = (int)ScaleX(rect.Right, targetX);
            int bottom = (int)ScaleY(rect.Bottom, targetY);
            return new RECT(left, top, right, bottom);
        }
    }
}
