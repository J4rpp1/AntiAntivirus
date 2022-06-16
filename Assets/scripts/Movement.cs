using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Movement : MonoBehaviour
{
	LevelSystem levelSystem;
	WeaponSystem weaponSystem;
	PlayerHp playerHp;
	[SerializeField] private LayerMask groundMask;
	private Camera mainCamera;

	public float walkSpeed = 6f;
	public Transform target;
	public Animator animator;
	public SortingGroup weaponSorting;

	float maxSpeed = 10f;
	float curSpeed;

	float sprintSpeed;
	Rigidbody rb;
	Vector3 moveDirection;
	public SpriteRenderer _renderer;
	void Start()
	{
		playerHp = GameObject.FindObjectOfType<PlayerHp>();
		levelSystem = GameObject.FindObjectOfType<LevelSystem>();
		weaponSystem = GameObject.FindObjectOfType<WeaponSystem>();
		rb = GetComponent<Rigidbody>();
		mainCamera = Camera.main;
		// sprintSpeed = walkSpeed + (walkSpeed / 2);

	}

	void FixedUpdate()
	{
		curSpeed = walkSpeed;
		maxSpeed = curSpeed;

		if (weaponSystem.knifeEquipped)
			walkSpeed = 8.5f;
		if (!weaponSystem.knifeEquipped)
			walkSpeed = 6f;

		if (!levelSystem.planning && !playerHp.isDead)
		{

			moveDirection = new Vector3(
				Input.GetAxisRaw("Horizontal"), 0f,
			 Input.GetAxisRaw("Vertical")).normalized * walkSpeed;

			rb.velocity = Vector3.MoveTowards(rb.velocity, moveDirection, walkSpeed / 8);

			animator.SetFloat("SpeedX", Mathf.Abs(moveDirection.x));
			animator.SetFloat("SpeedZ", Mathf.Abs(moveDirection.z));
			if (Mathf.Abs(moveDirection.x) > 0.1 || Mathf.Abs(moveDirection.z) > 0.1)
				animator.SetBool("Moving", true);
			else
				animator.SetBool("Moving", false);
			Aim();

			if (moveDirection.x > 0.01)
			{
				//Walking right
				_renderer.flipX = true;
			}
			if (moveDirection.x < -0.01)
			{
				//Walking left
				_renderer.flipX = false;
			}
			if (moveDirection.z > 0.1)
			{
				animator.SetBool("Back", true);
				weaponSorting.sortingOrder = -1;
			}
			else
			{
				animator.SetBool("Back", false);
				weaponSorting.sortingOrder = 1;
			}
		}
		/*Vector3 mousePosition = Input.mousePosition;
		Vector3 targetPosition = Camera.main.ScreenToWorldPoint(mousePosition);
		Vector3 relativePos = targetPosition - transform.position;

		Quaternion rotation = Quaternion.LookRotation(relativePos);
		transform.rotation = rotation;*/

	}


	private void Aim()
	{
		var (success, position) = GetMousePosition();
		if (success)
		{
			// Calculate the direction
			var direction = position - transform.position;

			// You might want to delete this line.
			// Ignore the height difference.
			direction.y = 0;

			// Make the transform look in the direction.
			transform.forward = direction;
		}
	}

	private (bool success, Vector3 position) GetMousePosition()
	{
		var ray = mainCamera.ScreenPointToRay(Input.mousePosition);

		if (Physics.Raycast(ray, out var hitInfo, Mathf.Infinity, groundMask))
		{
			// The Raycast hit something, return with the position.
			return (success: true, position: hitInfo.point);
		}
		else
		{
			// The Raycast did not hit anything.
			return (success: false, position: Vector3.zero);
		}
	}







}

