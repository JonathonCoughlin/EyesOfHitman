using UnityEngine;
using System.Collections;

public class RedCarpetSequence : MonoBehaviour {

    public GameObject FP47StartingLocation;
    public FirstPersonDrifter FP47Drifter;
    public FPClownController FP47Controller;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        
	}

    public void StartRedCarpetSequence()
    {
        HitmanGameManager.KillAllCameras();
        FP47Drifter.transform.position = FP47StartingLocation.transform.position;
        FP47Drifter.transform.rotation = FP47StartingLocation.transform.rotation;
        FP47Controller.ActivateFPControl();
    }

}
