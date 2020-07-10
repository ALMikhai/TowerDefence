namespace projectDirectory.Scens.Charecters.SM
{
    public class IdleState : State
    {
        public IdleState(Character character, StateMachine stateMachine) : base(character, stateMachine) { }

        public override void Enter()
        {
            _character.SetAnimation(projectDirectory.Static.Character.IDLE);
        }
    }
}
