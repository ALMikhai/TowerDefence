namespace projectDirectory.Scens
{
    public enum Names : int
    {
        ATACK = 0,
        DEATH = 1,
        WALK = 2,
        IDLE = 3
    };
    public static class CharacterAnimationNames
    {

        private static string[] _names = { "Atack", "Death", "Walk", "Idle" };
        public static string GetAnimation(Names animation) => _names[(int)animation];
    }
}
