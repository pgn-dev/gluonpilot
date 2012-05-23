#
# Generated Makefile - do not edit!
#
# Edit the Makefile in the project folder instead (../Makefile). Each target
# has a -pre and a -post target defined where you can add customized code.
#
# This makefile implements configuration specific macros and targets.


# Include project Makefile
include Makefile

# Environment
SHELL=cmd.exe
# Adding MPLAB X bin directory to path
PATH:=C:/Program Files (x86)/Microchip/MPLABX/mplab_ide/mplab_ide/modules/../../bin/:$(PATH)
MKDIR=gnumkdir -p
RM=rm -f 
MV=mv 
CP=cp 

# Macros
CND_CONF=quadrocopter
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
OBJECTFILES_QUOTED_IF_SPACED=${OBJECTDIR}/_ext/1970174492/croutine.o ${OBJECTDIR}/_ext/1970174492/heap_1.o ${OBJECTDIR}/_ext/1970174492/list.o ${OBJECTDIR}/_ext/1970174492/port.o ${OBJECTDIR}/_ext/1970174492/portasm_dsPIC.o ${OBJECTDIR}/_ext/1970174492/queue.o ${OBJECTDIR}/_ext/1970174492/tasks.o ${OBJECTDIR}/_ext/957539446/adc.o ${OBJECTDIR}/_ext/1077768206/bmp085.o ${OBJECTDIR}/_ext/1070193764/button.o ${OBJECTDIR}/_ext/968823332/dataflash.o ${OBJECTDIR}/_ext/957545600/gps.o ${OBJECTDIR}/_ext/1967121974/hmc5843.o ${OBJECTDIR}/_ext/957545584/i2c.o ${OBJECTDIR}/_ext/957550049/led.o ${OBJECTDIR}/_ext/773745621/matrix.o ${OBJECTDIR}/_ext/1785572984/max7456.o ${OBJECTDIR}/_ext/1843177418/microcontroller.o ${OBJECTDIR}/_ext/957554017/pid.o ${OBJECTDIR}/_ext/674232159/ppm_in.o ${OBJECTDIR}/_ext/667767512/pwm_in.o ${OBJECTDIR}/_ext/888521352/quaternion.o ${OBJECTDIR}/_ext/1429652139/scp1000.o ${OBJECTDIR}/_ext/1089077615/servo.o ${OBJECTDIR}/_ext/1591518261/uart1_queue.o ${OBJECTDIR}/_ext/1090805370/uart2.o ${OBJECTDIR}/_ext/1472/ahrs_kalman_2x3.o ${OBJECTDIR}/_ext/1472/alarms.o ${OBJECTDIR}/_ext/1472/communication_csv.o ${OBJECTDIR}/_ext/1472/configuration.o ${OBJECTDIR}/_ext/1472/control.o ${OBJECTDIR}/_ext/1472/datalogger.o ${OBJECTDIR}/_ext/1472/gluonscript.o ${OBJECTDIR}/_ext/1472/navigation.o ${OBJECTDIR}/_ext/1472/osd_max7456.o ${OBJECTDIR}/_ext/1472/rtos_pilot.o ${OBJECTDIR}/_ext/1472/sensors.o ${OBJECTDIR}/_ext/1472/trigger.o
POSSIBLE_DEPFILES=${OBJECTDIR}/_ext/1970174492/croutine.o.d ${OBJECTDIR}/_ext/1970174492/heap_1.o.d ${OBJECTDIR}/_ext/1970174492/list.o.d ${OBJECTDIR}/_ext/1970174492/port.o.d ${OBJECTDIR}/_ext/1970174492/portasm_dsPIC.o.d ${OBJECTDIR}/_ext/1970174492/queue.o.d ${OBJECTDIR}/_ext/1970174492/tasks.o.d ${OBJECTDIR}/_ext/957539446/adc.o.d ${OBJECTDIR}/_ext/1077768206/bmp085.o.d ${OBJECTDIR}/_ext/1070193764/button.o.d ${OBJECTDIR}/_ext/968823332/dataflash.o.d ${OBJECTDIR}/_ext/957545600/gps.o.d ${OBJECTDIR}/_ext/1967121974/hmc5843.o.d ${OBJECTDIR}/_ext/957545584/i2c.o.d ${OBJECTDIR}/_ext/957550049/led.o.d ${OBJECTDIR}/_ext/773745621/matrix.o.d ${OBJECTDIR}/_ext/1785572984/max7456.o.d ${OBJECTDIR}/_ext/1843177418/microcontroller.o.d ${OBJECTDIR}/_ext/957554017/pid.o.d ${OBJECTDIR}/_ext/674232159/ppm_in.o.d ${OBJECTDIR}/_ext/667767512/pwm_in.o.d ${OBJECTDIR}/_ext/888521352/quaternion.o.d ${OBJECTDIR}/_ext/1429652139/scp1000.o.d ${OBJECTDIR}/_ext/1089077615/servo.o.d ${OBJECTDIR}/_ext/1591518261/uart1_queue.o.d ${OBJECTDIR}/_ext/1090805370/uart2.o.d ${OBJECTDIR}/_ext/1472/ahrs_kalman_2x3.o.d ${OBJECTDIR}/_ext/1472/alarms.o.d ${OBJECTDIR}/_ext/1472/communication_csv.o.d ${OBJECTDIR}/_ext/1472/configuration.o.d ${OBJECTDIR}/_ext/1472/control.o.d ${OBJECTDIR}/_ext/1472/datalogger.o.d ${OBJECTDIR}/_ext/1472/gluonscript.o.d ${OBJECTDIR}/_ext/1472/navigation.o.d ${OBJECTDIR}/_ext/1472/osd_max7456.o.d ${OBJECTDIR}/_ext/1472/rtos_pilot.o.d ${OBJECTDIR}/_ext/1472/sensors.o.d ${OBJECTDIR}/_ext/1472/trigger.o.d

# Object Files
OBJECTFILES=${OBJECTDIR}/_ext/1970174492/croutine.o ${OBJECTDIR}/_ext/1970174492/heap_1.o ${OBJECTDIR}/_ext/1970174492/list.o ${OBJECTDIR}/_ext/1970174492/port.o ${OBJECTDIR}/_ext/1970174492/portasm_dsPIC.o ${OBJECTDIR}/_ext/1970174492/queue.o ${OBJECTDIR}/_ext/1970174492/tasks.o ${OBJECTDIR}/_ext/957539446/adc.o ${OBJECTDIR}/_ext/1077768206/bmp085.o ${OBJECTDIR}/_ext/1070193764/button.o ${OBJECTDIR}/_ext/968823332/dataflash.o ${OBJECTDIR}/_ext/957545600/gps.o ${OBJECTDIR}/_ext/1967121974/hmc5843.o ${OBJECTDIR}/_ext/957545584/i2c.o ${OBJECTDIR}/_ext/957550049/led.o ${OBJECTDIR}/_ext/773745621/matrix.o ${OBJECTDIR}/_ext/1785572984/max7456.o ${OBJECTDIR}/_ext/1843177418/microcontroller.o ${OBJECTDIR}/_ext/957554017/pid.o ${OBJECTDIR}/_ext/674232159/ppm_in.o ${OBJECTDIR}/_ext/667767512/pwm_in.o ${OBJECTDIR}/_ext/888521352/quaternion.o ${OBJECTDIR}/_ext/1429652139/scp1000.o ${OBJECTDIR}/_ext/1089077615/servo.o ${OBJECTDIR}/_ext/1591518261/uart1_queue.o ${OBJECTDIR}/_ext/1090805370/uart2.o ${OBJECTDIR}/_ext/1472/ahrs_kalman_2x3.o ${OBJECTDIR}/_ext/1472/alarms.o ${OBJECTDIR}/_ext/1472/communication_csv.o ${OBJECTDIR}/_ext/1472/configuration.o ${OBJECTDIR}/_ext/1472/control.o ${OBJECTDIR}/_ext/1472/datalogger.o ${OBJECTDIR}/_ext/1472/gluonscript.o ${OBJECTDIR}/_ext/1472/navigation.o ${OBJECTDIR}/_ext/1472/osd_max7456.o ${OBJECTDIR}/_ext/1472/rtos_pilot.o ${OBJECTDIR}/_ext/1472/sensors.o ${OBJECTDIR}/_ext/1472/trigger.o


CFLAGS=
ASFLAGS=
LDLIBSOPTIONS=

# Path to java used to run MPLAB X when this makefile was created
MP_JAVA_PATH="C:\Program Files\Java\jre6/bin/"
OS_CURRENT="$(shell uname -s)"
############# Tool locations ##########################################
# If you copy a project from one host to another, the path where the  #
# compiler is installed may be different.                             #
# If you open this project with MPLAB X in the new host, this         #
# makefile will be regenerated and the paths will be corrected.       #
#######################################################################
MP_CC="C:\Program Files (x86)\Microchip\mplabc30\v3.30c\bin\pic30-gcc.exe"
# MP_BC is not defined
MP_AS="C:\Program Files (x86)\Microchip\mplabc30\v3.30c\bin\pic30-as.exe"
MP_LD="C:\Program Files (x86)\Microchip\mplabc30\v3.30c\bin\pic30-ld.exe"
MP_AR="C:\Program Files (x86)\Microchip\mplabc30\v3.30c\bin\pic30-ar.exe"
DEP_GEN=${MP_JAVA_PATH}java -jar "C:/Program Files (x86)/Microchip/MPLABX/mplab_ide/mplab_ide/modules/../../bin/extractobjectdependencies.jar" 
# fixDeps replaces a bunch of sed/cat/printf statements that slow down the build
FIXDEPS=fixDeps
MP_CC_DIR="C:\Program Files (x86)\Microchip\mplabc30\v3.30c\bin"
# MP_BC_DIR is not defined
MP_AS_DIR="C:\Program Files (x86)\Microchip\mplabc30\v3.30c\bin"
MP_LD_DIR="C:\Program Files (x86)\Microchip\mplabc30\v3.30c\bin"
MP_AR_DIR="C:\Program Files (x86)\Microchip\mplabc30\v3.30c\bin"
# MP_BC_DIR is not defined

.build-conf:  ${BUILD_SUBPROJECTS}
	${MAKE}  -f nbproject/Makefile-quadrocopter.mk dist/${CND_CONF}/${IMAGE_TYPE}/rtos_pilot.X.${IMAGE_TYPE}.${OUTPUT_SUFFIX}

MP_PROCESSOR_OPTION=33FJ256MC710
MP_LINKER_FILE_OPTION=,-Tp33FJ256MC710.gld
# ------------------------------------------------------------------------------------
# Rules for buildStep: assemble
ifeq ($(TYPE_IMAGE), DEBUG_RUN)
else
endif

# ------------------------------------------------------------------------------------
# Rules for buildStep: assembleWithPreprocess
ifeq ($(TYPE_IMAGE), DEBUG_RUN)
${OBJECTDIR}/_ext/1970174492/portasm_dsPIC.o: ../../lib/FreeRTOS/portasm_dsPIC.S  nbproject/Makefile-${CND_CONF}.mk
	@${MKDIR} ${OBJECTDIR}/_ext/1970174492 
	@${RM} ${OBJECTDIR}/_ext/1970174492/portasm_dsPIC.o.d ${OBJECTDIR}/_ext/1970174492/portasm_dsPIC.o.asm.d 
	@${RM} ${OBJECTDIR}/_ext/1970174492/portasm_dsPIC.o.ok ${OBJECTDIR}/_ext/1970174492/portasm_dsPIC.o.err 
	@${FIXDEPS} "${OBJECTDIR}/_ext/1970174492/portasm_dsPIC.o.d" "${OBJECTDIR}/_ext/1970174492/portasm_dsPIC.o.asm.d" -t $(SILENT) -c ${MP_CC} $(MP_EXTRA_AS_PRE)  -D__DEBUG -D__MPLAB_DEBUGGER_ICD3=1 -omf=elf -c -mcpu=$(MP_PROCESSOR_OPTION)  -I"../../lib/FreeRTOS" -I"../../lib" -I"../../lib/button" -I"../../lib/adc" -I".." -I"../../lib/i2c" -I"../../lib/bmp085" -I"../../lib/hmc5843" -I"../../lib/max7456" -I"../../lib/matrix" -I"../../lib/quaternion" -I"../../lib/pid" -I"../../lib/pwm_in" -I"../../lib/led" -I"../../lib/ppm_in" -I"../../lib/uart2" -I"../../lib/uart1_queue" -I"../../lib/servo" -I"../../lib/scp1000" -I"../../lib/microcontroller" -I"../../lib/gps" -I"../../lib/dataflash" -MMD -MF "${OBJECTDIR}/_ext/1970174492/portasm_dsPIC.o.d"  -o ${OBJECTDIR}/_ext/1970174492/portasm_dsPIC.o ../../lib/FreeRTOS/portasm_dsPIC.S  -Wa,--defsym=__MPLAB_BUILD=1$(MP_EXTRA_AS_POST),-MD="${OBJECTDIR}/_ext/1970174492/portasm_dsPIC.o.asm.d",--defsym=__MPLAB_DEBUG=1,--defsym=__ICD2RAM=1,--defsym=__DEBUG=1,--defsym=__MPLAB_DEBUGGER_ICD3=1,-g,-I".."
	
else
${OBJECTDIR}/_ext/1970174492/portasm_dsPIC.o: ../../lib/FreeRTOS/portasm_dsPIC.S  nbproject/Makefile-${CND_CONF}.mk
	@${MKDIR} ${OBJECTDIR}/_ext/1970174492 
	@${RM} ${OBJECTDIR}/_ext/1970174492/portasm_dsPIC.o.d ${OBJECTDIR}/_ext/1970174492/portasm_dsPIC.o.asm.d 
	@${RM} ${OBJECTDIR}/_ext/1970174492/portasm_dsPIC.o.ok ${OBJECTDIR}/_ext/1970174492/portasm_dsPIC.o.err 
	@${FIXDEPS} "${OBJECTDIR}/_ext/1970174492/portasm_dsPIC.o.d" "${OBJECTDIR}/_ext/1970174492/portasm_dsPIC.o.asm.d" -t $(SILENT) -c ${MP_CC} $(MP_EXTRA_AS_PRE)  -omf=elf -c -mcpu=$(MP_PROCESSOR_OPTION)  -I"../../lib/FreeRTOS" -I"../../lib" -I"../../lib/button" -I"../../lib/adc" -I".." -I"../../lib/i2c" -I"../../lib/bmp085" -I"../../lib/hmc5843" -I"../../lib/max7456" -I"../../lib/matrix" -I"../../lib/quaternion" -I"../../lib/pid" -I"../../lib/pwm_in" -I"../../lib/led" -I"../../lib/ppm_in" -I"../../lib/uart2" -I"../../lib/uart1_queue" -I"../../lib/servo" -I"../../lib/scp1000" -I"../../lib/microcontroller" -I"../../lib/gps" -I"../../lib/dataflash" -MMD -MF "${OBJECTDIR}/_ext/1970174492/portasm_dsPIC.o.d"  -o ${OBJECTDIR}/_ext/1970174492/portasm_dsPIC.o ../../lib/FreeRTOS/portasm_dsPIC.S  -Wa,--defsym=__MPLAB_BUILD=1$(MP_EXTRA_AS_POST),-MD="${OBJECTDIR}/_ext/1970174492/portasm_dsPIC.o.asm.d",-g,-I".."
	
endif

# ------------------------------------------------------------------------------------
# Rules for buildStep: compile
ifeq ($(TYPE_IMAGE), DEBUG_RUN)
${OBJECTDIR}/_ext/1970174492/croutine.o: ../../lib/FreeRTOS/croutine.c  nbproject/Makefile-${CND_CONF}.mk
	@${MKDIR} ${OBJECTDIR}/_ext/1970174492 
	@${RM} ${OBJECTDIR}/_ext/1970174492/croutine.o.d 
	@${RM} ${OBJECTDIR}/_ext/1970174492/croutine.o.ok ${OBJECTDIR}/_ext/1970174492/croutine.o.err 
	@${FIXDEPS} "${OBJECTDIR}/_ext/1970174492/croutine.o.d" $(SILENT) -c ${MP_CC} $(MP_EXTRA_CC_PRE) -g -D__DEBUG -D__MPLAB_DEBUGGER_ICD3=1 -omf=elf -x c -c -mcpu=$(MP_PROCESSOR_OPTION) -Wall -DMPLAB_DSPIC_PORT -DENABLE_QUADROCOPTER -I"../../lib/FreeRTOS" -I"../../lib" -I"../../lib/button" -I"../../lib/adc" -I".." -I"../../lib/i2c" -I"../../lib/bmp085" -I"../../lib/hmc5843" -I"../../lib/max7456" -I"../../lib/matrix" -I"../../lib/quaternion" -I"../../lib/pid" -I"../../lib/pwm_in" -I"../../lib/led" -I"../../lib/ppm_in" -I"../../lib/uart2" -I"../../lib/uart1_queue" -I"../../lib/servo" -I"../../lib/scp1000" -I"../../lib/microcontroller" -I"../../lib/gps" -I"../../lib/dataflash" -mlarge-code -mlarge-data -O1 -MMD -MF "${OBJECTDIR}/_ext/1970174492/croutine.o.d" -o ${OBJECTDIR}/_ext/1970174492/croutine.o ../../lib/FreeRTOS/croutine.c  
	
${OBJECTDIR}/_ext/1970174492/heap_1.o: ../../lib/FreeRTOS/heap_1.c  nbproject/Makefile-${CND_CONF}.mk
	@${MKDIR} ${OBJECTDIR}/_ext/1970174492 
	@${RM} ${OBJECTDIR}/_ext/1970174492/heap_1.o.d 
	@${RM} ${OBJECTDIR}/_ext/1970174492/heap_1.o.ok ${OBJECTDIR}/_ext/1970174492/heap_1.o.err 
	@${FIXDEPS} "${OBJECTDIR}/_ext/1970174492/heap_1.o.d" $(SILENT) -c ${MP_CC} $(MP_EXTRA_CC_PRE) -g -D__DEBUG -D__MPLAB_DEBUGGER_ICD3=1 -omf=elf -x c -c -mcpu=$(MP_PROCESSOR_OPTION) -Wall -DMPLAB_DSPIC_PORT -DENABLE_QUADROCOPTER -I"../../lib/FreeRTOS" -I"../../lib" -I"../../lib/button" -I"../../lib/adc" -I".." -I"../../lib/i2c" -I"../../lib/bmp085" -I"../../lib/hmc5843" -I"../../lib/max7456" -I"../../lib/matrix" -I"../../lib/quaternion" -I"../../lib/pid" -I"../../lib/pwm_in" -I"../../lib/led" -I"../../lib/ppm_in" -I"../../lib/uart2" -I"../../lib/uart1_queue" -I"../../lib/servo" -I"../../lib/scp1000" -I"../../lib/microcontroller" -I"../../lib/gps" -I"../../lib/dataflash" -mlarge-code -mlarge-data -O1 -MMD -MF "${OBJECTDIR}/_ext/1970174492/heap_1.o.d" -o ${OBJECTDIR}/_ext/1970174492/heap_1.o ../../lib/FreeRTOS/heap_1.c  
	
