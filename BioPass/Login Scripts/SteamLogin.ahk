#NoEnv  ; Recommended for performance and compatibility with future AutoHotkey releases.
; #Warn  ; Enable warnings to assist with detecting common errors.
SendMode Input  ; Recommended for new scripts due to its superior speed and reliability.
SetWorkingDir %A_ScriptDir%  ; Ensures a consistent starting directory.

User=
pw=

	run, C:\Program Files\Steam\Steam.exe
winwaitactive,Steam,,5

	ifwinexist,Steam Login
	{	
		login(user,pw)
		winwaitactive,Steam,,5
		if ErrorLevel
		{
			blockinput,off
			return
		}
		
	}
	winactivate, Steam
	sleep, 5000
	return


login(name,pass){
	winwait, Steam Login,,5
	if ErrorLevel
	{
		blockinput,off
		return
	}
	winactivate,Steam Login
	winwaitactive,Steam Login
	sleep, 500
	setkeydelay,15,15
	coordmode, mouse, window
	blockinput,on
	click 335,100,2
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