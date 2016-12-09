#NoEnv  ; Recom#NoEnv  ; Recommended for performance and compatibility with future AutoHotkey releases.
; #Warn  ; Enable warnings to assist with detecting common errors.
SendMode Input  ; Recommended for new scripts due to its superior speed and reliability.
SetWorkingDir %A_ScriptDir%  ; Ensures a consistent starting directory.

User=
pw=

	run, C:\Program Files\Microsoft Office 15\root\office15\MSACCESS.EXE
	winwaitactive,Access,,5

	ifwinexist,Access
	{	
		login(user,pw)
		winwaitactive,Access,,5
		if ErrorLevel
		{
			blockinput,off
			return
		}
		
	}
	winactivate, Access
	sleep, 5000
	return


login(name,pass){

	blockinput,on
	click,1120,49,1
	sleep,2500
	sendInput, %name%
	send,{enter}
	sleep,2000
	sendInput,%pass%
	click,72,292,1
	send,{enter}
	blockinput,off
}