${OBJECTDIR}/_ext/1970174492/list.o: ../../lib/FreeRTOS/list.c  nbproject/Makefile-${CND_CONF}.mk
	@${MKDIR} ${OBJECTDIR}/_ext/1970174492 
	@${RM} ${OBJECTDIR}/_ext/1970174492/list.o.d 
	@${RM} ${OBJECTDIR}/_ext/1970174492/list.o.ok ${OBJECTDIR}/_ext/1970174492/list.o.err 
	@${FIXDEPS} "${OBJECTDIR}/_ext/1970174492/list.o.d" $(SILENT) -c ${MP_CC} $(MP_EXTRA_CC_PRE) -g -D__DEBUG -D__MPLAB_DEBUGGER_ICD3=1 -omf=elf -x c -c -mcpu=$(MP_PROCESSOR_OPTION) -Wall -DMPLAB_DSPIC_PORT -DENABLE_QUADROCOPTER -I"../../lib/FreeRTOS" -I"../../lib" -I"../../lib/button" -I"../../lib/adc" -I".." -I"../../lib/i2c" -I"../../lib/bmp085" -I"../../lib/hmc5843" -I"../../lib/max7456" -I"../../lib/matrix" -I"../../lib/quaternion" -I"../../lib/pid" -I"../../lib/pwm_in" -I"../../lib/led" -I"../../lib/ppm_in" -I"../../lib/uart2" -I"../../lib/uart1_queue" -I"../../lib/servo" -I"../../lib/scp1000" -I"../../lib/microcontroller" -I"../../lib/gps" -I"../../lib/dataflash" -mlarge-code -mlarge-data -O1 -MMD -MF "${OBJECTDIR}/_ext/1970174492/list.o.d" -o ${OBJECTDIR}/_ext/1970174492/list.o ../../lib/FreeRTOS/list.c  
	
${OBJECTDIR}/_ext/1970174492/port.o: ../../lib/FreeRTOS/port.c  nbproject/Makefile-${CND_CONF}.mk
	@${MKDIR} ${OBJECTDIR}/_ext/1970174492 
	@${RM} ${OBJECTDIR}/_ext/1970174492/port.o.d 
	@${RM} ${OBJECTDIR}/_ext/1970174492/port.o.ok ${OBJECTDIR}/_ext/1970174492/port.o.err 
	@${FIXDEPS} "${OBJECTDIR}/_ext/1970174492/port.o.d" $(SILENT) -c ${MP_CC} $(MP_EXTRA_CC_PRE) -g -D__DEBUG -D__MPLAB_DEBUGGER_ICD3=1 -omf=elf -x c -c -mcpu=$(MP_PROCESSOR_OPTION) -Wall -DMPLAB_DSPIC_PORT -DENABLE_QUADROCOPTER -I"../../lib/FreeRTOS" -I"../../lib" -I"../../lib/button" -I"../../lib/adc" -I".." -I"../../lib/i2c" -I"../../lib/bmp085" -I"../../lib/hmc5843" -I"../../lib/max7456" -I"../../lib/matrix" -I"../../lib/quaternion" -I"../../lib/pid" -I"../../lib/pwm_in" -I"../../lib/led" -I"../../lib/ppm_in" -I"../../lib/uart2" -I"../../lib/uart1_queue" -I"../../lib/servo" -I"../../lib/scp1000" -I"../../lib/microcontroller" -I"../../lib/gps" -I"../../lib/dataflash" -mlarge-code -mlarge-data -O1 -MMD -MF "${OBJECTDIR}/_ext/1970174492/port.o.d" -o ${OBJECTDIR}/_ext/1970174492/port.o ../../lib/FreeRTOS/port.c  
	
${OBJECTDIR}/_ext/1970174492/queue.o: ../../lib/FreeRTOS/queue.c  nbproject/Makefile-${CND_CONF}.mk
	@${MKDIR} ${OBJECTDIR}/_ext/1970174492 
	@${RM} ${OBJECTDIR}/_ext/1970174492/queue.o.d 
	@${RM} ${OBJECTDIR}/_ext/1970174492/queue.o.ok ${OBJECTDIR}/_ext/1970174492/queue.o.err 
	@${FIXDEPS} "${OBJECTDIR}/_ext/1970174492/queue.o.d" $(SILENT) -c ${MP_CC} $(MP_EXTRA_CC_PRE) -g -D__DEBUG -D__MPLAB_DEBUGGER_ICD3=1 -omf=elf -x c -c -mcpu=$(MP_PROCESSOR_OPTION) -Wall -DMPLAB_DSPIC_PORT -DENABLE_QUADROCOPTER -I"../../lib/FreeRTOS" -I"../../lib" -I"../../lib/button" -I"../../lib/adc" -I".." -I"../../lib/i2c" -I"../../lib/bmp085" -I"../../lib/hmc5843" -I"../../lib/max7456" -I"../../lib/matrix" -I"../../lib/quaternion" -I"../../lib/pid" -I"../../lib/pwm_in" -I"../../lib/led" -I"../../lib/ppm_in" -I"../../lib/uart2" -I"../../lib/uart1_queue" -I"../../lib/servo" -I"../../lib/scp1000" -I"../../lib/microcontroller" -I"../../lib/gps" -I"../../lib/dataflash" -mlarge-code -mlarge-data -O1 -MMD -MF "${OBJECTDIR}/_ext/1970174492/queue.o.d" -o ${OBJECTDIR}/_ext/1970174492/queue.o ../../lib/FreeRTOS/queue.c  
	
${OBJECTDIR}/_ext/1970174492/tasks.o: ../../lib/FreeRTOS/tasks.c  nbproject/Makefile-${CND_CONF}.mk
	@${MKDIR} ${OBJECTDIR}/_ext/1970174492 
	@${RM} ${OBJECTDIR}/_ext/1970174492/tasks.o.d 
	@${RM} ${OBJECTDIR}/_ext/1970174492/tasks.o.ok ${OBJECTDIR}/_ext/1970174492/tasks.o.err 
	@${FIXDEPS} "${OBJECTDIR}/_ext/1970174492/tasks.o.d" $(SILENT) -c ${MP_CC} $(MP_EXTRA_CC_PRE) -g -D__DEBUG -D__MPLAB_DEBUGGER_ICD3=1 -omf=elf -x c -c -mcpu=$(MP_PROCESSOR_OPTION) -Wall -DMPLAB_DSPIC_PORT -DENABLE_QUADROCOPTER -I"../../lib/FreeRTOS" -I"../../lib" -I"../../lib/button" -I"../../lib/adc" -I".." -I"../../lib/i2c" -I"../../lib/bmp085" -I"../../lib/hmc5843" -I"../../lib/max7456" -I"../../lib/matrix" -I"../../lib/quaternion" -I"../../lib/pid" -I"../../lib/pwm_in" -I"../../lib/led" -I"../../lib/ppm_in" -I"../../lib/uart2" -I"../../lib/uart1_queue" -I"../../lib/servo" -I"../../lib/scp1000" -I"../../lib/microcontroller" -I"../../lib/gps" -I"../../lib/dataflash" -mlarge-code -mlarge-data -O1 -MMD -MF "${OBJECTDIR}/_ext/1970174492/tasks.o.d" -o ${OBJECTDIR}/_ext/1970174492/tasks.o ../../lib/FreeRTOS/tasks.c  
	
${OBJECTDIR}/_ext/957539446/adc.o: ../../lib/adc/adc.c  nbproject/Makefile-${CND_CONF}.mk
	@${MKDIR} ${OBJECTDIR}/_ext/957539446 
	@${RM} ${OBJECTDIR}/_ext/957539446/adc.o.d 
	@${RM} ${OBJECTDIR}/_ext/957539446/adc.o.ok ${OBJECTDIR}/_ext/957539446/adc.o.err 
	@${FIXDEPS} "${OBJECTDIR}/_ext/957539446/adc.o.d" $(SILENT) -c ${MP_CC} $(MP_EXTRA_CC_PRE) -g -D__DEBUG -D__MPLAB_DEBUGGER_ICD3=1 -omf=elf -x c -c -mcpu=$(MP_PROCESSOR_OPTION) -Wall -DMPLAB_DSPIC_PORT -DENABLE_QUADROCOPTER -I"../../lib/FreeRTOS" -I"../../lib" -I"../../lib/button" -I"../../lib/adc" -I".." -I"../../lib/i2c" -I"../../lib/bmp085" -I"../../lib/hmc5843" -I"../../lib/max7456" -I"../../lib/matrix" -I"../../lib/quaternion" -I"../../lib/pid" -I"../../lib/pwm_in" -I"../../lib/led" -I"../../lib/ppm_in" -I"../../lib/uart2" -I"../../lib/uart1_queue" -I"../../lib/servo" -I"../../lib/scp1000" -I"../../lib/microcontroller" -I"../../lib/gps" -I"../../lib/dataflash" -mlarge-code -mlarge-data -O1 -MMD -MF "${OBJECTDIR}/_ext/957539446/adc.o.d" -o ${OBJECTDIR}/_ext/957539446/adc.o ../../lib/adc/adc.c  
	
${OBJECTDIR}/_ext/1077768206/bmp085.o: ../../lib/bmp085/bmp085.c  nbproject/Makefile-${CND_CONF}.mk
	@${MKDIR} ${OBJECTDIR}/_ext/1077768206 
	@${RM} ${OBJECTDIR}/_ext/1077768206/bmp085.o.d 
	@${RM} ${OBJECTDIR}/_ext/1077768206/bmp085.o.ok ${OBJECTDIR}/_ext/1077768206/bmp085.o.err 
	@${FIXDEPS} "${OBJECTDIR}/_ext/1077768206/bmp085.o.d" $(SILENT) -c ${MP_CC} $(MP_EXTRA_CC_PRE) -g -D__DEBUG -D__MPLAB_DEBUGGER_ICD3=1 -omf=elf -x c -c -mcpu=$(MP_PROCESSOR_OPTION) -Wall -DMPLAB_DSPIC_PORT -DENABLE_QUADROCOPTER -I"../../lib/FreeRTOS" -I"../../lib" -I"../../lib/button" -I"../../lib/adc" -I".." -I"../../lib/i2c" -I"../../lib/bmp085" -I"../../lib/hmc5843" -I"../../lib/max7456" -I"../../lib/matrix" -I"../../lib/quaternion" -I"../../lib/pid" -I"../../lib/pwm_in" -I"../../lib/led" -I"../../lib/ppm_in" -I"../../lib/uart2" -I"../../lib/uart1_queue" -I"../../lib/servo" -I"../../lib/scp1000" -I"../../lib/microcontroller" -I"../../lib/gps" -I"../../lib/dataflash" -mlarge-code -mlarge-data -O1 -MMD -MF "${OBJECTDIR}/_ext/1077768206/bmp085.o.d" -o ${OBJECTDIR}/_ext/1077768206/bmp085.o ../../lib/bmp085/bmp085.c  
	
${OBJECTDIR}/_ext/1070193764/button.o: ../../lib/button/button.c  nbproject/Makefile-${CND_CONF}.mk
	@${MKDIR} ${OBJECTDIR}/_ext/1070193764 
	@${RM} ${OBJECTDIR}/_ext/1070193764/button.o.d 
	@${RM} ${OBJECTDIR}/_ext/1070193764/button.o.ok ${OBJECTDIR}/_ext/1070193764/button.o.err 
	@${FIXDEPS} "${OBJECTDIR}/_ext/1070193764/button.o.d" $(SILENT) -c ${MP_CC} $(MP_EXTRA_CC_PRE) -g -D__DEBUG -D__MPLAB_DEBUGGER_ICD3=1 -omf=elf -x c -c -mcpu=$(MP_PROCESSOR_OPTION) -Wall -DMPLAB_DSPIC_PORT -DENABLE_QUADROCOPTER -I"../../lib/FreeRTOS" -I"../../lib" -I"../../lib/button" -I"../../lib/adc" -I".." -I"../../lib/i2c" -I"../../lib/bmp085" -I"../../lib/hmc5843" -I"../../lib/max7456" -I"../../lib/matrix" -I"../../lib/quaternion" -I"../../lib/pid" -I"../../lib/pwm_in" -I"../../lib/led" -I"../../lib/ppm_in" -I"../../lib/uart2" -I"../../lib/uart1_queue" -I"../../lib/servo" -I"../../lib/scp1000" -I"../../lib/microcontroller" -I"../../lib/gps" -I"../../lib/dataflash" -mlarge-code -mlarge-data -O1 -MMD -MF "${OBJECTDIR}/_ext/1070193764/button.o.d" -o ${OBJECTDIR}/_ext/1070193764/button.o ../../lib/button/button.c  
	
${OBJECTDIR}/_ext/968823332/dataflash.o: ../../lib/dataflash/dataflash.c  nbproject/Makefile-${CND_CONF}.mk
	@${MKDIR} ${OBJECTDIR}/_ext/968823332 
	@${RM} ${OBJECTDIR}/_ext/968823332/dataflash.o.d 
	@${RM} ${OBJECTDIR}/_ext/968823332/dataflash.o.ok ${OBJECTDIR}/_ext/968823332/dataflash.o.err 
	@${FIXDEPS} "${OBJECTDIR}/_ext/968823332/dataflash.o.d" $(SILENT) -c ${MP_CC} $(MP_EXTRA_CC_PRE) -g -D__DEBUG -D__MPLAB_DEBUGGER_ICD3=1 -omf=elf -x c -c -mcpu=$(MP_PROCESSOR_OPTION) -Wall -DMPLAB_DSPIC_PORT -DENABLE_QUADROCOPTER -I"../../lib/FreeRTOS" -I"../../lib" -I"../../lib/button" -I"../../lib/adc" -I".." -I"../../lib/i2c" -I"../../lib/bmp085" -I"../../lib/hmc5843" -I"../../lib/max7456" -I"../../lib/matrix" -I"../../lib/quaternion" -I"../../lib/pid" -I"../../lib/pwm_in" -I"../../lib/led" -I"../../lib/ppm_in" -I"../../lib/uart2" -I"../../lib/uart1_queue" -I"../../lib/servo" -I"../../lib/scp1000" -I"../../lib/microcontroller" -I"../../lib/gps" -I"../../lib/dataflash" -mlarge-code -mlarge-data -O1 -MMD -MF "${OBJECTDIR}/_ext/968823332/dataflash.o.d" -o ${OBJECTDIR}/_ext/968823332/dataflash.o ../../lib/dataflash/dataflash.c  
	
${OBJECTDIR}/_ext/957545600/gps.o: ../../lib/gps/gps.c  nbproject/Makefile-${CND_CONF}.mk
	@${MKDIR} ${OBJECTDIR}/_ext/957545600 
	@${RM} ${OBJECTDIR}/_ext/957545600/gps.o.d 
	@${RM} ${OBJECTDIR}/_ext/957545600/gps.o.ok ${OBJECTDIR}/_ext/957545600/gps.o.err 
	@${FIXDEPS} "${OBJECTDIR}/_ext/957545600/gps.o.d" $(SILENT) -c ${MP_CC} $(MP_EXTRA_CC_PRE) -g -D__DEBUG -D__MPLAB_DEBUGGER_ICD3=1 -omf=elf -x c -c -mcpu=$(MP_PROCESSOR_OPTION) -Wall -DMPLAB_DSPIC_PORT -DENABLE_QUADROCOPTER -I"../../lib/FreeRTOS" -I"../../lib" -I"../../lib/button" -I"../../lib/adc" -I".." -I"../../lib/i2c" -I"../../lib/bmp085" -I"../../lib/hmc5843" -I"../../lib/max7456" -I"../../lib/matrix" -I"../../lib/quaternion" -I"../../lib/pid" -I"../../lib/pwm_in" -I"../../lib/led" -I"../../lib/ppm_in" -I"../../lib/uart2" -I"../../lib/uart1_queue" -I"../../lib/servo" -I"../../lib/scp1000" -I"../../lib/microcontroller" -I"../../lib/gps" -I"../../lib/dataflash" -mlarge-code -mlarge-data -O1 -MMD -MF "${OBJECTDIR}/_ext/957545600/gps.o.d" -o ${OBJECTDIR}/_ext/957545600/gps.o ../../lib/gps/gps.c  
	
${OBJECTDIR}/_ext/1967121974/hmc5843.o: ../../lib/hmc5843/hmc5843.c  nbproject/Makefile-${CND_CONF}.mk
	@${MKDIR} ${OBJECTDIR}/_ext/1967121974 
	@${RM} ${OBJECTDIR}/_ext/1967121974/hmc5843.o.d 
	@${RM} ${OBJECTDIR}/_ext/1967121974/hmc5843.o.ok ${OBJECTDIR}/_ext/1967121974/hmc5843.o.err 
	@${FIXDEPS} "${OBJECTDIR}/_ext/1967121974/hmc5843.o.d" $(SILENT) -c ${MP_CC} $(MP_EXTRA_CC_PRE) -g -D__DEBUG -D__MPLAB_DEBUGGER_ICD3=1 -omf=elf -x c -c -mcpu=$(MP_PROCESSOR_OPTION) -Wall -DMPLAB_DSPIC_PORT -DENABLE_QUADROCOPTER -I"../../lib/FreeRTOS" -I"../../lib" -I"../../lib/button" -I"../../lib/adc" -I".." -I"../../lib/i2c" -I"../../lib/bmp085" -I"../../lib/hmc5843" -I"../../lib/max7456" -I"../../lib/matrix" -I"../../lib/quaternion" -I"../../lib/pid" -I"../../lib/pwm_in" -I"../../lib/led" -I"../../lib/ppm_in" -I"../../lib/uart2" -I"../../lib/uart1_queue" -I"../../lib/servo" -I"../../lib/scp1000" -I"../../lib/microcontroller" -I"../../lib/gps" -I"../../lib/dataflash" -mlarge-code -mlarge-data -O1 -MMD -MF "${OBJECTDIR}/_ext/1967121974/hmc5843.o.d" -o ${OBJECTDIR}/_ext/1967121974/hmc5843.o ../../lib/hmc5843/hmc5843.c  
	
