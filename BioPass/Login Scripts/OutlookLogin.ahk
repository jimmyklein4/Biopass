#NoEnv  ; Recom#NoEnv  ; Recommended for performance and compatibility with future AutoHotkey releases.
; #Warn  ; Enable warnings to assist with detecting common errors.
SendMode Input  ; Recommended for new scripts due to its superior speed and reliability.
SetWorkingDir %A_ScriptDir%  ; Ensures a consistent starting directory.

User=
pw=

	run, C:\ProgramData\Microsoft\Windows\Start Menu\Programs\Microsoft Office 2013
winwaitactive,Outlook,,5

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
	winwait, Outlook Today - Outlook,,5
	if ErrorLevel
	{
		blockinput,off
		return
	}
	winactivate,Outlook Today - Outlook
	winwaitactive,Outlook Today - Outlook
	sleep, 500
	setkeydelay,15,15
	coordmode, mouse, window
	blockinput,on
	click 60,167,3
	sendInput, {Backspace}
	sendInput, %name%
	sleep,200
	sendInput,{Tab}
	sleep,200
	sendInput, %pass%
	sleep, 200
	send, {enter}
	blockinput,off
}