using UnityEngine;
using System.Collections;

public enum GameState { Title, RedCarpet, HoF, Ballroom, Bathroom, Assassination, Credits }

public class HitmanGameManager : MonoBehaviour {

    //Game States & Parameters
    public bool freeRoamTest;
    public GameState m_GameState;
    private bool m_GameBegun = false;
    private int m_ballroomCount = 0;

    //Player
    public FirstPersonDrifter m_Player;

    //Sequences
    public FishingSequence m_FishingSequence;
    public RedCarpetSequence m_RedCarpetSequence;
    public HallOfFameSequence m_HoFSequence;
    public BallroomSequence m_BallroomSequence;
    public BathroomSequence m_BathroomSequence;
    public AssassinationSequence m_AssassinationSequence;

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
	    if (!m_GameBegun)
        {
            LoadGameState();
            m_GameBegun = true;
        }
	}

    public void LoadGameState()
    {
        if (!m_GameBegun)
        {
            m_BallroomSequence.LowResourceSequence();
            m_HoFSequence.LowResourceSequence();
            m_RedCarpetSequence.PauseRedCarpetSequence();
            m_BathroomSequence.LowResourceSequence();
        }

        switch (m_GameState)
        {
            case GameState.Title:
                {
                    m_FishingSequence.StartFishingSequence();
                    break;
                }

            case GameState.RedCarpet:
                {
                    m_RedCarpetSequence.StartRedCarpetSequence();
                    m_FishingSequence.FadeOut();
                    break;
                }

            case GameState.HoF:
                {  
                    m_HoFSequence.StartHoFSequence();
                    break;
                }

            case GameState.Ballroom:
                {
                    m_BallroomSequence.StartBallroomSequence();
                    m_RedCarpetSequence.EndRedCarpetSequence();
                    m_BathroomSequence.LowResourceSequence();
                    m_HoFSequence.EndSequence();
                    m_ballroomCount++;
                    break;
                }

            case GameState.Assassination:
                {
                    m_AssassinationSequence.Animate();
                    m_RedCarpetSequence.EndRedCarpetSequence();
                    m_RedCarpetSequence.TurnOnLights();
                    m_HoFSequence.EndSequence();
                    m_HoFSequence.TurnOnLights();
                    break;
                }

            case GameState.Credits:
                {

                    break;
                }
        }
    }

    public void TransitionToGameState()
    {
        switch (m_GameState)
        {
            case GameState.Title:
                {
                    m_FishingSequence.StartFishingSequence();
                    break;
                }

            case GameState.RedCarpet:
                {
                    m_RedCarpetSequence.StartRedCarpetSequence();
                    m_FishingSequence.FadeOut();
                    break;
                }

            case GameState.HoF:
                {
                    m_RedCarpetSequence.EndRedCarpetSequence();
                    m_HoFSequence.TransitionSequence();
                    break;
                }

            case GameState.Ballroom:
                {
                    m_BathroomSequence.EndSequence();
                    m_BallroomSequence.TransitionSequence();
                    
                    if (m_ballroomCount < 1)
                    {
                        m_HoFSequence.EndSequence();
                    } else
                    {

                    }
                    m_ballroomCount++;
                    break;
                }

            case GameState.Bathroom:
                {
                    m_BathroomSequence.StartSequence();
                    m_BallroomSequence.LowResourceSequence();
                    break;
                }

            case GameState.Assassination:
                {
                    m_AssassinationSequence.Animate();

                    break;
                }

            case GameState.Credits:
                {

                    break;
                }
        }
    }
    public static void KillAllCameras()
    {
        Camera[] allCameras = FindObjectsOfType(typeof(Camera)) as Camera[];
        for (int ii = 0; ii < allCameras.Length; ii++)
        {
            allCameras[ii].enabled = false;
            //kill audio listener
            allCameras[ii].gameObject.GetComponent<AudioListener>().enabled = false;
        }
    }

    public static void ActivateCameraAndListen(Camera toActivate)
    {
        toActivate.enabled = true;
        toActivate.gameObject.GetComponent<AudioListener>().enabled = true;
    }
}