${OBJECTDIR}/_ext/957545584/i2c.o: ../../lib/i2c/i2c.c  nbproject/Makefile-${CND_CONF}.mk
	@${MKDIR} ${OBJECTDIR}/_ext/957545584 
	@${RM} ${OBJECTDIR}/_ext/957545584/i2c.o.d 
	@${RM} ${OBJECTDIR}/_ext/957545584/i2c.o.ok ${OBJECTDIR}/_ext/957545584/i2c.o.err 
	@${FIXDEPS} "${OBJECTDIR}/_ext/957545584/i2c.o.d" $(SILENT) -c ${MP_CC} $(MP_EXTRA_CC_PRE) -g -D__DEBUG -D__MPLAB_DEBUGGER_ICD3=1 -omf=elf -x c -c -mcpu=$(MP_PROCESSOR_OPTION) -Wall -DMPLAB_DSPIC_PORT -DENABLE_QUADROCOPTER -I"../../lib/FreeRTOS" -I"../../lib" -I"../../lib/button" -I"../../lib/adc" -I".." -I"../../lib/i2c" -I"../../lib/bmp085" -I"../../lib/hmc5843" -I"../../lib/max7456" -I"../../lib/matrix" -I"../../lib/quaternion" -I"../../lib/pid" -I"../../lib/pwm_in" -I"../../lib/led" -I"../../lib/ppm_in" -I"../../lib/uart2" -I"../../lib/uart1_queue" -I"../../lib/servo" -I"../../lib/scp1000" -I"../../lib/microcontroller" -I"../../lib/gps" -I"../../lib/dataflash" -mlarge-code -mlarge-data -O1 -MMD -MF "${OBJECTDIR}/_ext/957545584/i2c.o.d" -o ${OBJECTDIR}/_ext/957545584/i2c.o ../../lib/i2c/i2c.c  
	
${OBJECTDIR}/_ext/957550049/led.o: ../../lib/led/led.c  nbproject/Makefile-${CND_CONF}.mk
	@${MKDIR} ${OBJECTDIR}/_ext/957550049 
	@${RM} ${OBJECTDIR}/_ext/957550049/led.o.d 
	@${RM} ${OBJECTDIR}/_ext/957550049/led.o.ok ${OBJECTDIR}/_ext/957550049/led.o.err 
	@${FIXDEPS} "${OBJECTDIR}/_ext/957550049/led.o.d" $(SILENT) -c ${MP_CC} $(MP_EXTRA_CC_PRE) -g -D__DEBUG -D__MPLAB_DEBUGGER_ICD3=1 -omf=elf -x c -c -mcpu=$(MP_PROCESSOR_OPTION) -Wall -DMPLAB_DSPIC_PORT -DENABLE_QUADROCOPTER -I"../../lib/FreeRTOS" -I"../../lib" -I"../../lib/button" -I"../../lib/adc" -I".." -I"../../lib/i2c" -I"../../lib/bmp085" -I"../../lib/hmc5843" -I"../../lib/max7456" -I"../../lib/matrix" -I"../../lib/quaternion" -I"../../lib/pid" -I"../../lib/pwm_in" -I"../../lib/led" -I"../../lib/ppm_in" -I"../../lib/uart2" -I"../../lib/uart1_queue" -I"../../lib/servo" -I"../../lib/scp1000" -I"../../lib/microcontroller" -I"../../lib/gps" -I"../../lib/dataflash" -mlarge-code -mlarge-data -O1 -MMD -MF "${OBJECTDIR}/_ext/957550049/led.o.d" -o ${OBJECTDIR}/_ext/957550049/led.o ../../lib/led/led.c  
	
${OBJECTDIR}/_ext/773745621/matrix.o: ../../lib/matrix/matrix.c  nbproject/Makefile-${CND_CONF}.mk
	@${MKDIR} ${OBJECTDIR}/_ext/773745621 
	@${RM} ${OBJECTDIR}/_ext/773745621/matrix.o.d 
	@${RM} ${OBJECTDIR}/_ext/773745621/matrix.o.ok ${OBJECTDIR}/_ext/773745621/matrix.o.err 
	@${FIXDEPS} "${OBJECTDIR}/_ext/773745621/matrix.o.d" $(SILENT) -c ${MP_CC} $(MP_EXTRA_CC_PRE) -g -D__DEBUG -D__MPLAB_DEBUGGER_ICD3=1 -omf=elf -x c -c -mcpu=$(MP_PROCESSOR_OPTION) -Wall -DMPLAB_DSPIC_PORT -DENABLE_QUADROCOPTER -I"../../lib/FreeRTOS" -I"../../lib" -I"../../lib/button" -I"../../lib/adc" -I".." -I"../../lib/i2c" -I"../../lib/bmp085" -I"../../lib/hmc5843" -I"../../lib/max7456" -I"../../lib/matrix" -I"../../lib/quaternion" -I"../../lib/pid" -I"../../lib/pwm_in" -I"../../lib/led" -I"../../lib/ppm_in" -I"../../lib/uart2" -I"../../lib/uart1_queue" -I"../../lib/servo" -I"../../lib/scp1000" -I"../../lib/microcontroller" -I"../../lib/gps" -I"../../lib/dataflash" -mlarge-code -mlarge-data -O1 -MMD -MF "${OBJECTDIR}/_ext/773745621/matrix.o.d" -o ${OBJECTDIR}/_ext/773745621/matrix.o ../../lib/matrix/matrix.c  
	
${OBJECTDIR}/_ext/1785572984/max7456.o: ../../lib/max7456/max7456.c  nbproject/Makefile-${CND_CONF}.mk
	@${MKDIR} ${OBJECTDIR}/_ext/1785572984 
	@${RM} ${OBJECTDIR}/_ext/1785572984/max7456.o.d 
	@${RM} ${OBJECTDIR}/_ext/1785572984/max7456.o.ok ${OBJECTDIR}/_ext/1785572984/max7456.o.err 
	@${FIXDEPS} "${OBJECTDIR}/_ext/1785572984/max7456.o.d" $(SILENT) -c ${MP_CC} $(MP_EXTRA_CC_PRE) -g -D__DEBUG -D__MPLAB_DEBUGGER_ICD3=1 -omf=elf -x c -c -mcpu=$(MP_PROCESSOR_OPTION) -Wall -DMPLAB_DSPIC_PORT -DENABLE_QUADROCOPTER -I"../../lib/FreeRTOS" -I"../../lib" -I"../../lib/button" -I"../../lib/adc" -I".." -I"../../lib/i2c" -I"../../lib/bmp085" -I"../../lib/hmc5843" -I"../../lib/max7456" -I"../../lib/matrix" -I"../../lib/quaternion" -I"../../lib/pid" -I"../../lib/pwm_in" -I"../../lib/led" -I"../../lib/ppm_in" -I"../../lib/uart2" -I"../../lib/uart1_queue" -I"../../lib/servo" -I"../../lib/scp1000" -I"../../lib/microcontroller" -I"../../lib/gps" -I"../../lib/dataflash" -mlarge-code -mlarge-data -O1 -MMD -MF "${OBJECTDIR}/_ext/1785572984/max7456.o.d" -o ${OBJECTDIR}/_ext/1785572984/max7456.o ../../lib/max7456/max7456.c  
	
${OBJECTDIR}/_ext/1843177418/microcontroller.o: ../../lib/microcontroller/microcontroller.c  nbproject/Makefile-${CND_CONF}.mk
	@${MKDIR} ${OBJECTDIR}/_ext/1843177418 
	@${RM} ${OBJECTDIR}/_ext/1843177418/microcontroller.o.d 
	@${RM} ${OBJECTDIR}/_ext/1843177418/microcontroller.o.ok ${OBJECTDIR}/_ext/1843177418/microcontroller.o.err 
	@${FIXDEPS} "${OBJECTDIR}/_ext/1843177418/microcontroller.o.d" $(SILENT) -c ${MP_CC} $(MP_EXTRA_CC_PRE) -g -D__DEBUG -D__MPLAB_DEBUGGER_ICD3=1 -omf=elf -x c -c -mcpu=$(MP_PROCESSOR_OPTION) -Wall -DMPLAB_DSPIC_PORT -DENABLE_QUADROCOPTER -I"../../lib/FreeRTOS" -I"../../lib" -I"../../lib/button" -I"../../lib/adc" -I".." -I"../../lib/i2c" -I"../../lib/bmp085" -I"../../lib/hmc5843" -I"../../lib/max7456" -I"../../lib/matrix" -I"../../lib/quaternion" -I"../../lib/pid" -I"../../lib/pwm_in" -I"../../lib/led" -I"../../lib/ppm_in" -I"../../lib/uart2" -I"../../lib/uart1_queue" -I"../../lib/servo" -I"../../lib/scp1000" -I"../../lib/microcontroller" -I"../../lib/gps" -I"../../lib/dataflash" -mlarge-code -mlarge-data -O1 -MMD -MF "${OBJECTDIR}/_ext/1843177418/microcontroller.o.d" -o ${OBJECTDIR}/_ext/1843177418/microcontroller.o ../../lib/microcontroller/microcontroller.c  
	
${OBJECTDIR}/_ext/957554017/pid.o: ../../lib/pid/pid.c  nbproject/Makefile-${CND_CONF}.mk
	@${MKDIR} ${OBJECTDIR}/_ext/957554017 
	@${RM} ${OBJECTDIR}/_ext/957554017/pid.o.d 
	@${RM} ${OBJECTDIR}/_ext/957554017/pid.o.ok ${OBJECTDIR}/_ext/957554017/pid.o.err 
	@${FIXDEPS} "${OBJECTDIR}/_ext/957554017/pid.o.d" $(SILENT) -c ${MP_CC} $(MP_EXTRA_CC_PRE) -g -D__DEBUG -D__MPLAB_DEBUGGER_ICD3=1 -omf=elf -x c -c -mcpu=$(MP_PROCESSOR_OPTION) -Wall -DMPLAB_DSPIC_PORT -DENABLE_QUADROCOPTER -I"../../lib/FreeRTOS" -I"../../lib" -I"../../lib/button" -I"../../lib/adc" -I".." -I"../../lib/i2c" -I"../../lib/bmp085" -I"../../lib/hmc5843" -I"../../lib/max7456" -I"../../lib/matrix" -I"../../lib/quaternion" -I"../../lib/pid" -I"../../lib/pwm_in" -I"../../lib/led" -I"../../lib/ppm_in" -I"../../lib/uart2" -I"../../lib/uart1_queue" -I"../../lib/servo" -I"../../lib/scp1000" -I"../../lib/microcontroller" -I"../../lib/gps" -I"../../lib/dataflash" -mlarge-code -mlarge-data -O1 -MMD -MF "${OBJECTDIR}/_ext/957554017/pid.o.d" -o ${OBJECTDIR}/_ext/957554017/pid.o ../../lib/pid/pid.c  
	
${OBJECTDIR}/_ext/674232159/ppm_in.o: ../../lib/ppm_in/ppm_in.c  nbproject/Makefile-${CND_CONF}.mk
	@${MKDIR} ${OBJECTDIR}/_ext/674232159 
	@${RM} ${OBJECTDIR}/_ext/674232159/ppm_in.o.d 
	@${RM} ${OBJECTDIR}/_ext/674232159/ppm_in.o.ok ${OBJECTDIR}/_ext/674232159/ppm_in.o.err 
	@${FIXDEPS} "${OBJECTDIR}/_ext/674232159/ppm_in.o.d" $(SILENT) -c ${MP_CC} $(MP_EXTRA_CC_PRE) -g -D__DEBUG -D__MPLAB_DEBUGGER_ICD3=1 -omf=elf -x c -c -mcpu=$(MP_PROCESSOR_OPTION) -Wall -DMPLAB_DSPIC_PORT -DENABLE_QUADROCOPTER -I"../../lib/FreeRTOS" -I"../../lib" -I"../../lib/button" -I"../../lib/adc" -I".." -I"../../lib/i2c" -I"../../lib/bmp085" -I"../../lib/hmc5843" -I"../../lib/max7456" -I"../../lib/matrix" -I"../../lib/quaternion" -I"../../lib/pid" -I"../../lib/pwm_in" -I"../../lib/led" -I"../../lib/ppm_in" -I"../../lib/uart2" -I"../../lib/uart1_queue" -I"../../lib/servo" -I"../../lib/scp1000" -I"../../lib/microcontroller" -I"../../lib/gps" -I"../../lib/dataflash" -mlarge-code -mlarge-data -O1 -MMD -MF "${OBJECTDIR}/_ext/674232159/ppm_in.o.d" -o ${OBJECTDIR}/_ext/674232159/ppm_in.o ../../lib/ppm_in/ppm_in.c  
	
${OBJECTDIR}/_ext/667767512/pwm_in.o: ../../lib/pwm_in/pwm_in.c  nbproject/Makefile-${CND_CONF}.mk
	@${MKDIR} ${OBJECTDIR}/_ext/667767512 
	@${RM} ${OBJECTDIR}/_ext/667767512/pwm_in.o.d 
	@${RM} ${OBJECTDIR}/_ext/667767512/pwm_in.o.ok ${OBJECTDIR}/_ext/667767512/pwm_in.o.err 
	@${FIXDEPS} "${OBJECTDIR}/_ext/667767512/pwm_in.o.d" $(SILENT) -c ${MP_CC} $(MP_EXTRA_CC_PRE) -g -D__DEBUG -D__MPLAB_DEBUGGER_ICD3=1 -omf=elf -x c -c -mcpu=$(MP_PROCESSOR_OPTION) -Wall -DMPLAB_DSPIC_PORT -DENABLE_QUADROCOPTER -I"../../lib/FreeRTOS" -I"../../lib" -I"../../lib/button" -I"../../lib/adc" -I".." -I"../../lib/i2c" -I"../../lib/bmp085" -I"../../lib/hmc5843" -I"../../lib/max7456" -I"../../lib/matrix" -I"../../lib/quaternion" -I"../../lib/pid" -I"../../lib/pwm_in" -I"../../lib/led" -I"../../lib/ppm_in" -I"../../lib/uart2" -I"../../lib/uart1_queue" -I"../../lib/servo" -I"../../lib/scp1000" -I"../../lib/microcontroller" -I"../../lib/gps" -I"../../lib/dataflash" -mlarge-code -mlarge-data -O1 -MMD -MF "${OBJECTDIR}/_ext/667767512/pwm_in.o.d" -o ${OBJECTDIR}/_ext/667767512/pwm_in.o ../../lib/pwm_in/pwm_in.c  
	
${OBJECTDIR}/_ext/888521352/quaternion.o: ../../lib/quaternion/quaternion.c  nbproject/Makefile-${CND_CONF}.mk
	@${MKDIR} ${OBJECTDIR}/_ext/888521352 
	@${RM} ${OBJECTDIR}/_ext/888521352/quaternion.o.d 
	@${RM} ${OBJECTDIR}/_ext/888521352/quaternion.o.ok ${OBJECTDIR}/_ext/888521352/quaternion.o.err 
	@${FIXDEPS} "${OBJECTDIR}/_ext/888521352/quaternion.o.d" $(SILENT) -c ${MP_CC} $(MP_EXTRA_CC_PRE) -g -D__DEBUG -D__MPLAB_DEBUGGER_ICD3=1 -omf=elf -x c -c -mcpu=$(MP_PROCESSOR_OPTION) -Wall -DMPLAB_DSPIC_PORT -DENABLE_QUADROCOPTER -I"../../lib/FreeRTOS" -I"../../lib" -I"../../lib/button" -I"../../lib/adc" -I".." -I"../../lib/i2c" -I"../../lib/bmp085" -I"../../lib/hmc5843" -I"../../lib/max7456" -I"../../lib/matrix" -I"../../lib/quaternion" -I"../../lib/pid" -I"../../lib/pwm_in" -I"../../lib/led" -I"../../lib/ppm_in" -I"../../lib/uart2" -I"../../lib/uart1_queue" -I"../../lib/servo" -I"../../lib/scp1000" -I"../../lib/microcontroller" -I"../../lib/gps" -I"../../lib/dataflash" -mlarge-code -mlarge-data -O1 -MMD -MF "${OBJECTDIR}/_ext/888521352/quaternion.o.d" -o ${OBJECTDIR}/_ext/888521352/quaternion.o ../../lib/quaternion/quaternion.c  
	
${OBJECTDIR}/_ext/1429652139/scp1000.o: ../../lib/scp1000/scp1000.c  nbproject/Makefile-${CND_CONF}.mk
	@${MKDIR} ${OBJECTDIR}/_ext/1429652139 
	@${RM} ${OBJECTDIR}/_ext/1429652139/scp1000.o.d 
	@${RM} ${OBJECTDIR}/_ext/1429652139/scp1000.o.ok ${OBJECTDIR}/_ext/1429652139/scp1000.o.err 
	@${FIXDEPS} "${OBJECTDIR}/_ext/1429652139/scp1000.o.d" $(SILENT) -c ${MP_CC} $(MP_EXTRA_CC_PRE) -g -D__DEBUG -D__MPLAB_DEBUGGER_ICD3=1 -omf=elf -x c -c -mcpu=$(MP_PROCESSOR_OPTION) -Wall -DMPLAB_DSPIC_PORT -DENABLE_QUADROCOPTER -I"../../lib/FreeRTOS" -I"../../lib" -I"../../lib/button" -I"../../lib/adc" -I".." -I"../../lib/i2c" -I"../../lib/bmp085" -I"../../lib/hmc5843" -I"../../lib/max7456" -I"../../lib/matrix" -I"../../lib/quaternion" -I"../../lib/pid" -I"../../lib/pwm_in" -I"../../lib/led" -I"../../lib/ppm_in" -I"../../lib/uart2" -I"../../lib/uart1_queue" -I"../../lib/servo" -I"../../lib/scp1000" -I"../../lib/microcontroller" -I"../../lib/gps" -I"../../lib/dataflash" -mlarge-code -mlarge-data -O1 -MMD -MF "${OBJECTDIR}/_ext/1429652139/scp1000.o.d" -o ${OBJECTDIR}/_ext/1429652139/scp1000.o ../../lib/scp1000/scp1000.c  
	
