using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{
  

    WeaponSystem weaponSystem;
    public static Enemy  instance;

    private bool rotating;

    public bool patrolEnemy;
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
        if (!canSeePlayer && alertStarted && alertLevel == 2 && !rotating && canStartIdle)
            StartCoroutine(LookAround());
        if (canSeePlayer)
        {
            canStartIdle = false;
            //Debug.Log("pysäytä");
            StopCoroutine(LookAround());
            StopCoroutine(RotateMe(Vector3.up * 90, 0.8f));
            StopCoroutine(RotateMe(Vector3.up * -90, 0.8f));
           
        }

        if (currentHp == 0)
        {
            Destroy(gameObject);
        }
        if (!agent.pathPending && agent.remainingDistance < 0.5f && patrolEnemy && alertLevel == 0)
            GotoNextPoint();

        if (alertLevel == 1 && !alertStarted && !canSeePlayer)
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
        yield return new WaitForSeconds(3);
        alertLevel = 0;
        alertStarted = false;
    }
    IEnumerator AlertMode()
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
        alertLevel = 1;
        alertStarted = false;
    }
    IEnumerator LookAround()
    {
        Debug.Log("pyörrrr");
        canStartIdle = false;
        yield return new WaitForSeconds(2);
        
        StartCoroutine(RotateMe(Vector3.up * 90, 0.8f));
     
        yield return new WaitForSeconds(2);
        StartCoroutine(RotateMe(Vector3.up * -90, 0.8f));

        yield return new WaitForSeconds(2);
        StartCoroutine(RotateMe(Vector3.up * -90, 0.8f));
        Debug.Log("pyörähys loppu");
    }
    IEnumerator RotateMe(Vector3 byAngles, float inTime)
    {
        rotating = true;
        Debug.Log("pyörähtää");
        var fromAngle = transform.rotation;
        var toAngle = Quaternion.Euler(transform.eulerAngles + byAngles);
        for (var t = 0f; t <= 1; t += Time.deltaTime / inTime)
        {
            transform.rotation = Quaternion.Slerp(fromAngle, toAngle, t);

            yield return null;
        }

        transform.rotation = toAngle;
        rotating = false;
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
