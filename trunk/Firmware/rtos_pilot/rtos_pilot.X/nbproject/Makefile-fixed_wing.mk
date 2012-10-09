#
# Generated Makefile - do not edit!
#
# Edit the Makefile in the project folder instead (../Makefile). Each target
# has a -pre and a -post target defined where you can add customized code.
#
# This makefile implements configuration specific macros and targets.


# Include project Makefile
include Makefile
# Include makefile containing local settings
ifeq "$(wildcard nbproject/Makefile-local-fixed_wing.mk)" "nbproject/Makefile-local-fixed_wing.mk"
include nbproject/Makefile-local-fixed_wing.mk
endif

# Environment
MKDIR=gnumkdir -p
RM=rm -f 
MV=mv 
CP=cp 

# Macros
CND_CONF=fixed_wing
ifeq ($(TYPE_IMAGE), DEBUG_RUN)
IMAGE_TYPE=debug
OUTPUT_SUFFIX=elf
DEBUGGABLE_SUFFIX=elf
FINAL_IMAGE=dist/${CND_CONF}/${IMAGE_TYPE}/rtos_pilot.X.${IMAGE_TYPE}.${OUTPUT_SUFFIX}
else
IMAGE_TYPE=production
OUTPUT_SUFFIX=hex
DEBUGGABLE_SUFFIX=elf
FINAL_IMAGE=dist/${CND_CONF}/${IMAGE_TYPE}/rtos_pilot.X.${IMAGE_TYPE}.${OUTPUT_SUFFIX}
endif

# Object Directory
OBJECTDIR=build/${CND_CONF}/${IMAGE_TYPE}

# Distribution Directory
DISTDIR=dist/${CND_CONF}/${IMAGE_TYPE}

# Object Files Quoted if spaced
OBJECTFILES_QUOTED_IF_SPACED=${OBJECTDIR}/_ext/1970174492/croutine.o ${OBJECTDIR}/_ext/1970174492/heap_1.o ${OBJECTDIR}/_ext/1970174492/list.o ${OBJECTDIR}/_ext/1970174492/port.o ${OBJECTDIR}/_ext/1970174492/portasm_dsPIC.o ${OBJECTDIR}/_ext/1970174492/queue.o ${OBJECTDIR}/_ext/1970174492/tasks.o ${OBJECTDIR}/_ext/1970174492/timers.o ${OBJECTDIR}/_ext/957539446/adc.o ${OBJECTDIR}/_ext/1077768206/bmp085.o ${OBJECTDIR}/_ext/1070193764/button.o ${OBJECTDIR}/_ext/968823332/dataflash.o ${OBJECTDIR}/_ext/957545600/gps.o ${OBJECTDIR}/_ext/1967121974/hmc5843.o ${OBJECTDIR}/_ext/957545584/i2c.o ${OBJECTDIR}/_ext/957550049/led.o ${OBJECTDIR}/_ext/773745621/matrix.o ${OBJECTDIR}/_ext/1785572984/max7456.o ${OBJECTDIR}/_ext/1843177418/microcontroller.o ${OBJECTDIR}/_ext/957554017/pid.o ${OBJECTDIR}/_ext/674232159/ppm_in.o ${OBJECTDIR}/_ext/667767512/pwm_in.o ${OBJECTDIR}/_ext/888521352/quaternion.o ${OBJECTDIR}/_ext/1429652139/scp1000.o ${OBJECTDIR}/_ext/1089077615/servo.o ${OBJECTDIR}/_ext/1591518261/uart1_queue.o ${OBJECTDIR}/_ext/1090805370/uart2.o ${OBJECTDIR}/_ext/2082761406/mpu6000.o ${OBJECTDIR}/_ext/1472/communication_csv.o ${OBJECTDIR}/_ext/1472/configuration.o ${OBJECTDIR}/_ext/1472/gluonscript.o ${OBJECTDIR}/_ext/1472/osd_max7456.o ${OBJECTDIR}/_ext/1472/rtos_pilot.o ${OBJECTDIR}/_ext/1472/ahrs_kalman_2x3.o ${OBJECTDIR}/_ext/1472/handler_alarms.o ${OBJECTDIR}/_ext/1472/handler_trigger.o ${OBJECTDIR}/_ext/1472/handler_navigation.o ${OBJECTDIR}/_ext/1472/handler_flightplan_switch.o ${OBJECTDIR}/_ext/1472/task_gps.o ${OBJECTDIR}/_ext/1472/task_datalogger.o ${OBJECTDIR}/_ext/1472/task_control.o ${OBJECTDIR}/_ext/1472/task_sensors_analog.o ${OBJECTDIR}/_ext/1472/sensors.o ${OBJECTDIR}/_ext/1472/task_sensors_mpu6000.o
POSSIBLE_DEPFILES=${OBJECTDIR}/_ext/1970174492/croutine.o.d ${OBJECTDIR}/_ext/1970174492/heap_1.o.d ${OBJECTDIR}/_ext/1970174492/list.o.d ${OBJECTDIR}/_ext/1970174492/port.o.d ${OBJECTDIR}/_ext/1970174492/portasm_dsPIC.o.d ${OBJECTDIR}/_ext/1970174492/queue.o.d ${OBJECTDIR}/_ext/1970174492/tasks.o.d ${OBJECTDIR}/_ext/1970174492/timers.o.d ${OBJECTDIR}/_ext/957539446/adc.o.d ${OBJECTDIR}/_ext/1077768206/bmp085.o.d ${OBJECTDIR}/_ext/1070193764/button.o.d ${OBJECTDIR}/_ext/968823332/dataflash.o.d ${OBJECTDIR}/_ext/957545600/gps.o.d ${OBJECTDIR}/_ext/1967121974/hmc5843.o.d ${OBJECTDIR}/_ext/957545584/i2c.o.d ${OBJECTDIR}/_ext/957550049/led.o.d ${OBJECTDIR}/_ext/773745621/matrix.o.d ${OBJECTDIR}/_ext/1785572984/max7456.o.d ${OBJECTDIR}/_ext/1843177418/microcontroller.o.d ${OBJECTDIR}/_ext/957554017/pid.o.d ${OBJECTDIR}/_ext/674232159/ppm_in.o.d ${OBJECTDIR}/_ext/667767512/pwm_in.o.d ${OBJECTDIR}/_ext/888521352/quaternion.o.d ${OBJECTDIR}/_ext/1429652139/scp1000.o.d ${OBJECTDIR}/_ext/1089077615/servo.o.d ${OBJECTDIR}/_ext/1591518261/uart1_queue.o.d ${OBJECTDIR}/_ext/1090805370/uart2.o.d ${OBJECTDIR}/_ext/2082761406/mpu6000.o.d ${OBJECTDIR}/_ext/1472/communication_csv.o.d ${OBJECTDIR}/_ext/1472/configuration.o.d ${OBJECTDIR}/_ext/1472/gluonscript.o.d ${OBJECTDIR}/_ext/1472/osd_max7456.o.d ${OBJECTDIR}/_ext/1472/rtos_pilot.o.d ${OBJECTDIR}/_ext/1472/ahrs_kalman_2x3.o.d ${OBJECTDIR}/_ext/1472/handler_alarms.o.d ${OBJECTDIR}/_ext/1472/handler_trigger.o.d ${OBJECTDIR}/_ext/1472/handler_navigation.o.d ${OBJECTDIR}/_ext/1472/handler_flightplan_switch.o.d ${OBJECTDIR}/_ext/1472/task_gps.o.d ${OBJECTDIR}/_ext/1472/task_datalogger.o.d ${OBJECTDIR}/_ext/1472/task_control.o.d ${OBJECTDIR}/_ext/1472/task_sensors_analog.o.d ${OBJECTDIR}/_ext/1472/sensors.o.d ${OBJECTDIR}/_ext/1472/task_sensors_mpu6000.o.d

# Object Files
OBJECTFILES=${OBJECTDIR}/_ext/1970174492/croutine.o ${OBJECTDIR}/_ext/1970174492/heap_1.o ${OBJECTDIR}/_ext/1970174492/list.o ${OBJECTDIR}/_ext/1970174492/port.o ${OBJECTDIR}/_ext/1970174492/portasm_dsPIC.o ${OBJECTDIR}/_ext/1970174492/queue.o ${OBJECTDIR}/_ext/1970174492/tasks.o ${OBJECTDIR}/_ext/1970174492/timers.o ${OBJECTDIR}/_ext/957539446/adc.o ${OBJECTDIR}/_ext/1077768206/bmp085.o ${OBJECTDIR}/_ext/1070193764/button.o ${OBJECTDIR}/_ext/968823332/dataflash.o ${OBJECTDIR}/_ext/957545600/gps.o ${OBJECTDIR}/_ext/1967121974/hmc5843.o ${OBJECTDIR}/_ext/957545584/i2c.o ${OBJECTDIR}/_ext/957550049/led.o ${OBJECTDIR}/_ext/773745621/matrix.o ${OBJECTDIR}/_ext/1785572984/max7456.o ${OBJECTDIR}/_ext/1843177418/microcontroller.o ${OBJECTDIR}/_ext/957554017/pid.o ${OBJECTDIR}/_ext/674232159/ppm_in.o ${OBJECTDIR}/_ext/667767512/pwm_in.o ${OBJECTDIR}/_ext/888521352/quaternion.o ${OBJECTDIR}/_ext/1429652139/scp1000.o ${OBJECTDIR}/_ext/1089077615/servo.o ${OBJECTDIR}/_ext/1591518261/uart1_queue.o ${OBJECTDIR}/_ext/1090805370/uart2.o ${OBJECTDIR}/_ext/2082761406/mpu6000.o ${OBJECTDIR}/_ext/1472/communication_csv.o ${OBJECTDIR}/_ext/1472/configuration.o ${OBJECTDIR}/_ext/1472/gluonscript.o ${OBJECTDIR}/_ext/1472/osd_max7456.o ${OBJECTDIR}/_ext/1472/rtos_pilot.o ${OBJECTDIR}/_ext/1472/ahrs_kalman_2x3.o ${OBJECTDIR}/_ext/1472/handler_alarms.o ${OBJECTDIR}/_ext/1472/handler_trigger.o ${OBJECTDIR}/_ext/1472/handler_navigation.o ${OBJECTDIR}/_ext/1472/handler_flightplan_switch.o ${OBJECTDIR}/_ext/1472/task_gps.o ${OBJECTDIR}/_ext/1472/task_datalogger.o ${OBJECTDIR}/_ext/1472/task_control.o ${OBJECTDIR}/_ext/1472/task_sensors_analog.o ${OBJECTDIR}/_ext/1472/sensors.o ${OBJECTDIR}/_ext/1472/task_sensors_mpu6000.o


CFLAGS=
ASFLAGS=
LDLIBSOPTIONS=

############# Tool locations ##########################################
# If you copy a project from one host to another, the path where the  #
# compiler is installed may be different.                             #
# If you open this project with MPLAB X in the new host, this         #
# makefile will be regenerated and the paths will be corrected.       #
#######################################################################
# fixDeps replaces a bunch of sed/cat/printf statements that slow down the build
FIXDEPS=fixDeps

.build-conf:  ${BUILD_SUBPROJECTS}
	${MAKE} ${MAKE_OPTIONS} -f nbproject/Makefile-fixed_wing.mk dist/${CND_CONF}/${IMAGE_TYPE}/rtos_pilot.X.${IMAGE_TYPE}.${OUTPUT_SUFFIX}

MP_PROCESSOR_OPTION=33FJ256MC710
MP_LINKER_FILE_OPTION=,--script=p33FJ256MC710.gld
# ------------------------------------------------------------------------------------
# Rules for buildStep: compile
ifeq ($(TYPE_IMAGE), DEBUG_RUN)
${OBJECTDIR}/_ext/1970174492/croutine.o: ../../lib/FreeRTOS/croutine.c  nbproject/Makefile-${CND_CONF}.mk
	@${MKDIR} ${OBJECTDIR}/_ext/1970174492 
	@${RM} ${OBJECTDIR}/_ext/1970174492/croutine.o.d 
	${MP_CC} $(MP_EXTRA_CC_PRE)  ../../lib/FreeRTOS/croutine.c  -o ${OBJECTDIR}/_ext/1970174492/croutine.o  -c -mcpu=$(MP_PROCESSOR_OPTION)  -MMD -MF "${OBJECTDIR}/_ext/1970174492/croutine.o.d"        -g -D__DEBUG -D__MPLAB_DEBUGGER_ICD3=1  -omf=elf -mlarge-code -mlarge-data -O1 -I"..\..\lib\FreeRTOS" -I"..\..\lib" -I"..\..\lib\button" -I"..\..\lib\adc" -I".." -I"..\..\lib\i2c" -I"..\..\lib\bmp085" -I"..\..\lib\hmc5843" -I"..\..\lib\max7456" -I"..\..\lib\matrix" -I"..\..\lib\quaternion" -I"..\..\lib\pid" -I"..\..\lib\pwm_in" -I"..\..\lib\led" -I"..\..\lib\ppm_in" -I"..\..\lib\uart2" -I"..\..\lib\uart1_queue" -I"..\..\lib\servo" -I"..\..\lib\scp1000" -I"..\..\lib\microcontroller" -I"..\..\lib\gps" -I"..\..\lib\dataflash" -DMPLAB_DSPIC_PORT -msmart-io=1 -Wall -msfr-warn=off
	@${FIXDEPS} "${OBJECTDIR}/_ext/1970174492/croutine.o.d" $(SILENT)  -rsi ${MP_CC_DIR}../ 
	
${OBJECTDIR}/_ext/1970174492/heap_1.o: ../../lib/FreeRTOS/heap_1.c  nbproject/Makefile-${CND_CONF}.mk
	@${MKDIR} ${OBJECTDIR}/_ext/1970174492 
	@${RM} ${OBJECTDIR}/_ext/1970174492/heap_1.o.d 
	${MP_CC} $(MP_EXTRA_CC_PRE)  ../../lib/FreeRTOS/heap_1.c  -o ${OBJECTDIR}/_ext/1970174492/heap_1.o  -c -mcpu=$(MP_PROCESSOR_OPTION)  -MMD -MF "${OBJECTDIR}/_ext/1970174492/heap_1.o.d"        -g -D__DEBUG -D__MPLAB_DEBUGGER_ICD3=1  -omf=elf -mlarge-code -mlarge-data -O1 -I"..\..\lib\FreeRTOS" -I"..\..\lib" -I"..\..\lib\button" -I"..\..\lib\adc" -I".." -I"..\..\lib\i2c" -I"..\..\lib\bmp085" -I"..\..\lib\hmc5843" -I"..\..\lib\max7456" -I"..\..\lib\matrix" -I"..\..\lib\quaternion" -I"..\..\lib\pid" -I"..\..\lib\pwm_in" -I"..\..\lib\led" -I"..\..\lib\ppm_in" -I"..\..\lib\uart2" -I"..\..\lib\uart1_queue" -I"..\..\lib\servo" -I"..\..\lib\scp1000" -I"..\..\lib\microcontroller" -I"..\..\lib\gps" -I"..\..\lib\dataflash" -DMPLAB_DSPIC_PORT -msmart-io=1 -Wall -msfr-warn=off
	@${FIXDEPS} "${OBJECTDIR}/_ext/1970174492/heap_1.o.d" $(SILENT)  -rsi ${MP_CC_DIR}../ 
	
${OBJECTDIR}/_ext/1970174492/list.o: ../../lib/FreeRTOS/list.c  nbproject/Makefile-${CND_CONF}.mk
	@${MKDIR} ${OBJECTDIR}/_ext/1970174492 
	@${RM} ${OBJECTDIR}/_ext/1970174492/list.o.d 
	${MP_CC} $(MP_EXTRA_CC_PRE)  ../../lib/FreeRTOS/list.c  -o ${OBJECTDIR}/_ext/1970174492/list.o  -c -mcpu=$(MP_PROCESSOR_OPTION)  -MMD -MF "${OBJECTDIR}/_ext/1970174492/list.o.d"        -g -D__DEBUG -D__MPLAB_DEBUGGER_ICD3=1  -omf=elf -mlarge-code -mlarge-data -O1 -I"..\..\lib\FreeRTOS" -I"..\..\lib" -I"..\..\lib\button" -I"..\..\lib\adc" -I".." -I"..\..\lib\i2c" -I"..\..\lib\bmp085" -I"..\..\lib\hmc5843" -I"..\..\lib\max7456" -I"..\..\lib\matrix" -I"..\..\lib\quaternion" -I"..\..\lib\pid" -I"..\..\lib\pwm_in" -I"..\..\lib\led" -I"..\..\lib\ppm_in" -I"..\..\lib\uart2" -I"..\..\lib\uart1_queue" -I"..\..\lib\servo" -I"..\..\lib\scp1000" -I"..\..\lib\microcontroller" -I"..\..\lib\gps" -I"..\..\lib\dataflash" -DMPLAB_DSPIC_PORT -msmart-io=1 -Wall -msfr-warn=off
	@${FIXDEPS} "${OBJECTDIR}/_ext/1970174492/list.o.d" $(SILENT)  -rsi ${MP_CC_DIR}../ 
	
${OBJECTDIR}/_ext/1970174492/port.o: ../../lib/FreeRTOS/port.c  nbproject/Makefile-${CND_CONF}.mk
	@${MKDIR} ${OBJECTDIR}/_ext/1970174492 
	@${RM} ${OBJECTDIR}/_ext/1970174492/port.o.d 
	${MP_CC} $(MP_EXTRA_CC_PRE)  ../../lib/FreeRTOS/port.c  -o ${OBJECTDIR}/_ext/1970174492/port.o  -c -mcpu=$(MP_PROCESSOR_OPTION)  -MMD -MF "${OBJECTDIR}/_ext/1970174492/port.o.d"        -g -D__DEBUG -D__MPLAB_DEBUGGER_ICD3=1  -omf=elf -mlarge-code -mlarge-data -O1 -I"..\..\lib\FreeRTOS" -I"..\..\lib" -I"..\..\lib\button" -I"..\..\lib\adc" -I".." -I"..\..\lib\i2c" -I"..\..\lib\bmp085" -I"..\..\lib\hmc5843" -I"..\..\lib\max7456" -I"..\..\lib\matrix" -I"..\..\lib\quaternion" -I"..\..\lib\pid" -I"..\..\lib\pwm_in" -I"..\..\lib\led" -I"..\..\lib\ppm_in" -I"..\..\lib\uart2" -I"..\..\lib\uart1_queue" -I"..\..\lib\servo" -I"..\..\lib\scp1000" -I"..\..\lib\microcontroller" -I"..\..\lib\gps" -I"..\..\lib\dataflash" -DMPLAB_DSPIC_PORT -msmart-io=1 -Wall -msfr-warn=off
	@${FIXDEPS} "${OBJECTDIR}/_ext/1970174492/port.o.d" $(SILENT)  -rsi ${MP_CC_DIR}../ 
	
${OBJECTDIR}/_ext/1970174492/queue.o: ../../lib/FreeRTOS/queue.c  nbproject/Makefile-${CND_CONF}.mk
	@${MKDIR} ${OBJECTDIR}/_ext/1970174492 
	@${RM} ${OBJECTDIR}/_ext/1970174492/queue.o.d 
	${MP_CC} $(MP_EXTRA_CC_PRE)  ../../lib/FreeRTOS/queue.c  -o ${OBJECTDIR}/_ext/1970174492/queue.o  -c -mcpu=$(MP_PROCESSOR_OPTION)  -MMD -MF "${OBJECTDIR}/_ext/1970174492/queue.o.d"        -g -D__DEBUG -D__MPLAB_DEBUGGER_ICD3=1  -omf=elf -mlarge-code -mlarge-data -O1 -I"..\..\lib\FreeRTOS" -I"..\..\lib" -I"..\..\lib\button" -I"..\..\lib\adc" -I".." -I"..\..\lib\i2c" -I"..\..\lib\bmp085" -I"..\..\lib\hmc5843" -I"..\..\lib\max7456" -I"..\..\lib\matrix" -I"..\..\lib\quaternion" -I"..\..\lib\pid" -I"..\..\lib\pwm_in" -I"..\..\lib\led" -I"..\..\lib\ppm_in" -I"..\..\lib\uart2" -I"..\..\lib\uart1_queue" -I"..\..\lib\servo" -I"..\..\lib\scp1000" -I"..\..\lib\microcontroller" -I"..\..\lib\gps" -I"..\..\lib\dataflash" -DMPLAB_DSPIC_PORT -msmart-io=1 -Wall -msfr-warn=off
	@${FIXDEPS} "${OBJECTDIR}/_ext/1970174492/queue.o.d" $(SILENT)  -rsi ${MP_CC_DIR}../ 
	