${OBJECTDIR}/_ext/1089077615/servo.o: ../../lib/servo/servo.c  nbproject/Makefile-${CND_CONF}.mk
	@${MKDIR} ${OBJECTDIR}/_ext/1089077615 
	@${RM} ${OBJECTDIR}/_ext/1089077615/servo.o.d 
	@${RM} ${OBJECTDIR}/_ext/1089077615/servo.o.ok ${OBJECTDIR}/_ext/1089077615/servo.o.err 
	@${FIXDEPS} "${OBJECTDIR}/_ext/1089077615/servo.o.d" $(SILENT) -c ${MP_CC} $(MP_EXTRA_CC_PRE) -g -D__DEBUG -D__MPLAB_DEBUGGER_ICD3=1 -omf=elf -x c -c -mcpu=$(MP_PROCESSOR_OPTION) -Wall -DMPLAB_DSPIC_PORT -DENABLE_QUADROCOPTER -I"../../lib/FreeRTOS" -I"../../lib" -I"../../lib/button" -I"../../lib/adc" -I".." -I"../../lib/i2c" -I"../../lib/bmp085" -I"../../lib/hmc5843" -I"../../lib/max7456" -I"../../lib/matrix" -I"../../lib/quaternion" -I"../../lib/pid" -I"../../lib/pwm_in" -I"../../lib/led" -I"../../lib/ppm_in" -I"../../lib/uart2" -I"../../lib/uart1_queue" -I"../../lib/servo" -I"../../lib/scp1000" -I"../../lib/microcontroller" -I"../../lib/gps" -I"../../lib/dataflash" -mlarge-code -mlarge-data -O1 -MMD -MF "${OBJECTDIR}/_ext/1089077615/servo.o.d" -o ${OBJECTDIR}/_ext/1089077615/servo.o ../../lib/servo/servo.c  
	
${OBJECTDIR}/_ext/1591518261/uart1_queue.o: ../../lib/uart1_queue/uart1_queue.c  nbproject/Makefile-${CND_CONF}.mk
	@${MKDIR} ${OBJECTDIR}/_ext/1591518261 
	@${RM} ${OBJECTDIR}/_ext/1591518261/uart1_queue.o.d 
	@${RM} ${OBJECTDIR}/_ext/1591518261/uart1_queue.o.ok ${OBJECTDIR}/_ext/1591518261/uart1_queue.o.err 
	@${FIXDEPS} "${OBJECTDIR}/_ext/1591518261/uart1_queue.o.d" $(SILENT) -c ${MP_CC} $(MP_EXTRA_CC_PRE) -g -D__DEBUG -D__MPLAB_DEBUGGER_ICD3=1 -omf=elf -x c -c -mcpu=$(MP_PROCESSOR_OPTION) -Wall -DMPLAB_DSPIC_PORT -DENABLE_QUADROCOPTER -I"../../lib/FreeRTOS" -I"../../lib" -I"../../lib/button" -I"../../lib/adc" -I".." -I"../../lib/i2c" -I"../../lib/bmp085" -I"../../lib/hmc5843" -I"../../lib/max7456" -I"../../lib/matrix" -I"../../lib/quaternion" -I"../../lib/pid" -I"../../lib/pwm_in" -I"../../lib/led" -I"../../lib/ppm_in" -I"../../lib/uart2" -I"../../lib/uart1_queue" -I"../../lib/servo" -I"../../lib/scp1000" -I"../../lib/microcontroller" -I"../../lib/gps" -I"../../lib/dataflash" -mlarge-code -mlarge-data -O1 -MMD -MF "${OBJECTDIR}/_ext/1591518261/uart1_queue.o.d" -o ${OBJECTDIR}/_ext/1591518261/uart1_queue.o ../../lib/uart1_queue/uart1_queue.c  
	
${OBJECTDIR}/_ext/1090805370/uart2.o: ../../lib/uart2/uart2.c  nbproject/Makefile-${CND_CONF}.mk
	@${MKDIR} ${OBJECTDIR}/_ext/1090805370 
	@${RM} ${OBJECTDIR}/_ext/1090805370/uart2.o.d 
	@${RM} ${OBJECTDIR}/_ext/1090805370/uart2.o.ok ${OBJECTDIR}/_ext/1090805370/uart2.o.err 
	@${FIXDEPS} "${OBJECTDIR}/_ext/1090805370/uart2.o.d" $(SILENT) -c ${MP_CC} $(MP_EXTRA_CC_PRE) -g -D__DEBUG -D__MPLAB_DEBUGGER_ICD3=1 -omf=elf -x c -c -mcpu=$(MP_PROCESSOR_OPTION) -Wall -DMPLAB_DSPIC_PORT -DENABLE_QUADROCOPTER -I"../../lib/FreeRTOS" -I"../../lib" -I"../../lib/button" -I"../../lib/adc" -I".." -I"../../lib/i2c" -I"../../lib/bmp085" -I"../../lib/hmc5843" -I"../../lib/max7456" -I"../../lib/matrix" -I"../../lib/quaternion" -I"../../lib/pid" -I"../../lib/pwm_in" -I"../../lib/led" -I"../../lib/ppm_in" -I"../../lib/uart2" -I"../../lib/uart1_queue" -I"../../lib/servo" -I"../../lib/scp1000" -I"../../lib/microcontroller" -I"../../lib/gps" -I"../../lib/dataflash" -mlarge-code -mlarge-data -O1 -MMD -MF "${OBJECTDIR}/_ext/1090805370/uart2.o.d" -o ${OBJECTDIR}/_ext/1090805370/uart2.o ../../lib/uart2/uart2.c  
	
${OBJECTDIR}/_ext/1472/ahrs_kalman_2x3.o: ../ahrs_kalman_2x3.c  nbproject/Makefile-${CND_CONF}.mk
	@${MKDIR} ${OBJECTDIR}/_ext/1472 
	@${RM} ${OBJECTDIR}/_ext/1472/ahrs_kalman_2x3.o.d 
	@${RM} ${OBJECTDIR}/_ext/1472/ahrs_kalman_2x3.o.ok ${OBJECTDIR}/_ext/1472/ahrs_kalman_2x3.o.err 
	@${FIXDEPS} "${OBJECTDIR}/_ext/1472/ahrs_kalman_2x3.o.d" $(SILENT) -c ${MP_CC} $(MP_EXTRA_CC_PRE) -g -D__DEBUG -D__MPLAB_DEBUGGER_ICD3=1 -omf=elf -x c -c -mcpu=$(MP_PROCESSOR_OPTION) -Wall -DMPLAB_DSPIC_PORT -DENABLE_QUADROCOPTER -I"../../lib/FreeRTOS" -I"../../lib" -I"../../lib/button" -I"../../lib/adc" -I".." -I"../../lib/i2c" -I"../../lib/bmp085" -I"../../lib/hmc5843" -I"../../lib/max7456" -I"../../lib/matrix" -I"../../lib/quaternion" -I"../../lib/pid" -I"../../lib/pwm_in" -I"../../lib/led" -I"../../lib/ppm_in" -I"../../lib/uart2" -I"../../lib/uart1_queue" -I"../../lib/servo" -I"../../lib/scp1000" -I"../../lib/microcontroller" -I"../../lib/gps" -I"../../lib/dataflash" -mlarge-code -mlarge-data -O1 -MMD -MF "${OBJECTDIR}/_ext/1472/ahrs_kalman_2x3.o.d" -o ${OBJECTDIR}/_ext/1472/ahrs_kalman_2x3.o ../ahrs_kalman_2x3.c  
	
${OBJECTDIR}/_ext/1472/alarms.o: ../alarms.c  nbproject/Makefile-${CND_CONF}.mk
	@${MKDIR} ${OBJECTDIR}/_ext/1472 
	@${RM} ${OBJECTDIR}/_ext/1472/alarms.o.d 
	@${RM} ${OBJECTDIR}/_ext/1472/alarms.o.ok ${OBJECTDIR}/_ext/1472/alarms.o.err 
	@${FIXDEPS} "${OBJECTDIR}/_ext/1472/alarms.o.d" $(SILENT) -c ${MP_CC} $(MP_EXTRA_CC_PRE) -g -D__DEBUG -D__MPLAB_DEBUGGER_ICD3=1 -omf=elf -x c -c -mcpu=$(MP_PROCESSOR_OPTION) -Wall -DMPLAB_DSPIC_PORT -DENABLE_QUADROCOPTER -I"../../lib/FreeRTOS" -I"../../lib" -I"../../lib/button" -I"../../lib/adc" -I".." -I"../../lib/i2c" -I"../../lib/bmp085" -I"../../lib/hmc5843" -I"../../lib/max7456" -I"../../lib/matrix" -I"../../lib/quaternion" -I"../../lib/pid" -I"../../lib/pwm_in" -I"../../lib/led" -I"../../lib/ppm_in" -I"../../lib/uart2" -I"../../lib/uart1_queue" -I"../../lib/servo" -I"../../lib/scp1000" -I"../../lib/microcontroller" -I"../../lib/gps" -I"../../lib/dataflash" -mlarge-code -mlarge-data -O1 -MMD -MF "${OBJECTDIR}/_ext/1472/alarms.o.d" -o ${OBJECTDIR}/_ext/1472/alarms.o ../alarms.c  
	
${OBJECTDIR}/_ext/1472/communication_csv.o: ../communication_csv.c  nbproject/Makefile-${CND_CONF}.mk
	@${MKDIR} ${OBJECTDIR}/_ext/1472 
	@${RM} ${OBJECTDIR}/_ext/1472/communication_csv.o.d 
	@${RM} ${OBJECTDIR}/_ext/1472/communication_csv.o.ok ${OBJECTDIR}/_ext/1472/communication_csv.o.err 
	@${FIXDEPS} "${OBJECTDIR}/_ext/1472/communication_csv.o.d" $(SILENT) -c ${MP_CC} $(MP_EXTRA_CC_PRE) -g -D__DEBUG -D__MPLAB_DEBUGGER_ICD3=1 -omf=elf -x c -c -mcpu=$(MP_PROCESSOR_OPTION) -Wall -DMPLAB_DSPIC_PORT -DENABLE_QUADROCOPTER -I"../../lib/FreeRTOS" -I"../../lib" -I"../../lib/button" -I"../../lib/adc" -I".." -I"../../lib/i2c" -I"../../lib/bmp085" -I"../../lib/hmc5843" -I"../../lib/max7456" -I"../../lib/matrix" -I"../../lib/quaternion" -I"../../lib/pid" -I"../../lib/pwm_in" -I"../../lib/led" -I"../../lib/ppm_in" -I"../../lib/uart2" -I"../../lib/uart1_queue" -I"../../lib/servo" -I"../../lib/scp1000" -I"../../lib/microcontroller" -I"../../lib/gps" -I"../../lib/dataflash" -mlarge-code -mlarge-data -O1 -MMD -MF "${OBJECTDIR}/_ext/1472/communication_csv.o.d" -o ${OBJECTDIR}/_ext/1472/communication_csv.o ../communication_csv.c  
	
${OBJECTDIR}/_ext/1472/configuration.o: ../configuration.c  nbproject/Makefile-${CND_CONF}.mk
	@${MKDIR} ${OBJECTDIR}/_ext/1472 
	@${RM} ${OBJECTDIR}/_ext/1472/configuration.o.d 
	@${RM} ${OBJECTDIR}/_ext/1472/configuration.o.ok ${OBJECTDIR}/_ext/1472/configuration.o.err 
	@${FIXDEPS} "${OBJECTDIR}/_ext/1472/configuration.o.d" $(SILENT) -c ${MP_CC} $(MP_EXTRA_CC_PRE) -g -D__DEBUG -D__MPLAB_DEBUGGER_ICD3=1 -omf=elf -x c -c -mcpu=$(MP_PROCESSOR_OPTION) -Wall -DMPLAB_DSPIC_PORT -DENABLE_QUADROCOPTER -I"../../lib/FreeRTOS" -I"../../lib" -I"../../lib/button" -I"../../lib/adc" -I".." -I"../../lib/i2c" -I"../../lib/bmp085" -I"../../lib/hmc5843" -I"../../lib/max7456" -I"../../lib/matrix" -I"../../lib/quaternion" -I"../../lib/pid" -I"../../lib/pwm_in" -I"../../lib/led" -I"../../lib/ppm_in" -I"../../lib/uart2" -I"../../lib/uart1_queue" -I"../../lib/servo" -I"../../lib/scp1000" -I"../../lib/microcontroller" -I"../../lib/gps" -I"../../lib/dataflash" -mlarge-code -mlarge-data -O1 -MMD -MF "${OBJECTDIR}/_ext/1472/configuration.o.d" -o ${OBJECTDIR}/_ext/1472/configuration.o ../configuration.c  
	
${OBJECTDIR}/_ext/1472/control.o: ../control.c  nbproject/Makefile-${CND_CONF}.mk
	@${MKDIR} ${OBJECTDIR}/_ext/1472 
	@${RM} ${OBJECTDIR}/_ext/1472/control.o.d 
	@${RM} ${OBJECTDIR}/_ext/1472/control.o.ok ${OBJECTDIR}/_ext/1472/control.o.err 
	@${FIXDEPS} "${OBJECTDIR}/_ext/1472/control.o.d" $(SILENT) -c ${MP_CC} $(MP_EXTRA_CC_PRE) -g -D__DEBUG -D__MPLAB_DEBUGGER_ICD3=1 -omf=elf -x c -c -mcpu=$(MP_PROCESSOR_OPTION) -Wall -DMPLAB_DSPIC_PORT -DENABLE_QUADROCOPTER -I"../../lib/FreeRTOS" -I"../../lib" -I"../../lib/button" -I"../../lib/adc" -I".." -I"../../lib/i2c" -I"../../lib/bmp085" -I"../../lib/hmc5843" -I"../../lib/max7456" -I"../../lib/matrix" -I"../../lib/quaternion" -I"../../lib/pid" -I"../../lib/pwm_in" -I"../../lib/led" -I"../../lib/ppm_in" -I"../../lib/uart2" -I"../../lib/uart1_queue" -I"../../lib/servo" -I"../../lib/scp1000" -I"../../lib/microcontroller" -I"../../lib/gps" -I"../../lib/dataflash" -mlarge-code -mlarge-data -O1 -MMD -MF "${OBJECTDIR}/_ext/1472/control.o.d" -o ${OBJECTDIR}/_ext/1472/control.o ../control.c  
	
${OBJECTDIR}/_ext/1472/datalogger.o: ../datalogger.c  nbproject/Makefile-${CND_CONF}.mk
	@${MKDIR} ${OBJECTDIR}/_ext/1472 
	@${RM} ${OBJECTDIR}/_ext/1472/datalogger.o.d 
	@${RM} ${OBJECTDIR}/_ext/1472/datalogger.o.ok ${OBJECTDIR}/_ext/1472/datalogger.o.err 
	@${FIXDEPS} "${OBJECTDIR}/_ext/1472/datalogger.o.d" $(SILENT) -c ${MP_CC} $(MP_EXTRA_CC_PRE) -g -D__DEBUG -D__MPLAB_DEBUGGER_ICD3=1 -omf=elf -x c -c -mcpu=$(MP_PROCESSOR_OPTION) -Wall -DMPLAB_DSPIC_PORT -DENABLE_QUADROCOPTER -I"../../lib/FreeRTOS" -I"../../lib" -I"../../lib/button" -I"../../lib/adc" -I".." -I"../../lib/i2c" -I"../../lib/bmp085" -I"../../lib/hmc5843" -I"../../lib/max7456" -I"../../lib/matrix" -I"../../lib/quaternion" -I"../../lib/pid" -I"../../lib/pwm_in" -I"../../lib/led" -I"../../lib/ppm_in" -I"../../lib/uart2" -I"../../lib/uart1_queue" -I"../../lib/servo" -I"../../lib/scp1000" -I"../../lib/microcontroller" -I"../../lib/gps" -I"../../lib/dataflash" -mlarge-code -mlarge-data -O1 -MMD -MF "${OBJECTDIR}/_ext/1472/datalogger.o.d" -o ${OBJECTDIR}/_ext/1472/datalogger.o ../datalogger.c  
	
${OBJECTDIR}/_ext/1472/gluonscript.o: ../gluonscript.c  nbproject/Makefile-${CND_CONF}.mk
	@${MKDIR} ${OBJECTDIR}/_ext/1472 
	@${RM} ${OBJECTDIR}/_ext/1472/gluonscript.o.d 
	@${RM} ${OBJECTDIR}/_ext/1472/gluonscript.o.ok ${OBJECTDIR}/_ext/1472/gluonscript.o.err 
	@${FIXDEPS} "${OBJECTDIR}/_ext/1472/gluonscript.o.d" $(SILENT) -c ${MP_CC} $(MP_EXTRA_CC_PRE) -g -D__DEBUG -D__MPLAB_DEBUGGER_ICD3=1 -omf=elf -x c -c -mcpu=$(MP_PROCESSOR_OPTION) -Wall -DMPLAB_DSPIC_PORT -DENABLE_QUADROCOPTER -I"../../lib/FreeRTOS" -I"../../lib" -I"../../lib/button" -I"../../lib/adc" -I".." -I"../../lib/i2c" -I"../../lib/bmp085" -I"../../lib/hmc5843" -I"../../lib/max7456" -I"../../lib/matrix" -I"../../lib/quaternion" -I"../../lib/pid" -I"../../lib/pwm_in" -I"../../lib/led" -I"../../lib/ppm_in" -I"../../lib/uart2" -I"../../lib/uart1_queue" -I"../../lib/servo" -I"../../lib/scp1000" -I"../../lib/microcontroller" -I"../../lib/gps" -I"../../lib/dataflash" -mlarge-code -mlarge-data -O1 -MMD -MF "${OBJECTDIR}/_ext/1472/gluonscript.o.d" -o ${OBJECTDIR}/_ext/1472/gluonscript.o ../gluonscript.c  
	
${OBJECTDIR}/_ext/1472/navigation.o: ../navigation.c  nbproject/Makefile-${CND_CONF}.mk
	@${MKDIR} ${OBJECTDIR}/_ext/1472 
	@${RM} ${OBJECTDIR}/_ext/1472/navigation.o.d 
	@${RM} ${OBJECTDIR}/_ext/1472/navigation.o.ok ${OBJECTDIR}/_ext/1472/navigation.o.err 
	@${FIXDEPS} "${OBJECTDIR}/_ext/1472/navigation.o.d" $(SILENT) -c ${MP_CC} $(MP_EXTRA_CC_PRE) -g -D__DEBUG -D__MPLAB_DEBUGGER_ICD3=1 -omf=elf -x c -c -mcpu=$(MP_PROCESSOR_OPTION) -Wall -DMPLAB_DSPIC_PORT -DENABLE_QUADROCOPTER -I"../../lib/FreeRTOS" -I"../../lib" -I"../../lib/button" -I"../../lib/adc" -I".." -I"../../lib/i2c" -I"../../lib/bmp085" -I"../../lib/hmc5843" -I"../../lib/max7456" -I"../../lib/matrix" -I"../../lib/quaternion" -I"../../lib/pid" -I"../../lib/pwm_in" -I"../../lib/led" -I"../../lib/ppm_in" -I"../../lib/uart2" -I"../../lib/uart1_queue" -I"../../lib/servo" -I"../../lib/scp1000" -I"../../lib/microcontroller" -I"../../lib/gps" -I"../../lib/dataflash" -mlarge-code -mlarge-data -O1 -MMD -MF "${OBJECTDIR}/_ext/1472/navigation.o.d" -o ${OBJECTDIR}/_ext/1472/navigation.o ../navigation.c  
	
