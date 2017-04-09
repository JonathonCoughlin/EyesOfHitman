using UnityEngine;
using System.Collections;
using JonClickSystem;
using FirstPersonExploration;

public class GrabPropHelper : MonoBehaviour {

    public HoldablePropClickEvents m_prop;
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
	
	// Update is called once per frame
	void Update () {
	
	}

    public void PutPropInHand()
    {
        m_prop.GrabMe();
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
        Transform camPos = Camera.main.transform;
        Ray throwRay = new Ray(camPos.position, camPos.forward);
        m_prop.ThrowMe(throwRay.direction);
    }

    public void ThrowProp2()
    {
        m_FPExplorer.ThrowProp();
    }

    public void EatProp()
    {
        m_FPExplorer.EatProp();
    }
}
