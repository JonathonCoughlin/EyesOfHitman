using UnityEngine;
using System.Collections;


[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(SphereCollider))]
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(AudioSource))]
public class FloatableBalloon : MonoBehaviour {

    //Components
    private MeshRenderer m_Renderer;
    private Animator m_Animator;
    private Rigidbody m_Rigidbody;
    private SphereCollider m_SphereCollider;
    private AudioSource m_AudioSource;

    public BalloonMachine m_Machine;

    public Material[] colors;
    private Material m_color;

    public float m_bouyancyForceMag;
    public AudioClip m_popSound;
    public float m_popForceMag_Speed;

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
        m_AudioSource = GetComponent<AudioSource>();
        m_SphereCollider = GetComponent<SphereCollider>();
    }

    public void InitializeBalloon(BalloonMachine myMachine)
    {
        m_Machine = myMachine;
        SetComponents();
        RandomizeColor();
        Vector3 localPos = transform.position;
        transform.SetParent(myMachine.transform);
        transform.localPosition = localPos;
    }

    private void RandomizeColor()
    {
        int colorIndex = Random.Range(0, colors.Length - 1);
        m_color = colors[colorIndex];
        m_Renderer.material = m_color;
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
        m_Animator.SetBool("Inflate", true);
        if (pausedInflationState != 0f)
        {
            m_Animator.speed = 1f;
            m_Animator.ForceStateNormalizedTime(pausedInflationState);
        }
    }

    public void PauseInflate()
    {
        pausedInflationState = m_Animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
        m_Animator.speed = 0f;
    }

    public void FloatAway()
    {
        transform.SetParent(null);
        m_Rigidbody.isKinematic = false;
        m_Rigidbody.useGravity = true;
        m_floating = true;
    }

    protected void PopMe()
    {
        m_AudioSource.PlayOneShot(m_popSound);
        //Hide, explode, stop collisions
        m_Renderer.enabled = false;
        m_SphereCollider.isTrigger = true;
        m_Rigidbody.isKinematic = true;
        m_Rigidbody.AddExplosionForce(m_popForceMag_Speed, transform.position, m_SphereCollider.radius * 2f, 0f, ForceMode.VelocityChange);
        Destroy(gameObject, m_popSound.length);
    }

    protected void OnCollisionEnter(Collision hitDetails)
    {
        if (hitDetails.gameObject.tag == "sharp")
        {
            PopMe();
        }
    }
}
