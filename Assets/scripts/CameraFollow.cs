using UnityEngine;

public class CameraFollow : MonoBehaviour
{
	LevelSystem levelSystem;

	public Transform crosshair;
	public Transform player;

	public float smoothSpeed = 0.125f;
	public Vector3 offset;
	Vector3 vec;
	[SerializeField] private Camera mainCamera;
	private void Awake()
	{
		levelSystem = GameObject.FindObjectOfType<LevelSystem>();
	}
	void LateUpdate()
	{
		if (!levelSystem.planning)
		{
			Vector3 playerPosition = player.position + offset;
			Vector3 desiredPosition = (crosshair.position + playerPosition) / 2f;
			Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
			transform.position = smoothedPosition;

		}
		if (levelSystem.planning)
		{
			vec = transform.localPosition;
			vec.y = 0;
			vec.x += Input.GetAxis("Horizontal") * Time.deltaTime * 20;
			vec.z += Input.GetAxis("Vertical") * Time.deltaTime * 20;
			transform.localPosition = vec + offset;
		}
	}
}
