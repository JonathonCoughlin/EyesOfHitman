using UnityEngine;
using System.Collections;


public class SplineWalker : MonoBehaviour {

    public BezierSpline m_Spline;

    //Walking states
    private bool m_walking;
    private float m_splinePos;
    public bool m_autoWalk = false;
    public bool m_autoReset = false;
    public bool m_destroyAtEnd;
    public float m_walkSpeed; //Percent per second
    public bool m_controlYRotation = false;
    public float m_yAngleOffset = 0f;

	// Use this for initialization
	void Start () {
        ResetSpline();
	}
	
	// Update is called once per frame
	void Update () {
        if (!m_walking)
        {
            if (Input.GetKey(KeyCode.Space) || m_autoWalk) { StartWalking(); }
        } else
        {
            WalkSpline(Time.deltaTime);
        }
	}

    private void ResetSpline()
    {
        m_walking = false;
        m_splinePos = 0f;
    }

    public void StartWalking()
    {
        m_walking = true;
    }

    private void WalkSpline(float deltaTime)
    {
        if (m_splinePos < 1.0f)
        {
            m_splinePos += deltaTime * m_walkSpeed / 100f;
            this.transform.position = m_Spline.GetPoint(m_splinePos, true);

            if (m_controlYRotation)
            {
                //get current euler angles
                Vector3 oldEuler = this.transform.rotation.eulerAngles;
                //calculate y rotation
                Vector3 splineVelocity = m_Spline.GetVelocity(m_splinePos, true);
                float newYangle = m_yAngleOffset + -Mathf.Rad2Deg * Mathf.Atan2(splineVelocity.z, splineVelocity.x);
                //combine angles
                Vector3 newEuler = new Vector3(oldEuler.x, newYangle, oldEuler.z);
                this.transform.eulerAngles = newEuler;
            }
        } else
        {
            if (m_destroyAtEnd)
            {
                Destroy(transform.gameObject);
            }
            else if (m_autoReset)
            {
                ResetSpline();
                StartWalking();
            }
        }
    }


}
