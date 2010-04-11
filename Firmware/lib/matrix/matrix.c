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
double * matrix_2x2_add(double *A, double *B, double *R)
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
double * matrix_3x3_add(double *A, double *B, double *R)
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


/**
 *   A * B = R
 */
/*inline double * matrix_2x2_mul(double *A, double *B, double *R)
{
	R[M2x2(0,0)] = A[M2x2(0,0)] * B[M2x2(0,0)] + A[M2x2(0,1)] * B[M2x2(1,0)];
	R[M2x2(0,1)] = A[M2x2(0,0)] * B[M2x2(0,1)] + A[M2x2(0,1)] * B[M2x2(1,1)];
	R[M2x2(1,0)] = A[M2x2(1,0)] * B[M2x2(0,0)] + A[M2x2(1,1)] * B[M2x2(1,0)];
	R[M2x2(1,1)] = A[M2x2(1,0)] * B[M2x2(0,1)] + A[M2x2(1,1)] * B[M2x2(1,1)];
	return R;
}*/

/**
 *   A[3x2] * B[2x2] = R[3x2]
 *
 *   [ 00  01      [ 00  01
 *     10  11        10  11 ]
 *     20  21 ]
 */
/*inline double * matrix_3x2_times_2x2(double *A, double *B, double *R)
{
	R[M3x2(0,0)] = A[M3x2(0,0)] * B[M2x2(0,0)] + A[M3x2(0,1)] * B[M2x2(1,0)];
	R[M3x2(0,1)] = A[M3x2(0,0)] * B[M2x2(0,1)] + A[M3x2(0,1)] * B[M2x2(1,1)];
	R[M3x2(1,0)] = A[M3x2(1,0)] * B[M2x2(0,0)] + A[M3x2(1,1)] * B[M2x2(1,0)];
	R[M3x2(1,1)] = A[M3x2(1,0)] * B[M2x2(0,1)] + A[M3x2(1,1)] * B[M2x2(1,1)];
	R[M3x2(2,0)] = A[M3x2(2,0)] * B[M2x2(0,0)] + A[M3x2(2,1)] * B[M2x2(1,0)];
	R[M3x2(2,1)] = A[M3x2(2,0)] * B[M2x2(0,1)] + A[M3x2(2,1)] * B[M2x2(1,1)];
	return R;
}*/

/**
 *   A[3x2] * B[3x2]' = R[3x3]
 *
 *   [ 00  01      [ 00  10 20
 *     10  11        01  11 21]
 *     20  21 ]
 */
/*inline double * matrix_3x2_times_3x2_transp(double *A, double *B, double *R)
{
	R[M3x3(0,0)] = A[M3x2(0,0)] * B[M3x2(0,0)] + A[M3x2(0,1)] * B[M3x2(0,1)];
	R[M3x3(0,1)] = A[M3x2(0,0)] * B[M3x2(1,0)] + A[M3x2(0,1)] * B[M3x2(1,1)];
	R[M3x3(0,2)] = A[M3x2(0,0)] * B[M3x2(2,0)] + A[M3x2(0,1)] * B[M3x2(2,1)];
	R[M3x3(1,0)] = A[M3x2(1,0)] * B[M3x2(0,0)] + A[M3x2(1,1)] * B[M3x2(0,1)];
	R[M3x3(1,1)] = A[M3x2(1,0)] * B[M3x2(1,0)] + A[M3x2(1,1)] * B[M3x2(1,1)];
	R[M3x3(1,2)] = A[M3x2(1,0)] * B[M3x2(2,0)] + A[M3x2(1,1)] * B[M3x2(2,1)];
	R[M3x3(2,0)] = A[M3x2(2,0)] * B[M3x2(0,0)] + A[M3x2(2,1)] * B[M3x2(0,1)];
	R[M3x3(2,1)] = A[M3x2(2,0)] * B[M3x2(1,0)] + A[M3x2(2,1)] * B[M3x2(1,1)];
	R[M3x3(2,2)] = A[M3x2(2,0)] * B[M3x2(2,0)] + A[M3x2(2,1)] * B[M3x2(2,1)];
	return R;
}*/

/**
 *   A[2x2] * B[3x2]' = R[2x3]
 *
 *   [ 00  01      [ 00  10 20
 *     10  11]        01  11 21]
 */

/*inline double * matrix_2x2_times_3x2_transp(double *A, double *B, double *R)
{
	R[M3x3(0,0)] = A[M3x2(0,0)] * B[M3x2(0,0)] + A[M3x2(0,1)] * B[M3x2(0,1)];
	R[M3x3(0,1)] = A[M3x2(0,0)] * B[M3x2(1,0)] + A[M3x2(0,1)] * B[M3x2(1,1)];
	R[M3x3(0,2)] = A[M3x2(0,0)] * B[M3x2(2,0)] + A[M3x2(0,1)] * B[M3x2(2,1)];
	R[M3x3(1,0)] = A[M3x2(1,0)] * B[M3x2(0,0)] + A[M3x2(1,1)] * B[M3x2(0,1)];
	R[M3x3(1,1)] = A[M3x2(1,0)] * B[M3x2(1,0)] + A[M3x2(1,1)] * B[M3x2(1,1)];
	R[M3x3(1,2)] = A[M3x2(1,0)] * B[M3x2(2,0)] + A[M3x2(1,1)] * B[M3x2(2,1)];
	return R;
}*/	

