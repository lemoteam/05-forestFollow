using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterForest : MonoBehaviour
{
	public Transform path;
	public GameObject forestElementsContainer;
	public float maxSteerAngle = 45f; 
	public WheelCollider wheelL;
	public WheelCollider wheelR;
	private List<Transform> nodes;
	private List<Transform> forestElNodes;
	private int currentNode = 0;

	private void Start()
	{
		var pathTransforms = path.GetComponentsInChildren<Transform>();
		var forestElements = forestElementsContainer;
		
		nodes = new List<Transform>();
		forestElNodes = new List<Transform>();

		for (var i = 0; i < pathTransforms.Length; i++)
		{
			if (pathTransforms[i] != path.transform)
			{
				nodes.Add(pathTransforms[i]);
			}
		}

		for (int i = 0; i < forestElements.transform.childCount; i++)
		{			
			forestElNodes.Add(forestElements.transform.GetChild(i));
		}
	}

	private void FixedUpdate()
	{
		ApplySteer();
		Move();
		CheckWaypointDistance();
		CheckForestElementsDistance();
	}

	private void Move()
	{
		wheelL.motorTorque = 15f;
		wheelR.motorTorque = 15f;
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

		if (Vector3.Distance(transform.position, nodes[currentNode].position) < 0.8f)
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


	private void CheckForestElementsDistance()
	{
		foreach (var node in forestElNodes)
		{
			var elementForest = node.GetComponent<ElementForest>();

			if (Vector3.Distance(transform.position, node.position) < 1f)
			{
				if (!elementForest.isActive)
				{
					elementForest.activeAnimation();
				}
			} else {
				if (elementForest.isActive)
				{
					elementForest.unactiveAnimation();
				}
			}
		}
	}
}
