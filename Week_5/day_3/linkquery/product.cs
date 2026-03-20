using System.Collections.Generic;

namespace LinqCodeTemplate
{
    internal class Product
    {
        public int ProCode { get; set; }
        public string ? ProName { get; set; }
        public string ? ProCategory { get; set; }
        public double ProMrp { get; set; }

        public List<Product> GetProducts()
        {
            return new List<Product>
            {
                new Product{ProCode=1001,ProName="Colgate-100gm",ProCategory="FMCG",ProMrp=55 },
                new Product{ProCode=1002,ProName="Colgate-50gm",ProCategory="FMCG",ProMrp=30 },
                new Product{ProCode=1010,ProName="Daawat-Basmati",ProCategory="Grain",ProMrp=130 }
            };
        }
    }
}
