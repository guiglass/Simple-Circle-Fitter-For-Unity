//Credits: Grant Olsen - 2018

using UnityEngine;

public class FitCircle : MonoBehaviour {
	//A stand alone script that Aproximates the center of a circle from N>=3 coordinates.

	public Transform[] coordinates; //Points that approximately lie on the circumference of a circle (must contain at least 3 coordinates).

	public Transform center; //Object to be placed at the circle's center point;

	float x_c = 0f; //Circle's center coordinate (X Axis).
	float y_c = 0f; //Circle's center coordinate (Y Axis).
	float r = 0f; //Circle's radius.

	void Update() {
		FitCircleToCoordinates (ref x_c, ref y_c, ref r, coordinates);

		if (center) {
			center.position = new Vector3 (x_c, 0, y_c);
		}
	}

	void OnDrawGizmos()	{
		Gizmos.color = Color.cyan;
		Gizmos.DrawWireSphere(new Vector3(x_c, 0, y_c), r);
	}
		
	public static void FitCircleToCoordinates(ref float _x, ref float _y, ref float _r, Transform[] C) { //Compute the aproximate center and radius of a circle given an array of coordinates.
		float[][] A = new float[C.Length][]; 
		for (int n = 0; n < A.Length; n++) {
			A [n] = new float[] {-2f*C[n].position.x, -2f*C[n].position.z, 1};
		}
		float[] b = new float[C.Length]; 
		for (int n = 0; n < A.Length; n++) {
			b [n] = -(Mathf.Pow(C[n].position.x,2) + Mathf.Pow(C[n].position.z,2));
		}

		var AT = Transpose(A);
		var ATA_inv = inv_3x3 (dot (AT, A));
		var Atb = dot (AT, b);
		var W = dot (ATA_inv, Atb);

		//Results:
		_x = W [0]; //Circle's center coordinate (X Axis)
		_y = W [1]; //Circle's center coordinate (Y Axis)
		_r = Mathf.Sqrt (Mathf.Pow (_x, 2) + Mathf.Pow (_y, 2) - W [2]); //Circle's radius
	}

	//Math Helpers:
	static float[][] inv_3x3(float[][] m1) {//Compute multiplicative inverse of a 3x3 matrix - patrickJMT - https://www.youtube.com/watch?v=YvjkPF6C_LI
		var d = (1.0f / det_3x3 (m1)); //get the determinate for the input 3x3 matrix

		//This is the output "inverse" matrix (using matrix of minors approach).
		//First find the 2x2 determinates for each minor (eg. det_2x2(m1[1][1],m1[1][2],m1[2][1],m1[2][2]) which contains all cells except 1st row/1st col, next is all but 1st row/2nd col, and so on..  ).
		//Then apply cofactors (+-+,-+-,+-+) on all columns and rows.
		//Then rotate around diagional (notice how [r1_c1*, r2_c2*, r3_c3*] are still in original positions).
		//Then multiply the input matrix's determinate "d" on all columns.

		return new float[3][] { //create new matrix to store the permutations
			new float[] { //create row 1
				+det_2x2 (m1[1][1], m1[1][2], m1[2][1], m1[2][2]) * d,//r1_c1*
				-det_2x2 (m1[0][1], m1[0][2], m1[2][1], m1[2][2]) * d,//r2_c1
				+det_2x2 (m1[0][1], m1[0][2], m1[1][1], m1[1][2]) * d,//r3_c1
			},
			new float[] { //create row 2
				-det_2x2 (m1[1][0], m1[1][2], m1[2][0], m1[2][2]) * d,//r1_c2
				+det_2x2 (m1[0][0], m1[0][2], m1[2][0], m1[2][2]) * d,//r2_c2*
				-det_2x2 (m1[0][0], m1[0][2], m1[1][0], m1[1][2]) * d,//r3_c2
			},
			new float[] { //create row 3
				+det_2x2 (m1[1][0], m1[1][1], m1[2][0], m1[2][1]) * d,//r1_c3
				-det_2x2 (m1[0][0], m1[0][1], m1[2][0], m1[2][1]) * d,//r2_c3
				+det_2x2 (m1[0][0], m1[0][1], m1[1][0], m1[1][1]) * d,//r3_c3*
			}
		};
	}

