using UnityEngine;
using System.Collections;

public class MachineOffButton : MonoBehaviour {

    public BalloonMachine m_Machine;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OffButton()
    {
        m_Machine.Off();
    }
}
