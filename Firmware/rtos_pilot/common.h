#define PI         3.14159265f
#define RAD2DEG(x) ((x)*(180.0f/PI))
#define DEG2RAD(x) ((x)*(PI/180.0f))
#define G          9.81f

#define MAX(a,b) ((a)>(b)?(a):(b))
#define MIN(a,b) ((a)<(b)?(a):(b))

#define BIND(x,a,b) ((x)>(b)?b:(((x)<(a))?a:x))