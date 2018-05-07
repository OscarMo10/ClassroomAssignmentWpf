using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassroomAssignment.Model
{
    [Serializable]
    public class ParsedCourse
    {
        public string ClassID { get; set; }

        public string SIS_ID { get; set; }

        public string Term { get; set; }

        public string TermCode { get; set; }

        public string DepartmentCode { get; set; }

        public string SubjectCode { get; set; }

        public string CatalogNumber { get; set; }


        /// <summary>
        /// Property maps to the "Course" column of the deparment spreadsheet.
        /// </summary>
        public string CourseName { get; set; }

        public string SectionNumber { get; set; }

        public string CourseTitle { get; set; }


        public string SectionType { get; set; }



        /// <summary>
        /// Property maps to the "Title/Topic" column of the department spreadsheet.
        /// </summary>
        public string Topic { get; set; }


        public string MeetingPattern { get; set; }


        public string Instructor { get; set; }


        public string Room { get; set; }


        public string Status { get; set; }


        public string Session { get; set; }


        public string Campus { get; set; }


        public string InstructionMethod { get; set; }


        public string IntegerPartner { get; set; }


        public String Schedule { get; set; }


        public string Consent { get; set; }


        public string CreditHrsMin { get; set; }


        public string CreditHrs { get; set; }

        public String GradeMode { get; set; }


        public string Attributes { get; set; }


        public string RoomAttributes { get; set; }


        public string Enrollment { get; set; }


        public string MaximumEnrollment { get; set; }


        public string PriorEnrollment { get; set; }


        public string ProjectedEnrollment { get; set; }


        public string WaitCap { get; set; }


        public string RoomCapRequest { get; set; }


        public string CrossListings { get; set; }


        public string LinkTo { get; set; }

        public string Comments { get; set; }


        public string Notes { get; set; }
    }
}
