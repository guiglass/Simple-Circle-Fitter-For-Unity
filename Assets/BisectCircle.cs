using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//Credits: Grant Olsen - 2018

using System;

public class BisectCircle : MonoBehaviour {
	//A stand alone script that finds exact center of a circle given 3 points (solves from system of equations for intersection of two perpendicular bisectors).

	public Transform A; //1st coordinate that lies on the circumference of the circle.
	public Transform B; //2nd coordinate that lies on the circumference of the circle.
	public Transform C; //3rd coordinate that lies on the circumference of the circle.

	public Transform center; //Object to be placed at the circle's center point;

	float x_c = 0f; //Circle's center coordinate (X Axis).
	float y_c = 0f; //Circle's center coordinate (Y Axis).
	float r = 0f; //Circle's radius.

	void Update() {
		var pt_A = new Vector2 (A.position.x, A.position.z);
		var pt_B = new Vector2 (B.position.x, B.position.z);
		var pt_C = new Vector2 (C.position.x, C.position.z);

		BisectCircleToCoordinates (ref x_c, ref y_c, ref r, pt_A, pt_B, pt_C);

		if (center) {
			center.position = new Vector3 (x_c, 0, y_c);
		}
	}

	void OnDrawGizmos()	{
		Gizmos.color = Color.yellow;
		Gizmos.DrawWireSphere(new Vector3(x_c, 0, y_c), r);
	}
				
	void BisectCircleToCoordinates(ref float _x, ref float _y, ref float _r, Vector2 _A, Vector2 _B, Vector2 _C) {//finds the center point of a circle on a plane from three coordinates - https://www.algebra.com/algebra/homework/Length-and-distance/Length-and-distance.faq.question.146948.html
		var b1 = new Vector2 ((_A.x + _B.x)/2f, (_A.y + _B.y)/2f); //Find center point between A and B
		var b2 = new Vector2 ((_B.x + _C.x)/2f, (_B.y + _C.y)/2f); //Find center point between A and B

		var m1 = -1 / ((_B.y - _A.y) / (_B.x - _A.x)); //find the slope and it's inverse signed reciprocal to convert direction to perpendicular
		var m2 = -1 / ((_C.y - _B.y) / (_C.x - _B.x)); //find the slope and it's inverse signed reciprocal to convert direction to perpendicular

		/*
		Point Slope Form - Simplification example:
		y - b1.y = m1 (x - b1.x)
		y = m1*x - m1*b1.x + b1.y

		Solve system of equations to find where the two bisectors intersect (at the circle's center):
		step 1: (m2*x) - (m2*b2.x) + b2.y = (m1*x) - (m1*b1.x) + b1.y;
		step 2: (m2*x) - (m2*b2.x) = (m1*x) - (m1*b1.x) + b1.y - b2.y;
		step 3: (m2*x) = (m1*x) - (m1*b1.x) + b1.y - b2.y + (m2*b2.x);
		step 4: (m2*x) - (m1*x) = -(m1*b1.x) + b1.y - b2.y + (m2*b2.x);
		step 5: x(m2 - m1) = -(m1*b1.x) + b1.y - b2.y + (m2*b2.x);
		step 6: x = (-(m1*b1.x) + b1.y - b2.y + (m2*b2.x)) / (m2 - m1);
		*/

		//Results:
		_x = (-(m1*b1.x) + b1.y - b2.y + (m2*b2.x)) / (m2 - m1);
		_y = m1 * _x - m1 * b1.x + b1.y;

		_r = Vector2.Distance(new Vector2 (_x, _y), _A);
	}

}
