 /*    Matrix library
  *
  *    Major row: 
  *
  *    [ a00, a01;
  *      a10, a11]
  *    is saved as [ a00, a01, a10, a11 ]
  */
  
#include <stdio.h>

#include "matrix/matrix.h"

/**
 *   A + B = R
 */
float * matrix_2x2_add(float *A, float *B, float *R)
{
	R[0] = A[0] + B[0];
	R[1] = A[1] + B[1];
	R[2] = A[2] + B[2];
	R[3] = A[3] + B[3];
	return R;
}	

/**
 *   A + B = R
 */
float * matrix_3x3_add(float *A, float *B, float *R)
{
	R[0] = A[0] + B[0];
	R[1] = A[1] + B[1];
	R[2] = A[2] + B[2];
	R[3] = A[3] + B[3];
	R[4] = A[4] + B[4];
	R[5] = A[5] + B[5];
	R[6] = A[6] + B[6];
	R[7] = A[7] + B[7];
	R[8] = A[8] + B[8];
	return R;
}

/*

void matrix_print_2x2(float *A)
{
	printf ("%.2f  %.2f	\r\n%.2f  %.2f\r\n", A[0], A[1], A[2], A[3]);
}	

void matrix_print_3x2(float *A)
{
	printf ("%.2f  %.2f	\r\n%.2f  %.2f\r\n%.2f  %.2f\r\n", A[0], A[1], A[2], A[3], A[4], A[5]);
}

void matrix_print_2x3(float *A)
{
	printf ("%.2f  %.2f	 %.2f\r\n%.2f  %.2f  %.2f\r\n", A[0], A[1], A[2], A[3], A[4], A[5]);
}

void matrix_print_3x3(float *A)
{
	printf ("%.3f  %.3f	 %.3f\r\n%.3f  %.3f  %.3f\r\n%.3f  %.3f  %.3f\r\n", A[0], A[1], A[2], A[3], A[4], A[5], A[6], A[7], A[8]);
}	
*/
