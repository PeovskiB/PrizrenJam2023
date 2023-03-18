using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BerryBush : MonoBehaviour
{

    [SerializeField] private float regrowTime = 120f;
    private float regrowTimer;
    private bool hasBerries;

    [SerializeField] private Transform berries;

    private static Transform playerTransform;

    private float pickRadius = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        hasBerries = true;
        if(playerTransform == null)
            playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        Grow();
        if(Input.GetKeyDown(KeyCode.E) && CheckIfInRadius() && hasBerries)
            Pick();

    }

    void Grow(){
        if(hasBerries)
            return;
        regrowTimer -= Time.deltaTime;
        if(regrowTimer <= 0){
            regrowTimer = regrowTime;
            RegenerateBerries();
        }
    }

    void Pick(){
        hasBerries = false;
        berries.gameObject.SetActive(false);
        regrowTimer = regrowTime;
    }

    void RegenerateBerries(){
        hasBerries = true;
        berries.gameObject.SetActive(true);
    }

    bool CheckIfInRadius(){
        if(Vector2.Distance(transform.position, playerTransform.position) <= pickRadius)
            return true;
        return false;    
    }




}
