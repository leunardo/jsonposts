using JsonPosts.Models;
using JsonPosts.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace JsonPosts.ViewModels
{
    public class PostsViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Post> _posts = [];
        private IdType _selectedIdType = PostService.SelectedIdType;

        public event PropertyChangedEventHandler? PropertyChanged;

        public ObservableCollection<Post> Posts
        {
            get => _posts;
            set
            {
                _posts = value;
                OnPropertyChanged(nameof(Posts));
            }
        }

        public IdType SelectedIdType 
        { 
            get => _selectedIdType;
            set
            {
                _selectedIdType = value;
                OnPropertyChanged(nameof(SelectedIdType));
            }
        }

        public PostsViewModel()
        {
            FetchData();
            PostService.OnSelectedIdTypeChanged += OnSelectedIdTypeChanged;
        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void OnSelectedIdTypeChanged(object? sender, IdType e) => SelectedIdType = e;

        private async void FetchData()
        {
            var posts = await PostService.GetPostsAsync();
            foreach (var post in posts!.Take(100))
            {
                Posts.Add(post);
            }
        }
    }
}
