using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
[RequireComponent(typeof(TextMesh))]
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(SphereCollider))]
public class ThreeDText : MonoBehaviour {

    //Required Components
    private TextMesh myTextMesh;
    private MeshRenderer myMeshRenderer;
    private SphereCollider mySphereCollider;

    //States
    private bool componentsSet = false;
    private bool isActive;
    public bool renderInEditor;
    public GameObject myReferenceObject;
    [SerializeField] private string myText;
    public bool dynamicScaling;
    public float nominalScaleDistance;
    public bool triggerByDistance;
    public float triggerDistance;
    public Vector3 OffsetPosition;
    
	// Use this for initialization
	void Start () {
        SetComponents();
	}
	
    private void SetComponents()
    {
        myTextMesh = GetComponent <TextMesh>();
        myMeshRenderer = GetComponent<MeshRenderer>();
        mySphereCollider = GetComponent<SphereCollider>();
        mySphereCollider.radius = triggerDistance;
        mySphereCollider.isTrigger = true;
        DeactivateText();
        componentsSet = true;
    }

	// Update is called once per frame
	void Update () {


        if (isActive)
        {
            MoveToReferenceObject();
            TurnToCamera();
            ScaleMe();
        }
        else if (!isActive && renderInEditor && !Application.isPlaying)
        {
            ActivateText();
        }
	}

    void OnTriggerEnter(Collider hitMe)
    {
        if (triggerByDistance)
        {
            if (hitMe.CompareTag("Player"))
            {
                ActivateText();
            }
        }
    }

    void OnTriggerExit(Collider hitMe)
    {
        if (triggerByDistance)
        {
            if (hitMe.CompareTag("Player"))
            {
                DeactivateText();
            }
        }
    }

    public void ActivateText()
    {
        if (!componentsSet) SetComponents();
        myTextMesh.text = myText;
        isActive = true;
        myMeshRenderer.enabled = true;
    }

    public void DeactivateText()
    {
        myMeshRenderer.enabled = false;
        isActive = false;
        transform.localScale = Vector3.one;
        mySphereCollider.radius = triggerDistance;
    }

    private void TurnToCamera()
    {
        transform.LookAt(Camera.main.transform);
    }

    private void ScaleMe()
    {
        if (dynamicScaling)
        {
            //!!!WHEN I DO THIS I'M SHRINKING MY SPHERE COLLIDER, TOO!!!!
            float distance = Vector3.Distance(transform.position, Camera.main.transform.position);
            float currentScaleFactor = distance / nominalScaleDistance;
            Vector3 newScale = new Vector3(-currentScaleFactor, currentScaleFactor, currentScaleFactor);
            transform.localScale = newScale;
            mySphereCollider.radius = triggerDistance / currentScaleFactor;//collider must stay same size as intended
        }
    }

    private void MoveToReferenceObject()
    {
        if (myReferenceObject != null)
        {
            Vector3 referencePosition = myReferenceObject.transform.position;
            transform.position = referencePosition + OffsetPosition;
        }
    }

    public void ChangeText(string updateText)
    {
        myText = updateText;
        ActivateText();
    }
}
