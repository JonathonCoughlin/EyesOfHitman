using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public enum SplineSpeedType { PercentPerSecond, TimeInterval };

public class CameraSplinePoint
{
    public float m_speedPerSegment;
    public bool m_newRotationTarget;
    public GameObject m_rotationTarget;

    public CameraSplinePoint(float spd, bool newTgt, GameObject tgt)
    {
        m_speedPerSegment = spd;
        m_newRotationTarget = newTgt;
        m_rotationTarget = tgt;
    }

}

public class SplineWalker : MonoBehaviour {

    public BezierSpline m_Spline;

    //Walking states
    private bool m_walking;
    private bool m_paused = false;
    private float m_splinePos;
    public bool m_autoWalk = false;
    public bool m_autoReset = false;
    public bool m_destroyAtEnd;
    public float m_walkSpeed; //Percent per second
    public bool m_controlYRotation = false;
    public float m_yAngleOffset = 0f;

    //Custom Speeds
    public bool m_variableSpeed;
    public List<CameraSplinePoint> m_cameraSplinePoints = new List<CameraSplinePoint>();
    public SplineSpeedType m_speedType;
    public List<float> segmentSpeedValue = new List<float>();
    public List<bool> useLookTarget = new List<bool>();
    public List<GameObject> currentTarget = new List<GameObject>();

	// Use this for initialization
	void Start () {
        ResetSpline();

        for (int ii = 0; ii < segmentSpeedValue.Count; ii++ )
        {
            m_cameraSplinePoints.Add(new CameraSplinePoint(segmentSpeedValue[ii], useLookTarget[ii], currentTarget[ii]));
        }

	}
	
	// Update is called once per frame
	void FixedUpdate () {
        
        if (!m_walking)
        {
            if (Input.GetKey(KeyCode.Space) || m_autoWalk) { StartWalking(); }
        } else if (!m_paused)
        {
            ManageWalk();
        } else
        {
            ManagePause();
        }
        ManageReset();
	}

    private void ManageWalk()
    {
        if (m_variableSpeed)
        {
            WalkSplineAtVariableSpeed(Time.fixedDeltaTime);
        }
        else
        {
            WalkSpline(Time.fixedDeltaTime);
        }
    }

    private void ManagePause()
    {
        if (Input.GetKeyDown(KeyCode.Space)) { PauseWalking(); }
    }

    private void ManageReset()
    {
        if (Input.GetKeyDown(KeyCode.R)) { ResetSpline(); }
    }

    private void ResetSpline()
    {
        m_walking = false;
        m_paused = false;
        m_splinePos = 0f;
    }

    public void StartWalking()
    {
        m_walking = true;
    }

    public void PauseWalking()
    {
        m_paused = !m_paused;
    }

    private void WalkSplineAtVariableSpeed(float deltaTime)
    {
        int currentCurveIndex = Mathf.Min(m_Spline.CurveIDatPercentage(m_splinePos),m_cameraSplinePoints.Count-1);
        float currentSpeed = 0f;
        switch (m_speedType)
        {
            case SplineSpeedType.PercentPerSecond:
                {
                    currentSpeed = m_cameraSplinePoints[currentCurveIndex].m_speedPerSegment;
                    break;
                }
            case SplineSpeedType.TimeInterval:
                {
                    float segmentPercentage = 100f / (m_Spline.CurveCount);
                    currentSpeed = segmentPercentage / m_cameraSplinePoints[currentCurveIndex].m_speedPerSegment;
                    Debug.Log("speed: " + currentSpeed + "pct/s");
                    break;
                }
        }
        

        if (m_splinePos < 1.0f)
        {
            m_splinePos += deltaTime * currentSpeed / 100f;
            this.transform.position = m_Spline.GetPoint(m_splinePos, true);

            if (m_cameraSplinePoints[currentCurveIndex].m_newRotationTarget)
            {
                transform.LookAt(m_cameraSplinePoints[currentCurveIndex].m_rotationTarget.transform);
                //Debug.Log("CurveIndex: " + currentCurveIndex);
                //Debug.Log("Spline Pct: " + m_splinePos);
                //Debug.Log("PctPerSec: " + m_cameraSplinePoints[currentCurveIndex].m_speedPerSegment);
                //Debug.Log("New Tgt: " + m_cameraSplinePoints[currentCurveIndex].m_newRotationTarget);
                //Debug.Log("TargetName:" + m_cameraSplinePoints[currentCurveIndex].m_rotationTarget.name);
            }
        }
        else
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
