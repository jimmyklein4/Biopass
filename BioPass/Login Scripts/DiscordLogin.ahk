#NoEnv  ; Recom#NoEnv  ; Recommended for performance and compatibility with future AutoHotkey releases.
; #Warn  ; Enable warnings to assist with detecting common errors.
SendMode Input  ; Recommended for new scripts due to its superior speed and reliability.
SetWorkingDir %A_ScriptDir%  ; Ensures a consistent starting directory.

User=mhoff145@gmail.com			
pw=105040mph

	
  	Runwait, taskkill /im Discord.exe /f
	sleep,2000
	run, C:\Users\Michael\AppData\Local\Discord\Update.exe --processStart Discord.exe
	winwaitactive,Discord,,5

	ifwinexist,Discord
	{
		winActivate, Discord
		sleep,8000
		logout()
		sleep, 5000
		login(user,pw)
		winwaitactive,Discord,,5
		if ErrorLevel
		{
		blockinput,off
			return
		}
		
	}
	winactivate, Discord
	sleep, 5000
	return


login(name,pass){

	blockinput,on
	click,721,264,1
	sendInput, %name%
	send,{tab}
	sendInput, %pass%
	send,{enter}
	blockinput,off
}

logout(){
	blockinput,on
	click,296,694,1
	sleep,500
	click,473,625,1
	sleep,100
	click,534,619,1
	blockinput,off	
}