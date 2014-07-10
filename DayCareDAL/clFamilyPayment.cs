using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Collections;

namespace DayCareDAL
{
    public class clFamilyPayment
    {
        #region "Load Family wise Payment, Dt: 9-Sept-2011, DB: A"
        public static List<DayCarePL.FamilyPaymentProperties> GetFamilyWisePayment(Guid ChildFamilyId)
        {
            DayCarePL.Logger.Write(DayCarePL.LogType.INFO, DayCarePL.ModuleToLog.clFamilyPayment, "GetFamilyWisePayment", "GetFamilyWisePayment method called", DayCarePL.Common.GUID_DEFAULT);
            clConnection.DoConnection();
            DayCareDataContext db = new DayCareDataContext();
            try
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.DEBUG, DayCarePL.ModuleToLog.clFamilyPayment, "GetFamilyWisePayment", "Debug GetFamilyWisePayment called", DayCarePL.Common.GUID_DEFAULT);
                List<DayCarePL.FamilyPaymentProperties> result = (from p in db.Payments
                                                                  where p.ChildFamilyId.Equals(ChildFamilyId)
                                                                  select new DayCarePL.FamilyPaymentProperties()
                                                                  {
                                                                      Id = p.Id,
                                                                      SchoolYearId = p.SchoolYearId,
                                                                      ChildFamilyId = p.ChildFamilyId,
                                                                      PostDate = p.PostDate,
                                                                      PaymentMethod = p.PaymentMethod,
                                                                      PaymentDetail = p.PaymentDetail,
                                                                      Amount = p.Amount.Value,
                                                                      CreatedById = p.CreatedById,
                                                                      CreatedDateTime = p.CreatedDateTime,
                                                                      LastModifiedById = p.LastModifiedById,
                                                                      LastModifiedDatetime = p.LastModifiedDatetime
                                                                  }).ToList();
                return result;
            }
            catch (Exception ex)
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.clFamilyPayment, "GetFamilyWisePayment", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
                return null;
            }
        }
        #endregion

        #region "Save, Dt: 9-Sept-2011, DB: A"
        public static bool Save(List<DayCarePL.FamilyPaymentProperties> lstFamilyPayment)
        {
            DayCarePL.Logger.Write(DayCarePL.LogType.INFO, DayCarePL.ModuleToLog.clFamilyPayment, "Save", "Save method called", DayCarePL.Common.GUID_DEFAULT);
            clConnection.DoConnection();
            DayCareDataContext db = new DayCareDataContext();
            System.Data.Common.DbTransaction Tran = null;
            Payment DBPayment = null;

            try
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.DEBUG, DayCarePL.ModuleToLog.clFamilyPayment, "Save", "Debug Save called", DayCarePL.Common.GUID_DEFAULT);
                if (db.Connection.State == System.Data.ConnectionState.Closed)
                {
                    db.Connection.Open();
                }
                Tran = db.Connection.BeginTransaction();
                db.Transaction = Tran;
                int cntSuccess = 0;
                foreach (DayCarePL.FamilyPaymentProperties objFamilyPayment in lstFamilyPayment)
                {
                    try
                    {
                        if (objFamilyPayment.Id.ToString().Equals(DayCarePL.Common.GUID_DEFAULT))
                        {
                            DBPayment = new Payment();
                            DBPayment.Id = Guid.NewGuid();
                            DBPayment.CreatedDateTime = DateTime.Now;
                            DBPayment.CreatedById = objFamilyPayment.CreatedById;
                        }
                        else
                        {
                            DBPayment = db.Payments.FirstOrDefault(i => i.Id.Equals(objFamilyPayment.Id));
                        }
                        DBPayment.SchoolYearId = objFamilyPayment.SchoolYearId;
                        DBPayment.ChildFamilyId = objFamilyPayment.ChildFamilyId;
                        DBPayment.PostDate = objFamilyPayment.PostDate;
                        DBPayment.PaymentMethod = objFamilyPayment.PaymentMethod;
                        DBPayment.PaymentDetail = objFamilyPayment.PaymentDetail;
                        DBPayment.Amount = objFamilyPayment.Amount;
                        DBPayment.LastModifiedDatetime = DateTime.Now;
                        DBPayment.LastModifiedById = objFamilyPayment.LastModifiedById;
                        if (objFamilyPayment.Id.ToString().Equals(DayCarePL.Common.GUID_DEFAULT))
                        {
                            db.Payments.InsertOnSubmit(DBPayment);
                        }
                        db.SubmitChanges();
                        Ledger DBLedger = new Ledger();
                        if (objFamilyPayment.Id.ToString().Equals(DayCarePL.Common.GUID_DEFAULT))
                        {
                            DBLedger.Id = Guid.NewGuid();
                            DBLedger.PaymentId = DBPayment.Id;
                            DBLedger.CreatedById = (objFamilyPayment.CreatedById).Value;
                            DBLedger.CreatedDateTime = DateTime.Now;
                        }
                        else
                        {
                            DBLedger = db.Ledgers.FirstOrDefault(i => i.PaymentId.Equals(objFamilyPayment.Id));
                        }
                        DBLedger.SchoolYearId = objFamilyPayment.SchoolYearId;
                        DBLedger.ChildFamilyId = objFamilyPayment.ChildFamilyId;
                        DBLedger.TransactionDate = Convert.ToDateTime(objFamilyPayment.PostDate);
                        string paymentmethod = "";
                        if (!string.IsNullOrEmpty(objFamilyPayment.PaymentMethod))
                        {
                            switch (objFamilyPayment.PaymentMethod)
                            {
                                case "0":
                                    paymentmethod = "Cash";
                                    break;
                                case "1":
                                    paymentmethod = "Check";
                                    break;
                                case "2":
                                    paymentmethod = "Credit";
                                    break;
                            }
                        }
                        DBLedger.PaymentMethodId = Convert.ToInt16(objFamilyPayment.PaymentMethod);
                        DBLedger.Comment = paymentmethod + (string.IsNullOrEmpty(objFamilyPayment.PaymentDetail) ? "" : "-" + objFamilyPayment.PaymentDetail);
                        DBLedger.Debit = 0;
                        DBLedger.Credit = objFamilyPayment.Amount;
                        DBLedger.Balance = DBLedger.Debit - DBLedger.Credit;
                        DBLedger.AllowEdit = true;
                        DBLedger.LastModifiedDatetime = DateTime.Now;
                        DBLedger.LastModifiedById = objFamilyPayment.LastModifiedById;
                        if (objFamilyPayment.Id.ToString().Equals(DayCarePL.Common.GUID_DEFAULT))
                        {
                            db.Ledgers.InsertOnSubmit(DBLedger);
                        }
                        db.SubmitChanges();

                        //update closing balance on 10 May 2013 By Akash
                        try
                        {
                            Guid SchoolId = clSchool.GetSchoolIdbySchoolYearId(objFamilyPayment.SchoolYearId.Value);
                            if (clSchoolYear.IsSelectedYearPrevYearORNot(SchoolId, objFamilyPayment.SchoolYearId.Value))//only prev year can only allow to edit closing balance. because in current year or in future year closing balance is not genrated in these year
                            {
                                clSchoolYear.UpdateClosingBalance(SchoolId, objFamilyPayment.SchoolYearId.Value, objFamilyPayment.ChildFamilyId.Value, Tran, db);
                            }
                        }
                        catch { }

                        //End
                        cntSuccess++;
                    }
                    catch
                    { }
                }
                Tran.Commit();
                if (cntSuccess > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.clFamilyPayment, "Save", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
                if (Tran != null)
                {
                    Tran.Rollback();
                }
                return false;
            }
        }
        #endregion

        #region "Delete Family Payment, Dt: 12-Sept-2011, DB: A"
        public static bool Delete(Guid Id)
        {
            bool result = false;
            DayCarePL.Logger.Write(DayCarePL.LogType.INFO, DayCarePL.ModuleToLog.clFamilyPayment, "Delete", "Delete Method", DayCarePL.Common.GUID_DEFAULT);
            //SqlConnection conn = clConnection.CreateConnection();
            clConnection.DoConnection();
            DayCareDataContext db = new DayCareDataContext();
            System.Data.Common.DbTransaction Tran = null;
            try
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.DEBUG, DayCarePL.ModuleToLog.clFamilyPayment, "Delete", "Debug Delete Method", DayCarePL.Common.GUID_DEFAULT);
                //clConnection.OpenConnection(conn);
                //SqlCommand cmd;
                //cmd = clConnection.CreateCommand("spDeleteFamilyPayment", conn);
                //cmd.Parameters.Add(clConnection.GetInputParameter("@Id", Id));
                //object Result = cmd.ExecuteScalar();
                //result = Convert.ToBoolean(Result);
                if (db.Connection.State == System.Data.ConnectionState.Closed)
                {
                    db.Connection.Open();
                }
                Tran = db.Connection.BeginTransaction();
                db.Transaction = Tran;

                Ledger DBLedger = db.Ledgers.FirstOrDefault(i => i.PaymentId.Equals(Id));
                if (DBLedger != null)
                {
                    db.Ledgers.DeleteOnSubmit(DBLedger);
                    db.SubmitChanges();

                }
                else
                {
                    Tran.Rollback();
                    result = false;
                }
                Payment DBPayment = db.Payments.FirstOrDefault(i => i.Id.Equals(Id));
                if (DBPayment != null)
                {
                    db.Payments.DeleteOnSubmit(DBPayment);
                    db.SubmitChanges();
                    Tran.Commit();
                    result = true;
                }
                else
                {
                    result = false;
                }

            }
            catch (Exception ex)
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.clFamilyPayment, "Delete", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
                result = false;
                if (Tran != null)
                {
                    Tran.Rollback();
                }
            }
            return result;
        }
        #endregion

        #region "Load PaymentDiposits Report,Dt:07-Oct-2011,Db:V"
        public static DataSet LoadPaymentDeposits(string SearchText, Guid SchoolYearId)
        {
            clConnection.DoConnection();
            DayCareDataContext db = new DayCareDataContext();
            DataSet ds = new DataSet();
            try
            {

                SortedList sl = new SortedList();
                sl.Add("@SchoolYearId", SchoolYearId);
                sl.Add("@SearchText", SearchText);
                ds = clConnection.GetDataSet("spRptDepositReport", sl);
                if (ds != null && ds.Tables.Count > 0)
                {
                    for (int cntRow = 0; cntRow < ds.Tables[0].Rows.Count; cntRow++)
                    {
                        ds.Tables[0].Rows[cntRow]["Children"] = ds.Tables[0].Rows[cntRow]["Children"].ToString().Replace(".", "\n");
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return ds;
        }
        #endregion
    }
}
