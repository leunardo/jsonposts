using JsonPosts.Models;
using JsonPosts.Services;
using System.Windows;
using System.Windows.Controls;

namespace JsonPosts.Views
{
    /// <summary>
    /// Interaction logic for PostItemControl.xaml
    /// </summary>
    public partial class PostItemControl : UserControl
    {

        public PostItemControl()
        {
            InitializeComponent();
        }

        private void PostItem_Click(object sender, RoutedEventArgs e)
        {
            PostService.SelectedIdType = PostService.SelectedIdType == IdType.PostId 
                ? IdType.UserId 
                : IdType.PostId;
        }
    }
}
