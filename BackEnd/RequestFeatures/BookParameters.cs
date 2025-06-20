namespace BackEnd.RequestFeatures
{
    public class BookParameters:RequestParameters
    {

        public uint MinPrice { get; set; }
        public uint MaxPrice { get; set; } = 1000;
        public bool ValidPriceRange => MaxPrice > MinPrice;

        public string? SearchTerm { get; set; }
        public string? OrderBy { get; set; }


        public BookParameters()
        {
            OrderBy = "id";
        }
      
        
    }
}