${OBJECTDIR}/_ext/1472/osd_max7456.o: ../osd_max7456.c  nbproject/Makefile-${CND_CONF}.mk
	@${MKDIR} ${OBJECTDIR}/_ext/1472 
	@${RM} ${OBJECTDIR}/_ext/1472/osd_max7456.o.d 
	@${RM} ${OBJECTDIR}/_ext/1472/osd_max7456.o.ok ${OBJECTDIR}/_ext/1472/osd_max7456.o.err 
	@${FIXDEPS} "${OBJECTDIR}/_ext/1472/osd_max7456.o.d" $(SILENT) -c ${MP_CC} $(MP_EXTRA_CC_PRE) -g -D__DEBUG -D__MPLAB_DEBUGGER_ICD3=1 -omf=elf -x c -c -mcpu=$(MP_PROCESSOR_OPTION) -Wall -DMPLAB_DSPIC_PORT -DENABLE_QUADROCOPTER -I"../../lib/FreeRTOS" -I"../../lib" -I"../../lib/button" -I"../../lib/adc" -I".." -I"../../lib/i2c" -I"../../lib/bmp085" -I"../../lib/hmc5843" -I"../../lib/max7456" -I"../../lib/matrix" -I"../../lib/quaternion" -I"../../lib/pid" -I"../../lib/pwm_in" -I"../../lib/led" -I"../../lib/ppm_in" -I"../../lib/uart2" -I"../../lib/uart1_queue" -I"../../lib/servo" -I"../../lib/scp1000" -I"../../lib/microcontroller" -I"../../lib/gps" -I"../../lib/dataflash" -mlarge-code -mlarge-data -O1 -MMD -MF "${OBJECTDIR}/_ext/1472/osd_max7456.o.d" -o ${OBJECTDIR}/_ext/1472/osd_max7456.o ../osd_max7456.c  
	
${OBJECTDIR}/_ext/1472/rtos_pilot.o: ../rtos_pilot.c  nbproject/Makefile-${CND_CONF}.mk
	@${MKDIR} ${OBJECTDIR}/_ext/1472 
	@${RM} ${OBJECTDIR}/_ext/1472/rtos_pilot.o.d 
	@${RM} ${OBJECTDIR}/_ext/1472/rtos_pilot.o.ok ${OBJECTDIR}/_ext/1472/rtos_pilot.o.err 
	@${FIXDEPS} "${OBJECTDIR}/_ext/1472/rtos_pilot.o.d" $(SILENT) -c ${MP_CC} $(MP_EXTRA_CC_PRE) -g -D__DEBUG -D__MPLAB_DEBUGGER_ICD3=1 -omf=elf -x c -c -mcpu=$(MP_PROCESSOR_OPTION) -Wall -DMPLAB_DSPIC_PORT -DENABLE_QUADROCOPTER -I"../../lib/FreeRTOS" -I"../../lib" -I"../../lib/button" -I"../../lib/adc" -I".." -I"../../lib/i2c" -I"../../lib/bmp085" -I"../../lib/hmc5843" -I"../../lib/max7456" -I"../../lib/matrix" -I"../../lib/quaternion" -I"../../lib/pid" -I"../../lib/pwm_in" -I"../../lib/led" -I"../../lib/ppm_in" -I"../../lib/uart2" -I"../../lib/uart1_queue" -I"../../lib/servo" -I"../../lib/scp1000" -I"../../lib/microcontroller" -I"../../lib/gps" -I"../../lib/dataflash" -mlarge-code -mlarge-data -O1 -MMD -MF "${OBJECTDIR}/_ext/1472/rtos_pilot.o.d" -o ${OBJECTDIR}/_ext/1472/rtos_pilot.o ../rtos_pilot.c  
	
${OBJECTDIR}/_ext/1472/sensors.o: ../sensors.c  nbproject/Makefile-${CND_CONF}.mk
	@${MKDIR} ${OBJECTDIR}/_ext/1472 
	@${RM} ${OBJECTDIR}/_ext/1472/sensors.o.d 
	@${RM} ${OBJECTDIR}/_ext/1472/sensors.o.ok ${OBJECTDIR}/_ext/1472/sensors.o.err 
	@${FIXDEPS} "${OBJECTDIR}/_ext/1472/sensors.o.d" $(SILENT) -c ${MP_CC} $(MP_EXTRA_CC_PRE) -g -D__DEBUG -D__MPLAB_DEBUGGER_ICD3=1 -omf=elf -x c -c -mcpu=$(MP_PROCESSOR_OPTION) -Wall -DMPLAB_DSPIC_PORT -DENABLE_QUADROCOPTER -I"../../lib/FreeRTOS" -I"../../lib" -I"../../lib/button" -I"../../lib/adc" -I".." -I"../../lib/i2c" -I"../../lib/bmp085" -I"../../lib/hmc5843" -I"../../lib/max7456" -I"../../lib/matrix" -I"../../lib/quaternion" -I"../../lib/pid" -I"../../lib/pwm_in" -I"../../lib/led" -I"../../lib/ppm_in" -I"../../lib/uart2" -I"../../lib/uart1_queue" -I"../../lib/servo" -I"../../lib/scp1000" -I"../../lib/microcontroller" -I"../../lib/gps" -I"../../lib/dataflash" -mlarge-code -mlarge-data -O1 -MMD -MF "${OBJECTDIR}/_ext/1472/sensors.o.d" -o ${OBJECTDIR}/_ext/1472/sensors.o ../sensors.c  
	
${OBJECTDIR}/_ext/1472/trigger.o: ../trigger.c  nbproject/Makefile-${CND_CONF}.mk
	@${MKDIR} ${OBJECTDIR}/_ext/1472 
	@${RM} ${OBJECTDIR}/_ext/1472/trigger.o.d 
	@${RM} ${OBJECTDIR}/_ext/1472/trigger.o.ok ${OBJECTDIR}/_ext/1472/trigger.o.err 
	@${FIXDEPS} "${OBJECTDIR}/_ext/1472/trigger.o.d" $(SILENT) -c ${MP_CC} $(MP_EXTRA_CC_PRE) -g -D__DEBUG -D__MPLAB_DEBUGGER_ICD3=1 -omf=elf -x c -c -mcpu=$(MP_PROCESSOR_OPTION) -Wall -DMPLAB_DSPIC_PORT -DENABLE_QUADROCOPTER -I"../../lib/FreeRTOS" -I"../../lib" -I"../../lib/button" -I"../../lib/adc" -I".." -I"../../lib/i2c" -I"../../lib/bmp085" -I"../../lib/hmc5843" -I"../../lib/max7456" -I"../../lib/matrix" -I"../../lib/quaternion" -I"../../lib/pid" -I"../../lib/pwm_in" -I"../../lib/led" -I"../../lib/ppm_in" -I"../../lib/uart2" -I"../../lib/uart1_queue" -I"../../lib/servo" -I"../../lib/scp1000" -I"../../lib/microcontroller" -I"../../lib/gps" -I"../../lib/dataflash" -mlarge-code -mlarge-data -O1 -MMD -MF "${OBJECTDIR}/_ext/1472/trigger.o.d" -o ${OBJECTDIR}/_ext/1472/trigger.o ../trigger.c  
	
else
${OBJECTDIR}/_ext/1970174492/croutine.o: ../../lib/FreeRTOS/croutine.c  nbproject/Makefile-${CND_CONF}.mk
	@${MKDIR} ${OBJECTDIR}/_ext/1970174492 
	@${RM} ${OBJECTDIR}/_ext/1970174492/croutine.o.d 
	@${RM} ${OBJECTDIR}/_ext/1970174492/croutine.o.ok ${OBJECTDIR}/_ext/1970174492/croutine.o.err 
	@${FIXDEPS} "${OBJECTDIR}/_ext/1970174492/croutine.o.d" $(SILENT) -c ${MP_CC} $(MP_EXTRA_CC_PRE)  -g -omf=elf -x c -c -mcpu=$(MP_PROCESSOR_OPTION) -Wall -DMPLAB_DSPIC_PORT -DENABLE_QUADROCOPTER -I"../../lib/FreeRTOS" -I"../../lib" -I"../../lib/button" -I"../../lib/adc" -I".." -I"../../lib/i2c" -I"../../lib/bmp085" -I"../../lib/hmc5843" -I"../../lib/max7456" -I"../../lib/matrix" -I"../../lib/quaternion" -I"../../lib/pid" -I"../../lib/pwm_in" -I"../../lib/led" -I"../../lib/ppm_in" -I"../../lib/uart2" -I"../../lib/uart1_queue" -I"../../lib/servo" -I"../../lib/scp1000" -I"../../lib/microcontroller" -I"../../lib/gps" -I"../../lib/dataflash" -mlarge-code -mlarge-data -O1 -MMD -MF "${OBJECTDIR}/_ext/1970174492/croutine.o.d" -o ${OBJECTDIR}/_ext/1970174492/croutine.o ../../lib/FreeRTOS/croutine.c  
	
${OBJECTDIR}/_ext/1970174492/heap_1.o: ../../lib/FreeRTOS/heap_1.c  nbproject/Makefile-${CND_CONF}.mk
	@${MKDIR} ${OBJECTDIR}/_ext/1970174492 
	@${RM} ${OBJECTDIR}/_ext/1970174492/heap_1.o.d 
	@${RM} ${OBJECTDIR}/_ext/1970174492/heap_1.o.ok ${OBJECTDIR}/_ext/1970174492/heap_1.o.err 
	@${FIXDEPS} "${OBJECTDIR}/_ext/1970174492/heap_1.o.d" $(SILENT) -c ${MP_CC} $(MP_EXTRA_CC_PRE)  -g -omf=elf -x c -c -mcpu=$(MP_PROCESSOR_OPTION) -Wall -DMPLAB_DSPIC_PORT -DENABLE_QUADROCOPTER -I"../../lib/FreeRTOS" -I"../../lib" -I"../../lib/button" -I"../../lib/adc" -I".." -I"../../lib/i2c" -I"../../lib/bmp085" -I"../../lib/hmc5843" -I"../../lib/max7456" -I"../../lib/matrix" -I"../../lib/quaternion" -I"../../lib/pid" -I"../../lib/pwm_in" -I"../../lib/led" -I"../../lib/ppm_in" -I"../../lib/uart2" -I"../../lib/uart1_queue" -I"../../lib/servo" -I"../../lib/scp1000" -I"../../lib/microcontroller" -I"../../lib/gps" -I"../../lib/dataflash" -mlarge-code -mlarge-data -O1 -MMD -MF "${OBJECTDIR}/_ext/1970174492/heap_1.o.d" -o ${OBJECTDIR}/_ext/1970174492/heap_1.o ../../lib/FreeRTOS/heap_1.c  
	
${OBJECTDIR}/_ext/1970174492/list.o: ../../lib/FreeRTOS/list.c  nbproject/Makefile-${CND_CONF}.mk
	@${MKDIR} ${OBJECTDIR}/_ext/1970174492 
	@${RM} ${OBJECTDIR}/_ext/1970174492/list.o.d 
	@${RM} ${OBJECTDIR}/_ext/1970174492/list.o.ok ${OBJECTDIR}/_ext/1970174492/list.o.err 
	@${FIXDEPS} "${OBJECTDIR}/_ext/1970174492/list.o.d" $(SILENT) -c ${MP_CC} $(MP_EXTRA_CC_PRE)  -g -omf=elf -x c -c -mcpu=$(MP_PROCESSOR_OPTION) -Wall -DMPLAB_DSPIC_PORT -DENABLE_QUADROCOPTER -I"../../lib/FreeRTOS" -I"../../lib" -I"../../lib/button" -I"../../lib/adc" -I".." -I"../../lib/i2c" -I"../../lib/bmp085" -I"../../lib/hmc5843" -I"../../lib/max7456" -I"../../lib/matrix" -I"../../lib/quaternion" -I"../../lib/pid" -I"../../lib/pwm_in" -I"../../lib/led" -I"../../lib/ppm_in" -I"../../lib/uart2" -I"../../lib/uart1_queue" -I"../../lib/servo" -I"../../lib/scp1000" -I"../../lib/microcontroller" -I"../../lib/gps" -I"../../lib/dataflash" -mlarge-code -mlarge-data -O1 -MMD -MF "${OBJECTDIR}/_ext/1970174492/list.o.d" -o ${OBJECTDIR}/_ext/1970174492/list.o ../../lib/FreeRTOS/list.c  
	
${OBJECTDIR}/_ext/1970174492/port.o: ../../lib/FreeRTOS/port.c  nbproject/Makefile-${CND_CONF}.mk
	@${MKDIR} ${OBJECTDIR}/_ext/1970174492 
	@${RM} ${OBJECTDIR}/_ext/1970174492/port.o.d 
	@${RM} ${OBJECTDIR}/_ext/1970174492/port.o.ok ${OBJECTDIR}/_ext/1970174492/port.o.err 
	@${FIXDEPS} "${OBJECTDIR}/_ext/1970174492/port.o.d" $(SILENT) -c ${MP_CC} $(MP_EXTRA_CC_PRE)  -g -omf=elf -x c -c -mcpu=$(MP_PROCESSOR_OPTION) -Wall -DMPLAB_DSPIC_PORT -DENABLE_QUADROCOPTER -I"../../lib/FreeRTOS" -I"../../lib" -I"../../lib/button" -I"../../lib/adc" -I".." -I"../../lib/i2c" -I"../../lib/bmp085" -I"../../lib/hmc5843" -I"../../lib/max7456" -I"../../lib/matrix" -I"../../lib/quaternion" -I"../../lib/pid" -I"../../lib/pwm_in" -I"../../lib/led" -I"../../lib/ppm_in" -I"../../lib/uart2" -I"../../lib/uart1_queue" -I"../../lib/servo" -I"../../lib/scp1000" -I"../../lib/microcontroller" -I"../../lib/gps" -I"../../lib/dataflash" -mlarge-code -mlarge-data -O1 -MMD -MF "${OBJECTDIR}/_ext/1970174492/port.o.d" -o ${OBJECTDIR}/_ext/1970174492/port.o ../../lib/FreeRTOS/port.c  
	
${OBJECTDIR}/_ext/1970174492/queue.o: ../../lib/FreeRTOS/queue.c  nbproject/Makefile-${CND_CONF}.mk
	@${MKDIR} ${OBJECTDIR}/_ext/1970174492 
	@${RM} ${OBJECTDIR}/_ext/1970174492/queue.o.d 
	@${RM} ${OBJECTDIR}/_ext/1970174492/queue.o.ok ${OBJECTDIR}/_ext/1970174492/queue.o.err 
	@${FIXDEPS} "${OBJECTDIR}/_ext/1970174492/queue.o.d" $(SILENT) -c ${MP_CC} $(MP_EXTRA_CC_PRE)  -g -omf=elf -x c -c -mcpu=$(MP_PROCESSOR_OPTION) -Wall -DMPLAB_DSPIC_PORT -DENABLE_QUADROCOPTER -I"../../lib/FreeRTOS" -I"../../lib" -I"../../lib/button" -I"../../lib/adc" -I".." -I"../../lib/i2c" -I"../../lib/bmp085" -I"../../lib/hmc5843" -I"../../lib/max7456" -I"../../lib/matrix" -I"../../lib/quaternion" -I"../../lib/pid" -I"../../lib/pwm_in" -I"../../lib/led" -I"../../lib/ppm_in" -I"../../lib/uart2" -I"../../lib/uart1_queue" -I"../../lib/servo" -I"../../lib/scp1000" -I"../../lib/microcontroller" -I"../../lib/gps" -I"../../lib/dataflash" -mlarge-code -mlarge-data -O1 -MMD -MF "${OBJECTDIR}/_ext/1970174492/queue.o.d" -o ${OBJECTDIR}/_ext/1970174492/queue.o ../../lib/FreeRTOS/queue.c  
	
${OBJECTDIR}/_ext/1970174492/tasks.o: ../../lib/FreeRTOS/tasks.c  nbproject/Makefile-${CND_CONF}.mk
	@${MKDIR} ${OBJECTDIR}/_ext/1970174492 
	@${RM} ${OBJECTDIR}/_ext/1970174492/tasks.o.d 
	@${RM} ${OBJECTDIR}/_ext/1970174492/tasks.o.ok ${OBJECTDIR}/_ext/1970174492/tasks.o.err 
	@${FIXDEPS} "${OBJECTDIR}/_ext/1970174492/tasks.o.d" $(SILENT) -c ${MP_CC} $(MP_EXTRA_CC_PRE)  -g -omf=elf -x c -c -mcpu=$(MP_PROCESSOR_OPTION) -Wall -DMPLAB_DSPIC_PORT -DENABLE_QUADROCOPTER -I"../../lib/FreeRTOS" -I"../../lib" -I"../../lib/button" -I"../../lib/adc" -I".." -I"../../lib/i2c" -I"../../lib/bmp085" -I"../../lib/hmc5843" -I"../../lib/max7456" -I"../../lib/matrix" -I"../../lib/quaternion" -I"../../lib/pid" -I"../../lib/pwm_in" -I"../../lib/led" -I"../../lib/ppm_in" -I"../../lib/uart2" -I"../../lib/uart1_queue" -I"../../lib/servo" -I"../../lib/scp1000" -I"../../lib/microcontroller" -I"../../lib/gps" -I"../../lib/dataflash" -mlarge-code -mlarge-data -O1 -MMD -MF "${OBJECTDIR}/_ext/1970174492/tasks.o.d" -o ${OBJECTDIR}/_ext/1970174492/tasks.o ../../lib/FreeRTOS/tasks.c  
	
