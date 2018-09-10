using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyControler : MonoBehaviour {

    public NavMeshAgent agent;
    public NavMeshAgent player;
    public Transform pointA;
    public Transform pointB;
    State state = State.goingToA;
    public enum State { goingToA, goingToB, chasing};

    Vector3 pos;

    // Update is called once per frame
    void Update () {
        if (state != State.chasing)
        {
            if (V3Equals(agent.transform.position, pointA.position) && state == State.goingToA)
            {
                state = State.goingToB;
                agent.SetDestination(pointB.position);
            }
            if (V3Equals(agent.transform.position, pointB.position) && state == State.goingToB)
            {
                state = State.goingToA;
                agent.SetDestination(pointA.position);
            }
        }
        else
        {
            if(V3Equals(pos,agent.transform.position))
            {
                state = State.goingToA;
                agent.SetDestination(pointA.position);
            }
        }

    }

    void EnemySees()
    {
        state = State.chasing;
        pos = player.transform.position;
        agent.SetDestination(player.transform.position);
    }

    bool V3Equals(Vector3 a, Vector3 b)
    {
        return Vector3.SqrMagnitude(a - b) <= 50;
    }
}