	static float det_3x3(float[][] m1) { //Compute determinate of 3x3 matrix
		var a = m1[0][0] * det_2x2 (m1 [1][1], m1 [1][2], m1 [2][1], m1 [2][2]);
		var b = m1[0][1] * det_2x2 (m1 [1][0], m1 [1][2], m1 [2][0], m1 [2][2]);
		var c = m1[0][2] * det_2x2 (m1 [1][0], m1 [1][1], m1 [2][0], m1 [2][1]);
		return a - b + c;
	}

	static float det_2x2(float w,float x,float y,float z) { //Compute determinate of 2x2 matrix
		return ((w * z) - (y * x));
	}

	static float[] dot(float[][] m1, float[] m2) { //Multiplying multidimensional matrix by a one dimensional matrix (Dot Product)
		var mo = new float[m1.Length];
		for (int r = 0; r < m1.Length; r++) {
			var s = 0.0f; //sum of cols and rows	
			for (int c = 0; c < m2.Length; c++) {
				s += m1 [r][c] * m2 [c];
			}
			mo [r] = s;
		}
		return mo;
	}

	static float[][] dot(float[][] m1, float[][] m2) { //Multiplying multidimensional matrix by a multidimensional dimensional matrix (Dot Product) - https://www.mathsisfun.com/algebra/matrix-multiplying.html
		var mo = new float[m1.Length][];
		for (int r = 0; r < m1.Length; r++) {
			var row = new float[m1.Length];
			for (int c = 0; c < m2[0].Length; c++) {
				var s = 0.0f; //sum of cols and rows
				for (int n = 0; n < m1[r].Length; n++) {
					var a1 = m1 [r] [n];
					var a2 = m2 [n] [c];
					s += a1 * a2;
				}
				row [c] = s;
			}
			mo [r] = row;
		}
		return mo;
	}

	static T[][] Transpose<T> ( T[][] source ) { //Change orientation of the jagged array - Michael Taylor - https://social.msdn.microsoft.com/Forums/en-US/2dc27cb3-1147-4184-a7a4-6cef5cbf4d22/how-to-change-the-orientation-of-a-jagged-array?forum=csharpgeneral
		var numRows = source[0].Length;
		var numCols = source.Length;

		var target = new T[numRows, numCols];
		for (int row = 0; row < source.Length; ++row)
		{

			for (int col = 0; col < source[row].Length; ++col)
				target[col, row] = source[row][col];
		};

		return ToJaggedArray(target);
	}

	static T[][] ToJaggedArray<T>( T[,] twoDimensionalArray) { //Convert multidimensional array to jagged array - Pedro - https://stackoverflow.com/a/25995025/3961748
		int rowsFirstIndex = twoDimensionalArray.GetLowerBound(0);
		int rowsLastIndex = twoDimensionalArray.GetUpperBound(0);
		int numberOfRows = rowsLastIndex + 1;

		int columnsFirstIndex = twoDimensionalArray.GetLowerBound(1);
		int columnsLastIndex = twoDimensionalArray.GetUpperBound(1);
		int numberOfColumns = columnsLastIndex + 1;

		T[][] jaggedArray = new T[numberOfRows][];
		for (int i = rowsFirstIndex; i <= rowsLastIndex; i++)
		{
			jaggedArray[i] = new T[numberOfColumns];

			for (int j = columnsFirstIndex; j <= columnsLastIndex; j++)
			{
				jaggedArray[i][j] = twoDimensionalArray[i, j];
			}
		}
		return jaggedArray;
	}

}