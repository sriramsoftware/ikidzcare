using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DayCareDAL
{
    public class clFont
    {
        #region "Save Font, Dt:8-Aug-2011,Db:V"
        public static bool Save(DayCarePL.FontProperties objFont)
        {
            DayCarePL.Logger.Write(DayCarePL.LogType.INFO, DayCarePL.ModuleToLog.clFont, "Save", "Execute Save Method", DayCarePL.Common.GUID_DEFAULT);
            clConnection.DoConnection();
            bool result = false;
            DayCareDataContext db = new DayCareDataContext();
            Font DBFont = null;
            try
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.DEBUG, DayCarePL.ModuleToLog.clFont, "Save", "Debug Save Method", DayCarePL.Common.GUID_DEFAULT);
                if (objFont.Id.ToString().Equals(DayCarePL.Common.GUID_DEFAULT))
                {
                    DBFont = new Font();
                    DBFont.Id = System.Guid.NewGuid();
                }
                else
                {
                    DBFont=db.Fonts.SingleOrDefault(F => F.Id.Equals(objFont.Id));

                }
                DBFont.Name=objFont.Name;
                DBFont.Color=objFont.Color;
                DBFont.Size= objFont.Size;
                DBFont.Active=objFont.Active;
                if (objFont.Id.ToString().Equals(DayCarePL.Common.GUID_DEFAULT))
                {
                 db.Fonts.InsertOnSubmit(DBFont);
                }
                db.SubmitChanges();
                result=true;
                
            }
            catch (Exception ex)
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.clFont, "Save", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
                result = false;

            }
            return result;
        }
        #endregion

        #region "Load Font, Dt:8-Aug-2011, Db:V"
        public static DayCarePL.FontProperties[] LoadFont()
        {
            DayCarePL.Logger.Write(DayCarePL.LogType.INFO, DayCarePL.ModuleToLog.clFont, "LoadFont", "Execute LoadFont Method", DayCarePL.Common.GUID_DEFAULT);
            clConnection.DoConnection();
            DayCareDataContext db = new DayCareDataContext();
            try
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.DEBUG, DayCarePL.ModuleToLog.clFont, "LoadFont", "Debug LoadFont Method", DayCarePL.Common.GUID_DEFAULT);
                var data = (from c in db.Fonts
                            orderby c.Name ascending
                            select new DayCarePL.FontProperties()
                            {
                                Id = c.Id,
                                Name = c.Name,
                                Color = c.Color,
                                Size = c.Size,
                                Active = c.Active,
                            }).ToArray();
                return data;
                
            }
            catch (Exception ex)
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.clFont, "LoadFont", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
                return null;
            }
        }
        #endregion

        
    }
}
