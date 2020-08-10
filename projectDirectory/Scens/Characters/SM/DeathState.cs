using Godot;

namespace projectDirectory.Scens.Charecters.SM
{
    public class DeathState : State
    {
        public DeathState(Character character, StateMachine stateMachine) : base(character, stateMachine) { }

        public override void Enter()
        {
            _character.SetAnimation(projectDirectory.Static.Character.DEATH);
            _character.CollisionLayer = 0;
            _character.CollisionMask = 0;
        }

        public override void AnimationFinished()
        {
            _character.CallDeferred("queue_free");
        }

        public override void Death() { }

        public override void SetTarget() { }
    }
}
