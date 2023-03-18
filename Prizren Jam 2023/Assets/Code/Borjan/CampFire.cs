using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class CampFire : MonoBehaviour
{

    float strenght = 1f;

    float duration = 15f;

    [SerializeField] private Transform fireSprite;
    [SerializeField] private Light2D fireLight;

    // Start is called before the first frame update
    void Start()
    {
        strenght = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        strenght -= Time.deltaTime/duration;
        if(fireSprite != null)
            fireSprite.localScale = new Vector2(strenght, strenght);
        if(fireLight != null)
            fireLight.intensity = strenght;    
        if(strenght <= 0)
            Destroy(gameObject);
    }
}