${OBJECTDIR}/_ext/1970174492/tasks.o: ../../lib/FreeRTOS/tasks.c  nbproject/Makefile-${CND_CONF}.mk
	@${MKDIR} ${OBJECTDIR}/_ext/1970174492 
	@${RM} ${OBJECTDIR}/_ext/1970174492/tasks.o.d 
	${MP_CC} $(MP_EXTRA_CC_PRE)  ../../lib/FreeRTOS/tasks.c  -o ${OBJECTDIR}/_ext/1970174492/tasks.o  -c -mcpu=$(MP_PROCESSOR_OPTION)  -MMD -MF "${OBJECTDIR}/_ext/1970174492/tasks.o.d"        -g -D__DEBUG -D__MPLAB_DEBUGGER_ICD3=1  -omf=elf -mlarge-code -mlarge-data -O1 -I"..\..\lib\FreeRTOS" -I"..\..\lib" -I"..\..\lib\button" -I"..\..\lib\adc" -I".." -I"..\..\lib\i2c" -I"..\..\lib\bmp085" -I"..\..\lib\hmc5843" -I"..\..\lib\max7456" -I"..\..\lib\matrix" -I"..\..\lib\quaternion" -I"..\..\lib\pid" -I"..\..\lib\pwm_in" -I"..\..\lib\led" -I"..\..\lib\ppm_in" -I"..\..\lib\uart2" -I"..\..\lib\uart1_queue" -I"..\..\lib\servo" -I"..\..\lib\scp1000" -I"..\..\lib\microcontroller" -I"..\..\lib\gps" -I"..\..\lib\dataflash" -DMPLAB_DSPIC_PORT -msmart-io=1 -Wall -msfr-warn=off
	@${FIXDEPS} "${OBJECTDIR}/_ext/1970174492/tasks.o.d" $(SILENT)  -rsi ${MP_CC_DIR}../ 
	
${OBJECTDIR}/_ext/1970174492/timers.o: ../../lib/FreeRTOS/timers.c  nbproject/Makefile-${CND_CONF}.mk
	@${MKDIR} ${OBJECTDIR}/_ext/1970174492 
	@${RM} ${OBJECTDIR}/_ext/1970174492/timers.o.d 
	${MP_CC} $(MP_EXTRA_CC_PRE)  ../../lib/FreeRTOS/timers.c  -o ${OBJECTDIR}/_ext/1970174492/timers.o  -c -mcpu=$(MP_PROCESSOR_OPTION)  -MMD -MF "${OBJECTDIR}/_ext/1970174492/timers.o.d"        -g -D__DEBUG -D__MPLAB_DEBUGGER_ICD3=1  -omf=elf -mlarge-code -mlarge-data -O1 -I"..\..\lib\FreeRTOS" -I"..\..\lib" -I"..\..\lib\button" -I"..\..\lib\adc" -I".." -I"..\..\lib\i2c" -I"..\..\lib\bmp085" -I"..\..\lib\hmc5843" -I"..\..\lib\max7456" -I"..\..\lib\matrix" -I"..\..\lib\quaternion" -I"..\..\lib\pid" -I"..\..\lib\pwm_in" -I"..\..\lib\led" -I"..\..\lib\ppm_in" -I"..\..\lib\uart2" -I"..\..\lib\uart1_queue" -I"..\..\lib\servo" -I"..\..\lib\scp1000" -I"..\..\lib\microcontroller" -I"..\..\lib\gps" -I"..\..\lib\dataflash" -DMPLAB_DSPIC_PORT -msmart-io=1 -Wall -msfr-warn=off
	@${FIXDEPS} "${OBJECTDIR}/_ext/1970174492/timers.o.d" $(SILENT)  -rsi ${MP_CC_DIR}../ 
	
${OBJECTDIR}/_ext/957539446/adc.o: ../../lib/adc/adc.c  nbproject/Makefile-${CND_CONF}.mk
	@${MKDIR} ${OBJECTDIR}/_ext/957539446 
	@${RM} ${OBJECTDIR}/_ext/957539446/adc.o.d 
	${MP_CC} $(MP_EXTRA_CC_PRE)  ../../lib/adc/adc.c  -o ${OBJECTDIR}/_ext/957539446/adc.o  -c -mcpu=$(MP_PROCESSOR_OPTION)  -MMD -MF "${OBJECTDIR}/_ext/957539446/adc.o.d"        -g -D__DEBUG -D__MPLAB_DEBUGGER_ICD3=1  -omf=elf -mlarge-code -mlarge-data -O1 -I"..\..\lib\FreeRTOS" -I"..\..\lib" -I"..\..\lib\button" -I"..\..\lib\adc" -I".." -I"..\..\lib\i2c" -I"..\..\lib\bmp085" -I"..\..\lib\hmc5843" -I"..\..\lib\max7456" -I"..\..\lib\matrix" -I"..\..\lib\quaternion" -I"..\..\lib\pid" -I"..\..\lib\pwm_in" -I"..\..\lib\led" -I"..\..\lib\ppm_in" -I"..\..\lib\uart2" -I"..\..\lib\uart1_queue" -I"..\..\lib\servo" -I"..\..\lib\scp1000" -I"..\..\lib\microcontroller" -I"..\..\lib\gps" -I"..\..\lib\dataflash" -DMPLAB_DSPIC_PORT -msmart-io=1 -Wall -msfr-warn=off
	@${FIXDEPS} "${OBJECTDIR}/_ext/957539446/adc.o.d" $(SILENT)  -rsi ${MP_CC_DIR}../ 
	
${OBJECTDIR}/_ext/1077768206/bmp085.o: ../../lib/bmp085/bmp085.c  nbproject/Makefile-${CND_CONF}.mk
	@${MKDIR} ${OBJECTDIR}/_ext/1077768206 
	@${RM} ${OBJECTDIR}/_ext/1077768206/bmp085.o.d 
	${MP_CC} $(MP_EXTRA_CC_PRE)  ../../lib/bmp085/bmp085.c  -o ${OBJECTDIR}/_ext/1077768206/bmp085.o  -c -mcpu=$(MP_PROCESSOR_OPTION)  -MMD -MF "${OBJECTDIR}/_ext/1077768206/bmp085.o.d"        -g -D__DEBUG -D__MPLAB_DEBUGGER_ICD3=1  -omf=elf -mlarge-code -mlarge-data -O1 -I"..\..\lib\FreeRTOS" -I"..\..\lib" -I"..\..\lib\button" -I"..\..\lib\adc" -I".." -I"..\..\lib\i2c" -I"..\..\lib\bmp085" -I"..\..\lib\hmc5843" -I"..\..\lib\max7456" -I"..\..\lib\matrix" -I"..\..\lib\quaternion" -I"..\..\lib\pid" -I"..\..\lib\pwm_in" -I"..\..\lib\led" -I"..\..\lib\ppm_in" -I"..\..\lib\uart2" -I"..\..\lib\uart1_queue" -I"..\..\lib\servo" -I"..\..\lib\scp1000" -I"..\..\lib\microcontroller" -I"..\..\lib\gps" -I"..\..\lib\dataflash" -DMPLAB_DSPIC_PORT -msmart-io=1 -Wall -msfr-warn=off
	@${FIXDEPS} "${OBJECTDIR}/_ext/1077768206/bmp085.o.d" $(SILENT)  -rsi ${MP_CC_DIR}../ 
	
${OBJECTDIR}/_ext/1070193764/button.o: ../../lib/button/button.c  nbproject/Makefile-${CND_CONF}.mk
	@${MKDIR} ${OBJECTDIR}/_ext/1070193764 
	@${RM} ${OBJECTDIR}/_ext/1070193764/button.o.d 
	${MP_CC} $(MP_EXTRA_CC_PRE)  ../../lib/button/button.c  -o ${OBJECTDIR}/_ext/1070193764/button.o  -c -mcpu=$(MP_PROCESSOR_OPTION)  -MMD -MF "${OBJECTDIR}/_ext/1070193764/button.o.d"        -g -D__DEBUG -D__MPLAB_DEBUGGER_ICD3=1  -omf=elf -mlarge-code -mlarge-data -O1 -I"..\..\lib\FreeRTOS" -I"..\..\lib" -I"..\..\lib\button" -I"..\..\lib\adc" -I".." -I"..\..\lib\i2c" -I"..\..\lib\bmp085" -I"..\..\lib\hmc5843" -I"..\..\lib\max7456" -I"..\..\lib\matrix" -I"..\..\lib\quaternion" -I"..\..\lib\pid" -I"..\..\lib\pwm_in" -I"..\..\lib\led" -I"..\..\lib\ppm_in" -I"..\..\lib\uart2" -I"..\..\lib\uart1_queue" -I"..\..\lib\servo" -I"..\..\lib\scp1000" -I"..\..\lib\microcontroller" -I"..\..\lib\gps" -I"..\..\lib\dataflash" -DMPLAB_DSPIC_PORT -msmart-io=1 -Wall -msfr-warn=off
	@${FIXDEPS} "${OBJECTDIR}/_ext/1070193764/button.o.d" $(SILENT)  -rsi ${MP_CC_DIR}../ 
	
${OBJECTDIR}/_ext/968823332/dataflash.o: ../../lib/dataflash/dataflash.c  nbproject/Makefile-${CND_CONF}.mk
	@${MKDIR} ${OBJECTDIR}/_ext/968823332 
	@${RM} ${OBJECTDIR}/_ext/968823332/dataflash.o.d 
	${MP_CC} $(MP_EXTRA_CC_PRE)  ../../lib/dataflash/dataflash.c  -o ${OBJECTDIR}/_ext/968823332/dataflash.o  -c -mcpu=$(MP_PROCESSOR_OPTION)  -MMD -MF "${OBJECTDIR}/_ext/968823332/dataflash.o.d"        -g -D__DEBUG -D__MPLAB_DEBUGGER_ICD3=1  -omf=elf -mlarge-code -mlarge-data -O1 -I"..\..\lib\FreeRTOS" -I"..\..\lib" -I"..\..\lib\button" -I"..\..\lib\adc" -I".." -I"..\..\lib\i2c" -I"..\..\lib\bmp085" -I"..\..\lib\hmc5843" -I"..\..\lib\max7456" -I"..\..\lib\matrix" -I"..\..\lib\quaternion" -I"..\..\lib\pid" -I"..\..\lib\pwm_in" -I"..\..\lib\led" -I"..\..\lib\ppm_in" -I"..\..\lib\uart2" -I"..\..\lib\uart1_queue" -I"..\..\lib\servo" -I"..\..\lib\scp1000" -I"..\..\lib\microcontroller" -I"..\..\lib\gps" -I"..\..\lib\dataflash" -DMPLAB_DSPIC_PORT -msmart-io=1 -Wall -msfr-warn=off
	@${FIXDEPS} "${OBJECTDIR}/_ext/968823332/dataflash.o.d" $(SILENT)  -rsi ${MP_CC_DIR}../ 
	
${OBJECTDIR}/_ext/957545600/gps.o: ../../lib/gps/gps.c  nbproject/Makefile-${CND_CONF}.mk
	@${MKDIR} ${OBJECTDIR}/_ext/957545600 
	@${RM} ${OBJECTDIR}/_ext/957545600/gps.o.d 
	${MP_CC} $(MP_EXTRA_CC_PRE)  ../../lib/gps/gps.c  -o ${OBJECTDIR}/_ext/957545600/gps.o  -c -mcpu=$(MP_PROCESSOR_OPTION)  -MMD -MF "${OBJECTDIR}/_ext/957545600/gps.o.d"        -g -D__DEBUG -D__MPLAB_DEBUGGER_ICD3=1  -omf=elf -mlarge-code -mlarge-data -O1 -I"..\..\lib\FreeRTOS" -I"..\..\lib" -I"..\..\lib\button" -I"..\..\lib\adc" -I".." -I"..\..\lib\i2c" -I"..\..\lib\bmp085" -I"..\..\lib\hmc5843" -I"..\..\lib\max7456" -I"..\..\lib\matrix" -I"..\..\lib\quaternion" -I"..\..\lib\pid" -I"..\..\lib\pwm_in" -I"..\..\lib\led" -I"..\..\lib\ppm_in" -I"..\..\lib\uart2" -I"..\..\lib\uart1_queue" -I"..\..\lib\servo" -I"..\..\lib\scp1000" -I"..\..\lib\microcontroller" -I"..\..\lib\gps" -I"..\..\lib\dataflash" -DMPLAB_DSPIC_PORT -msmart-io=1 -Wall -msfr-warn=off
	@${FIXDEPS} "${OBJECTDIR}/_ext/957545600/gps.o.d" $(SILENT)  -rsi ${MP_CC_DIR}../ 
	
${OBJECTDIR}/_ext/1967121974/hmc5843.o: ../../lib/hmc5843/hmc5843.c  nbproject/Makefile-${CND_CONF}.mk
	@${MKDIR} ${OBJECTDIR}/_ext/1967121974 
	@${RM} ${OBJECTDIR}/_ext/1967121974/hmc5843.o.d 
	${MP_CC} $(MP_EXTRA_CC_PRE)  ../../lib/hmc5843/hmc5843.c  -o ${OBJECTDIR}/_ext/1967121974/hmc5843.o  -c -mcpu=$(MP_PROCESSOR_OPTION)  -MMD -MF "${OBJECTDIR}/_ext/1967121974/hmc5843.o.d"        -g -D__DEBUG -D__MPLAB_DEBUGGER_ICD3=1  -omf=elf -mlarge-code -mlarge-data -O1 -I"..\..\lib\FreeRTOS" -I"..\..\lib" -I"..\..\lib\button" -I"..\..\lib\adc" -I".." -I"..\..\lib\i2c" -I"..\..\lib\bmp085" -I"..\..\lib\hmc5843" -I"..\..\lib\max7456" -I"..\..\lib\matrix" -I"..\..\lib\quaternion" -I"..\..\lib\pid" -I"..\..\lib\pwm_in" -I"..\..\lib\led" -I"..\..\lib\ppm_in" -I"..\..\lib\uart2" -I"..\..\lib\uart1_queue" -I"..\..\lib\servo" -I"..\..\lib\scp1000" -I"..\..\lib\microcontroller" -I"..\..\lib\gps" -I"..\..\lib\dataflash" -DMPLAB_DSPIC_PORT -msmart-io=1 -Wall -msfr-warn=off
	@${FIXDEPS} "${OBJECTDIR}/_ext/1967121974/hmc5843.o.d" $(SILENT)  -rsi ${MP_CC_DIR}../ 
	
${OBJECTDIR}/_ext/957545584/i2c.o: ../../lib/i2c/i2c.c  nbproject/Makefile-${CND_CONF}.mk
	@${MKDIR} ${OBJECTDIR}/_ext/957545584 
	@${RM} ${OBJECTDIR}/_ext/957545584/i2c.o.d 
	${MP_CC} $(MP_EXTRA_CC_PRE)  ../../lib/i2c/i2c.c  -o ${OBJECTDIR}/_ext/957545584/i2c.o  -c -mcpu=$(MP_PROCESSOR_OPTION)  -MMD -MF "${OBJECTDIR}/_ext/957545584/i2c.o.d"        -g -D__DEBUG -D__MPLAB_DEBUGGER_ICD3=1  -omf=elf -mlarge-code -mlarge-data -O1 -I"..\..\lib\FreeRTOS" -I"..\..\lib" -I"..\..\lib\button" -I"..\..\lib\adc" -I".." -I"..\..\lib\i2c" -I"..\..\lib\bmp085" -I"..\..\lib\hmc5843" -I"..\..\lib\max7456" -I"..\..\lib\matrix" -I"..\..\lib\quaternion" -I"..\..\lib\pid" -I"..\..\lib\pwm_in" -I"..\..\lib\led" -I"..\..\lib\ppm_in" -I"..\..\lib\uart2" -I"..\..\lib\uart1_queue" -I"..\..\lib\servo" -I"..\..\lib\scp1000" -I"..\..\lib\microcontroller" -I"..\..\lib\gps" -I"..\..\lib\dataflash" -DMPLAB_DSPIC_PORT -msmart-io=1 -Wall -msfr-warn=off
	@${FIXDEPS} "${OBJECTDIR}/_ext/957545584/i2c.o.d" $(SILENT)  -rsi ${MP_CC_DIR}../ 
	
${OBJECTDIR}/_ext/957550049/led.o: ../../lib/led/led.c  nbproject/Makefile-${CND_CONF}.mk
	@${MKDIR} ${OBJECTDIR}/_ext/957550049 
	@${RM} ${OBJECTDIR}/_ext/957550049/led.o.d 
	${MP_CC} $(MP_EXTRA_CC_PRE)  ../../lib/led/led.c  -o ${OBJECTDIR}/_ext/957550049/led.o  -c -mcpu=$(MP_PROCESSOR_OPTION)  -MMD -MF "${OBJECTDIR}/_ext/957550049/led.o.d"        -g -D__DEBUG -D__MPLAB_DEBUGGER_ICD3=1  -omf=elf -mlarge-code -mlarge-data -O1 -I"..\..\lib\FreeRTOS" -I"..\..\lib" -I"..\..\lib\button" -I"..\..\lib\adc" -I".." -I"..\..\lib\i2c" -I"..\..\lib\bmp085" -I"..\..\lib\hmc5843" -I"..\..\lib\max7456" -I"..\..\lib\matrix" -I"..\..\lib\quaternion" -I"..\..\lib\pid" -I"..\..\lib\pwm_in" -I"..\..\lib\led" -I"..\..\lib\ppm_in" -I"..\..\lib\uart2" -I"..\..\lib\uart1_queue" -I"..\..\lib\servo" -I"..\..\lib\scp1000" -I"..\..\lib\microcontroller" -I"..\..\lib\gps" -I"..\..\lib\dataflash" -DMPLAB_DSPIC_PORT -msmart-io=1 -Wall -msfr-warn=off
	@${FIXDEPS} "${OBJECTDIR}/_ext/957550049/led.o.d" $(SILENT)  -rsi ${MP_CC_DIR}../ 
	
${OBJECTDIR}/_ext/773745621/matrix.o: ../../lib/matrix/matrix.c  nbproject/Makefile-${CND_CONF}.mk
	@${MKDIR} ${OBJECTDIR}/_ext/773745621 
	@${RM} ${OBJECTDIR}/_ext/773745621/matrix.o.d 
	${MP_CC} $(MP_EXTRA_CC_PRE)  ../../lib/matrix/matrix.c  -o ${OBJECTDIR}/_ext/773745621/matrix.o  -c -mcpu=$(MP_PROCESSOR_OPTION)  -MMD -MF "${OBJECTDIR}/_ext/773745621/matrix.o.d"        -g -D__DEBUG -D__MPLAB_DEBUGGER_ICD3=1  -omf=elf -mlarge-code -mlarge-data -O1 -I"..\..\lib\FreeRTOS" -I"..\..\lib" -I"..\..\lib\button" -I"..\..\lib\adc" -I".." -I"..\..\lib\i2c" -I"..\..\lib\bmp085" -I"..\..\lib\hmc5843" -I"..\..\lib\max7456" -I"..\..\lib\matrix" -I"..\..\lib\quaternion" -I"..\..\lib\pid" -I"..\..\lib\pwm_in" -I"..\..\lib\led" -I"..\..\lib\ppm_in" -I"..\..\lib\uart2" -I"..\..\lib\uart1_queue" -I"..\..\lib\servo" -I"..\..\lib\scp1000" -I"..\..\lib\microcontroller" -I"..\..\lib\gps" -I"..\..\lib\dataflash" -DMPLAB_DSPIC_PORT -msmart-io=1 -Wall -msfr-warn=off
	@${FIXDEPS} "${OBJECTDIR}/_ext/773745621/matrix.o.d" $(SILENT)  -rsi ${MP_CC_DIR}../ 
	
${OBJECTDIR}/_ext/1785572984/max7456.o: ../../lib/max7456/max7456.c  nbproject/Makefile-${CND_CONF}.mk
	@${MKDIR} ${OBJECTDIR}/_ext/1785572984 
	@${RM} ${OBJECTDIR}/_ext/1785572984/max7456.o.d 
	${MP_CC} $(MP_EXTRA_CC_PRE)  ../../lib/max7456/max7456.c  -o ${OBJECTDIR}/_ext/1785572984/max7456.o  -c -mcpu=$(MP_PROCESSOR_OPTION)  -MMD -MF "${OBJECTDIR}/_ext/1785572984/max7456.o.d"        -g -D__DEBUG -D__MPLAB_DEBUGGER_ICD3=1  -omf=elf -mlarge-code -mlarge-data -O1 -I"..\..\lib\FreeRTOS" -I"..\..\lib" -I"..\..\lib\button" -I"..\..\lib\adc" -I".." -I"..\..\lib\i2c" -I"..\..\lib\bmp085" -I"..\..\lib\hmc5843" -I"..\..\lib\max7456" -I"..\..\lib\matrix" -I"..\..\lib\quaternion" -I"..\..\lib\pid" -I"..\..\lib\pwm_in" -I"..\..\lib\led" -I"..\..\lib\ppm_in" -I"..\..\lib\uart2" -I"..\..\lib\uart1_queue" -I"..\..\lib\servo" -I"..\..\lib\scp1000" -I"..\..\lib\microcontroller" -I"..\..\lib\gps" -I"..\..\lib\dataflash" -DMPLAB_DSPIC_PORT -msmart-io=1 -Wall -msfr-warn=off
	@${FIXDEPS} "${OBJECTDIR}/_ext/1785572984/max7456.o.d" $(SILENT)  -rsi ${MP_CC_DIR}../ 
	
