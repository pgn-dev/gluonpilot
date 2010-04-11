void quaternion_from_attitude (const double pitch, const double roll, const double yaw, double* q);

void quaternion_update_with_rates (const double rollrate, const double pitchrate, const double headingrate, double *q, const double dt);

double quaternion_to_roll (const double* q);	

double quaternion_to_pitch(const double* q);

double quaternion_to_yaw(const double* q);

void quaternion_normalize(double* q);