${OBJECTDIR}/_ext/957539446/adc.o: ../../lib/adc/adc.c  nbproject/Makefile-${CND_CONF}.mk
	@${MKDIR} ${OBJECTDIR}/_ext/957539446 
	@${RM} ${OBJECTDIR}/_ext/957539446/adc.o.d 
	@${RM} ${OBJECTDIR}/_ext/957539446/adc.o.ok ${OBJECTDIR}/_ext/957539446/adc.o.err 
	@${FIXDEPS} "${OBJECTDIR}/_ext/957539446/adc.o.d" $(SILENT) -c ${MP_CC} $(MP_EXTRA_CC_PRE)  -g -omf=elf -x c -c -mcpu=$(MP_PROCESSOR_OPTION) -Wall -DMPLAB_DSPIC_PORT -DENABLE_QUADROCOPTER -I"../../lib/FreeRTOS" -I"../../lib" -I"../../lib/button" -I"../../lib/adc" -I".." -I"../../lib/i2c" -I"../../lib/bmp085" -I"../../lib/hmc5843" -I"../../lib/max7456" -I"../../lib/matrix" -I"../../lib/quaternion" -I"../../lib/pid" -I"../../lib/pwm_in" -I"../../lib/led" -I"../../lib/ppm_in" -I"../../lib/uart2" -I"../../lib/uart1_queue" -I"../../lib/servo" -I"../../lib/scp1000" -I"../../lib/microcontroller" -I"../../lib/gps" -I"../../lib/dataflash" -mlarge-code -mlarge-data -O1 -MMD -MF "${OBJECTDIR}/_ext/957539446/adc.o.d" -o ${OBJECTDIR}/_ext/957539446/adc.o ../../lib/adc/adc.c  
	
${OBJECTDIR}/_ext/1077768206/bmp085.o: ../../lib/bmp085/bmp085.c  nbproject/Makefile-${CND_CONF}.mk
	@${MKDIR} ${OBJECTDIR}/_ext/1077768206 
	@${RM} ${OBJECTDIR}/_ext/1077768206/bmp085.o.d 
	@${RM} ${OBJECTDIR}/_ext/1077768206/bmp085.o.ok ${OBJECTDIR}/_ext/1077768206/bmp085.o.err 
	@${FIXDEPS} "${OBJECTDIR}/_ext/1077768206/bmp085.o.d" $(SILENT) -c ${MP_CC} $(MP_EXTRA_CC_PRE)  -g -omf=elf -x c -c -mcpu=$(MP_PROCESSOR_OPTION) -Wall -DMPLAB_DSPIC_PORT -DENABLE_QUADROCOPTER -I"../../lib/FreeRTOS" -I"../../lib" -I"../../lib/button" -I"../../lib/adc" -I".." -I"../../lib/i2c" -I"../../lib/bmp085" -I"../../lib/hmc5843" -I"../../lib/max7456" -I"../../lib/matrix" -I"../../lib/quaternion" -I"../../lib/pid" -I"../../lib/pwm_in" -I"../../lib/led" -I"../../lib/ppm_in" -I"../../lib/uart2" -I"../../lib/uart1_queue" -I"../../lib/servo" -I"../../lib/scp1000" -I"../../lib/microcontroller" -I"../../lib/gps" -I"../../lib/dataflash" -mlarge-code -mlarge-data -O1 -MMD -MF "${OBJECTDIR}/_ext/1077768206/bmp085.o.d" -o ${OBJECTDIR}/_ext/1077768206/bmp085.o ../../lib/bmp085/bmp085.c  
	
${OBJECTDIR}/_ext/1070193764/button.o: ../../lib/button/button.c  nbproject/Makefile-${CND_CONF}.mk
	@${MKDIR} ${OBJECTDIR}/_ext/1070193764 
	@${RM} ${OBJECTDIR}/_ext/1070193764/button.o.d 
	@${RM} ${OBJECTDIR}/_ext/1070193764/button.o.ok ${OBJECTDIR}/_ext/1070193764/button.o.err 
	@${FIXDEPS} "${OBJECTDIR}/_ext/1070193764/button.o.d" $(SILENT) -c ${MP_CC} $(MP_EXTRA_CC_PRE)  -g -omf=elf -x c -c -mcpu=$(MP_PROCESSOR_OPTION) -Wall -DMPLAB_DSPIC_PORT -DENABLE_QUADROCOPTER -I"../../lib/FreeRTOS" -I"../../lib" -I"../../lib/button" -I"../../lib/adc" -I".." -I"../../lib/i2c" -I"../../lib/bmp085" -I"../../lib/hmc5843" -I"../../lib/max7456" -I"../../lib/matrix" -I"../../lib/quaternion" -I"../../lib/pid" -I"../../lib/pwm_in" -I"../../lib/led" -I"../../lib/ppm_in" -I"../../lib/uart2" -I"../../lib/uart1_queue" -I"../../lib/servo" -I"../../lib/scp1000" -I"../../lib/microcontroller" -I"../../lib/gps" -I"../../lib/dataflash" -mlarge-code -mlarge-data -O1 -MMD -MF "${OBJECTDIR}/_ext/1070193764/button.o.d" -o ${OBJECTDIR}/_ext/1070193764/button.o ../../lib/button/button.c  
	
${OBJECTDIR}/_ext/968823332/dataflash.o: ../../lib/dataflash/dataflash.c  nbproject/Makefile-${CND_CONF}.mk
	@${MKDIR} ${OBJECTDIR}/_ext/968823332 
	@${RM} ${OBJECTDIR}/_ext/968823332/dataflash.o.d 
	@${RM} ${OBJECTDIR}/_ext/968823332/dataflash.o.ok ${OBJECTDIR}/_ext/968823332/dataflash.o.err 
	@${FIXDEPS} "${OBJECTDIR}/_ext/968823332/dataflash.o.d" $(SILENT) -c ${MP_CC} $(MP_EXTRA_CC_PRE)  -g -omf=elf -x c -c -mcpu=$(MP_PROCESSOR_OPTION) -Wall -DMPLAB_DSPIC_PORT -DENABLE_QUADROCOPTER -I"../../lib/FreeRTOS" -I"../../lib" -I"../../lib/button" -I"../../lib/adc" -I".." -I"../../lib/i2c" -I"../../lib/bmp085" -I"../../lib/hmc5843" -I"../../lib/max7456" -I"../../lib/matrix" -I"../../lib/quaternion" -I"../../lib/pid" -I"../../lib/pwm_in" -I"../../lib/led" -I"../../lib/ppm_in" -I"../../lib/uart2" -I"../../lib/uart1_queue" -I"../../lib/servo" -I"../../lib/scp1000" -I"../../lib/microcontroller" -I"../../lib/gps" -I"../../lib/dataflash" -mlarge-code -mlarge-data -O1 -MMD -MF "${OBJECTDIR}/_ext/968823332/dataflash.o.d" -o ${OBJECTDIR}/_ext/968823332/dataflash.o ../../lib/dataflash/dataflash.c  
	
${OBJECTDIR}/_ext/957545600/gps.o: ../../lib/gps/gps.c  nbproject/Makefile-${CND_CONF}.mk
	@${MKDIR} ${OBJECTDIR}/_ext/957545600 
	@${RM} ${OBJECTDIR}/_ext/957545600/gps.o.d 
	@${RM} ${OBJECTDIR}/_ext/957545600/gps.o.ok ${OBJECTDIR}/_ext/957545600/gps.o.err 
	@${FIXDEPS} "${OBJECTDIR}/_ext/957545600/gps.o.d" $(SILENT) -c ${MP_CC} $(MP_EXTRA_CC_PRE)  -g -omf=elf -x c -c -mcpu=$(MP_PROCESSOR_OPTION) -Wall -DMPLAB_DSPIC_PORT -DENABLE_QUADROCOPTER -I"../../lib/FreeRTOS" -I"../../lib" -I"../../lib/button" -I"../../lib/adc" -I".." -I"../../lib/i2c" -I"../../lib/bmp085" -I"../../lib/hmc5843" -I"../../lib/max7456" -I"../../lib/matrix" -I"../../lib/quaternion" -I"../../lib/pid" -I"../../lib/pwm_in" -I"../../lib/led" -I"../../lib/ppm_in" -I"../../lib/uart2" -I"../../lib/uart1_queue" -I"../../lib/servo" -I"../../lib/scp1000" -I"../../lib/microcontroller" -I"../../lib/gps" -I"../../lib/dataflash" -mlarge-code -mlarge-data -O1 -MMD -MF "${OBJECTDIR}/_ext/957545600/gps.o.d" -o ${OBJECTDIR}/_ext/957545600/gps.o ../../lib/gps/gps.c  
	
${OBJECTDIR}/_ext/1967121974/hmc5843.o: ../../lib/hmc5843/hmc5843.c  nbproject/Makefile-${CND_CONF}.mk
	@${MKDIR} ${OBJECTDIR}/_ext/1967121974 
	@${RM} ${OBJECTDIR}/_ext/1967121974/hmc5843.o.d 
	@${RM} ${OBJECTDIR}/_ext/1967121974/hmc5843.o.ok ${OBJECTDIR}/_ext/1967121974/hmc5843.o.err 
	@${FIXDEPS} "${OBJECTDIR}/_ext/1967121974/hmc5843.o.d" $(SILENT) -c ${MP_CC} $(MP_EXTRA_CC_PRE)  -g -omf=elf -x c -c -mcpu=$(MP_PROCESSOR_OPTION) -Wall -DMPLAB_DSPIC_PORT -DENABLE_QUADROCOPTER -I"../../lib/FreeRTOS" -I"../../lib" -I"../../lib/button" -I"../../lib/adc" -I".." -I"../../lib/i2c" -I"../../lib/bmp085" -I"../../lib/hmc5843" -I"../../lib/max7456" -I"../../lib/matrix" -I"../../lib/quaternion" -I"../../lib/pid" -I"../../lib/pwm_in" -I"../../lib/led" -I"../../lib/ppm_in" -I"../../lib/uart2" -I"../../lib/uart1_queue" -I"../../lib/servo" -I"../../lib/scp1000" -I"../../lib/microcontroller" -I"../../lib/gps" -I"../../lib/dataflash" -mlarge-code -mlarge-data -O1 -MMD -MF "${OBJECTDIR}/_ext/1967121974/hmc5843.o.d" -o ${OBJECTDIR}/_ext/1967121974/hmc5843.o ../../lib/hmc5843/hmc5843.c  
	
${OBJECTDIR}/_ext/957545584/i2c.o: ../../lib/i2c/i2c.c  nbproject/Makefile-${CND_CONF}.mk
	@${MKDIR} ${OBJECTDIR}/_ext/957545584 
	@${RM} ${OBJECTDIR}/_ext/957545584/i2c.o.d 
	@${RM} ${OBJECTDIR}/_ext/957545584/i2c.o.ok ${OBJECTDIR}/_ext/957545584/i2c.o.err 
	@${FIXDEPS} "${OBJECTDIR}/_ext/957545584/i2c.o.d" $(SILENT) -c ${MP_CC} $(MP_EXTRA_CC_PRE)  -g -omf=elf -x c -c -mcpu=$(MP_PROCESSOR_OPTION) -Wall -DMPLAB_DSPIC_PORT -DENABLE_QUADROCOPTER -I"../../lib/FreeRTOS" -I"../../lib" -I"../../lib/button" -I"../../lib/adc" -I".." -I"../../lib/i2c" -I"../../lib/bmp085" -I"../../lib/hmc5843" -I"../../lib/max7456" -I"../../lib/matrix" -I"../../lib/quaternion" -I"../../lib/pid" -I"../../lib/pwm_in" -I"../../lib/led" -I"../../lib/ppm_in" -I"../../lib/uart2" -I"../../lib/uart1_queue" -I"../../lib/servo" -I"../../lib/scp1000" -I"../../lib/microcontroller" -I"../../lib/gps" -I"../../lib/dataflash" -mlarge-code -mlarge-data -O1 -MMD -MF "${OBJECTDIR}/_ext/957545584/i2c.o.d" -o ${OBJECTDIR}/_ext/957545584/i2c.o ../../lib/i2c/i2c.c  
	
${OBJECTDIR}/_ext/957550049/led.o: ../../lib/led/led.c  nbproject/Makefile-${CND_CONF}.mk
	@${MKDIR} ${OBJECTDIR}/_ext/957550049 
	@${RM} ${OBJECTDIR}/_ext/957550049/led.o.d 
	@${RM} ${OBJECTDIR}/_ext/957550049/led.o.ok ${OBJECTDIR}/_ext/957550049/led.o.err 
	@${FIXDEPS} "${OBJECTDIR}/_ext/957550049/led.o.d" $(SILENT) -c ${MP_CC} $(MP_EXTRA_CC_PRE)  -g -omf=elf -x c -c -mcpu=$(MP_PROCESSOR_OPTION) -Wall -DMPLAB_DSPIC_PORT -DENABLE_QUADROCOPTER -I"../../lib/FreeRTOS" -I"../../lib" -I"../../lib/button" -I"../../lib/adc" -I".." -I"../../lib/i2c" -I"../../lib/bmp085" -I"../../lib/hmc5843" -I"../../lib/max7456" -I"../../lib/matrix" -I"../../lib/quaternion" -I"../../lib/pid" -I"../../lib/pwm_in" -I"../../lib/led" -I"../../lib/ppm_in" -I"../../lib/uart2" -I"../../lib/uart1_queue" -I"../../lib/servo" -I"../../lib/scp1000" -I"../../lib/microcontroller" -I"../../lib/gps" -I"../../lib/dataflash" -mlarge-code -mlarge-data -O1 -MMD -MF "${OBJECTDIR}/_ext/957550049/led.o.d" -o ${OBJECTDIR}/_ext/957550049/led.o ../../lib/led/led.c  
	
${OBJECTDIR}/_ext/773745621/matrix.o: ../../lib/matrix/matrix.c  nbproject/Makefile-${CND_CONF}.mk
	@${MKDIR} ${OBJECTDIR}/_ext/773745621 
	@${RM} ${OBJECTDIR}/_ext/773745621/matrix.o.d 
	@${RM} ${OBJECTDIR}/_ext/773745621/matrix.o.ok ${OBJECTDIR}/_ext/773745621/matrix.o.err 
	@${FIXDEPS} "${OBJECTDIR}/_ext/773745621/matrix.o.d" $(SILENT) -c ${MP_CC} $(MP_EXTRA_CC_PRE)  -g -omf=elf -x c -c -mcpu=$(MP_PROCESSOR_OPTION) -Wall -DMPLAB_DSPIC_PORT -DENABLE_QUADROCOPTER -I"../../lib/FreeRTOS" -I"../../lib" -I"../../lib/button" -I"../../lib/adc" -I".." -I"../../lib/i2c" -I"../../lib/bmp085" -I"../../lib/hmc5843" -I"../../lib/max7456" -I"../../lib/matrix" -I"../../lib/quaternion" -I"../../lib/pid" -I"../../lib/pwm_in" -I"../../lib/led" -I"../../lib/ppm_in" -I"../../lib/uart2" -I"../../lib/uart1_queue" -I"../../lib/servo" -I"../../lib/scp1000" -I"../../lib/microcontroller" -I"../../lib/gps" -I"../../lib/dataflash" -mlarge-code -mlarge-data -O1 -MMD -MF "${OBJECTDIR}/_ext/773745621/matrix.o.d" -o ${OBJECTDIR}/_ext/773745621/matrix.o ../../lib/matrix/matrix.c  
	
${OBJECTDIR}/_ext/1785572984/max7456.o: ../../lib/max7456/max7456.c  nbproject/Makefile-${CND_CONF}.mk
	@${MKDIR} ${OBJECTDIR}/_ext/1785572984 
	@${RM} ${OBJECTDIR}/_ext/1785572984/max7456.o.d 
	@${RM} ${OBJECTDIR}/_ext/1785572984/max7456.o.ok ${OBJECTDIR}/_ext/1785572984/max7456.o.err 
	@${FIXDEPS} "${OBJECTDIR}/_ext/1785572984/max7456.o.d" $(SILENT) -c ${MP_CC} $(MP_EXTRA_CC_PRE)  -g -omf=elf -x c -c -mcpu=$(MP_PROCESSOR_OPTION) -Wall -DMPLAB_DSPIC_PORT -DENABLE_QUADROCOPTER -I"../../lib/FreeRTOS" -I"../../lib" -I"../../lib/button" -I"../../lib/adc" -I".." -I"../../lib/i2c" -I"../../lib/bmp085" -I"../../lib/hmc5843" -I"../../lib/max7456" -I"../../lib/matrix" -I"../../lib/quaternion" -I"../../lib/pid" -I"../../lib/pwm_in" -I"../../lib/led" -I"../../lib/ppm_in" -I"../../lib/uart2" -I"../../lib/uart1_queue" -I"../../lib/servo" -I"../../lib/scp1000" -I"../../lib/microcontroller" -I"../../lib/gps" -I"../../lib/dataflash" -mlarge-code -mlarge-data -O1 -MMD -MF "${OBJECTDIR}/_ext/1785572984/max7456.o.d" -o ${OBJECTDIR}/_ext/1785572984/max7456.o ../../lib/max7456/max7456.c  
	
${OBJECTDIR}/_ext/1843177418/microcontroller.o: ../../lib/microcontroller/microcontroller.c  nbproject/Makefile-${CND_CONF}.mk
	@${MKDIR} ${OBJECTDIR}/_ext/1843177418 
	@${RM} ${OBJECTDIR}/_ext/1843177418/microcontroller.o.d 
	@${RM} ${OBJECTDIR}/_ext/1843177418/microcontroller.o.ok ${OBJECTDIR}/_ext/1843177418/microcontroller.o.err 
	@${FIXDEPS} "${OBJECTDIR}/_ext/1843177418/microcontroller.o.d" $(SILENT) -c ${MP_CC} $(MP_EXTRA_CC_PRE)  -g -omf=elf -x c -c -mcpu=$(MP_PROCESSOR_OPTION) -Wall -DMPLAB_DSPIC_PORT -DENABLE_QUADROCOPTER -I"../../lib/FreeRTOS" -I"../../lib" -I"../../lib/button" -I"../../lib/adc" -I".." -I"../../lib/i2c" -I"../../lib/bmp085" -I"../../lib/hmc5843" -I"../../lib/max7456" -I"../../lib/matrix" -I"../../lib/quaternion" -I"../../lib/pid" -I"../../lib/pwm_in" -I"../../lib/led" -I"../../lib/ppm_in" -I"../../lib/uart2" -I"../../lib/uart1_queue" -I"../../lib/servo" -I"../../lib/scp1000" -I"../../lib/microcontroller" -I"../../lib/gps" -I"../../lib/dataflash" -mlarge-code -mlarge-data -O1 -MMD -MF "${OBJECTDIR}/_ext/1843177418/microcontroller.o.d" -o ${OBJECTDIR}/_ext/1843177418/microcontroller.o ../../lib/microcontroller/microcontroller.c  
	
