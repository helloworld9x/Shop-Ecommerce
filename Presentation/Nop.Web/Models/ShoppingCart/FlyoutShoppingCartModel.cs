using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Nop.Web.Framework.Mvc;

namespace Nop.Web.Models.ShoppingCart
{
    public class FlyoutShoppingCartModel : BaseNopModel
    {
        public bool IsAuthenticated { get; set; }

        public bool ShoppingCartEnabled { get; set; }

        public int ShoppingCartItems { get; set; }

        public bool WishlistEnabled { get; set; }

        public int WishlistItems { get; set; }
    }
}