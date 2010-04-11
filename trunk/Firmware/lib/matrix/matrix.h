 
 
 /**
  *    MACROS from GLUT (vvector.h) see http://developer.apple.com/mac/library/samplecode/glut/Listings/gle_vvector_h.html#//apple_ref/doc/uid/DTS10000528-gle_vvector_h-DontLinkElementID_52
  */
  
#define M2x2(x,y)  (x*2 + y)
#define M3x2(x,y)  (x*2 + y)
#define M2x3(x,y)  (x*3 + y)
#define M3x3(x,y)  (x*3 + y)
  
  
 #define DETERMINANT_3X3(d,m)   \
{                               \
   d = m[M3x3(0,0)] * (m[M3x3(1,1)]*m[M3x3(2,2)] - m[M3x3(1,2)] * m[M3x3(2,1)]);     \
   d -= m[M3x3(0,1)] * (m[M3x3(1,0)]*m[M3x3(2,2)] - m[M3x3(1,2)] * m[M3x3(2,0)]);    \
   d += m[M3x3(0,2)] * (m[M3x3(1,0)]*m[M3x3(2,1)] - m[M3x3(1,1)] * m[M3x3(2,0)]);    \
}

#define INVERT_3X3(b,det,a)         \
{                       			\
   double tmp;                  	\
   DETERMINANT_3X3 (det, a);        \
   tmp = 1.0 / (det);               \
   SCALE_ADJOINT_3X3 (b, tmp, a);   \
}

#define SCALE_ADJOINT_3X3(a,s,m)     \
{                              		 \
   a[M3x3(0,0)] = (s) * (m[M3x3(1,1)] * m[M3x3(2,2)] - m[M3x3(1,2)] * m[M3x3(2,1)]); \
   a[M3x3(1,0)] = (s) * (m[M3x3(1,2)] * m[M3x3(2,0)] - m[M3x3(1,0)] * m[M3x3(2,2)]); \
   a[M3x3(2,0)] = (s) * (m[M3x3(1,0)] * m[M3x3(2,1)] - m[M3x3(1,1)] * m[M3x3(2,0)]); \
                                \
   a[M3x3(0,1)] = (s) * (m[M3x3(0,2)] * m[M3x3(2,1)] - m[M3x3(0,1)] * m[M3x3(2,2)]); \
   a[M3x3(1,1)] = (s) * (m[M3x3(0,0)] * m[M3x3(2,2)] - m[M3x3(0,2)] * m[M3x3(2,0)]); \
   a[M3x3(2,1)] = (s) * (m[M3x3(0,1)] * m[M3x3(2,0)] - m[M3x3(0,0)] * m[M3x3(2,1)]); \
                                \
   a[M3x3(0,2)] = (s) * (m[M3x3(0,1)] * m[M3x3(1,2)] - m[M3x3(0,2)] * m[M3x3(1,1)]); \
   a[M3x3(1,2)] = (s) * (m[M3x3(0,2)] * m[M3x3(1,0)] - m[M3x3(0,0)] * m[M3x3(1,2)]); \
   a[M3x3(2,2)] = (s) * (m[M3x3(0,0)] * m[M3x3(1,1)] - m[M3x3(0,1)] * m[M3x3(1,0)]); \
}

#define matrix_2x3_times_3x3(A, B, R) \
{ \
	R[M2x3(0,0)] = A[M2x3(0,0)] * B[M3x3(0,0)] + A[M2x3(0,1)] * B[M3x3(1,0)] + A[M2x3(0,2)] * B[M3x3(2,0)]; \
	R[M2x3(0,1)] = A[M2x3(0,0)] * B[M3x3(0,1)] + A[M2x3(0,1)] * B[M3x3(1,1)] + A[M2x3(0,2)] * B[M3x3(2,1)]; \
	R[M2x3(0,2)] = A[M2x3(0,0)] * B[M3x3(0,2)] + A[M2x3(0,1)] * B[M3x3(1,2)] + A[M2x3(0,2)] * B[M3x3(2,2)]; \
	R[M2x3(1,0)] = A[M2x3(1,0)] * B[M3x3(0,0)] + A[M2x3(1,1)] * B[M3x3(1,0)] + A[M2x3(1,2)] * B[M3x3(2,0)]; \
	R[M2x3(1,1)] = A[M2x3(1,0)] * B[M3x3(0,1)] + A[M2x3(1,1)] * B[M3x3(1,1)] + A[M2x3(1,2)] * B[M3x3(2,1)]; \
	R[M2x3(1,2)] = A[M2x3(1,0)] * B[M3x3(0,2)] + A[M2x3(1,1)] * B[M3x3(1,2)] + A[M2x3(1,2)] * B[M3x3(2,2)]; \
}

