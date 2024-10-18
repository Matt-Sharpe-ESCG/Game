
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
namespace Player
{
    public class AttackState : State
    {
        // constructor
        public AttackState(EnemyController player, StateMachine sm) : base(player, sm)
        {
        }

        public override void Enter()
        {
            player.animator.Play("Running");
            base.Enter();
        }

        public override void Exit()
        {

            base.Exit();
        }

        public override void HandleInput()
        {
            base.HandleInput();
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            player.CheckForIdle();
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();

            // Move towards the player
            player.agent.SetDestination(player.target.position);

            if (player.distance <= player.agent.stoppingDistance)
            {
                // Attack
                player.FaceTarget();
            }
        }
    }
}