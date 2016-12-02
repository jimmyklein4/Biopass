#NoEnv  ; Recom#NoEnv  ; Recommended for performance and compatibility with future AutoHotkey releases.
; #Warn  ; Enable warnings to assist with detecting common errors.
SendMode Input  ; Recommended for new scripts due to its superior speed and reliability.
SetWorkingDir %A_ScriptDir%  ; Ensures a consistent starting directory.

User=
pw=

	run, C:\Program Files\Microsoft Office 15\root\office15\OUTLOOK.EXE
	winwaitactive,Outlook Today - Outlook,,5

	ifwinexist,Outlook Today - Outlook
	{	
		login(user,pw)
		winwaitactive,Outlook,,5
		if ErrorLevel
		{
			blockinput,off
			return
		}
		
	}
	winactivate, Outlook
	sleep, 5000
	return


login(name,pass){

	blockinput,on
	click 30,39,1
	click 87,302,1
	sleep,1500
	click 217,282,1
	sleep,1500
	sendInput, %name%
	send,{enter}
	sleep,2000
	sendInput,%pass%
	click,72,292,1
	send,{enter}
	blockinput,off
}