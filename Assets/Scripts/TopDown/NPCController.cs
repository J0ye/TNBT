using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCController : MonoBehaviour
{
    public LayerMask m_TargetLayer;

    private List<NPCMove> m_Npcs = new List<NPCMove>(); 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(1))
        {
            Ray ray = GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
            RaycastHit rayHit;
            if (Physics.Raycast(ray, out rayHit, m_TargetLayer)) 
            {
                //Debug.Log("Commanding units to: " + rayHit.point);
                foreach (NPCMove npc in m_Npcs)
                {
                    npc.SetDestination(rayHit.point);
                }
            }
        }
    }

    public void RegisterNPC(NPCMove _Target)
    {
        if(!CheckNpcsForElement(_Target))
        {
            m_Npcs.Add(_Target);
        }
    }

    private bool CheckNpcsForElement(NPCMove _Target)
    {
        foreach (NPCMove npc in m_Npcs)
        {
            if(npc.gameObject == _Target.gameObject)
            {
                return true;
            }
        }

        return false;
    }
}
