using System;

namespace IntroducingWF.EmployeeLibrary
{
    public class VacationRequest : NotifyPropertyBase
    {
        public Employee RequestingEmployee { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
