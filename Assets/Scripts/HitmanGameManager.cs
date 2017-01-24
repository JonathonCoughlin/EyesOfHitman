using UnityEngine;
using System.Collections;

public enum GameState { Title, RedCarpet, HoF, Ballroom, Assassination, Credits }

public class HitmanGameManager : MonoBehaviour {

    //Game States & Parameters
    public bool freeRoamTest;
    public GameState m_GameState;
    private bool m_GameBegun = false;

    //Player
    public FirstPersonDrifter m_Player;

    //Sequences
    public FishingSequence m_FishingSequence;
    public RedCarpetSequence m_RedCarpetSequence;
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

    private void LoadGameState()
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
                    break;
                }

            case GameState.HoF:
                {

                    break;
                }

            case GameState.Ballroom:
                {

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
        }
    }
}
