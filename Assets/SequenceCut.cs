using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Collider))]
public class SequenceCut : MonoBehaviour {

    public HitmanGameManager m_Game;
    private Collider m_Collider;
    public GameState m_stateToLoad;

	// Use this for initialization
	void Start () {
        SetComponents();
	}
	
    private void SetComponents()
    {
        m_Collider = GetComponent<Collider>();
    }

	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "PlayerSecondary")
        {
            m_Game.m_GameState = m_stateToLoad;
            m_Game.LoadGameState();
        }
    }


}
