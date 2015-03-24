package bsg.sample.ecl.MultipleScreen.src;
import java.util.*;
import net.rim.device.api.ui.container.MainScreen;
import net.rim.device.api.ui.component.*;
/**
 * <description>This class extends Main Screen and displays all contact information for a single contact
 */
class InfoScreen extends MainScreen
{
      /**
       * <description> The constructor parses through the contact's information and displays them as ActiveRichTextField.
       * @param location <description> The index of the vector to which the selected is located
       * @param contactList <description> The vector list that contains all contacts
       */
      public InfoScreen(int location, EclStore contactList){
                
            
            EclStore contactData = (EclStore)contactList.elementAt(location);
            //display their info
            int size = contactData.size();
            //the title is contact's name
            this.setTitle((String)contactData.elementAt(0));
            int j;
            //display all of the contact's info as active field
            for (j=1;j<size;j++){
                String information = (String)contactData.elementAt(j);
                information.trim();
                //if the field is empty no need to display it
                if(!information.endsWith(" ")) this.add(new ActiveRichTextField(information));
            }
      } 
}
