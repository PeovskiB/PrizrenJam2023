using UnityEngine;

[System.Serializable]
public struct MovementInfo
{
    public float speed, friction, maxSpeed;
}

public class Movement : MonoBehaviour
{

    [SerializeField]
    public MovementInfo info;

    public float dashSprint, maxStamina, staminaRegen, staminaRegenExhaustedFactor, staminaCostFactor;

    public ShakeData shake;

    public float sprint = 1;
    private float dashTime;
    private float stamina;
    private bool exhausted = false;

    private Vector2 dir;

    private Rigidbody2D body;
    private static Movement instance;

    private static Transform playerTransform;

    void Awake(){
        if(playerTransform == null)
            playerTransform = transform;
    }

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        instance = this;
        stamina = maxStamina;
    }

    void Update()
    {
        dir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        if (!exhausted && stamina > 0 && Input.GetButton("Dash"))
        {
            if (Input.GetButtonDown("Dash"))
                CameraController.Shake(shake);

            sprint = dashSprint;
            SubtractStamina(Time.deltaTime * staminaCostFactor);
        }
        else
            UpdateStamina();

        if (Input.GetButtonUp("Dash"))
            sprint = 1;
    }

    void FixedUpdate()
    {
        if (dir.sqrMagnitude > 1)
            dir.Normalize();

        body.AddForce(dir * info.speed);

        float velMag = body.velocity.magnitude;

        if (velMag > info.maxSpeed * sprint)
            body.velocity = body.velocity.normalized * info.maxSpeed;

        body.AddForce(body.velocity.normalized * -info.friction);
    }

    public static bool SubtractStamina(float amount)
    {
        float stamina = instance.stamina;
        if (stamina - amount >= 0)
        {
            instance.stamina -= amount;
            // UIConnector.SetStamina(instance.stamina, instance.maxStamina);
            return true;
        }
        else
        {
            instance.stamina = 0;
            instance.sprint = 1;
            instance.exhausted = true;
            // UIConnector.SetExhausted(instance.exhausted);
            return false;
        }
    }

    private void UpdateStamina()
    {
        if (stamina < maxStamina)
            stamina += Time.deltaTime * staminaRegen * ((exhausted) ? staminaRegenExhaustedFactor : 1);
        else
        {
            stamina = maxStamina;
            exhausted = false;
            // UIConnector.SetExhausted(exhausted);
            sprint = 1;
        }
        // UIConnector.SetStamina(stamina, maxStamina);
    }

    public static bool IsExhausted()
    {
        return instance.exhausted;
    }

    public static Transform GetPlayerTransform(){
        return playerTransform;
    }
}