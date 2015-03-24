package bsg.sample.ecl.MultipleScreen.src;
import java.util.*;
import net.rim.device.api.ui.container.*;
import net.rim.device.api.ui.component.*;
import net.rim.device.api.ui.*;
import net.rim.device.api.system.Characters;
/**
 * <description> The second screen, displays contacts under a particular group. Also holds the find field.
 */

class DisplayScreen extends MainScreen implements FieldChangeListener{

    EclStore contactList;
    ECLApplication _callBack;
    ObjectListField showContacts;
    FindEditField findField = new FindEditField();
    ObjectListField AllContacts = new ObjectListField();
    ObjectListField filterContacts = new ObjectListField();
    HorizontalFieldManager hfm = new HorizontalFieldManager();

   /**
    * <description> Constructor displays all contacts under a particular group and adds the find field
    * @param list <description> Required to find which group was selected, passed on by the first screen
    * @param dataStore <description> Required to get all contact data under a particular group
    */
    public DisplayScreen(ObjectListField list, DataStore dataStore, ECLApplication callBack){
            //add a change listener to the findfield for key inputs
            findField.setChangeListener(this);
            showContacts= new ObjectListField();
            //adding the find field as a horizontal manager
            hfm.add(findField);
            this.add(hfm);
            this.add(new SeparatorField());

            _callBack = callBack;

            String contactName;
            //get the selected group
            int i=list.getSelectedIndex();
            this.setTitle((String)list.get(list,i));
            contactList= dataStore.getContactList(i);
            int size = contactList.size();
            //get the contacts name
            for(int curContact = 0;  curContact<size;  curContact++) {
                EclStore contactData = (EclStore)contactList.elementAt(curContact);
                contactName = (String)contactData.elementAt(0);
                showContacts.insert(curContact,(String)contactName);

            }
            //add the contacts to the screen
            showContacts.setSearchable(true);
            AllContacts=showContacts;
            this.add(showContacts);
    }
     /**
      * <description> Returns the EclStore of All Contacts for a group
      * @return <description>
      */
    public EclStore getContactList(){
        return contactList;
    }
    /**
     * <description> Returns the list of contacts that are currently displayed
     * @return <description>
     */
    public ObjectListField getSelectedContacts(){
         return showContacts;
    }
    /**
     * <description>Over writting the function and is invoked when an action occurs to the find field
     * @param Field <description> the find field
     * @param context <description> same as super
     */
    public void fieldChanged(Field Field, int context){
        //if it was not done programmatically that means the user typed something
        if (context != FieldChangeListener.PROGRAMMATIC) {
            //concainating to the proper FindEditField
            FindEditField textField = (FindEditField)Field;
            //since a keystroke occured filter the list
            filtersearch(textField.getText());
        }
    }
    /**
     * <description> Over writting the function for when enter is pressed to invoke the first menu item
     * @param ch <description>
     * @param status <description>
     * @param time <description>
     * @return <description>
     */
    protected boolean keyChar(char ch, int status, int time) {
        boolean retval = super.keyChar(ch,status,time);
        //if the enter key is pressed, jump to the next screen
        if(ch==Characters.ENTER)
            _callBack.displayInfo();
        return retval;
    }
    /**
     * <description> Called when the find field is invoked. Matches the character from the find field and displays the revelant
     * contacts.
     * @param text <description> The text typed in the find field
     */
    public void filtersearch(String text){
        //in case a user presses enter the "\n" gets added on to the string
        //therefore by removing it we can still show a proper filter list
        if(text.indexOf("\n")>0) text = text.substring(0,text.indexOf("\n"));
        //delete the content that is there and re add it at the end
        this.delete(showContacts);
        int j=0;
        int maincount=AllContacts.getSize();
        int filtercount=filterContacts.getSize();
        //check if the contact is already in the list
        for(int k=0;k<filtercount;k++){
                //after a deleted objectlistfield shifts the following rows up
               filterContacts.delete(0);
        }
        //we need to repaint the screen, so deleting everything
        if(text.equals("")){
            showContacts=AllContacts;
        }
        //we need to add a new objectlistfield since there is something in the find field
         else {

            for(int i=0;i<maincount;i++){
                //get a contact
                String contact = (String)AllContacts.get(AllContacts,i);
                contact=contact.toUpperCase();
                text=text.toUpperCase();
                //see if the contact starts with the text
                if(contact.startsWith(text)){

                    filterContacts.insert(j++,(String)AllContacts.get(AllContacts,i));
                }
            }
            //update the screen with the new filter list
            showContacts=filterContacts;

        }
        //add the appropriate list
        this.add(showContacts);
    }
    /**
     * <description> Close when escape key is pressed
     * @return <description>
     */
    public boolean onClose(){
        //if escape is pressed, close the screen
        this.close();
        return true;
    }
}
