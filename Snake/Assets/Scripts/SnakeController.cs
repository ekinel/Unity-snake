using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class SnakeController : MonoBehaviour
{
	public List<Transform> Tails;
	[Range(0, 3)]
	public float BonesDistance;
	public GameObject BonePrefab;

	private Transform _transform;

	[Range(0, 4)]
	public float Speed;

	public UnityEvent OnEat;

	private void Start()
	{
		_transform = GetComponent<Transform>();
	}

	private void Update()
	{
		MoveSnake(_transform.position + _transform.forward * Speed);

		float angel = Input.GetAxis("Horizontal") * 4;
		_transform.Rotate(0, angel, 0);
	}

	private void MoveSnake(Vector3 newPosition)
	{
		float squarDistance = BonesDistance * BonesDistance;
		Vector3 previousPosition = _transform.position;

		foreach(var bone in Tails)
		{
			if ((bone.position - previousPosition).sqrMagnitude > squarDistance)
			{
				var temp = bone.position;
				bone.position = previousPosition;
				previousPosition = temp;
			}
			else break;
		}

		_transform.position = newPosition;
	}

	private void OnCollisionEnter(Collision collision)
	{
		System.Random random = new System.Random();

		int x = random.Next(-18, 18);
		int z = random.Next(-9, 26);


		if (collision.gameObject.tag == "Food")
		{
			//Destroy(collision.gameObject);
			collision.gameObject.transform.position = new Vector3(x, 0.5f, z); 

			var bone = Instantiate(BonePrefab);
			Tails.Add(bone.transform); 

			if(OnEat != null)
			{
				OnEat.Invoke(); 
			}
		}

		if (collision.gameObject.tag == "Enemy")
		{
			SceneManager.LoadScene("End");
		}

	}
}
