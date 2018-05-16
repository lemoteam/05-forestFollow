using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementForest : MonoBehaviour {

	private Vector3 elementPosition;
	private Vector3 destinationPosition;
	private Vector3 currentPosition;
	public bool isActive;
	private bool isCurrentPositionSet;
	
	private float gemReplaceFraction = 0;
	private float gemReplaceSpeed = 5f;

	private void Start()
	{
		isActive = false;
		elementPosition = transform.position;
		destinationPosition = new Vector3(elementPosition.x, 1, elementPosition.z);
	}


	private void FixedUpdate()
	{
		
		
		if (isActive)
		{			
			if (!(gemReplaceFraction < 1)) return;
			gemReplaceFraction += Time.deltaTime * gemReplaceSpeed;
			transform.position = Vector3.Lerp(currentPosition, destinationPosition, gemReplaceFraction);
			
		} else {
			
			if (!isCurrentPositionSet) {
				currentPosition = transform.position;
				isCurrentPositionSet = true;
			}
			
			if (!(gemReplaceFraction < 1)) return;
			gemReplaceFraction += Time.deltaTime * gemReplaceSpeed;
			transform.position = Vector3.Lerp(currentPosition, elementPosition, gemReplaceFraction);
		}
	}


	public void activeAnimation()
	{
		isActive = true;
		gemReplaceFraction = 0;
		isCurrentPositionSet = false;
		currentPosition = transform.position;
	}



	public void unactiveAnimation() {
		isActive = false;
		gemReplaceFraction = 0;
	}
}
