using Microsoft.EntityFrameworkCore;
using Q1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Q1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        PE_PRN_24SumB1Context _context = new PE_PRN_24SumB1Context();
        public MainWindow()
        {
            InitializeComponent();
            var courses = _context.Courses.ToList();
            cbCourse.Items.Clear();
            foreach (var course in courses)
            {
                cbCourse.Items.Add(course.Title);
            }
            cbCourse.SelectedIndex = 0;
            lvUsers.ItemsSource = _context.Reviews
                    .Include(r => r.User)
                    .Include(r => r.Course)
                    .Where(r => r.Course.Title == cbCourse.SelectedItem.ToString())
                    .Select(r => new reviewDTO
                    {
                        id = r.User.UserId,
                        name = r.User.Username,
                        email = r.User.Email,
                        rating = r.Rating??0,
                        rvt = r.ReviewText,
                        rvd = r.ReviewDate??new DateTime(),
                    })
                    .ToList(); 
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            //reviewDTO reviewDTO = (reviewDTO)lvUsers.SelectedItem;
            //Review a = _context.Reviews.FirstOrDefault(r => r.UserId.ToString().Equals(reviewDTO.id.ToString()) && r.ReviewText.ToString().Equals(reviewDTO.rvt.ToString()) && r.Rating.ToString().Equals(reviewDTO.rating.ToString()));
            //_context.Reviews.Remove(a);
            //_context.SaveChanges();
            //txtUserId.Text = "";
            //txtUserName.Text = "";
            //txtEmail.Text = "";
            //txtRating.Text = "";
            //txtReviewText.Text = "";
            //dtpReviewDate.Text = "";
            //lvUsers.ItemsSource = _context.Reviews
            //        .Include(r => r.User)
            //        .Include(r => r.Course)
            //        .Where(r => r.Course.Title == cbCourse.SelectedItem.ToString())
            //        .Select(r => new
            //        {
            //            id = r.User.UserId,
            //            name = r.User.Username,
            //            email = r.User.Email,
            //            rating = r.Rating,
            //            rvt = r.ReviewText,
            //            rvd = r.ReviewDate
            //        })
            //        .ToList();
        }
        private void lvEmps_SelectionChanged(object sender, RoutedEventArgs e)
        {
            var a = new DateTime();
            reviewDTO reviewDTO = (reviewDTO)lvUsers.SelectedItem;
            if (reviewDTO!= null)
            {
                txtUserId.Text = reviewDTO.id.ToString();
                txtUserName.Text = reviewDTO.name.ToString();
                txtEmail.Text = reviewDTO.email.ToString();
                txtRating.Text = reviewDTO.rating.ToString();
                txtReviewText.Text = reviewDTO.rvt.ToString();
                if (reviewDTO.rvd != null)
                {
                    dtpReviewDate.Text = reviewDTO.rvd.ToString();
                }
                else
                {
                    dtpReviewDate.Text = a.ToString();
                }
                
            }
            
        }

        private void cbCourse_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            lvUsers.ItemsSource = _context.Reviews
                    .Include(r => r.User)
                    .Include(r => r.Course)
                    .Where(r => r.Course.Title == cbCourse.SelectedItem.ToString())
                    .Select(r => new
                    {
                        id = r.User.UserId,
                        name = r.User.Username,
                        email = r.User.Email,
                        rating = r.Rating,
                        rvt = r.ReviewText,
                        rvd = r.ReviewDate
                    })
                    .ToList();
        }
    }
}
