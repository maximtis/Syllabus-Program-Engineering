using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Syllabus_Program_Engineering
{
    abstract class Element
    { //base class
    }
    enum LecturerType { Lecture = 0, Laboratory = 1 }

    class Lecturer : Element
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string LastName { get; set; }
        public LecturerType Obligation { get; set; }
    }
    class Subject : Element
    {
        public string Name { get; set; }
        public Lecturer Lecturer { get; set; }
        public int CourseNumber { get; set; }
        public int Hours { get; set; }
        public int CouplePerPeriod
        {
            get
            {
                return Convert.ToInt32(
                    Math.Round((double)(Hours / 4) / 8, MidpointRounding.ToEven));
            }
        }
        public int AlreadyUsed { get; set; }
    }
    class Audience : Element
    {
        private List<List<bool>> BusyMatrix = new List<List<bool>>()
        {
            new List<bool>() {false,false,false,false,false},
            new List<bool>() {false,false,false,false,false},
            new List<bool>() {false,false,false,false,false},
            new List<bool>() {false,false,false,false,false},
            new List<bool>() {false,false,false,false,false}
        };

        public string Number { get; set; }
        public bool IsBusy(int day, int number)
        {
            return BusyMatrix[day][number];
        }
        public void SetBusy(int day, int number)
        {
            BusyMatrix[day][number] = true;
        }
        public void SetFree(int day, int number)
        {
            BusyMatrix[day][number] = false;
        }
    }

    class DataBaseContex
    {
        public DataBaseContex()
        {
            audiences = new List<Audience>();
            lecturers = new List<Lecturer>();
            subjects = new List<Subject>();
        }

        private List<Audience> audiences;
        private List<Lecturer> lecturers;
        private List<Subject> subjects;

        public Audience GetAudienceByName(string number)
        {
            if (number == string.Empty)
                throw new ArgumentNullException();

            foreach (Audience aud in audiences)
                if (aud.Number == number)
                    return aud;

            return null;
        }
        public Lecturer GetLecturerByName(string name)
        {
            if (name == string.Empty)
                throw new ArgumentNullException();

            foreach (Lecturer lec in lecturers)
                if (lec.Name == name)
                    return lec;

            return null;
        }
        public Subject GetSubjectByName(string name)
        {
            if (name == string.Empty)
                throw new ArgumentNullException();

            foreach (Subject sub in subjects)
                if (sub.Name == name)
                    return sub;

            return null;
        }

        internal List<Audience> Audiences
        {
            get
            {
                return audiences;
            }

            set
            {
                audiences = value;
            }
        }
        internal List<Lecturer> Lecturers
        {
            get
            {
                return lecturers;
            }

            set
            {
                lecturers = value;
            }
        }
        internal List<Subject> Subjects
        {
            get
            {
                return subjects;
            }

            set
            {
                subjects = value;
            }
        }

    }
    class GroupContext
    {
        private String Name;
        private String Course;
        private List<Audience> audiences;
        private List<Lecturer> lecturers;
        private List<Subject> subjects;
        private Schedule schedule;

        internal List<Audience> Audiences
        {
            get
            {
                return audiences;
            }

            set
            {
                audiences = value;
            }
        }
        internal List<Lecturer> Lecturers
        {
            get
            {
                return lecturers;
            }

            set
            {
                lecturers = value;
            }
        }
        internal List<Subject> Subjects
        {
            get
            {
                return subjects;
            }

            set
            {
                subjects = value;
            }
        }

        internal Schedule Schedule
        {
            get
            {
                return schedule;
            }

            set
            {
                schedule = value;
            }
        }
    }

    class ScheduleDayItem
    {
        public ScheduleDayItem()
        {
            subject = new Subject();
            audience = new Audience();
            lecturer = new Lecturer();
        }
        private Subject subject;
        private Audience audience;
        private Lecturer lecturer;
        internal Subject Subject
        {
            get
            {
                return subject;
            }

            set
            {
                subject = value;
            }
        }
        internal Audience Audience
        {
            get
            {
                return audience;
            }

            set
            {
                audience = value;
            }
        }
        internal Lecturer Lecturer
        {
            get
            {
                return lecturer;
            }

            set
            {
                lecturer = value;
            }
        }
    }
    class ScheduleDay
    {
        public ScheduleDay()
        {
            day = new List<ScheduleDayItem>();
        }
        List<ScheduleDayItem> day;
        public int CountOfSubjectByName(string Name)
        {
            int i = 0;
            foreach(ScheduleDayItem sdi in Day)
                if (sdi.Subject.Name == Name)
                    i++;
            return i;
        }
        internal List<ScheduleDayItem> Day
        {
            get
            {
                return day;
            }

            set
            {
                day = value;
            }
        }
    }
    class Schedule
    {
        public Schedule()
        {
            week = new List<ScheduleDay>();
        }

        public static List<string> DayOfWeek = new List<string>()
        {
            "Понеділок",
            "Вівторок",
            "Середа",
            "Четвер",
            "П'ятниця"
        };
        List<ScheduleDay> week;
        internal List<ScheduleDay> Week
        {
            get
            {
                return week;
            }

            set
            {
                week = value;
            }
        }
        public override string ToString()
        {
            string str = string.Empty;
            for(int i=0;i<5;i++)
            {
                str += Environment.NewLine;
                str += DayOfWeek[i]+ "     *******************";
                for(int j=0;j< week[i].Day.Count; j++)
                {
                    str += Environment.NewLine;
                    str += week[i].Day[j].Subject.Name;
                }
                str += Environment.NewLine;
                str += "*************************************************";
            }   str += Environment.NewLine;
            return str;
        }
    }

    abstract class ScheduleBuilder
    {
        public abstract void BuildSchedule();
        public abstract void ResetSchedule();
    }
    class UniversitySchedule : ScheduleBuilder
    {
        public UniversitySchedule(GroupContext destination)
        {
            GroupСontext = destination;
            Schedule = new Schedule();
            ResetSchedule();
        }
        private Schedule Schedule;
        public GroupContext GroupСontext;
        public override void ResetSchedule()
        {
            for (int i = 0; i < 5; i++)
                Schedule.Week.Add(
                    new ScheduleDay()
                    {
                        Day = new List<ScheduleDayItem>()
                    });
        }
        public override void BuildSchedule()
        {
            int CurrentSubjectIndex = 0;
            int CurrentScheduleDay = 0;

            while (CurrentSubjectIndex != GroupСontext.Subjects.Count)
            {
                while (GroupСontext.Subjects[CurrentSubjectIndex].AlreadyUsed != GroupСontext.Subjects[CurrentSubjectIndex].CouplePerPeriod)
                {
                    if(CurrentScheduleDay<5)
                    if (Schedule.Week[CurrentScheduleDay].CountOfSubjectByName(
                        GroupСontext.Subjects[CurrentSubjectIndex].Name) < 2 // Не больше 2-х одинак.предметов в день,
                        && Schedule.Week[CurrentScheduleDay].Day.Count <= 3) // и количество пар не больше 4-х в день
                        {
                        Schedule.Week[CurrentScheduleDay].Day.Add(
                            new ScheduleDayItem() { Subject = GroupСontext.Subjects[CurrentSubjectIndex] });
                        GroupСontext.Subjects[CurrentSubjectIndex].AlreadyUsed++;
                    }
                    else
                        CurrentScheduleDay++;
                }
                CurrentSubjectIndex++;
                CurrentScheduleDay = 0;
            }
            GroupСontext.Schedule = Schedule;
        }
        public void Print(string Path)
        {
            using (var Writer = new System.IO.StreamWriter(Path))
            {
                Writer.WriteLine(GroupСontext.Schedule.ToString());
            }
        }
    }

    class TestEntry
    {
        public List<Subject> testSubjects = new List<Subject>()
        {
            new Subject() {Name="Укр Мова ПС", CourseNumber=1, Hours=144 },
            new Subject() {Name="Вища Математика", CourseNumber=1, Hours=108 },
            new Subject() {Name="Основи Алгоритмізації", CourseNumber=1, Hours=108 },
            new Subject() {Name="Архітектура Комп'ютера", CourseNumber=1, Hours=72 },
            new Subject() {Name="Безпека Життєдіяльності", CourseNumber=1, Hours=36 },
            new Subject() {Name="Фізичне Виховання", CourseNumber=1, Hours=72 }
        };
        public List<Audience> testAudience = new List<Audience>()
        {
            new Audience() { Number = "40" },
            new Audience() { Number = "38" },
            new Audience() { Number = "28" },
            new Audience() { Number = "219" },
            new Audience() { Number = "410" },
            new Audience() { Number = "21" },
        };
        public List<Lecturer> testLecturers = new List<Lecturer>()
        {
            new Lecturer() { Name ="Наталя", Surname="Полякова", LastName="Петрівна", Obligation=LecturerType.Lecture},
            new Lecturer() { Name ="Ірина", Surname="Скрипник", LastName="Анатоліївна", Obligation=LecturerType.Lecture},
            new Lecturer() { Name ="Володимер", Surname="Кривуляк", LastName="Васильович", Obligation=LecturerType.Lecture},
            new Lecturer() { Name ="Юрій", Surname="Пишнограєв", LastName="Миколайович", Obligation=LecturerType.Lecture},
            new Lecturer() { Name ="Юрій", Surname="Пазюк", LastName="Михайлович", Obligation=LecturerType.Lecture},
            new Lecturer() { Name ="Евгеній", Surname="Заєць", LastName="Миколайович", Obligation=LecturerType.Lecture}
        };
    }
}
