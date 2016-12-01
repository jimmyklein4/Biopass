#NoEnv  ; Recom#NoEnv  ; Recommended for performance and compatibility with future AutoHotkey releases.
; #Warn  ; Enable warnings to assist with detecting common errors.
SendMode Input  ; Recommended for new scripts due to its superior speed and reliability.
SetWorkingDir %A_ScriptDir%  ; Ensures a consistent starting directory.

User=
pw=105040

	run, C:\Users\Michael\Desktop\Book2.xlsx
	winwaitactive,Excel,,5

	ifwinexist,Excel
	{	
		login(pw)
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


login(pass){

	blockinput,on
	sleep,1500
	sendInput, %pass%
	send,{enter}
	blockinput,off
}