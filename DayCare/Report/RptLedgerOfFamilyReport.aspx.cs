using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions;
using CrystalDecisions.Web;
using CrystalDecisions.Shared;
using System.Configuration;
using System.Data;


namespace DayCare.Report
{
    public partial class RptLedgerOfFamilyReport : System.Web.UI.Page
    {
        string strConnection;
        CrystalDecisions.Web.Report rpt = new CrystalDecisions.Web.Report();
        CrystalDecisions.CrystalReports.Engine.ReportDocument rpt1;
        string StartDate = "";
        string EndDate = "";
        double OpeningBal = 0;
        bool OpeningBalance = false;

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Page_Init(object sender, EventArgs e)
        {
            if (Session["CurrentSchoolYearId"] == null)
            {
                Response.Redirect("~/Login.aspx");
            }

            crdata.Report = rpt;
            rpt1 = crdata.ReportDocument;
            DataSet dsReport = new xmlClassWiseStudentAttendTime();
            DataSet ds = new DataSet();
            DataSet dsForReport = new DataSet();
            DataTable dt = new DataTable();
            //strConnection = ConfigurationSettings.AppSettings["8CA767A0-5E36-4343-8B1D-5ECC40EB9E1B"];
            strConnection = ConfigurationSettings.AppSettings[Session["SchoolId"].ToString()];
            if (!String.IsNullOrEmpty(Request.QueryString["StartDate"]))
            {
                StartDate = Request.QueryString["StartDate"].ToString();
            }
            if (!String.IsNullOrEmpty(Request.QueryString["EndDate"]))
            {
                EndDate = Request.QueryString["EndDate"].ToString();
            }
            if (!String.IsNullOrEmpty(Request.QueryString["ChildFamilyId"]))
            {
                ViewState["ChildFamilyId"] = new Guid(Request.QueryString["ChildFamilyId"].ToString());
            }

            if (!String.IsNullOrEmpty(ViewState["ChildFamilyId"].ToString()))
            {
                if (string.IsNullOrEmpty(StartDate) && string.IsNullOrEmpty(EndDate))
                {
                    rpt.FileName = Server.MapPath("rptLedgerOfFamily.rpt");
                    DayCarePL.LedgerProperties objLedger = new DayCarePL.LedgerProperties();
                    DayCareBAL.LedgerService proxyLedger = new DayCareBAL.LedgerService();
                    CrystalDecisions.CrystalReports.Engine.TextObject titleText = (CrystalDecisions.CrystalReports.Engine.TextObject)rpt1.ReportDefinition.ReportObjects["Text9"];
                    CrystalDecisions.CrystalReports.Engine.TextObject titleTextSchool = (CrystalDecisions.CrystalReports.Engine.TextObject)rpt1.ReportDefinition.ReportObjects["Text5"];
                    CrystalDecisions.CrystalReports.Engine.TextObject LedgerEIN = (CrystalDecisions.CrystalReports.Engine.TextObject)rpt1.ReportDefinition.ReportObjects["Text15"];
                    CrystalDecisions.CrystalReports.Engine.TextObject footer = (CrystalDecisions.CrystalReports.Engine.TextObject)rpt1.ReportDefinition.ReportObjects["txtfooter"];
                    footer.Text = Common.GetSchoolWiseAddress(new Guid(Session["SchoolId"].ToString()));
                    if (!string.IsNullOrEmpty(strConnection))
                    {
                        LedgerEIN.Text = "EIN#: " + strConnection;
                    }
                    //titleText.Text = "Ledger Of Family report as on " + Convert.ToDateTime(StartDate).ToShortDateString() + " to " + Convert.ToDateTime(EndDate).ToShortDateString();
                    titleTextSchool.Text = Session["SchoolName"].ToString().ToUpper();
                    ds = proxyLedger.LoadLedgerOfFamily(new Guid(Session["CurrentSchoolYearId"].ToString()), new Guid(ViewState["ChildFamilyId"].ToString()));
                    List<DayCarePL.ClosingBalance> lstOpeningBalance = proxyLedger.GetPreviousYearClosingBalance(new Guid(ViewState["ChildFamilyId"].ToString()), new Guid(Session["CurrentSchoolYearId"].ToString()));
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        try
                        {
                            titleText.Text = "Ledger of Family Report for period:" + Convert.ToDateTime(ds.Tables[0].Rows[0]["TransactionDate"].ToString()).ToShortDateString() + " to " + Convert.ToDateTime(ds.Tables[0].Rows[ds.Tables[0].Rows.Count - 1]["TransactionDate"].ToString()).ToShortDateString();
                        }
                        catch { }
                    }
                    if (ds != null && ds.Tables.Count > 0 && lstOpeningBalance != null)// && lstOpeningBalance.Count > 0)
                    {
                        DataRow dr = ds.Tables[0].NewRow();
                        //dr["Id"] = DBNull.Value;
                        //dr["SchoolYearId"] = Session["CurrentSchoolYearId"].ToString();
                        //dr["ChildFamilyId"] = ViewState["ChildFamilyId"].ToString();
                        //dr["PaymentId"] = "";
                        //dr["TransactionDate"] = "";
                        //dr["Detail"] = "";
                        //dr["ChildDataId"] = "";
                        dr["Comment"] = "OPENING BALANCE :";
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            dr["FamilyTitle"] = ds.Tables[0].Rows[0]["FamilyTitle"].ToString();
                        }
                        else
                        {
                            dr["FamilyTitle"] = Common.GetFamilyName(new Guid(ViewState["ChildFamilyId"].ToString()));
                        }
                        //dr["Debit"] = "";
                        //dr["Credit"] = "";
                        dr["Balance"] = lstOpeningBalance.Sum(i => i.ClosingBalanceAmount);
                        ds.Tables[0].Rows.InsertAt(dr, 0);


                    }
                    decimal debit = 0;
                    decimal credit = 0;
                    decimal balance = 0;
                    int i1 = 0;
                    if (lstOpeningBalance != null)// && lstOpeningBalance.Count > 0)
                    {
                        i1 = 1;
                        balance = lstOpeningBalance.Sum(i => i.ClosingBalanceAmount);
                    }
                    for (int i = i1; i < ds.Tables[0].Rows.Count; i++)
                    {

                        debit = Convert.ToDecimal(ds.Tables[0].Rows[i]["Debit"].ToString());
                        if (debit > 0)
                        {
                            balance += debit;
                        }
                        else
                        {
                            credit = Convert.ToDecimal(ds.Tables[0].Rows[i]["Credit"].ToString());
                            balance -= credit;
                        }
                        ds.Tables[0].Rows[i]["Balance"] = balance;
                    }
                    dsReport.Tables["dtLedgerOfFamily"].Merge(ds.Tables[0]);
                    rpt1.SetDataSource(dsReport.Tables["dtLedgerOfFamily"]);

                }
                else
                {
                    rpt.FileName = Server.MapPath("rptLedgerOfFamily.rpt");
                    DayCarePL.LedgerProperties objLedger = new DayCarePL.LedgerProperties();
                    DayCareBAL.LedgerService proxyLedger = new DayCareBAL.LedgerService();
                    CrystalDecisions.CrystalReports.Engine.TextObject titleText = (CrystalDecisions.CrystalReports.Engine.TextObject)rpt1.ReportDefinition.ReportObjects["Text9"];
                    CrystalDecisions.CrystalReports.Engine.TextObject titleTextSchool = (CrystalDecisions.CrystalReports.Engine.TextObject)rpt1.ReportDefinition.ReportObjects["Text5"];
                    CrystalDecisions.CrystalReports.Engine.TextObject LedgerEIN = (CrystalDecisions.CrystalReports.Engine.TextObject)rpt1.ReportDefinition.ReportObjects["Text15"];
                    if (!string.IsNullOrEmpty(strConnection))
                    {
                        LedgerEIN.Text = "EIN#: " + strConnection;
                    }
                    titleText.Text = "Ledger of Family Report for period: " + Convert.ToDateTime(StartDate).ToShortDateString() + " to " + Convert.ToDateTime(EndDate).ToShortDateString();
                    titleTextSchool.Text = Session["SchoolName"].ToString().ToUpper();
                    ds = proxyLedger.LoadLedgerOfFamily(new Guid(Session["CurrentSchoolYearId"].ToString()), new Guid(ViewState["ChildFamilyId"].ToString()));
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        DataView dv = ds.Tables[0].DefaultView;
                        dv.Sort = "TransactionDate ASC";

                    }
                    List<DayCarePL.ClosingBalance> lstOpeningBalance = proxyLedger.GetPreviousYearClosingBalance(new Guid(ViewState["ChildFamilyId"].ToString()), new Guid(Session["CurrentSchoolYearId"].ToString()));
                    if (ds != null && lstOpeningBalance != null)
                    {
                        //DataRow dr = ds.Tables[0].NewRow();
                        DataColumn Id = new DataColumn("Id", typeof(System.Guid));
                        dt.Columns.Add(Id);

                        DataColumn SchoolYearId = new DataColumn("SchoolYearId", typeof(System.Guid));
                        dt.Columns.Add(SchoolYearId);

                        DataColumn ChildFamilyId = new DataColumn("ChildFamilyId", typeof(System.Guid));
                        dt.Columns.Add(ChildFamilyId);

                        DataColumn PaymentId = new DataColumn("PaymentId", typeof(System.Guid));
                        dt.Columns.Add(PaymentId);

                        DataColumn TransactionDate = new DataColumn("TransactionDate", typeof(System.DateTime));
                        dt.Columns.Add(TransactionDate);


                        DataColumn Detail = new DataColumn("Detail", typeof(System.String));
                        dt.Columns.Add(Detail);

                        DataColumn ChildDataId = new DataColumn("ChildDataId", typeof(System.Guid));
                        dt.Columns.Add(ChildDataId);


                        DataColumn Comment = new DataColumn("Comment", typeof(System.String));
                        dt.Columns.Add(Comment);

                        DataColumn FamilyTitle = new DataColumn("FamilyTitle", typeof(System.String));
                        dt.Columns.Add(FamilyTitle);

                        DataColumn Debit = new DataColumn("Debit", typeof(System.Decimal));
                        dt.Columns.Add(Debit);

                        DataColumn Credit = new DataColumn("Credit", typeof(System.Decimal));
                        dt.Columns.Add(Credit);

                        DataColumn Balance = new DataColumn("Balance", typeof(System.Decimal));
                        dt.Columns.Add(Balance);

                        DataColumn PaymentMethodOrCharges = new DataColumn("PaymentMethodOrCharges", typeof(System.String));
                        dt.Columns.Add(PaymentMethodOrCharges);

                        DataRow dr = dt.NewRow();
                        //dr["Id"] = DBNull.Value;
                        //dr["SchoolYearId"] = Session["CurrentSchoolYearId"].ToString();
                        //dr["ChildFamilyId"] = ViewState["ChildFamilyId"].ToString();
                        //dr["PaymentId"] = "";
                        //dr["TransactionDate"] = "";
                        //dr["Detail"] = "";
                        //dr["ChildDataId"] = "";
                        dr["Comment"] = "OPENING BALANCE :";
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            dr["FamilyTitle"] = ds.Tables[0].Rows[0]["FamilyTitle"].ToString();
                        }
                        else
                        {
                            dr["FamilyTitle"] = Common.GetFamilyName(new Guid(ViewState["ChildFamilyId"].ToString()));
                        }
                        //dr["Debit"] = "";
                        //dr["Credit"] = "";
                        dr["Balance"] = lstOpeningBalance.Sum(i => i.ClosingBalanceAmount);
                        //ds.Tables[0].Rows.InsertAt(dr, 0);
                        dt.Rows.Add(dr);
                        dsForReport.Tables.Add(dt);
                        //dsForReport.Tables[0].Rows.InsertAt(dr, 0);

                    }
                    //else
                    //{
                    //    //DataRow dr = ds.Tables[0].NewRow();
                    //    DataColumn Id = new DataColumn("Id", typeof(System.Guid));
                    //    dt.Columns.Add(Id);

