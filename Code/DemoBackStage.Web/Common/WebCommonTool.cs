using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using DemoBackStage.Entity;

using DemoBackStage.Web.ViewData;

namespace DemoBackStage.Web.Common
{
    public static class WebCommonTool
    {
        public static MenuVD MenuEntity2MenuVD(MenuEntity entity)
        {
            return new MenuVD
            {
                Id = entity.Id,
                IsDir = entity.isdir > 0,
                Level = entity.Level,
                Name = entity.Name,
                Rank = entity.Rank,
                Url = entity.Url,
                ParentId = entity.ParentId,
                Remark = entity.Remark
            };
        }
    }
}