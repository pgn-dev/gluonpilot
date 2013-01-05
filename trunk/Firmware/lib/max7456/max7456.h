// MAX7456.h

/*
 * MX7456 EV kit Firmware
 * Author: Paul Holden
 * Date  : Jan 25th, 2005
 *
 * max7456.h
 *
 * Constant Definitions
 */

#ifndef __MAX7456_H__
#define __MAX7456_H__

// Demo Mode
#define NTSC_DM_SIZE              390         

// Data Buffer
#define DATA_BUF_LENGTH           968

// SCI0 Protocol
#define NEW_MSG                   0xAA
#define ESC_CHAR                  0x80
#define ESC_CHAR_0x00             0xAA
#define ESC_CHAR_0x01             0x80

// Command codes sent by GUI
#define SCI0_CMD_WREG             0x00
#define SCI0_CMD_RREG             0x01
#define SCI0_CMD_WDM              0x10
#define SCI0_CMD_RDM              0x11
#define SCI0_CMD_WCM              0x20
#define SCI0_CMD_RCM              0x21

// MAX7456 device register definitions
//
// MAX7456 VIDEO_MODE_0 register
#define VIDEO_MODE_0_WRITE        0x00          
#define VIDEO_MODE_0_READ         0x80
#define VIDEO_MODE_0_40_PAL               0x40
#define VIDEO_MODE_0_40_NTSC              0x00
#define VIDEO_MODE_0_20_NoAutoSync        0x20
#define VIDEO_MODE_0_10_SyncInt           0x10
#define VIDEO_MODE_0_08_EnOSD             0x08
#define VIDEO_MODE_0_04_UpdateVsync       0x04
#define VIDEO_MODE_0_02_Reset             0x02
#define VIDEO_MODE_0_01_EnVideo           0x01
// VIDEO MODE 0 bitmap
#define NTSC                      0x00
#define PAL                       0x40
#define AUTO_SYNC                 0x00
#define EXT_SYNC                  0x20
#define INT_SYNC                  0x30
#define OSD_EN                    0x08
#define VERT_SYNC_IMM             0x00
#define VERT_SYNC_VSYNC           0x04
#define SW_RESET                  0x02
#define BUF_EN                    0x00
#define BUF_DI                    0x01
//
// MAX7456 VIDEO_MODE_1 register
#define VIDEO_MODE_1_WRITE        0x01
#define VIDEO_MODE_1_READ         0x81
//
// MAX7456 DM_MODE register
#define DM_MODE_WRITE             0x04
#define DM_MODE_READ              0x84
//
// MAX7456 DM_ADDRH register
#define DM_ADDRH_WRITE            0x05
#define DM_ADDRH_READ             0x85
//
// MAX7456 DM_ADDRL register
#define DM_ADDRL_WRITE            0x06
#define DM_ADDRL_READ             0x86
//
// MAX7456 DM_CODE_IN register
#define DM_CODE_IN_WRITE          0x07
#define DM_CODE_IN_READ           0x87
//
// MAX7456 DM_CODE_OUT register
#define DM_CODE_OUT_READ          0xB0
//
// MAX7456 FM_MODE register
#define FM_MODE_WRITE             0x08
#define FM_MODE_READ              0x88
//
// MAX7456 FM_ADDRH register
#define FM_ADDRH_WRITE            0x09
#define FM_ADDRH_READ             0x89
//
// MAX7456 FM_ADDRL register
#define FM_ADDRL_WRITE            0x0A
#define FM_ADDRL_READ             0x8A
//
// MAX7456 FM_DATA_IN register
#define FM_DATA_IN_WRITE          0x0B
#define FM_DATA_IN_READ           0x8B
//
// MAX7456 FM_DATA_OUT register
#define FM_DATA_OUT_READ          0xC0
//
// MAX7456 STATUS register
#define STATUS_READ               0xA0
#define STATUS_40_RESET_BUSY           0x40
#define STATUS_20_NVRAM_BUSY           0x20
#define STATUS_04_LOSS_OF_SYNC         0x04
#define STATUS_02_PAL_DETECTED         0x02
#define STATUS_01_NTSC_DETECTED        0x01

#if 1
// 07/04/2007 [mku] Pass 2 IC requires clearing OSD Black Level register bit 0x10 after reset
#define OSDBL_WR                 0x6C
#define OSDBL_RD                 0xEC
#define OSDBL_10_DisableAutoBlackLevel       0x10
#endif




void max7456_init();
void spiWriteReg(const unsigned char regAddr, const unsigned char regData);
unsigned char spiReadReg (const unsigned char regAddr);
void spiWriteCM();
void spiWriteFM();

int max756_read_status();


#endif
