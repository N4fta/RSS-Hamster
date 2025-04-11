using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public enum OrderByReviews
    {
        ID,
        Title,
        FeedID,
        UserID
    }
    public enum OrderByFeeds
    {
        ID,
        ID_DESC,
        Name,
        Popularity
    }
    public enum OrderByCategories
    {
        ID,
        Category
    }
    public enum OrderByRoles
    {
        ID,
        Role
    }
    public enum OrderByUsers
    {
        ID,
        Name,
        Username,
        Email,
        RoleID
    }
}
