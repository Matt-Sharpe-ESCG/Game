using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.VFX;
using UnityEditor.SceneManagement;


/* Makes enemies follow and attack the player */
namespace Player
{
	public class EnemyController : MonoBehaviour
	{

		public float lookRadius = 10f;
		public float distance;
		public float speed;

		public Transform target;
		public NavMeshAgent agent;
		public GameObject player;

		public Rigidbody rb;
		public StateMachine sm;
		public Animator animator;

		// variables holding the different player states
		public IdleState idleState;
		public RunningState runningState;
		public AttackState attackState;


		void Start()
		{
			target = player.transform;
			agent = GetComponent<NavMeshAgent>();

			sm = gameObject.AddComponent<StateMachine>();
			animator = gameObject.GetComponent<Animator>();

			// add new states here
			idleState = new IdleState(this, sm);
			runningState = new RunningState(this, sm);
			attackState = new AttackState(this, sm);

			// initialise the statemachine with the default state
			sm.Init(idleState);
		}

		void FixedUpdate()
		{
			sm.CurrentState.PhysicsUpdate();
		}

		public void CheckForIdle()
		{
			if (speed == 0)
			{
				sm.ChangeState(idleState);
			}
		}

		public void CheckForRun()
		{
			if (speed > 0)
            {
				sm.ChangeState(runningState);
			}		
		}

		public void CheckForAttack()
		{
			if (Input.GetKeyDown(KeyCode.Return))
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




