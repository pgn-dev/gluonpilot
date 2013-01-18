
; from http://www.microchip.com/forums/tm.aspx?high=&m=428375&mpage=1#428561
;
 ; Storage for the exception return address
 ;
 .section *,bss,near
 .global __AbortAddressContainer
 __AbortAddressContainer: .space 4  ; address where the exception occurred
                          .space 2  ; previous word of stack data
 ;
 ; Function to read exception return address
 ; from the stack, plus the previous word
 ;
 .text
 .global _where_was_i

 _where_was_i:

         push.d w0
         sub    w15,#12,w1                ; twelve bytes pushed since last trap!
         mov    [w1], w0
         mov    w0, __AbortAddressContainer
         mov    [w1+2], w0
         mov.b  WREG, __AbortAddressContainer+2
         mov    [w1-2], w0
         mov    w0, __AbortAddressContainer+4
         pop.d  w0
         return

 ;
 ; Tabulation of stack usage since the exception
 ; occurred (read from bottom to top).
 ;
 ; current SP +12
 ;
 ; push w1 +10
 ; where_am_i push w0 +8
 ;
 ; rcall in PC 22->16 +6
 ; handler PC 15->0 +4
 ;
 ; PC 22->16 +2
 ; TRAP! PC 15->0 +0
 