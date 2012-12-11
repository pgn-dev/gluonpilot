#ifndef __PPM_IN_H_
#define __PPM_IN_H_

void ppm_in_open();
void ppm_in_guess_num_channels();

struct ppm_info {
	char connection_alive;
	char valid_frame;
	unsigned int channel[14]; // max 14!!
};

extern volatile struct ppm_info ppm;

void ppm_in_update_status(float dt);
void ppm_in_update_status_ticks_50hz();

int ppm_signal_quality();


#define MODE1 ppm.channel[config.channel_mode]<1400
#define MODE2 (ppm.channel[config.channel_mode]>1400 && ppm.channel[config.channel_mode]<1700)
#define MODE3 ppm.channel[config.channel_mode]>1700

#endif
