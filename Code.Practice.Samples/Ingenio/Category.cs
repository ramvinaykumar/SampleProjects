using System.Collections.Generic;
using System.Linq;

namespace Code.Practice.Samples.Ingenio
{
    public class Category
    {
        public int CategoryId { get; set; }
        public int ParentCategoryId { get; set; }
        public string Name { get; set; }
        public string Keywords { get; set; }

        public List<Category> CategoryDataSet()
        {
            var categories = new List<Category>()
            {
                new Category() { CategoryId = 100, ParentCategoryId = -1, Name = "Business", Keywords = "Money" },
                new Category() { CategoryId = 200, ParentCategoryId = -1, Name = "Tutoring", Keywords = "Teaching" },
                new Category() { CategoryId = 101, ParentCategoryId = 100, Name = "Accounting", Keywords = "Taxes" },
                new Category() { CategoryId = 102, ParentCategoryId = 100, Name = "Taxation" },
                new Category() { CategoryId = 201, ParentCategoryId = 200, Name = "Computer" },
                new Category() { CategoryId = 103, ParentCategoryId = 101, Name = "Corporate Tax" },
                new Category() { CategoryId = 202, ParentCategoryId = 201, Name = "Operating System" },
                new Category() { CategoryId = 109, ParentCategoryId = 101, Name = "Small Business Tax" }
            };
            return categories;
        }

        public string Solution(int catId)
        {
            string result = "", keyword = "";
            if (catId > 0)
            {
                var dataSet = CategoryDataSet();

                var data = dataSet.Where(x => x.CategoryId == catId).Select(s => new Category
                {
                    ParentCategoryId = s.ParentCategoryId,
                    Name = s.Name,
                    Keywords = s.Keywords
                }).FirstOrDefault();

                if (data != null && string.IsNullOrEmpty(data.Keywords))
                {
                    var parentCatId = dataSet.Where(x => x.CategoryId == catId).Select(p => p.ParentCategoryId).FirstOrDefault();
                    if (parentCatId > 0)
                    {
                        keyword = dataSet.Where(x => x.CategoryId == parentCatId).Select(s => s.Keywords).LastOrDefault();
                        if (string.IsNullOrEmpty(keyword))
                        {
                            parentCatId = dataSet.Where(x => x.CategoryId == parentCatId).Select(p => p.ParentCategoryId).FirstOrDefault();
                            if (parentCatId > 0)
                            {
                                keyword = dataSet.Where(x => x.CategoryId == parentCatId).Select(s => s.Keywords).FirstOrDefault();
                            }
                        }
                    }
                }

                if (!string.IsNullOrEmpty(keyword))
                {
                    result = "ParentCategoryId=" + data.ParentCategoryId + ", Name=" + data.Name + ", Keywords=" + keyword + "";
                }
                else
                {
                    result = "ParentCategoryId=" + data.ParentCategoryId + ", Name=" + data.Name + ", Keywords=" + data.Keywords + "";
                }
            }
            return result;
        }
    }
}
