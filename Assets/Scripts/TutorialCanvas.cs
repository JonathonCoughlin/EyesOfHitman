using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class TutorialInput
{
    public KeyCode m_Control;
    public Image m_Sprite;
    public bool m_fadeHasBegun { get; private set; }

    public void BeginFade(float fadeTime)
    {
        m_Sprite.CrossFadeAlpha(0f, fadeTime, false);
        m_fadeHasBegun = true;
    }
    
}


public class TutorialCanvas : MonoBehaviour {

    private bool m_fadeComplete = false;

    public float m_fadeTime;
    
    public List<TutorialInput> m_Inputs = new List<TutorialInput>();

	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
        if (!m_fadeComplete)  ManageFades();
    }

    private void ManageFades()
    {
        m_fadeComplete = true;
        foreach (TutorialInput curInput in m_Inputs)
        {
            if (!curInput.m_fadeHasBegun)
            {
                if (Input.GetKeyDown(curInput.m_Control))
                {
                    curInput.BeginFade(m_fadeTime);
                } else
                {
                    m_fadeComplete = false; 
                }
            }
        }
    }
}


