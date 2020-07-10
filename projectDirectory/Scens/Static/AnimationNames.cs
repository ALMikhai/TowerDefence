namespace projectDirectory.Static
{
    public enum Character : int
    {
        ATTACK = 0,
        DEATH = 1,
        WALK = 2,
        IDLE = 3
    };
    public enum Shell : int
    {
        DEFAULT = 0,
        FALLING = 1
    }

    public static class AnimationNames
    {
        private static string[] _characterNames = { "Attack", "Death", "Walk", "Idle" };
        private static string[] _shellNames = { "Default", "Falling" };
        public static string GetCharacterAnimation(Character animation) => _characterNames[(int)animation];
        public static string GetShellAnimation(Shell animation) => _shellNames[(int)animation];
    }
}
