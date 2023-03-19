using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{

    public static PlayerStats instance;

    //The ammount of hours it takes to drain the hunger or energy
    [SerializeField] float hungerDuration = 8;
    [SerializeField] float energyDuration = 16;

    [SerializeField] float healthDrainRate = 0.05f;

    [SerializeField] Slider healthBar;
    [SerializeField] Slider hungerBar;
    [SerializeField] Slider energyBar;

    float SleepCooldown = 8;
    float sleepCounter = 0;

    private float minDistanceFromCampfire=10f;


    private float hunger;
    public float Hunger{
        get{
            return hunger;
        }
        set{
            if(value < 0)
                value = 0;
            hunger = value;
            hungerBar.value = value;
        }
    }

    private float energy;
    public float Energy{
        get{
            return energy;
        }
        set{
            if(value < 0)
                value = 0;
            energy = value;
            energyBar.value = value;
        }
    }

    private float health;
    public float Health{
        get{
            return health;
        }
        set{
            if(value < 0)
                value = 0;
            health = value;
            healthBar.value = value;
            if(value == 0)
                Die();
        }
        
    }



    void Awake(){
        if(instance == null)
            instance = this;
        else
            Destroy(gameObject);    
    }

    // Start is called before the first frame update
    void Start()
    {
        Hunger = 1f;
        Energy = 1f;
        Health = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        DrainStats();
        if(sleepCounter > 0)
            sleepCounter -= Time.deltaTime / DayCycle.GetDayLengthInSeconds() * 24 / SleepCooldown;
        if(sleepCounter <= 0  && Input.GetKeyDown(KeyCode.F))
            Sleep();    
    }

    void DrainStats(){
        Hunger -= Time.deltaTime / DayCycle.GetDayLengthInSeconds() * 24 / hungerDuration;
        Energy -= Time.deltaTime / DayCycle.GetDayLengthInSeconds() * 24  / energyDuration;
        if(Hunger <= 0)
            Health -= Time.deltaTime * healthDrainRate;
        if(Energy <= 0)
            Health -= Time.deltaTime * healthDrainRate;    
    }

    void Die(){

    }

    void Sleep(){
        Energy = 1;
        Hunger -= 0.3f;
        sleepCounter = SleepCooldown;
        DayCycle.instance.AddHours(8);
        //Check if a campfire is nearby, if not lose health unless it's day
        GameObject[] campFires = GameObject.FindGameObjectsWithTag("Campfire");
        bool isSafe = false;
        foreach(GameObject c in campFires){
            if(Vector2.Distance(c.transform.position, Movement.GetPlayerTransform().position) <= minDistanceFromCampfire){
                isSafe = true;
                break;
            }
        }
        if(DayCycle.instance.GetHour() > 6 && DayCycle.instance.GetHour() < 17)
            isSafe = true;
        if(!isSafe){
            Health -= 0.2f;
        }

    }

}