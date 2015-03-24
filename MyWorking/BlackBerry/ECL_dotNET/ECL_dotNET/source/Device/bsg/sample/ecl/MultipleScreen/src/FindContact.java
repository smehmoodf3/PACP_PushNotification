
package bsg.sample.ecl.MultipleScreen.src;
import java.util.*;
import net.rim.device.api.ui.*;
import net.rim.device.api.ui.component.*;
/**
 * <description> Parses through the a given list field of all contacts to find the index of a selected contact
 */

class FindContact {
    /**
     * <description> empty constructor
     */
    FindContact() { }
    /**
     * <description>Gets the index of a selected contact from the full list
     * @param contacts <description> The selected contact
     * @param AllContacts <description> List of all contacts
     * @return <description> The index location of the selected contact
     */
    public int getContact(ObjectListField contacts, ObjectListField AllContacts){
          //get the selected contact
            int i =contacts.getSelectedIndex();
            String name=(String)contacts.get(contacts,i);
            //we need to parse through the list to get the vector location of the selected contact
            //only useable if someone is using the find field function on the DisplayScreen
            int count;
            int size=AllContacts.getSize();
            for(count=0;count<size;count++){
            String temp=(String)AllContacts.get(AllContacts,count);
            //the names are being matched to get the vector location
            if(name.equals(temp))break;
            }
            return count;
        
    }
    
} 
