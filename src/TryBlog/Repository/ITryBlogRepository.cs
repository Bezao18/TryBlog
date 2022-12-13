using projeto_final.Models;

namespace projeto_final.Repository
{
    public interface ITryBlogRepository
    {
        IEnumerable<User> GetAllUsers();
        User GetUserByEmail(string email);
        User GetUserById(Guid userId);
        void CreateUser(User user);
        void UpdateUser(User user);
        void DeleteUser(User user);

        IEnumerable<Post> GetPostsByUser(Guid userId);
        Post GetPost(Guid postId);
        void CreatePost(Post post);
        void UpdatePost(Guid postId, Post post);
        void DeletePost(Guid postId);
    }
}