using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DayCareSchedular
{
    class Program
    {
        static void Main(string[] args)
        {
            DayCareBAL.StaffService proxyStaffService = new DayCareBAL.StaffService();
            Guid SchoolId = new Guid("8CA767A0-5E36-4343-8B1D-5ECC40EB9E1B");
            DayCarePL.StaffProperties objStaffDetails = proxyStaffService.LoadStaffDetailsByUserNameAndPassword("schooladmin", "schooladmin", SchoolId);
            GetChildProgEnrollmentFeeDetail(objStaffDetails.ScoolYearId);
        }

        public static void GetChildProgEnrollmentFeeDetail(Guid SchoolYearId)
        {
            //DayCarePL.Logger.Write(DayCarePL.LogType.INFO, DayCarePL.ModuleToLog.Ledger, "GetChildProgEnrollmentFeeDetail", "GetChildProgEnrollmentFeeDetail method called", DayCarePL.Common.GUID_DEFAULT);
            try
            {
                //  DayCarePL.Logger.Write(DayCarePL.LogType.DEBUG, DayCarePL.ModuleToLog.Ledger, "GetChildProgEnrollmentFeeDetail", "Debug GetChildProgEnrollmentFeeDetail called", DayCarePL.Common.GUID_DEFAULT);
                DayCareBAL.LedgerService proxyLedger = new DayCareBAL.LedgerService();
                List<DayCarePL.ChildProgEnrollmentProperties> lstChildProgEnrollmentFeeDetail = proxyLedger.GetChildProgEnrollmentFeeDetail(SchoolYearId, new Guid(DayCarePL.Common.GUID_DEFAULT));
                if (lstChildProgEnrollmentFeeDetail != null)
                {
                    bool result = false;
                    List<DayCarePL.LedgerProperties> lstChildEnrollForLedger = new List<DayCarePL.LedgerProperties>();
                    DayCarePL.LedgerProperties objChildEnrollForLedger = null;
                    foreach (DayCarePL.ChildProgEnrollmentProperties objChildProgEnrollmentFeeDetail in lstChildProgEnrollmentFeeDetail)
                    {
                        string strDay = "";
                        DateTime StartDate = DateTime.Now, EndDate = DateTime.Now;
                        if (objChildProgEnrollmentFeeDetail.StartDate != null)
                        {
                            StartDate = objChildProgEnrollmentFeeDetail.StartDate.Value;
                        }
                        if (objChildProgEnrollmentFeeDetail.EndDate != null)
                        {
                            EndDate = objChildProgEnrollmentFeeDetail.EndDate.Value;
                        }
                        objChildEnrollForLedger = new DayCarePL.LedgerProperties();
                        switch (objChildProgEnrollmentFeeDetail.EffectiveWeekDay)
                        {
                            case 1:
                                {
                                    strDay = "Monday";
                                    break;
                                }
                            case 2:
                                {
                                    strDay = "Tuesday";
                                    break;
                                }
                            case 3:
                                {
                                    strDay = "Wednesday";
                                    break;
                                }
                            case 4:
                                {
                                    strDay = "Thursday";
                                    break;
                                }
                            case 5:
                                {
                                    strDay = "Friday";
                                    break;
                                }
                        }
                        if (objChildProgEnrollmentFeeDetail.EffectiveWeekDay != null && objChildProgEnrollmentFeeDetail.EffectiveWeekDay != 0)
                        {
                            DateTime? OldDate = proxyLedger.GetLastTransactionDate(SchoolYearId, objChildProgEnrollmentFeeDetail.ChildFamilyId, objChildProgEnrollmentFeeDetail.ChildDataId, objChildProgEnrollmentFeeDetail.SchoolProgramId);
                            if (OldDate != null)
                            {
                                DateTime LastDate = OldDate.Value;
                                if (LastDate.Equals(new DateTime()))
                                {
                                    LastDate = StartDate;
                                }
                                //else
                                //{
                                //    LastDate = LastDate.AddDays(7);
                                //}
                                while (LastDate.Date <= DateTime.Now.Date && LastDate.Date <= EndDate.Date)
                                {
                                    if (LastDate.DayOfWeek.ToString().ToLower().Equals(strDay.ToLower()))
                                    {
                                        break;
                                    }
                                    LastDate = LastDate.AddDays(1);
                                }
                                DateTime TranDate;
                                if (!LastDate.Equals(new DateTime()))
                                {
                                    while (LastDate.Date <= DateTime.Now.Date && LastDate.Date <= EndDate.Date)
                                    {
                                        DateTime.TryParse(LastDate.Month + "/" + LastDate.Day + "/" + LastDate.Year, out TranDate);
                                        if (TranDate.Equals(new DateTime()))
                                        {
                                            TranDate = new DateTime(LastDate.Year, LastDate.Month, System.DateTime.DaysInMonth(LastDate.Year, LastDate.Month));
                                        }
                                        if (!TranDate.Equals(new DateTime()) && TranDate.Date <= System.DateTime.Now.Date && TranDate.Date <= EndDate.Date && !OldDate.Value.Date.Equals(LastDate.Date))
                                        {
                                            objChildEnrollForLedger = new DayCarePL.LedgerProperties();
                                            objChildEnrollForLedger.TransactionDate = TranDate + DateTime.Now.TimeOfDay;
                                            objChildEnrollForLedger.Comment = objChildProgEnrollmentFeeDetail.ProgramTitle + " " + objChildProgEnrollmentFeeDetail.FeesPeriodName + " " + strDay;
                                            //objChildEnrollForLedger.Detail = objChildProgEnrollmentFeeDetail.ProgramTitle + " " + objChildProgEnrollmentFeeDetail.FeesPeriodName + " " + strDay;
                                            SetLegderProperties(SchoolYearId, lstChildEnrollForLedger, objChildEnrollForLedger, objChildProgEnrollmentFeeDetail);
                                        }
                                        LastDate = LastDate.AddDays(7);
                                    }
                                }
                            }
                        }

                        if (objChildProgEnrollmentFeeDetail.EffectiveMonthDay != null)// && objChildProgEnrollmentFeeDetail.EffectiveMonthDay.Equals(DateTime.Now.Month))
                        {
                            DateTime? OldDate = proxyLedger.GetLastTransactionDate(SchoolYearId, objChildProgEnrollmentFeeDetail.ChildFamilyId, objChildProgEnrollmentFeeDetail.ChildDataId, objChildProgEnrollmentFeeDetail.SchoolProgramId);
                            if (OldDate != null)
                            {
                                DateTime LastDate = OldDate.Value;
                                if (LastDate.Equals(new DateTime()))
                                {
                                    LastDate = StartDate;
                                }
                                //else
                                //{
                                //    LastDate = LastDate.AddMonths(1);
                                //}
                                DateTime TranDate;
                                if (!LastDate.Equals(new DateTime()))
                                {
                                    while (LastDate.Date <= DateTime.Now.Date && LastDate.Date <= EndDate.Date)
                                    {
                                        DateTime.TryParse(LastDate.Month + "/" + objChildProgEnrollmentFeeDetail.EffectiveMonthDay.Value + "/" + LastDate.Year, out TranDate);
                                        if (TranDate.Equals(new DateTime()))
                                        {
                                            TranDate = new DateTime(LastDate.Year, LastDate.Month, System.DateTime.DaysInMonth(LastDate.Year, LastDate.Month));
                                        }
                                        if (!TranDate.Equals(new DateTime()) && TranDate.Date <= System.DateTime.Now.Date && TranDate.Date <= EndDate.Date && !OldDate.Value.Date.Equals(LastDate.Date))
                                        {
                                            objChildEnrollForLedger = new DayCarePL.LedgerProperties();
                                            objChildEnrollForLedger.TransactionDate = TranDate + DateTime.Now.TimeOfDay;
                                            objChildEnrollForLedger.Comment = objChildProgEnrollmentFeeDetail.ProgramTitle + " " + objChildProgEnrollmentFeeDetail.FeesPeriodName + " " + objChildEnrollForLedger.TransactionDate.ToShortDateString();
                                            //objChildEnrollForLedger.Detail = objChildProgEnrollmentFeeDetail.ProgramTitle + " " + objChildProgEnrollmentFeeDetail.FeesPeriodName + " " + objChildEnrollForLedger.TransactionDate.ToShortDateString();
                                            SetLegderProperties(SchoolYearId, lstChildEnrollForLedger, objChildEnrollForLedger, objChildProgEnrollmentFeeDetail);
                                        }
                                        LastDate = LastDate.AddMonths(1);
                                    }
                                }
                            }
                        }

                        if (objChildProgEnrollmentFeeDetail.EffectiveYearDate != null)// && objChildProgEnrollmentFeeDetail.EffectiveYearDate.Value.
                        {
                            DateTime? OldDate = proxyLedger.GetLastTransactionDate(SchoolYearId, objChildProgEnrollmentFeeDetail.ChildFamilyId, objChildProgEnrollmentFeeDetail.ChildDataId, objChildProgEnrollmentFeeDetail.SchoolProgramId);
                            if (OldDate != null)
                            {
                                DateTime LastDate = OldDate.Value;
                                if (LastDate.Equals(new DateTime()))
                                {
                                    LastDate = StartDate;
                                }
                                //else
                                //{
                                //    LastDate = LastDate.AddYears(1);
                                //}
                                DateTime TranDate;
                                if (!LastDate.Equals(new DateTime()))
                                {
                                    while (LastDate.Date <= System.DateTime.Now.Date && LastDate.Date <= EndDate.Date)
                                    {
                                        DateTime.TryParse(objChildProgEnrollmentFeeDetail.EffectiveYearDate.Value.Month + "/" + objChildProgEnrollmentFeeDetail.EffectiveYearDate.Value.Day + "/" + LastDate.Year, out TranDate);
                                        if (!TranDate.Equals(new DateTime()) && TranDate.Date <= System.DateTime.Now.Date && TranDate.Date <= EndDate.Date && !OldDate.Value.Date.Equals(LastDate.Date))
                                        {
                                            objChildEnrollForLedger = new DayCarePL.LedgerProperties();
                                            objChildEnrollForLedger.TransactionDate = TranDate + DateTime.Now.TimeOfDay;
                                            objChildEnrollForLedger.Comment = objChildProgEnrollmentFeeDetail.ProgramTitle + " " + objChildProgEnrollmentFeeDetail.FeesPeriodName + " " + objChildEnrollForLedger.TransactionDate.ToShortDateString();
                                            //objChildEnrollForLedger.Detail = objChildProgEnrollmentFeeDetail.ProgramTitle + " " + objChildProgEnrollmentFeeDetail.FeesPeriodName + " " + objChildEnrollForLedger.TransactionDate.ToShortDateString();
                                            SetLegderProperties(SchoolYearId, lstChildEnrollForLedger, objChildEnrollForLedger, objChildProgEnrollmentFeeDetail);
                                        }
                                        LastDate = LastDate.AddYears(1);
                                    }
                                }
                            }
                        }
                    }
                    if (lstChildEnrollForLedger != null && lstChildEnrollForLedger.Count > 0)
                        result = proxyLedger.Save(lstChildEnrollForLedger);
                }
            }
            catch (Exception ex)
            {
                // DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.LedgerOfFamily, "GetChildProgEnrollmentFeeDetail", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
            }
        }

        private static void SetLegderProperties(Guid SchoolYearId, List<DayCarePL.LedgerProperties> lstChildEnrollForLedger, DayCarePL.LedgerProperties objChildEnrollForLedger, DayCarePL.ChildProgEnrollmentProperties objChildProgEnrollmentFeeDetail)
        {
            objChildEnrollForLedger.ChildFamilyId = objChildProgEnrollmentFeeDetail.ChildFamilyId;
            objChildEnrollForLedger.SchoolYearId = SchoolYearId;
            objChildEnrollForLedger.ChildDataId = objChildProgEnrollmentFeeDetail.ChildDataId;
            objChildEnrollForLedger.Debit = objChildProgEnrollmentFeeDetail.Fees.Value;
            objChildEnrollForLedger.Credit = 0;
            objChildEnrollForLedger.AllowEdit = false;
            objChildEnrollForLedger.PaymentId = null;
            objChildEnrollForLedger.CreatedById = new Guid("9E10D25A-9CA2-488D-BB59-4890D3786961");
            objChildEnrollForLedger.CreatedDateTime = DateTime.Now;
            objChildEnrollForLedger.LastModifiedById = new Guid("9E10D25A-9CA2-488D-BB59-4890D3786961");
            objChildEnrollForLedger.LastModifiedDatetime = DateTime.Now;
            objChildEnrollForLedger.SchoolProgramId = objChildProgEnrollmentFeeDetail.SchoolProgramId;
            lstChildEnrollForLedger.Add(objChildEnrollForLedger);
        }

    }
}
