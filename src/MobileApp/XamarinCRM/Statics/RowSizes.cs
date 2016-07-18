

namespace XamarinCRM.Statics
{
    public static class RowSizes
    {
        public readonly static double LargeRowHeightDouble = 60;
        public readonly static double MediumRowHeightDouble = 44;
        public readonly static double SmallRowHeightDouble = 30;

        public static int LargeRowHeightInt { get { return (int)LargeRowHeightDouble; } }
        public static int MediumRowHeightInt { get { return (int)MediumRowHeightDouble; } }
        public static int SmallRowHeightInt { get { return (int)SmallRowHeightDouble; } }
    }
}

