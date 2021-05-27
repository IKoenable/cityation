using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReachDestination : VehicleObjective
{
    [SerializeField] public Transform[] Destinations;
    [SerializeField] public bool CompleteInOrder;

    [SerializeField] float Radius = 5; // the distance from the destination that is allowed

    private bool[] isCollected;
    private int nCollected = 0;


    public void Reset()
    {
        Description = "Collect the targets";
    }

    void OnEnable()
    {
        isCollected = new bool[Destinations.Length];
        if (CompleteInOrder)
        { Description += " (collect in order)"; }
    }

    public override void ResetObjectives()
    {
        base.ResetObjectives();
        nCollected = 0;
        isCollected = new bool[Destinations.Length];
        Debug.Log("Everything reset");
    }

    void Update()
    {
        if (CompleteInOrder)
        {
            CheckDestination(nCollected);
        }
        else
        {
            for (int iCheck = 0; iCheck < Destinations.Length; iCheck++)
            {
                CheckDestination(iCheck);
            }
        }
    }


    private void CheckDestination(int iCheck)
    {
        if (!isCollected[iCheck] && Vector3.Distance(transform.position, Destinations[iCheck].position) < Radius)
        {
            isCollected[iCheck] = true;
            nCollected++;
            Debug.Log("Target Collected :D");
            if (nCollected >= Destinations.Length)
                { CompleteObjective(); }
        }
    }

}
