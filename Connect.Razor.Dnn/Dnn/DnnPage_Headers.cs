﻿using System.Web.UI;
using System.Web.UI.HtmlControls;
using Connect.Razor.Blade;
using Connect.Razor.Internals;

namespace Connect.Razor.Dnn
{
    public partial class DnnHtmlPage 
    {
        public void AddToHead(string tag)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(tag)) return;
                var control = new LiteralControl(tag);
                Page?.Header.Controls.Add(control);
            }
            catch {  /* ignore */ }
        }

        public void AddMeta(string name, string content)
        {
            //AddToHead(Tags.Tag(AddMeta(, attributel)));
            AddToHead($"<meta name=\'{name}\' content=\'{Tags.Encode(content)}\' /> ");
        }

        public void AddOpenGraph(string property, string content)
            => AddToHead($"<meta property=\'{property}\' content=\'{Tags.Encode(content)}\' /> ");

        public void AddJsonLd(string jsonString)
            => AddToHead($"<script type=\"application/ld+json\">{jsonString}</script>");

        public void AddJsonLd(object jsonObject)
        {
            var str = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(jsonObject);
            AddJsonLd(str);
        }

        private void EnsureFieldVisibleAndSetValueAgain(string id, string value)
        {
            if (!(Page?.FindControl(id) is HtmlMeta metaTag)) return;
            metaTag.Visible = true;
            // todo: 2rm check why we are doing this - feels like we're setting things 2x
            metaTag.Content = Html.Encode(value);
        }



    }
}