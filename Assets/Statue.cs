using UnityEngine;
using System.Collections;

public class Statue : MonoBehaviour {

    public enum StatuePose { Cardman, Gregarious, Juggler, Magician, Sad, Tamer, Thinker, Yoga};
    public StatuePose m_pose;
    private Animator m_Animator;

	// Use this for initialization
	void Start () {
        m_Animator = GetComponent<Animator>();
        SetPose();
	}
	
    private void SetPose()
    {
        switch (m_pose)
        {
            case StatuePose.Cardman:
                {
                    m_Animator.SetInteger("PoseID", 0);
                    break;
                }
            case StatuePose.Gregarious:
                {
                    m_Animator.SetInteger("PoseID", 1);
                    break;
                }
            case StatuePose.Juggler:
                {
                    m_Animator.SetInteger("PoseID", 2);
                    break;
                }
            case StatuePose.Magician:
                {
                    m_Animator.SetInteger("PoseID", 3);
                    break;
                }
            case StatuePose.Sad:
                {
                    m_Animator.SetInteger("PoseID", 4);
                    break;
                }

            case StatuePose.Tamer:
                {
                    m_Animator.SetInteger("PoseID", 5);
                    break;
                }
            case StatuePose.Thinker:
                {
                    m_Animator.SetInteger("PoseID", 6);
                    break;
                }
            case StatuePose.Yoga:
                {
                    m_Animator.SetInteger("PoseID", 7);
                    break;
                }
        }
    }

	// Update is called once per frame
	void Update () {
	
	}
}
