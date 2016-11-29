using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ClownShuffler : MonoBehaviour
{

    public GameObject m_ClownHead;
    private Renderer m_HeadRenderer;
    public GameObject m_ClownBody;
    private Renderer m_BodyRenderer;

    public List<Material> m_HeadLibrary = new List<Material>();

    // Use this for initialization
    void Start()
    {
        
    }

    public void Awake()
    {
        SetComponents();
        ShuffleHead();
    }

    private void SetComponents()
    {
        m_HeadRenderer = m_ClownHead.GetComponent<Renderer>();
        m_BodyRenderer = m_ClownBody.GetComponent<Renderer>();
    }

    private void ShuffleHead()
    {
        int shuffleID = Random.Range(0, m_HeadLibrary.Count);
        m_HeadRenderer.material = m_HeadLibrary[shuffleID];
        Debug.Log("Head ID: " + shuffleID);
    }
    
    // Update is called once per frame
    void Update()
    {

    }
}
