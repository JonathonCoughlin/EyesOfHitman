using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Collider))]
public class BlendoCut : MonoBehaviour
{

    //GameManagement
    private HitmanGameManager m_Game;
    public bool m_ForceGameTransition;
    public GameState m_TransitionState;
    
    //Target
    public BlendoSpawn m_SpawnTarget;

    //Components
    private Collider m_collider;

    //Events
    public SplineWalker m_startObjectWalking;
    public Material m_costumeSwitch;
    public GameObject m_playerBody;
    public bool m_OnUnicycleAfterCut;


    // Use this for initialization
    void Start()
    {
        SetComponents();
    }

    private void SetComponents()
    {
        m_collider = GetComponent<Collider>();
        m_Game = (HitmanGameManager) FindObjectOfType(typeof(HitmanGameManager));
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnTriggerEnter(Collider other)
    {
        if (this.enabled)
        {
            if (other.gameObject.tag == "Player")
            {
                if (m_ForceGameTransition)
                {
                    m_Game.m_GameState = m_TransitionState;
                    m_Game.TransitionToGameState();
                }

                if (m_startObjectWalking != null)
                {
                    m_startObjectWalking.StartWalking();
                }

                other.gameObject.transform.position = m_SpawnTarget.transform.position;
                other.gameObject.GetComponent<MouseLook>().TriggerBlendoCut(m_SpawnTarget.transform.rotation.eulerAngles);
                GameObject.FindGameObjectWithTag("MainCamera").GetComponent<MouseLook>().TriggerBlendoCut(m_SpawnTarget.transform.rotation.eulerAngles);

                if (m_costumeSwitch != null)
                {
                    m_playerBody.GetComponent<Renderer>().material = m_costumeSwitch;
                }

                if (m_OnUnicycleAfterCut)
                {
                    other.gameObject.GetComponent<FPClownController>().ActivateUnicycle();
                }
                else
                {
                    other.gameObject.GetComponent<FPClownController>().DeactivateUnicycle();
                }


            }
        }
        
    }
}
