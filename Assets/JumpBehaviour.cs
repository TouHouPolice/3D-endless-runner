using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpBehaviour : StateMachineBehaviour {
    Vector3 lastPosition;

    Vector3 oCenter = new Vector3(0f, 0.75f, 0f);
    float oRadius = 0.3f;
    float oHeight = 1.5f;

    Vector3 jCenter = new Vector3(0f, 1.75f, 0f);
    float jRadius = 0.3f;
    float jHeight = 1f;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Player.collider.center = jCenter;
        Player.collider.radius = jRadius;
        Player.collider.height = jHeight;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Vector3 currPos = Player.playertransform.transform.position;
        if(Mathf.Sign(currPos.y - lastPosition.y) == -1 && currPos.y < 1)
        {
            Player.collider.center = oCenter;
            Player.collider.radius = oRadius;
            Player.collider.height = oHeight;
        }

        if(currPos.y == 0 && Mathf.Sign(currPos.y - lastPosition.y) == -1)
        {
            Player.rb.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotation;
            //Player.canJump = true;
        }

        lastPosition = currPos;
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //Player.rb.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotation;
        Player.canJump = true;
    }

    // OnStateMove is called right after Animator.OnAnimatorMove(). Code that processes and affects root motion should be implemented here
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK(). Code that sets up animation IK (inverse kinematics) should be implemented here.
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}
}
