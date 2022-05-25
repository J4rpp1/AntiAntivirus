using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{

    [Header("Pickable weapons")]
    public GameObject pistol;
    public GameObject shotgun;
    public GameObject Ar;

    [Header("Patrol points")]
    public bool patrolEnemy;
    public Transform[] points;


    WeaponSystem weaponSystem;
    EnemyWeaponSystem enemyWeaponSystem;
    public static Enemy  instance;

   

    [Header("Don't change!")]
    public bool alertStarted;
    public bool canStartIdle;

    public int currentHp;
    public int maxHp;

    public int alertLevel;

    public float rotateSpeed = 1f;

    public GameObject playerRef;
    public float radius;
    [Range(0,360)]
    public float angle;
    public LayerMask targetMask;
    public LayerMask obstructionMask;
    public bool canSeePlayer;
    public Transform heardSoundPosition;

    private int destPoint = 0;
    private NavMeshAgent agent;
    public float timer;
    public float timer2;
    public float timer3;

    // Start is called before the first frame update
    void Start()
    {
        weaponSystem = GameObject.FindObjectOfType<WeaponSystem>();
        enemyWeaponSystem = GameObject.FindObjectOfType<EnemyWeaponSystem>();
        playerRef = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(FOVRoutine());
        currentHp = maxHp;
        agent = GetComponent<NavMeshAgent>();

        // Disabling auto-braking allows for continuous movement
        // between points (ie, the agent doesn't slow down as it
        // approaches a destination point).
        agent.autoBraking = false;

        GotoNextPoint();
    }
    public void Awake()
    {
        instance = this;
    }
    
    public void Damage()
    {
        currentHp = 0;
    }
    void GotoNextPoint()
    {
        // Returns if no points have been set up
        if (points.Length == 0)
            return;

        // Set the agent to go to the currently selected destination.
        agent.destination = points[destPoint].position;

        // Choose the next point in the array as the destination,
        // cycling to the start if necessary.
        destPoint = (destPoint + 1) % points.Length;
    }
    
    // Update is called once per frame
    void Update()
    {
        
        //n‰kee pelaajan
        if(canSeePlayer)
        {
            timer = 0;
            timer3 = 0;
            Debug.Log("n‰kee pelaajan");
            alertLevel = 2;
            heardSoundPosition = weaponSystem.playerLocation;
            //agent.destination = weaponSystem.playerLocation.position;
            agent.destination = heardSoundPosition.position;
            Vector3 targetDirection = weaponSystem.playerLocation.position - transform.position;
            float singleStep = rotateSpeed * Time.deltaTime;
            Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, singleStep, 0.0f);
            transform.rotation = Quaternion.LookRotation(newDirection);
        }
        if (!canSeePlayer && alertLevel == 1 && canStartIdle)
        {
            Debug.Log("ajastimet");
            timer += 1 * Time.deltaTime;
            

        }
        if (timer > 2)
        {
            Debug.Log("timer valmis");
           
            alertStarted = false;
            agent.isStopped = true;
            
        }
        if(timer > 4)
        {
            Debug.Log("timer2 valmis");
                alertLevel = 0;
            agent.isStopped = false;
            timer = 0;

        }
      
        //StartCoroutine(LookAround());
        if (canSeePlayer)
        {
            canStartIdle = false;
            //Debug.Log("pys‰yt‰");
            
            
        }

        if (currentHp == 0)
        {
            if (enemyWeaponSystem.isPistolEnemy)
                Instantiate(pistol, transform.position, transform.rotation);
            if (enemyWeaponSystem.isShotGunEnemy)
                Instantiate(shotgun, transform.position, transform.rotation);
            if (enemyWeaponSystem.isArEnemy)
                Instantiate(Ar, transform.position, transform.rotation);
            Destroy(gameObject);
        }
        if (!agent.pathPending && agent.remainingDistance < 0.5f && patrolEnemy && alertLevel == 0)
            GotoNextPoint();

        if (alertLevel == 1 && !alertStarted && !canSeePlayer)
        {

        }
           // StartCoroutine(AlertMode1());
       if(alertLevel == 2 && !alertStarted)
        {
            Alert1();
        }
        // StartCoroutine(AlertMode());
        //Debug.Log(timer);
        if (alertStarted)
            timer3 += 1 * Time.deltaTime;
        //Debug.Log(timer3);

        if(timer3 > 1)
        {
            Alert2();
        }
        if(timer3 > 4)
        {

        canStartIdle = true;
        alertStarted = false;
            timer3 = 0;
        alertLevel = 1;

        }

        Debug.Log(alertLevel);
    }


    void Alert1()
    {
        Debug.Log("alertti 1");
            alertStarted = true;
            agent.isStopped = true;
    }
    void Alert2()
    {
            agent.destination = heardSoundPosition.position;
            agent.isStopped = false;
            Debug.Log("toimii");

    }
    public void SoundHeard()
    {
        alertStarted = false;
        Debug.Log("kuulee ‰‰nen");
        heardSoundPosition = weaponSystem.playerLocation;
        alertLevel = 2;
    }
    private IEnumerator FOVRoutine()
    {

        WaitForSeconds wait = new WaitForSeconds(0.2f);

        while (true)
        {
            yield return wait;
            FieldofViewCheck();
        }
    }
   /* IEnumerator AlertMode1()
    {
        alertStarted = true;
        Debug.Log("alertLevel1");
        yield return new WaitForSeconds(3);
        alertLevel = 0;
        alertStarted = false;
    }
    /*IEnumerator AlertMode()
    {
        Debug.Log("alertlevel2");
        alertStarted = true;
        agent.isStopped = true;
        yield return new WaitForSeconds(0.5f);
        agent.destination = heardSoundPosition.position;
        agent.isStopped = false;
        yield return new WaitForSeconds(2);
        canStartIdle = true;
        yield return new WaitForSeconds(4);
       // alertLevel = 1;
        alertStarted = false;
    }
   */
   
    private void FieldofViewCheck()
    {
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, radius, targetMask);

        if (rangeChecks.Length != 0)
        {
            Transform target = rangeChecks[0].transform;
            Vector3 directionToTarget = (target.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, directionToTarget) < angle / 2)
            {
                float distanceToTarget = Vector3.Distance(transform.position, target.position);

                if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionMask))
                    canSeePlayer = true;
                else
                    canSeePlayer = false;
            }
            else
                canSeePlayer = false;
        }
        else if (canSeePlayer)
            canSeePlayer = false;
    }
}
