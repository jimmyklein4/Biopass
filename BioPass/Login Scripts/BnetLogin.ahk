#NoEnv  ; Recommended for performance and compatibility with future AutoHotkey releases.
; #Warn  ; Enable warnings to assist with detecting common errors.
SendMode Input  ; Recommended for new scripts due to its superior speed and reliability.
SetWorkingDir %A_ScriptDir%  ; Ensures a consistent starting directory.

User=
pw=

	run, C:\Program Files\Battle.net\Battle.net Launcher.exe
winwaitactive,Battle.net,,5

	ifwinexist,Battle.net Login
	{	
		login(user,pw)
		winwaitactive,Battle.net,,5
		if ErrorLevel
		{
			blockinput,off
			return
		}
		
	}
	winactivate, Battle.net
	sleep, 5000
	return


login(name,pass){
	winwait, Battle.net Login,,5
	if ErrorLevel
	{
		blockinput,off
		return
	}
	winactivate,Battle.net Login
	winwaitactive,Battle.net Login
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