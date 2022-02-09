using UnityEngine.Purchasing;

namespace Model.Shop
{
    public class ShopProduct
    {
        public string Id;
        public ProductType CurrentProductType;

        public ShopProduct(string ID, ProductType productType)
        {
            Id = ID;
            CurrentProductType = productType;
        }
    }
}