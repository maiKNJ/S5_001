@ECHO OFF

REG QUERY HKLM\SOFTWARE\NDI\Multicast\Send > nul
IF %ERRORLEVEL% equ 1 GOTO NOT_CONFIGURED

REG QUERY HKLM\SOFTWARE\NDI\Multicast\Send /v Mode | Find "0x1" > nul
IF %ERRORLEVEL% equ 1 GOTO NOT_CONFIGURED

REG QUERY HKLM\SOFTWARE\NDI\Multicast\Send /v NetMask | Find "255.255.255.0" > nul
IF %ERRORLEVEL% equ 1 GOTO NOT_CONFIGURED

REG QUERY HKLM\SOFTWARE\NDI\Multicast\Send /v NetPrefix | Find "239.255.10.0" > nul
IF %ERRORLEVEL% equ 1 GOTO NOT_CONFIGURED

ECHO This system is properly configured to support multicast streaming to Digistar.
EXIT /B 0

:NOT_CONFIGURED

ECHO This system is not configured correctly to support multicast streaming to Digistar. Please apply the 'ndimulticast.reg' registry file included with the DomeStream plugin.
EXIT /B 1