                    //    DataColumn SchoolYearId = new DataColumn("SchoolYearId", typeof(System.Guid));
                    //    dt.Columns.Add(SchoolYearId);

                    //    DataColumn ChildFamilyId = new DataColumn("ChildFamilyId", typeof(System.Guid));
                    //    dt.Columns.Add(ChildFamilyId);

                    //    DataColumn PaymentId = new DataColumn("PaymentId", typeof(System.Guid));
                    //    dt.Columns.Add(PaymentId);

                    //    DataColumn TransactionDate = new DataColumn("TransactionDate", typeof(System.DateTime));
                    //    dt.Columns.Add(TransactionDate);


                    //    DataColumn Detail = new DataColumn("Detail", typeof(System.String));
                    //    dt.Columns.Add(Detail);

                    //    DataColumn ChildDataId = new DataColumn("ChildDataId", typeof(System.Guid));
                    //    dt.Columns.Add(ChildDataId);


                    //    DataColumn Comment = new DataColumn("Comment", typeof(System.String));
                    //    dt.Columns.Add(Comment);

                    //    DataColumn FamilyTitle = new DataColumn("FamilyTitle", typeof(System.String));
                    //    dt.Columns.Add(FamilyTitle);

                    //    DataColumn Debit = new DataColumn("Debit", typeof(System.Decimal));
                    //    dt.Columns.Add(Debit);

