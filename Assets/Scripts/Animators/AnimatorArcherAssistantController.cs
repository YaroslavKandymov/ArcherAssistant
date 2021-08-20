using UnityEngine;

public class AnimatorArcherAssistantController : MonoBehaviour
{
    public static class Params
    {
        public const string TakeDamage = nameof(TakeDamage);
        public const string TakeArrow = nameof(TakeArrow);
        public const string Speed = nameof(Speed);
    }

    public static class States
    {
        public const string TakeDamage = nameof(TakeDamage);
        public const string TakeArrow = nameof(TakeArrow);
        public const string Run = nameof(Run);
    }
}
