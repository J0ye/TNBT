using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCMove : MonoBehaviour
{
    NavMeshAgent _navMeshAgent; 

    // Start is called before the first frame update
    void Start()
    {
        if(GetComponent<NavMeshAgent>() != null)
        {
            _navMeshAgent = GetComponent<NavMeshAgent>();
        }else {
            Debug.LogError("There is no agent on this npc " + gameObject.name);
            this.enabled = false;
        }

        try 
        {
            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<NPCController>().RegisterNPC(this);
        }catch (Exception ex)
        {
            Debug.LogError("Could not register " + gameObject.name + " as an npc. " + ex.Message);
        }
    }

    public void SetDestination(Vector3 target)
    {
        //Debug.Log("Hey, im moving to: " + target);
        _navMeshAgent.SetDestination(target);
    }
}
