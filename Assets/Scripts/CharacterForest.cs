using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterForest : MonoBehaviour
{
	public Transform path;
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
		Move();
	}

	private void Move()
	{
		
	}
}
