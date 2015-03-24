package src;

import net.rim.device.api.ui.MenuItem;
import net.rim.device.api.ui.component.*;
import net.rim.device.api.ui.container.MainScreen;
import net.rim.device.api.ui.*;
import net.rim.device.api.system.*;


class OptionScreen extends MainScreen
{
    private static PersistentObject persistentStore;
    RadioButtonGroup group1 = new RadioButtonGroup();
    RadioButtonField button1;
    RadioButtonField button2;
    MenuItem Item1 = new MenuItem("Save",3,3){
                        public void run(){
                            save();
                            close();//exit the screen
                        }
                    };
    static EclStore StoredObject;
    //create radio buttons with the two options
    static{ 
        persistentStore = PersistentStore.getPersistentObject(PushedDataListener.RTSID_MY_APP);
        synchronized(persistentStore){
            if(persistentStore.getContents() instanceof EclStore)
                StoredObject = (EclStore)persistentStore.getContents();
        }
    }

    public OptionScreen(){
        
        this.setTitle(new RichTextField("Select Detail Display Method"));
            //initial setup
            button1 = new RadioButtonField("Last Name, First Name",group1,false);
            button2 = new RadioButtonField("First Name Last Name",group1,true);
       //there must be data prior to having options     
       if(StoredObject!=null&&StoredObject.displayOrder==0){
            button1.setSelected(true);
            button2.setSelected(false);
       }
        this.add(button1);
        this.add(button2);
        this.addMenuItem(Item1);
        
        
    }
    
    //over ride save() to persistent store the data
    public void save(){

        if(button2.isSelected())
                StoredObject.displayOrder=1;
            else
                StoredObject.displayOrder=0;
    
        synchronized (persistentStore) {
                    persistentStore.setContents(StoredObject);
                    persistentStore.commit();
        }
    }

    public boolean getCheck(){
        if(button1.isSelected()) return true;
        else return false;
    }
}
