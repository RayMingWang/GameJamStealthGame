using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerControler : MonoBehaviour
{


    
    public Camera cam;
    public float camPositionLerpSpeed=0.8f;
    public float camRotationLerpSpeed=0.8f;
    public LayerMask terrainLayer;
    public NavMeshAgent agent;
    [SerializeField]
    private int camState = 0;
	// Update is called once per frame
	void Update ()
    {
        
        
        
	    if(Input.GetMouseButton(0))//Left mouse clicked?
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);//take current mouse position and create ray in that direction
            RaycastHit hit;

            if(Physics.Raycast(ray, out hit,5000f,terrainLayer)/*store ray information in hit*/)//if ray hits something move agent
            {
                agent.SetDestination(hit.point);
            }
        }
	}
    
    
    private void OnTriggerEnter(Collider other) {
        Debug.Log("ttt");
        if (other.tag=="XGuide")
        {
            Debug.Log("xxx");
            camState = 1;
        }

        if (other.tag=="ZGuide")
        {
            Debug.Log("zzz");
            camState = 2;
        }
    }

    private void OnTriggerExit(Collider other)
    {
    
        if (other.tag=="XGuide")
        {
            camState = 0;
        }

        if (other.tag=="ZGuide")
        {
            camState = 0;
        }
    }

    private void UpdateCam()
    {
        Vector3 camNewPosition;
        Vector3 camNEwRotation;
        switch (camState)
        {
                
            case 1:
                camNewPosition = transform.position + Vector3.up * 56;
                break;
            case 2:
                camNewPosition = transform.position + Vector3.up * 56;
                break;
            case 0:
            default:
                camNewPosition = transform.position + Vector3.up * 56;
                break;
           
        }
        
        cam.transform.position = Vector3.Lerp(cam.transform.position, camNewPosition, camPositionLerpSpeed);
    }
}
