#NoEnv  ; Recom#NoEnv  ; Recommended for performance and compatibility with future AutoHotkey releases.

User=		
pw=
  	Runwait, taskkill /im Discord.exe /f
	sleep,2000


	run, C:\Users\Michael\AppData\Local\Discord\Update.exe --processStart Discord.exe
	winwaitactive,Discord,,5

	ifwinexist,Discord
	{
		winActivate, Discord
		sleep,10000
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