package bsg.sample.ecl.GenericView.src;

import java.io.*;
import java.util.*;
import javax.microedition.io.*;
import net.rim.device.api.util.*;
import net.rim.device.api.system.*;
import net.rim.device.api.ui.container.*;
import net.rim.device.api.ui.*;
import net.rim.device.api.ui.component.*;

class ECLApplication extends UiApplication {

    private MenuItem _copyItem;

    /******************************************************************************************
    * main() - controls the startup of the application...thread will start in the background
    *           and the GUI starts when user clicks from main menu
    ******************************************************************************************/    
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
    public ECLApplication() {
       
        _copyItem = new MenuItem("Copy", 200000, 10) {
            public void run() {
                copyTextToClipboard();
            }
        };        

        //call the datastore class
        DataStore dataStore = new DataStore();
        //opens the persistent store and loads in the group list
        dataStore.loadGroupListFromStore();
        
        EclScreen screen = new EclScreen(dataStore);
        pushScreen(screen);
    }
    
    private class EclScreen extends MainScreen
    {
        public EclScreen(DataStore dataStore)
        {
            setTitle("Emergency Contact List");

            //retrieve the number of groups
            int numOfGroups = dataStore.getNumOfGroups();
        
            //used for the treefields
            TreeField tf;
            String contactName, contactField;
            int groupNode;
            int contactNode;
            int numOfContacts;

            for (int curGroup=0; curGroup <numOfGroups; curGroup++) {
                //build the treefield
                tf = new TreeField(new TreeCallback(),Field.FOCUSABLE);
                groupNode = tf.addChildNode(0,dataStore.getGroupNameFromStore(curGroup));
                
                //get the contacts under the group
                Vector contactList = dataStore.getContactList(curGroup);
                
                for(int curContact = contactList.size()-1;  curContact >= 0 ;  curContact--) {
                    Vector contactData = (Vector)contactList.elementAt(curContact);
                    
                    //add the first data field as a parent node for the current subtree
                    contactName = (String)contactData.elementAt(0);
                    contactNode = tf.addChildNode(groupNode,contactName);
                    tf.setExpanded(contactNode,false);
                    
                    //add the second data field beneath the parent
                    tf.addChildNode(contactNode,(String)contactData.elementAt(1));
                    
                    //now get the contact data and add to the tree
                    // started at the 3rd column as we have the first two there already
                    for (int i = 2; i<contactData.size(); i++) {
                        contactField = (String)contactData.elementAt(i);
                        if (!contactField.equals("")) {
                            tf.addSiblingNode(++contactNode,contactField);
                        }
                    }
                }
    
                //add the tree field to the screen
                tf.setExpanded(groupNode,false);
                add(tf);
            }
        }
    
        /******************************************************************************************
        * makeMenu(Menu,int) - creates a custom menu to add user define fields
        ******************************************************************************************/
        protected void makeMenu(Menu menu, int instance)
        {
            menu.add(_copyItem);
            menu.addSeparator();
            super.makeMenu(menu, instance);
        }
    }

    /******************************************************************************************
    * copyTextToClipboard() - copies the currently highlighted text to the clipboard so allow
    *   a user to paste pins, phone #'s later.
    ******************************************************************************************/ 
    private void copyTextToClipboard() {
         TreeField focus = (TreeField)UiApplication.getUiApplication().getActiveScreen().getLeafFieldWithFocus();
         if(focus != null) {
            int node = focus.getCurrentNode();
            String fieldText = (String)focus.getCookie(node);
            
            //check to see if there is a header for this field
            //if so, we don't want to copy that
            int startIndex = fieldText.indexOf(':');
            if (startIndex != -1)
                fieldText = fieldText.substring(startIndex+1).trim(); //add a 1 so we don't grab the colon
            
            //get the systems clipboard and add the text to it
            Clipboard clipBoard = Clipboard.getClipboard();
            clipBoard.put(fieldText);
        }
    }    

    /******************************************************************************************
    * The implementation of a TreeFieldCallback
    ******************************************************************************************/    
    private class TreeCallback implements TreeFieldCallback {
        public void drawTreeItem(TreeField tree, Graphics g, int node, int y, int width, int indent) {
            String text = (String)tree.getCookie(node);
            g.drawText(text, indent, y);
        }
    }
}
