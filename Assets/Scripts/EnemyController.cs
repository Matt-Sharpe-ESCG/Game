using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.VFX;

namespace Player
{
	public class EnemyController : MonoBehaviour
	{
		public Transform[] aiPoints;
		public int destPoint;

		public float lookRadius = 10f;
		public float distance;
		public float speed;

		public Transform target;
		public NavMeshAgent agent;
		public GameObject player;

		public StateMachine sm;
		public Animator animator;

		// variables holding the different player states
		public IdleState idleState;
		public RunningState runningState;
		public AttackState attackState;


		void Start()
		{
			target = player.transform;

			// add new states here
			idleState = new IdleState(this, sm);
			runningState = new RunningState(this, sm);
			attackState = new AttackState(this, sm);

			// initialise the statemachine with the default state
			sm.Init(idleState);
		}

        void Update()
        {
			sm.CurrentState.LogicUpdate();   
			Debug.Log(sm.CurrentState.ToString());
        }

        void FixedUpdate()
		{
			sm.CurrentState.PhysicsUpdate();
		}

		public void CheckForIdle()
		{
			if (agent.velocity.magnitude == 0)
            {
				sm.ChangeState(idleState);
			}			
		}

		public void CheckForRun()
		{
			sm.ChangeState(runningState);
		}

		public void CheckForAttack()
		{
			// Get the distance to the player
			distance = Vector3.Distance(target.position, transform.position);

			// If inside the radius
			if (distance <= lookRadius)
			{
				sm.ChangeState(attackState);
			}
		}

		// Point towards the player
		public void FaceTarget()
		{
			Vector3 direction = (target.position - transform.position).normalized;
			Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
			transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
		}

		void OnDrawGizmosSelected()
		{
			Gizmos.color = Color.red;
			Gizmos.DrawWireSphere(transform.position, lookRadius);
		}
	}
}




