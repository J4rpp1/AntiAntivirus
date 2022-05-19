using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{
    WeaponSystem weaponSystem;
    public static Enemy  instance;

    public bool patrolEnemy;
    public bool alertStarted;

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

    public Transform[] points;
    private int destPoint = 0;
    private NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        weaponSystem = GameObject.FindObjectOfType<WeaponSystem>();
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
        if(canSeePlayer)
        {
            alertLevel = 2;
            heardSoundPosition = weaponSystem.playerLocation;
            agent.destination = weaponSystem.playerLocation.position;
            Vector3 targetDirection = weaponSystem.playerLocation.position - transform.position;
            float singleStep = rotateSpeed * Time.deltaTime;
            Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, singleStep, 0.0f);
            transform.rotation = Quaternion.LookRotation(newDirection);
        }

        if (currentHp == 0)
        {
            Destroy(gameObject);
        }
        if (!agent.pathPending && agent.remainingDistance < 0.5f && patrolEnemy && alertLevel == 0)
            GotoNextPoint();

        if (alertLevel == 1 && !alertStarted)
            StartCoroutine(AlertMode1());

       if(alertLevel == 2 && !alertStarted)
        StartCoroutine(AlertMode());
        
    }

    public void SoundHeard()
    {
        alertStarted = false;
        Debug.Log("kuulee äänen");
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
    IEnumerator AlertMode1()
    {
        alertStarted = true;
        Debug.Log("alertLevel1");
        yield return new WaitForSeconds(1);
        alertLevel = 0;
        alertStarted = false;
    }
    IEnumerator AlertMode()
    {
        alertStarted = true;
        agent.isStopped = true;
        yield return new WaitForSeconds(1);
        agent.destination = heardSoundPosition.position;
        agent.isStopped = false;
        alertLevel = 1;
        alertStarted = false;
    }
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
