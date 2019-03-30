# IIS Development Proxy for BCeID

The website in this folder is configured to act as a fake implementation of BCeID for development purposes.  These instructions assume that you are using Windows, and Visual Studio. 

## Install prerequisites

1. Install IIS on Windows by turning on the "Internet Information Services" feature on Windows Features. 

2. Install IIS extension url-rewrite. [LINK](https://www.iis.net/downloads/microsoft/url-rewrite)

3. Install IIS extension application-request-routing. [LINK](https://www.iis.net/downloads/microsoft/application-request-routing)


## Enable the proxy feature

1. Open IIS (if already open, close and open again)

2. At the ROOT LEVEL in the tree, look for the "Application Request Routing" feature under IIS section of "Features View"

3. Double click the "Application Request Routing Cache"  feature and on the right section, click on "Server Proxy Settings"

4. Check the box "Enable Proxy" and "Apply" the action (on the right section)


## Create a website

1. In IIS, right click on "Sites" and add a new website 

	* Site name: Anything you want (e.g. BCEID.IIS.Dev.Proxy)
	* Physical path: path to the folder dev-tools\BCEID.IIS.Dev.Proxy
	* Host name: scj-booking.local

2. Edit your `C:\Windows\System32\drivers\etc\hosts` file and add this entry

	```
	127.0.0.1       scj-booking.local
	```


## Add Allowed Server Variables

1. Select the website you created in IIS

2. Open the "Url Rewrite" feature and click "View Server Variables" on the right side.

3. Add these 2 entries to the Allowed Server Variables 

	* HEADER_smgov_userguid
	* HEADER_smgov_userdisplayname

## Run the website in Visual Studio access the proxy from your browser

1. The proxy configuration assumes that either IIS Express or Kestrel is running the website on HTTP port 5000.  

	- Start the webite using "Run" or "Debug" or "Start Without debugging" or whatever you prefer.

2. Go to [http://scj-booking.local](http://scj-booking.local) in your browser!
