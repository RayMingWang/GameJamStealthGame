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
        
        UpdateCam();
        
	    if(Input.GetMouseButton(0))//Left mouse clicked?
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);//take current mouse position and create ray in that direction
            RaycastHit hit;

            if(Physics.Raycast(ray, out hit,Mathf.Infinity,terrainLayer)/*store ray information in hit*/)//if ray hits something move agent
            {
                agent.SetDestination(hit.point);
            }
        }
	}
    
    
    private void OnTriggerEnter(Collider other) {
      
        if (other.tag=="XGuide")
        {
          
            camState = 1;
        }

        if (other.tag=="ZGuide")
        {
            
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
        Vector3 camNewRotation;
        switch (camState)
        {
                
            case 1:
                if (transform.rotation.eulerAngles.y>0&&transform.rotation.eulerAngles.y<180)
                {
                    camNewPosition = transform.position+ Vector3.up * 56 - Vector3.right * 20;
                    camNewRotation = new Vector3(125,-90,-90);
                    
                }
                else
                {
                    camNewPosition = transform.position+ Vector3.up * 56 + Vector3.right * 25;
                    camNewRotation = new Vector3(125,90,90);
                }

                break;
            case 2:
                
                if (transform.rotation.eulerAngles.y>90&&transform.rotation.eulerAngles.y<270)
                {
                    camNewPosition = transform.position + Vector3.up * 56 + Vector3.forward * 25;
                    camNewRotation = new Vector3(125,0,0);
                }
                else
                {
                    camNewPosition = transform.position+ Vector3.up * 56 - Vector3.forward * 14;
                    camNewRotation = new Vector3(65,0,0);
                }
                break;
                
            case 0:
            default:
                camNewPosition = transform.position + Vector3.up * 56;
                camNewRotation = new Vector3(90,0,0);
                break;
           
        }
        
        cam.transform.position = Vector3.Lerp(cam.transform.position, camNewPosition,
            camPositionLerpSpeed*Time.deltaTime);
        cam.transform.rotation = Quaternion.Lerp(cam.transform.rotation, Quaternion.Euler( camNewRotation), 
            camRotationLerpSpeed*Time.deltaTime);
    }
}
