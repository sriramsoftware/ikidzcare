using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DayCareDAL
{
    public class clRelationship
    {
        #region "Load Relationship, Dt: 5-Aug-2011, DB: A"
        public static List<DayCarePL.RelationshipProperties> LoadRelationship(Guid SchoolId)
        {
            DayCarePL.Logger.Write(DayCarePL.LogType.INFO, DayCarePL.ModuleToLog.clRelationship, "LoadRelationship", "Execute LoadRelationship Method", DayCarePL.Common.GUID_DEFAULT);
            clConnection.DoConnection();
            DayCareDataContext db = new DayCareDataContext();
            try
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.DEBUG, DayCarePL.ModuleToLog.clRelationship, "LoadRelationship", "Debug LoadRelationship Method", DayCarePL.Common.GUID_DEFAULT);

                var Relationship = (from r in db.Relationships
                                    where r.SchoolId.Equals(SchoolId)
                                    orderby r.Name
                                    select new DayCarePL.RelationshipProperties()
                                    {
                                        Id = r.Id,
                                        SchoolId = r.SchoolId,
                                        Name = r.Name,
                                        Active = r.Active,
                                        Comments = r.Comments,
                                        LastModifiedById = r.LastModifiedById,
                                        LastModifiedDatetime = r.LastModifiedDatetime
                                    }).ToList();
                return Relationship;
            }
            catch (Exception ex)
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.clRelationship, "LoadRelationship", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
                return null;
            }
        }
        #endregion

        #region "Save, Dt: 4-Aug-2011, DB: A"
        public static bool Save(DayCarePL.RelationshipProperties objRelationship)
        {
            DayCarePL.Logger.Write(DayCarePL.LogType.INFO, DayCarePL.ModuleToLog.clRelationship, "Save", "Execute Save Method", DayCarePL.Common.GUID_DEFAULT);
            clConnection.DoConnection();

            DayCareDataContext db = new DayCareDataContext();
            Relationship DBRelationship = null;
            try
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.DEBUG, DayCarePL.ModuleToLog.clRelationship, "Save", "Debug Save Method", DayCarePL.Common.GUID_DEFAULT);
                
                if (objRelationship.Id.ToString().Equals(DayCarePL.Common.GUID_DEFAULT))
                {
                    DBRelationship = new Relationship();
                    DBRelationship.Id = System.Guid.NewGuid();
                }
                else
                {
                    DBRelationship = db.Relationships.SingleOrDefault(u => u.Id.Equals(objRelationship.Id));
                
                }
                DBRelationship.Name = objRelationship.Name;
                DBRelationship.SchoolId = objRelationship.SchoolId;
                DBRelationship.Active = objRelationship.Active;
                DBRelationship.Comments = objRelationship.Comments;
                DBRelationship.LastModifiedById = objRelationship.LastModifiedById;
                DBRelationship.LastModifiedDatetime = DateTime.Now;
                
                if (objRelationship.Id.ToString().Equals(DayCarePL.Common.GUID_DEFAULT))
                {
                    db.Relationships.InsertOnSubmit(DBRelationship);
                }
                db.SubmitChanges();
                return true;
            }
            catch (Exception ex)
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.clRelationship, "Save", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
                return false;       
            }
        }
        #endregion

        #region Chech Duplicate Relationship Name, Dt: 4-Aug-2011, DB: A"
        public static bool CheckDuplicateRelationshipName(string RelationshipName, Guid RelationshipId, Guid SchoolId)
        {
            DayCarePL.Logger.Write(DayCarePL.LogType.INFO, DayCarePL.ModuleToLog.clRelationship, "CheckDuplicateRelationshipName", "Execute CheckDuplicateRelationshipName Method", DayCarePL.Common.GUID_DEFAULT);
            clConnection.DoConnection();
            DayCareDataContext db = new DayCareDataContext();
            bool result = false;
            try
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.DEBUG, DayCarePL.ModuleToLog.clRelationship, "CheckDuplicateRelationshipName", "Debug CheckDuplicateRelationshipName Method", DayCarePL.Common.GUID_DEFAULT);
                int count;

                if (RelationshipId.ToString().Equals(DayCarePL.Common.GUID_DEFAULT))
                {
                    count = (from r in db.Relationships
                             where r.Name.Equals(RelationshipName)
                             && r.SchoolId.Equals(SchoolId)
                             select r).Count();
                }
                else
                {
                    count = (from r in db.Relationships
                             where r.Name.Equals(RelationshipName)
                             && r.SchoolId.Equals(SchoolId) && !r.Id.Equals(RelationshipId)
                             select r).Count();
                }
                if (count > 0)
                {
                    result = true;
                }
                else
                {
                    result = false;
                }
            }
            catch (Exception ex)
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.clRelationship, "CheckDuplicateRelationshipName", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
                result = false;
            }
            return result;
        }
        #endregion
    }
}
