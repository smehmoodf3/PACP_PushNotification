                             EMERGENCY CONTACT LIST
                   An Example Push Application for BlackBerry


Overview
========

This Emergency Contact List (ECL) example makes contact data in a Microsoft Excel spreadsheet readable from BlackBerry handhelds.  ECL uses BlackBerry Push functionality to publish contact data to many devices.

There are two forms of the ECL demo; each involves a back-end program and a handheld program.  The back-end program gets invoked from the GUI to push the contact data to handhelds by communicating with the BlackBerry Enterprise Server.  The handheld program listens for incoming pushed updates and offers an icon on the device's ribbon to view the latest data that it received.

The form first uses Browser Channel Push, where the server encodes the contact data in an HTML page and pushes that page to the BlackBerry Browser.  When the browser receives a Channel Push, it stores the pushed content in its cache and adds an icon to the main ribbon that acts as a bookmark to that page.  In this form, the program on the handheld is the BlackBerry browser, so there is no ECL-specific code that needs to be installed on the handheld.

The second form pushes to a Client Catcher.  Here, the server encodes the contact data into a nonstandard format and sends that to a custom handheld application that must be installed on the user's BlackBerry.  When this catcher receives a pushed message, it stores the encoded data locally using the BlackBerry persistence framework.  The device's user interface component displays its locally stored copy of the contact list in a collapsable tree form.

By the end of the installation procedure that follows, the handheld is ready to receive data.  Running the server program (described in the section after) pushes data to the handheld.


How to Install the Demo
=======================

1.  Unzip the archive into a folder of your choice.  This can be done on any machine that has network access to your BlackBerry Enterprise Server.  The [source] subfolder contains dotNET source code for the server and device.  The [setup] subfolder contains the installation for different types of push engine: CommandLine, GUI, Services and WEB.  The [content] subfolder contains files that need to made accessible on your intranet.

2.  Under [setup\gui] isntall ECL_GUISetup.msi. The new installation directory will be c:\Program Files\Research In Motion\Emergency Contact List

3a.  (For Browser Channel operation.)  Arrange to have a copy of the [content] folder published as a web resource.  That is, you need to make it possible (from a BlackBerry) to fetch a URL such as "http://YOURSERVERNAME/ecl/content/ecl_unread_icon.gif" and obtain the data from [content\ecl_unread_icon.gif]. For defaault, place the content folder under [c:\inetpub\wwwwroot\ecl].

3b.  (For Custom Catcher operation.)  Open the Application Loader file [setup\Device\ECLContactList.alx] with your BlackBerry desktop software and follow the procedure for installing that program on your cradled handheld.  Once installed, the catcher is running in the background.  Alternatively, if using the simulator, open the workspace [source\Device\com\rim\application\GenericView\ECLContactList.jdw] in the BlackBerry Java Development Environment and press F5 to launch the device simulator; make sure that the MDS simulator is running too.


How to Run the Server
=====================

1.  Open the push engine found under Start -> Programs -> Research In Motion -> Emergency Contact List.

2.  The user information file , corresponds to user.txt found under [c:\Program Files\Research In Motion\Emergency Contact List]. The file Stores BESNAME BESPUSHPORT PINorEmail. Please modify it with the correct information if pushing to something other than the simulator.

3.  Select your Database source, by default its excel.

4.  (For Browser Channel operation.) Select Push Type as Browser-Channel. 

5.  For Excel and Access the sampe file can be found under [c:\Program Files\Research In Motion\Emergency Contact List]. For SQL, please input the server name and database name. Make Sure you run xyzlist.sql against your server to create the ECL database. By default the table name for all three source is Contacts.

6.  (For Browser Channel operation.) To provide your own unread and read gif, provide a "WebContextRoot." Since these ribbon icons are found under the "WebContextRoot" do not provide the full address. For example the full address of a Read_icon is http://localhost/ecl/content/ecl_read_icon.gif but in the properties set the "Read_Icon" value to equal "ecl_read_icon.gif." This is because the "WebContextRoot" defines the first half of the address.

7.  If you are using your own database, make sure the column format under settings corresponds to the number of columns of your database.

8.  (For Browser Channel operation.) Under settings, define the store location of ecl.html for page refersh from the BlackBerry. Make sure [c:\inetpub\ecl] is directory browseable and the read and write option is checked. This can be done under [folder properties -> Web Sharing] by sharing this folder.


For More Detail on all push engine, please read the Emergency Contact List - Indepth Guide.doc
=======================

