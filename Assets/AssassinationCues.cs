using UnityEngine;
using System.Collections;
using JonClickSystem;
using FirstPersonExploration;

[RequireComponent(typeof(AudioSource))]
public class AssassinationCues : MonoBehaviour {

    private HitmanGameManager m_Game;

    public bool FinalBuild = false;

    //Objects
    public BlendoCut BallroomDoor;
    public BlendoCut ControlRoomDoor;
    public Prop Axe;
    public CanonClickEvents LightSwitch;
    public PlayButtonClickEvents PlayButton;

    //Speaking Parts
    public AudioSource m_DialogVoice;
    public AudioClip m_pulleyDialog;


    //States
    private bool spotlightActivated = false;
    private bool axeHeld = false;
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
        if (m_Game.m_GameState == GameState.ControlRoom && !axeHeld) CheckAxe();
	}

    private void CheckAxe()
    {
        if (Axe.AmHeld()) HoldingAxe();
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
        m_DialogVoice.PlayOneShot(m_pulleyDialog);
        axeHeld = true;
        //Open Light Switch
        LightSwitch.enabled = true;
    }

    public void CannonLightOn()
    {
        spotlightActivated = true;
    }

    public void CannonLightOff()
    {
        spotlightActivated = false;
    }

    public void PlayButtonPress()
    {
        if (axeHeld && spotlightActivated)
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
