using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace DayCareBAL
{
    // NOTE: If you change the class name "FontService" here, you must also update the reference to "FontService" in App.config.
    public class FontService : IFontService
    {
        public bool Save(DayCarePL.FontProperties objFont)
        {
            return DayCareDAL.clFont.Save(objFont);
        }
        public DayCarePL.FontProperties[] LoadFont()
        {

            return DayCareDAL.clFont.LoadFont();
        }
    }
}
