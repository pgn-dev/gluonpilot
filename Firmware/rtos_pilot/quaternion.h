void quaternion_from_attitude (const float pitch, const float roll, const float yaw, float* q);

void quaternion_update_with_rates (const float rollrate, const float pitchrate, const float headingrate, float *q, const float dt);

float quaternion_to_roll (const float* q);	

float quaternion_to_pitch(const float* q);

float quaternion_to_yaw(const float* q);

void quaternion_normalize(float* q);
