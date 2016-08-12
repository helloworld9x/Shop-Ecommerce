using System.Collections.Generic;
using System.Linq;
using Nop.Core;
using Nop.Core.Domain.Catalog;
using Nop.Core.Domain.Customers;
using Nop.Core.Domain.Orders;
using Nop.Services.Localization;

namespace Nop.Services.Orders
{
    /// <summary>
    /// Represents a shopping cart
    /// </summary>
    public static class ShoppingCartExtensions
    {
        /// <summary>
        /// Indicates whether the shopping cart requires shipping
        /// </summary>
        /// <param name="shoppingCart">Shopping cart</param>
        /// <returns>True if the shopping cart requires shipping; otherwise, false.</returns>
        public static bool RequiresShipping(this IList<ShoppingCartItem> shoppingCart)
        {
            foreach (var shoppingCartItem in shoppingCart)
                if (shoppingCartItem.IsShipEnabled)
                    return true;
            return false;
        }

        /// <summary>
        /// Gets a number of product in the cart
        /// </summary>
        /// <param name="shoppingCart">Shopping cart</param>
        /// <returns>Result</returns>
        public static int GetTotalProducts(this IList<ShoppingCartItem> shoppingCart)
        {
            int result = 0;
            foreach (ShoppingCartItem sci in shoppingCart)
            {
                result += sci.Quantity;
            }
            return result;
        }

        /// <summary>
        /// Get customer of shopping cart
        /// </summary>
        /// <param name="shoppingCart">Shopping cart</param>
        /// <returns>Customer of shopping cart</returns>
        public static Customer GetCustomer(this IList<ShoppingCartItem> shoppingCart)
        {
            if (shoppingCart.Count == 0)
                return null;

            return shoppingCart[0].Customer;
        }

        public static IEnumerable<ShoppingCartItem> LimitPerStore(this IEnumerable<ShoppingCartItem> cart, int storeId)
        {
            //simply replace the following code with "return cart"
            //if you want to share shopping carts between stores

            return cart.Where(x => x.StoreId == storeId);
        }
    }
}
