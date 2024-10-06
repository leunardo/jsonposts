using JsonPosts.Models;
using System.Globalization;
using System.Windows.Data;

namespace JsonPosts.Converters
{
    public class PostIdConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Length == 3 &&
                 values[0] is int postId &&
                 values[1] is int userId &&
                 values[2] is IdType selectedIdType)
            {
                return selectedIdType switch
                {
                    IdType.PostId => postId.ToString(),
                    IdType.UserId => userId.ToString(),
                    _ => throw new ArgumentOutOfRangeException("values[2]", "Invalid IdType provided")
                };
            }

            throw new ArgumentException("Unable to parse the provided values");
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
