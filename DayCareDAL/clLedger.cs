using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Collections;

namespace DayCareDAL
{
    public class clLedger
    {
        #region "Get Child Prog. Enrollment Fee Detail ,Dt:19-09-2011 ,Db: A"
        // Get Enrolled student list which due fees ammount. Enrollment Status must be Enrolled and get only active child
        public static List<DayCarePL.ChildProgEnrollmentProperties> GetChildProgEnrollmentFeeDetail(Guid SchoolYearId, Guid ChildFamilyId)
        {

            DayCarePL.Logger.Write(DayCarePL.LogType.INFO, DayCarePL.ModuleToLog.clLedger, "GetChildProgEnrollmentFeeDetail", "GetChildProgEnrollmentFeeDetail method called", DayCarePL.Common.GUID_DEFAULT);
            DayCareDataContext db = new DayCareDataContext();
            try
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.DEBUG, DayCarePL.ModuleToLog.clLedger, "GetChildProgEnrollmentFeeDetail", "Debug GetChildProgEnrollmentFeeDetail called", DayCarePL.Common.GUID_DEFAULT);
                if (ChildFamilyId.ToString().Equals(DayCarePL.Common.GUID_DEFAULT))
                {
                    var data = (from cpefd in db.ChildProgEnrollmentFeeDetails
                                join fp in db.FeesPeriods on cpefd.FeesPeriodId equals fp.Id
                                join csy in db.ChildSchoolYears on cpefd.ChildSchoolYearId equals csy.Id
                                join cd in db.ChildDatas on csy.ChildDataId equals cd.Id
                                join sp in db.SchoolPrograms on cpefd.SchoolProgramId equals sp.Id
                                where csy.SchoolYearId.Equals(SchoolYearId) && csy.active.Equals(true) //&& cpefd.EnrollmentStatus.Equals(DayCarePL.Common.DEFAULT_ENROLLMENT_STATUS, StringComparison.InvariantCultureIgnoreCase)
                                select new DayCarePL.ChildProgEnrollmentProperties()
                                {
                                    Id = cpefd.Id,
                                    ChildFamilyId = cpefd.ChildFamilyId.Value,
                                    ChildSchoolYearId = cpefd.ChildSchoolYearId.Value,
                                    SchoolProgramId = cpefd.SchoolProgramId.Value,
                                    ProgramTitle = sp.Title,
                                    ChildDataId = csy.ChildDataId,
                                    FeesPeriodId = cpefd.FeesPeriodId,
                                    Fees = cpefd.Fee,
                                    FeesPeriodName = fp.Name,
                                    EffectiveMonthDay = cpefd.EffectiveMonthDay,
                                    EffectiveWeekDay = cpefd.EffectiveWeekDay,
                                    EffectiveYearDate = cpefd.EffectiveYearDate,
                                    EnrollmentDate = cpefd.EnrollmentDate,
                                    EnrollmentStatus = cpefd.EnrollmentStatus,
                                    CreateDateTime = cpefd.CreatedDatetime.Value,
                                    CreatedById = cpefd.CreatedById.Value,
                                    LastModifiedById = cpefd.LastmodiedById.Value,
                                    LastModifiedDateTime = cpefd.LastmodifiedDatetime.Value,
                                    StartDate = cpefd.StartDate,
                                    EndDate = cpefd.EndDate
                                }).ToList();
                    return data.FindAll(i => i.EnrollmentStatus != null && i.EnrollmentStatus.Equals(DayCarePL.Common.DEFAULT_ENROLLMENT_STATUS, StringComparison.InvariantCultureIgnoreCase));
                }
                else
                {
                    var data = (from cpefd in db.ChildProgEnrollmentFeeDetails
                                join fp in db.FeesPeriods on cpefd.FeesPeriodId equals fp.Id
                                join csy in db.ChildSchoolYears on cpefd.ChildSchoolYearId equals csy.Id
                                join cd in db.ChildDatas on csy.ChildDataId equals cd.Id
                                join sp in db.SchoolPrograms on cpefd.SchoolProgramId equals sp.Id
                                where csy.SchoolYearId.Equals(SchoolYearId) && csy.active.Equals(true) //&& cpefd.EnrollmentStatus.Equals(DayCarePL.Common.DEFAULT_ENROLLMENT_STATUS, StringComparison.InvariantCultureIgnoreCase)
                                && cpefd.ChildFamilyId.Equals(ChildFamilyId)
                                select new DayCarePL.ChildProgEnrollmentProperties()
                                {
                                    Id = cpefd.Id,
                                    ChildFamilyId = cpefd.ChildFamilyId.Value,
                                    ChildSchoolYearId = cpefd.ChildSchoolYearId.Value,
                                    SchoolProgramId = cpefd.SchoolProgramId.Value,
                                    ProgramTitle = sp.Title,
                                    ChildDataId = csy.ChildDataId,
                                    FeesPeriodId = cpefd.FeesPeriodId,
                                    Fees = cpefd.Fee,
                                    FeesPeriodName = fp.Name,
                                    EffectiveMonthDay = cpefd.EffectiveMonthDay,
                                    EffectiveWeekDay = cpefd.EffectiveWeekDay,
                                    EffectiveYearDate = cpefd.EffectiveYearDate,
                                    EnrollmentDate = cpefd.EnrollmentDate,
                                    EnrollmentStatus = cpefd.EnrollmentStatus,
                                    CreateDateTime = cpefd.CreatedDatetime.Value,
                                    CreatedById = cpefd.CreatedById.Value,
                                    LastModifiedById = cpefd.LastmodiedById.Value,
                                    LastModifiedDateTime = cpefd.LastmodifiedDatetime.Value,
                                    StartDate = cpefd.StartDate,
                                    EndDate = cpefd.EndDate
                                }).ToList();
                    return data.FindAll(i => i.EnrollmentStatus != null && i.EnrollmentStatus.Equals(DayCarePL.Common.DEFAULT_ENROLLMENT_STATUS, StringComparison.InvariantCultureIgnoreCase));
                }
            }
            catch (Exception ex)
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.clLedger, "GetChildProgEnrollmentFeeDetail", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
                return null;
            }
        }
        #endregion

        #region "Check Child Prog In Ledger ,Dt:19-09-2011 ,Db: A"
        public static bool CheckChildProgInLedger(Guid? SchoolYearId, Guid? ChildFamilyId, Guid? ChildDataId, DateTime TransactionDate, Guid? SchoolProgramId)
        {
            DayCarePL.Logger.Write(DayCarePL.LogType.INFO, DayCarePL.ModuleToLog.clLedger, "CheckChildProgInLedger", "CheckChildProgInLedger method called", DayCarePL.Common.GUID_DEFAULT);
            DayCareDataContext db = new DayCareDataContext();
            try
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.DEBUG, DayCarePL.ModuleToLog.clLedger, "CheckChildProgInLedger", "Debug CheckChildProgInLedger called", DayCarePL.Common.GUID_DEFAULT);
                int count = 0;
                count = (from l in db.Ledgers
                         where l.SchoolYearId.Equals(SchoolYearId) && l.ChildFamilyId.Equals(ChildFamilyId)
                         && l.ChildDataId.Equals(ChildDataId) && l.TransactionDate.Date.Equals(TransactionDate.Date) && l.SchoolProgramId.Equals(SchoolProgramId)
                         select l).Count();
                if (count > 0)
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
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.clLedger, "CheckChildProgInLedger", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
                return false;
            }
        }
        #endregion

        #region "Save ,Dt:19-09-2011 ,Db: A"
        public static bool Save(List<DayCarePL.LedgerProperties> lstLedger)
        {
            DayCarePL.Logger.Write(DayCarePL.LogType.INFO, DayCarePL.ModuleToLog.clLedger, "Save", "Save method called", DayCarePL.Common.GUID_DEFAULT);
            DayCareDataContext db = new DayCareDataContext();
            Ledger DBLedger = null;
            try
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.DEBUG, DayCarePL.ModuleToLog.clLedger, "Save", "Debug Save called", DayCarePL.Common.GUID_DEFAULT);
                foreach (DayCarePL.LedgerProperties objLedger in lstLedger)
                {
                    try
                    {
                        if (!CheckChildProgInLedger(objLedger.SchoolYearId, objLedger.ChildFamilyId, objLedger.ChildDataId, objLedger.TransactionDate, objLedger.SchoolProgramId))
                        {
                            DBLedger = new Ledger();
                            DBLedger.Id = Guid.NewGuid();
                            DBLedger.SchoolYearId = objLedger.SchoolYearId;
                            DBLedger.ChildFamilyId = objLedger.ChildFamilyId;
                            DBLedger.ChildDataId = objLedger.ChildDataId;
                            DBLedger.TransactionDate = objLedger.TransactionDate;
                            DBLedger.Comment = objLedger.Comment;
                            DBLedger.Detail = objLedger.Detail;
                            DBLedger.Debit = objLedger.Debit;
                            DBLedger.Credit = objLedger.Credit;
                            DBLedger.Balance = objLedger.Debit - objLedger.Credit;
                            DBLedger.AllowEdit = objLedger.AllowEdit;
                            DBLedger.PaymentId = objLedger.PaymentId;
                            DBLedger.CreatedById = objLedger.CreatedById.HasValue == true ? objLedger.CreatedById.Value : new Guid(DayCarePL.Common.GUID_DEFAULT);
                            DBLedger.CreatedDateTime = objLedger.CreatedDateTime;
                            DBLedger.LastModifiedById = objLedger.LastModifiedById;
                            DBLedger.LastModifiedDatetime = objLedger.LastModifiedDatetime;
                            DBLedger.SchoolProgramId = objLedger.SchoolProgramId;
                            db.Ledgers.InsertOnSubmit(DBLedger);
                            db.SubmitChanges();


                        }
                    }
                    catch (Exception ex)
                    {
                        DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.clLedger, "Save", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
                    }
                }
                if (lstLedger.Count > 0)
                {
                    try
                    {
                        //update closing balance
                        Guid SchoolId = clSchool.GetSchoolIdbySchoolYearId(lstLedger[0].SchoolYearId.Value);
                        if (clSchoolYear.IsSelectedYearPrevYearORNot(SchoolId, lstLedger[0].SchoolYearId.Value))//only prev year can only allow to edit closing balance. because in current year or in future year closing balance is not genrated in these year
                        {
                            clSchoolYear.UpdateClosingBalance(SchoolId, lstLedger[0].SchoolYearId.Value, lstLedger[0].ChildFamilyId);
                        }
                        //end
                    }
                    catch { }
                }
                return true;
            }
            catch (Exception ex)
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.clLedger, "Save", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
                return false;
            }
        }
        #endregion

        #region Get Last Transaction Date, Dt: 19-Sept-2011, DB: A"
        public static System.Nullable<DateTime> GetLastTransactionDate(Guid? SchoolYearId, Guid? ChildFamilyId, Guid? ChildDataId, Guid? SchoolProgramId)
        {
            DayCarePL.Logger.Write(DayCarePL.LogType.INFO, DayCarePL.ModuleToLog.clLedger, "GetLastTransactionDate", "GetLastTransactionDate method called", DayCarePL.Common.GUID_DEFAULT);
            DayCareDataContext db = new DayCareDataContext();
            try
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.DEBUG, DayCarePL.ModuleToLog.clLedger, "GetLastTransactionDate", "Debug GetLastTransactionDate called", DayCarePL.Common.GUID_DEFAULT);
                DateTime date = (from l in db.Ledgers
                                 where l.SchoolYearId.Equals(SchoolYearId) && l.ChildFamilyId.Equals(ChildFamilyId)
                                 && l.ChildDataId.Equals(ChildDataId) && l.SchoolProgramId.Equals(SchoolProgramId)
                                 orderby l.TransactionDate descending
                                 select l.TransactionDate).FirstOrDefault();
                return date;
            }
            catch (Exception ex)
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.clLedger, "GetLastTransactionDate", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
                return null;
            }
        }
        #endregion

        #region "Get Load ledger Details ,Dt:16-09-2011 ,Db:V"
        public static List<DayCarePL.LedgerProperties> LoadLedgerDetail(Guid SchoolYearId, Guid ChildFamilyId)
        {
            DayCarePL.Logger.Write(DayCarePL.LogType.INFO, DayCarePL.ModuleToLog.clLedger, "LoadLedgerDetail", "Execute LoadLedgerDetail Method", DayCarePL.Common.GUID_DEFAULT);
            clConnection.DoConnection();
            DayCareDataContext db = new DayCareDataContext();
            List<DayCarePL.LedgerProperties> lstLedgerDetail = new List<DayCarePL.LedgerProperties>();
            try
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.DEBUG, DayCarePL.ModuleToLog.clLedger, "LoadLedgerDetail", "Debug LoadLedgerDetail Method", DayCarePL.Common.GUID_DEFAULT);
                DayCarePL.LedgerProperties objLedger = null;
                var data = db.spGetLedgerDetail(SchoolYearId, ChildFamilyId);
                foreach (var c in data)
                {
                    objLedger = new DayCarePL.LedgerProperties();
                    objLedger.TransactionDate = Convert.ToDateTime(c.TransactionDate);
                    objLedger.Detail = c.Detail;
                    //objLedger.ChildName = c.FullName;
                    objLedger.FamilyName = c.FamilyTitle;
                    objLedger.Debit = c.Debit;
                    objLedger.Credit = c.Credit;
                    objLedger.Balance = c.Balance;
                    objLedger.ChildDataId = c.ChildDataId;
                    objLedger.ChildFamilyId = c.ChildFamilyId;
                    objLedger.SchoolYearId = c.SchoolYearId;
                    objLedger.Id = c.Id;
                    objLedger.AllowEdit = c.AllowEdit;
                    objLedger.PaymentId = c.PaymentId;
                    objLedger.FamilyName = c.FamilyTitle;
                    try
                    {
                        //objLedger.Comment = c.Comment.Substring(0, c.Comment.LastIndexOf(' '));
                        objLedger.Comment = c.Comment.Replace(objLedger.TransactionDate.ToString("M/d/yyyy"), "");
                    }
                    catch
                    {
                        objLedger.Comment = c.Comment;
                    }

                    objLedger.ChargeCodeCategoryId = c.ChargeCodeCategoryId;
                    objLedger.ChargeCodeCategoryName = c.ChargeCodeName;
                    objLedger.PaymentMethodId = c.PaymentMethodId;
                    lstLedgerDetail.Add(objLedger);

                }
                return lstLedgerDetail;
            }
            catch (Exception ex)
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.clLedger, "LoadLedgerDetail", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
                return null;
            }
        }
        #endregion

        #region"Print Ledger OF Family ,Dt:14-Oct-2011,Db:V"
        public static DataSet LoadLedgerOfFamily(Guid SchoolYearId, Guid ChildFamilyId)
        {
            clConnection.DoConnection();
            DayCareDataContext db = new DayCareDataContext();
            DataSet ds = new DataSet();
            try
            {

                SortedList sl = new SortedList();
                sl.Add("@SchoolYearId", SchoolYearId);
                sl.Add("@ChildFamilyId", ChildFamilyId);
                ds = clConnection.GetDataSet("spGetRptLedgerDetail", sl);
                if (ds != null && ds.Tables.Count > 0)
                {
                    for (int cnt = 0; cnt < ds.Tables[0].Rows.Count; cnt++)
                    {
                        try
                        {
                            if (!string.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[cnt]["Comment"])))
                            {
                                ds.Tables[0].Rows[cnt]["Comment"] = ds.Tables[0].Rows[cnt]["Comment"].ToString().Substring(0, ds.Tables[0].Rows[cnt]["Comment"].ToString().LastIndexOf(' '));
                            }
                        }
                        catch
                        {
                            //ds.Tables[0].Rows[cnt]["Comment"] = c.Comment;
                        }

                    }
                }
            }
            catch (Exception ex)
            {

            }
            return ds;
        }
        #endregion

        #region "Load Chagrge Code ,Dt:19-Sep-2011,Db:V"
        public static List<DayCarePL.ChargeCodeProperties> LoadChargeCode()
        {
            DayCarePL.Logger.Write(DayCarePL.LogType.INFO, DayCarePL.ModuleToLog.clLedger, "LoadChargeCode", "Execute LoadChargeCode Method", DayCarePL.Common.GUID_DEFAULT);
            clConnection.DoConnection();
            DayCareDataContext db = new DayCareDataContext();
            try
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.DEBUG, DayCarePL.ModuleToLog.clLedger, "LoadChargeCode", "Debug LoadChargeCode Method", DayCarePL.Common.GUID_DEFAULT);
                var data = (from c in db.ChargeCodes
                            orderby c.Category
                            select new DayCarePL.ChargeCodeProperties()
                            {
                                Id = c.Id,
                                DebitCrdit = c.DebitCrdit,
                                CategoryName = c.Name + " - " + (c.Category == "1" ? "Fees" : "Payment")
                            }).ToList();
                return data;
            }
            catch (Exception ex)
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.clLedger, "LoadChargeCode", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
                return null;

            }
        }
        #endregion

        #region "Update Grid Item in ledger,dt:30-Sep-2011"
        public static bool UpdateLedger(DayCarePL.LedgerProperties objLedger)
        {
            DayCarePL.Logger.Write(DayCarePL.LogType.INFO, DayCarePL.ModuleToLog.clLedger, "SaveLedger", "Execute SaveLedger Method", DayCarePL.Common.GUID_DEFAULT);
            clConnection.DoConnection();
            DayCareDataContext db = new DayCareDataContext();
            Ledger DBLedger = null;
            bool result = false;
            try
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.DEBUG, DayCarePL.ModuleToLog.clLedger, "SaveLedger", "Debug SaveLedger Method", DayCarePL.Common.GUID_DEFAULT);
                if (objLedger.Id.ToString().Equals(DayCarePL.Common.GUID_DEFAULT))
                {
                    DBLedger = new Ledger();
                    DBLedger.Id = Guid.NewGuid();
                    DBLedger.CreatedById = objLedger.CreatedById.Value;
                    DBLedger.CreatedDateTime = DateTime.Now;
                }
                else
                {
                    DBLedger = db.Ledgers.FirstOrDefault(c => c.Id.Equals(objLedger.Id));
                }
                DBLedger.TransactionDate = objLedger.TransactionDate;
                DBLedger.SchoolYearId = objLedger.SchoolYearId;
                DBLedger.ChildFamilyId = objLedger.ChildFamilyId;
                DBLedger.Detail = objLedger.Detail;
                DBLedger.Debit = objLedger.Debit;
                DBLedger.Credit = objLedger.Credit;
                DBLedger.Balance = objLedger.Debit - objLedger.Credit;
                DBLedger.AllowEdit = objLedger.AllowEdit;
                DBLedger.LastModifiedDatetime = DateTime.Now;
                DBLedger.LastModifiedById = objLedger.LastModifiedById;
                DBLedger.Comment = objLedger.Comment;
                DBLedger.PaymentMethodId = objLedger.PaymentMethodId;
                DBLedger.ChargeCodeCategoryId = objLedger.ChargeCodeCategoryId;
                if (objLedger.Id.ToString().Equals(DayCarePL.Common.GUID_DEFAULT))
                {
                    db.Ledgers.InsertOnSubmit(DBLedger);
                }
                db.SubmitChanges();
                try
                {
                    //update closing balance on 10 May 2013 By Akash
                    Guid SchoolId = clSchool.GetSchoolIdbySchoolYearId(objLedger.SchoolYearId.Value);
                    if (clSchoolYear.IsSelectedYearPrevYearORNot(SchoolId, objLedger.SchoolYearId.Value))//only prev year can only allow to edit closing balance. because in current year or in future year closing balance is not genrated in these year
                    {
                        clSchoolYear.UpdateClosingBalance(SchoolId, objLedger.SchoolYearId.Value, objLedger.ChildFamilyId);
                    }
                    //end
                }
                catch { }
                result = true;


            }
            catch (Exception ex)
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.clLedger, "SaveLedger", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
                result = false;
            }
            return result;
        }

        #endregion

        #region "Save Ledger Details ,dt:19-Sep-2011, Db:V"
        public static bool SaveLedger(DayCarePL.LedgerProperties objLedger)
        {
            DayCarePL.Logger.Write(DayCarePL.LogType.INFO, DayCarePL.ModuleToLog.clLedger, "SaveLedger", "Execute SaveLedger Method", DayCarePL.Common.GUID_DEFAULT);
            clConnection.DoConnection();
            DayCareDataContext db = new DayCareDataContext();
            Ledger DBLedger = null;
            bool result = false;
            try
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.DEBUG, DayCarePL.ModuleToLog.clLedger, "SaveLedger", "Debug SaveLedger Method", DayCarePL.Common.GUID_DEFAULT);
                if (objLedger.Id.ToString().Equals(DayCarePL.Common.GUID_DEFAULT))
                {
                    DBLedger = new Ledger();
                    DBLedger.Id = System.Guid.NewGuid();
                    DBLedger.CreatedById = objLedger.CreatedById.Value;
                    DBLedger.CreatedDateTime = DateTime.Now;



                }
                else
                {
                    DBLedger = db.Ledgers.FirstOrDefault(c => c.Id.Equals(objLedger.Id));
                }
                DBLedger.SchoolYearId = objLedger.SchoolYearId;
                DBLedger.ChildFamilyId = objLedger.ChildFamilyId;
                DBLedger.ChildDataId = objLedger.ChildDataId;
                DBLedger.TransactionDate = objLedger.TransactionDate;
                DBLedger.Detail = objLedger.Detail;
                DBLedger.Debit = objLedger.Debit;
                DBLedger.Credit = objLedger.Credit;
                DBLedger.AllowEdit = objLedger.AllowEdit;
                DBLedger.PaymentId = objLedger.PaymentId;
                DBLedger.Balance = objLedger.Debit - objLedger.Credit;
                DBLedger.LastModifiedDatetime = DateTime.Now;
                DBLedger.LastModifiedById = objLedger.LastModifiedById;
                DBLedger.Comment = objLedger.Comment;
                if (objLedger.Id.ToString().Equals(DayCarePL.Common.GUID_DEFAULT))
                {
                    db.Ledgers.InsertOnSubmit(DBLedger);
                }
                db.SubmitChanges();
                result = true;
            }
            catch (Exception ex)
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.clLedger, "SaveLedger", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
                result = false;
            }
            return result;
        }
        #endregion

        #region "Delete Ledger Detail,Dt:19-Sep-2011,Db:V"
        public static bool DeleteLedger(Guid Id)
        {
            bool result = false;
            DayCarePL.Logger.Write(DayCarePL.LogType.INFO, DayCarePL.ModuleToLog.clLedger, "Delete", "Delete Method", DayCarePL.Common.GUID_DEFAULT);
            clConnection.DoConnection();
            DayCareDataContext db = new DayCareDataContext();
            try
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.DEBUG, DayCarePL.ModuleToLog.clLedger, "Delete", "Debug Delete Method", DayCarePL.Common.GUID_DEFAULT);
                Ledger DBLedger = db.Ledgers.FirstOrDefault(c => c.Id.Equals(Id));

                

                if (DBLedger != null)
                {
                   
                    db.Ledgers.DeleteOnSubmit(DBLedger);
                    db.SubmitChanges();
                    if (DBLedger.PaymentId != null)
                    {
                        Payment DBPayment = db.Payments.FirstOrDefault(p => p.Id.Equals(DBLedger.PaymentId));
                        if (DBPayment != null)
                        {
                            db.Payments.DeleteOnSubmit(DBPayment);
                            db.SubmitChanges();
                        }
                    }
                    result = true;
                }
            }
            catch (Exception ex)
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.clLedger, "Delete", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
                result = false;
            }
            return result;
        }
        #endregion

        #region "Delete All Ledger Detail By ChildFamilyId and schoolProgramId,Dt:19-Sep-2011,Db:V"
        public static int DeleteLedgerDetailsByChildFamilyIdAndSchoolProgramId(Guid ChildFamilyId)
        {
            DayCarePL.Logger.Write(DayCarePL.LogType.INFO, DayCarePL.ModuleToLog.clLedger, "DeleteLedgerDetailsByChildFamilyIdAndSchoolProgramId", "DeleteLedgerDetailsByChildFamilyIdAndSchoolProgramId Method", DayCarePL.Common.GUID_DEFAULT);
            clConnection.DoConnection();
            SqlConnection con = clConnection.CreateConnection();
            try
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.DEBUG, DayCarePL.ModuleToLog.clLedger, "DeleteLedgerDetailsByChildFamilyIdAndSchoolProgramId", "Debug DeleteLedgerDetailsByChildFamilyIdAndSchoolProgramId Method", DayCarePL.Common.GUID_DEFAULT);
                clConnection.OpenConnection(con);
                SqlCommand cmd = clConnection.CreateCommand("spDeleteLedgerDetailsByChildFamilyIdAndSchoolProgramId", con);
                cmd.Parameters.Add(clConnection.GetInputParameter("@ChildFamilyId", ChildFamilyId));
                //cmd.Parameters.Add(clConnection.GetOutputParameter("@status", SqlDbType.Bit));
                int rowcount = Convert.ToInt16(cmd.ExecuteScalar());
                return rowcount;
            }
            catch (Exception ex)
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.clLedger, "DeleteLedgerDetailsByChildFamilyIdAndSchoolProgramId", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
                return -1;
            }
        }
        #endregion

        #region "DelereSelectdLedget, Dt:13-Jan-2012, Db: A"
        public static bool DeleteSelectedLedger(List<Guid> lstLedger)
        {
            DayCarePL.Logger.Write(DayCarePL.LogType.INFO, DayCarePL.ModuleToLog.clLedger, "DeleteSelectedLedger", "DeleteSelectedLedger Method", DayCarePL.Common.GUID_DEFAULT);
            clConnection.DoConnection();
            SqlConnection con = clConnection.CreateConnection();
            try
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.DEBUG, DayCarePL.ModuleToLog.clLedger, "DeleteSelectedLedger", "Debug DeleteSelectedLedger Method", DayCarePL.Common.GUID_DEFAULT);
                clConnection.OpenConnection(con);
                DayCareDataContext db = new DayCareDataContext();
                Ledger DBLedger = null;
                foreach (Guid Id in lstLedger)
                {
                    try
                    {
                        DBLedger = db.Ledgers.FirstOrDefault(id => id.Id == Id);
                        if (DBLedger != null)
                        {
                            db.Ledgers.DeleteOnSubmit(DBLedger);
                            db.SubmitChanges();
                        }
                    }
                    catch
                    { }
                }
                return true;
            }
            catch (Exception ex)
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.clLedger, "DeleteSelectedLedger", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
                return false;
            }
        }
        #endregion

        #region "Edit AllLedger, Dt: 16-Jan-2012, DB: A"
        public static bool EditAllLedger(List<DayCarePL.LedgerProperties> lstLedger)
        {
            DayCarePL.Logger.Write(DayCarePL.LogType.INFO, DayCarePL.ModuleToLog.clLedger, "EditAllLedger", "EditAllLedger Method", DayCarePL.Common.GUID_DEFAULT);
            DayCareDataContext db = new DayCareDataContext();
            clConnection.DoConnection();
            try
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.DEBUG, DayCarePL.ModuleToLog.clLedger, "EditAllLedger", "Debug EditAllLedger Method", DayCarePL.Common.GUID_DEFAULT);
                foreach (DayCarePL.LedgerProperties objLedger in lstLedger)
                {
                    try
                    {
                        Ledger DBLedger = db.Ledgers.FirstOrDefault(ledger => ledger.Id == objLedger.Id);
                        if (DBLedger != null)
                        {
                            DBLedger.SchoolYearId = objLedger.SchoolYearId;
                            DBLedger.ChildFamilyId = objLedger.ChildFamilyId;
                            DBLedger.ChildDataId = DBLedger.ChildDataId;
                            DBLedger.TransactionDate = objLedger.TransactionDate;
                            DBLedger.Comment = objLedger.Comment;
                            DBLedger.Detail = objLedger.Detail;
                            DBLedger.Debit = objLedger.Debit;
                            DBLedger.Credit = objLedger.Credit;
                            DBLedger.Balance = objLedger.Debit - objLedger.Credit;
                            DBLedger.AllowEdit = objLedger.AllowEdit;
                            DBLedger.PaymentId = DBLedger.PaymentId;
                            DBLedger.PaymentMethodId = objLedger.PaymentMethodId;
                            DBLedger.ChargeCodeCategoryId = objLedger.ChargeCodeCategoryId;
                            DBLedger.CreatedById = DBLedger.CreatedById;
                            DBLedger.CreatedDateTime = DBLedger.CreatedDateTime;
                            DBLedger.LastModifiedById = objLedger.LastModifiedById;
                            DBLedger.LastModifiedDatetime = DateTime.Now;
                            DBLedger.SchoolProgramId = DBLedger.SchoolProgramId;
                            db.SubmitChanges();
                        }
                    }
                    catch { }
                }
                return true;
            }
            catch (Exception ex)
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.clLedger, "EditAllLedger", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
                return false;
            }
        }
        #endregion

        #region "For Late Fee, Dt:16-Jan-2012, DB: A"
        public static List<DayCarePL.ChildFamilyProperties> GetLateFeeofFamilies(Guid SchoolId, Guid SchoolYearId, decimal Balance)
        {
            DayCarePL.Logger.Write(DayCarePL.LogType.INFO, DayCarePL.ModuleToLog.clLedger, "GetLateFeeofFamilies", "Execute GetLateFeeofFamilies Method", DayCarePL.Common.GUID_DEFAULT);
            clConnection.DoConnection();
            DayCareDataContext db = new DayCareDataContext();
            DayCarePL.ChildFamilyProperties objChildFamily;
            List<DayCarePL.ChildFamilyProperties> lstChildFamily = new List<DayCarePL.ChildFamilyProperties>();
            try
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.DEBUG, DayCarePL.ModuleToLog.clLedger, "GetLateFeeofFamilies", "Debug GetLateFeeofFamilies Method", DayCarePL.Common.GUID_DEFAULT);

                var data = db.spGetLateFeeofFamilies(SchoolId, SchoolYearId, Balance);
                foreach (var d in data)
                {

                    var PreviousClosing = GetPreviousYearClosingBalance(d.Id, SchoolYearId);

                    objChildFamily = new DayCarePL.ChildFamilyProperties();
                    objChildFamily.Id = d.Id;
                    objChildFamily.FamilyTitle = d.FamilyTitle;
                    if (d.ChildName.Length > 0)
                    {
                        objChildFamily.ChildName = objChildFamily.FamilyTitle + " [ " + d.ChildName.Substring(0, d.ChildName.LastIndexOf(", ")) + " ]";
                    }
                    objChildFamily.Debit = d.Debit;
                    objChildFamily.Credit = d.Credit;
                    if (PreviousClosing.Count > 0)
                    {
                        objChildFamily.Balance = d.Debit - d.Credit + PreviousClosing[0].ClosingBalanceAmount;
                    }
                    else
                    {
                        objChildFamily.Balance = d.Debit - d.Credit + 0;
                    }

                    lstChildFamily.Add(objChildFamily);
                }
                if (lstChildFamily != null && lstChildFamily.Count > 0)
                {
                    lstChildFamily = lstChildFamily.FindAll(i => i.Balance >= Balance);
                }
                return lstChildFamily;
            }
            catch (Exception ex)
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.clLedger, "GetLateFeeofFamilies", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
                return null;
            }
        }

        public static bool SaveLateFeeOfFamily(List<DayCarePL.LedgerProperties> lstLedger)
        {
            DayCarePL.Logger.Write(DayCarePL.LogType.INFO, DayCarePL.ModuleToLog.clLedger, "SaveLateFeeOfFamily", "SaveLateFeeOfFamily Method", DayCarePL.Common.GUID_DEFAULT);
            DayCareDataContext db = new DayCareDataContext();
            clConnection.DoConnection();
            try
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.DEBUG, DayCarePL.ModuleToLog.clLedger, "SaveLateFeeOfFamily", "Debug SaveLateFeeOfFamily Method", DayCarePL.Common.GUID_DEFAULT);
                foreach (DayCarePL.LedgerProperties objLedger in lstLedger)
                {
                    try
                    {
                        Ledger DBLedger = new Ledger();
                        DBLedger = new Ledger();
                        DBLedger.Id = Guid.NewGuid();
                        DBLedger.CreatedById = objLedger.CreatedById;
                        DBLedger.CreatedDateTime = DateTime.Now;
                        DBLedger.SchoolYearId = objLedger.SchoolYearId;
                        DBLedger.ChildFamilyId = objLedger.ChildFamilyId;
                        DBLedger.TransactionDate = objLedger.TransactionDate;
                        DBLedger.Debit = objLedger.Debit;
                        DBLedger.Credit = 0;
                        DBLedger.Balance = objLedger.Debit - objLedger.Credit;
                        DBLedger.AllowEdit = false;
                        DBLedger.Comment = objLedger.Comment;
                        DBLedger.LateFee = objLedger.LateFee; // 1. LateFee 0. No Late Fee
                        DBLedger.LastModifiedById = objLedger.LastModifiedById;
                        DBLedger.LastModifiedDatetime = DateTime.Now;
                        db.Ledgers.InsertOnSubmit(DBLedger);
                        db.SubmitChanges();
                    }
                    catch
                    {
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.clLedger, "SaveLateFeeOfFamily", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
                return false;
            }
        }
        #endregion

        #region "Get Previous years closing balance, Dt: 18-Jan-2012, DB: A"
        public static List<DayCarePL.ClosingBalance> GetPreviousYearClosingBalance(Guid ChildFamilyId, Guid SchoolYearId)
        {
            DayCareDataContext db = new DayCareDataContext();
            try
            {
                var data = db.spGetLastYearClosingBalance(SchoolYearId, ChildFamilyId);
                List<DayCarePL.ClosingBalance> lstClosingBalance = new List<DayCarePL.ClosingBalance>();
                DayCarePL.ClosingBalance objClosingBalance = null;
                foreach (var d in data)
                {
                    objClosingBalance = new DayCarePL.ClosingBalance();
                    objClosingBalance.ClosingBalanceAmount = Convert.ToDecimal(d.balance);
                    objClosingBalance.SchoolYear = d.year;
                    lstClosingBalance.Add(objClosingBalance);
                }
                return lstClosingBalance;
            }
            catch (Exception ex)
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.clLedger, "GetPreviousYearClosingBalance", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
                return null;
            }
        }
        #endregion

        #region "Report"
        public static DataSet GetAccountReceiable(Guid SchoolYearId, DateTime StartDate, string Receivable_Credit)
        {
            DataSet ds = new DataSet();
            DataSet dsNew = new DataSet();
            DataTable dt = new DataTable();
            dsNew.Tables.Add(dt);
            decimal AddAllDays = 0;
            try
            {
                SortedList sl = new SortedList();
                sl.Add("@SchoolYearId", SchoolYearId);
                sl.Add("@SelectedTranDate", StartDate);
                if (Receivable_Credit.ToLower().Equals("accountsreceivable"))
                {
                    ds = clConnection.GetDataSet("spRptAccountsReceivableReport", sl);
                }
                else if (Receivable_Credit.ToLower().Equals("credit"))
                {
                    ds = clConnection.GetDataSet("spRptCreditReport", sl);
                }
                if (ds != null)
                {
                    DataColumn col = ds.Tables[0].Columns.Add("Balance", typeof(decimal));
                    ds.Tables[0].Columns.Add("OpBalance", typeof(decimal));
                    for (int cntRow = 0; cntRow < ds.Tables[0].Rows.Count; cntRow++)
                    {
                        Guid ChildFamilyId = new Guid(ds.Tables[0].Rows[cntRow]["childfamilyid"].ToString());
                        string FamilyTitle = ds.Tables[0].Rows[cntRow]["FamilyTitle"].ToString();
                        string ChildName = ds.Tables[0].Rows[cntRow]["ChildName"].ToString().Replace(".", "\n");
                        decimal First = Convert.ToDecimal(ds.Tables[0].Rows[cntRow]["First"].ToString());
                        decimal Second = Convert.ToDecimal(ds.Tables[0].Rows[cntRow]["Second"].ToString());
                        decimal Third = Convert.ToDecimal(ds.Tables[0].Rows[cntRow]["Third"].ToString());
                        decimal Four = Convert.ToDecimal(ds.Tables[0].Rows[cntRow]["Four"].ToString());
                        decimal DebitCredit = Convert.ToDecimal(ds.Tables[0].Rows[cntRow]["DebitCredit"].ToString());

                        decimal OpeningBal = 0;
                        List<DayCarePL.ClosingBalance> lstOpBal = GetPreviousYearClosingBalance(ChildFamilyId, SchoolYearId);
                        if (lstOpBal != null && lstOpBal.Count > 0)
                        {
                            OpeningBal = lstOpBal.Sum(i => i.ClosingBalanceAmount);
                        }
                        //for (int cntCol = 0; cntCol < ds.Tables[0].Columns.Count; cntCol++)
                        //{
                        if (!string.IsNullOrEmpty(ChildName))
                        {
                            //ChildName = " [ " + ChildName.Substring(0, ChildName.LastIndexOf(", ")) + " ]";
                            //ChildName = " [ " + ChildName.Substring(0, ChildName.LastIndexOf(", ")) + " ]";
                        }
                        if (DebitCredit > 0)
                        {


                            if (Four >= DebitCredit)
                            {
                                Four -= DebitCredit;
                                DebitCredit = 0;
                            }
                            else
                            {
                                DebitCredit = DebitCredit - Four;
                                Four = 0;
                            }

                            if (Third >= DebitCredit)
                            {
                                Third -= DebitCredit;
                                DebitCredit = 0;
                            }
                            else
                            {
                                DebitCredit = DebitCredit - Third;
                                Third = 0;
                            }

                            if (Second >= DebitCredit)
                            {
                                Second -= DebitCredit;
                                DebitCredit = 0;
                            }
                            else
                            {
                                DebitCredit = DebitCredit - Second;
                                Second = 0;
                            }

                            if (First >= DebitCredit)
                            {
                                First -= DebitCredit;
                                DebitCredit = 0;
                            }
                            else
                            {
                                DebitCredit = DebitCredit - First;
                                First = 0;
                            }
                            AddAllDays = (First + Second + Third + Four);
                            if (Receivable_Credit.ToLower().Equals("accountsreceivable"))
                            {
                                if (AddAllDays == 0)
                                {
                                    AddAllDays = OpeningBal - DebitCredit;
                                }
                                else
                                {
                                    AddAllDays = OpeningBal + AddAllDays;
                                }
                            }
                            //else if (Receivable_Credit.ToLower().Equals("credit"))
                            //{
                            //    if (AddAllDays == 0)
                            //    {
                            //        AddAllDays = OpeningBal + DebitCredit;
                            //    }
                            //    else
                            //    {
                            //        AddAllDays = OpeningBal + AddAllDays;
                            //    }
                            //}
                        }
                        else
                        {
                            AddAllDays = DebitCredit - (First + Second + Third + Four);
                            if (Receivable_Credit.ToLower().Equals("accountsreceivable"))
                            {
                                AddAllDays = OpeningBal + (First + Second + Third + Four);
                            }
                        }
                        if (Receivable_Credit.ToLower().Equals("credit"))
                        {
                            AddAllDays = Math.Abs(AddAllDays);
                        }
                        ds.Tables[0].Rows[cntRow]["FamilyTitle"] = FamilyTitle;
                        ds.Tables[0].Rows[cntRow]["TranDate"] = StartDate.ToShortDateString();
                        ds.Tables[0].Rows[cntRow]["ChildName"] = ChildName;
                        ds.Tables[0].Rows[cntRow]["First"] = First;
                        ds.Tables[0].Rows[cntRow]["Second"] = Second;
                        ds.Tables[0].Rows[cntRow]["Third"] = Third;
                        ds.Tables[0].Rows[cntRow]["Four"] = Four;
                        ds.Tables[0].Rows[cntRow]["DebitCredit"] = DebitCredit;
                        ds.Tables[0].Rows[cntRow]["Balance"] = AddAllDays;// (First + Second + Third + Four);
                        ds.Tables[0].Rows[cntRow]["OpBalance"] = OpeningBal;
                        //if (Credit > 0)
                        //{
                        //    ds.Tables[0].Rows[cntRow]["Balance"] = Credit.ToString() + "(Cr.)";
                        //}
                        //else
                        //{
                        //    ds.Tables[0].Rows[cntRow]["Balance"] = First + Second + Third + Four;
                        //}
                        //}

                        if (Receivable_Credit.ToLower().Equals("accountsreceivable"))
                        {
                            if (Convert.ToDecimal(ds.Tables[0].Rows[cntRow]["Balance"]) <= 0)
                            {
                                ds.Tables[0].Rows.Remove(ds.Tables[0].Rows[cntRow]);
                                cntRow--;
                            }
                        }
                        else if (Receivable_Credit.ToLower().Equals("credit"))
                        {
                            if (Convert.ToDecimal(ds.Tables[0].Rows[cntRow]["Balance"]) == 0)
                            {
                                ds.Tables[0].Rows.Remove(ds.Tables[0].Rows[cntRow]);
                                cntRow--;
                            }
                        }

                    }
                    if (ds.Tables[0].Rows.Count == 0)
                    {
                        DataRow dr = ds.Tables[0].NewRow();
                        dr["FamilyTitle"] = "-";
                        dr["TranDate"] = StartDate.ToShortDateString();
                        dr["ChildName"] = "-";
                        dr["Balance"] = 0;
                        dr["First"] = 0;
                        dr["Second"] = 0;
                        dr["Third"] = 0;
                        dr["Four"] = 0;
                        ds.Tables[0].Rows.Add(dr);
                    }
                }
            }
            catch (Exception ex)
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.clLedger, "GetAccountReceiable", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
            }
            return ds;
        }

        public static DataSet GetFamilyWiseLateFeesReport(Guid SchoolId, Guid SchoolYearId, Guid ChildFamilyId, string SearchText)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            try
            {

                ds.Tables.Add(dt);
                SortedList sl = new SortedList();
                sl.Add("@SchoolId", SchoolId);
                sl.Add("@SchoolYearId", SchoolYearId);
                sl.Add("@ChildFamilyId", ChildFamilyId);
                sl.Add("@SearchText", SearchText);
                ds = clConnection.GetDataSet("spGetLateFeeofFamiliesReport", sl);
                if (ds != null)
                {
                    for (int cnt = 0; cnt < ds.Tables[0].Rows.Count; cnt++)
                    {
                        if (ds.Tables[0].Rows[cnt]["ChildName"] != null && !string.IsNullOrEmpty(ds.Tables[0].Rows[cnt]["ChildName"].ToString()))
                        {
                            ds.Tables[0].Rows[cnt]["FamilyTitle"] += " [ " + ds.Tables[0].Rows[cnt]["ChildName"].ToString().Substring(0, ds.Tables[0].Rows[cnt]["ChildName"].ToString().LastIndexOf(", ")) + " ]";
                        }
                    }
                    return ds;
                }
                else
                {
                    ds.Tables.Add(dt);
                    return ds;
                }
            }
            catch (Exception ex)
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.clLedger, "GetFamilyWiseLateFeesReport", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
                return ds;
            }
        }

        public static DataSet GetFamilyChildListReport(Guid SchoolId, Guid SchoolYearId,string SearchStr)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            try
            {

                ds.Tables.Add(dt);
                SortedList sl = new SortedList();
                sl.Add("@SchoolId", SchoolId);
                sl.Add("@SchoolYearId", SchoolYearId);
                sl.Add("@SearchFamily", SearchStr);//
                ds = clConnection.GetDataSet("spGetFamilyChildList", sl);
                if (ds != null)
                {
                    return ds;
                }
                else
                {
                    ds.Tables.Add(dt);
                    return ds;
                }
            }
            catch (Exception ex)
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.clLedger, "GetFamilyChildListReport", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
                return ds;
            }
            
        }
        #endregion
    }
}

