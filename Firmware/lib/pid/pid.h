#ifndef PID_H
#define PID_H

// pid.h

struct pid 
{
	double last_error;
	double i_state;
	double i_max, i_min;
	double d_state;
	double d_gain, p_gain, i_gain;
	double d_term_min_var;
};

void pid_init(struct pid *pid, double d_gain, double p_gain, double i_gain, double i_min, double i_max, double d_term_min_var);

double pid_update(struct pid *pid, double position, double dt);
double pid_update_external_d(struct pid *pid, double position, double d, double dt);
double pid_update_only_p(struct pid *pid, double position, double dt);
double pid_update_only_p_and_i(struct pid *pid, double position, double dt);
/*
float pid_pitch_to_elevator(float desired_pitch, struct ahrs_info *ahrs);
float pid_roll_to_aileron(float roll, struct ahrs_info *ahrs);
float pid_heading_to_roll(float desired_heading_rad, float current_heading_rad);*/

#endif // PID_H
