package bsg.sample.ecl.MultipleScreen.src;

import java.io.*;
import javax.microedition.io.*;
import net.rim.device.api.ui.*;
import net.rim.device.api.system.*;
import net.rim.device.api.util.*;
import net.rim.device.api.notification.*;
import net.rim.device.api.ui.component.Dialog;
/**
 * <description> This is a background thread that listens for incoming data.
 * The notification and adding ECL to the profile is also handled here
 */

class PushedDataListener extends UiApplication {

    public static final long RTSID_MY_APP = 0x11350933e0918291L;//bsg.application.emergencycontactlist.src
    public static final long ID_1=0xe87f8eb69cfcc11cL;//for notification--"ECLContactList.PushedDataListener"
    private static final String LISTEN_URL = "http://:911;deviceside=false"; //the listen port
    private ListenerThread myThread;
    private static final Object event = new Object(){
            public String toString(){return "Ecl";}
    };

    public PushedDataListener() {
        myThread = new ListenerThread();
    }

/******************************************************************************************
* acceptsForeground() - Prevents this entry point from showing up in the task switcher
* since this entry point has no user interface.
******************************************************************************************/
    protected boolean acceptsForeground()
    {
        return false;
    }

/******************************************************************************************
* BackGround waitForSingleton() - returns an instance of a listening thread
******************************************************************************************/
    public static PushedDataListener waitForSingleton(){
        //make sure this is a singleton instance
        RuntimeStore store = RuntimeStore.getRuntimeStore();
        Object o = store.get(RTSID_MY_APP);
        if (o == null){
            store.put(RTSID_MY_APP, new PushedDataListener());
            return (PushedDataListener)store.get(RTSID_MY_APP);
        } else {
            return (PushedDataListener)o;
        }
    }

/******************************************************************************************
* start() - starts the custom listen thread
******************************************************************************************/
    public void start(){
        invokeLater(new Runnable() {
            public void run() {
                myThread.start();
                NotificationsManager.registerSource(ID_1, event, NotificationsConstants.CASUAL);//registring the object event to profiles
            }
        });

        this.enterEventDispatcher();
    }

/******************************************************************************************
* customer listening thread that is an extention of thread()
******************************************************************************************/
    class ListenerThread extends Thread {

        public void run() {

            System.out.println("eclBackGroundThread -- running");

            StreamConnectionNotifier notify = null;
            StreamConnection stream = null;
            InputStream input = null;
            try{//wait for the device to load
                sleep(1000);
           }
           catch(Exception e){}
            for(;;) {//in case an exception is thrown, re try to get the data
                    try {

                        notify = (StreamConnectionNotifier)Connector.open(LISTEN_URL);

                        for(;;){

                            //NOTE: the following will block until data is received
                            stream = notify.acceptAndOpen();
                            input = stream.openInputStream();

                            //Extract the data from the input stream
                            StringBuffer sb = new StringBuffer();
                            int datum = -1;
                            while ( -1 != (datum = input.read()) )
                            {
                                    sb.append((char)datum);
                            }
                            stream.close();
                            stream = null;

                            String contactData = sb.toString();
                            DataStore dataStore = new DataStore();
                            dataStore.saveData(contactData);

                            System.err.println("Push message received...("+contactData.length()+" bytes)\n");
                            System.err.println(contactData);

                            //triggering a notification event since data has been received
                            NotificationsManager.triggerImmediateEvent(ID_1,0,this,null);//notifing the device of new content being received
                        }

                    }
                    catch (IOException e){
                            //likely the stream was closed
                            System.err.println(e.toString());
                    }
                    finally {
                            try {
                                    if (stream != null){
                                            stream.close();
                                    }
                            }
                            catch (Exception ex){}
                            try{
                                    if (notify != null){
                                            notify.close();
                                    }
                            }
                            catch (Exception ex){}
                    }//end finally
            }//end for
        }//end run
    }//end listenerthread
}//end pushdatalistener

