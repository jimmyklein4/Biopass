#README#

BioPass - BioMetric Verification Authentication System

BioPass is a program designed to allow a user to log into a variety of 
applications and services from a catalogued list via biometric authentication.
Merely submitting the text-based authentication information for a specific service 
or application allows for security breaches and the possibility of many users the 
ability to access a single user’s account. Authentication via biometric analysis 
allows for a secure solution to this issue and ensures that only a registered and 
verified user may access a specific service or application, as well as removing 
the burden of maintaining separate passwords for each. BioPass is user friendly 
and allows a user to bypass the hassles and potential hazards of navigating to 
a specific service or application and manually entering text-based login information.

 
	 Application Functionality

BioPass allows a user to log into select applications that are supported by the BioPass application in fully 
automated fashion. User’s must verify their identity via various forms BioMetric verification based on desired
 security level, specifically some combination of fingerprint, facial, and iris biometrics. 

Additionally user’s can update existing credentials for these applications once they are logged in. They can view 
all applications that are supported by BioPass by pressing the Show Apps button, which then sorts these applications 
into separate lists depending on whether or not the logged in user has credentials saved for these applications. 
Pressing this button again, which should now be labeled Hide Apps, will hide this view. The apps list is presented in
an interactive view, meaning a user can click any application to edit the credentials of selected application.
User’s can press the Security Level  button to select high, medium, or low security forms of BioMetric authentication 
to undergo. At time of login the predefined security level begins a linear BioMetric Verification process, which prompts 
a user to input, depending on their BioMetric selections, either a real-time fingerprint scan or a real-time facial 
picture. These inputs will then be compared to the BioMetric collections stored within the SQLite database that BioPass
 utilizes. User’s may forego one form of BioMetric authentication by inputting their pin.





Getting Started

	 BioPass Installation
The installation of BioPass is fully automated. If you are not operating under a Windows Environment 
then installation will be aborted. It is only available in English and it creates a database based on a 
build script to allow for such automation. 

After you install BioPass, the application will recognize that there is no data in the database, 
thus prompting the user to register. If you do not have a webcam or other computer-connected camera, 
as well as a fingerprint scanner, then initial BioMetric Data Collection at time of registration will 
not be possible, and thus aborted when attempted. 

	 BioPass Uninstall
When uninstalling BioPass all local data will be lost. Database information will be deleted, which includes; 
BioMetric data collections, user specific application credentials, and user account information.. 
See Backup and Recovery section of the User Manual if you wish to maintain this data when uninstalling.




System Requirements

Users of this product will be running within a Windows operating system, have webcam or 
other computer-connected camera, and a fingerprint scanner.





Configuration

	User registration
To register a user to the BioPass application simply press the Add Account button.You will then be prompted 
to enter your name and you will be assigned a pin number, unique to the user. BioPass does not ask user’s to 
select a password, but this unique pin number essentially serves as an assigned password for users. BioPass 
will then collect a series of BioMetric Data unique to the user for future BioMetric comparative purposes. 

	BioMetric Data Collection
After entering your user name and being assigned your pin number, Initial BioMetric Data Collection will begin. 
Ten images of the user’s index finger and ten images of the user’s face will be collected and stored within our 
SQLite Database. This data will then be used for user verification based entirely on the identification of a user’s 
BioMetric properties by comparing realtime images to that of the user input in this initial BioMetric Data Collection. 
There are three forms of BioMetric verification that BioPass utilizes; fingerprint, facial, and iris. 

	Database 
Installation and maintenance is fully automated. The database is built at time of installation via build scripts.  
Data is stored within a SQLite database. Data within the SQLite database is all some form of user input, excluding the
 user pin number, which is created and assigned by BioPass application before it is entered into the database. BioPass 
will store and transmit all digital identification data securely. Data is encrypted via the Data Protection API and 
stored within SQLite. No personal user data is sent unencrypted through web. Users cannot directly access data stored
 within database.

	Logging in
After the initial user data collection a user will be able to log into catalogued applications supported by BioPass in 
fully automated fashion following a variety of forms of BioMetric verification based upon desired security level. 
Following successful verifications of a user’s identity, a user will select an application to start. If the selected 
application is catalogued then the selected application will launch and be logged into by way of retrieving 
usernames/password from our SQLite database and placing this information into the associated fields of the selected 
application.
User verification is performed by manner of undergoing two of the three forms of BioMetric identification. As an 
alternative to one form of BioMetric verification, a user may verify their identity by entering their unique user-pin, 
which is assigned at time of account creation.

	Application Credentials
After the initial user data collection you will then be able to enter your credentials for select applications supported 
by BioPass. Credentials can be updated at anytime by pressing the Show Apps button and selecting the application you wish 
to edit from the list. You must be logged in to BioPass in order to edit credentials of any kind.

 	Backup and Recovery
A local copy of the database is stored on the user's computer. Hardware specifications are saved in a text file after 
first successful run and identification of hardware. User’s may revert to original installation at anytime with a copy 
of all data placed into the database available. To do so delete the BioPass application and reinstall.
	
	Support
If you require further assistance resolving an issue please contact support at BioPass@Temple.Edu with a description of 
your query.





Versioning

We use BitBucket GIT repository for versioning control.
https://bitbucket.org/TheoHauser/biopass





Developers
Sam Yelman - Project Lead & Fingerprint Verification Development
Michael Hoffman - Desktop Automation Development
Theo Hauser - Web Automation Development
Robert Woods - Iris Verification Development
James Klein - Facial Verification Development





License

This project is unlicensed and available for use in an opensource manner.




Built With Visual Studio IDE in C# 
Testing,API References, Development Plan can be found in the Documentation Folder included with this project.