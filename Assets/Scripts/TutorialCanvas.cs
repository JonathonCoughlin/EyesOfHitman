using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;


public enum TutorialKeyStatus { Hidden, FadingIn, Shown, FadingOut}

[System.Serializable]
public class TutorialInput
{
    public KeyCode m_Control;
    public Image m_Sprite;
    public TutorialKeyStatus m_showFadeStatus;

    public IEnumerator FadeOut(float fadeTime)
    {
        m_Sprite.CrossFadeAlpha(0f, fadeTime, false);
        m_showFadeStatus = TutorialKeyStatus.FadingOut;
        yield return new WaitForSeconds(fadeTime);
        m_showFadeStatus = TutorialKeyStatus.Hidden;
    }
    
    public IEnumerator FadeIn(float showTime, float showTransparency)
    {
        m_Sprite.CrossFadeAlpha(showTransparency, showTime, false);
        m_showFadeStatus = TutorialKeyStatus.FadingIn;
        yield return new WaitForSeconds(showTime);
        m_showFadeStatus = TutorialKeyStatus.Shown;
    }

    public void HideInput()
    {
        m_Sprite.CrossFadeAlpha(0f, 0f, false);
        m_showFadeStatus = TutorialKeyStatus.Hidden;
    }

    public void ForceShowInput(float showTransparency)
    {
        m_Sprite.CrossFadeAlpha(showTransparency, 0f, false);
        m_showFadeStatus = TutorialKeyStatus.Shown;
    }

}


public class TutorialCanvas : MonoBehaviour {

    //States
    private bool m_showing = false;
    private bool m_fadeReady = false;
    
    public float m_fadeTime;
    public float m_showTime;
    [Range(0.2f, 1f)]
    public float m_showTransparency;
    
    public List<TutorialInput> m_Inputs = new List<TutorialInput>();
    public List<int> m_walkKeysIdxs = new List<int>();
    public List<int> m_clickKeysIdxs = new List<int>();
    public List<int> m_danceKeys = new List<int>();

	// Use this for initialization
	void Start () {
        HideInputs();
	}
	
	// Update is called once per frame
	void Update () {
        //if (m_showing)
        ManageShows();
        //if (m_fadeReady)  
        ManageFades();        
    }

    private void ManageFades()
    {
        m_fadeReady = false;
        foreach (TutorialInput curInput in m_Inputs)
        {
            switch (curInput.m_showFadeStatus)
            {
                case TutorialKeyStatus.Shown:
                {
                    if (Input.GetKeyDown(curInput.m_Control))
                    {
                        StartCoroutine(curInput.FadeOut(m_fadeTime));
                    } else
                    {
                        m_fadeReady = true;
                    }
                    break;
                }
            }
        }
    }

    private void ManageShows()
    {
        m_showing = false;
        foreach (TutorialInput curInput in m_Inputs)
        {
            switch (curInput.m_showFadeStatus)
            {
                case TutorialKeyStatus.FadingIn:
                    {
                        m_showing = true;
                        break;
                    }
                case TutorialKeyStatus.Shown:
                    {
                        m_fadeReady = true;
                        break;
                    }
            }
        }
    }
    private void ShowInputs(List<int> inputsToShow)
    {
        foreach (int curInput in inputsToShow)
        {
            StartCoroutine(m_Inputs[curInput].FadeIn(m_showTime, m_showTransparency));            
        }
    }

    public void ShowWalkKeys()
    {
        ShowInputs(m_walkKeysIdxs);
    }

    public void ShowClickKeys()
    {
        ShowInputs(m_clickKeysIdxs);
    }

    public void ShowDanceKeys()
    {
        ShowInputs(m_danceKeys);
    }
    
    private void HideInputs()
    {
        foreach (TutorialInput curInput in m_Inputs)
        {
            curInput.HideInput();
        }
        m_fadeReady = false;
    }
}


