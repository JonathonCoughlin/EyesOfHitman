using UnityEngine;
using System.Collections;


[RequireComponent(typeof(Animator))]
public class TableClown : MonoBehaviour {

    //Public animation commands
    public int animationIDCommand;
    public bool speakingCommand;
    public bool drinkingCommand;

    //Private animation states
    private int m_animationID;
    private bool m_speaking;
    private bool m_drinking;

    //Components
    private Animator m_Animator;

	// Use this for initialization
	void Start () {
        SetComponents();
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
        bool animationChange = animationIDChange || speakingChange || drinkingChange;

	    if (animationChange) { UpdateAnimation(); }
	}

    private void UpdateAnimation()
    {
        //Set States
        m_animationID = animationIDCommand;
        m_speaking = speakingCommand;
        m_drinking = drinkingCommand;

        m_Animator.SetInteger("AnimationID", m_animationID);
        m_Animator.SetBool("Speaking", m_speaking);
        m_Animator.SetBool("WithDrink", m_drinking);
    }

}
