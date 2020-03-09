using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Chase : MonoBehaviour
{
    public Transform goal;
    public Transform player;
    
    // Start is called before the first frame update
    void Start()
    {
        //NavMeshAgent agent = GetComponent<NavMeshAgent>();
        //agent.destination = goal.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(player.position, this.transform.position)<10)
        {
           // Vector3 direction = player.position - this.transform.position;

           // this.transform.position = Quaternion.Slerp(this.transform.rotation,
                                          // Quaternion.LookRotation(direction), 0.1f);
            NavMeshAgent agent = GetComponent<NavMeshAgent>();
            agent.destination = goal.position;
        }
       
        
    }
}
