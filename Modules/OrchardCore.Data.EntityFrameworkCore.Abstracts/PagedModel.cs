using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using OrchardCore.Navigation;

namespace OrchardCore.Data
{
    public abstract class PagedModelAbstract<T>
    {
        public int TotalItemCount { get; set; }
        public T Items { get; set; }

        public Pager Pager { get; set; }

    }

    public class PagedModel<T> : PagedModelAbstract<IReadOnlyList<T>> { }

    public class PagedModel : PagedModelAbstract<dynamic>
    {
    }
}
