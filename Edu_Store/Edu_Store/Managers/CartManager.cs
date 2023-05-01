using DataAccessLayer.Repository;
using Edu_Store.Models;

namespace Edu_Store.Managers
{
    public class CartManager
    {
        public CartManager(ApplicationDbContext context)
        {
            baseRepo = new BaseRepo<Cart>(context);
            Context = context;
        }
        public BaseRepo<Cart> baseRepo { get; set; }
        public ApplicationDbContext Context { get; }

        public List<Cart> GetAll()
        {
            return baseRepo.GetAll().ToList();
        }
        public void Add(Cart cart)
        {
            baseRepo.Add(cart);
        }
        public void DeleteCart(int id)
        {
            baseRepo.Delete(id);
        }
    }
}
