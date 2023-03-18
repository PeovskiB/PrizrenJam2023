using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Builder : MonoBehaviour
{

    [SerializeField] Building campFire;
    
    float placemnetOffset = 2f;

    void Update()
    {
        
    }

    void BuildBuilding(Building b){
        //Instantiate(b, transform.position+Vector3.up*placemnetOffset)
    }

}
