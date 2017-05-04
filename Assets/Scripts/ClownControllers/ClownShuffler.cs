using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using EyeOfHitman;

public class ClownShuffler : MonoBehaviour
{

    public GameObject m_ClownHead;
    private Renderer m_HeadRenderer;
    public GameObject m_ClownBody;
    private Renderer m_BodyRenderer;

    public List<Material> m_HeadLibrary = new List<Material>();
    public bool m_shuffleHead = true;
    public int m_headOverrideID = 0;
    private int m_currentHeadID = 0;

    public List<Material> m_BodyLibrary = new List<Material>();
    public bool m_shuffleBody = true;
    public int m_bodyOverrideID = 0;
    private int m_currentBodyID = 0;

    // Use this for initialization
    void Start()
    {
        SetComponents();
        ShuffleMe();
    }

    public void ShuffleMe()
    {
        if (m_shuffleHead)
        {
            ShuffleHead();
        }
        else
        {
            m_currentHeadID = m_headOverrideID;
            SetHead();
        }

        if (m_shuffleBody)
        {
            ShuffleBody();
        }
        else
        {
            m_currentBodyID = m_bodyOverrideID;
            SetBody();
        }
    }

    private void SetComponents()
    {
        m_HeadRenderer = m_ClownHead.GetComponent<Renderer>();
        m_BodyRenderer = m_ClownBody.GetComponent<Renderer>();
    }

    private void ShuffleHead()
    {
        m_currentHeadID = Random.Range(0, m_HeadLibrary.Count);
        SetHead();        
    }

    private void SetHead()
    {
        m_HeadRenderer.material = m_HeadLibrary[m_currentHeadID];
        DialogParticipant participant = GetComponent<DialogParticipant>();
        if (participant != null)
        {
            participant.RefreshTextures();
        }
    }

    private void ShuffleBody()
    {
        m_currentBodyID = Random.Range(0, m_BodyLibrary.Count);
        SetBody();
    }

    private void SetBody()
    {
        m_BodyRenderer.material = m_BodyLibrary[m_currentBodyID];
    }

    // Update is called once per frame
    void Update()
    {

    }
}