${OBJECTDIR}/_ext/1843177418/microcontroller.o: ../../lib/microcontroller/microcontroller.c  nbproject/Makefile-${CND_CONF}.mk
	@${MKDIR} ${OBJECTDIR}/_ext/1843177418 
	@${RM} ${OBJECTDIR}/_ext/1843177418/microcontroller.o.d 
	${MP_CC} $(MP_EXTRA_CC_PRE)  ../../lib/microcontroller/microcontroller.c  -o ${OBJECTDIR}/_ext/1843177418/microcontroller.o  -c -mcpu=$(MP_PROCESSOR_OPTION)  -MMD -MF "${OBJECTDIR}/_ext/1843177418/microcontroller.o.d"        -g -D__DEBUG -D__MPLAB_DEBUGGER_ICD3=1  -omf=elf -mlarge-code -mlarge-data -O1 -I"..\..\lib\FreeRTOS" -I"..\..\lib" -I"..\..\lib\button" -I"..\..\lib\adc" -I".." -I"..\..\lib\i2c" -I"..\..\lib\bmp085" -I"..\..\lib\hmc5843" -I"..\..\lib\max7456" -I"..\..\lib\matrix" -I"..\..\lib\quaternion" -I"..\..\lib\pid" -I"..\..\lib\pwm_in" -I"..\..\lib\led" -I"..\..\lib\ppm_in" -I"..\..\lib\uart2" -I"..\..\lib\uart1_queue" -I"..\..\lib\servo" -I"..\..\lib\scp1000" -I"..\..\lib\microcontroller" -I"..\..\lib\gps" -I"..\..\lib\dataflash" -DMPLAB_DSPIC_PORT -msmart-io=1 -Wall -msfr-warn=off
	@${FIXDEPS} "${OBJECTDIR}/_ext/1843177418/microcontroller.o.d" $(SILENT)  -rsi ${MP_CC_DIR}../ 
	
${OBJECTDIR}/_ext/957554017/pid.o: ../../lib/pid/pid.c  nbproject/Makefile-${CND_CONF}.mk
	@${MKDIR} ${OBJECTDIR}/_ext/957554017 
	@${RM} ${OBJECTDIR}/_ext/957554017/pid.o.d 
	${MP_CC} $(MP_EXTRA_CC_PRE)  ../../lib/pid/pid.c  -o ${OBJECTDIR}/_ext/957554017/pid.o  -c -mcpu=$(MP_PROCESSOR_OPTION)  -MMD -MF "${OBJECTDIR}/_ext/957554017/pid.o.d"        -g -D__DEBUG -D__MPLAB_DEBUGGER_ICD3=1  -omf=elf -mlarge-code -mlarge-data -O1 -I"..\..\lib\FreeRTOS" -I"..\..\lib" -I"..\..\lib\button" -I"..\..\lib\adc" -I".." -I"..\..\lib\i2c" -I"..\..\lib\bmp085" -I"..\..\lib\hmc5843" -I"..\..\lib\max7456" -I"..\..\lib\matrix" -I"..\..\lib\quaternion" -I"..\..\lib\pid" -I"..\..\lib\pwm_in" -I"..\..\lib\led" -I"..\..\lib\ppm_in" -I"..\..\lib\uart2" -I"..\..\lib\uart1_queue" -I"..\..\lib\servo" -I"..\..\lib\scp1000" -I"..\..\lib\microcontroller" -I"..\..\lib\gps" -I"..\..\lib\dataflash" -DMPLAB_DSPIC_PORT -msmart-io=1 -Wall -msfr-warn=off
	@${FIXDEPS} "${OBJECTDIR}/_ext/957554017/pid.o.d" $(SILENT)  -rsi ${MP_CC_DIR}../ 
	
${OBJECTDIR}/_ext/674232159/ppm_in.o: ../../lib/ppm_in/ppm_in.c  nbproject/Makefile-${CND_CONF}.mk
	@${MKDIR} ${OBJECTDIR}/_ext/674232159 
	@${RM} ${OBJECTDIR}/_ext/674232159/ppm_in.o.d 
	${MP_CC} $(MP_EXTRA_CC_PRE)  ../../lib/ppm_in/ppm_in.c  -o ${OBJECTDIR}/_ext/674232159/ppm_in.o  -c -mcpu=$(MP_PROCESSOR_OPTION)  -MMD -MF "${OBJECTDIR}/_ext/674232159/ppm_in.o.d"        -g -D__DEBUG -D__MPLAB_DEBUGGER_ICD3=1  -omf=elf -mlarge-code -mlarge-data -O1 -I"..\..\lib\FreeRTOS" -I"..\..\lib" -I"..\..\lib\button" -I"..\..\lib\adc" -I".." -I"..\..\lib\i2c" -I"..\..\lib\bmp085" -I"..\..\lib\hmc5843" -I"..\..\lib\max7456" -I"..\..\lib\matrix" -I"..\..\lib\quaternion" -I"..\..\lib\pid" -I"..\..\lib\pwm_in" -I"..\..\lib\led" -I"..\..\lib\ppm_in" -I"..\..\lib\uart2" -I"..\..\lib\uart1_queue" -I"..\..\lib\servo" -I"..\..\lib\scp1000" -I"..\..\lib\microcontroller" -I"..\..\lib\gps" -I"..\..\lib\dataflash" -DMPLAB_DSPIC_PORT -msmart-io=1 -Wall -msfr-warn=off
	@${FIXDEPS} "${OBJECTDIR}/_ext/674232159/ppm_in.o.d" $(SILENT)  -rsi ${MP_CC_DIR}../ 
	
${OBJECTDIR}/_ext/667767512/pwm_in.o: ../../lib/pwm_in/pwm_in.c  nbproject/Makefile-${CND_CONF}.mk
	@${MKDIR} ${OBJECTDIR}/_ext/667767512 
	@${RM} ${OBJECTDIR}/_ext/667767512/pwm_in.o.d 
	${MP_CC} $(MP_EXTRA_CC_PRE)  ../../lib/pwm_in/pwm_in.c  -o ${OBJECTDIR}/_ext/667767512/pwm_in.o  -c -mcpu=$(MP_PROCESSOR_OPTION)  -MMD -MF "${OBJECTDIR}/_ext/667767512/pwm_in.o.d"        -g -D__DEBUG -D__MPLAB_DEBUGGER_ICD3=1  -omf=elf -mlarge-code -mlarge-data -O1 -I"..\..\lib\FreeRTOS" -I"..\..\lib" -I"..\..\lib\button" -I"..\..\lib\adc" -I".." -I"..\..\lib\i2c" -I"..\..\lib\bmp085" -I"..\..\lib\hmc5843" -I"..\..\lib\max7456" -I"..\..\lib\matrix" -I"..\..\lib\quaternion" -I"..\..\lib\pid" -I"..\..\lib\pwm_in" -I"..\..\lib\led" -I"..\..\lib\ppm_in" -I"..\..\lib\uart2" -I"..\..\lib\uart1_queue" -I"..\..\lib\servo" -I"..\..\lib\scp1000" -I"..\..\lib\microcontroller" -I"..\..\lib\gps" -I"..\..\lib\dataflash" -DMPLAB_DSPIC_PORT -msmart-io=1 -Wall -msfr-warn=off
	@${FIXDEPS} "${OBJECTDIR}/_ext/667767512/pwm_in.o.d" $(SILENT)  -rsi ${MP_CC_DIR}../ 
	
${OBJECTDIR}/_ext/888521352/quaternion.o: ../../lib/quaternion/quaternion.c  nbproject/Makefile-${CND_CONF}.mk
	@${MKDIR} ${OBJECTDIR}/_ext/888521352 
	@${RM} ${OBJECTDIR}/_ext/888521352/quaternion.o.d 
	${MP_CC} $(MP_EXTRA_CC_PRE)  ../../lib/quaternion/quaternion.c  -o ${OBJECTDIR}/_ext/888521352/quaternion.o  -c -mcpu=$(MP_PROCESSOR_OPTION)  -MMD -MF "${OBJECTDIR}/_ext/888521352/quaternion.o.d"        -g -D__DEBUG -D__MPLAB_DEBUGGER_ICD3=1  -omf=elf -mlarge-code -mlarge-data -O1 -I"..\..\lib\FreeRTOS" -I"..\..\lib" -I"..\..\lib\button" -I"..\..\lib\adc" -I".." -I"..\..\lib\i2c" -I"..\..\lib\bmp085" -I"..\..\lib\hmc5843" -I"..\..\lib\max7456" -I"..\..\lib\matrix" -I"..\..\lib\quaternion" -I"..\..\lib\pid" -I"..\..\lib\pwm_in" -I"..\..\lib\led" -I"..\..\lib\ppm_in" -I"..\..\lib\uart2" -I"..\..\lib\uart1_queue" -I"..\..\lib\servo" -I"..\..\lib\scp1000" -I"..\..\lib\microcontroller" -I"..\..\lib\gps" -I"..\..\lib\dataflash" -DMPLAB_DSPIC_PORT -msmart-io=1 -Wall -msfr-warn=off
	@${FIXDEPS} "${OBJECTDIR}/_ext/888521352/quaternion.o.d" $(SILENT)  -rsi ${MP_CC_DIR}../ 
	
${OBJECTDIR}/_ext/1429652139/scp1000.o: ../../lib/scp1000/scp1000.c  nbproject/Makefile-${CND_CONF}.mk
	@${MKDIR} ${OBJECTDIR}/_ext/1429652139 
	@${RM} ${OBJECTDIR}/_ext/1429652139/scp1000.o.d 
	${MP_CC} $(MP_EXTRA_CC_PRE)  ../../lib/scp1000/scp1000.c  -o ${OBJECTDIR}/_ext/1429652139/scp1000.o  -c -mcpu=$(MP_PROCESSOR_OPTION)  -MMD -MF "${OBJECTDIR}/_ext/1429652139/scp1000.o.d"        -g -D__DEBUG -D__MPLAB_DEBUGGER_ICD3=1  -omf=elf -mlarge-code -mlarge-data -O1 -I"..\..\lib\FreeRTOS" -I"..\..\lib" -I"..\..\lib\button" -I"..\..\lib\adc" -I".." -I"..\..\lib\i2c" -I"..\..\lib\bmp085" -I"..\..\lib\hmc5843" -I"..\..\lib\max7456" -I"..\..\lib\matrix" -I"..\..\lib\quaternion" -I"..\..\lib\pid" -I"..\..\lib\pwm_in" -I"..\..\lib\led" -I"..\..\lib\ppm_in" -I"..\..\lib\uart2" -I"..\..\lib\uart1_queue" -I"..\..\lib\servo" -I"..\..\lib\scp1000" -I"..\..\lib\microcontroller" -I"..\..\lib\gps" -I"..\..\lib\dataflash" -DMPLAB_DSPIC_PORT -msmart-io=1 -Wall -msfr-warn=off
	@${FIXDEPS} "${OBJECTDIR}/_ext/1429652139/scp1000.o.d" $(SILENT)  -rsi ${MP_CC_DIR}../ 
	
${OBJECTDIR}/_ext/1089077615/servo.o: ../../lib/servo/servo.c  nbproject/Makefile-${CND_CONF}.mk
	@${MKDIR} ${OBJECTDIR}/_ext/1089077615 
	@${RM} ${OBJECTDIR}/_ext/1089077615/servo.o.d 
	${MP_CC} $(MP_EXTRA_CC_PRE)  ../../lib/servo/servo.c  -o ${OBJECTDIR}/_ext/1089077615/servo.o  -c -mcpu=$(MP_PROCESSOR_OPTION)  -MMD -MF "${OBJECTDIR}/_ext/1089077615/servo.o.d"        -g -D__DEBUG -D__MPLAB_DEBUGGER_ICD3=1  -omf=elf -mlarge-code -mlarge-data -O1 -I"..\..\lib\FreeRTOS" -I"..\..\lib" -I"..\..\lib\button" -I"..\..\lib\adc" -I".." -I"..\..\lib\i2c" -I"..\..\lib\bmp085" -I"..\..\lib\hmc5843" -I"..\..\lib\max7456" -I"..\..\lib\matrix" -I"..\..\lib\quaternion" -I"..\..\lib\pid" -I"..\..\lib\pwm_in" -I"..\..\lib\led" -I"..\..\lib\ppm_in" -I"..\..\lib\uart2" -I"..\..\lib\uart1_queue" -I"..\..\lib\servo" -I"..\..\lib\scp1000" -I"..\..\lib\microcontroller" -I"..\..\lib\gps" -I"..\..\lib\dataflash" -DMPLAB_DSPIC_PORT -msmart-io=1 -Wall -msfr-warn=off
	@${FIXDEPS} "${OBJECTDIR}/_ext/1089077615/servo.o.d" $(SILENT)  -rsi ${MP_CC_DIR}../ 
	
${OBJECTDIR}/_ext/1591518261/uart1_queue.o: ../../lib/uart1_queue/uart1_queue.c  nbproject/Makefile-${CND_CONF}.mk
	@${MKDIR} ${OBJECTDIR}/_ext/1591518261 
	@${RM} ${OBJECTDIR}/_ext/1591518261/uart1_queue.o.d 
	${MP_CC} $(MP_EXTRA_CC_PRE)  ../../lib/uart1_queue/uart1_queue.c  -o ${OBJECTDIR}/_ext/1591518261/uart1_queue.o  -c -mcpu=$(MP_PROCESSOR_OPTION)  -MMD -MF "${OBJECTDIR}/_ext/1591518261/uart1_queue.o.d"        -g -D__DEBUG -D__MPLAB_DEBUGGER_ICD3=1  -omf=elf -mlarge-code -mlarge-data -O1 -I"..\..\lib\FreeRTOS" -I"..\..\lib" -I"..\..\lib\button" -I"..\..\lib\adc" -I".." -I"..\..\lib\i2c" -I"..\..\lib\bmp085" -I"..\..\lib\hmc5843" -I"..\..\lib\max7456" -I"..\..\lib\matrix" -I"..\..\lib\quaternion" -I"..\..\lib\pid" -I"..\..\lib\pwm_in" -I"..\..\lib\led" -I"..\..\lib\ppm_in" -I"..\..\lib\uart2" -I"..\..\lib\uart1_queue" -I"..\..\lib\servo" -I"..\..\lib\scp1000" -I"..\..\lib\microcontroller" -I"..\..\lib\gps" -I"..\..\lib\dataflash" -DMPLAB_DSPIC_PORT -msmart-io=1 -Wall -msfr-warn=off
	@${FIXDEPS} "${OBJECTDIR}/_ext/1591518261/uart1_queue.o.d" $(SILENT)  -rsi ${MP_CC_DIR}../ 
	
${OBJECTDIR}/_ext/1090805370/uart2.o: ../../lib/uart2/uart2.c  nbproject/Makefile-${CND_CONF}.mk
	@${MKDIR} ${OBJECTDIR}/_ext/1090805370 
	@${RM} ${OBJECTDIR}/_ext/1090805370/uart2.o.d 
	${MP_CC} $(MP_EXTRA_CC_PRE)  ../../lib/uart2/uart2.c  -o ${OBJECTDIR}/_ext/1090805370/uart2.o  -c -mcpu=$(MP_PROCESSOR_OPTION)  -MMD -MF "${OBJECTDIR}/_ext/1090805370/uart2.o.d"        -g -D__DEBUG -D__MPLAB_DEBUGGER_ICD3=1  -omf=elf -mlarge-code -mlarge-data -O1 -I"..\..\lib\FreeRTOS" -I"..\..\lib" -I"..\..\lib\button" -I"..\..\lib\adc" -I".." -I"..\..\lib\i2c" -I"..\..\lib\bmp085" -I"..\..\lib\hmc5843" -I"..\..\lib\max7456" -I"..\..\lib\matrix" -I"..\..\lib\quaternion" -I"..\..\lib\pid" -I"..\..\lib\pwm_in" -I"..\..\lib\led" -I"..\..\lib\ppm_in" -I"..\..\lib\uart2" -I"..\..\lib\uart1_queue" -I"..\..\lib\servo" -I"..\..\lib\scp1000" -I"..\..\lib\microcontroller" -I"..\..\lib\gps" -I"..\..\lib\dataflash" -DMPLAB_DSPIC_PORT -msmart-io=1 -Wall -msfr-warn=off
	@${FIXDEPS} "${OBJECTDIR}/_ext/1090805370/uart2.o.d" $(SILENT)  -rsi ${MP_CC_DIR}../ 
	
${OBJECTDIR}/_ext/2082761406/mpu6000.o: ../../lib/mpu6000/mpu6000.c  nbproject/Makefile-${CND_CONF}.mk
	@${MKDIR} ${OBJECTDIR}/_ext/2082761406 
	@${RM} ${OBJECTDIR}/_ext/2082761406/mpu6000.o.d 
	${MP_CC} $(MP_EXTRA_CC_PRE)  ../../lib/mpu6000/mpu6000.c  -o ${OBJECTDIR}/_ext/2082761406/mpu6000.o  -c -mcpu=$(MP_PROCESSOR_OPTION)  -MMD -MF "${OBJECTDIR}/_ext/2082761406/mpu6000.o.d"        -g -D__DEBUG -D__MPLAB_DEBUGGER_ICD3=1  -omf=elf -mlarge-code -mlarge-data -O1 -I"..\..\lib\FreeRTOS" -I"..\..\lib" -I"..\..\lib\button" -I"..\..\lib\adc" -I".." -I"..\..\lib\i2c" -I"..\..\lib\bmp085" -I"..\..\lib\hmc5843" -I"..\..\lib\max7456" -I"..\..\lib\matrix" -I"..\..\lib\quaternion" -I"..\..\lib\pid" -I"..\..\lib\pwm_in" -I"..\..\lib\led" -I"..\..\lib\ppm_in" -I"..\..\lib\uart2" -I"..\..\lib\uart1_queue" -I"..\..\lib\servo" -I"..\..\lib\scp1000" -I"..\..\lib\microcontroller" -I"..\..\lib\gps" -I"..\..\lib\dataflash" -DMPLAB_DSPIC_PORT -msmart-io=1 -Wall -msfr-warn=off
	@${FIXDEPS} "${OBJECTDIR}/_ext/2082761406/mpu6000.o.d" $(SILENT)  -rsi ${MP_CC_DIR}../ 
	
${OBJECTDIR}/_ext/1472/communication_csv.o: ../communication_csv.c  nbproject/Makefile-${CND_CONF}.mk
	@${MKDIR} ${OBJECTDIR}/_ext/1472 
	@${RM} ${OBJECTDIR}/_ext/1472/communication_csv.o.d 
	${MP_CC} $(MP_EXTRA_CC_PRE)  ../communication_csv.c  -o ${OBJECTDIR}/_ext/1472/communication_csv.o  -c -mcpu=$(MP_PROCESSOR_OPTION)  -MMD -MF "${OBJECTDIR}/_ext/1472/communication_csv.o.d"        -g -D__DEBUG -D__MPLAB_DEBUGGER_ICD3=1  -omf=elf -mlarge-code -mlarge-data -O1 -I"..\..\lib\FreeRTOS" -I"..\..\lib" -I"..\..\lib\button" -I"..\..\lib\adc" -I".." -I"..\..\lib\i2c" -I"..\..\lib\bmp085" -I"..\..\lib\hmc5843" -I"..\..\lib\max7456" -I"..\..\lib\matrix" -I"..\..\lib\quaternion" -I"..\..\lib\pid" -I"..\..\lib\pwm_in" -I"..\..\lib\led" -I"..\..\lib\ppm_in" -I"..\..\lib\uart2" -I"..\..\lib\uart1_queue" -I"..\..\lib\servo" -I"..\..\lib\scp1000" -I"..\..\lib\microcontroller" -I"..\..\lib\gps" -I"..\..\lib\dataflash" -DMPLAB_DSPIC_PORT -msmart-io=1 -Wall -msfr-warn=off
	@${FIXDEPS} "${OBJECTDIR}/_ext/1472/communication_csv.o.d" $(SILENT)  -rsi ${MP_CC_DIR}../ 
	
${OBJECTDIR}/_ext/1472/configuration.o: ../configuration.c  nbproject/Makefile-${CND_CONF}.mk
	@${MKDIR} ${OBJECTDIR}/_ext/1472 
	@${RM} ${OBJECTDIR}/_ext/1472/configuration.o.d 
	${MP_CC} $(MP_EXTRA_CC_PRE)  ../configuration.c  -o ${OBJECTDIR}/_ext/1472/configuration.o  -c -mcpu=$(MP_PROCESSOR_OPTION)  -MMD -MF "${OBJECTDIR}/_ext/1472/configuration.o.d"        -g -D__DEBUG -D__MPLAB_DEBUGGER_ICD3=1  -omf=elf -mlarge-code -mlarge-data -O1 -I"..\..\lib\FreeRTOS" -I"..\..\lib" -I"..\..\lib\button" -I"..\..\lib\adc" -I".." -I"..\..\lib\i2c" -I"..\..\lib\bmp085" -I"..\..\lib\hmc5843" -I"..\..\lib\max7456" -I"..\..\lib\matrix" -I"..\..\lib\quaternion" -I"..\..\lib\pid" -I"..\..\lib\pwm_in" -I"..\..\lib\led" -I"..\..\lib\ppm_in" -I"..\..\lib\uart2" -I"..\..\lib\uart1_queue" -I"..\..\lib\servo" -I"..\..\lib\scp1000" -I"..\..\lib\microcontroller" -I"..\..\lib\gps" -I"..\..\lib\dataflash" -DMPLAB_DSPIC_PORT -msmart-io=1 -Wall -msfr-warn=off
	@${FIXDEPS} "${OBJECTDIR}/_ext/1472/configuration.o.d" $(SILENT)  -rsi ${MP_CC_DIR}../ 
	
