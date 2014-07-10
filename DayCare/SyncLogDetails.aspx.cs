using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DayCare.UI
{
    public partial class SyncLogDetails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void rgSyncLogList_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            try
            {
                DayCareBAL.SyncLogService proxySyncLog = new DayCareBAL.SyncLogService();

                List<DayCarePL.SyncLogProperties> lstSyncLog = proxySyncLog.LoadSyncLoad();
                if (lstSyncLog != null)
                {
                    rgSyncLogList.DataSource = lstSyncLog;
                }
                else
                {
                    rgSyncLogList.DataSource = new List<DayCarePL.SyncLogProperties>();
                }
            }
            catch { }
        }
    }
}
