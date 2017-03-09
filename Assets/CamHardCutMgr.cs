using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CamHardCutMgr : MonoBehaviour {

    public List<GameObject> m_camLocRotTgts = new List<GameObject>();
    private int m_tgtIdx = 0;

	// Use this for initialization
	void Start () {
        SetUpCam();
	}

    // Update is called once per frame
	void Update () {
	
	}

    public void SetUpCam()
    {
        this.transform.parent = m_camLocRotTgts[m_tgtIdx].transform;
        this.transform.localPosition = Vector3.zero;
        this.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
    }

    public void IncrementCam()
    {
        m_tgtIdx++;
        if (m_tgtIdx >= m_camLocRotTgts.Count)
        {
            m_tgtIdx = 0;
        }

        SetUpCam();
    }

}
