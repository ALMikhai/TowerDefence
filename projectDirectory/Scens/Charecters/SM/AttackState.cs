namespace projectDirectory.Scens.Charecters.SM
{
    public class AttackState : State
    {
        public AttackState(Character character, StateMachine stateMachine) : base(character, stateMachine) { }

        public override void Enter()
        {
            _character.SetAnimation(projectDirectory.Static.Character.ATTACK);
            TargetConnect();
        }

        public override void Exit()
        {
            TargetDisconnect();
        }

        public override void PhysicsUpdate(float delta)
        {
            if (!_character.IsCloseToTarget())
            {
                _stateMachine.ChangeState(_character._moveState);
            }
        }

        public override void AnimationFinished()
        {
            _character.Attack();
        }
    }
}
