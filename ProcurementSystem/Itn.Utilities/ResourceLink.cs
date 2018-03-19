namespace Itn.Utilities
{
    public class ResourceLink
    {
        public string rel { get; set; } // relationship that link represents. (e.g. self)
        public string method { get; set; } // verb
        public string href { get; set; } // actual link
        public string title { get; set; } // title of this link that can be used by client to describe the link (e.g. Get Item)

        public static ResourceLink Create(string _rel, string _method, string _href, string _title = "")
        {
            return new ResourceLink { rel = _rel, method = _method, href = _href, title = _title };
        }
    }
}
