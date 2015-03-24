package bsg.sample.ecl.MultipleScreen.src;
import net.rim.device.api.system.*;
import net.rim.device.api.util.*;
import java.util.*;
/**
 * <description> Stores the data on the device in EclStore format
 */
class DataStore {
 
    private String rawData;
    private static EclStore storeData;
    
    private static PersistentObject persistentStore;
    static {
        //open the persistent store using the SID
        persistentStore = PersistentStore.getPersistentObject(PushedDataListener.RTSID_MY_APP);

        synchronized (persistentStore) {
            //if there is no persistent storage, create it
            if (!(persistentStore.getContents() instanceof EclStore)) {
                persistentStore.setContents(new EclStore());
                persistentStore.commit();
            }
        }
    }    
    
    public DataStore() {
    }
    
/******************************************************************************************
* saveData(String) - recieves the data after the push and stores it
******************************************************************************************/
    public void saveData(String data) {
        
        rawData = data;
       
        
        EclStore updatedContactsList = new EclStore();

        String groupName = readNextGroupNameFromRawData();
        while (groupName != null) {
            
            EclStore groupEntry = new EclStore();
            groupEntry.addElement(groupName);
            EclStore groupMembers = new EclStore();
            groupEntry.addElement(groupMembers);
            
            EclStore contactData = readNextContactFromRawData();
            while (contactData != null) {
                groupMembers.addElement(contactData);
                contactData = readNextContactFromRawData();
            }
            groupMembers.trimToSize();

            updatedContactsList.addElement(groupEntry);

            groupName = readNextGroupNameFromRawData();
        }
        updatedContactsList.trimToSize();


        //save the data to the store
        synchronized(persistentStore) {
            storeData = updatedContactsList;
            persistentStore.setContents(storeData);
            persistentStore.commit();
        }
    }

/******************************************************************************************
* String readNextGroupNameFromRawData() - pulls out the group name from the data and returns
*    the group name.  We also remove the group name from the data string
******************************************************************************************/
    private String readNextGroupNameFromRawData() {
        
        // Check whether there are more groups to read
        if (!rawData.startsWith("NEXT_GROUP;")) {
            return null;
        }
        
        rawData = rawData.substring(11);
        
        //find the first semi-colin which is the end of the group name
        int finishIndex = rawData.indexOf(';');
        String groupName = rawData.substring(0,finishIndex);
        
        //now remove the groupname from the raw data
        rawData = rawData.substring(finishIndex+1);
       
        return groupName;
    }
    
/******************************************************************************************
* EclStore readNextContactFromRawData() - pulls out a single contact from the raw data.  Again
*       removes that contact from the rawData string.
******************************************************************************************/
    private EclStore readNextContactFromRawData() {
        
        // Check whether we are at the end of the contacts for the current group
        if (!rawData.startsWith("NEW_CONTACT;")) {
            return null;
        }
        
        rawData = rawData.substring(12);
        
        //serach for the next contact/group begin token
        int newContactStartIndex = rawData.indexOf("NEW_CONTACT;");
        int nextGroupStartIndex = rawData.indexOf("NEXT_GROUP;");
        
        String contactData;
        
        if (nextGroupStartIndex >= 0  &&  ( newContactStartIndex < 0  ||  nextGroupStartIndex < newContactStartIndex )) {
            // the current contact is the last in a group and another group follows: read as far as the next NEXT_GROUP
            contactData = rawData.substring(0, nextGroupStartIndex);
            rawData = rawData.substring(nextGroupStartIndex);
        } else if (newContactStartIndex >= 0) {
            // the current contact is not last in its group: read as far as the next NEW_CONTACT
            contactData = rawData.substring(0, newContactStartIndex);
            rawData = rawData.substring(newContactStartIndex);
        } else {
            // the current contact is last in its group, which is last in the raw data: read to the end
            contactData = rawData;
            rawData = "";
        }
        
        EclStore contactInfo = new EclStore();

        int startIndex = 0;
        int endIndex;

        while ( (endIndex = contactData.indexOf(";",startIndex)) != -1 ) {
            contactInfo.addElement(contactData.substring(startIndex,endIndex));
            startIndex = endIndex + 1;
        }
        
        contactInfo.trimToSize();
        return contactInfo;
    } 
    
/******************************************************************************************
* loadGroupListFromStore() - reads the data from the persistent storage
******************************************************************************************/
    public void loadGroupListFromStore() {
        synchronized(persistentStore) {
            storeData = (EclStore)persistentStore.getContents();
        }
    }
/******************************************************************************************
* String getGroupNameFromStore(int) - reads a groupname out and returns a string
******************************************************************************************/
    public String getGroupNameFromStore(int count) { 
        EclStore groupInfo = (EclStore)storeData.elementAt(count);
        return (String)groupInfo.elementAt(0); //the first element holds a string
    }
    
/******************************************************************************************
* EclStore getContactList(int) - gets the contact list for a particular group
******************************************************************************************/
    public EclStore getContactList(int count) {
        EclStore groupInfo = (EclStore)storeData.elementAt(count);
        return (EclStore)groupInfo.elementAt(1); //the second element holds the list of contacts
    }

/******************************************************************************************
* int getNumOfGroups() - returns the size of the groupList
******************************************************************************************/
    public int getNumOfGroups() {
        return storeData.size();
    }
}//end of DataStore