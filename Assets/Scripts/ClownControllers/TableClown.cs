using UnityEngine;
using System.Collections;

public enum ClownAnimations
{
    CrossedArms,
    HandsOnHips,
    PowerStanceOration,
    RubbingFace,
    TabletopStretch,
    GentleOration,
    SympatheticDrinking,
    StrongDrinking,
    Walk,
    Dance,
    MagicianStaticPerform,
    MagicianStaticFolded,
    MagicianStaticCrouch,
    MagicianStaticTrick,
    BlenderImport,
    BoatDriver
};

[RequireComponent(typeof(Animator))]
public class TableClown : MonoBehaviour {

    //Public animation commands
    public int animationIDCommand;
    public bool speakingCommand;
    public bool drinkingCommand;
    public bool staticCommand;
    public bool sittingCommand;

    //Overrides
    public bool m_overrideAnimation;
    public ClownAnimations m_AnimationStateOverride;
    
    //Private animation states
    private int m_animationID;
    private bool m_speaking;
    private bool m_drinking;
    private bool m_static;
    private bool m_sitting;

    //Components
    private Animator m_Animator;

	// Use this for initialization
	void Start () {
        SetComponents();
        UpdateAnimation();
	}

    private void SetComponents()
    {
        m_Animator = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {

        bool animationIDChange = (m_animationID == animationIDCommand);
        bool speakingChange = (m_speaking == speakingCommand);
        bool drinkingChange = (m_drinking == drinkingCommand);
        bool staticChange = (m_static == staticCommand);
        bool sittingChange = (m_sitting == sittingCommand);
        bool animationChange = animationIDChange || speakingChange || drinkingChange || staticChange || sittingChange;

	    if (animationChange) { UpdateAnimation(); }
	}

    private void UpdateAnimation()
    {
        if (m_overrideAnimation)
        {
            SetAnimationFromKey();
        }
        else {
            //Set States
            m_animationID = animationIDCommand;
            m_speaking = speakingCommand;
            m_drinking = drinkingCommand;
            m_static = staticCommand;
            SetAnimatorStates();
        }
    }

    private void SetAnimatorStates()
    {
        m_Animator.SetInteger("AnimationID", m_animationID);
        m_Animator.SetBool("Speaking", m_speaking);
        m_Animator.SetBool("WithDrink", m_drinking);
        m_Animator.SetBool("Static", m_static);
        m_Animator.SetBool("Sitting", m_sitting);

    }

    public void RandAnimationStartPos()
    {
        float randTime = Random.Range(0.1f, 0.9f);
        //This just doesn't work yet
        
    }

    private void SetAnimationFromKey()
    {
        switch (m_AnimationStateOverride)
        {
            //Static Poses
            case ClownAnimations.MagicianStaticTrick:
                {
                    m_animationID = 0;
                    m_static = true;
                    break;
                }

            case ClownAnimations.MagicianStaticCrouch:
                {
                    m_animationID = 1;
                    m_static = true;
                    break;
                }

            case ClownAnimations.MagicianStaticPerform:
                {
                    m_animationID = 2;
                    m_static = true;
                    break;
                }
            case ClownAnimations.MagicianStaticFolded:
                {
                    m_animationID = 3;
                    m_static = true;
                    break;
                }
            //Speaking Poses
            case ClownAnimations.PowerStanceOration:
                {
                    m_animationID = 0;
                    m_speaking = true;
                    m_static = false;
                    break;
                }
            case ClownAnimations.GentleOration:
                {
                    m_animationID = 1;
                    m_speaking = true;
                    m_static = false;
                    break;
                }
            //Listening Poses
            case ClownAnimations.CrossedArms:
                {
                    m_animationID = 0;
                    m_speaking = false;
                    m_drinking = false;
                    m_static = false;
                    break;
                }
            case ClownAnimations.TabletopStretch:
                {
                    m_animationID = 1;
                    m_speaking = false;
                    m_drinking = false;
                    m_static = false;
                    break;
                }
            case ClownAnimations.HandsOnHips:
                {
                    m_animationID = 2;
                    m_speaking = false;
                    m_drinking = false;
                    m_static = false;
                    break;
                }
            case ClownAnimations.RubbingFace:
                {
                    m_animationID = 3;
                    m_speaking = false;
                    m_drinking = false;
                    m_static = false;
                    break;
                }
            case ClownAnimations.Dance:
                {
                    m_animationID = 4;
                    m_speaking = false;
                    m_drinking = false;
                    m_static = false;
                    break;
                }
            case ClownAnimations.SympatheticDrinking:
                {
                    m_animationID = 0;
                    m_speaking = false;
                    m_drinking = true;
                    m_static = false;
                    break;
                }
            case ClownAnimations.StrongDrinking:
                {
                    m_animationID = 1;
                    m_speaking = false;
                    m_drinking = true;
                    m_static = false;
                    break;
                }

            case ClownAnimations.BoatDriver:
                {
                    m_sitting = true;
                    m_speaking = false;
                    m_drinking = false;
                    m_static = false;
                    m_animationID = 1;
                    break;
                }

        }
        SetAnimatorStates();
    }

}
