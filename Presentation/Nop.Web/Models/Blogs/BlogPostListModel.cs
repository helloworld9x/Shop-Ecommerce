using System.Collections.Generic;
using Nop.Web.Framework.Mvc;

namespace Nop.Web.Models.Blogs
{
    public class BlogPostListModel : BaseNopModel
    {
        public BlogPostListModel()
        {
            PagingFilteringContext = new BlogPagingFilteringModel();
        }

        public int WorkingLanguageId { get; set; }
        public BlogPagingFilteringModel PagingFilteringContext { get; set; }
    }
}