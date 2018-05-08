using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PagedList;
using PagedList.Mvc;

namespace JustBlog
{
    public class CustomPagedListRenderOptions : PagedListRenderOptions
    {
        public CustomPagedListRenderOptions()
        {
            DisplayLinkToFirstPage = PagedListDisplayMode.Never;
            DisplayLinkToLastPage = PagedListDisplayMode.Never;
            DisplayLinkToPreviousPage = PagedListDisplayMode.Always;
            DisplayLinkToNextPage = PagedListDisplayMode.Always;
            DisplayLinkToIndividualPages = true;
            DisplayPageCountAndCurrentLocation = false;
            MaximumPageNumbersToDisplay = 10;
            DisplayEllipsesWhenNotShowingAllPageNumbers = true;
            EllipsesFormat = "&#8230;";
            LinkToFirstPageFormat = "««";
            LinkToPreviousPageFormat = "Назад";
            LinkToIndividualPageFormat = "{0}";
            LinkToNextPageFormat = "Вперед";
            LinkToLastPageFormat = "»»";
            PageCountAndCurrentLocationFormat = "Page {0} of {1}.";
            ItemSliceAndTotalFormat = "Showing items {0} through {1} of {2}.";
            FunctionToDisplayEachPageNumber = null;
            ClassToApplyToFirstListItemInPager = null;
            ClassToApplyToLastListItemInPager = null;
            ContainerDivClasses = new[] { "pagination-container" };
            UlElementClasses = new[] { "pagination" };
            LiElementClasses = new[] { "page-numbers" };
            FunctionToTransformEachPageLink = (liTag, aTag) => {
                
                if(aTag.Attributes.ContainsKey("rel") && !aTag.Attributes.ContainsKey("href"))
                {
                    aTag.AddCssClass("inactive");
                }
                else if (!aTag.Attributes.ContainsKey("rel") && !aTag.Attributes.ContainsKey("href") && aTag.InnerHtml != "&#8230;")
                {
                    aTag.AddCssClass("current");
                }
                    
                return aTag;
            };
        }
    }
}