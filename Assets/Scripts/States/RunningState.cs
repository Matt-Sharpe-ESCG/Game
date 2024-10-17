
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
namespace Player
{
    public class RunningState : State
    {
        // constructor
        public RunningState(EnemyController player, StateMachine sm) : base(player, sm)
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
            player.CheckForAttack();
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();

            // Get the distance to the player
            player.distance = Vector3.Distance(player.target.position, player.transform.position);

            // If inside the radius
            if (player.distance <= player.lookRadius)
            {
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
}