${OBJECTDIR}/_ext/1472/gluonscript.o: ../gluonscript.c  nbproject/Makefile-${CND_CONF}.mk
	@${MKDIR} ${OBJECTDIR}/_ext/1472 
	@${RM} ${OBJECTDIR}/_ext/1472/gluonscript.o.d 
	${MP_CC} $(MP_EXTRA_CC_PRE)  ../gluonscript.c  -o ${OBJECTDIR}/_ext/1472/gluonscript.o  -c -mcpu=$(MP_PROCESSOR_OPTION)  -MMD -MF "${OBJECTDIR}/_ext/1472/gluonscript.o.d"        -g -D__DEBUG -D__MPLAB_DEBUGGER_ICD3=1  -omf=elf -mlarge-code -mlarge-data -O1 -I"..\..\lib\FreeRTOS" -I"..\..\lib" -I"..\..\lib\button" -I"..\..\lib\adc" -I".." -I"..\..\lib\i2c" -I"..\..\lib\bmp085" -I"..\..\lib\hmc5843" -I"..\..\lib\max7456" -I"..\..\lib\matrix" -I"..\..\lib\quaternion" -I"..\..\lib\pid" -I"..\..\lib\pwm_in" -I"..\..\lib\led" -I"..\..\lib\ppm_in" -I"..\..\lib\uart2" -I"..\..\lib\uart1_queue" -I"..\..\lib\servo" -I"..\..\lib\scp1000" -I"..\..\lib\microcontroller" -I"..\..\lib\gps" -I"..\..\lib\dataflash" -DMPLAB_DSPIC_PORT -msmart-io=1 -Wall -msfr-warn=off
	@${FIXDEPS} "${OBJECTDIR}/_ext/1472/gluonscript.o.d" $(SILENT)  -rsi ${MP_CC_DIR}../ 
	
${OBJECTDIR}/_ext/1472/osd_max7456.o: ../osd_max7456.c  nbproject/Makefile-${CND_CONF}.mk
	@${MKDIR} ${OBJECTDIR}/_ext/1472 
	@${RM} ${OBJECTDIR}/_ext/1472/osd_max7456.o.d 
	${MP_CC} $(MP_EXTRA_CC_PRE)  ../osd_max7456.c  -o ${OBJECTDIR}/_ext/1472/osd_max7456.o  -c -mcpu=$(MP_PROCESSOR_OPTION)  -MMD -MF "${OBJECTDIR}/_ext/1472/osd_max7456.o.d"        -g -D__DEBUG -D__MPLAB_DEBUGGER_ICD3=1  -omf=elf -mlarge-code -mlarge-data -O1 -I"..\..\lib\FreeRTOS" -I"..\..\lib" -I"..\..\lib\button" -I"..\..\lib\adc" -I".." -I"..\..\lib\i2c" -I"..\..\lib\bmp085" -I"..\..\lib\hmc5843" -I"..\..\lib\max7456" -I"..\..\lib\matrix" -I"..\..\lib\quaternion" -I"..\..\lib\pid" -I"..\..\lib\pwm_in" -I"..\..\lib\led" -I"..\..\lib\ppm_in" -I"..\..\lib\uart2" -I"..\..\lib\uart1_queue" -I"..\..\lib\servo" -I"..\..\lib\scp1000" -I"..\..\lib\microcontroller" -I"..\..\lib\gps" -I"..\..\lib\dataflash" -DMPLAB_DSPIC_PORT -msmart-io=1 -Wall -msfr-warn=off
	@${FIXDEPS} "${OBJECTDIR}/_ext/1472/osd_max7456.o.d" $(SILENT)  -rsi ${MP_CC_DIR}../ 
	
${OBJECTDIR}/_ext/1472/rtos_pilot.o: ../rtos_pilot.c  nbproject/Makefile-${CND_CONF}.mk
	@${MKDIR} ${OBJECTDIR}/_ext/1472 
	@${RM} ${OBJECTDIR}/_ext/1472/rtos_pilot.o.d 
	${MP_CC} $(MP_EXTRA_CC_PRE)  ../rtos_pilot.c  -o ${OBJECTDIR}/_ext/1472/rtos_pilot.o  -c -mcpu=$(MP_PROCESSOR_OPTION)  -MMD -MF "${OBJECTDIR}/_ext/1472/rtos_pilot.o.d"        -g -D__DEBUG -D__MPLAB_DEBUGGER_ICD3=1  -omf=elf -mlarge-code -mlarge-data -O1 -I"..\..\lib\FreeRTOS" -I"..\..\lib" -I"..\..\lib\button" -I"..\..\lib\adc" -I".." -I"..\..\lib\i2c" -I"..\..\lib\bmp085" -I"..\..\lib\hmc5843" -I"..\..\lib\max7456" -I"..\..\lib\matrix" -I"..\..\lib\quaternion" -I"..\..\lib\pid" -I"..\..\lib\pwm_in" -I"..\..\lib\led" -I"..\..\lib\ppm_in" -I"..\..\lib\uart2" -I"..\..\lib\uart1_queue" -I"..\..\lib\servo" -I"..\..\lib\scp1000" -I"..\..\lib\microcontroller" -I"..\..\lib\gps" -I"..\..\lib\dataflash" -DMPLAB_DSPIC_PORT -msmart-io=1 -Wall -msfr-warn=off
	@${FIXDEPS} "${OBJECTDIR}/_ext/1472/rtos_pilot.o.d" $(SILENT)  -rsi ${MP_CC_DIR}../ 
	
${OBJECTDIR}/_ext/1472/ahrs_kalman_2x3.o: ../ahrs_kalman_2x3.c  nbproject/Makefile-${CND_CONF}.mk
	@${MKDIR} ${OBJECTDIR}/_ext/1472 
	@${RM} ${OBJECTDIR}/_ext/1472/ahrs_kalman_2x3.o.d 
	${MP_CC} $(MP_EXTRA_CC_PRE)  ../ahrs_kalman_2x3.c  -o ${OBJECTDIR}/_ext/1472/ahrs_kalman_2x3.o  -c -mcpu=$(MP_PROCESSOR_OPTION)  -MMD -MF "${OBJECTDIR}/_ext/1472/ahrs_kalman_2x3.o.d"        -g -D__DEBUG -D__MPLAB_DEBUGGER_ICD3=1  -omf=elf -mlarge-code -mlarge-data -O1 -I"..\..\lib\FreeRTOS" -I"..\..\lib" -I"..\..\lib\button" -I"..\..\lib\adc" -I".." -I"..\..\lib\i2c" -I"..\..\lib\bmp085" -I"..\..\lib\hmc5843" -I"..\..\lib\max7456" -I"..\..\lib\matrix" -I"..\..\lib\quaternion" -I"..\..\lib\pid" -I"..\..\lib\pwm_in" -I"..\..\lib\led" -I"..\..\lib\ppm_in" -I"..\..\lib\uart2" -I"..\..\lib\uart1_queue" -I"..\..\lib\servo" -I"..\..\lib\scp1000" -I"..\..\lib\microcontroller" -I"..\..\lib\gps" -I"..\..\lib\dataflash" -DMPLAB_DSPIC_PORT -msmart-io=1 -Wall -msfr-warn=off
	@${FIXDEPS} "${OBJECTDIR}/_ext/1472/ahrs_kalman_2x3.o.d" $(SILENT)  -rsi ${MP_CC_DIR}../ 
	
${OBJECTDIR}/_ext/1472/handler_alarms.o: ../handler_alarms.c  nbproject/Makefile-${CND_CONF}.mk
	@${MKDIR} ${OBJECTDIR}/_ext/1472 
	@${RM} ${OBJECTDIR}/_ext/1472/handler_alarms.o.d 
	${MP_CC} $(MP_EXTRA_CC_PRE)  ../handler_alarms.c  -o ${OBJECTDIR}/_ext/1472/handler_alarms.o  -c -mcpu=$(MP_PROCESSOR_OPTION)  -MMD -MF "${OBJECTDIR}/_ext/1472/handler_alarms.o.d"        -g -D__DEBUG -D__MPLAB_DEBUGGER_ICD3=1  -omf=elf -mlarge-code -mlarge-data -O1 -I"..\..\lib\FreeRTOS" -I"..\..\lib" -I"..\..\lib\button" -I"..\..\lib\adc" -I".." -I"..\..\lib\i2c" -I"..\..\lib\bmp085" -I"..\..\lib\hmc5843" -I"..\..\lib\max7456" -I"..\..\lib\matrix" -I"..\..\lib\quaternion" -I"..\..\lib\pid" -I"..\..\lib\pwm_in" -I"..\..\lib\led" -I"..\..\lib\ppm_in" -I"..\..\lib\uart2" -I"..\..\lib\uart1_queue" -I"..\..\lib\servo" -I"..\..\lib\scp1000" -I"..\..\lib\microcontroller" -I"..\..\lib\gps" -I"..\..\lib\dataflash" -DMPLAB_DSPIC_PORT -msmart-io=1 -Wall -msfr-warn=off
	@${FIXDEPS} "${OBJECTDIR}/_ext/1472/handler_alarms.o.d" $(SILENT)  -rsi ${MP_CC_DIR}../ 
	
${OBJECTDIR}/_ext/1472/handler_trigger.o: ../handler_trigger.c  nbproject/Makefile-${CND_CONF}.mk
	@${MKDIR} ${OBJECTDIR}/_ext/1472 
	@${RM} ${OBJECTDIR}/_ext/1472/handler_trigger.o.d 
	${MP_CC} $(MP_EXTRA_CC_PRE)  ../handler_trigger.c  -o ${OBJECTDIR}/_ext/1472/handler_trigger.o  -c -mcpu=$(MP_PROCESSOR_OPTION)  -MMD -MF "${OBJECTDIR}/_ext/1472/handler_trigger.o.d"        -g -D__DEBUG -D__MPLAB_DEBUGGER_ICD3=1  -omf=elf -mlarge-code -mlarge-data -O1 -I"..\..\lib\FreeRTOS" -I"..\..\lib" -I"..\..\lib\button" -I"..\..\lib\adc" -I".." -I"..\..\lib\i2c" -I"..\..\lib\bmp085" -I"..\..\lib\hmc5843" -I"..\..\lib\max7456" -I"..\..\lib\matrix" -I"..\..\lib\quaternion" -I"..\..\lib\pid" -I"..\..\lib\pwm_in" -I"..\..\lib\led" -I"..\..\lib\ppm_in" -I"..\..\lib\uart2" -I"..\..\lib\uart1_queue" -I"..\..\lib\servo" -I"..\..\lib\scp1000" -I"..\..\lib\microcontroller" -I"..\..\lib\gps" -I"..\..\lib\dataflash" -DMPLAB_DSPIC_PORT -msmart-io=1 -Wall -msfr-warn=off
	@${FIXDEPS} "${OBJECTDIR}/_ext/1472/handler_trigger.o.d" $(SILENT)  -rsi ${MP_CC_DIR}../ 
	
${OBJECTDIR}/_ext/1472/handler_navigation.o: ../handler_navigation.c  nbproject/Makefile-${CND_CONF}.mk
	@${MKDIR} ${OBJECTDIR}/_ext/1472 
	@${RM} ${OBJECTDIR}/_ext/1472/handler_navigation.o.d 
	${MP_CC} $(MP_EXTRA_CC_PRE)  ../handler_navigation.c  -o ${OBJECTDIR}/_ext/1472/handler_navigation.o  -c -mcpu=$(MP_PROCESSOR_OPTION)  -MMD -MF "${OBJECTDIR}/_ext/1472/handler_navigation.o.d"        -g -D__DEBUG -D__MPLAB_DEBUGGER_ICD3=1  -omf=elf -mlarge-code -mlarge-data -O1 -I"..\..\lib\FreeRTOS" -I"..\..\lib" -I"..\..\lib\button" -I"..\..\lib\adc" -I".." -I"..\..\lib\i2c" -I"..\..\lib\bmp085" -I"..\..\lib\hmc5843" -I"..\..\lib\max7456" -I"..\..\lib\matrix" -I"..\..\lib\quaternion" -I"..\..\lib\pid" -I"..\..\lib\pwm_in" -I"..\..\lib\led" -I"..\..\lib\ppm_in" -I"..\..\lib\uart2" -I"..\..\lib\uart1_queue" -I"..\..\lib\servo" -I"..\..\lib\scp1000" -I"..\..\lib\microcontroller" -I"..\..\lib\gps" -I"..\..\lib\dataflash" -DMPLAB_DSPIC_PORT -msmart-io=1 -Wall -msfr-warn=off
	@${FIXDEPS} "${OBJECTDIR}/_ext/1472/handler_navigation.o.d" $(SILENT)  -rsi ${MP_CC_DIR}../ 
	
${OBJECTDIR}/_ext/1472/handler_flightplan_switch.o: ../handler_flightplan_switch.c  nbproject/Makefile-${CND_CONF}.mk
	@${MKDIR} ${OBJECTDIR}/_ext/1472 
	@${RM} ${OBJECTDIR}/_ext/1472/handler_flightplan_switch.o.d 
	${MP_CC} $(MP_EXTRA_CC_PRE)  ../handler_flightplan_switch.c  -o ${OBJECTDIR}/_ext/1472/handler_flightplan_switch.o  -c -mcpu=$(MP_PROCESSOR_OPTION)  -MMD -MF "${OBJECTDIR}/_ext/1472/handler_flightplan_switch.o.d"        -g -D__DEBUG -D__MPLAB_DEBUGGER_ICD3=1  -omf=elf -mlarge-code -mlarge-data -O1 -I"..\..\lib\FreeRTOS" -I"..\..\lib" -I"..\..\lib\button" -I"..\..\lib\adc" -I".." -I"..\..\lib\i2c" -I"..\..\lib\bmp085" -I"..\..\lib\hmc5843" -I"..\..\lib\max7456" -I"..\..\lib\matrix" -I"..\..\lib\quaternion" -I"..\..\lib\pid" -I"..\..\lib\pwm_in" -I"..\..\lib\led" -I"..\..\lib\ppm_in" -I"..\..\lib\uart2" -I"..\..\lib\uart1_queue" -I"..\..\lib\servo" -I"..\..\lib\scp1000" -I"..\..\lib\microcontroller" -I"..\..\lib\gps" -I"..\..\lib\dataflash" -DMPLAB_DSPIC_PORT -msmart-io=1 -Wall -msfr-warn=off
	@${FIXDEPS} "${OBJECTDIR}/_ext/1472/handler_flightplan_switch.o.d" $(SILENT)  -rsi ${MP_CC_DIR}../ 
	
${OBJECTDIR}/_ext/1472/task_gps.o: ../task_gps.c  nbproject/Makefile-${CND_CONF}.mk
	@${MKDIR} ${OBJECTDIR}/_ext/1472 
	@${RM} ${OBJECTDIR}/_ext/1472/task_gps.o.d 
	${MP_CC} $(MP_EXTRA_CC_PRE)  ../task_gps.c  -o ${OBJECTDIR}/_ext/1472/task_gps.o  -c -mcpu=$(MP_PROCESSOR_OPTION)  -MMD -MF "${OBJECTDIR}/_ext/1472/task_gps.o.d"        -g -D__DEBUG -D__MPLAB_DEBUGGER_ICD3=1  -omf=elf -mlarge-code -mlarge-data -O1 -I"..\..\lib\FreeRTOS" -I"..\..\lib" -I"..\..\lib\button" -I"..\..\lib\adc" -I".." -I"..\..\lib\i2c" -I"..\..\lib\bmp085" -I"..\..\lib\hmc5843" -I"..\..\lib\max7456" -I"..\..\lib\matrix" -I"..\..\lib\quaternion" -I"..\..\lib\pid" -I"..\..\lib\pwm_in" -I"..\..\lib\led" -I"..\..\lib\ppm_in" -I"..\..\lib\uart2" -I"..\..\lib\uart1_queue" -I"..\..\lib\servo" -I"..\..\lib\scp1000" -I"..\..\lib\microcontroller" -I"..\..\lib\gps" -I"..\..\lib\dataflash" -DMPLAB_DSPIC_PORT -msmart-io=1 -Wall -msfr-warn=off
	@${FIXDEPS} "${OBJECTDIR}/_ext/1472/task_gps.o.d" $(SILENT)  -rsi ${MP_CC_DIR}../ 
	
${OBJECTDIR}/_ext/1472/task_datalogger.o: ../task_datalogger.c  nbproject/Makefile-${CND_CONF}.mk
	@${MKDIR} ${OBJECTDIR}/_ext/1472 
	@${RM} ${OBJECTDIR}/_ext/1472/task_datalogger.o.d 
	${MP_CC} $(MP_EXTRA_CC_PRE)  ../task_datalogger.c  -o ${OBJECTDIR}/_ext/1472/task_datalogger.o  -c -mcpu=$(MP_PROCESSOR_OPTION)  -MMD -MF "${OBJECTDIR}/_ext/1472/task_datalogger.o.d"        -g -D__DEBUG -D__MPLAB_DEBUGGER_ICD3=1  -omf=elf -mlarge-code -mlarge-data -O1 -I"..\..\lib\FreeRTOS" -I"..\..\lib" -I"..\..\lib\button" -I"..\..\lib\adc" -I".." -I"..\..\lib\i2c" -I"..\..\lib\bmp085" -I"..\..\lib\hmc5843" -I"..\..\lib\max7456" -I"..\..\lib\matrix" -I"..\..\lib\quaternion" -I"..\..\lib\pid" -I"..\..\lib\pwm_in" -I"..\..\lib\led" -I"..\..\lib\ppm_in" -I"..\..\lib\uart2" -I"..\..\lib\uart1_queue" -I"..\..\lib\servo" -I"..\..\lib\scp1000" -I"..\..\lib\microcontroller" -I"..\..\lib\gps" -I"..\..\lib\dataflash" -DMPLAB_DSPIC_PORT -msmart-io=1 -Wall -msfr-warn=off
	@${FIXDEPS} "${OBJECTDIR}/_ext/1472/task_datalogger.o.d" $(SILENT)  -rsi ${MP_CC_DIR}../ 
	
${OBJECTDIR}/_ext/1472/task_control.o: ../task_control.c  nbproject/Makefile-${CND_CONF}.mk
	@${MKDIR} ${OBJECTDIR}/_ext/1472 
	@${RM} ${OBJECTDIR}/_ext/1472/task_control.o.d 
	${MP_CC} $(MP_EXTRA_CC_PRE)  ../task_control.c  -o ${OBJECTDIR}/_ext/1472/task_control.o  -c -mcpu=$(MP_PROCESSOR_OPTION)  -MMD -MF "${OBJECTDIR}/_ext/1472/task_control.o.d"        -g -D__DEBUG -D__MPLAB_DEBUGGER_ICD3=1  -omf=elf -mlarge-code -mlarge-data -O1 -I"..\..\lib\FreeRTOS" -I"..\..\lib" -I"..\..\lib\button" -I"..\..\lib\adc" -I".." -I"..\..\lib\i2c" -I"..\..\lib\bmp085" -I"..\..\lib\hmc5843" -I"..\..\lib\max7456" -I"..\..\lib\matrix" -I"..\..\lib\quaternion" -I"..\..\lib\pid" -I"..\..\lib\pwm_in" -I"..\..\lib\led" -I"..\..\lib\ppm_in" -I"..\..\lib\uart2" -I"..\..\lib\uart1_queue" -I"..\..\lib\servo" -I"..\..\lib\scp1000" -I"..\..\lib\microcontroller" -I"..\..\lib\gps" -I"..\..\lib\dataflash" -DMPLAB_DSPIC_PORT -msmart-io=1 -Wall -msfr-warn=off
	@${FIXDEPS} "${OBJECTDIR}/_ext/1472/task_control.o.d" $(SILENT)  -rsi ${MP_CC_DIR}../ 
	
${OBJECTDIR}/_ext/1472/task_sensors_analog.o: ../task_sensors_analog.c  nbproject/Makefile-${CND_CONF}.mk
	@${MKDIR} ${OBJECTDIR}/_ext/1472 
	@${RM} ${OBJECTDIR}/_ext/1472/task_sensors_analog.o.d 
	${MP_CC} $(MP_EXTRA_CC_PRE)  ../task_sensors_analog.c  -o ${OBJECTDIR}/_ext/1472/task_sensors_analog.o  -c -mcpu=$(MP_PROCESSOR_OPTION)  -MMD -MF "${OBJECTDIR}/_ext/1472/task_sensors_analog.o.d"        -g -D__DEBUG -D__MPLAB_DEBUGGER_ICD3=1  -omf=elf -mlarge-code -mlarge-data -O1 -I"..\..\lib\FreeRTOS" -I"..\..\lib" -I"..\..\lib\button" -I"..\..\lib\adc" -I".." -I"..\..\lib\i2c" -I"..\..\lib\bmp085" -I"..\..\lib\hmc5843" -I"..\..\lib\max7456" -I"..\..\lib\matrix" -I"..\..\lib\quaternion" -I"..\..\lib\pid" -I"..\..\lib\pwm_in" -I"..\..\lib\led" -I"..\..\lib\ppm_in" -I"..\..\lib\uart2" -I"..\..\lib\uart1_queue" -I"..\..\lib\servo" -I"..\..\lib\scp1000" -I"..\..\lib\microcontroller" -I"..\..\lib\gps" -I"..\..\lib\dataflash" -DMPLAB_DSPIC_PORT -msmart-io=1 -Wall -msfr-warn=off
	@${FIXDEPS} "${OBJECTDIR}/_ext/1472/task_sensors_analog.o.d" $(SILENT)  -rsi ${MP_CC_DIR}../ 
	
