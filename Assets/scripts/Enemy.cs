using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{
    PauseMenu pauseMenu;

    [Header("Pickable weapons")]
    public GameObject pistolDrop;
    public GameObject shotgunDrop;
    public GameObject ArDrop;

    [Header("Patrol points")]
    public bool patrolEnemy;
    public Transform[] points;


    WeaponSystem weaponSystem;
    
   

   

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

    public bool isPistolEnemy;
    public bool isShotGunEnemy;
    public bool isArEnemy;

    public Animator animator;
    public float velocity;
    private Vector3 previousPosition;
    private Vector3 velocityVector;
    [SerializeField] WeaponBase pistol = null;
    [SerializeField] WeaponBase shotgun = null;
    [SerializeField] WeaponBase assaultRifle = null;
    public bool notShooting;
    public float fireRate;
    public SpriteRenderer _renderer;

    /*Vector3 worldDeltaPosition;
    Vector3 groundDeltaPosition;
    Vector2 velocity = Vector2.zero;*/

    // weapon socket helps us position our weapon and graphics
    [SerializeField] Transform _weaponSocket = null;


    public WeaponBase EquippedWeapon { get; private set; }
    // Start is called before the first frame update
    void Start()
    {
        
        weaponSystem = GameObject.FindObjectOfType<WeaponSystem>();
        
        playerRef = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(FOVRoutine());
        currentHp = maxHp;
        agent = GetComponent<NavMeshAgent>();

        //agent.updatePosition = false;
        // Disabling auto-braking allows for continuous movement
        // between points (ie, the agent doesn't slow down as it
        // approaches a destination point).
        agent.autoBraking = false;

        GotoNextPoint();
    }
    private void Awake()
    {
        pauseMenu = GameObject.FindObjectOfType<PauseMenu>();
        notShooting = true;

        
        if (isPistolEnemy)
        {
            EquipWeapon(pistol);
        }
        if (isShotGunEnemy)
        {
            EquipWeapon(shotgun);
        }
        if (isArEnemy)
        {
            EquipWeapon(assaultRifle);
        }

    }
    public void EquipWeapon(WeaponBase newWeapon)
    {
        if (EquippedWeapon != null)
        {
            Destroy(EquippedWeapon.gameObject);
        }

        // spawn weapon in the world and hold a reference
        EquippedWeapon = Instantiate
            (newWeapon, _weaponSocket.position, _weaponSocket.rotation);
        // make sure to include it in the player GameObject so it follows
        EquippedWeapon.transform.SetParent(_weaponSocket);
    }

    IEnumerator Shoot()
    {
        Debug.Log("Ampuu");
        notShooting = false;
        yield return new WaitForSeconds(fireRate);
        EquippedWeapon.Shoot();
        notShooting = true;
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
    void OnAnimatorMove()
    {
        transform.position = agent.nextPosition;
    }
    // Update is called once per frame
    void Update()
    {
		velocityVector = (transform.position - previousPosition) /Time.deltaTime;

        //velocity = ((transform.position - previousPosition).magnitude) / Time.deltaTime;
        previousPosition = transform.position;

		if(velocityVector.x > 0.01)
		{
			//Debug.Log("Walking right, with speed "+velocityVector.magnitude);
            animator.SetBool("Move", true);
            _renderer.flipX = false;
        }
		else if(velocityVector.x < -0.01)
		{
			//Debug.Log("Walking left, with speed "+velocityVector.x);
            animator.SetBool("Move", true);
            _renderer.flipX = true;
        }
		else
		{
            animator.SetBool("Move", false);
           // Debug.Log("Standing still");
		}

        if(velocityVector.z > 0.01)
        {
           // Debug.Log("Walking up, with speed " + velocityVector.magnitude);
            animator.SetBool("IsFacingUp", true);
        }
        else if(velocityVector.z < -0.01)
        {
           // Debug.Log("Walking down, with speed " + velocityVector.magnitude);
            animator.SetBool("IsFacingUp", false);

        }
        
        //Debug.DrawRay(transform.position, velocityVector, Color.magenta, 0.05f);

        /* worldDeltaPosition = agent.nextPosition - transform.position;
         groundDeltaPosition.x = Vector3.Dot(transform.right, worldDeltaPosition);
         groundDeltaPosition.y = Vector3.Dot(transform.forward, worldDeltaPosition);
         velocity = (Time.deltaTime > 1e-5f) ? groundDeltaPosition / Time.deltaTime : velocity = Vector2.zero;
         bool shouldMove = velocity.magnitude > 0.025f && agent.remainingDistance > agent.radius;*/

        /*animator.SetBool("Move", true);
        animator.SetFloat("Vertical", previousPosition.x);
        animator.SetFloat("Horizontal", previousPosition.y);*/


        if (canSeePlayer && notShooting && !pauseMenu.pause)
        {
            Debug.Log("saa ampua");
            StartCoroutine(Shoot());

        }
        //n�kee pelaajan
        if(canSeePlayer && !pauseMenu.pause)
        {
            timer = 0;
            timer3 = 0;
            Debug.Log("n�kee pelaajan");
            alertLevel = 2;
            heardSoundPosition = weaponSystem.transform;
            //agent.destination = weaponSystem.transform.position;
            agent.destination = heardSoundPosition.position;
            Vector3 targetDirection = weaponSystem.transform.position - transform.position;
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
        if (canSeePlayer && !pauseMenu.pause)
        {
            canStartIdle = false;
            //Debug.Log("pys�yt�");
            
            
        }

        if (currentHp == 0)
        {
            if (isPistolEnemy)
                Instantiate(pistolDrop, transform.position, transform.rotation);
            if (isShotGunEnemy)
                Instantiate(shotgunDrop, transform.position, transform.rotation);
            if (isArEnemy)
                Instantiate(ArDrop, transform.position, transform.rotation);
            Destroy(gameObject);
        }
        if (!agent.pathPending && agent.remainingDistance < 0.5f && patrolEnemy && alertLevel == 0 && !pauseMenu.pause)
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

      //  Debug.Log(alertLevel);
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
        Debug.Log("kuulee ��nen");
        heardSoundPosition = weaponSystem.transform;
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
