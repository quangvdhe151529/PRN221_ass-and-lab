using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace AutomobileWBFApppp
{
    public class PriceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is decimal price)
            {
                return $"${price.ToString("#,##0.###", CultureInfo.InvariantCulture)}";
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string priceString)
            {
                // Xử lý chuỗi giá trị người dùng nhập vào để loại bỏ ký tự "$" và dấu phân tách hàng nghìn
                string cleanedPriceString = priceString.Replace("$", "").Replace(",", "");

                // Chuyển đổi chuỗi đã được làm sạch thành giá trị số
                if (decimal.TryParse(cleanedPriceString, out decimal result))
                {
                    return result;
                }
            }

            return value;
        }
    }
}
