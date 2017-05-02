using UnityEngine;
using System.Collections;
using FirstPersonExploration;

public class MachineOffButton : Button
{

    public BalloonMachine m_Machine;

    public override void RegisterClick()
    {
        base.RegisterClick();
        m_Machine.Off();
    }

}
