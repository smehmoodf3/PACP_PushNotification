Requriments for testing


Run PAP.sql in an appropriate database and if using sql as source run xyzlist.sql with its dataset found in sampledata.txt


Following changes to be made in web.config **********************************************************************

Modify the location of the source to be pushed, it can be excel, access, sql (similar to other versions of ECL).

Modify the value for "DataAddress" to the location where the PAP database resides.

Modify the value for "NotifyURL" to the location where "PAPListener.aspx" resides.

Modify the value for "PAPAddress" to the location wehre "pap.txt" resides.
*******************************************************************************************************************


To modify the coulmn format, it can be found in ECL.aspx.cs as it is currently hardcoded.
