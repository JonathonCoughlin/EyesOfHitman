using UnityEngine;
using System.Collections;

public class MirrorMode : MonoBehaviour {

    public GameObject m_MirrorSurface;

    public GameObject m_Player;

    public enum XYZOption { X, Y, Z};
    public XYZOption m_directionFaced;

    public float m_offset;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (m_directionFaced == XYZOption.X)
        {
            m_offset = (m_MirrorSurface.transform.position.x - m_Player.transform.position.x);

            Vector3 mirrorPos = new Vector3();
            mirrorPos.x = m_MirrorSurface.transform.position.x + m_offset;
            mirrorPos.y = m_Player.transform.position.y;
            mirrorPos.z = m_Player.transform.position.z;

            this.transform.position = mirrorPos;
        }

        if (m_directionFaced == XYZOption.Y)
        {
            m_offset = (m_MirrorSurface.transform.position.y - m_Player.transform.position.y);

            Vector3 mirrorPos = new Vector3();
            mirrorPos.y = m_MirrorSurface.transform.position.y + m_offset;
            mirrorPos.x = m_Player.transform.position.x;
            mirrorPos.z = m_Player.transform.position.z;

            this.transform.position = mirrorPos;
        }

        if (m_directionFaced == XYZOption.Z)
        {
            m_offset = (m_MirrorSurface.transform.position.z - m_Player.transform.position.z);

            Vector3 mirrorPos = new Vector3();
            mirrorPos.z = m_MirrorSurface.transform.position.z + m_offset;
            mirrorPos.y = m_Player.transform.position.y;
            mirrorPos.x = m_Player.transform.position.x;

            this.transform.position = mirrorPos;
        }
    }
}
