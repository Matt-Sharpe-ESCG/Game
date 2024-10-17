
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

namespace Player
{
    public abstract class State
    {
        protected EnemyController player;
        protected StateMachine sm;

        // base constructor
        protected State(EnemyController player, StateMachine sm)
        {
            this.player = player;
            this.sm = sm;
        }

        // These methods are common to all states
        public virtual void Enter()
        {
            
        }

        public virtual void HandleInput()
        {
        }

        public virtual void LogicUpdate()
        {
        }

        public virtual void PhysicsUpdate()
        {
        }

        public virtual void Exit()
        {
        }

    }

}