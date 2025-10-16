using System;
using System.Collections.Generic;
using System.Text;

namespace Db_Core
{
    public enum SqlConnString
    {
        CSM_APP
    };

    public abstract class SqlCommon
    {
        public static string[] ConnectionStrings = new string[]
        {
            // localhost connection using root user
             //"Data Source=localhost; Initial Catalog = csm; User Id = root; password=pass"
             
            // localhost connection using custom user
             "Data Source=localhost; Initial Catalog = csm; User Id = hr_csm; password=Csm@9876&x"
            
            // remote connection
            // "Data Source=mysql9001.site4now.net; Initial Catalog = db_aa5d99_csm; User Id = aa5d99_csm; password=Csm@9876&x"


        /*
        //"Data Source=(DESCRIPTION =(ADDRESS = (PROTOCOL = TCP)(HOST = 10.228.33.29)(PORT = 1521))(ADDRESS = (PROTOCOL = TCP)(HOST = 10.228.33.30)(PORT = 1521))(LOAD_BALANCE = yes)(CONNECT_DATA =(SERVER = SHARED)(SERVICE_NAME = predetail)));User ID=bonus_corp;Password=bonus_corp@123;Max Pool Size=100;",
        "Data Source=(DESCRIPTION =(ADDRESS = (PROTOCOL = TCP)(HOST = 10.228.37.53)(PORT = 1521))(ADDRESS = (PROTOCOL = TCP)(HOST = 10.228.37.55)(PORT = 1521))(LOAD_BALANCE = yes)(CONNECT_DATA =(SERVER = SHARED)(SERVICE_NAME = dbvas)));User ID=adminone;Password=a#A!diN1(4Jk)@3;Max Pool Size=100;",
        //"Data Source=(DESCRIPTION = (ADDRESS = (PROTOCOL = TCP)(HOST = 10.228.33.80)(PORT = 1521))(CONNECT_DATA =(SERVER = DEDICATED)(SERVICE_NAME = vas)));User ID=adminone;Password=oNe#mI!@)120;Max Pool Size=100;",
        "Data Source=(DESCRIPTION =(ADDRESS = (PROTOCOL = TCP)(HOST = 10.228.33.29)(PORT = 1521))(ADDRESS = (PROTOCOL = TCP)(HOST = 10.228.33.30)(PORT = 1521))(LOAD_BALANCE = yes)(CONNECT_DATA =(SERVER = SHARED)(SERVICE_NAME = predetail)));User ID=HR_OWNER;Password=HR_OWNER123!;Max Pool Size=100;",
        //"Data Source=(DESCRIPTION =(ADDRESS = (PROTOCOL = TCP)(HOST = 10.228.33.29)(PORT = 1521))(ADDRESS = (PROTOCOL = TCP)(HOST = 10.228.33.30)(PORT = 1521))(LOAD_BALANCE = yes)(CONNECT_DATA =(SERVER = SHARED)(SERVICE_NAME = predetail)));User ID=saleone;Password=saleone123a@!;Max Pool Size=100;",
        //"Data Source=(DESCRIPTION =(ADDRESS = (PROTOCOL = TCP)(HOST = 10.228.33.29)(PORT = 1521))(ADDRESS = (PROTOCOL = TCP)(HOST = 10.228.33.30)(PORT = 1521))(LOAD_BALANCE = yes)(CONNECT_DATA =(SERVER = SHARED)(SERVICE_NAME = predetail)));User ID=RMS_CM;Password=nat!@#123;Max Pool Size=100;",
        "Data Source=(DESCRIPTION =(ADDRESS = (PROTOCOL = TCP)(HOST = 10.228.33.29)(PORT = 1521))(ADDRESS = (PROTOCOL = TCP)(HOST = 10.228.33.30)(PORT = 1521))(LOAD_BALANCE = yes)(CONNECT_DATA =(SERVER = SHARED)(SERVICE_NAME = predetail)));User ID=RMS_CM;Password=nat!@#123;Max Pool Size=100;",
        //"Data Source=(DESCRIPTION =(ADDRESS = (PROTOCOL = TCP)(HOST = 10.228.37.59)(PORT = 1521))(ADDRESS = (PROTOCOL = TCP)(HOST = 10.228.37.61)(PORT = 1521))(LOAD_BALANCE = yes)(CONNECT_DATA =(SERVER = SHARED)(SERVICE_NAME = dbvaspri)));User ID=street_marketing;Password=s_M#123(;Max Pool Size=100;",            
        //"Data Source=(DESCRIPTION =(ADDRESS = (PROTOCOL = TCP)(HOST = 10.228.33.80)(PORT = 1521))(ADDRESS = (PROTOCOL = TCP)(HOST = 10.228.33.80)(PORT = 1521))(LOAD_BALANCE = yes)(CONNECT_DATA =(SERVER = SHARED)(SERVICE_NAME = vas)));User ID=IM_HAITI;Password=IM_HAITI;Max Pool Size=100;",
        //"Data Source=(DESCRIPTION =(ADDRESS = (PROTOCOL = TCP)(HOST = 10.228.33.15)(PORT = 1521))(ADDRESS = (PROTOCOL = TCP)(HOST = 10.228.33.15)(PORT = 1521))(LOAD_BALANCE = yes)(CONNECT_DATA =(SERVER = SHARED)(SERVICE_NAME = PRE1)));User ID=VTG_REPORT;Password=VTG_REPORT;Max Pool Size=100;",
        //"Data Source=(DESCRIPTION =(ADDRESS_LIST = (ADDRESS = (PROTOCOL = TCP)(HOST = 10.228.37.53)(PORT = 1521))) (CONNECT_DATA = (SERVICE_NAME = dbvas)(SERVER = SHARED)));User ID=vas_lapoula;Password=Vas_Lapoula!@#456;Max Pool Size=100;",
        "Data Source=(DESCRIPTION =(ADDRESS = (PROTOCOL = TCP)(HOST = 10.228.33.29)(PORT = 1521))(ADDRESS = (PROTOCOL = TCP)(HOST = 10.228.33.30)(PORT = 1521))(LOAD_BALANCE = yes)(CONNECT_DATA =(SERVER = SHARED)(SERVICE_NAME = predetail)));User ID=RMS_CM;Password=nat!@#123;Max Pool Size=2000;"
        */
    };

        public static string GetConnectionString(SqlConnString str)
        {
            return ConnectionStrings[(int)str];
        }
    }
}


