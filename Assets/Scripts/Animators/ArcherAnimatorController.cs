public static class ArcherAnimatorController
{
    public static class Params
    {
        public const string Shot = nameof(Shot);
        public const string GetArrow = nameof(GetArrow);
    }

    public static class States
    {
        public const string Idle = nameof(Idle);
        public const string Shot = nameof(Shot);
        public const string GetArrow = nameof(GetArrow);
        public const string TakeArrow = nameof(TakeArrow);
    }
}
