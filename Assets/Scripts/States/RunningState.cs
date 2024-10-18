
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
            player.animator.Play("Walk");
            player.agent.speed = 8;
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

            if (!player.agent.pathPending && player.agent.remainingDistance < 0.5f)
            {
                GoToNextPoint();
            }
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
        }

        void GoToNextPoint()
        {
            if (player.aiPoints.Length == 0)
            {
                return;
            }
            player.agent.destination = player.aiPoints[player.destPoint].position;
            player.destPoint = (player.destPoint + 1) % player.aiPoints.Length;
        }
    }
}