${OBJECTDIR}/_ext/1472/sensors.o: ../sensors.c  nbproject/Makefile-${CND_CONF}.mk
	@${MKDIR} ${OBJECTDIR}/_ext/1472 
	@${RM} ${OBJECTDIR}/_ext/1472/sensors.o.d 
	${MP_CC} $(MP_EXTRA_CC_PRE)  ../sensors.c  -o ${OBJECTDIR}/_ext/1472/sensors.o  -c -mcpu=$(MP_PROCESSOR_OPTION)  -MMD -MF "${OBJECTDIR}/_ext/1472/sensors.o.d"        -g -D__DEBUG -D__MPLAB_DEBUGGER_ICD3=1  -omf=elf -mlarge-code -mlarge-data -O1 -I"..\..\lib\FreeRTOS" -I"..\..\lib" -I"..\..\lib\button" -I"..\..\lib\adc" -I".." -I"..\..\lib\i2c" -I"..\..\lib\bmp085" -I"..\..\lib\hmc5843" -I"..\..\lib\max7456" -I"..\..\lib\matrix" -I"..\..\lib\quaternion" -I"..\..\lib\pid" -I"..\..\lib\pwm_in" -I"..\..\lib\led" -I"..\..\lib\ppm_in" -I"..\..\lib\uart2" -I"..\..\lib\uart1_queue" -I"..\..\lib\servo" -I"..\..\lib\scp1000" -I"..\..\lib\microcontroller" -I"..\..\lib\gps" -I"..\..\lib\dataflash" -DMPLAB_DSPIC_PORT -msmart-io=1 -Wall -msfr-warn=off
	@${FIXDEPS} "${OBJECTDIR}/_ext/1472/sensors.o.d" $(SILENT)  -rsi ${MP_CC_DIR}../ 
	
${OBJECTDIR}/_ext/1472/task_sensors_mpu6000.o: ../task_sensors_mpu6000.c  nbproject/Makefile-${CND_CONF}.mk
	@${MKDIR} ${OBJECTDIR}/_ext/1472 
	@${RM} ${OBJECTDIR}/_ext/1472/task_sensors_mpu6000.o.d 
	${MP_CC} $(MP_EXTRA_CC_PRE)  ../task_sensors_mpu6000.c  -o ${OBJECTDIR}/_ext/1472/task_sensors_mpu6000.o  -c -mcpu=$(MP_PROCESSOR_OPTION)  -MMD -MF "${OBJECTDIR}/_ext/1472/task_sensors_mpu6000.o.d"        -g -D__DEBUG -D__MPLAB_DEBUGGER_ICD3=1  -omf=elf -mlarge-code -mlarge-data -O1 -I"..\..\lib\FreeRTOS" -I"..\..\lib" -I"..\..\lib\button" -I"..\..\lib\adc" -I".." -I"..\..\lib\i2c" -I"..\..\lib\bmp085" -I"..\..\lib\hmc5843" -I"..\..\lib\max7456" -I"..\..\lib\matrix" -I"..\..\lib\quaternion" -I"..\..\lib\pid" -I"..\..\lib\pwm_in" -I"..\..\lib\led" -I"..\..\lib\ppm_in" -I"..\..\lib\uart2" -I"..\..\lib\uart1_queue" -I"..\..\lib\servo" -I"..\..\lib\scp1000" -I"..\..\lib\microcontroller" -I"..\..\lib\gps" -I"..\..\lib\dataflash" -DMPLAB_DSPIC_PORT -msmart-io=1 -Wall -msfr-warn=off
	@${FIXDEPS} "${OBJECTDIR}/_ext/1472/task_sensors_mpu6000.o.d" $(SILENT)  -rsi ${MP_CC_DIR}../ 
	
else
${OBJECTDIR}/_ext/1970174492/croutine.o: ../../lib/FreeRTOS/croutine.c  nbproject/Makefile-${CND_CONF}.mk
	@${MKDIR} ${OBJECTDIR}/_ext/1970174492 
	@${RM} ${OBJECTDIR}/_ext/1970174492/croutine.o.d 
	${MP_CC} $(MP_EXTRA_CC_PRE)  ../../lib/FreeRTOS/croutine.c  -o ${OBJECTDIR}/_ext/1970174492/croutine.o  -c -mcpu=$(MP_PROCESSOR_OPTION)  -MMD -MF "${OBJECTDIR}/_ext/1970174492/croutine.o.d"        -g -omf=elf -mlarge-code -mlarge-data -O1 -I"..\..\lib\FreeRTOS" -I"..\..\lib" -I"..\..\lib\button" -I"..\..\lib\adc" -I".." -I"..\..\lib\i2c" -I"..\..\lib\bmp085" -I"..\..\lib\hmc5843" -I"..\..\lib\max7456" -I"..\..\lib\matrix" -I"..\..\lib\quaternion" -I"..\..\lib\pid" -I"..\..\lib\pwm_in" -I"..\..\lib\led" -I"..\..\lib\ppm_in" -I"..\..\lib\uart2" -I"..\..\lib\uart1_queue" -I"..\..\lib\servo" -I"..\..\lib\scp1000" -I"..\..\lib\microcontroller" -I"..\..\lib\gps" -I"..\..\lib\dataflash" -DMPLAB_DSPIC_PORT -msmart-io=1 -Wall -msfr-warn=off
	@${FIXDEPS} "${OBJECTDIR}/_ext/1970174492/croutine.o.d" $(SILENT)  -rsi ${MP_CC_DIR}../ 
	
${OBJECTDIR}/_ext/1970174492/heap_1.o: ../../lib/FreeRTOS/heap_1.c  nbproject/Makefile-${CND_CONF}.mk
	@${MKDIR} ${OBJECTDIR}/_ext/1970174492 
	@${RM} ${OBJECTDIR}/_ext/1970174492/heap_1.o.d 
	${MP_CC} $(MP_EXTRA_CC_PRE)  ../../lib/FreeRTOS/heap_1.c  -o ${OBJECTDIR}/_ext/1970174492/heap_1.o  -c -mcpu=$(MP_PROCESSOR_OPTION)  -MMD -MF "${OBJECTDIR}/_ext/1970174492/heap_1.o.d"        -g -omf=elf -mlarge-code -mlarge-data -O1 -I"..\..\lib\FreeRTOS" -I"..\..\lib" -I"..\..\lib\button" -I"..\..\lib\adc" -I".." -I"..\..\lib\i2c" -I"..\..\lib\bmp085" -I"..\..\lib\hmc5843" -I"..\..\lib\max7456" -I"..\..\lib\matrix" -I"..\..\lib\quaternion" -I"..\..\lib\pid" -I"..\..\lib\pwm_in" -I"..\..\lib\led" -I"..\..\lib\ppm_in" -I"..\..\lib\uart2" -I"..\..\lib\uart1_queue" -I"..\..\lib\servo" -I"..\..\lib\scp1000" -I"..\..\lib\microcontroller" -I"..\..\lib\gps" -I"..\..\lib\dataflash" -DMPLAB_DSPIC_PORT -msmart-io=1 -Wall -msfr-warn=off
	@${FIXDEPS} "${OBJECTDIR}/_ext/1970174492/heap_1.o.d" $(SILENT)  -rsi ${MP_CC_DIR}../ 
	
${OBJECTDIR}/_ext/1970174492/list.o: ../../lib/FreeRTOS/list.c  nbproject/Makefile-${CND_CONF}.mk
	@${MKDIR} ${OBJECTDIR}/_ext/1970174492 
	@${RM} ${OBJECTDIR}/_ext/1970174492/list.o.d 
	${MP_CC} $(MP_EXTRA_CC_PRE)  ../../lib/FreeRTOS/list.c  -o ${OBJECTDIR}/_ext/1970174492/list.o  -c -mcpu=$(MP_PROCESSOR_OPTION)  -MMD -MF "${OBJECTDIR}/_ext/1970174492/list.o.d"        -g -omf=elf -mlarge-code -mlarge-data -O1 -I"..\..\lib\FreeRTOS" -I"..\..\lib" -I"..\..\lib\button" -I"..\..\lib\adc" -I".." -I"..\..\lib\i2c" -I"..\..\lib\bmp085" -I"..\..\lib\hmc5843" -I"..\..\lib\max7456" -I"..\..\lib\matrix" -I"..\..\lib\quaternion" -I"..\..\lib\pid" -I"..\..\lib\pwm_in" -I"..\..\lib\led" -I"..\..\lib\ppm_in" -I"..\..\lib\uart2" -I"..\..\lib\uart1_queue" -I"..\..\lib\servo" -I"..\..\lib\scp1000" -I"..\..\lib\microcontroller" -I"..\..\lib\gps" -I"..\..\lib\dataflash" -DMPLAB_DSPIC_PORT -msmart-io=1 -Wall -msfr-warn=off
	@${FIXDEPS} "${OBJECTDIR}/_ext/1970174492/list.o.d" $(SILENT)  -rsi ${MP_CC_DIR}../ 
	
${OBJECTDIR}/_ext/1970174492/port.o: ../../lib/FreeRTOS/port.c  nbproject/Makefile-${CND_CONF}.mk
	@${MKDIR} ${OBJECTDIR}/_ext/1970174492 
	@${RM} ${OBJECTDIR}/_ext/1970174492/port.o.d 
	${MP_CC} $(MP_EXTRA_CC_PRE)  ../../lib/FreeRTOS/port.c  -o ${OBJECTDIR}/_ext/1970174492/port.o  -c -mcpu=$(MP_PROCESSOR_OPTION)  -MMD -MF "${OBJECTDIR}/_ext/1970174492/port.o.d"        -g -omf=elf -mlarge-code -mlarge-data -O1 -I"..\..\lib\FreeRTOS" -I"..\..\lib" -I"..\..\lib\button" -I"..\..\lib\adc" -I".." -I"..\..\lib\i2c" -I"..\..\lib\bmp085" -I"..\..\lib\hmc5843" -I"..\..\lib\max7456" -I"..\..\lib\matrix" -I"..\..\lib\quaternion" -I"..\..\lib\pid" -I"..\..\lib\pwm_in" -I"..\..\lib\led" -I"..\..\lib\ppm_in" -I"..\..\lib\uart2" -I"..\..\lib\uart1_queue" -I"..\..\lib\servo" -I"..\..\lib\scp1000" -I"..\..\lib\microcontroller" -I"..\..\lib\gps" -I"..\..\lib\dataflash" -DMPLAB_DSPIC_PORT -msmart-io=1 -Wall -msfr-warn=off
	@${FIXDEPS} "${OBJECTDIR}/_ext/1970174492/port.o.d" $(SILENT)  -rsi ${MP_CC_DIR}../ 
	
${OBJECTDIR}/_ext/1970174492/queue.o: ../../lib/FreeRTOS/queue.c  nbproject/Makefile-${CND_CONF}.mk
	@${MKDIR} ${OBJECTDIR}/_ext/1970174492 
	@${RM} ${OBJECTDIR}/_ext/1970174492/queue.o.d 
	${MP_CC} $(MP_EXTRA_CC_PRE)  ../../lib/FreeRTOS/queue.c  -o ${OBJECTDIR}/_ext/1970174492/queue.o  -c -mcpu=$(MP_PROCESSOR_OPTION)  -MMD -MF "${OBJECTDIR}/_ext/1970174492/queue.o.d"        -g -omf=elf -mlarge-code -mlarge-data -O1 -I"..\..\lib\FreeRTOS" -I"..\..\lib" -I"..\..\lib\button" -I"..\..\lib\adc" -I".." -I"..\..\lib\i2c" -I"..\..\lib\bmp085" -I"..\..\lib\hmc5843" -I"..\..\lib\max7456" -I"..\..\lib\matrix" -I"..\..\lib\quaternion" -I"..\..\lib\pid" -I"..\..\lib\pwm_in" -I"..\..\lib\led" -I"..\..\lib\ppm_in" -I"..\..\lib\uart2" -I"..\..\lib\uart1_queue" -I"..\..\lib\servo" -I"..\..\lib\scp1000" -I"..\..\lib\microcontroller" -I"..\..\lib\gps" -I"..\..\lib\dataflash" -DMPLAB_DSPIC_PORT -msmart-io=1 -Wall -msfr-warn=off
	@${FIXDEPS} "${OBJECTDIR}/_ext/1970174492/queue.o.d" $(SILENT)  -rsi ${MP_CC_DIR}../ 
	
${OBJECTDIR}/_ext/1970174492/tasks.o: ../../lib/FreeRTOS/tasks.c  nbproject/Makefile-${CND_CONF}.mk
	@${MKDIR} ${OBJECTDIR}/_ext/1970174492 
	@${RM} ${OBJECTDIR}/_ext/1970174492/tasks.o.d 
	${MP_CC} $(MP_EXTRA_CC_PRE)  ../../lib/FreeRTOS/tasks.c  -o ${OBJECTDIR}/_ext/1970174492/tasks.o  -c -mcpu=$(MP_PROCESSOR_OPTION)  -MMD -MF "${OBJECTDIR}/_ext/1970174492/tasks.o.d"        -g -omf=elf -mlarge-code -mlarge-data -O1 -I"..\..\lib\FreeRTOS" -I"..\..\lib" -I"..\..\lib\button" -I"..\..\lib\adc" -I".." -I"..\..\lib\i2c" -I"..\..\lib\bmp085" -I"..\..\lib\hmc5843" -I"..\..\lib\max7456" -I"..\..\lib\matrix" -I"..\..\lib\quaternion" -I"..\..\lib\pid" -I"..\..\lib\pwm_in" -I"..\..\lib\led" -I"..\..\lib\ppm_in" -I"..\..\lib\uart2" -I"..\..\lib\uart1_queue" -I"..\..\lib\servo" -I"..\..\lib\scp1000" -I"..\..\lib\microcontroller" -I"..\..\lib\gps" -I"..\..\lib\dataflash" -DMPLAB_DSPIC_PORT -msmart-io=1 -Wall -msfr-warn=off
	@${FIXDEPS} "${OBJECTDIR}/_ext/1970174492/tasks.o.d" $(SILENT)  -rsi ${MP_CC_DIR}../ 
	
${OBJECTDIR}/_ext/1970174492/timers.o: ../../lib/FreeRTOS/timers.c  nbproject/Makefile-${CND_CONF}.mk
	@${MKDIR} ${OBJECTDIR}/_ext/1970174492 
	@${RM} ${OBJECTDIR}/_ext/1970174492/timers.o.d 
	${MP_CC} $(MP_EXTRA_CC_PRE)  ../../lib/FreeRTOS/timers.c  -o ${OBJECTDIR}/_ext/1970174492/timers.o  -c -mcpu=$(MP_PROCESSOR_OPTION)  -MMD -MF "${OBJECTDIR}/_ext/1970174492/timers.o.d"        -g -omf=elf -mlarge-code -mlarge-data -O1 -I"..\..\lib\FreeRTOS" -I"..\..\lib" -I"..\..\lib\button" -I"..\..\lib\adc" -I".." -I"..\..\lib\i2c" -I"..\..\lib\bmp085" -I"..\..\lib\hmc5843" -I"..\..\lib\max7456" -I"..\..\lib\matrix" -I"..\..\lib\quaternion" -I"..\..\lib\pid" -I"..\..\lib\pwm_in" -I"..\..\lib\led" -I"..\..\lib\ppm_in" -I"..\..\lib\uart2" -I"..\..\lib\uart1_queue" -I"..\..\lib\servo" -I"..\..\lib\scp1000" -I"..\..\lib\microcontroller" -I"..\..\lib\gps" -I"..\..\lib\dataflash" -DMPLAB_DSPIC_PORT -msmart-io=1 -Wall -msfr-warn=off
	@${FIXDEPS} "${OBJECTDIR}/_ext/1970174492/timers.o.d" $(SILENT)  -rsi ${MP_CC_DIR}../ 
	
${OBJECTDIR}/_ext/957539446/adc.o: ../../lib/adc/adc.c  nbproject/Makefile-${CND_CONF}.mk
	@${MKDIR} ${OBJECTDIR}/_ext/957539446 
	@${RM} ${OBJECTDIR}/_ext/957539446/adc.o.d 
	${MP_CC} $(MP_EXTRA_CC_PRE)  ../../lib/adc/adc.c  -o ${OBJECTDIR}/_ext/957539446/adc.o  -c -mcpu=$(MP_PROCESSOR_OPTION)  -MMD -MF "${OBJECTDIR}/_ext/957539446/adc.o.d"        -g -omf=elf -mlarge-code -mlarge-data -O1 -I"..\..\lib\FreeRTOS" -I"..\..\lib" -I"..\..\lib\button" -I"..\..\lib\adc" -I".." -I"..\..\lib\i2c" -I"..\..\lib\bmp085" -I"..\..\lib\hmc5843" -I"..\..\lib\max7456" -I"..\..\lib\matrix" -I"..\..\lib\quaternion" -I"..\..\lib\pid" -I"..\..\lib\pwm_in" -I"..\..\lib\led" -I"..\..\lib\ppm_in" -I"..\..\lib\uart2" -I"..\..\lib\uart1_queue" -I"..\..\lib\servo" -I"..\..\lib\scp1000" -I"..\..\lib\microcontroller" -I"..\..\lib\gps" -I"..\..\lib\dataflash" -DMPLAB_DSPIC_PORT -msmart-io=1 -Wall -msfr-warn=off
	@${FIXDEPS} "${OBJECTDIR}/_ext/957539446/adc.o.d" $(SILENT)  -rsi ${MP_CC_DIR}../ 
	
${OBJECTDIR}/_ext/1077768206/bmp085.o: ../../lib/bmp085/bmp085.c  nbproject/Makefile-${CND_CONF}.mk
	@${MKDIR} ${OBJECTDIR}/_ext/1077768206 
	@${RM} ${OBJECTDIR}/_ext/1077768206/bmp085.o.d 
	${MP_CC} $(MP_EXTRA_CC_PRE)  ../../lib/bmp085/bmp085.c  -o ${OBJECTDIR}/_ext/1077768206/bmp085.o  -c -mcpu=$(MP_PROCESSOR_OPTION)  -MMD -MF "${OBJECTDIR}/_ext/1077768206/bmp085.o.d"        -g -omf=elf -mlarge-code -mlarge-data -O1 -I"..\..\lib\FreeRTOS" -I"..\..\lib" -I"..\..\lib\button" -I"..\..\lib\adc" -I".." -I"..\..\lib\i2c" -I"..\..\lib\bmp085" -I"..\..\lib\hmc5843" -I"..\..\lib\max7456" -I"..\..\lib\matrix" -I"..\..\lib\quaternion" -I"..\..\lib\pid" -I"..\..\lib\pwm_in" -I"..\..\lib\led" -I"..\..\lib\ppm_in" -I"..\..\lib\uart2" -I"..\..\lib\uart1_queue" -I"..\..\lib\servo" -I"..\..\lib\scp1000" -I"..\..\lib\microcontroller" -I"..\..\lib\gps" -I"..\..\lib\dataflash" -DMPLAB_DSPIC_PORT -msmart-io=1 -Wall -msfr-warn=off
	@${FIXDEPS} "${OBJECTDIR}/_ext/1077768206/bmp085.o.d" $(SILENT)  -rsi ${MP_CC_DIR}../ 
	
${OBJECTDIR}/_ext/1070193764/button.o: ../../lib/button/button.c  nbproject/Makefile-${CND_CONF}.mk
	@${MKDIR} ${OBJECTDIR}/_ext/1070193764 
	@${RM} ${OBJECTDIR}/_ext/1070193764/button.o.d 
	${MP_CC} $(MP_EXTRA_CC_PRE)  ../../lib/button/button.c  -o ${OBJECTDIR}/_ext/1070193764/button.o  -c -mcpu=$(MP_PROCESSOR_OPTION)  -MMD -MF "${OBJECTDIR}/_ext/1070193764/button.o.d"        -g -omf=elf -mlarge-code -mlarge-data -O1 -I"..\..\lib\FreeRTOS" -I"..\..\lib" -I"..\..\lib\button" -I"..\..\lib\adc" -I".." -I"..\..\lib\i2c" -I"..\..\lib\bmp085" -I"..\..\lib\hmc5843" -I"..\..\lib\max7456" -I"..\..\lib\matrix" -I"..\..\lib\quaternion" -I"..\..\lib\pid" -I"..\..\lib\pwm_in" -I"..\..\lib\led" -I"..\..\lib\ppm_in" -I"..\..\lib\uart2" -I"..\..\lib\uart1_queue" -I"..\..\lib\servo" -I"..\..\lib\scp1000" -I"..\..\lib\microcontroller" -I"..\..\lib\gps" -I"..\..\lib\dataflash" -DMPLAB_DSPIC_PORT -msmart-io=1 -Wall -msfr-warn=off
	@${FIXDEPS} "${OBJECTDIR}/_ext/1070193764/button.o.d" $(SILENT)  -rsi ${MP_CC_DIR}../ 
	