#define matrix_2x2_times_3x2_transp(A, B, R) \
{ \
	R[M3x3(0,0)] = A[M3x2(0,0)] * B[M3x2(0,0)] + A[M3x2(0,1)] * B[M3x2(0,1)]; \
	R[M3x3(0,1)] = A[M3x2(0,0)] * B[M3x2(1,0)] + A[M3x2(0,1)] * B[M3x2(1,1)]; \
	R[M3x3(0,2)] = A[M3x2(0,0)] * B[M3x2(2,0)] + A[M3x2(0,1)] * B[M3x2(2,1)]; \
	R[M3x3(1,0)] = A[M3x2(1,0)] * B[M3x2(0,0)] + A[M3x2(1,1)] * B[M3x2(0,1)]; \
	R[M3x3(1,1)] = A[M3x2(1,0)] * B[M3x2(1,0)] + A[M3x2(1,1)] * B[M3x2(1,1)]; \
	R[M3x3(1,2)] = A[M3x2(1,0)] * B[M3x2(2,0)] + A[M3x2(1,1)] * B[M3x2(2,1)]; \
}	
 
#define matrix_3x2_times_3x2_transp(A, B, R) \
{ \
	R[M3x3(0,0)] = A[M3x2(0,0)] * B[M3x2(0,0)] + A[M3x2(0,1)] * B[M3x2(0,1)]; \
	R[M3x3(0,1)] = A[M3x2(0,0)] * B[M3x2(1,0)] + A[M3x2(0,1)] * B[M3x2(1,1)]; \
	R[M3x3(0,2)] = A[M3x2(0,0)] * B[M3x2(2,0)] + A[M3x2(0,1)] * B[M3x2(2,1)]; \
	R[M3x3(1,0)] = A[M3x2(1,0)] * B[M3x2(0,0)] + A[M3x2(1,1)] * B[M3x2(0,1)]; \
	R[M3x3(1,1)] = A[M3x2(1,0)] * B[M3x2(1,0)] + A[M3x2(1,1)] * B[M3x2(1,1)]; \
	R[M3x3(1,2)] = A[M3x2(1,0)] * B[M3x2(2,0)] + A[M3x2(1,1)] * B[M3x2(2,1)]; \
	R[M3x3(2,0)] = A[M3x2(2,0)] * B[M3x2(0,0)] + A[M3x2(2,1)] * B[M3x2(0,1)]; \
	R[M3x3(2,1)] = A[M3x2(2,0)] * B[M3x2(1,0)] + A[M3x2(2,1)] * B[M3x2(1,1)]; \
	R[M3x3(2,2)] = A[M3x2(2,0)] * B[M3x2(2,0)] + A[M3x2(2,1)] * B[M3x2(2,1)]; \
}

#define matrix_2x3_times_3x2(A, B, R) \
{ \
	R[M2x2(0,0)] = A[M2x3(0,0)] * B[M3x2(0,0)] + A[M2x3(0,1)] * B[M3x2(1,0)] + A[M2x3(0,2)] * B[M3x2(2,0)]; \
	R[M2x2(0,1)] = A[M2x3(0,0)] * B[M3x2(0,1)] + A[M2x3(0,1)] * B[M3x2(1,1)] + A[M2x3(0,2)] * B[M3x2(2,1)]; \
	R[M2x2(1,0)] = A[M2x3(1,0)] * B[M3x2(0,0)] + A[M2x3(1,1)] * B[M3x2(1,0)] + A[M2x3(1,2)] * B[M3x2(2,0)]; \
	R[M2x2(1,1)] = A[M2x3(1,0)] * B[M3x2(0,1)] + A[M2x3(1,1)] * B[M3x2(1,1)] + A[M2x3(1,2)] * B[M3x2(2,1)]; \
}

