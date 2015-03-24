package bsg.sample.ecl.MultipleScreen.src;
import java.util.*;
import net.rim.device.api.ui.container.MainScreen;
import net.rim.device.api.ui.component.*;
import net.rim.device.api.system.KeyListener;
import net.rim.device.api.system.Characters;
/**
 * <description> The intial screen of ECL that displays all groups
 */
class EclScreen extends MainScreen
{
    ECLApplication _callBack;
     int numOfGroups;
     /**
      * <description> The constructor parses through the dataStore and creates a list field with all groups
      * @param dataStore <description> The persistable data store where the raw data exists
      * @param list <description> The objectListField that will showcase the all groups
      */
     public EclScreen(DataStore dataStore,ObjectListField list, ECLApplication callBack)
    {
        this.setTitle("Emergency Contact List");
        //retrieve the number of groups
         numOfGroups = dataStore.getNumOfGroups();
        for (int curGroup=0; curGroup <numOfGroups; curGroup++) {
            list.insert(curGroup, (String)dataStore.getGroupNameFromStore(curGroup));
        }
        _callBack = callBack;
        this.add(list);
    }
    /**
     * <description> Over writting keyChar function that is part of MainScreen.
     * When "Enter" or "Space" is pressed, the first menu item gets called
     * @param key <description>
     * @param status <description>
     * @param time <description>
     * @return <description>
     */
    public boolean keyChar(char key, int status, int time) {
       boolean retval = false;
       if(numOfGroups>0){//if no data, do nothing
            switch (key) {//in either case go on to the next screen
                case Characters.ENTER:
                    _callBack.display();
                    retval = true;
                    break;
                case Characters.SPACE:
                    _callBack.display();
                    retval = true;
                    break;
            }
       }
       return retval;
   }

}
