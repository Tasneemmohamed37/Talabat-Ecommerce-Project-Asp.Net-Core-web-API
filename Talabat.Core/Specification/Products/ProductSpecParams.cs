namespace Talabat.Core.Specification.Products
{
    public class ProductSpecParams
    {
        private const int MaxPageSize = 10;

        private int _PageSize = 6 ;

        public int PageSize
        {
            get { return _PageSize; }
            set { _PageSize = value > MaxPageSize ? MaxPageSize : value; }
        }

        public int PageIndex { get; set; } =  1;

        public string? sort { get; set; }

        public int? BrandId { get; set; }
        public int? TypeId { get; set; }
    }
}
