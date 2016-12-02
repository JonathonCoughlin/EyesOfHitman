using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Collider))]
public class BlendoCut : MonoBehaviour
{

    //Target
    public BlendoSpawn m_SpawnTarget;

    //Components
    private Collider m_collider;

    // Use this for initialization
    void Start()
    {
        SetComponents();
    }

    private void SetComponents()
    {
        m_collider = GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.transform.position = m_SpawnTarget.transform.position;
            other.gameObject.transform.rotation = m_SpawnTarget.transform.rotation;
        }
    }
}
