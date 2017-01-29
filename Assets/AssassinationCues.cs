using UnityEngine;
using System.Collections;
using JonClickSystem;

public class AssassinationCues : MonoBehaviour {

    private HitmanGameManager m_Game;

    public bool FinalBuild = false;

    //Objects
    public BlendoCut BallroomDoor;
    public BlendoCut ControlRoomDoor;
    public HoldablePropClickEvents Axe;
    public CanonClickEvents LightSwitch;
    public PlayButtonClickEvents PlayButton;

    //States
    private bool readyForPlayButton = false;

	// Use this for initialization
	void Start () {
        SetComponents();
        if (FinalBuild)
        {
            DeactivateSwitches();
        }
	}

    private void SetComponents()
    {
        m_Game = (HitmanGameManager) FindObjectOfType(typeof(HitmanGameManager));
    }

    private void DeactivateSwitches()
    {
        BallroomDoor.enabled = false;
        ControlRoomDoor.enabled = false;
        Axe.enabled = false;
        LightSwitch.enabled = false;
    }
	
	// Update is called once per frame
	void Update () {
	
	}
    
    public void WaiterFound()
    {
        //Open Ballroom Door
        BallroomDoor.enabled = true;
    }

    public void CostumeOn()
    {
        //Open Control Room Door
        ControlRoomDoor.enabled = true;
    }

    public void EnableAxe()
    {
        Axe.enabled = true;
    }

    public void HoldingAxe()
    {
        //Speak about pulley

        //Open Light Switch
        LightSwitch.enabled = true;
    }

    public void CannonLightOn()
    {
        //WaitForPlayButton
        readyForPlayButton = true;
    }

    public void PlayButtonPress()
    {
        if (readyForPlayButton)
        {
            CueAssassination();
        }
    }

    private void CueAssassination()
    {
        m_Game.m_GameState = GameState.Assassination;
        m_Game.TransitionToGameState();
    }
}