${OBJECTDIR}/_ext/968823332/dataflash.o: ../../lib/dataflash/dataflash.c  nbproject/Makefile-${CND_CONF}.mk
	@${MKDIR} ${OBJECTDIR}/_ext/968823332 
	@${RM} ${OBJECTDIR}/_ext/968823332/dataflash.o.d 
	${MP_CC} $(MP_EXTRA_CC_PRE)  ../../lib/dataflash/dataflash.c  -o ${OBJECTDIR}/_ext/968823332/dataflash.o  -c -mcpu=$(MP_PROCESSOR_OPTION)  -MMD -MF "${OBJECTDIR}/_ext/968823332/dataflash.o.d"        -g -omf=elf -mlarge-code -mlarge-data -O1 -I"..\..\lib\FreeRTOS" -I"..\..\lib" -I"..\..\lib\button" -I"..\..\lib\adc" -I".." -I"..\..\lib\i2c" -I"..\..\lib\bmp085" -I"..\..\lib\hmc5843" -I"..\..\lib\max7456" -I"..\..\lib\matrix" -I"..\..\lib\quaternion" -I"..\..\lib\pid" -I"..\..\lib\pwm_in" -I"..\..\lib\led" -I"..\..\lib\ppm_in" -I"..\..\lib\uart2" -I"..\..\lib\uart1_queue" -I"..\..\lib\servo" -I"..\..\lib\scp1000" -I"..\..\lib\microcontroller" -I"..\..\lib\gps" -I"..\..\lib\dataflash" -DMPLAB_DSPIC_PORT -msmart-io=1 -Wall -msfr-warn=off
	@${FIXDEPS} "${OBJECTDIR}/_ext/968823332/dataflash.o.d" $(SILENT)  -rsi ${MP_CC_DIR}../ 
	
${OBJECTDIR}/_ext/957545600/gps.o: ../../lib/gps/gps.c  nbproject/Makefile-${CND_CONF}.mk
	@${MKDIR} ${OBJECTDIR}/_ext/957545600 
	@${RM} ${OBJECTDIR}/_ext/957545600/gps.o.d 
	${MP_CC} $(MP_EXTRA_CC_PRE)  ../../lib/gps/gps.c  -o ${OBJECTDIR}/_ext/957545600/gps.o  -c -mcpu=$(MP_PROCESSOR_OPTION)  -MMD -MF "${OBJECTDIR}/_ext/957545600/gps.o.d"        -g -omf=elf -mlarge-code -mlarge-data -O1 -I"..\..\lib\FreeRTOS" -I"..\..\lib" -I"..\..\lib\button" -I"..\..\lib\adc" -I".." -I"..\..\lib\i2c" -I"..\..\lib\bmp085" -I"..\..\lib\hmc5843" -I"..\..\lib\max7456" -I"..\..\lib\matrix" -I"..\..\lib\quaternion" -I"..\..\lib\pid" -I"..\..\lib\pwm_in" -I"..\..\lib\led" -I"..\..\lib\ppm_in" -I"..\..\lib\uart2" -I"..\..\lib\uart1_queue" -I"..\..\lib\servo" -I"..\..\lib\scp1000" -I"..\..\lib\microcontroller" -I"..\..\lib\gps" -I"..\..\lib\dataflash" -DMPLAB_DSPIC_PORT -msmart-io=1 -Wall -msfr-warn=off
	@${FIXDEPS} "${OBJECTDIR}/_ext/957545600/gps.o.d" $(SILENT)  -rsi ${MP_CC_DIR}../ 
	
${OBJECTDIR}/_ext/1967121974/hmc5843.o: ../../lib/hmc5843/hmc5843.c  nbproject/Makefile-${CND_CONF}.mk
	@${MKDIR} ${OBJECTDIR}/_ext/1967121974 
	@${RM} ${OBJECTDIR}/_ext/1967121974/hmc5843.o.d 
	${MP_CC} $(MP_EXTRA_CC_PRE)  ../../lib/hmc5843/hmc5843.c  -o ${OBJECTDIR}/_ext/1967121974/hmc5843.o  -c -mcpu=$(MP_PROCESSOR_OPTION)  -MMD -MF "${OBJECTDIR}/_ext/1967121974/hmc5843.o.d"        -g -omf=elf -mlarge-code -mlarge-data -O1 -I"..\..\lib\FreeRTOS" -I"..\..\lib" -I"..\..\lib\button" -I"..\..\lib\adc" -I".." -I"..\..\lib\i2c" -I"..\..\lib\bmp085" -I"..\..\lib\hmc5843" -I"..\..\lib\max7456" -I"..\..\lib\matrix" -I"..\..\lib\quaternion" -I"..\..\lib\pid" -I"..\..\lib\pwm_in" -I"..\..\lib\led" -I"..\..\lib\ppm_in" -I"..\..\lib\uart2" -I"..\..\lib\uart1_queue" -I"..\..\lib\servo" -I"..\..\lib\scp1000" -I"..\..\lib\microcontroller" -I"..\..\lib\gps" -I"..\..\lib\dataflash" -DMPLAB_DSPIC_PORT -msmart-io=1 -Wall -msfr-warn=off
	@${FIXDEPS} "${OBJECTDIR}/_ext/1967121974/hmc5843.o.d" $(SILENT)  -rsi ${MP_CC_DIR}../ 
	
${OBJECTDIR}/_ext/957545584/i2c.o: ../../lib/i2c/i2c.c  nbproject/Makefile-${CND_CONF}.mk
	@${MKDIR} ${OBJECTDIR}/_ext/957545584 
	@${RM} ${OBJECTDIR}/_ext/957545584/i2c.o.d 
	${MP_CC} $(MP_EXTRA_CC_PRE)  ../../lib/i2c/i2c.c  -o ${OBJECTDIR}/_ext/957545584/i2c.o  -c -mcpu=$(MP_PROCESSOR_OPTION)  -MMD -MF "${OBJECTDIR}/_ext/957545584/i2c.o.d"        -g -omf=elf -mlarge-code -mlarge-data -O1 -I"..\..\lib\FreeRTOS" -I"..\..\lib" -I"..\..\lib\button" -I"..\..\lib\adc" -I".." -I"..\..\lib\i2c" -I"..\..\lib\bmp085" -I"..\..\lib\hmc5843" -I"..\..\lib\max7456" -I"..\..\lib\matrix" -I"..\..\lib\quaternion" -I"..\..\lib\pid" -I"..\..\lib\pwm_in" -I"..\..\lib\led" -I"..\..\lib\ppm_in" -I"..\..\lib\uart2" -I"..\..\lib\uart1_queue" -I"..\..\lib\servo" -I"..\..\lib\scp1000" -I"..\..\lib\microcontroller" -I"..\..\lib\gps" -I"..\..\lib\dataflash" -DMPLAB_DSPIC_PORT -msmart-io=1 -Wall -msfr-warn=off
	@${FIXDEPS} "${OBJECTDIR}/_ext/957545584/i2c.o.d" $(SILENT)  -rsi ${MP_CC_DIR}../ 
	
${OBJECTDIR}/_ext/957550049/led.o: ../../lib/led/led.c  nbproject/Makefile-${CND_CONF}.mk
	@${MKDIR} ${OBJECTDIR}/_ext/957550049 
	@${RM} ${OBJECTDIR}/_ext/957550049/led.o.d 
	${MP_CC} $(MP_EXTRA_CC_PRE)  ../../lib/led/led.c  -o ${OBJECTDIR}/_ext/957550049/led.o  -c -mcpu=$(MP_PROCESSOR_OPTION)  -MMD -MF "${OBJECTDIR}/_ext/957550049/led.o.d"        -g -omf=elf -mlarge-code -mlarge-data -O1 -I"..\..\lib\FreeRTOS" -I"..\..\lib" -I"..\..\lib\button" -I"..\..\lib\adc" -I".." -I"..\..\lib\i2c" -I"..\..\lib\bmp085" -I"..\..\lib\hmc5843" -I"..\..\lib\max7456" -I"..\..\lib\matrix" -I"..\..\lib\quaternion" -I"..\..\lib\pid" -I"..\..\lib\pwm_in" -I"..\..\lib\led" -I"..\..\lib\ppm_in" -I"..\..\lib\uart2" -I"..\..\lib\uart1_queue" -I"..\..\lib\servo" -I"..\..\lib\scp1000" -I"..\..\lib\microcontroller" -I"..\..\lib\gps" -I"..\..\lib\dataflash" -DMPLAB_DSPIC_PORT -msmart-io=1 -Wall -msfr-warn=off
	@${FIXDEPS} "${OBJECTDIR}/_ext/957550049/led.o.d" $(SILENT)  -rsi ${MP_CC_DIR}../ 
	
${OBJECTDIR}/_ext/773745621/matrix.o: ../../lib/matrix/matrix.c  nbproject/Makefile-${CND_CONF}.mk
	@${MKDIR} ${OBJECTDIR}/_ext/773745621 
	@${RM} ${OBJECTDIR}/_ext/773745621/matrix.o.d 
	${MP_CC} $(MP_EXTRA_CC_PRE)  ../../lib/matrix/matrix.c  -o ${OBJECTDIR}/_ext/773745621/matrix.o  -c -mcpu=$(MP_PROCESSOR_OPTION)  -MMD -MF "${OBJECTDIR}/_ext/773745621/matrix.o.d"        -g -omf=elf -mlarge-code -mlarge-data -O1 -I"..\..\lib\FreeRTOS" -I"..\..\lib" -I"..\..\lib\button" -I"..\..\lib\adc" -I".." -I"..\..\lib\i2c" -I"..\..\lib\bmp085" -I"..\..\lib\hmc5843" -I"..\..\lib\max7456" -I"..\..\lib\matrix" -I"..\..\lib\quaternion" -I"..\..\lib\pid" -I"..\..\lib\pwm_in" -I"..\..\lib\led" -I"..\..\lib\ppm_in" -I"..\..\lib\uart2" -I"..\..\lib\uart1_queue" -I"..\..\lib\servo" -I"..\..\lib\scp1000" -I"..\..\lib\microcontroller" -I"..\..\lib\gps" -I"..\..\lib\dataflash" -DMPLAB_DSPIC_PORT -msmart-io=1 -Wall -msfr-warn=off
	@${FIXDEPS} "${OBJECTDIR}/_ext/773745621/matrix.o.d" $(SILENT)  -rsi ${MP_CC_DIR}../ 
	
${OBJECTDIR}/_ext/1785572984/max7456.o: ../../lib/max7456/max7456.c  nbproject/Makefile-${CND_CONF}.mk
	@${MKDIR} ${OBJECTDIR}/_ext/1785572984 
	@${RM} ${OBJECTDIR}/_ext/1785572984/max7456.o.d 
	${MP_CC} $(MP_EXTRA_CC_PRE)  ../../lib/max7456/max7456.c  -o ${OBJECTDIR}/_ext/1785572984/max7456.o  -c -mcpu=$(MP_PROCESSOR_OPTION)  -MMD -MF "${OBJECTDIR}/_ext/1785572984/max7456.o.d"        -g -omf=elf -mlarge-code -mlarge-data -O1 -I"..\..\lib\FreeRTOS" -I"..\..\lib" -I"..\..\lib\button" -I"..\..\lib\adc" -I".." -I"..\..\lib\i2c" -I"..\..\lib\bmp085" -I"..\..\lib\hmc5843" -I"..\..\lib\max7456" -I"..\..\lib\matrix" -I"..\..\lib\quaternion" -I"..\..\lib\pid" -I"..\..\lib\pwm_in" -I"..\..\lib\led" -I"..\..\lib\ppm_in" -I"..\..\lib\uart2" -I"..\..\lib\uart1_queue" -I"..\..\lib\servo" -I"..\..\lib\scp1000" -I"..\..\lib\microcontroller" -I"..\..\lib\gps" -I"..\..\lib\dataflash" -DMPLAB_DSPIC_PORT -msmart-io=1 -Wall -msfr-warn=off
	@${FIXDEPS} "${OBJECTDIR}/_ext/1785572984/max7456.o.d" $(SILENT)  -rsi ${MP_CC_DIR}../ 
	
${OBJECTDIR}/_ext/1843177418/microcontroller.o: ../../lib/microcontroller/microcontroller.c  nbproject/Makefile-${CND_CONF}.mk
	@${MKDIR} ${OBJECTDIR}/_ext/1843177418 
	@${RM} ${OBJECTDIR}/_ext/1843177418/microcontroller.o.d 
	${MP_CC} $(MP_EXTRA_CC_PRE)  ../../lib/microcontroller/microcontroller.c  -o ${OBJECTDIR}/_ext/1843177418/microcontroller.o  -c -mcpu=$(MP_PROCESSOR_OPTION)  -MMD -MF "${OBJECTDIR}/_ext/1843177418/microcontroller.o.d"        -g -omf=elf -mlarge-code -mlarge-data -O1 -I"..\..\lib\FreeRTOS" -I"..\..\lib" -I"..\..\lib\button" -I"..\..\lib\adc" -I".." -I"..\..\lib\i2c" -I"..\..\lib\bmp085" -I"..\..\lib\hmc5843" -I"..\..\lib\max7456" -I"..\..\lib\matrix" -I"..\..\lib\quaternion" -I"..\..\lib\pid" -I"..\..\lib\pwm_in" -I"..\..\lib\led" -I"..\..\lib\ppm_in" -I"..\..\lib\uart2" -I"..\..\lib\uart1_queue" -I"..\..\lib\servo" -I"..\..\lib\scp1000" -I"..\..\lib\microcontroller" -I"..\..\lib\gps" -I"..\..\lib\dataflash" -DMPLAB_DSPIC_PORT -msmart-io=1 -Wall -msfr-warn=off
	@${FIXDEPS} "${OBJECTDIR}/_ext/1843177418/microcontroller.o.d" $(SILENT)  -rsi ${MP_CC_DIR}../ 
	
${OBJECTDIR}/_ext/957554017/pid.o: ../../lib/pid/pid.c  nbproject/Makefile-${CND_CONF}.mk
	@${MKDIR} ${OBJECTDIR}/_ext/957554017 
	@${RM} ${OBJECTDIR}/_ext/957554017/pid.o.d 
	${MP_CC} $(MP_EXTRA_CC_PRE)  ../../lib/pid/pid.c  -o ${OBJECTDIR}/_ext/957554017/pid.o  -c -mcpu=$(MP_PROCESSOR_OPTION)  -MMD -MF "${OBJECTDIR}/_ext/957554017/pid.o.d"        -g -omf=elf -mlarge-code -mlarge-data -O1 -I"..\..\lib\FreeRTOS" -I"..\..\lib" -I"..\..\lib\button" -I"..\..\lib\adc" -I".." -I"..\..\lib\i2c" -I"..\..\lib\bmp085" -I"..\..\lib\hmc5843" -I"..\..\lib\max7456" -I"..\..\lib\matrix" -I"..\..\lib\quaternion" -I"..\..\lib\pid" -I"..\..\lib\pwm_in" -I"..\..\lib\led" -I"..\..\lib\ppm_in" -I"..\..\lib\uart2" -I"..\..\lib\uart1_queue" -I"..\..\lib\servo" -I"..\..\lib\scp1000" -I"..\..\lib\microcontroller" -I"..\..\lib\gps" -I"..\..\lib\dataflash" -DMPLAB_DSPIC_PORT -msmart-io=1 -Wall -msfr-warn=off
	@${FIXDEPS} "${OBJECTDIR}/_ext/957554017/pid.o.d" $(SILENT)  -rsi ${MP_CC_DIR}../ 
	
${OBJECTDIR}/_ext/674232159/ppm_in.o: ../../lib/ppm_in/ppm_in.c  nbproject/Makefile-${CND_CONF}.mk
	@${MKDIR} ${OBJECTDIR}/_ext/674232159 
	@${RM} ${OBJECTDIR}/_ext/674232159/ppm_in.o.d 
	${MP_CC} $(MP_EXTRA_CC_PRE)  ../../lib/ppm_in/ppm_in.c  -o ${OBJECTDIR}/_ext/674232159/ppm_in.o  -c -mcpu=$(MP_PROCESSOR_OPTION)  -MMD -MF "${OBJECTDIR}/_ext/674232159/ppm_in.o.d"        -g -omf=elf -mlarge-code -mlarge-data -O1 -I"..\..\lib\FreeRTOS" -I"..\..\lib" -I"..\..\lib\button" -I"..\..\lib\adc" -I".." -I"..\..\lib\i2c" -I"..\..\lib\bmp085" -I"..\..\lib\hmc5843" -I"..\..\lib\max7456" -I"..\..\lib\matrix" -I"..\..\lib\quaternion" -I"..\..\lib\pid" -I"..\..\lib\pwm_in" -I"..\..\lib\led" -I"..\..\lib\ppm_in" -I"..\..\lib\uart2" -I"..\..\lib\uart1_queue" -I"..\..\lib\servo" -I"..\..\lib\scp1000" -I"..\..\lib\microcontroller" -I"..\..\lib\gps" -I"..\..\lib\dataflash" -DMPLAB_DSPIC_PORT -msmart-io=1 -Wall -msfr-warn=off
	@${FIXDEPS} "${OBJECTDIR}/_ext/674232159/ppm_in.o.d" $(SILENT)  -rsi ${MP_CC_DIR}../ 
	
${OBJECTDIR}/_ext/667767512/pwm_in.o: ../../lib/pwm_in/pwm_in.c  nbproject/Makefile-${CND_CONF}.mk
	@${MKDIR} ${OBJECTDIR}/_ext/667767512 
	@${RM} ${OBJECTDIR}/_ext/667767512/pwm_in.o.d 
	${MP_CC} $(MP_EXTRA_CC_PRE)  ../../lib/pwm_in/pwm_in.c  -o ${OBJECTDIR}/_ext/667767512/pwm_in.o  -c -mcpu=$(MP_PROCESSOR_OPTION)  -MMD -MF "${OBJECTDIR}/_ext/667767512/pwm_in.o.d"        -g -omf=elf -mlarge-code -mlarge-data -O1 -I"..\..\lib\FreeRTOS" -I"..\..\lib" -I"..\..\lib\button" -I"..\..\lib\adc" -I".." -I"..\..\lib\i2c" -I"..\..\lib\bmp085" -I"..\..\lib\hmc5843" -I"..\..\lib\max7456" -I"..\..\lib\matrix" -I"..\..\lib\quaternion" -I"..\..\lib\pid" -I"..\..\lib\pwm_in" -I"..\..\lib\led" -I"..\..\lib\ppm_in" -I"..\..\lib\uart2" -I"..\..\lib\uart1_queue" -I"..\..\lib\servo" -I"..\..\lib\scp1000" -I"..\..\lib\microcontroller" -I"..\..\lib\gps" -I"..\..\lib\dataflash" -DMPLAB_DSPIC_PORT -msmart-io=1 -Wall -msfr-warn=off
	@${FIXDEPS} "${OBJECTDIR}/_ext/667767512/pwm_in.o.d" $(SILENT)  -rsi ${MP_CC_DIR}../ 
	
${OBJECTDIR}/_ext/888521352/quaternion.o: ../../lib/quaternion/quaternion.c  nbproject/Makefile-${CND_CONF}.mk
	@${MKDIR} ${OBJECTDIR}/_ext/888521352 
	@${RM} ${OBJECTDIR}/_ext/888521352/quaternion.o.d 
	${MP_CC} $(MP_EXTRA_CC_PRE)  ../../lib/quaternion/quaternion.c  -o ${OBJECTDIR}/_ext/888521352/quaternion.o  -c -mcpu=$(MP_PROCESSOR_OPTION)  -MMD -MF "${OBJECTDIR}/_ext/888521352/quaternion.o.d"        -g -omf=elf -mlarge-code -mlarge-data -O1 -I"..\..\lib\FreeRTOS" -I"..\..\lib" -I"..\..\lib\button" -I"..\..\lib\adc" -I".." -I"..\..\lib\i2c" -I"..\..\lib\bmp085" -I"..\..\lib\hmc5843" -I"..\..\lib\max7456" -I"..\..\lib\matrix" -I"..\..\lib\quaternion" -I"..\..\lib\pid" -I"..\..\lib\pwm_in" -I"..\..\lib\led" -I"..\..\lib\ppm_in" -I"..\..\lib\uart2" -I"..\..\lib\uart1_queue" -I"..\..\lib\servo" -I"..\..\lib\scp1000" -I"..\..\lib\microcontroller" -I"..\..\lib\gps" -I"..\..\lib\dataflash" -DMPLAB_DSPIC_PORT -msmart-io=1 -Wall -msfr-warn=off
	@${FIXDEPS} "${OBJECTDIR}/_ext/888521352/quaternion.o.d" $(SILENT)  -rsi ${MP_CC_DIR}../ 
	
${OBJECTDIR}/_ext/1429652139/scp1000.o: ../../lib/scp1000/scp1000.c  nbproject/Makefile-${CND_CONF}.mk
	@${MKDIR} ${OBJECTDIR}/_ext/1429652139 
	@${RM} ${OBJECTDIR}/_ext/1429652139/scp1000.o.d 
	${MP_CC} $(MP_EXTRA_CC_PRE)  ../../lib/scp1000/scp1000.c  -o ${OBJECTDIR}/_ext/1429652139/scp1000.o  -c -mcpu=$(MP_PROCESSOR_OPTION)  -MMD -MF "${OBJECTDIR}/_ext/1429652139/scp1000.o.d"        -g -omf=elf -mlarge-code -mlarge-data -O1 -I"..\..\lib\FreeRTOS" -I"..\..\lib" -I"..\..\lib\button" -I"..\..\lib\adc" -I".." -I"..\..\lib\i2c" -I"..\..\lib\bmp085" -I"..\..\lib\hmc5843" -I"..\..\lib\max7456" -I"..\..\lib\matrix" -I"..\..\lib\quaternion" -I"..\..\lib\pid" -I"..\..\lib\pwm_in" -I"..\..\lib\led" -I"..\..\lib\ppm_in" -I"..\..\lib\uart2" -I"..\..\lib\uart1_queue" -I"..\..\lib\servo" -I"..\..\lib\scp1000" -I"..\..\lib\microcontroller" -I"..\..\lib\gps" -I"..\..\lib\dataflash" -DMPLAB_DSPIC_PORT -msmart-io=1 -Wall -msfr-warn=off
	@${FIXDEPS} "${OBJECTDIR}/_ext/1429652139/scp1000.o.d" $(SILENT)  -rsi ${MP_CC_DIR}../ 
	
