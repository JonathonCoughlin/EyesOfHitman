using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(Collider))]
public class CollisionBarker : MonoBehaviour
{

    //States
    public bool m_speaking { get; private set; }

    //Barks
    public List<AudioClip> m_barks = new List<AudioClip>();

    //Components
    private AudioSource m_Voice;
    private Collider m_Collider;

    // Use this for initialization
    void Start()
    {
        SetComponents();
    }

    private void SetComponents()
    {
        m_Voice = GetComponent<AudioSource>();
        m_Collider = GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player") { Bark(); }
    }

    private void Bark()
    {
        int barkID = Random.Range(0, m_barks.Count);
        m_Voice.PlayOneShot(m_barks[barkID]);
        Debug.Log("Bark ID: " + barkID);
    }
}