${OBJECTDIR}/_ext/957554017/pid.o: ../../lib/pid/pid.c  nbproject/Makefile-${CND_CONF}.mk
	@${MKDIR} ${OBJECTDIR}/_ext/957554017 
	@${RM} ${OBJECTDIR}/_ext/957554017/pid.o.d 
	@${RM} ${OBJECTDIR}/_ext/957554017/pid.o.ok ${OBJECTDIR}/_ext/957554017/pid.o.err 
	@${FIXDEPS} "${OBJECTDIR}/_ext/957554017/pid.o.d" $(SILENT) -c ${MP_CC} $(MP_EXTRA_CC_PRE)  -g -omf=elf -x c -c -mcpu=$(MP_PROCESSOR_OPTION) -Wall -DMPLAB_DSPIC_PORT -DENABLE_QUADROCOPTER -I"../../lib/FreeRTOS" -I"../../lib" -I"../../lib/button" -I"../../lib/adc" -I".." -I"../../lib/i2c" -I"../../lib/bmp085" -I"../../lib/hmc5843" -I"../../lib/max7456" -I"../../lib/matrix" -I"../../lib/quaternion" -I"../../lib/pid" -I"../../lib/pwm_in" -I"../../lib/led" -I"../../lib/ppm_in" -I"../../lib/uart2" -I"../../lib/uart1_queue" -I"../../lib/servo" -I"../../lib/scp1000" -I"../../lib/microcontroller" -I"../../lib/gps" -I"../../lib/dataflash" -mlarge-code -mlarge-data -O1 -MMD -MF "${OBJECTDIR}/_ext/957554017/pid.o.d" -o ${OBJECTDIR}/_ext/957554017/pid.o ../../lib/pid/pid.c  
	
${OBJECTDIR}/_ext/674232159/ppm_in.o: ../../lib/ppm_in/ppm_in.c  nbproject/Makefile-${CND_CONF}.mk
	@${MKDIR} ${OBJECTDIR}/_ext/674232159 
	@${RM} ${OBJECTDIR}/_ext/674232159/ppm_in.o.d 
	@${RM} ${OBJECTDIR}/_ext/674232159/ppm_in.o.ok ${OBJECTDIR}/_ext/674232159/ppm_in.o.err 
	@${FIXDEPS} "${OBJECTDIR}/_ext/674232159/ppm_in.o.d" $(SILENT) -c ${MP_CC} $(MP_EXTRA_CC_PRE)  -g -omf=elf -x c -c -mcpu=$(MP_PROCESSOR_OPTION) -Wall -DMPLAB_DSPIC_PORT -DENABLE_QUADROCOPTER -I"../../lib/FreeRTOS" -I"../../lib" -I"../../lib/button" -I"../../lib/adc" -I".." -I"../../lib/i2c" -I"../../lib/bmp085" -I"../../lib/hmc5843" -I"../../lib/max7456" -I"../../lib/matrix" -I"../../lib/quaternion" -I"../../lib/pid" -I"../../lib/pwm_in" -I"../../lib/led" -I"../../lib/ppm_in" -I"../../lib/uart2" -I"../../lib/uart1_queue" -I"../../lib/servo" -I"../../lib/scp1000" -I"../../lib/microcontroller" -I"../../lib/gps" -I"../../lib/dataflash" -mlarge-code -mlarge-data -O1 -MMD -MF "${OBJECTDIR}/_ext/674232159/ppm_in.o.d" -o ${OBJECTDIR}/_ext/674232159/ppm_in.o ../../lib/ppm_in/ppm_in.c  
	
${OBJECTDIR}/_ext/667767512/pwm_in.o: ../../lib/pwm_in/pwm_in.c  nbproject/Makefile-${CND_CONF}.mk
	@${MKDIR} ${OBJECTDIR}/_ext/667767512 
	@${RM} ${OBJECTDIR}/_ext/667767512/pwm_in.o.d 
	@${RM} ${OBJECTDIR}/_ext/667767512/pwm_in.o.ok ${OBJECTDIR}/_ext/667767512/pwm_in.o.err 
	@${FIXDEPS} "${OBJECTDIR}/_ext/667767512/pwm_in.o.d" $(SILENT) -c ${MP_CC} $(MP_EXTRA_CC_PRE)  -g -omf=elf -x c -c -mcpu=$(MP_PROCESSOR_OPTION) -Wall -DMPLAB_DSPIC_PORT -DENABLE_QUADROCOPTER -I"../../lib/FreeRTOS" -I"../../lib" -I"../../lib/button" -I"../../lib/adc" -I".." -I"../../lib/i2c" -I"../../lib/bmp085" -I"../../lib/hmc5843" -I"../../lib/max7456" -I"../../lib/matrix" -I"../../lib/quaternion" -I"../../lib/pid" -I"../../lib/pwm_in" -I"../../lib/led" -I"../../lib/ppm_in" -I"../../lib/uart2" -I"../../lib/uart1_queue" -I"../../lib/servo" -I"../../lib/scp1000" -I"../../lib/microcontroller" -I"../../lib/gps" -I"../../lib/dataflash" -mlarge-code -mlarge-data -O1 -MMD -MF "${OBJECTDIR}/_ext/667767512/pwm_in.o.d" -o ${OBJECTDIR}/_ext/667767512/pwm_in.o ../../lib/pwm_in/pwm_in.c  
	
${OBJECTDIR}/_ext/888521352/quaternion.o: ../../lib/quaternion/quaternion.c  nbproject/Makefile-${CND_CONF}.mk
	@${MKDIR} ${OBJECTDIR}/_ext/888521352 
	@${RM} ${OBJECTDIR}/_ext/888521352/quaternion.o.d 
	@${RM} ${OBJECTDIR}/_ext/888521352/quaternion.o.ok ${OBJECTDIR}/_ext/888521352/quaternion.o.err 
	@${FIXDEPS} "${OBJECTDIR}/_ext/888521352/quaternion.o.d" $(SILENT) -c ${MP_CC} $(MP_EXTRA_CC_PRE)  -g -omf=elf -x c -c -mcpu=$(MP_PROCESSOR_OPTION) -Wall -DMPLAB_DSPIC_PORT -DENABLE_QUADROCOPTER -I"../../lib/FreeRTOS" -I"../../lib" -I"../../lib/button" -I"../../lib/adc" -I".." -I"../../lib/i2c" -I"../../lib/bmp085" -I"../../lib/hmc5843" -I"../../lib/max7456" -I"../../lib/matrix" -I"../../lib/quaternion" -I"../../lib/pid" -I"../../lib/pwm_in" -I"../../lib/led" -I"../../lib/ppm_in" -I"../../lib/uart2" -I"../../lib/uart1_queue" -I"../../lib/servo" -I"../../lib/scp1000" -I"../../lib/microcontroller" -I"../../lib/gps" -I"../../lib/dataflash" -mlarge-code -mlarge-data -O1 -MMD -MF "${OBJECTDIR}/_ext/888521352/quaternion.o.d" -o ${OBJECTDIR}/_ext/888521352/quaternion.o ../../lib/quaternion/quaternion.c  
	
${OBJECTDIR}/_ext/1429652139/scp1000.o: ../../lib/scp1000/scp1000.c  nbproject/Makefile-${CND_CONF}.mk
	@${MKDIR} ${OBJECTDIR}/_ext/1429652139 
	@${RM} ${OBJECTDIR}/_ext/1429652139/scp1000.o.d 
	@${RM} ${OBJECTDIR}/_ext/1429652139/scp1000.o.ok ${OBJECTDIR}/_ext/1429652139/scp1000.o.err 
	@${FIXDEPS} "${OBJECTDIR}/_ext/1429652139/scp1000.o.d" $(SILENT) -c ${MP_CC} $(MP_EXTRA_CC_PRE)  -g -omf=elf -x c -c -mcpu=$(MP_PROCESSOR_OPTION) -Wall -DMPLAB_DSPIC_PORT -DENABLE_QUADROCOPTER -I"../../lib/FreeRTOS" -I"../../lib" -I"../../lib/button" -I"../../lib/adc" -I".." -I"../../lib/i2c" -I"../../lib/bmp085" -I"../../lib/hmc5843" -I"../../lib/max7456" -I"../../lib/matrix" -I"../../lib/quaternion" -I"../../lib/pid" -I"../../lib/pwm_in" -I"../../lib/led" -I"../../lib/ppm_in" -I"../../lib/uart2" -I"../../lib/uart1_queue" -I"../../lib/servo" -I"../../lib/scp1000" -I"../../lib/microcontroller" -I"../../lib/gps" -I"../../lib/dataflash" -mlarge-code -mlarge-data -O1 -MMD -MF "${OBJECTDIR}/_ext/1429652139/scp1000.o.d" -o ${OBJECTDIR}/_ext/1429652139/scp1000.o ../../lib/scp1000/scp1000.c  
	
${OBJECTDIR}/_ext/1089077615/servo.o: ../../lib/servo/servo.c  nbproject/Makefile-${CND_CONF}.mk
	@${MKDIR} ${OBJECTDIR}/_ext/1089077615 
	@${RM} ${OBJECTDIR}/_ext/1089077615/servo.o.d 
	@${RM} ${OBJECTDIR}/_ext/1089077615/servo.o.ok ${OBJECTDIR}/_ext/1089077615/servo.o.err 
	@${FIXDEPS} "${OBJECTDIR}/_ext/1089077615/servo.o.d" $(SILENT) -c ${MP_CC} $(MP_EXTRA_CC_PRE)  -g -omf=elf -x c -c -mcpu=$(MP_PROCESSOR_OPTION) -Wall -DMPLAB_DSPIC_PORT -DENABLE_QUADROCOPTER -I"../../lib/FreeRTOS" -I"../../lib" -I"../../lib/button" -I"../../lib/adc" -I".." -I"../../lib/i2c" -I"../../lib/bmp085" -I"../../lib/hmc5843" -I"../../lib/max7456" -I"../../lib/matrix" -I"../../lib/quaternion" -I"../../lib/pid" -I"../../lib/pwm_in" -I"../../lib/led" -I"../../lib/ppm_in" -I"../../lib/uart2" -I"../../lib/uart1_queue" -I"../../lib/servo" -I"../../lib/scp1000" -I"../../lib/microcontroller" -I"../../lib/gps" -I"../../lib/dataflash" -mlarge-code -mlarge-data -O1 -MMD -MF "${OBJECTDIR}/_ext/1089077615/servo.o.d" -o ${OBJECTDIR}/_ext/1089077615/servo.o ../../lib/servo/servo.c  
	
${OBJECTDIR}/_ext/1591518261/uart1_queue.o: ../../lib/uart1_queue/uart1_queue.c  nbproject/Makefile-${CND_CONF}.mk
	@${MKDIR} ${OBJECTDIR}/_ext/1591518261 
	@${RM} ${OBJECTDIR}/_ext/1591518261/uart1_queue.o.d 
	@${RM} ${OBJECTDIR}/_ext/1591518261/uart1_queue.o.ok ${OBJECTDIR}/_ext/1591518261/uart1_queue.o.err 
	@${FIXDEPS} "${OBJECTDIR}/_ext/1591518261/uart1_queue.o.d" $(SILENT) -c ${MP_CC} $(MP_EXTRA_CC_PRE)  -g -omf=elf -x c -c -mcpu=$(MP_PROCESSOR_OPTION) -Wall -DMPLAB_DSPIC_PORT -DENABLE_QUADROCOPTER -I"../../lib/FreeRTOS" -I"../../lib" -I"../../lib/button" -I"../../lib/adc" -I".." -I"../../lib/i2c" -I"../../lib/bmp085" -I"../../lib/hmc5843" -I"../../lib/max7456" -I"../../lib/matrix" -I"../../lib/quaternion" -I"../../lib/pid" -I"../../lib/pwm_in" -I"../../lib/led" -I"../../lib/ppm_in" -I"../../lib/uart2" -I"../../lib/uart1_queue" -I"../../lib/servo" -I"../../lib/scp1000" -I"../../lib/microcontroller" -I"../../lib/gps" -I"../../lib/dataflash" -mlarge-code -mlarge-data -O1 -MMD -MF "${OBJECTDIR}/_ext/1591518261/uart1_queue.o.d" -o ${OBJECTDIR}/_ext/1591518261/uart1_queue.o ../../lib/uart1_queue/uart1_queue.c  
	
${OBJECTDIR}/_ext/1090805370/uart2.o: ../../lib/uart2/uart2.c  nbproject/Makefile-${CND_CONF}.mk
	@${MKDIR} ${OBJECTDIR}/_ext/1090805370 
	@${RM} ${OBJECTDIR}/_ext/1090805370/uart2.o.d 
	@${RM} ${OBJECTDIR}/_ext/1090805370/uart2.o.ok ${OBJECTDIR}/_ext/1090805370/uart2.o.err 
	@${FIXDEPS} "${OBJECTDIR}/_ext/1090805370/uart2.o.d" $(SILENT) -c ${MP_CC} $(MP_EXTRA_CC_PRE)  -g -omf=elf -x c -c -mcpu=$(MP_PROCESSOR_OPTION) -Wall -DMPLAB_DSPIC_PORT -DENABLE_QUADROCOPTER -I"../../lib/FreeRTOS" -I"../../lib" -I"../../lib/button" -I"../../lib/adc" -I".." -I"../../lib/i2c" -I"../../lib/bmp085" -I"../../lib/hmc5843" -I"../../lib/max7456" -I"../../lib/matrix" -I"../../lib/quaternion" -I"../../lib/pid" -I"../../lib/pwm_in" -I"../../lib/led" -I"../../lib/ppm_in" -I"../../lib/uart2" -I"../../lib/uart1_queue" -I"../../lib/servo" -I"../../lib/scp1000" -I"../../lib/microcontroller" -I"../../lib/gps" -I"../../lib/dataflash" -mlarge-code -mlarge-data -O1 -MMD -MF "${OBJECTDIR}/_ext/1090805370/uart2.o.d" -o ${OBJECTDIR}/_ext/1090805370/uart2.o ../../lib/uart2/uart2.c  
	
${OBJECTDIR}/_ext/1472/ahrs_kalman_2x3.o: ../ahrs_kalman_2x3.c  nbproject/Makefile-${CND_CONF}.mk
	@${MKDIR} ${OBJECTDIR}/_ext/1472 
	@${RM} ${OBJECTDIR}/_ext/1472/ahrs_kalman_2x3.o.d 
	@${RM} ${OBJECTDIR}/_ext/1472/ahrs_kalman_2x3.o.ok ${OBJECTDIR}/_ext/1472/ahrs_kalman_2x3.o.err 
	@${FIXDEPS} "${OBJECTDIR}/_ext/1472/ahrs_kalman_2x3.o.d" $(SILENT) -c ${MP_CC} $(MP_EXTRA_CC_PRE)  -g -omf=elf -x c -c -mcpu=$(MP_PROCESSOR_OPTION) -Wall -DMPLAB_DSPIC_PORT -DENABLE_QUADROCOPTER -I"../../lib/FreeRTOS" -I"../../lib" -I"../../lib/button" -I"../../lib/adc" -I".." -I"../../lib/i2c" -I"../../lib/bmp085" -I"../../lib/hmc5843" -I"../../lib/max7456" -I"../../lib/matrix" -I"../../lib/quaternion" -I"../../lib/pid" -I"../../lib/pwm_in" -I"../../lib/led" -I"../../lib/ppm_in" -I"../../lib/uart2" -I"../../lib/uart1_queue" -I"../../lib/servo" -I"../../lib/scp1000" -I"../../lib/microcontroller" -I"../../lib/gps" -I"../../lib/dataflash" -mlarge-code -mlarge-data -O1 -MMD -MF "${OBJECTDIR}/_ext/1472/ahrs_kalman_2x3.o.d" -o ${OBJECTDIR}/_ext/1472/ahrs_kalman_2x3.o ../ahrs_kalman_2x3.c  
	
${OBJECTDIR}/_ext/1472/alarms.o: ../alarms.c  nbproject/Makefile-${CND_CONF}.mk
	@${MKDIR} ${OBJECTDIR}/_ext/1472 
	@${RM} ${OBJECTDIR}/_ext/1472/alarms.o.d 
	@${RM} ${OBJECTDIR}/_ext/1472/alarms.o.ok ${OBJECTDIR}/_ext/1472/alarms.o.err 
	@${FIXDEPS} "${OBJECTDIR}/_ext/1472/alarms.o.d" $(SILENT) -c ${MP_CC} $(MP_EXTRA_CC_PRE)  -g -omf=elf -x c -c -mcpu=$(MP_PROCESSOR_OPTION) -Wall -DMPLAB_DSPIC_PORT -DENABLE_QUADROCOPTER -I"../../lib/FreeRTOS" -I"../../lib" -I"../../lib/button" -I"../../lib/adc" -I".." -I"../../lib/i2c" -I"../../lib/bmp085" -I"../../lib/hmc5843" -I"../../lib/max7456" -I"../../lib/matrix" -I"../../lib/quaternion" -I"../../lib/pid" -I"../../lib/pwm_in" -I"../../lib/led" -I"../../lib/ppm_in" -I"../../lib/uart2" -I"../../lib/uart1_queue" -I"../../lib/servo" -I"../../lib/scp1000" -I"../../lib/microcontroller" -I"../../lib/gps" -I"../../lib/dataflash" -mlarge-code -mlarge-data -O1 -MMD -MF "${OBJECTDIR}/_ext/1472/alarms.o.d" -o ${OBJECTDIR}/_ext/1472/alarms.o ../alarms.c  
	
${OBJECTDIR}/_ext/1472/communication_csv.o: ../communication_csv.c  nbproject/Makefile-${CND_CONF}.mk
	@${MKDIR} ${OBJECTDIR}/_ext/1472 
	@${RM} ${OBJECTDIR}/_ext/1472/communication_csv.o.d 
	@${RM} ${OBJECTDIR}/_ext/1472/communication_csv.o.ok ${OBJECTDIR}/_ext/1472/communication_csv.o.err 
	@${FIXDEPS} "${OBJECTDIR}/_ext/1472/communication_csv.o.d" $(SILENT) -c ${MP_CC} $(MP_EXTRA_CC_PRE)  -g -omf=elf -x c -c -mcpu=$(MP_PROCESSOR_OPTION) -Wall -DMPLAB_DSPIC_PORT -DENABLE_QUADROCOPTER -I"../../lib/FreeRTOS" -I"../../lib" -I"../../lib/button" -I"../../lib/adc" -I".." -I"../../lib/i2c" -I"../../lib/bmp085" -I"../../lib/hmc5843" -I"../../lib/max7456" -I"../../lib/matrix" -I"../../lib/quaternion" -I"../../lib/pid" -I"../../lib/pwm_in" -I"../../lib/led" -I"../../lib/ppm_in" -I"../../lib/uart2" -I"../../lib/uart1_queue" -I"../../lib/servo" -I"../../lib/scp1000" -I"../../lib/microcontroller" -I"../../lib/gps" -I"../../lib/dataflash" -mlarge-code -mlarge-data -O1 -MMD -MF "${OBJECTDIR}/_ext/1472/communication_csv.o.d" -o ${OBJECTDIR}/_ext/1472/communication_csv.o ../communication_csv.c  
	
