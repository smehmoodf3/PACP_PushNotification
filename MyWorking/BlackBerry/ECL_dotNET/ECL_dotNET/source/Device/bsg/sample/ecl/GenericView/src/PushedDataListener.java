package bsg.sample.ecl.GenericView.src;

import java.io.*;
import javax.microedition.io.*;
import net.rim.device.api.ui.*;
import net.rim.device.api.system.*;
import net.rim.device.api.util.*;

class PushedDataListener extends UiApplication {

    public static final long RTSID_MY_APP = 0x56b19e51d45ff827L;
    private static final String LISTEN_URL = "http://:911"; //the listen port
    private ListenerThread myThread;

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
            try{
                sleep(1000);
           }
           catch(Exception e){}

            try {

                notify = (StreamConnectionNotifier)Connector.open(LISTEN_URL);

                for(;;) {

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
                }
            } catch (IOException e){
                //likely the stream was closed
                System.err.println(e.toString());
            } finally {
                try {
                    if (stream != null) {
                        stream.close();
                    }
                } catch (Exception ex) {
                }
                try {
                    if (notify != null) {
                        notify.close();
                    }
                } catch (Exception ex) {
                }
            }
        }
    }
}

