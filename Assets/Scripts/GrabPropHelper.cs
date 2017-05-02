using UnityEngine;
using System.Collections;
using JonClickSystem;
using FirstPersonExploration;

public class GrabPropHelper : MonoBehaviour {

    private GameObject m_Player;
    private FPExplorer m_FPExplorer;

	// Use this for initialization
	void Start () {
        SetComponents();
	}

    private void SetComponents()
    {
        m_Player = GameObject.FindGameObjectWithTag("Player");
        m_FPExplorer = m_Player.GetComponent<FPExplorer>();
    }

    public void PlayActionAudio()
    {
        m_FPExplorer.PlayActionAudio();
    }

    public void PlaceProp()
    {
        m_FPExplorer.PlaceProp();
    }

    public void ThrowProp()
    {
        m_FPExplorer.ThrowProp();
    }

    public void EatProp()
    {
        m_FPExplorer.EatProp();
    }
}
