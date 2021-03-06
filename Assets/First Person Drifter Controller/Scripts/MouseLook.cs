﻿// original by asteins
// adapted by @torahhorse
// http://wiki.unity3d.com/index.php/SmoothMouseLook

// Instructions:
// There should be one MouseLook script on the Player itself, and another on the camera
// player's MouseLook should use MouseX, camera's MouseLook should use MouseY

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MouseLook : MonoBehaviour
{
 
	public enum RotationAxes { MouseX = 1, MouseY = 2 }
	public RotationAxes axes = RotationAxes.MouseX;
	public bool invertY = false;
	
	public float sensitivityX = 10F;
	public float sensitivityY = 9F;
 
	public float minimumX = -360F;
	public float maximumX = 360F;
 
	public float minimumY = -85F;
	public float maximumY = 85F;
 
	public float rotationX { get; private set; }
    
	public float rotationY { get; private set; }
 
	private List<float> rotArrayX = new List<float>();
	float rotAverageX = 0F;	
 
	private List<float> rotArrayY = new List<float>();
	float rotAverageY = 0F;
 
	public float framesOfSmoothing = 5;

    //Modifications for Blendo Cut
    private bool triggerBlendoCut = false;
    private Vector3 blendoAngles;
 
	Quaternion originalRotation;

    private GameObject targetToTrack;
    private bool amTrackingTarget = false;
	
	void Start ()
	{			
		if (GetComponent<Rigidbody>())
		{
			GetComponent<Rigidbody>().freezeRotation = true;
		}
		
		originalRotation = transform.localRotation;
	}
 
	void Update ()
	{
        if (triggerBlendoCut)
        {
            if (axes == RotationAxes.MouseX)
            {
                //Get difference between new and original orientation
                Vector3 diffAngles = blendoAngles - originalRotation.eulerAngles;
                //Reset angle movement history
                rotArrayX = new List<float>();
                rotationX = diffAngles.y;
                rotArrayX.Add(rotationX);
                Quaternion xQuaternion = Quaternion.AngleAxis(rotArrayX[0], Vector3.up);
                transform.localRotation = originalRotation * xQuaternion;
                triggerBlendoCut = false;
            } else
            {
                //Get difference between new and original orientation
                Vector3 diffAngles = blendoAngles - originalRotation.eulerAngles;
                //Reset angle movement history
                rotArrayY = new List<float>();
                rotationY = -diffAngles.x;
                rotArrayY.Add(rotationY);
                Quaternion yQuaternion = Quaternion.AngleAxis(rotArrayY[0], Vector3.left);
                transform.localRotation = originalRotation * yQuaternion;
                triggerBlendoCut = false;
            }
        }
        else {
            if (axes == RotationAxes.MouseX)
            {
                rotAverageX = 0f;

                //rotationX - total rotation rel original
                if (amTrackingTarget)
                {
                    Vector3 meToTgtWorld = targetToTrack.transform.position - this.transform.position;
                    Vector3 originalFwd = originalRotation * Vector3.forward;
                    Vector3 xPlane = Vector3.forward + Vector3.right;
                    Vector3 meToTgtInXPlane = new Vector3(meToTgtWorld.x,0f,meToTgtWorld.z);
                    rotationX = Vector3.Angle(originalFwd, meToTgtInXPlane);
                } else
                {
                    rotationX += Input.GetAxis("Mouse X") * sensitivityX * Time.timeScale;
                }
                
                //rotArrayX - history of X rotation
                rotArrayX.Add(rotationX);

                if (rotArrayX.Count >= framesOfSmoothing)
                {
                    rotArrayX.RemoveAt(0);
                }
                for (int i = 0; i < rotArrayX.Count; i++)
                {
                    rotAverageX += rotArrayX[i];
                }
                rotAverageX /= rotArrayX.Count;
                rotAverageX = ClampAngle(rotAverageX, minimumX, maximumX);
                
                Quaternion xQuaternion = Quaternion.AngleAxis(rotAverageX, Vector3.up);
                transform.localRotation = originalRotation * xQuaternion;
            }
            else
            {
                rotAverageY = 0f;

                float invertFlag = 1f;
                if (invertY)
                {
                    invertFlag = -1f;
                }
                rotationY += Input.GetAxis("Mouse Y") * sensitivityY * invertFlag * Time.timeScale;
                rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);
                rotArrayY.Add(rotationY);

                if (rotArrayY.Count >= framesOfSmoothing)
                {
                    rotArrayY.RemoveAt(0);
                }
                for (int j = 0; j < rotArrayY.Count; j++)
                {
                    rotAverageY += rotArrayY[j];
                }
                rotAverageY /= rotArrayY.Count;

                Quaternion yQuaternion = Quaternion.AngleAxis(rotAverageY, Vector3.left);
                transform.localRotation = originalRotation * yQuaternion;
            }
        }
	}

    public void TrackThisTarget(GameObject tempTarget)
    {
        targetToTrack = tempTarget;

        amTrackingTarget = true;
    }

    public void StopTracking()
    {
        amTrackingTarget = false;
    }
	
	public void SetSensitivity(float s)
	{
		sensitivityX = s;
		sensitivityY = s;
	}

    public void TriggerBlendoCut(Vector3 eulerAngles)
    {
        triggerBlendoCut = true;
        blendoAngles = eulerAngles;
    }
 
	public static float ClampAngle (float angle, float min, float max)
	{
		angle = angle % 360;
		if ((angle >= -360F) && (angle <= 360F)) {
			if (angle < -360F) {
				angle += 360F;
			}
			if (angle > 360F) {
				angle -= 360F;
			}			
		}
		return Mathf.Clamp (angle, min, max);
	}
}