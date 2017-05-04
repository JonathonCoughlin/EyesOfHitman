using UnityEngine;
using System.Collections;
using EyeOfHitman;

[RequireComponent(typeof(Collider))]
public class ConversationTable : MonoBehaviour
{

    //Conversation Specifics
    public ClownAnimations m_speakerAnimation;
    public AudioClip m_speakerMonologue;
    public float m_secondsBetweenMonologues;
    
    //Conversation States


    //TableMembers
    public SpeakingClown m_Speaker;

    //Components
    private Collider m_collider;
    private Dialog m_dialog;
    // Use this for initialization
    void Start()
    {
        SetComponents();
        SetSpeakingClown();
    }

    private void SetComponents()
    {
        m_collider = GetComponent<Collider>();
        m_dialog = GetComponent<Dialog>();
    }

    private void SetSpeakingClown()
    {
        //m_Speaker.SilenceMe();
        m_Speaker.SetMyStates(m_speakerAnimation, m_speakerMonologue, m_secondsBetweenMonologues);
    }

    // Update is called once per frame
    void Update()
    {

    }

    //Conversation Management
    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            m_Speaker.BeginSpeaking();
            if (m_dialog != null)
            {
                m_dialog.StartDialog();
            }
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            m_Speaker.EndSpeaking();
            if (m_dialog != null)
            {
                m_dialog.StopDialog();
            }
        }
    }
}
