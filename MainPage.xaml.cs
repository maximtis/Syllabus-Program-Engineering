using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Syllabus_Program_Engineering
{
    /// <summary>
    /// Interaction logic for MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();
        }
        ThicknessAnimation ThickAnim = new ThicknessAnimation();


        private void CreateNewButton_Click(object sender, RoutedEventArgs e)
        {
            ThickAnim.Duration = TimeSpan.FromSeconds(1);
            ThickAnim.To = new Thickness(-380, OpenButton.Margin.Top, OpenButton.Margin.Right, OpenButton.Margin.Bottom);
            OpenButton.BeginAnimation(MarginProperty, ThickAnim);
            ThickAnim.To = new Thickness(-380, RecentButton.Margin.Top, RecentButton.Margin.Right, RecentButton.Margin.Bottom);
            RecentButton.BeginAnimation(MarginProperty, ThickAnim);
            CreateNewButton.IsEnabled = false;
            ThickAnim.To = new Thickness(66, CreatingProjectMenu.Margin.Top, CreatingProjectMenu.Margin.Right, CreatingProjectMenu.Margin.Bottom);
            CreatingProjectMenu.BeginAnimation(MarginProperty, ThickAnim);
            
        }

        private void SuccessButton_Click(object sender, RoutedEventArgs e)
        {
            ThickAnim.To = new Thickness(-380, CreatingProjectMenu.Margin.Top, CreatingProjectMenu.Margin.Right, CreatingProjectMenu.Margin.Bottom);
            CreatingProjectMenu.BeginAnimation(MarginProperty, ThickAnim);
            ThickAnim.To = new Thickness(-380, CreateNewButton.Margin.Top, CreateNewButton.Margin.Right, CreateNewButton.Margin.Bottom);
            CreateNewButton.BeginAnimation(MarginProperty, ThickAnim);
            ThickAnim.To = new Thickness(66, ProjectStatistic.Margin.Top, ProjectStatistic.Margin.Right, ProjectStatistic.Margin.Bottom);
            ProjectStatistic.BeginAnimation(MarginProperty, ThickAnim);
            ImportExportButton.IsEnabled = true;
            EnteringButton.IsEnabled = true;
            BuildingButton.IsEnabled = true;
        }

        private void OpenButton_Click(object sender, RoutedEventArgs e)
        {
            TestEntry te = new TestEntry();
            GroupContext GroupContextTest = new GroupContext()
            {
                Audiences = te.testAudience,
                Lecturers = te.testLecturers,
                Subjects = te.testSubjects
            };
            UniversitySchedule UniversScheduleTest = new UniversitySchedule(GroupContextTest);
            UniversScheduleTest.BuildSchedule();
            UniversScheduleTest.Print("Print.txt");
        }
    }
}
