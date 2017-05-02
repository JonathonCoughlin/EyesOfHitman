using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(BarkVolume))]
public class PlatterManager : MonoBehaviour {

    //Components
    private BarkVolume m_BarkVolume;

    //Platter parameters
    public List<ServeableFood> m_foodList = new List<ServeableFood>();
    public GameObject m_PlatterParentObject;
    public bool m_shuffleOnStart = true;
    public int m_platterIdxToStart;
    public ServeableFood m_currentPlatter;

    // Use this for initialization
    void Start() {
        SetComponents();

        if (m_shuffleOnStart)
        {
            ShufflePlatter();
        } else
        {
            SetPlatterByIdx(m_platterIdxToStart);
        }
    }

    private void SetComponents()
    {
        m_BarkVolume = GetComponent<BarkVolume>();
    }

    public void ShufflePlatter()
    {
        int randPlatter = Random.Range(0, m_foodList.Count - 1);
        if (m_currentPlatter != null) Destroy(m_currentPlatter.gameObject);
        m_currentPlatter = Instantiate(m_foodList[randPlatter]);
        SetPlatterToParent();
        SetPlatterAudio();
    }

    private void SetPlatterAudio()
    {
        m_BarkVolume.m_barkClip = m_currentPlatter.m_waiterLine;
    }

    public void SetPlatterByIdx(int idx)
    {
        if (m_currentPlatter != null) Destroy(m_currentPlatter.gameObject);
        m_currentPlatter = Instantiate(m_foodList[idx]);
        SetPlatterToParent();
        SetPlatterAudio();
    }

    private void SetPlatterToParent()
    {
        m_currentPlatter.transform.SetParent(m_PlatterParentObject.transform);
        m_currentPlatter.transform.localPosition = Vector3.zero;
        m_currentPlatter.transform.localEulerAngles = Vector3.zero;
    }
	
}
