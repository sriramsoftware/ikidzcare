using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DayCareDAL
{
    public class clAdditionalNotes
    {
        #region" Save Additional Notes,Dt: 13-Jan-2012,Db:V"
        public static bool Save(DayCarePL.AdditionalNotesProperties objNotes)
        {
            DayCarePL.Logger.Write(DayCarePL.LogType.INFO, DayCarePL.ModuleToLog.clAbsentReason, "Save", "Execute Save Method", DayCarePL.Common.GUID_DEFAULT);
            clConnection.DoConnection();
            bool result = false;
            DayCareDataContext db = new DayCareDataContext();
            AdditionalNote DBAdditionalNote = null;
            try
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.DEBUG, DayCarePL.ModuleToLog.clAbsentReason, "Save", "Debug Save Method", DayCarePL.Common.GUID_DEFAULT);
                if (objNotes.Id.ToString().Equals(DayCarePL.Common.GUID_DEFAULT))
                {
                    DBAdditionalNote = new AdditionalNote();
                    DBAdditionalNote.Id = System.Guid.NewGuid();
                    DBAdditionalNote.CreatedById = objNotes.CreatedById;
                    DBAdditionalNote.CreatedDateTime = DateTime.Now;
                }
                else
                {
                    DBAdditionalNote = db.AdditionalNotes.SingleOrDefault(A => A.Id.Equals(objNotes.Id));
                }
                DBAdditionalNote.ChildSchoolYearId = objNotes.ChildSchoolYearId;
                DBAdditionalNote.CommentDate = objNotes.CommentDate;
                DBAdditionalNote.Comments = objNotes.Comments;
                DBAdditionalNote.LastModifiedById = objNotes.LastModifiedById;
                DBAdditionalNote.LastMidifiedDateTime = DateTime.Now;
                if (objNotes.Id.ToString().Equals(DayCarePL.Common.GUID_DEFAULT))
                {
                    db.AdditionalNotes.InsertOnSubmit(DBAdditionalNote);

                }
                db.SubmitChanges();
                result = true;
            }
            catch (Exception ex)
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.clAbsentReason, "Save", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
                result = false;
            }
            return result;
        }
        #endregion

        #region Additional Notes,Dt:16-Jan-2012,Db:V
        public static List<DayCarePL.AdditionalNotesProperties> LoadAdditionalNotes(Guid ChildSchoolYearId)
        {
            DayCarePL.Logger.Write(DayCarePL.LogType.INFO, DayCarePL.ModuleToLog.clChildAbsentHistory, "LoadAdditionalNotes", "Execute LoadAdditionalNotes Method", DayCarePL.Common.GUID_DEFAULT);
            clConnection.DoConnection();
            DayCareDataContext db = new DayCareDataContext();
            try
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.DEBUG, DayCarePL.ModuleToLog.clAbsentReason, "LoadAdditionalNotes", "Debug LoadAdditionalNotes Method", DayCarePL.Common.GUID_DEFAULT);
                var data = (from addNotes in db.AdditionalNotes
                            join csy in db.ChildSchoolYears on addNotes.ChildSchoolYearId equals csy.Id
                            where csy.Id.Equals(ChildSchoolYearId)
                            orderby addNotes.LastMidifiedDateTime descending
                            select new DayCarePL.AdditionalNotesProperties()
                            {
                                Id = addNotes.Id,
                                Comments = addNotes.Comments,
                                CommentDate = addNotes.CommentDate,
                                ChildSchoolYearId = addNotes.ChildSchoolYearId,
                            }).ToList();
                return data;
            }
            catch (Exception ex)
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.clChildAbsentHistory, "LoadAdditionalNotes", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
                return null;
            }
        }
        #endregion

        #region Additional Notes Bind By Id,dt:16-Jan-2012,Db:V
        public static DayCarePL.AdditionalNotesProperties[] GetAdditionNoteById(Guid Id, Guid ChildSchoolYearId)
        {
            DayCarePL.Logger.Write(DayCarePL.LogType.INFO, DayCarePL.ModuleToLog.clChildAbsentHistory, "LoadAdditionalNotes", "Execute LoadAdditionalNotes Method", DayCarePL.Common.GUID_DEFAULT);
            clConnection.DoConnection();
            DayCareDataContext db = new DayCareDataContext();
            try
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.DEBUG, DayCarePL.ModuleToLog.clAbsentReason, "LoadAdditionalNotes", "Debug LoadAdditionalNotes Method", DayCarePL.Common.GUID_DEFAULT);
                var data = (from addNotes in db.AdditionalNotes
                            join csy in db.ChildSchoolYears on addNotes.ChildSchoolYearId equals csy.Id
                            where csy.Id.Equals(ChildSchoolYearId) && addNotes.Id.Equals(Id)
                            orderby addNotes.Comments ascending
                            select new DayCarePL.AdditionalNotesProperties()
                            {
                                Id = addNotes.Id,
                                Comments = addNotes.Comments,
                                CommentDate = addNotes.CommentDate,
                                ChildSchoolYearId = addNotes.ChildSchoolYearId,
                            }).ToArray();
                return data;
            }
            catch (Exception ex)
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.clChildAbsentHistory, "LoadAdditionalNotes", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
                return null;
            }
        }
        #endregion

        #region Additional Notes delete,Dt:16-Jan-2012,Db:V
        public static bool DeleteAdditionalNotes(Guid Id)
        {
            bool result = false;
            DayCarePL.Logger.Write(DayCarePL.LogType.INFO, DayCarePL.ModuleToLog.AdditionalNote, "Delete", "Delete Method", DayCarePL.Common.GUID_DEFAULT);
            clConnection.DoConnection();
            DayCareDataContext db = new DayCareDataContext();
            try
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.DEBUG, DayCarePL.ModuleToLog.AdditionalNote, "Delete", "Debug Delete Method", DayCarePL.Common.GUID_DEFAULT);
                AdditionalNote DBAdditionalNotes = db.AdditionalNotes.FirstOrDefault(c => c.Id.Equals(Id));
                if (DBAdditionalNotes != null)
                {
                    db.AdditionalNotes.DeleteOnSubmit(DBAdditionalNotes);
                    db.SubmitChanges();
                    result = true;
                }
            }
            catch (Exception ex)
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.AdditionalNote, "Delete", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
                result = false;
            }
            return result;
        }
        #endregion
    }
}
