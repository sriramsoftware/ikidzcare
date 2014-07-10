﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DayCarePL 
{
    public enum ModuleToLog 
    {
        clRole,
        clCountry,
        clState,
        clFeesPeriod,
        clSchool,
        clChargeCode,
        clStaff,
        clStaffCategory,
        clUserGroup,
        clSchoolYear,
        clStaffSchoolYear,
        clClassCategory,
        clClassRoom,
        clRelationship,
        clEnrollmentStatus,
        clEmploymentStatus,
        clFont,
        clHoursOfOperation,
        clProgSchedule,
        clChildProgEnrollment,
        clChildData,
        clProgStaff,
        clProgClassCategory,
        clChildEnrollmentStatus,
        clProgClassRoom,
        clSchoolProgram,
        clFamilyData,
        clChildAbsentHistory,
        clChildSchedule,
        clAddEditChild,
        clChildProgEnrollmentFeeDetail,
        clChildSchoolYear,
        clMyAccount,
        clChildList,
        Role,
        EmploymentStatus,
        FeesPeriod,
        Country,
        State,
        School,
        ChildProgEnrollment,
        ChildEnrollmentStatus,
        ChargeCode,
        Staff,
        AdditionalNote,
        StaffList,
        staffCategory,
        UserGroup, 
        SchoolYear,
        ChildData,
        clAbsentReason,
        clLedger,
        AbsentReason,
        ClassCategory,
        clStaffAttendenceHistory,
        clFamilyPayment,
        ClassRoom,
        Relationship,
        EnrollmentStatus,
        Font,
        HoursOfOperation,
        ProgSchedule,
        ProgStaff,
        ProgClassCategory,
        ProgClassRoom,
        SchoolProgram,
        FamilyInfo,
        ChildAbsentHistory,
        ChildSchedule,
        clChildFamily,
        clLedgerOfFamily,
        ChildFamily,
        FamilyData,
        AddEditChild,
        FamilyPayment,
        Ledger,
        LedgerOfFamily,
        ChildProgEnrollmentFeeDetail,
        ChildSchoolYear,
        MyAccount,
        ChildList,
        AttendenceReport,
        Families,
        SchoolProgramFeesDetail,
        LateFee,
        rptFamilyWiseLateFeesReport
    }

    public enum LogType 
    {
        DEBUG,
        EXCEPTION,
        INFO
    }
}