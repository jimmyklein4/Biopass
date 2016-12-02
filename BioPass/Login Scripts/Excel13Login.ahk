#NoEnv  ; Recom#NoEnv  ; Recommended for performance and compatibility with future AutoHotkey releases.
; #Warn  ; Enable warnings to assist with detecting common errors.
SendMode Input  ; Recommended for new scripts due to its superior speed and reliability.
SetWorkingDir %A_ScriptDir%  ; Ensures a consistent starting directory.

User=
pw=

	run, C:\Program Files\Microsoft Office 15\root\office15\EXCEL.EXE
	winwaitactive,Excel,,5

	ifwinexist,Excel
	{	
		login(user,pw)
		winwaitactive,Excel,,5
		if ErrorLevel
		{
			blockinput,off
			return
		}
		
	}
	winactivate, Excel
	sleep, 5000
	return


login(name,pass){

	blockinput,on
	click,859,53,1
	sleep,1500
	sendInput, %name%
	send,{enter}
	sleep,2000
	sendInput,%pass%
	click,72,292,1
	send,{enter}
	blockinput,off
}