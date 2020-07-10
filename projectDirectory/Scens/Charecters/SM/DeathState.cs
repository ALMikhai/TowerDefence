using Godot;

namespace projectDirectory.Scens.Charecters.SM
{
    public class DeathState : State
    {
        public DeathState(Character character, StateMachine stateMachine) : base(character, stateMachine) { }

        public override void Enter()
        {
            _character.SetAnimation(projectDirectory.Static.Character.DEATH);
            _character.GetNode<CollisionShape2D>("CollisionShape2D").Disabled = true;
        }

        public override void AnimationFinished()
        {
            _character.QueueFree();
        }

        public override void Death() { }

        public override void SetTarget() { }
    }
}
