using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{

    [SerializeField] float wanderSpeed;
    [SerializeField] float runSpeed;
    [SerializeField] float wanderRange;
    [SerializeField] float wanderMaxDistance;

    //Also used for fleeing
    [SerializeField] float aggroDistance;
    [SerializeField] float deAggroDistance;

    [SerializeField] bool isAggresive = false;

    Vector2 wayPoint;

    private eEnemyState enemyState;

    // Start is called before the first frame update
    void Start()
    {
        enemyState = eEnemyState.WANDER;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateState();
        if(enemyState == eEnemyState.WANDER)
            Wander();
        else if(enemyState == eEnemyState.FLEE)
            Flee();
        else
            Aggro();        
    }

    void Wander(){
        // transform.position = Vector2.Lerp(transform.position, wayPoint, wanderSpeed * Time.deltaTime);
        Vector2 dir = wayPoint-(Vector2)transform.position;
        dir.Normalize();
        transform.position += (Vector3) (wanderSpeed*Time.deltaTime*dir);
        if(Vector2.Distance(transform.position, wayPoint) < wanderRange)
            SetNewWaypoint();
    }

    void Flee(){
        Vector2 dir = transform.position-Movement.GetPlayerTransform().position;
        dir.Normalize();
        // Vector2.Lerp(transform.position, transform.position + (Vector3)dir, wanderSpeed * Time.deltaTime);
        transform.position += (Vector3) (runSpeed*Time.deltaTime*dir);
    }

    void Aggro(){
        Vector2 dir = Movement.GetPlayerTransform().position-transform.position;
        dir.Normalize();
        transform.position += (Vector3) (runSpeed*Time.deltaTime*dir);
    }

    void SetNewWaypoint(){
        wayPoint = (Vector2)transform.position + new Vector2(Random.Range(-wanderMaxDistance, wanderMaxDistance), Random.Range(-wanderMaxDistance, wanderMaxDistance));
    }

    bool CheckIfPlayerWithinDistance(float d){
        if(Vector2.Distance(transform.position, Movement.GetPlayerTransform().position) <= d)
            return true;
        return false;  
    }

    void UpdateState(){
        if(enemyState == eEnemyState.WANDER){
            if(CheckIfPlayerWithinDistance(aggroDistance)){
                if(isAggresive)
                    enemyState = eEnemyState.AGGRO;
                else
                    enemyState = eEnemyState.FLEE;
            }
        }else{
            if(Vector2.Distance(transform.position, Movement.GetPlayerTransform().position) > deAggroDistance){
                enemyState = eEnemyState.WANDER;
                SetNewWaypoint();
            }
        }
    }

}