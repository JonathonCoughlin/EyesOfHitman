using UnityEngine;
using System.Collections;
using FirstPersonExploration;


[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(AudioSource))]
public class FloatableBalloon : MonoBehaviour {

    //Components
    private MeshRenderer m_Renderer;
    private Animator m_Animator;
    private Rigidbody m_Rigidbody;
    private Collider m_Collider;
    private AudioSource m_AudioSource;
    private Prop m_Prop;

    public BalloonMachine m_Machine;

    public Material[] colors;
    private Material m_color;

    public float m_fullScaleRadius;
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
        m_Collider = GetComponent<Collider>();
        m_Prop = GetComponent<Prop>();
    }

    public void InitializeBalloon(BalloonMachine myMachine)
    {
        m_Machine = myMachine;
        SetComponents();
        RandomizeColor();
        Vector3 localPos = transform.position;
        transform.SetParent(myMachine.transform);
        transform.localPosition = localPos;
        m_Prop.DisableMyBehaviors();
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
        m_Rigidbody.AddForce(new Vector3(Random.Range(0f, 0.5f), 0f, Random.Range(0f, 0.5f)), ForceMode.VelocityChange);
        m_floating = true;
        m_Prop.ActivateAllBehaviors();
        m_Prop.PauseMyCollisions(0.5f);
    }

    protected void PopMe()
    {
        m_AudioSource.PlayOneShot(m_popSound);
        //Hide, explode, stop collisions
        m_Renderer.enabled = false;
        m_Collider.isTrigger = true;
        m_Rigidbody.isKinematic = true;
        m_Rigidbody.AddExplosionForce(m_popForceMag_Speed, transform.position, m_fullScaleRadius * transform.localScale.x * 2f, 0f, ForceMode.VelocityChange);
        if (m_Machine != null) m_Machine.DeadBalloon();
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
