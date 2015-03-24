/*********************************************************************************************
* ECL APPLICATION                                                                            *
* INCLUDES A NEW METHOD OF VIEWING DATA ON THE CUSTOM CATCHER APPLICATION                    *   
* The main purpose of this development was that one only has to modify one java class to     *
* change the view on the custom app.                                                         *
*                                                                                            *
*Author: Rohit Gupta, Wireless Solution Anaylst                                              *
*Date: April 13/05                                                                           *
*********************************************************************************************/
package bsg.sample.ecl.MultipleScreen.src;
import java.util.*;
import net.rim.device.api.ui.*;
import net.rim.device.api.ui.component.*;

/**
 * <description> The main class that contains all menu items and instances of all screens.
 */

class ECLApplication extends UiApplication {

    private MenuItem _display1;
    private MenuItem _information;
    private MenuItem _addContact;
    DataStore dataStore;
    ObjectListField list= new ObjectListField(); 
    ObjectListField AllContacts = new ObjectListField();
    EclStore contactList;
    DisplayScreen screen2;
    int location;
    FindContact ContactLocation = new FindContact();
    /******************************************************************************************
    * main() - controls the startup of the application...thread will start in the background
    *           and the GUI starts when user clicks from main menu
    ******************************************************************************************/ 
    /**
     * <description> Starts the push data listener intially
     * @param args <description>
     */
       
    public static void main(String[] args) {
        if( args != null && args.length > 0) {
            ECLApplication theApp = new ECLApplication();
            theApp.enterEventDispatcher();
        }
        else {
            PushedDataListener.waitForSingleton().start();
        }
    }

    /******************************************************************************************
    * Constructor:
    *   builds the GUI and displays the fields
    ******************************************************************************************/  
    /**
     * <description> Creates all the relative menu items and pushes the first screen "EclScreen"
     */
       
    public ECLApplication() {
         //call the datastore class
        dataStore = new DataStore();
        //opens the persistent store and loads in the group list
        dataStore.loadGroupListFromStore();
            _display1 = new MenuItem("View Details",1,1){
                public void run(){
                    display();
            }
            };   
            _information = new MenuItem("Show Info", 1,1){
                public void run(){
                    displayInfo();
            }
            };  
            _addContact = new MenuItem("Add Contact",2,2){
                 public void run(){
                    addContact();
            }
            }; 
        EclScreen screen1 = new EclScreen(dataStore,list, this);
        if(dataStore.getNumOfGroups()>0){//if no data, don't add the menu item
            screen1.addMenuItem(_display1); 
        }
        pushScreen(screen1);
    }
    /**
     * <description> Passes all necessary data to the second screen and pushes the second screen (DisplayScreen) on the stack.
     * This function also adds the menu item for the second screen and is called by "View Details" menu item
     */
    public void display(){
        //create the DisplayScreen, by passing the selected group
        screen2 = new DisplayScreen(list,dataStore, this);
        //add the menu items
        screen2.addMenuItem(_information);
        screen2.addMenuItem(_addContact);
        //update the contact list
        contactList = screen2.getContactList();
        AllContacts=screen2.getSelectedContacts();
        AllContacts.setSearchable(true);
        pushScreen(screen2);
    } 
    /**
     * <description> Function is called by "Show info" menu item. It creates the InfoScreen and pushes it on the stack.
     */
    public void displayInfo(){
       //we require the selected contact's index
       location = ContactLocation.getContact(screen2.getSelectedContacts(),AllContacts);  
       //given the location, parse the vector to get the related information  
       InfoScreen screen3 = new InfoScreen(location,contactList);
       pushScreen(screen3);
    }   
    /**
     * <description> Functions is called by "Add Contact" menu item. Calls the AddContact class to add the selected
     * contact in the device's addressbook. 
     */
    public void addContact(){
        //we require the selected contact's index
        location = ContactLocation.getContact(screen2.getSelectedContacts(),AllContacts);
        //given the location, parse the vector to get the related information  
        AddContact Add = new AddContact(location, contactList);
        
    }
   
  
}
