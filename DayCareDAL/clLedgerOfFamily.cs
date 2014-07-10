using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DayCareDAL
{
    public class clLedgerOfFamily
    {
        #region "Load ChildFamily Dt:31-Aug-2011, Db:V"
        public static List<DayCarePL.ChildFamilyProperties> LoadChildFamily(Guid SchoolId, Guid SchoolYearId)
        {
            DayCarePL.Logger.Write(DayCarePL.LogType.INFO, DayCarePL.ModuleToLog.clLedgerOfFamily, "LoadChildFamily", "Execute LoadChildFamily Method", DayCarePL.Common.GUID_DEFAULT);
            clConnection.DoConnection();
            DayCareDataContext db = new DayCareDataContext();
            DayCarePL.ChildFamilyProperties objChildFamily;
            List<DayCarePL.ChildFamilyProperties> lstChildFamily = new List<DayCarePL.ChildFamilyProperties>();
            try
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.DEBUG, DayCarePL.ModuleToLog.clLedgerOfFamily, "LoadChildFamily", "Debug LoadChildFamily Method", DayCarePL.Common.GUID_DEFAULT);

                var data = db.spGetChildFamilyForLEdgerOfFammily(SchoolId, SchoolYearId);
                foreach (var d in data)
                {
                    objChildFamily = new DayCarePL.ChildFamilyProperties();
                    objChildFamily.Id = d.Id;
                    objChildFamily.Email = d.Email;
                    objChildFamily.FamilyTitle = d.FamilyTitle;
                    objChildFamily.HomePhone = d.HomePhone;
                    if (d.ChildName.Length > 0)
                    {
                        objChildFamily.ChildName = "[ " + d.ChildName.Substring(0, d.ChildName.LastIndexOf(", ")) + " ]";
                    }
                    objChildFamily.Debit = d.Debit;
                    objChildFamily.Credit = d.Credit;

                    try
                    {

                        objChildFamily.OpBal = d.OpBal.HasValue ? d.OpBal.Value : 0;
                    }
                    catch
                    {
                        objChildFamily.OpBal = 0;
                    }
                    objChildFamily.Balance = d.Balance + objChildFamily.OpBal;// d.Debit - d.Credit;
                    try
                    {
                        objChildFamily.Active = d.Active == null ? false : d.Active.Value;
                    }
                    catch
                    {
                        objChildFamily.Active = false;
                    }

                    lstChildFamily.Add(objChildFamily);
                }
                return lstChildFamily;
            }
            catch (Exception ex)
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.clLedgerOfFamily, "LoadChildFamily", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
                return null;

            }
        }
        #endregion

        #region "For Late fee"
        #region "Load Childfammily wise last transaction date and fees amount ,Dt:13-oct-2011,Db:A"
        public static DayCarePL.LedgerProperties LoadChildFamilyWiseTranDateAmount(Guid ChildFamilyId)
        {
            DayCarePL.Logger.Write(DayCarePL.LogType.INFO, DayCarePL.ModuleToLog.clLedgerOfFamily, "LoadChildFamilyWiseTranDateAmount", "Execute LoadChildFamilyWiseTranDateAmount Method", DayCarePL.Common.GUID_DEFAULT);
            clConnection.DoConnection();
            DayCareDataContext db = new DayCareDataContext();
            try
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.DEBUG, DayCarePL.ModuleToLog.clLedgerOfFamily, "LoadChildFamilyWiseTranDateAmount", "Debug LoadChildFamilyWiseTranDateAmount Method", DayCarePL.Common.GUID_DEFAULT);
                DayCarePL.LedgerProperties data = (from l in db.Ledgers
                                                   where l.LateFee.Equals(1) && l.ChildFamilyId.Equals(ChildFamilyId)
                                                   orderby l.TransactionDate descending
                                                   select new DayCarePL.LedgerProperties()
                                                   {
                                                       Id = l.Id,
                                                       Debit = l.Debit,
                                                       TransactionDate = l.TransactionDate
                                                   }).FirstOrDefault();
                return data;
            }
            catch (Exception ex)
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.clLedgerOfFamily, "LoadChildFamilyWiseTranDateAmount", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
                return null;
            }
        }
        #endregion

        #region Save
        public static bool Save(DayCarePL.LedgerProperties objLedger)
        {
            DayCarePL.Logger.Write(DayCarePL.LogType.INFO, DayCarePL.ModuleToLog.clLedgerOfFamily, "Save", "Execute Save Method", DayCarePL.Common.GUID_DEFAULT);
            clConnection.DoConnection();
            DayCareDataContext db = new DayCareDataContext();
            Ledger DBLedger = null;
            try
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.DEBUG, DayCarePL.ModuleToLog.clLedgerOfFamily, "Save", "Debug Save Method", DayCarePL.Common.GUID_DEFAULT);
                if (objLedger.Id.ToString().Equals(DayCarePL.Common.GUID_DEFAULT))
                {
                    DBLedger = new Ledger();
                    DBLedger.Id = Guid.NewGuid();
                    DBLedger.CreatedById = objLedger.CreatedById;
                    DBLedger.CreatedDateTime = DateTime.Now;
                }
                else
                {
                    DBLedger = db.Ledgers.FirstOrDefault(i => i.Id.Equals(objLedger.Id));
                }
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
                return true;
            }
            catch (Exception ex)
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.clLedgerOfFamily, "Save", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
                return false;
            }
        }
        #endregion
        #endregion

        #region "Geting Last Updated Ledger date"
        public static DayCarePL.LedgerProperties GetLastUpdateLedgerDate()
        {
            DayCarePL.Logger.Write(DayCarePL.LogType.INFO, DayCarePL.ModuleToLog.clLedgerOfFamily, "LoadChildFamilyWiseTranDateAmount", "Execute LoadChildFamilyWiseTranDateAmount Method", DayCarePL.Common.GUID_DEFAULT);
            clConnection.DoConnection();
            DayCareDataContext db = new DayCareDataContext();
            try
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.DEBUG, DayCarePL.ModuleToLog.clLedgerOfFamily, "LoadChildFamilyWiseTranDateAmount", "Debug LoadChildFamilyWiseTranDateAmount Method", DayCarePL.Common.GUID_DEFAULT);
                DayCarePL.LedgerProperties data = (from l in db.Ledgers
                                                   orderby l.LastModifiedDatetime descending
                                                   select new DayCarePL.LedgerProperties()
                                                   {
                                                       LastModifiedDatetime = l.LastModifiedDatetime .Value
                                                   }).FirstOrDefault();
                return data;

            }
            catch (Exception ex)
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.clLedgerOfFamily, "LoadChildFamilyWiseTranDateAmount", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
                return null;
            }
        }
        #endregion
    }
}
