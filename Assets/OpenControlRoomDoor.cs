using UnityEngine;
using System.Collections;

public class OpenControlRoomDoor : MonoBehaviour {

    private AssassinationCues m_Cues;
    private Collider m_Collider;

	// Use this for initialization
	void Start () {
        m_Cues = (AssassinationCues)FindObjectOfType(typeof(AssassinationCues));
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            m_Cues.CostumeOn();
        }
    }

}
