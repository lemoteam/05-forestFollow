using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterForest : MonoBehaviour
{
	public Transform path;
	public float maxSteerAngle = 45f; 
	public WheelCollider wheelL;
	public WheelCollider wheelR;
	private List<Transform> nodes;
	private int currentNode = 0;

	private void Start()
	{
		var pathTransforms = path.GetComponentsInChildren<Transform>();
		nodes = new List<Transform>();

		for (var i = 0; i < pathTransforms.Length; i++)
		{
			if (pathTransforms[i] != path.transform)
			{
				nodes.Add(pathTransforms[i]);
			}
		}
	}

	private void FixedUpdate()
	{
		ApplySteer();
		Move();
		CheckWaypointDistance();
	}

	private void Move()
	{
		wheelL.motorTorque = 20f;
		wheelR.motorTorque = 20f;
	}

	private void ApplySteer()
	{
		var relativeVector = transform.InverseTransformPoint(nodes[currentNode].position);
		var wheelAngle = (relativeVector.x / relativeVector.magnitude) * maxSteerAngle;
		wheelL.steerAngle = wheelAngle;
		wheelR.steerAngle = wheelAngle;
		//Debug.Log(wheelAngle);
	}

	private void CheckWaypointDistance()
	{
		//Debug.Log(Vector3.Distance(transform.position, nodes[currentNode].position).ToString());

		if (Vector3.Distance(transform.position, nodes[currentNode].position) < 0.6f)
		{
			if (currentNode == nodes.Count - 1)
			{
				currentNode = 0;
				Debug.Log(currentNode);
			}
			else
			{
				currentNode++;
				Debug.Log(currentNode);
			}
		}
	}
}