${OBJECTDIR}/_ext/1089077615/servo.o: ../../lib/servo/servo.c  nbproject/Makefile-${CND_CONF}.mk
	@${MKDIR} ${OBJECTDIR}/_ext/1089077615 
	@${RM} ${OBJECTDIR}/_ext/1089077615/servo.o.d 
	${MP_CC} $(MP_EXTRA_CC_PRE)  ../../lib/servo/servo.c  -o ${OBJECTDIR}/_ext/1089077615/servo.o  -c -mcpu=$(MP_PROCESSOR_OPTION)  -MMD -MF "${OBJECTDIR}/_ext/1089077615/servo.o.d"        -g -omf=elf -mlarge-code -mlarge-data -O1 -I"..\..\lib\FreeRTOS" -I"..\..\lib" -I"..\..\lib\button" -I"..\..\lib\adc" -I".." -I"..\..\lib\i2c" -I"..\..\lib\bmp085" -I"..\..\lib\hmc5843" -I"..\..\lib\max7456" -I"..\..\lib\matrix" -I"..\..\lib\quaternion" -I"..\..\lib\pid" -I"..\..\lib\pwm_in" -I"..\..\lib\led" -I"..\..\lib\ppm_in" -I"..\..\lib\uart2" -I"..\..\lib\uart1_queue" -I"..\..\lib\servo" -I"..\..\lib\scp1000" -I"..\..\lib\microcontroller" -I"..\..\lib\gps" -I"..\..\lib\dataflash" -DMPLAB_DSPIC_PORT -msmart-io=1 -Wall -msfr-warn=off
	@${FIXDEPS} "${OBJECTDIR}/_ext/1089077615/servo.o.d" $(SILENT)  -rsi ${MP_CC_DIR}../ 
	
${OBJECTDIR}/_ext/1591518261/uart1_queue.o: ../../lib/uart1_queue/uart1_queue.c  nbproject/Makefile-${CND_CONF}.mk
	@${MKDIR} ${OBJECTDIR}/_ext/1591518261 
	@${RM} ${OBJECTDIR}/_ext/1591518261/uart1_queue.o.d 
	${MP_CC} $(MP_EXTRA_CC_PRE)  ../../lib/uart1_queue/uart1_queue.c  -o ${OBJECTDIR}/_ext/1591518261/uart1_queue.o  -c -mcpu=$(MP_PROCESSOR_OPTION)  -MMD -MF "${OBJECTDIR}/_ext/1591518261/uart1_queue.o.d"        -g -omf=elf -mlarge-code -mlarge-data -O1 -I"..\..\lib\FreeRTOS" -I"..\..\lib" -I"..\..\lib\button" -I"..\..\lib\adc" -I".." -I"..\..\lib\i2c" -I"..\..\lib\bmp085" -I"..\..\lib\hmc5843" -I"..\..\lib\max7456" -I"..\..\lib\matrix" -I"..\..\lib\quaternion" -I"..\..\lib\pid" -I"..\..\lib\pwm_in" -I"..\..\lib\led" -I"..\..\lib\ppm_in" -I"..\..\lib\uart2" -I"..\..\lib\uart1_queue" -I"..\..\lib\servo" -I"..\..\lib\scp1000" -I"..\..\lib\microcontroller" -I"..\..\lib\gps" -I"..\..\lib\dataflash" -DMPLAB_DSPIC_PORT -msmart-io=1 -Wall -msfr-warn=off
	@${FIXDEPS} "${OBJECTDIR}/_ext/1591518261/uart1_queue.o.d" $(SILENT)  -rsi ${MP_CC_DIR}../ 
	
${OBJECTDIR}/_ext/1090805370/uart2.o: ../../lib/uart2/uart2.c  nbproject/Makefile-${CND_CONF}.mk
	@${MKDIR} ${OBJECTDIR}/_ext/1090805370 
	@${RM} ${OBJECTDIR}/_ext/1090805370/uart2.o.d 
	${MP_CC} $(MP_EXTRA_CC_PRE)  ../../lib/uart2/uart2.c  -o ${OBJECTDIR}/_ext/1090805370/uart2.o  -c -mcpu=$(MP_PROCESSOR_OPTION)  -MMD -MF "${OBJECTDIR}/_ext/1090805370/uart2.o.d"        -g -omf=elf -mlarge-code -mlarge-data -O1 -I"..\..\lib\FreeRTOS" -I"..\..\lib" -I"..\..\lib\button" -I"..\..\lib\adc" -I".." -I"..\..\lib\i2c" -I"..\..\lib\bmp085" -I"..\..\lib\hmc5843" -I"..\..\lib\max7456" -I"..\..\lib\matrix" -I"..\..\lib\quaternion" -I"..\..\lib\pid" -I"..\..\lib\pwm_in" -I"..\..\lib\led" -I"..\..\lib\ppm_in" -I"..\..\lib\uart2" -I"..\..\lib\uart1_queue" -I"..\..\lib\servo" -I"..\..\lib\scp1000" -I"..\..\lib\microcontroller" -I"..\..\lib\gps" -I"..\..\lib\dataflash" -DMPLAB_DSPIC_PORT -msmart-io=1 -Wall -msfr-warn=off
	@${FIXDEPS} "${OBJECTDIR}/_ext/1090805370/uart2.o.d" $(SILENT)  -rsi ${MP_CC_DIR}../ 
	
${OBJECTDIR}/_ext/2082761406/mpu6000.o: ../../lib/mpu6000/mpu6000.c  nbproject/Makefile-${CND_CONF}.mk
	@${MKDIR} ${OBJECTDIR}/_ext/2082761406 
	@${RM} ${OBJECTDIR}/_ext/2082761406/mpu6000.o.d 
	${MP_CC} $(MP_EXTRA_CC_PRE)  ../../lib/mpu6000/mpu6000.c  -o ${OBJECTDIR}/_ext/2082761406/mpu6000.o  -c -mcpu=$(MP_PROCESSOR_OPTION)  -MMD -MF "${OBJECTDIR}/_ext/2082761406/mpu6000.o.d"        -g -omf=elf -mlarge-code -mlarge-data -O1 -I"..\..\lib\FreeRTOS" -I"..\..\lib" -I"..\..\lib\button" -I"..\..\lib\adc" -I".." -I"..\..\lib\i2c" -I"..\..\lib\bmp085" -I"..\..\lib\hmc5843" -I"..\..\lib\max7456" -I"..\..\lib\matrix" -I"..\..\lib\quaternion" -I"..\..\lib\pid" -I"..\..\lib\pwm_in" -I"..\..\lib\led" -I"..\..\lib\ppm_in" -I"..\..\lib\uart2" -I"..\..\lib\uart1_queue" -I"..\..\lib\servo" -I"..\..\lib\scp1000" -I"..\..\lib\microcontroller" -I"..\..\lib\gps" -I"..\..\lib\dataflash" -DMPLAB_DSPIC_PORT -msmart-io=1 -Wall -msfr-warn=off
	@${FIXDEPS} "${OBJECTDIR}/_ext/2082761406/mpu6000.o.d" $(SILENT)  -rsi ${MP_CC_DIR}../ 
	
${OBJECTDIR}/_ext/1472/communication_csv.o: ../communication_csv.c  nbproject/Makefile-${CND_CONF}.mk
	@${MKDIR} ${OBJECTDIR}/_ext/1472 
	@${RM} ${OBJECTDIR}/_ext/1472/communication_csv.o.d 
	${MP_CC} $(MP_EXTRA_CC_PRE)  ../communication_csv.c  -o ${OBJECTDIR}/_ext/1472/communication_csv.o  -c -mcpu=$(MP_PROCESSOR_OPTION)  -MMD -MF "${OBJECTDIR}/_ext/1472/communication_csv.o.d"        -g -omf=elf -mlarge-code -mlarge-data -O1 -I"..\..\lib\FreeRTOS" -I"..\..\lib" -I"..\..\lib\button" -I"..\..\lib\adc" -I".." -I"..\..\lib\i2c" -I"..\..\lib\bmp085" -I"..\..\lib\hmc5843" -I"..\..\lib\max7456" -I"..\..\lib\matrix" -I"..\..\lib\quaternion" -I"..\..\lib\pid" -I"..\..\lib\pwm_in" -I"..\..\lib\led" -I"..\..\lib\ppm_in" -I"..\..\lib\uart2" -I"..\..\lib\uart1_queue" -I"..\..\lib\servo" -I"..\..\lib\scp1000" -I"..\..\lib\microcontroller" -I"..\..\lib\gps" -I"..\..\lib\dataflash" -DMPLAB_DSPIC_PORT -msmart-io=1 -Wall -msfr-warn=off
	@${FIXDEPS} "${OBJECTDIR}/_ext/1472/communication_csv.o.d" $(SILENT)  -rsi ${MP_CC_DIR}../ 
	
${OBJECTDIR}/_ext/1472/configuration.o: ../configuration.c  nbproject/Makefile-${CND_CONF}.mk
	@${MKDIR} ${OBJECTDIR}/_ext/1472 
	@${RM} ${OBJECTDIR}/_ext/1472/configuration.o.d 
	${MP_CC} $(MP_EXTRA_CC_PRE)  ../configuration.c  -o ${OBJECTDIR}/_ext/1472/configuration.o  -c -mcpu=$(MP_PROCESSOR_OPTION)  -MMD -MF "${OBJECTDIR}/_ext/1472/configuration.o.d"        -g -omf=elf -mlarge-code -mlarge-data -O1 -I"..\..\lib\FreeRTOS" -I"..\..\lib" -I"..\..\lib\button" -I"..\..\lib\adc" -I".." -I"..\..\lib\i2c" -I"..\..\lib\bmp085" -I"..\..\lib\hmc5843" -I"..\..\lib\max7456" -I"..\..\lib\matrix" -I"..\..\lib\quaternion" -I"..\..\lib\pid" -I"..\..\lib\pwm_in" -I"..\..\lib\led" -I"..\..\lib\ppm_in" -I"..\..\lib\uart2" -I"..\..\lib\uart1_queue" -I"..\..\lib\servo" -I"..\..\lib\scp1000" -I"..\..\lib\microcontroller" -I"..\..\lib\gps" -I"..\..\lib\dataflash" -DMPLAB_DSPIC_PORT -msmart-io=1 -Wall -msfr-warn=off
	@${FIXDEPS} "${OBJECTDIR}/_ext/1472/configuration.o.d" $(SILENT)  -rsi ${MP_CC_DIR}../ 
	
${OBJECTDIR}/_ext/1472/gluonscript.o: ../gluonscript.c  nbproject/Makefile-${CND_CONF}.mk
	@${MKDIR} ${OBJECTDIR}/_ext/1472 
	@${RM} ${OBJECTDIR}/_ext/1472/gluonscript.o.d 
	${MP_CC} $(MP_EXTRA_CC_PRE)  ../gluonscript.c  -o ${OBJECTDIR}/_ext/1472/gluonscript.o  -c -mcpu=$(MP_PROCESSOR_OPTION)  -MMD -MF "${OBJECTDIR}/_ext/1472/gluonscript.o.d"        -g -omf=elf -mlarge-code -mlarge-data -O1 -I"..\..\lib\FreeRTOS" -I"..\..\lib" -I"..\..\lib\button" -I"..\..\lib\adc" -I".." -I"..\..\lib\i2c" -I"..\..\lib\bmp085" -I"..\..\lib\hmc5843" -I"..\..\lib\max7456" -I"..\..\lib\matrix" -I"..\..\lib\quaternion" -I"..\..\lib\pid" -I"..\..\lib\pwm_in" -I"..\..\lib\led" -I"..\..\lib\ppm_in" -I"..\..\lib\uart2" -I"..\..\lib\uart1_queue" -I"..\..\lib\servo" -I"..\..\lib\scp1000" -I"..\..\lib\microcontroller" -I"..\..\lib\gps" -I"..\..\lib\dataflash" -DMPLAB_DSPIC_PORT -msmart-io=1 -Wall -msfr-warn=off
	@${FIXDEPS} "${OBJECTDIR}/_ext/1472/gluonscript.o.d" $(SILENT)  -rsi ${MP_CC_DIR}../ 
	
${OBJECTDIR}/_ext/1472/osd_max7456.o: ../osd_max7456.c  nbproject/Makefile-${CND_CONF}.mk
	@${MKDIR} ${OBJECTDIR}/_ext/1472 
	@${RM} ${OBJECTDIR}/_ext/1472/osd_max7456.o.d 
	${MP_CC} $(MP_EXTRA_CC_PRE)  ../osd_max7456.c  -o ${OBJECTDIR}/_ext/1472/osd_max7456.o  -c -mcpu=$(MP_PROCESSOR_OPTION)  -MMD -MF "${OBJECTDIR}/_ext/1472/osd_max7456.o.d"        -g -omf=elf -mlarge-code -mlarge-data -O1 -I"..\..\lib\FreeRTOS" -I"..\..\lib" -I"..\..\lib\button" -I"..\..\lib\adc" -I".." -I"..\..\lib\i2c" -I"..\..\lib\bmp085" -I"..\..\lib\hmc5843" -I"..\..\lib\max7456" -I"..\..\lib\matrix" -I"..\..\lib\quaternion" -I"..\..\lib\pid" -I"..\..\lib\pwm_in" -I"..\..\lib\led" -I"..\..\lib\ppm_in" -I"..\..\lib\uart2" -I"..\..\lib\uart1_queue" -I"..\..\lib\servo" -I"..\..\lib\scp1000" -I"..\..\lib\microcontroller" -I"..\..\lib\gps" -I"..\..\lib\dataflash" -DMPLAB_DSPIC_PORT -msmart-io=1 -Wall -msfr-warn=off
	@${FIXDEPS} "${OBJECTDIR}/_ext/1472/osd_max7456.o.d" $(SILENT)  -rsi ${MP_CC_DIR}../ 
	
${OBJECTDIR}/_ext/1472/rtos_pilot.o: ../rtos_pilot.c  nbproject/Makefile-${CND_CONF}.mk
	@${MKDIR} ${OBJECTDIR}/_ext/1472 
	@${RM} ${OBJECTDIR}/_ext/1472/rtos_pilot.o.d 
	${MP_CC} $(MP_EXTRA_CC_PRE)  ../rtos_pilot.c  -o ${OBJECTDIR}/_ext/1472/rtos_pilot.o  -c -mcpu=$(MP_PROCESSOR_OPTION)  -MMD -MF "${OBJECTDIR}/_ext/1472/rtos_pilot.o.d"        -g -omf=elf -mlarge-code -mlarge-data -O1 -I"..\..\lib\FreeRTOS" -I"..\..\lib" -I"..\..\lib\button" -I"..\..\lib\adc" -I".." -I"..\..\lib\i2c" -I"..\..\lib\bmp085" -I"..\..\lib\hmc5843" -I"..\..\lib\max7456" -I"..\..\lib\matrix" -I"..\..\lib\quaternion" -I"..\..\lib\pid" -I"..\..\lib\pwm_in" -I"..\..\lib\led" -I"..\..\lib\ppm_in" -I"..\..\lib\uart2" -I"..\..\lib\uart1_queue" -I"..\..\lib\servo" -I"..\..\lib\scp1000" -I"..\..\lib\microcontroller" -I"..\..\lib\gps" -I"..\..\lib\dataflash" -DMPLAB_DSPIC_PORT -msmart-io=1 -Wall -msfr-warn=off
	@${FIXDEPS} "${OBJECTDIR}/_ext/1472/rtos_pilot.o.d" $(SILENT)  -rsi ${MP_CC_DIR}../ 
	
${OBJECTDIR}/_ext/1472/ahrs_kalman_2x3.o: ../ahrs_kalman_2x3.c  nbproject/Makefile-${CND_CONF}.mk
	@${MKDIR} ${OBJECTDIR}/_ext/1472 
	@${RM} ${OBJECTDIR}/_ext/1472/ahrs_kalman_2x3.o.d 
	${MP_CC} $(MP_EXTRA_CC_PRE)  ../ahrs_kalman_2x3.c  -o ${OBJECTDIR}/_ext/1472/ahrs_kalman_2x3.o  -c -mcpu=$(MP_PROCESSOR_OPTION)  -MMD -MF "${OBJECTDIR}/_ext/1472/ahrs_kalman_2x3.o.d"        -g -omf=elf -mlarge-code -mlarge-data -O1 -I"..\..\lib\FreeRTOS" -I"..\..\lib" -I"..\..\lib\button" -I"..\..\lib\adc" -I".." -I"..\..\lib\i2c" -I"..\..\lib\bmp085" -I"..\..\lib\hmc5843" -I"..\..\lib\max7456" -I"..\..\lib\matrix" -I"..\..\lib\quaternion" -I"..\..\lib\pid" -I"..\..\lib\pwm_in" -I"..\..\lib\led" -I"..\..\lib\ppm_in" -I"..\..\lib\uart2" -I"..\..\lib\uart1_queue" -I"..\..\lib\servo" -I"..\..\lib\scp1000" -I"..\..\lib\microcontroller" -I"..\..\lib\gps" -I"..\..\lib\dataflash" -DMPLAB_DSPIC_PORT -msmart-io=1 -Wall -msfr-warn=off
	@${FIXDEPS} "${OBJECTDIR}/_ext/1472/ahrs_kalman_2x3.o.d" $(SILENT)  -rsi ${MP_CC_DIR}../ 
	
${OBJECTDIR}/_ext/1472/handler_alarms.o: ../handler_alarms.c  nbproject/Makefile-${CND_CONF}.mk
	@${MKDIR} ${OBJECTDIR}/_ext/1472 
	@${RM} ${OBJECTDIR}/_ext/1472/handler_alarms.o.d 
	${MP_CC} $(MP_EXTRA_CC_PRE)  ../handler_alarms.c  -o ${OBJECTDIR}/_ext/1472/handler_alarms.o  -c -mcpu=$(MP_PROCESSOR_OPTION)  -MMD -MF "${OBJECTDIR}/_ext/1472/handler_alarms.o.d"        -g -omf=elf -mlarge-code -mlarge-data -O1 -I"..\..\lib\FreeRTOS" -I"..\..\lib" -I"..\..\lib\button" -I"..\..\lib\adc" -I".." -I"..\..\lib\i2c" -I"..\..\lib\bmp085" -I"..\..\lib\hmc5843" -I"..\..\lib\max7456" -I"..\..\lib\matrix" -I"..\..\lib\quaternion" -I"..\..\lib\pid" -I"..\..\lib\pwm_in" -I"..\..\lib\led" -I"..\..\lib\ppm_in" -I"..\..\lib\uart2" -I"..\..\lib\uart1_queue" -I"..\..\lib\servo" -I"..\..\lib\scp1000" -I"..\..\lib\microcontroller" -I"..\..\lib\gps" -I"..\..\lib\dataflash" -DMPLAB_DSPIC_PORT -msmart-io=1 -Wall -msfr-warn=off
	@${FIXDEPS} "${OBJECTDIR}/_ext/1472/handler_alarms.o.d" $(SILENT)  -rsi ${MP_CC_DIR}../ 
	
${OBJECTDIR}/_ext/1472/handler_trigger.o: ../handler_trigger.c  nbproject/Makefile-${CND_CONF}.mk
	@${MKDIR} ${OBJECTDIR}/_ext/1472 
	@${RM} ${OBJECTDIR}/_ext/1472/handler_trigger.o.d 
	${MP_CC} $(MP_EXTRA_CC_PRE)  ../handler_trigger.c  -o ${OBJECTDIR}/_ext/1472/handler_trigger.o  -c -mcpu=$(MP_PROCESSOR_OPTION)  -MMD -MF "${OBJECTDIR}/_ext/1472/handler_trigger.o.d"        -g -omf=elf -mlarge-code -mlarge-data -O1 -I"..\..\lib\FreeRTOS" -I"..\..\lib" -I"..\..\lib\button" -I"..\..\lib\adc" -I".." -I"..\..\lib\i2c" -I"..\..\lib\bmp085" -I"..\..\lib\hmc5843" -I"..\..\lib\max7456" -I"..\..\lib\matrix" -I"..\..\lib\quaternion" -I"..\..\lib\pid" -I"..\..\lib\pwm_in" -I"..\..\lib\led" -I"..\..\lib\ppm_in" -I"..\..\lib\uart2" -I"..\..\lib\uart1_queue" -I"..\..\lib\servo" -I"..\..\lib\scp1000" -I"..\..\lib\microcontroller" -I"..\..\lib\gps" -I"..\..\lib\dataflash" -DMPLAB_DSPIC_PORT -msmart-io=1 -Wall -msfr-warn=off
	@${FIXDEPS} "${OBJECTDIR}/_ext/1472/handler_trigger.o.d" $(SILENT)  -rsi ${MP_CC_DIR}../ 
	