/**
 *   A[2x3] * B[3x2] = R[3x3]
 *
 *    [ 00  01 02  [ 00  01     
 *      10  11 12]   10  11      
 *                   20  21 ]
 */
/*inline double * matrix_2x3_times_3x2(double *A, double *B, double *R)
{
	R[M2x2(0,0)] = A[M2x3(0,0)] * B[M3x2(0,0)] + A[M2x3(0,1)] * B[M3x2(1,0)] + A[M2x3(0,2)] * B[M3x2(2,0)];
	R[M2x2(0,1)] = A[M2x3(0,0)] * B[M3x2(0,1)] + A[M2x3(0,1)] * B[M3x2(1,1)] + A[M2x3(0,2)] * B[M3x2(2,1)];
	R[M2x2(1,0)] = A[M2x3(1,0)] * B[M3x2(0,0)] + A[M2x3(1,1)] * B[M3x2(1,0)] + A[M2x3(1,2)] * B[M3x2(2,0)];
	R[M2x2(1,1)] = A[M2x3(1,0)] * B[M3x2(0,1)] + A[M2x3(1,1)] * B[M3x2(1,1)] + A[M2x3(1,2)] * B[M3x2(2,1)];
}*/	

/**
 *   A[2x3] * B[3x3] = R[2x3]
 *
 *    [ 00  01 02  [ 00  01 02   
 *      10  11 12]   10  11 12     
 *                   20  21 22 ]
 */
/*inline double * matrix_2x3_times_3x3(double *A, double *B, double *R)
{
	R[M2x3(0,0)] = A[M2x3(0,0)] * B[M3x3(0,0)] + A[M2x3(0,1)] * B[M3x3(1,0)] + A[M2x3(0,2)] * B[M3x3(2,0)];
	R[M2x3(0,1)] = A[M2x3(0,0)] * B[M3x3(0,1)] + A[M2x3(0,1)] * B[M3x3(1,1)] + A[M2x3(0,2)] * B[M3x3(2,1)];
	R[M2x3(0,2)] = A[M2x3(0,0)] * B[M3x3(0,2)] + A[M2x3(0,1)] * B[M3x3(1,2)] + A[M2x3(0,2)] * B[M3x3(2,2)];
	R[M2x3(1,0)] = A[M2x3(1,0)] * B[M3x3(0,0)] + A[M2x3(1,1)] * B[M3x3(1,0)] + A[M2x3(1,2)] * B[M3x3(2,0)];
	R[M2x3(1,1)] = A[M2x3(1,0)] * B[M3x3(0,1)] + A[M2x3(1,1)] * B[M3x3(1,1)] + A[M2x3(1,2)] * B[M3x3(2,1)];
	R[M2x3(1,2)] = A[M2x3(1,0)] * B[M3x3(0,2)] + A[M2x3(1,1)] * B[M3x3(1,2)] + A[M2x3(1,2)] * B[M3x3(2,2)];
}*/	


/**
 *   A * B' = R
 */
/*double * matrix_2x2_mul_transp(double *A, double *B, double *R)
{
	R[M2x2(0,0)] = A[M2x2(0,0)] * B[M2x2(0,0)] + A[M2x2(0,1)] * B[M2x2(0,1)];
	R[M2x2(0,1)] = A[M2x2(0,0)] * B[M2x2(1,0)] + A[M2x2(0,1)] * B[M2x2(1,1)];
	R[M2x2(1,0)] = A[M2x2(1,0)] * B[M2x2(0,0)] + A[M2x2(1,1)] * B[M2x2(0,1)];
	R[M2x2(1,1)] = A[M2x2(1,0)] * B[M2x2(1,0)] + A[M2x2(1,1)] * B[M2x2(1,1)];
	return R;
}*/

void matrix_print_2x2(double *A)
{
	printf ("%.2f  %.2f	\r\n%.2f  %.2f\r\n", A[0], A[1], A[2], A[3]);
}	

void matrix_print_3x2(double *A)
{
	printf ("%.2f  %.2f	\r\n%.2f  %.2f\r\n%.2f  %.2f\r\n", A[0], A[1], A[2], A[3], A[4], A[5]);
}

void matrix_print_2x3(double *A)
{
	printf ("%.2f  %.2f	 %.2f\r\n%.2f  %.2f  %.2f\r\n", A[0], A[1], A[2], A[3], A[4], A[5]);
}

void matrix_print_3x3(double *A)
{
	printf ("%.3f  %.3f	 %.3f\r\n%.3f  %.3f  %.3f\r\n%.3f  %.3f  %.3f\r\n", A[0], A[1], A[2], A[3], A[4], A[5], A[6], A[7], A[8]);
}	
