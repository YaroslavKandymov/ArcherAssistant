public static class ArcherAssistantAnimatorController 
{
    public static class Params
    {
        public const string TakeDamage = nameof(TakeDamage);
        public const string Speed = nameof(Speed);
        public const string Idle = nameof(Idle);
    }

    public static class States
    {
        public const string Idle = nameof(Idle);
        public const string TakeArrow = nameof(TakeArrow);
        public const string Run = nameof(Run);
        public const string GiveArrow = nameof(GiveArrow);
        public const string RunForward = nameof(RunForward);
    }
}
