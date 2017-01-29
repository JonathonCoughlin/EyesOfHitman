using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SplineWalker))]
public class CarScript : MonoBehaviour {

    public ClownSpawner m_Spawner;
    private SplineWalker m_Walker;

    private int lastCurve;
    public int curveIDtoSpawn;

	// Use this for initialization
	void Start () {
        SetComponents();
        lastCurve = m_Walker.CurrentCurve();
	}

    private void SetComponents()
    {
        m_Walker = GetComponent<SplineWalker>();
    }
	
	// Update is called once per frame
	void Update () {
        int currentCurve = m_Walker.CurrentCurve();
        if (currentCurve > lastCurve)
        {
            if (currentCurve == curveIDtoSpawn)
            {
                m_Spawner.PoolAndSpawn(0.3f, 10, 0.3f);
            }
        }
        lastCurve = currentCurve;
	}

    
}
