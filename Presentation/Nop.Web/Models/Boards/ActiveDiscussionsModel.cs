using System.Collections.Generic;

namespace Nop.Web.Models.Boards
{
    public class ActiveDiscussionsModel
    {

        public bool ViewAllLinkEnabled { get; set; }

        public bool ActiveDiscussionsFeedEnabled { get; set; }

        public int TopicPageSize { get; set; }
        public int TopicTotalRecords { get; set; }
        public int TopicPageIndex { get; set; }

        public int PostsPageSize { get; set; }
    }
}