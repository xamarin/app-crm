using System;
using System.Collections;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using XamarinCRMv2CatalogDataService.DataObjects;

namespace XamarinCRMv2CatalogDataService.Controllers
{
    /// <summary>
    /// Category API endpoints
    /// </summary>
    [RoutePrefix("api/Categories")]
    public class CategoryController : BaseController<Category>
    {
        // GET api/Categories/SubCategories/48D68C86-6EA6-4C25-AA33-223FC9A27959

        /// <summary>
        /// Gets a collection of [sub]categories of a given parent category.
        /// </summary>
        /// <param name="parentCategoryId">The id of the parent category to get subcategories from. If parentCategoryId is null, the top-level categories will be returned.</param>
        /// <returns>A collection of [sub]categories.</returns>
        /// <remarks>If parentCategoryId is null, then returns subcategories of the root category. 
        /// These subcategories of the root are considered to "top-level" categories. 
        /// The root category only serves as the base of the hierarchy, and is not intended to be returned by this service.</remarks>
        [Route("SubCategories")]
        public async Task<IEnumerable> GetSubCategories(string parentCategoryId = null)
        {
            // since parentCategory is null, we're assuming that the root category level is being requested

            if (String.IsNullOrWhiteSpace(parentCategoryId))
            {
                var rootCategory = await
                    Query()
                    .SingleOrDefaultAsync(x => x.ParentCategoryId == null);

                return rootCategory == null ? null : rootCategory.SubCategories.OrderBy(x => x.Sequence);
            }

            // since parentCategory is not null, we're assuming that a specific category level is being requested

            var category = await
                    Query()
                    .SingleOrDefaultAsync(x => x.Id == parentCategoryId);

            return category == null ? null : category.SubCategories.OrderBy(x => x.Sequence);
        }

        
        // GET api/Categories/48D68C86-6EA6-4C25-AA33-223FC9A27959

        /// <summary>
        /// Gets a specific category by id.
        /// </summary>
        /// <param name="id">The id of the category to retrieve.</param>
        /// <returns>A category.</returns>
        [Route("")]
        public async Task<Category> GetCategory(string id)
        {
            return await
                Query()
                .SingleOrDefaultAsync(x => x.Id == id);
        }
    }
}