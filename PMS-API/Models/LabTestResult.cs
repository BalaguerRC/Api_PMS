﻿namespace PMS_API.Models
{
    public class LabTestResult
    {
        public int? Id_LabTestResult { get; set; }
        public int Id_Patient { get; set; }
        public int Id_MedicalAppointment { get; set; }
        public int Id_LabTest { get; set; }
        public int Id_Doctor { get; set; }
        public string? Test_Result { get; set; }
        public int State_Result { get; set; }
        public DateTime Date_TestResult { get; set; }


    }
}
