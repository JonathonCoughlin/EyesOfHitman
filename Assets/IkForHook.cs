using UnityEngine;
using System.Collections;

public class IkForHook : MonoBehaviour {

    public GameObject Agent47;
    public GameObject Hook;
    public bool ikActive = false;
    void OnAnimatorIK(int layerIndex)
    {
        Animator animator = Agent47.GetComponentInParent<Animator>();
        if (ikActive)
        {
            animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1);
            animator.SetIKPosition(AvatarIKGoal.LeftHand, Hook.transform.position);

            animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1);
            animator.SetIKRotation(AvatarIKGoal.LeftHand, Hook.transform.rotation);
        }
        else
        {
        }
    }
}
