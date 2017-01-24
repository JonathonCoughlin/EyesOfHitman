using UnityEngine;
using System.Collections;


[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(Animator))]
public class FloatableBalloon : MonoBehaviour {

    //Components
    private MeshRenderer m_Renderer;
    private Animator m_Animator;
    private Rigidbody m_Rigidbody;
    private Collider m_Collider;

    public BalloonMachine m_Machine;

    public Material[] colors;
    private Material m_color;

    public float m_bouyancyForceMag;

    //States
    private float pausedInflationState = 0f;
    private bool m_floating;


    // Use this for initialization
    void Start () {
        SetComponents();
	}

    private void SetComponents()
    {
        m_Renderer = GetComponent<MeshRenderer>();
        m_Animator = GetComponent<Animator>();
        m_Rigidbody = GetComponent<Rigidbody>();
    }

    public void InitializeBalloon(BalloonMachine myMachine)
    {
        m_Machine = myMachine;
        RandomizeColor();
    }

    private void RandomizeColor()
    {
        int colorIndex = Random.Range(0, colors.Length - 1);
        m_color = colors[colorIndex];
        m_Renderer.materials[0] = m_color;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        if (m_floating)
        {
            m_Rigidbody.AddForce(Vector3.up * m_bouyancyForceMag);
        }
    }

    public void Inflate()
    {
        m_Animator.SetBool("Inflating", true);
        if (pausedInflationState != 0f)
        {
            m_Animator.ForceStateNormalizedTime(pausedInflationState);
        }
    }

    public void PauseInflate()
    {
        pausedInflationState = m_Animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
    }

    public void FloatAway()
    {
        m_floating = true;
    }
}
