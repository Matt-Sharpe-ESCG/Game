using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.VFX;

namespace Player
{
    public class PlayerScript : MonoBehaviour
    {
        // variables holding the different player states
        public IdleState idleState;
        public RunningState runningState;
        public AttackState attackState;

        // Game Object Components
        public Rigidbody rb;
        public StateMachine sm;
        public Animator animator;

        // Start is called before the first frame update
        public void Start()
        {
            sm = gameObject.AddComponent<StateMachine>();
            animator = gameObject.GetComponent<Animator>();

            // add new states here
            idleState = new IdleState(this, sm);
            runningState = new RunningState(this, sm);
            attackState = new AttackState(this, sm);

            // initialise the statemachine with the default state
            sm.Init(idleState);
        }

        // Update is called once per frame
        public void Update()
        {
            sm.CurrentState.LogicUpdate();
        }

        void FixedUpdate()
        {
            sm.CurrentState.PhysicsUpdate();
        }

        public void CheckForIdle()
        {
            if (rb.velocity == Vector3.zero)
            {
                sm.ChangeState(idleState);
            }
        }

        public void CheckForRun()
        {
            if (rb.velocity != Vector3.zero)
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
/*
        private void OnCollisionStay2D(Collision2D collision)
        {
            isGrounded = true;
            print("isGrounded");
        }

        private void OnCollisionExit2D(Collision2D collision)
        {
            isGrounded = false;
            print("Not Grounded");
        }
*/
    }

}