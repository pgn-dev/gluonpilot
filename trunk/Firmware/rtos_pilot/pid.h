#ifndef PID_H
#define PID_H

// pid.h

struct pid 
{
	float last_error;
	float i_state;
	float i_max, i_min;
	float d_state;
	float d_gain, p_gain, i_gain;
	float d_term_min_var;
};

void pid_init(struct pid *pid, float d_gain, float p_gain, float i_gain, float i_min, float i_max, float d_term_min_var);

float pid_update(struct pid *pid, float position, float dt);
float pid_update_only_p(struct pid *pid, float position, float dt);
float pid_update_only_p_and_i(struct pid *pid, float position, float dt);
/*
float pid_pitch_to_elevator(float desired_pitch, struct ahrs_info *ahrs);
float pid_roll_to_aileron(float roll, struct ahrs_info *ahrs);
float pid_heading_to_roll(float desired_heading_rad, float current_heading_rad);*/

#endif // PID_H
