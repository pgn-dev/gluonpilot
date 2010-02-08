#ifndef SERVO_H
#define SERVO_H


void servo_init();

void servo_set_us(int servo, unsigned int us);

void servo_set_ms(int servo, float ms);

void servo_all_neutral();


#endif // SERVO_H