${OBJECTDIR}/_ext/1472/configuration.o: ../configuration.c  nbproject/Makefile-${CND_CONF}.mk
	@${MKDIR} ${OBJECTDIR}/_ext/1472 
	@${RM} ${OBJECTDIR}/_ext/1472/configuration.o.d 
	@${RM} ${OBJECTDIR}/_ext/1472/configuration.o.ok ${OBJECTDIR}/_ext/1472/configuration.o.err 
	@${FIXDEPS} "${OBJECTDIR}/_ext/1472/configuration.o.d" $(SILENT) -c ${MP_CC} $(MP_EXTRA_CC_PRE)  -g -omf=elf -x c -c -mcpu=$(MP_PROCESSOR_OPTION) -Wall -DMPLAB_DSPIC_PORT -DENABLE_QUADROCOPTER -I"../../lib/FreeRTOS" -I"../../lib" -I"../../lib/button" -I"../../lib/adc" -I".." -I"../../lib/i2c" -I"../../lib/bmp085" -I"../../lib/hmc5843" -I"../../lib/max7456" -I"../../lib/matrix" -I"../../lib/quaternion" -I"../../lib/pid" -I"../../lib/pwm_in" -I"../../lib/led" -I"../../lib/ppm_in" -I"../../lib/uart2" -I"../../lib/uart1_queue" -I"../../lib/servo" -I"../../lib/scp1000" -I"../../lib/microcontroller" -I"../../lib/gps" -I"../../lib/dataflash" -mlarge-code -mlarge-data -O1 -MMD -MF "${OBJECTDIR}/_ext/1472/configuration.o.d" -o ${OBJECTDIR}/_ext/1472/configuration.o ../configuration.c  
	
${OBJECTDIR}/_ext/1472/control.o: ../control.c  nbproject/Makefile-${CND_CONF}.mk
	@${MKDIR} ${OBJECTDIR}/_ext/1472 
	@${RM} ${OBJECTDIR}/_ext/1472/control.o.d 
	@${RM} ${OBJECTDIR}/_ext/1472/control.o.ok ${OBJECTDIR}/_ext/1472/control.o.err 
	@${FIXDEPS} "${OBJECTDIR}/_ext/1472/control.o.d" $(SILENT) -c ${MP_CC} $(MP_EXTRA_CC_PRE)  -g -omf=elf -x c -c -mcpu=$(MP_PROCESSOR_OPTION) -Wall -DMPLAB_DSPIC_PORT -DENABLE_QUADROCOPTER -I"../../lib/FreeRTOS" -I"../../lib" -I"../../lib/button" -I"../../lib/adc" -I".." -I"../../lib/i2c" -I"../../lib/bmp085" -I"../../lib/hmc5843" -I"../../lib/max7456" -I"../../lib/matrix" -I"../../lib/quaternion" -I"../../lib/pid" -I"../../lib/pwm_in" -I"../../lib/led" -I"../../lib/ppm_in" -I"../../lib/uart2" -I"../../lib/uart1_queue" -I"../../lib/servo" -I"../../lib/scp1000" -I"../../lib/microcontroller" -I"../../lib/gps" -I"../../lib/dataflash" -mlarge-code -mlarge-data -O1 -MMD -MF "${OBJECTDIR}/_ext/1472/control.o.d" -o ${OBJECTDIR}/_ext/1472/control.o ../control.c  
	
${OBJECTDIR}/_ext/1472/datalogger.o: ../datalogger.c  nbproject/Makefile-${CND_CONF}.mk
	@${MKDIR} ${OBJECTDIR}/_ext/1472 
	@${RM} ${OBJECTDIR}/_ext/1472/datalogger.o.d 
	@${RM} ${OBJECTDIR}/_ext/1472/datalogger.o.ok ${OBJECTDIR}/_ext/1472/datalogger.o.err 
	@${FIXDEPS} "${OBJECTDIR}/_ext/1472/datalogger.o.d" $(SILENT) -c ${MP_CC} $(MP_EXTRA_CC_PRE)  -g -omf=elf -x c -c -mcpu=$(MP_PROCESSOR_OPTION) -Wall -DMPLAB_DSPIC_PORT -DENABLE_QUADROCOPTER -I"../../lib/FreeRTOS" -I"../../lib" -I"../../lib/button" -I"../../lib/adc" -I".." -I"../../lib/i2c" -I"../../lib/bmp085" -I"../../lib/hmc5843" -I"../../lib/max7456" -I"../../lib/matrix" -I"../../lib/quaternion" -I"../../lib/pid" -I"../../lib/pwm_in" -I"../../lib/led" -I"../../lib/ppm_in" -I"../../lib/uart2" -I"../../lib/uart1_queue" -I"../../lib/servo" -I"../../lib/scp1000" -I"../../lib/microcontroller" -I"../../lib/gps" -I"../../lib/dataflash" -mlarge-code -mlarge-data -O1 -MMD -MF "${OBJECTDIR}/_ext/1472/datalogger.o.d" -o ${OBJECTDIR}/_ext/1472/datalogger.o ../datalogger.c  
	
${OBJECTDIR}/_ext/1472/gluonscript.o: ../gluonscript.c  nbproject/Makefile-${CND_CONF}.mk
	@${MKDIR} ${OBJECTDIR}/_ext/1472 
	@${RM} ${OBJECTDIR}/_ext/1472/gluonscript.o.d 
	@${RM} ${OBJECTDIR}/_ext/1472/gluonscript.o.ok ${OBJECTDIR}/_ext/1472/gluonscript.o.err 
	@${FIXDEPS} "${OBJECTDIR}/_ext/1472/gluonscript.o.d" $(SILENT) -c ${MP_CC} $(MP_EXTRA_CC_PRE)  -g -omf=elf -x c -c -mcpu=$(MP_PROCESSOR_OPTION) -Wall -DMPLAB_DSPIC_PORT -DENABLE_QUADROCOPTER -I"../../lib/FreeRTOS" -I"../../lib" -I"../../lib/button" -I"../../lib/adc" -I".." -I"../../lib/i2c" -I"../../lib/bmp085" -I"../../lib/hmc5843" -I"../../lib/max7456" -I"../../lib/matrix" -I"../../lib/quaternion" -I"../../lib/pid" -I"../../lib/pwm_in" -I"../../lib/led" -I"../../lib/ppm_in" -I"../../lib/uart2" -I"../../lib/uart1_queue" -I"../../lib/servo" -I"../../lib/scp1000" -I"../../lib/microcontroller" -I"../../lib/gps" -I"../../lib/dataflash" -mlarge-code -mlarge-data -O1 -MMD -MF "${OBJECTDIR}/_ext/1472/gluonscript.o.d" -o ${OBJECTDIR}/_ext/1472/gluonscript.o ../gluonscript.c  
	
${OBJECTDIR}/_ext/1472/navigation.o: ../navigation.c  nbproject/Makefile-${CND_CONF}.mk
	@${MKDIR} ${OBJECTDIR}/_ext/1472 
	@${RM} ${OBJECTDIR}/_ext/1472/navigation.o.d 
	@${RM} ${OBJECTDIR}/_ext/1472/navigation.o.ok ${OBJECTDIR}/_ext/1472/navigation.o.err 
	@${FIXDEPS} "${OBJECTDIR}/_ext/1472/navigation.o.d" $(SILENT) -c ${MP_CC} $(MP_EXTRA_CC_PRE)  -g -omf=elf -x c -c -mcpu=$(MP_PROCESSOR_OPTION) -Wall -DMPLAB_DSPIC_PORT -DENABLE_QUADROCOPTER -I"../../lib/FreeRTOS" -I"../../lib" -I"../../lib/button" -I"../../lib/adc" -I".." -I"../../lib/i2c" -I"../../lib/bmp085" -I"../../lib/hmc5843" -I"../../lib/max7456" -I"../../lib/matrix" -I"../../lib/quaternion" -I"../../lib/pid" -I"../../lib/pwm_in" -I"../../lib/led" -I"../../lib/ppm_in" -I"../../lib/uart2" -I"../../lib/uart1_queue" -I"../../lib/servo" -I"../../lib/scp1000" -I"../../lib/microcontroller" -I"../../lib/gps" -I"../../lib/dataflash" -mlarge-code -mlarge-data -O1 -MMD -MF "${OBJECTDIR}/_ext/1472/navigation.o.d" -o ${OBJECTDIR}/_ext/1472/navigation.o ../navigation.c  
	
${OBJECTDIR}/_ext/1472/osd_max7456.o: ../osd_max7456.c  nbproject/Makefile-${CND_CONF}.mk
	@${MKDIR} ${OBJECTDIR}/_ext/1472 
	@${RM} ${OBJECTDIR}/_ext/1472/osd_max7456.o.d 
	@${RM} ${OBJECTDIR}/_ext/1472/osd_max7456.o.ok ${OBJECTDIR}/_ext/1472/osd_max7456.o.err 
	@${FIXDEPS} "${OBJECTDIR}/_ext/1472/osd_max7456.o.d" $(SILENT) -c ${MP_CC} $(MP_EXTRA_CC_PRE)  -g -omf=elf -x c -c -mcpu=$(MP_PROCESSOR_OPTION) -Wall -DMPLAB_DSPIC_PORT -DENABLE_QUADROCOPTER -I"../../lib/FreeRTOS" -I"../../lib" -I"../../lib/button" -I"../../lib/adc" -I".." -I"../../lib/i2c" -I"../../lib/bmp085" -I"../../lib/hmc5843" -I"../../lib/max7456" -I"../../lib/matrix" -I"../../lib/quaternion" -I"../../lib/pid" -I"../../lib/pwm_in" -I"../../lib/led" -I"../../lib/ppm_in" -I"../../lib/uart2" -I"../../lib/uart1_queue" -I"../../lib/servo" -I"../../lib/scp1000" -I"../../lib/microcontroller" -I"../../lib/gps" -I"../../lib/dataflash" -mlarge-code -mlarge-data -O1 -MMD -MF "${OBJECTDIR}/_ext/1472/osd_max7456.o.d" -o ${OBJECTDIR}/_ext/1472/osd_max7456.o ../osd_max7456.c  
	
${OBJECTDIR}/_ext/1472/rtos_pilot.o: ../rtos_pilot.c  nbproject/Makefile-${CND_CONF}.mk
	@${MKDIR} ${OBJECTDIR}/_ext/1472 
	@${RM} ${OBJECTDIR}/_ext/1472/rtos_pilot.o.d 
	@${RM} ${OBJECTDIR}/_ext/1472/rtos_pilot.o.ok ${OBJECTDIR}/_ext/1472/rtos_pilot.o.err 
	@${FIXDEPS} "${OBJECTDIR}/_ext/1472/rtos_pilot.o.d" $(SILENT) -c ${MP_CC} $(MP_EXTRA_CC_PRE)  -g -omf=elf -x c -c -mcpu=$(MP_PROCESSOR_OPTION) -Wall -DMPLAB_DSPIC_PORT -DENABLE_QUADROCOPTER -I"../../lib/FreeRTOS" -I"../../lib" -I"../../lib/button" -I"../../lib/adc" -I".." -I"../../lib/i2c" -I"../../lib/bmp085" -I"../../lib/hmc5843" -I"../../lib/max7456" -I"../../lib/matrix" -I"../../lib/quaternion" -I"../../lib/pid" -I"../../lib/pwm_in" -I"../../lib/led" -I"../../lib/ppm_in" -I"../../lib/uart2" -I"../../lib/uart1_queue" -I"../../lib/servo" -I"../../lib/scp1000" -I"../../lib/microcontroller" -I"../../lib/gps" -I"../../lib/dataflash" -mlarge-code -mlarge-data -O1 -MMD -MF "${OBJECTDIR}/_ext/1472/rtos_pilot.o.d" -o ${OBJECTDIR}/_ext/1472/rtos_pilot.o ../rtos_pilot.c  
	
${OBJECTDIR}/_ext/1472/sensors.o: ../sensors.c  nbproject/Makefile-${CND_CONF}.mk
	@${MKDIR} ${OBJECTDIR}/_ext/1472 
	@${RM} ${OBJECTDIR}/_ext/1472/sensors.o.d 
	@${RM} ${OBJECTDIR}/_ext/1472/sensors.o.ok ${OBJECTDIR}/_ext/1472/sensors.o.err 
	@${FIXDEPS} "${OBJECTDIR}/_ext/1472/sensors.o.d" $(SILENT) -c ${MP_CC} $(MP_EXTRA_CC_PRE)  -g -omf=elf -x c -c -mcpu=$(MP_PROCESSOR_OPTION) -Wall -DMPLAB_DSPIC_PORT -DENABLE_QUADROCOPTER -I"../../lib/FreeRTOS" -I"../../lib" -I"../../lib/button" -I"../../lib/adc" -I".." -I"../../lib/i2c" -I"../../lib/bmp085" -I"../../lib/hmc5843" -I"../../lib/max7456" -I"../../lib/matrix" -I"../../lib/quaternion" -I"../../lib/pid" -I"../../lib/pwm_in" -I"../../lib/led" -I"../../lib/ppm_in" -I"../../lib/uart2" -I"../../lib/uart1_queue" -I"../../lib/servo" -I"../../lib/scp1000" -I"../../lib/microcontroller" -I"../../lib/gps" -I"../../lib/dataflash" -mlarge-code -mlarge-data -O1 -MMD -MF "${OBJECTDIR}/_ext/1472/sensors.o.d" -o ${OBJECTDIR}/_ext/1472/sensors.o ../sensors.c  
	
${OBJECTDIR}/_ext/1472/trigger.o: ../trigger.c  nbproject/Makefile-${CND_CONF}.mk
	@${MKDIR} ${OBJECTDIR}/_ext/1472 
	@${RM} ${OBJECTDIR}/_ext/1472/trigger.o.d 
	@${RM} ${OBJECTDIR}/_ext/1472/trigger.o.ok ${OBJECTDIR}/_ext/1472/trigger.o.err 
	@${FIXDEPS} "${OBJECTDIR}/_ext/1472/trigger.o.d" $(SILENT) -c ${MP_CC} $(MP_EXTRA_CC_PRE)  -g -omf=elf -x c -c -mcpu=$(MP_PROCESSOR_OPTION) -Wall -DMPLAB_DSPIC_PORT -DENABLE_QUADROCOPTER -I"../../lib/FreeRTOS" -I"../../lib" -I"../../lib/button" -I"../../lib/adc" -I".." -I"../../lib/i2c" -I"../../lib/bmp085" -I"../../lib/hmc5843" -I"../../lib/max7456" -I"../../lib/matrix" -I"../../lib/quaternion" -I"../../lib/pid" -I"../../lib/pwm_in" -I"../../lib/led" -I"../../lib/ppm_in" -I"../../lib/uart2" -I"../../lib/uart1_queue" -I"../../lib/servo" -I"../../lib/scp1000" -I"../../lib/microcontroller" -I"../../lib/gps" -I"../../lib/dataflash" -mlarge-code -mlarge-data -O1 -MMD -MF "${OBJECTDIR}/_ext/1472/trigger.o.d" -o ${OBJECTDIR}/_ext/1472/trigger.o ../trigger.c  
	
endif

# ------------------------------------------------------------------------------------
# Rules for buildStep: link
ifeq ($(TYPE_IMAGE), DEBUG_RUN)
dist/${CND_CONF}/${IMAGE_TYPE}/rtos_pilot.X.${IMAGE_TYPE}.${OUTPUT_SUFFIX}: ${OBJECTFILES}  nbproject/Makefile-${CND_CONF}.mk
	@${MKDIR} dist/${CND_CONF}/${IMAGE_TYPE} 
	${MP_CC} $(MP_EXTRA_LD_PRE)  -omf=elf -mcpu=$(MP_PROCESSOR_OPTION)  -D__DEBUG -D__MPLAB_DEBUGGER_ICD3=1 -o dist/${CND_CONF}/${IMAGE_TYPE}/rtos_pilot.X.${IMAGE_TYPE}.${OUTPUT_SUFFIX} ${OBJECTFILES_QUOTED_IF_SPACED}        -Wl,--defsym=__MPLAB_BUILD=1,--heap=11300,-L"../../../../../../../../Program Files/Microchip/MPLAB C30/lib",-Map="${DISTDIR}/rtos_pilot.X.${IMAGE_TYPE}.map",--report-mem$(MP_EXTRA_LD_POST)$(MP_LINKER_FILE_OPTION),--defsym=__MPLAB_DEBUG=1,--defsym=__ICD2RAM=1,--defsym=__DEBUG=1,--defsym=__MPLAB_DEBUGGER_ICD3=1
else
dist/${CND_CONF}/${IMAGE_TYPE}/rtos_pilot.X.${IMAGE_TYPE}.${OUTPUT_SUFFIX}: ${OBJECTFILES}  nbproject/Makefile-${CND_CONF}.mk
	@${MKDIR} dist/${CND_CONF}/${IMAGE_TYPE} 
	${MP_CC} $(MP_EXTRA_LD_PRE)  -omf=elf -mcpu=$(MP_PROCESSOR_OPTION)  -o dist/${CND_CONF}/${IMAGE_TYPE}/rtos_pilot.X.${IMAGE_TYPE}.${DEBUGGABLE_SUFFIX} ${OBJECTFILES_QUOTED_IF_SPACED}        -Wl,--defsym=__MPLAB_BUILD=1,--heap=11300,-L"../../../../../../../../Program Files/Microchip/MPLAB C30/lib",-Map="${DISTDIR}/rtos_pilot.X.${IMAGE_TYPE}.map",--report-mem$(MP_EXTRA_LD_POST)$(MP_LINKER_FILE_OPTION)
	${MP_CC_DIR}\\pic30-bin2hex dist/${CND_CONF}/${IMAGE_TYPE}/rtos_pilot.X.${IMAGE_TYPE}.${DEBUGGABLE_SUFFIX} -omf=elf
endif


# Subprojects
.build-subprojects:

# Clean Targets
.clean-conf:
	${RM} -r build/quadrocopter
	${RM} -r dist/quadrocopter

# Enable dependency checking
.dep.inc: .depcheck-impl

DEPFILES=$(shell mplabwildcard ${POSSIBLE_DEPFILES})
ifneq (${DEPFILES},)
include ${DEPFILES}
endif
