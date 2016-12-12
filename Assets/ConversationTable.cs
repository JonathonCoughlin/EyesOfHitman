using UnityEngine;
using System.Collections;

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

    // Use this for initialization
    void Start()
    {
        SetComponents();
        SetSpeakingClown();
    }

    private void SetComponents()
    {
        m_collider = GetComponent<Collider>();
    }

    private void SetSpeakingClown()
    {
        m_Speaker.SilenceMe();

        m_Speaker.m_Story = m_speakerMonologue;
        m_Speaker.m_AnimationStateOverride = m_speakerAnimation;
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
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            m_Speaker.EndSpeaking();
        }
    }
}