                    //    DataColumn Credit = new DataColumn("Credit", typeof(System.Decimal));
                    //    dt.Columns.Add(Credit);

                    //    DataColumn Balance = new DataColumn("Balance", typeof(System.Decimal));
                    //    dt.Columns.Add(Balance);

                    //    DataColumn PaymentMethodOrCharges = new DataColumn("PaymentMethodOrCharges", typeof(System.String));
                    //    dt.Columns.Add(PaymentMethodOrCharges);

                    //    //DataRow dr = dt.NewRow();

                    //    //dr["Comment"] = "OPENING BALANCE :";
                    //    //if (ds.Tables[0].Rows.Count > 0)
                    //    //{
                    //    //    dr["FamilyTitle"] = ds.Tables[0].Rows[0]["FamilyTitle"].ToString();
                    //    //}
                    //    //else
                    //    //{
                    //    //    dr["FamilyTitle"] = Common.GetFamilyName(new Guid(ViewState["ChildFamilyId"].ToString()));
                    //    //}
                    //    //dr["Balance"] = lstOpeningBalance.Sum(i => i.ClosingBalanceAmount);
                    //    //dt.Rows.Add(dr);
                    //    dsForReport.Tables.Add(dt);
                    //}
                    decimal debit = 0;
                    decimal credit = 0;
                    decimal balance = 0;
                    int i1 = 0;
                    if (lstOpeningBalance != null)
                    {
                        //i1 = 1;
                        balance = lstOpeningBalance.Sum(i => i.ClosingBalanceAmount);
                    }
                    for (int i = i1; i < ds.Tables[0].Rows.Count; i++)
                    {
                        if (Convert.ToDateTime(ds.Tables[0].Rows[i]["TransactionDate"].ToString()).Date <= Convert.ToDateTime(EndDate).Date)
                        {
                            debit = Convert.ToDecimal(ds.Tables[0].Rows[i]["Debit"].ToString());
                            if (debit > 0)
                            {
                                balance += debit;
                            }
                            else
                            {
                                credit = Convert.ToDecimal(ds.Tables[0].Rows[i]["Credit"].ToString());
                                balance -= credit;
                            }
                        }
                        if (Convert.ToDateTime(ds.Tables[0].Rows[i]["TransactionDate"].ToString()).Date >= Convert.ToDateTime(StartDate).Date && Convert.ToDateTime(ds.Tables[0].Rows[i]["TransactionDate"].ToString()).Date <= Convert.ToDateTime(EndDate).Date)
                        {
                            DataRow dr = dt.NewRow();
                            if (OpeningBalance == false)
                            {
                                if (debit > 0)
                                {
                                    balance -= debit;
                                }
                                else
                                {
                                    credit = Convert.ToDecimal(ds.Tables[0].Rows[i]["Credit"].ToString());
                                    balance += credit;
                                }
                                if (dsForReport.Tables.Count > 0)
                                {
                                    dsForReport.Tables[0].Rows[0]["Balance"] = balance;
                                }
                                OpeningBalance = true;
                                if (debit > 0)
                                {
                                    balance += debit;
                                }
                                else
                                {
                                    credit = Convert.ToDecimal(ds.Tables[0].Rows[i]["Credit"].ToString());
                                    balance -= credit;
                                }
                            }
                            dr["Id"] = ds.Tables[0].Rows[i]["Id"].ToString();
                            dr["SchoolYearId"] = new Guid(Session["CurrentSchoolYearId"].ToString());
                            dr["ChildFamilyId"] = new Guid(ViewState["ChildFamilyId"].ToString());
                            // dr["PaymentId"] =new Guid( ds.Tables[0].Rows[i]["PaymentId"].ToString());
                            dr["TransactionDate"] = Convert.ToDateTime(ds.Tables[0].Rows[i]["TransactionDate"].ToString());
                            dr["Detail"] = ds.Tables[0].Rows[i]["Detail"].ToString();
                            //dr["ChildDataId"] =new Guid( ds.Tables[0].Rows[i]["ChildDataId"].ToString());
                            dr["Comment"] = ds.Tables[0].Rows[i]["Comment"].ToString();
                            dr["FamilyTitle"] = ds.Tables[0].Rows[0]["FamilyTitle"].ToString();
                            dr["Debit"] = Convert.ToDecimal(ds.Tables[0].Rows[i]["Debit"].ToString());
                            dr["Credit"] = Convert.ToDecimal(ds.Tables[0].Rows[i]["Credit"].ToString());
                            dr["Balance"] = balance;
                            if (!string.IsNullOrEmpty(ds.Tables[0].Rows[i]["PaymentMethodOrCharges"].ToString()))
                            {
                                dr["PaymentMethodOrCharges"] = ds.Tables[0].Rows[i]["PaymentMethodOrCharges"].ToString();
                            }
                            if (dsForReport.Tables.Count > 0)
                            {
                                dsForReport.Tables[0].Rows.Add(dr);
                            }

                        }
                        //if (Convert.ToDateTime(ds.Tables[0].Rows[i]["TransactionDate"].ToString()).Date >= Convert.ToDateTime(StartDate).Date && Convert.ToDateTime(ds.Tables[0].Rows[i]["TransactionDate"].ToString()).Date <= Convert.ToDateTime(EndDate).Date)
                        //{
                        //    break;
                        //}
                        //ds.Tables[0].Rows[i]["Balance"] = balance;
                    }
                    if (dsForReport.Tables.Count == 0)
                    {
                        dsForReport.Tables.Add(dt);
                    }
                    if (OpeningBalance == false)
                    {
                        dsForReport.Tables[0].Rows[0]["Balance"] = balance;
                    }
                    dsReport.Tables["dtLedgerOfFamily"].Merge(dsForReport.Tables[0]);
                    rpt1.SetDataSource(dsReport.Tables["dtLedgerOfFamily"]);

                }
            }
            crp.DisplayGroupTree = false;
            crp.ReportSource = rpt1;
            crp.RefreshReport();

            crp.DataBind();
        }
    }
}
