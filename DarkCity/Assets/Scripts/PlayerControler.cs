using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerControler : MonoBehaviour
{


    [Header("Cam Setting")]
    public Camera cam;
    public float camPositionLerpSpeed=0.8f;
    public float camRotationLerpSpeed=0.8f;
    public LayerMask terrainLayer;
    public NavMeshAgent agent;
    [SerializeField]
    private int camState = 0;

    [Header("Lightning")] public Light flashLight;
    public bool flashLightOn = false;
    public float flashLightOnTime = 2f;



    public LayerMask _layerMask;
    GameObject spawnedClicker;
    public enum State { ClickerCreated, ClickerDestroyed };
    public enum Speed { walking, running };

    Speed speed = Speed.running;

    public GameObject Clicker;
    State state = State.ClickerDestroyed;

    float timer = 10f;





    // Update is called once per frame
    void Update ()
    {
        
        UpdateCam();
        UpdateFlashLight();
        if (Input.GetKeyDown(KeyCode.S))
        {
            agent.isStopped = true;
            speed = Speed.walking;
        }
        if (state == State.ClickerCreated)
        {
            timer -= Time.deltaTime;
            if (timer < 0)
            {
                DestroyClickerState();
                Destroy(spawnedClicker);

            }
        }
        if (V3Equals(Clicker.transform.position, agent.transform.position))
        {
            state = State.ClickerDestroyed;
            agent.speed = 8f;
            speed = Speed.walking;

        }
        if (Input.GetMouseButtonDown(0))//Left mouse clicked?
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);//take current mouse position and create ray in that direction
            RaycastHit hit;

            if(Physics.Raycast(ray, out hit,Mathf.Infinity,terrainLayer)/*store ray information in hit*/)//if ray hits something move agent
            {
                if (hit.collider.tag != "wall")
                {
                    if (hit.collider.tag == "ClickerTag")
                    {
                        agent.speed = 20f;
                        speed = Speed.running;
                        timer = 2f;
                        hit.transform.GetComponent<WalkMark>().SwitchMark();
                        //
                        //
                        //ADD SOUND HERE
                        //
                        //
                    }
                    else
                    {
                        agent.isStopped = false;
                        agent.SetDestination(hit.point);


                        if (state == State.ClickerDestroyed)
                        {

                            state = State.ClickerCreated;

                            Clicker.transform.position = hit.point;
                            spawnedClicker = Instantiate(Clicker);
                            spawnedClicker.AddComponent<ClickerScript>();


                        }
                        else
                        {
                            DestroyClickerState();
                            Destroy(spawnedClicker);

                            state = State.ClickerCreated;

                            Clicker.transform.position = hit.point;
                            spawnedClicker = Instantiate(Clicker);
                            spawnedClicker.AddComponent<ClickerScript>();
                        }
                    }

                }
            }
        }
	}

    void DestroyClickerState()
    {
        state = State.ClickerDestroyed;
        agent.speed = 8f;
        speed = Speed.walking;
        timer = 10f;
    }

    bool V3Equals(Vector3 a, Vector3 b)
    {
        return Vector3.SqrMagnitude(a - b) <= 1;
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
                    camNewRotation = new Vector3(125,-90,-180);

                }
                else
                {
                    camNewPosition = transform.position+ Vector3.up * 56 + Vector3.right * 25;
                    camNewRotation = new Vector3(45,-90,0);
                }

                break;
            case 2:
                
                if (transform.rotation.eulerAngles.y>90&&transform.rotation.eulerAngles.y<270)
                {
                    camNewPosition = transform.position + Vector3.up * 56 + Vector3.forward * 25;
                    camNewRotation = new Vector3(125,0,180);
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


    private void UpdateFlashLight()
    {
        if (Input.GetMouseButtonDown(1)&&!flashLightOn)
        {
            flashLight.enabled = true;
            flashLightOn = true;
            StartCoroutine("ChargeFlashLight");
        }
    }

    IEnumerator ChargeFlashLight()
    {
        yield return new WaitForSeconds(flashLightOnTime);
        flashLight.enabled = false;
        flashLightOn = false;
        
    }
}
