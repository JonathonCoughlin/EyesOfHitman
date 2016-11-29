using UnityEngine;
using System.Collections;
using System.Collections.Generic;


[RequireComponent(typeof(SplineWalker))]
public class SplineShuffler : MonoBehaviour {

    private SplineWalker m_Walker;

    public List<BezierSpline> m_Splines = new List<BezierSpline>();

	// Use this for initialization
	void Start () {
        SetComponents();
        ShuffleSpline();
	}
	
    private void SetComponents()
    {
        m_Walker = GetComponent<SplineWalker>();
    }

    private void ShuffleSpline()
    {
        int splineID = Random.Range(0, m_Splines.Count);
        m_Walker.m_Spline = m_Splines[splineID];
    }

	// Update is called once per frame
	void Update () {
	
	}
}
