public static class EnemyArcherAnimatorController
{
    public static class Params
    {
        public const string Shot = nameof(Shot);
        public const string GetArrow = nameof(GetArrow);
        public const string Hold= nameof(Hold);
        public const string Release = nameof(Release);
    }

    public static class States
    {
        public const string Idle = nameof(Idle);
        public const string GetArrow = nameof(GetArrow);
        public const string Hold = nameof(Hold);
        public const string Release = nameof(Release);
        public const string TakeArrow = nameof(TakeArrow);
        public const string TakeDamage = nameof(TakeDamage);
        public const string Death = nameof(Death);
    }
}
