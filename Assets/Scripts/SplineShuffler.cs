using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class SimpleSplineParameters
{
    public WalkerSpeedType m_speedType;
    public float m_walkSpeed;
    public WalkerRotationType m_rotationType;
    public GameObject m_lookTarget;
    public RotationAxis m_rotationAxis;
    public float m_offsetAngle;
    public bool m_autoWalk;
    public bool m_autoReset;
    public bool m_destroyAtEnd;
}

[RequireComponent(typeof(SplineWalker))]
public class SplineShuffler : MonoBehaviour {

    private SplineWalker m_Walker;

    public WalkerSpeedType m_speedType;
    public float m_walkSpeed;
    public WalkerRotationType m_RotationType;
    public RotationAxis m_rotationAxis;
    public float m_offsetAngle;
    public GameObject m_LookAtTarget;
    public bool m_autoWalk;
    public bool m_autoReset;
    public bool m_autoKill;

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
        SimpleSplineParameters parameters = new SimpleSplineParameters();
        parameters.m_speedType = m_speedType;
        parameters.m_walkSpeed = m_walkSpeed;
        parameters.m_rotationType = m_RotationType;
        parameters.m_rotationAxis = m_rotationAxis;
        parameters.m_offsetAngle = m_offsetAngle;
        parameters.m_lookTarget = m_LookAtTarget;
        parameters.m_autoWalk = m_autoWalk;
        parameters.m_autoReset = m_autoReset;
        parameters.m_destroyAtEnd = m_autoKill;

        int splineID = Random.Range(0, m_Splines.Count);
        m_Walker.SetSpline(m_Splines[splineID], parameters);
        
    }

	// Update is called once per frame
	void Update () {
	
	}
}
