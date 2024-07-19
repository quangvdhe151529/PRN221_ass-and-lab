using System;
using System.Collections.Generic;
using System.Globalization;
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
using AutomobileLibrary.DataAccess;
using AutomobileLibrary.Repository;

namespace AutomobileWBFApppp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class WindowCarManagement : Window
    {
        ICarRepository carRepository;
        public WindowCarManagement(ICarRepository repository)
        {
            InitializeComponent();
            carRepository = repository;
            lvCars.MouseDoubleClick += lvCars_MouseDoubleClick; // Gắn sự kiện MouseDoubleClick cho ListView
        }
        private Car GetCarObject()
        {
            String s = txtPrice.Text;
            if (s.StartsWith("$"))
            {
                s = s.Substring(1);
            }
            Car car = null;
            try
            {
                if (!int.TryParse(txtCarId.Text, out int carId) || carId <= 0)
                {
                    MessageBox.Show("CarID must be a positive integer.", "Error");
                    return null;
                }

                if (string.IsNullOrWhiteSpace(txtCarName.Text))
                {
                    MessageBox.Show("Car Name cannot be empty.", "Error");
                    return null;
                }

                if (string.IsNullOrWhiteSpace(txtManufacturer.Text))
                {
                    MessageBox.Show("Manufacturer cannot be empty.", "Error");
                    return null;
                }

                if (!decimal.TryParse(s, out decimal price) || price < 0)
                {
                    MessageBox.Show("Price must be a non-negative number.", "Error");
                    return null;
                }

                if (!int.TryParse(txtReleasedYear.Text, out int releasedYear) || releasedYear < 2000 || releasedYear > 2100)
                {
                    MessageBox.Show("Released Year must be between 2000 and 2100.", "Error");
                    return null;
                }
                
                car = new Car
                {
                    CarId = carId,
                    CarName = txtCarName.Text,
                    Manufacturer = txtManufacturer.Text,
                    Price = decimal.Parse(s),
                    ReleasedYear = releasedYear,
                    Age = DateTime.Now.Year - releasedYear
                };
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Get car");
            }
            return car;
        }

        public void LoadCarList()
        {
            // Lấy danh sách xe từ repository
            var cars = carRepository.GetCars();
            // Tính toán trường "Age" cho mỗi xe trong danh sách
            foreach (var car in cars)
            {
                car.Age = DateTime.Now.Year - car.ReleasedYear;
            }
            // Đặt danh sách đã tính toán vào ItemsSource của ListView
            lvCars.ItemsSource = cars;
        }
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                

                Car car = GetCarObject();
                if (car != null)
                {
                    carRepository.InsertCar(car);
                    LoadCarList();
                    MessageBox.Show($"{car.CarName} inserted successfully", "Insert car");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Insert car");
            }
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                Car car = GetCarObject();
                if (car != null)
                {
                    carRepository.UpdateCar(car);
                    LoadCarList();
                    MessageBox.Show($"{car.CarName} update successfully", "Update car");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Updatee car");
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            try
            {
                Car car = GetCarObject();
                if (car != null)
                {
                    carRepository.DeleteCar(car);
                    LoadCarList();
                    MessageBox.Show($"{car.CarName} deleted successfully", "Delete car");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Delete car");
            }
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            try
            {
                LoadCarList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Load car list");
            }

        }

        private void btnClose_Click(object sender, RoutedEventArgs e) => Close();
        
        // Xử lý sự kiện khi bấm đúp chuột vào ListView
        private void lvCars_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var selectedItem = lvCars.SelectedItem as Car;
            if (selectedItem != null)
            {
                var confirmation = MessageBox.Show($"Are you sure you want to delete {selectedItem.CarName}?", "Confirm Delete", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (confirmation == MessageBoxResult.Yes)
                {
                    try
                    {
                        carRepository.DeleteCar(selectedItem);
                        LoadCarList();
                        MessageBox.Show($"{selectedItem.CarName} deleted successfully", "Delete car");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Delete car");
                    }
                }
            }
        }
    }
}
