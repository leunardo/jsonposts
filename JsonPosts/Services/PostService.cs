
using JsonPosts.Models;

namespace JsonPosts.Services
{
    internal static class PostService
    {
        private static IdType _idType = IdType.PostId;

        public static event EventHandler<IdType>? OnSelectedIdTypeChanged;

        public static IdType SelectedIdType
        {
            get => _idType;
            set
            {
                _idType = value;
                OnSelectedIdTypeChanged?.Invoke(null, value);
            }
        }

        private static readonly JsonPlaceholderService _placeholderService = new();
        public static Task<IEnumerable<Post>?> GetPostsAsync() => _placeholderService.GetPostsAsync();
    }
}