${OBJECTDIR}/_ext/1472/handler_navigation.o: ../handler_navigation.c  nbproject/Makefile-${CND_CONF}.mk
	@${MKDIR} ${OBJECTDIR}/_ext/1472 
	@${RM} ${OBJECTDIR}/_ext/1472/handler_navigation.o.d 
	${MP_CC} $(MP_EXTRA_CC_PRE)  ../handler_navigation.c  -o ${OBJECTDIR}/_ext/1472/handler_navigation.o  -c -mcpu=$(MP_PROCESSOR_OPTION)  -MMD -MF "${OBJECTDIR}/_ext/1472/handler_navigation.o.d"        -g -omf=elf -mlarge-code -mlarge-data -O1 -I"..\..\lib\FreeRTOS" -I"..\..\lib" -I"..\..\lib\button" -I"..\..\lib\adc" -I".." -I"..\..\lib\i2c" -I"..\..\lib\bmp085" -I"..\..\lib\hmc5843" -I"..\..\lib\max7456" -I"..\..\lib\matrix" -I"..\..\lib\quaternion" -I"..\..\lib\pid" -I"..\..\lib\pwm_in" -I"..\..\lib\led" -I"..\..\lib\ppm_in" -I"..\..\lib\uart2" -I"..\..\lib\uart1_queue" -I"..\..\lib\servo" -I"..\..\lib\scp1000" -I"..\..\lib\microcontroller" -I"..\..\lib\gps" -I"..\..\lib\dataflash" -DMPLAB_DSPIC_PORT -msmart-io=1 -Wall -msfr-warn=off
	@${FIXDEPS} "${OBJECTDIR}/_ext/1472/handler_navigation.o.d" $(SILENT)  -rsi ${MP_CC_DIR}../ 
	
${OBJECTDIR}/_ext/1472/handler_flightplan_switch.o: ../handler_flightplan_switch.c  nbproject/Makefile-${CND_CONF}.mk
	@${MKDIR} ${OBJECTDIR}/_ext/1472 
	@${RM} ${OBJECTDIR}/_ext/1472/handler_flightplan_switch.o.d 
	${MP_CC} $(MP_EXTRA_CC_PRE)  ../handler_flightplan_switch.c  -o ${OBJECTDIR}/_ext/1472/handler_flightplan_switch.o  -c -mcpu=$(MP_PROCESSOR_OPTION)  -MMD -MF "${OBJECTDIR}/_ext/1472/handler_flightplan_switch.o.d"        -g -omf=elf -mlarge-code -mlarge-data -O1 -I"..\..\lib\FreeRTOS" -I"..\..\lib" -I"..\..\lib\button" -I"..\..\lib\adc" -I".." -I"..\..\lib\i2c" -I"..\..\lib\bmp085" -I"..\..\lib\hmc5843" -I"..\..\lib\max7456" -I"..\..\lib\matrix" -I"..\..\lib\quaternion" -I"..\..\lib\pid" -I"..\..\lib\pwm_in" -I"..\..\lib\led" -I"..\..\lib\ppm_in" -I"..\..\lib\uart2" -I"..\..\lib\uart1_queue" -I"..\..\lib\servo" -I"..\..\lib\scp1000" -I"..\..\lib\microcontroller" -I"..\..\lib\gps" -I"..\..\lib\dataflash" -DMPLAB_DSPIC_PORT -msmart-io=1 -Wall -msfr-warn=off
	@${FIXDEPS} "${OBJECTDIR}/_ext/1472/handler_flightplan_switch.o.d" $(SILENT)  -rsi ${MP_CC_DIR}../ 
	
${OBJECTDIR}/_ext/1472/task_gps.o: ../task_gps.c  nbproject/Makefile-${CND_CONF}.mk
	@${MKDIR} ${OBJECTDIR}/_ext/1472 
	@${RM} ${OBJECTDIR}/_ext/1472/task_gps.o.d 
	${MP_CC} $(MP_EXTRA_CC_PRE)  ../task_gps.c  -o ${OBJECTDIR}/_ext/1472/task_gps.o  -c -mcpu=$(MP_PROCESSOR_OPTION)  -MMD -MF "${OBJECTDIR}/_ext/1472/task_gps.o.d"        -g -omf=elf -mlarge-code -mlarge-data -O1 -I"..\..\lib\FreeRTOS" -I"..\..\lib" -I"..\..\lib\button" -I"..\..\lib\adc" -I".." -I"..\..\lib\i2c" -I"..\..\lib\bmp085" -I"..\..\lib\hmc5843" -I"..\..\lib\max7456" -I"..\..\lib\matrix" -I"..\..\lib\quaternion" -I"..\..\lib\pid" -I"..\..\lib\pwm_in" -I"..\..\lib\led" -I"..\..\lib\ppm_in" -I"..\..\lib\uart2" -I"..\..\lib\uart1_queue" -I"..\..\lib\servo" -I"..\..\lib\scp1000" -I"..\..\lib\microcontroller" -I"..\..\lib\gps" -I"..\..\lib\dataflash" -DMPLAB_DSPIC_PORT -msmart-io=1 -Wall -msfr-warn=off
	@${FIXDEPS} "${OBJECTDIR}/_ext/1472/task_gps.o.d" $(SILENT)  -rsi ${MP_CC_DIR}../ 
	
${OBJECTDIR}/_ext/1472/task_datalogger.o: ../task_datalogger.c  nbproject/Makefile-${CND_CONF}.mk
	@${MKDIR} ${OBJECTDIR}/_ext/1472 
	@${RM} ${OBJECTDIR}/_ext/1472/task_datalogger.o.d 
	${MP_CC} $(MP_EXTRA_CC_PRE)  ../task_datalogger.c  -o ${OBJECTDIR}/_ext/1472/task_datalogger.o  -c -mcpu=$(MP_PROCESSOR_OPTION)  -MMD -MF "${OBJECTDIR}/_ext/1472/task_datalogger.o.d"        -g -omf=elf -mlarge-code -mlarge-data -O1 -I"..\..\lib\FreeRTOS" -I"..\..\lib" -I"..\..\lib\button" -I"..\..\lib\adc" -I".." -I"..\..\lib\i2c" -I"..\..\lib\bmp085" -I"..\..\lib\hmc5843" -I"..\..\lib\max7456" -I"..\..\lib\matrix" -I"..\..\lib\quaternion" -I"..\..\lib\pid" -I"..\..\lib\pwm_in" -I"..\..\lib\led" -I"..\..\lib\ppm_in" -I"..\..\lib\uart2" -I"..\..\lib\uart1_queue" -I"..\..\lib\servo" -I"..\..\lib\scp1000" -I"..\..\lib\microcontroller" -I"..\..\lib\gps" -I"..\..\lib\dataflash" -DMPLAB_DSPIC_PORT -msmart-io=1 -Wall -msfr-warn=off
	@${FIXDEPS} "${OBJECTDIR}/_ext/1472/task_datalogger.o.d" $(SILENT)  -rsi ${MP_CC_DIR}../ 
	
${OBJECTDIR}/_ext/1472/task_control.o: ../task_control.c  nbproject/Makefile-${CND_CONF}.mk
	@${MKDIR} ${OBJECTDIR}/_ext/1472 
	@${RM} ${OBJECTDIR}/_ext/1472/task_control.o.d 
	${MP_CC} $(MP_EXTRA_CC_PRE)  ../task_control.c  -o ${OBJECTDIR}/_ext/1472/task_control.o  -c -mcpu=$(MP_PROCESSOR_OPTION)  -MMD -MF "${OBJECTDIR}/_ext/1472/task_control.o.d"        -g -omf=elf -mlarge-code -mlarge-data -O1 -I"..\..\lib\FreeRTOS" -I"..\..\lib" -I"..\..\lib\button" -I"..\..\lib\adc" -I".." -I"..\..\lib\i2c" -I"..\..\lib\bmp085" -I"..\..\lib\hmc5843" -I"..\..\lib\max7456" -I"..\..\lib\matrix" -I"..\..\lib\quaternion" -I"..\..\lib\pid" -I"..\..\lib\pwm_in" -I"..\..\lib\led" -I"..\..\lib\ppm_in" -I"..\..\lib\uart2" -I"..\..\lib\uart1_queue" -I"..\..\lib\servo" -I"..\..\lib\scp1000" -I"..\..\lib\microcontroller" -I"..\..\lib\gps" -I"..\..\lib\dataflash" -DMPLAB_DSPIC_PORT -msmart-io=1 -Wall -msfr-warn=off
	@${FIXDEPS} "${OBJECTDIR}/_ext/1472/task_control.o.d" $(SILENT)  -rsi ${MP_CC_DIR}../ 
	
${OBJECTDIR}/_ext/1472/task_sensors_analog.o: ../task_sensors_analog.c  nbproject/Makefile-${CND_CONF}.mk
	@${MKDIR} ${OBJECTDIR}/_ext/1472 
	@${RM} ${OBJECTDIR}/_ext/1472/task_sensors_analog.o.d 
	${MP_CC} $(MP_EXTRA_CC_PRE)  ../task_sensors_analog.c  -o ${OBJECTDIR}/_ext/1472/task_sensors_analog.o  -c -mcpu=$(MP_PROCESSOR_OPTION)  -MMD -MF "${OBJECTDIR}/_ext/1472/task_sensors_analog.o.d"        -g -omf=elf -mlarge-code -mlarge-data -O1 -I"..\..\lib\FreeRTOS" -I"..\..\lib" -I"..\..\lib\button" -I"..\..\lib\adc" -I".." -I"..\..\lib\i2c" -I"..\..\lib\bmp085" -I"..\..\lib\hmc5843" -I"..\..\lib\max7456" -I"..\..\lib\matrix" -I"..\..\lib\quaternion" -I"..\..\lib\pid" -I"..\..\lib\pwm_in" -I"..\..\lib\led" -I"..\..\lib\ppm_in" -I"..\..\lib\uart2" -I"..\..\lib\uart1_queue" -I"..\..\lib\servo" -I"..\..\lib\scp1000" -I"..\..\lib\microcontroller" -I"..\..\lib\gps" -I"..\..\lib\dataflash" -DMPLAB_DSPIC_PORT -msmart-io=1 -Wall -msfr-warn=off
	@${FIXDEPS} "${OBJECTDIR}/_ext/1472/task_sensors_analog.o.d" $(SILENT)  -rsi ${MP_CC_DIR}../ 
	
${OBJECTDIR}/_ext/1472/sensors.o: ../sensors.c  nbproject/Makefile-${CND_CONF}.mk
	@${MKDIR} ${OBJECTDIR}/_ext/1472 
	@${RM} ${OBJECTDIR}/_ext/1472/sensors.o.d 
	${MP_CC} $(MP_EXTRA_CC_PRE)  ../sensors.c  -o ${OBJECTDIR}/_ext/1472/sensors.o  -c -mcpu=$(MP_PROCESSOR_OPTION)  -MMD -MF "${OBJECTDIR}/_ext/1472/sensors.o.d"        -g -omf=elf -mlarge-code -mlarge-data -O1 -I"..\..\lib\FreeRTOS" -I"..\..\lib" -I"..\..\lib\button" -I"..\..\lib\adc" -I".." -I"..\..\lib\i2c" -I"..\..\lib\bmp085" -I"..\..\lib\hmc5843" -I"..\..\lib\max7456" -I"..\..\lib\matrix" -I"..\..\lib\quaternion" -I"..\..\lib\pid" -I"..\..\lib\pwm_in" -I"..\..\lib\led" -I"..\..\lib\ppm_in" -I"..\..\lib\uart2" -I"..\..\lib\uart1_queue" -I"..\..\lib\servo" -I"..\..\lib\scp1000" -I"..\..\lib\microcontroller" -I"..\..\lib\gps" -I"..\..\lib\dataflash" -DMPLAB_DSPIC_PORT -msmart-io=1 -Wall -msfr-warn=off
	@${FIXDEPS} "${OBJECTDIR}/_ext/1472/sensors.o.d" $(SILENT)  -rsi ${MP_CC_DIR}../ 
	
${OBJECTDIR}/_ext/1472/task_sensors_mpu6000.o: ../task_sensors_mpu6000.c  nbproject/Makefile-${CND_CONF}.mk
	@${MKDIR} ${OBJECTDIR}/_ext/1472 
	@${RM} ${OBJECTDIR}/_ext/1472/task_sensors_mpu6000.o.d 
	${MP_CC} $(MP_EXTRA_CC_PRE)  ../task_sensors_mpu6000.c  -o ${OBJECTDIR}/_ext/1472/task_sensors_mpu6000.o  -c -mcpu=$(MP_PROCESSOR_OPTION)  -MMD -MF "${OBJECTDIR}/_ext/1472/task_sensors_mpu6000.o.d"        -g -omf=elf -mlarge-code -mlarge-data -O1 -I"..\..\lib\FreeRTOS" -I"..\..\lib" -I"..\..\lib\button" -I"..\..\lib\adc" -I".." -I"..\..\lib\i2c" -I"..\..\lib\bmp085" -I"..\..\lib\hmc5843" -I"..\..\lib\max7456" -I"..\..\lib\matrix" -I"..\..\lib\quaternion" -I"..\..\lib\pid" -I"..\..\lib\pwm_in" -I"..\..\lib\led" -I"..\..\lib\ppm_in" -I"..\..\lib\uart2" -I"..\..\lib\uart1_queue" -I"..\..\lib\servo" -I"..\..\lib\scp1000" -I"..\..\lib\microcontroller" -I"..\..\lib\gps" -I"..\..\lib\dataflash" -DMPLAB_DSPIC_PORT -msmart-io=1 -Wall -msfr-warn=off
	@${FIXDEPS} "${OBJECTDIR}/_ext/1472/task_sensors_mpu6000.o.d" $(SILENT)  -rsi ${MP_CC_DIR}../ 
	
endif

# ------------------------------------------------------------------------------------
# Rules for buildStep: assemble
ifeq ($(TYPE_IMAGE), DEBUG_RUN)
else
endif

# ------------------------------------------------------------------------------------
# Rules for buildStep: assemblePreproc
ifeq ($(TYPE_IMAGE), DEBUG_RUN)
${OBJECTDIR}/_ext/1970174492/portasm_dsPIC.o: ../../lib/FreeRTOS/portasm_dsPIC.S  nbproject/Makefile-${CND_CONF}.mk
	@${MKDIR} ${OBJECTDIR}/_ext/1970174492 
	@${RM} ${OBJECTDIR}/_ext/1970174492/portasm_dsPIC.o.d 
	${MP_CC} $(MP_EXTRA_AS_PRE)  ../../lib/FreeRTOS/portasm_dsPIC.S  -o ${OBJECTDIR}/_ext/1970174492/portasm_dsPIC.o  -c -mcpu=$(MP_PROCESSOR_OPTION)  -MMD -MF "${OBJECTDIR}/_ext/1970174492/portasm_dsPIC.o.d"  -D__DEBUG -D__MPLAB_DEBUGGER_ICD3=1  -omf=elf -I"..\..\lib\FreeRTOS" -I"..\..\lib" -I"..\..\lib\button" -I"..\..\lib\adc" -I".." -I"..\..\lib\i2c" -I"..\..\lib\bmp085" -I"..\..\lib\hmc5843" -I"..\..\lib\max7456" -I"..\..\lib\matrix" -I"..\..\lib\quaternion" -I"..\..\lib\pid" -I"..\..\lib\pwm_in" -I"..\..\lib\led" -I"..\..\lib\ppm_in" -I"..\..\lib\uart2" -I"..\..\lib\uart1_queue" -I"..\..\lib\servo" -I"..\..\lib\scp1000" -I"..\..\lib\microcontroller" -I"..\..\lib\gps" -I"..\..\lib\dataflash" -I".." -Wa,-MD,"${OBJECTDIR}/_ext/1970174492/portasm_dsPIC.o.asm.d",--defsym=__MPLAB_BUILD=1,--defsym=__MPLAB_DEBUG=1,--defsym=__ICD2RAM=1,--defsym=__DEBUG=1,--defsym=__MPLAB_DEBUGGER_ICD3=1,-g,--no-relax$(MP_EXTRA_AS_POST)
	@${FIXDEPS} "${OBJECTDIR}/_ext/1970174492/portasm_dsPIC.o.d" "${OBJECTDIR}/_ext/1970174492/portasm_dsPIC.o.asm.d"  -t $(SILENT)  -rsi ${MP_CC_DIR}../  
	
else
${OBJECTDIR}/_ext/1970174492/portasm_dsPIC.o: ../../lib/FreeRTOS/portasm_dsPIC.S  nbproject/Makefile-${CND_CONF}.mk
	@${MKDIR} ${OBJECTDIR}/_ext/1970174492 
	@${RM} ${OBJECTDIR}/_ext/1970174492/portasm_dsPIC.o.d 
	${MP_CC} $(MP_EXTRA_AS_PRE)  ../../lib/FreeRTOS/portasm_dsPIC.S  -o ${OBJECTDIR}/_ext/1970174492/portasm_dsPIC.o  -c -mcpu=$(MP_PROCESSOR_OPTION)  -MMD -MF "${OBJECTDIR}/_ext/1970174492/portasm_dsPIC.o.d"  -omf=elf -I"..\..\lib\FreeRTOS" -I"..\..\lib" -I"..\..\lib\button" -I"..\..\lib\adc" -I".." -I"..\..\lib\i2c" -I"..\..\lib\bmp085" -I"..\..\lib\hmc5843" -I"..\..\lib\max7456" -I"..\..\lib\matrix" -I"..\..\lib\quaternion" -I"..\..\lib\pid" -I"..\..\lib\pwm_in" -I"..\..\lib\led" -I"..\..\lib\ppm_in" -I"..\..\lib\uart2" -I"..\..\lib\uart1_queue" -I"..\..\lib\servo" -I"..\..\lib\scp1000" -I"..\..\lib\microcontroller" -I"..\..\lib\gps" -I"..\..\lib\dataflash" -I".." -Wa,-MD,"${OBJECTDIR}/_ext/1970174492/portasm_dsPIC.o.asm.d",--defsym=__MPLAB_BUILD=1,-g,--no-relax$(MP_EXTRA_AS_POST)
	@${FIXDEPS} "${OBJECTDIR}/_ext/1970174492/portasm_dsPIC.o.d" "${OBJECTDIR}/_ext/1970174492/portasm_dsPIC.o.asm.d"  -t $(SILENT)  -rsi ${MP_CC_DIR}../  
	
endif

# ------------------------------------------------------------------------------------
# Rules for buildStep: link
ifeq ($(TYPE_IMAGE), DEBUG_RUN)
dist/${CND_CONF}/${IMAGE_TYPE}/rtos_pilot.X.${IMAGE_TYPE}.${OUTPUT_SUFFIX}: ${OBJECTFILES}  nbproject/Makefile-${CND_CONF}.mk   
	@${MKDIR} dist/${CND_CONF}/${IMAGE_TYPE} 
	${MP_CC} $(MP_EXTRA_LD_PRE)  -o dist/${CND_CONF}/${IMAGE_TYPE}/rtos_pilot.X.${IMAGE_TYPE}.${OUTPUT_SUFFIX}  ${OBJECTFILES_QUOTED_IF_SPACED}      -mcpu=$(MP_PROCESSOR_OPTION)        -D__DEBUG -D__MPLAB_DEBUGGER_ICD3=1  -omf=elf -Wl,--defsym=__MPLAB_BUILD=1,--defsym=__MPLAB_DEBUG=1,--defsym=__ICD2RAM=1,--defsym=__DEBUG=1,--defsym=__MPLAB_DEBUGGER_ICD3=1,$(MP_LINKER_FILE_OPTION),--heap=11300,--check-sections,--data-init,--pack-data,--handles,--isr,--no-gc-sections,--fill-upper=0,--stackguard=16,--library-path="..\..\..\..\..\..\..\..\Program Files\Microchip\MPLAB C30\lib",--no-force-link,--smart-io,-Map="${DISTDIR}\rtos_pilot.X.${IMAGE_TYPE}.map",--report-mem$(MP_EXTRA_LD_POST) 
	
else
dist/${CND_CONF}/${IMAGE_TYPE}/rtos_pilot.X.${IMAGE_TYPE}.${OUTPUT_SUFFIX}: ${OBJECTFILES}  nbproject/Makefile-${CND_CONF}.mk   
	@${MKDIR} dist/${CND_CONF}/${IMAGE_TYPE} 
	${MP_CC} $(MP_EXTRA_LD_PRE)  -o dist/${CND_CONF}/${IMAGE_TYPE}/rtos_pilot.X.${IMAGE_TYPE}.${DEBUGGABLE_SUFFIX}  ${OBJECTFILES_QUOTED_IF_SPACED}      -mcpu=$(MP_PROCESSOR_OPTION)        -omf=elf -Wl,--defsym=__MPLAB_BUILD=1,$(MP_LINKER_FILE_OPTION),--heap=11300,--check-sections,--data-init,--pack-data,--handles,--isr,--no-gc-sections,--fill-upper=0,--stackguard=16,--library-path="..\..\..\..\..\..\..\..\Program Files\Microchip\MPLAB C30\lib",--no-force-link,--smart-io,-Map="${DISTDIR}\rtos_pilot.X.${IMAGE_TYPE}.map",--report-mem$(MP_EXTRA_LD_POST) 
	${MP_CC_DIR}\\xc16-bin2hex dist/${CND_CONF}/${IMAGE_TYPE}/rtos_pilot.X.${IMAGE_TYPE}.${DEBUGGABLE_SUFFIX} -a  -omf=elf 
	
endif


# Subprojects
.build-subprojects:


# Subprojects
.clean-subprojects:

# Clean Targets
.clean-conf: ${CLEAN_SUBPROJECTS}
	${RM} -r build/fixed_wing
	${RM} -r dist/fixed_wing

# Enable dependency checking
.dep.inc: .depcheck-impl

DEPFILES=$(shell mplabwildcard ${POSSIBLE_DEPFILES})
ifneq (${DEPFILES},)
include ${DEPFILES}
endif