#define matrix_3x2_times_2x2(A, B, R) \
{ \
	R[M3x2(0,0)] = A[M3x2(0,0)] * B[M2x2(0,0)] + A[M3x2(0,1)] * B[M2x2(1,0)]; \
	R[M3x2(0,1)] = A[M3x2(0,0)] * B[M2x2(0,1)] + A[M3x2(0,1)] * B[M2x2(1,1)]; \
	R[M3x2(1,0)] = A[M3x2(1,0)] * B[M2x2(0,0)] + A[M3x2(1,1)] * B[M2x2(1,0)]; \
	R[M3x2(1,1)] = A[M3x2(1,0)] * B[M2x2(0,1)] + A[M3x2(1,1)] * B[M2x2(1,1)]; \
	R[M3x2(2,0)] = A[M3x2(2,0)] * B[M2x2(0,0)] + A[M3x2(2,1)] * B[M2x2(1,0)]; \
	R[M3x2(2,1)] = A[M3x2(2,0)] * B[M2x2(0,1)] + A[M3x2(2,1)] * B[M2x2(1,1)]; \
}

#define matrix_2x2_mul_transp(A, B, R) \
{ \
	R[M2x2(0,0)] = A[M2x2(0,0)] * B[M2x2(0,0)] + A[M2x2(0,1)] * B[M2x2(0,1)]; \
	R[M2x2(0,1)] = A[M2x2(0,0)] * B[M2x2(1,0)] + A[M2x2(0,1)] * B[M2x2(1,1)]; \
	R[M2x2(1,0)] = A[M2x2(1,0)] * B[M2x2(0,0)] + A[M2x2(1,1)] * B[M2x2(0,1)]; \
	R[M2x2(1,1)] = A[M2x2(1,0)] * B[M2x2(1,0)] + A[M2x2(1,1)] * B[M2x2(1,1)]; \
}

#define matrix_2x2_mul(A, B, R) \
{ \
	R[M2x2(0,0)] = A[M2x2(0,0)] * B[M2x2(0,0)] + A[M2x2(0,1)] * B[M2x2(1,0)]; \
	R[M2x2(0,1)] = A[M2x2(0,0)] * B[M2x2(0,1)] + A[M2x2(0,1)] * B[M2x2(1,1)]; \
	R[M2x2(1,0)] = A[M2x2(1,0)] * B[M2x2(0,0)] + A[M2x2(1,1)] * B[M2x2(1,0)]; \
	R[M2x2(1,1)] = A[M2x2(1,0)] * B[M2x2(0,1)] + A[M2x2(1,1)] * B[M2x2(1,1)]; \
}


inline double* matrix_2x2_add(double *A, double *B, double *R);
inline double* matrix_3x3_add(double *A, double *B, double *R);
//inline double* matrix_2x2_mul(double *A, double *B, double *R);
//inline double * matrix_2x2_mul_transp(double *A, double *B, double *R);
//inline double * matrix_2x2_times_3x2_transp(double *A, double *B, double *R);
//inline double * matrix_3x2_times_3x2_transp(double *A, double *B, double *R);
//inline double * matrix_3x2_times_2x2(double *A, double *B, double *R);
//inline double * matrix_2x3_times_3x2(double *A, double *B, double *R);
//inline double * matrix_2x3_times_3x3(double *A, double *B, double *R);
inline double matrix_3x3_determintant(double *A);
inline double* matrix_3x3_invert(double *A);	

void matrix_print_2x2(double *A);
void matrix_print_3x2(double *A);
void matrix_print_2x3(double *A);
void matrix_print_3x3(double *A);

