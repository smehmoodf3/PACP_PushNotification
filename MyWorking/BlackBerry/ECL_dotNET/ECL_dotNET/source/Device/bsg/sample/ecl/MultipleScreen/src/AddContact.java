package bsg.sample.ecl.MultipleScreen.src;
import java.util.*;
import net.rim.device.api.ui.*;
import net.rim.device.api.ui.component.*;
import javax.microedition.pim.*;
import net.rim.blackberry.api.pdap.*;
/**
 * <description> Adds the selected to the device's address book. This class is customized for this sample
 */
class AddContact
{
    /**
     * <description> The constructor gets the name of the contact to add and all its information.
     * 
     * @param ContactList <description> The vector that stores all contacts info
     * @param location <description> The index of the selected contact in that vector
     */
    public AddContact(int location,EclStore ContactList){
           //get the location of the selected contact to add
           EclStore contactData = (EclStore)ContactList.elementAt(location);
           int size = contactData.size();
           int i;
           //place the contact's information in a string array so it can be modified and added to the address book
           String [] information= new String[size];
           for(i=0;i<size;i++){
                information[i]=(String)contactData.elementAt(i);
                String temp = information[i];
                //since we only want contact data, the corresponding field names should be removed
                int index=temp.indexOf(":");
                if(temp.indexOf(":")!=-1){
                    information[i]=temp.substring(index+1);
                }
                //if a field is empty, display a blank field
                if(temp.equals(""))information[i]="";
           }
            try{
                newContact(information);
            }
            catch(Exception e){
                System.out.println(e.getMessage());
            }

    }
    /**
     * <description> Throws exception if the contact cannot be created and/or added
     * @param information <description> String array that contains the user name and information
     */
    public void newContact(String [] information) throws Exception{
        //get an instance of the PIM list to add a contact
        //the read and write allows us to read the address book list and write to it
        ContactList cl = (ContactList)PIM.getInstance().openPIMList(PIM.CONTACT_LIST, PIM.READ_WRITE);
        //a Contact Name contains multiple names, therefore get its appropriate size
        String []ContactName = new String [cl.stringArraySize(Contact.NAME)];
        //create the contact so it can be added
        Contact contact= cl.createContact();
        //seperate the given name from the family name
        String temp = information[0];
        //the space character indicates a break between the given name and family name
        int index = temp.indexOf(" ");
        String FirstName = temp.substring(0,index);
        String LastName = temp.substring(index+1);
        //update the contact name
        ContactName[Contact.NAME_GIVEN]= FirstName;
        ContactName[Contact.NAME_FAMILY]= LastName;
        //we need to see if this contact already exists, if it does remove it and re add it as 
        //information might have changed.
        Enumeration e;
            contact.addStringArray(Contact.NAME,PIMItem.ATTR_NONE,ContactName);
            //cl.items returns an enumeration of all matching contacts
            e= cl.items(contact);
            //if the contact exist, remove it
            if(e.hasMoreElements()){
                cl.removeContact((Contact)e.nextElement());
                
            }
            //add the remaining information about the contact
            contact.addString(Contact.TITLE,PIMItem.ATTR_NONE,information[1]);
            contact.addString(Contact.TEL,Contact.ATTR_WORK,information[2]);
            contact.addString(Contact.TEL,Contact.ATTR_MOBILE,information[3]);
            contact.addString(Contact.EMAIL,PIMItem.ATTR_NONE,information[4]);
            contact.addString(BlackBerryContact.PIN,PIMItem.ATTR_NONE,information[5]);
            contact.addString(Contact.TEL,Contact.ATTR_HOME,information[6]);
            contact.commit();

    }
    
}
