using projeto_final.Models;
using Microsoft.EntityFrameworkCore;


namespace projeto_final.Repository;
public class TryBlogRepository : ITryBlogRepository {
        private readonly ITryBlogContext _context;
        public TryBlogRepository(ITryBlogContext context)
        {
            _context = context;
        }

        public IEnumerable<User> GetAllUsers(){
            return _context.Users.ToList();
        }

        public User GetUserByEmail(string email){
            return _context.Users.FirstOrDefault(u => u.Email == email);
        }

        public User GetUserById(Guid id){
            return _context.Users.FirstOrDefault(u => u.UserId == id);
        }
        public void CreateUser(User user){
            _context.Users.Add(user);
            _context.SaveChanges();
        }
        public void UpdateUser(User user){
            _context.Users.Update(user);
            _context.SaveChanges();
        }
        public void DeleteUser(User user){
            _context.Users.Remove(user);
            _context.SaveChanges();
        }

        public IEnumerable<Post> GetPostsByUser(Guid userId){
            return _context.Posts.Where(p => p.UserId == userId).ToList();
        }
        public Post GetPost(Guid postId){
            return _context.Posts.FirstOrDefault(p => p.PostId == postId);
        }
        public void CreatePost(Post post){
            _context.Posts.Add(post);
            _context.SaveChanges();
            }
        public void UpdatePost(Guid postId, Post post){
            _context.Posts.Update(post);
            _context.SaveChanges();
        }
        public void DeletePost(Guid postId){
            var post = _context.Posts.FirstOrDefault(p => p.PostId == postId);
            _context.Posts.Remove(post);
            _context.SaveChanges();
        }
}
