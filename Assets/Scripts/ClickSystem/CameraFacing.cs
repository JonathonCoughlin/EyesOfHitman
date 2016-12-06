using UnityEngine;
using System.Collections;

public class CameraFacing : MonoBehaviour {

    public bool flipSprite;
    public bool restrictToY;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        TurnToCamera();
	}

    private void TurnToCamera()
    {
        Transform target = Camera.main.transform;
        if (flipSprite)
        {
            var relativeUp = target.TransformDirection(Vector3.up);
            var relativePos = target.position - transform.position;
            transform.rotation = Quaternion.LookRotation(-relativePos);
        }
        else
        {
            transform.LookAt(target);
        }

        if (restrictToY)
        {
            // Find a way to restrict rotation to Y axis only
        }

    }
}
