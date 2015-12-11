using System;

namespace IntroducingWF.EmployeeLibrary
{
    public class Employee 
    {
        private Employee mgr;

        public string Name { get; set; }
        public string Email { get; set; }
        
        public Employee Manager {
            get
            {
                return mgr ?? (mgr = new Employee
                {
                    Name = "Jane Manager",
                    Email = "jane@contoso.org"
                });
            }
            set
            {
                mgr = value;
            }
        }

        public int GetRemainingVacationDays()
        {
            return 2;
        }
    }
}
