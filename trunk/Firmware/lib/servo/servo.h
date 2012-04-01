#ifndef SERVO_H
#define SERVO_H


void servo_init();

void servo_set_us(int servo, unsigned int us);

void servo_set_ms(int servo, float ms);

unsigned int servo_raw_to_us(unsigned int raw);

unsigned int servo_read_us(int channel);

void servo_all_neutral();

void servo_turbopwm();

void servo_set_logical_0(int servo);

void servo_set_logical_1(int servo);

#endif // SERVO_H
