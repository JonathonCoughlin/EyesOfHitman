using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]
public class SimplePlayerController : MonoBehaviour
{

    public float m_Speed;

    //Components
    private Rigidbody m_rigidbody;

    // Use this for initialization
    void Start()
    {
        SetComponents();
    }

    private void SetComponents()
    {
        m_rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void FixedUpdate()
    {
        float xVel = 0f;
        float zVel = 0f;
        
        if (Input.GetKey(KeyCode.W)) { xVel += m_Speed; }
        if (Input.GetKey(KeyCode.A)) { zVel += m_Speed; }
        if (Input.GetKey(KeyCode.S)) { xVel -= m_Speed; }
        if (Input.GetKey(KeyCode.D)) { zVel -= m_Speed; }
                      
        Vector3 localVel = new Vector3(xVel, 0f, zVel);
        m_rigidbody.velocity = transform.TransformVector(localVel);
    }
}
