using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Shopping_Product_API.Data.ValueObjects;
using Shopping_Product_API.Model;
using Shopping_Product_API.Model.Context;

namespace Shopping_Product_API.Repository
{
    public class ProductRepository : IProductRepository
    {

        private readonly IMapper _mapper;
        private readonly BaseContext _context;

        public ProductRepository(BaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProductVO>> GetAll()
        {
            List<Product> products = await _context.Products.ToListAsync();
            return _mapper.Map<List<ProductVO>>(products);
        }

        public async Task<ProductVO> GetById(long id)
        {
            Product product =
                await _context.Products.Where(p => p.Id == id)
                .FirstOrDefaultAsync();
            return _mapper.Map<ProductVO>(product);
        }

        public async Task<ProductVO> Create(ProductVO productVO)
        {
            Product product = _mapper.Map<Product>(productVO);
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return _mapper.Map<ProductVO>(product);
        }

        public async Task<ProductVO> Update(ProductVO productVO)
        {
            Product product = _mapper.Map<Product>(productVO);
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
            return _mapper.Map<ProductVO>(product);
        }

        public async Task<bool> Delete(long id)
        {
            try
            {
                Product product =
                await _context.Products.Where(p => p.Id == id)
                    .FirstOrDefaultAsync();

                if (product == null) return false;
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
