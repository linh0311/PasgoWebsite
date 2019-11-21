using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;


namespace WebApplication2.CustomHtmlHelper
{
    public static class CustomHtmlHelpers
    {
        public static string SlideImage(this HtmlHelper helper, string href, string src, string alt)
        {
            //return new MvcHtmlString(tag.ToString(TagRenderMode.SelfClosing));
            return String.Format("<div class='carousel-item'><a href='{0}'><img src='{1}' alt='{2}'></a></div>", href, src, alt);
        }

        public static IHtmlString SlideShow(this HtmlHelper helper,int id , string href, string src, string alt, object htmlAttributes)
        {
            var builderdiv = new TagBuilder("div");
            if (id == 1)
                builderdiv.MergeAttribute("class", "carousel-item active");
            else
                builderdiv.MergeAttribute("class", "carousel-item");

            var buildera = new TagBuilder("a");
            buildera.MergeAttribute("href", href);
            buildera.MergeAttributes(new RouteValueDictionary(htmlAttributes));


            var builderimg = new TagBuilder("img");
            builderimg.MergeAttribute("src", src);
            builderimg.MergeAttribute("alt", alt);
            builderimg.MergeAttributes(new RouteValueDictionary(htmlAttributes));

            StringBuilder htmlBuilder = new StringBuilder();
            htmlBuilder.Append(builderdiv.ToString(TagRenderMode.StartTag));
            htmlBuilder.Append(buildera.ToString(TagRenderMode.StartTag));
            htmlBuilder.Append(builderimg.ToString(TagRenderMode.Normal));
            htmlBuilder.Append(buildera.ToString(TagRenderMode.EndTag));
            htmlBuilder.Append(builderdiv.ToString(TagRenderMode.EndTag));

            return MvcHtmlString.Create(htmlBuilder.ToString());

        }
        public static IHtmlString SlideShowIndicators( this HtmlHelper helper, int id, string href, string src, string alt, string promotion, object htmlAttributes)
        {
            var builderli = new TagBuilder("li");
            if (id == 1)
                builderli.MergeAttribute("class", "active");
            else
                builderli.MergeAttribute("class", "");

            var buildera = new TagBuilder("a");
            buildera.MergeAttribute("href", href);
            buildera.MergeAttributes(new RouteValueDictionary(htmlAttributes));

            var builderdiv = new TagBuilder("div");
            builderdiv.MergeAttribute("class", "img");

            var builderimg = new TagBuilder("img");
            builderimg.MergeAttribute("src", src);
            builderimg.MergeAttribute("alt", alt);
            builderimg.MergeAttributes(new RouteValueDictionary(htmlAttributes));

            var builderp = new TagBuilder("p");
            builderp.SetInnerText(alt);

            var builderspan = new TagBuilder("span");
            builderspan.SetInnerText(promotion);

            StringBuilder htmlBuilder = new StringBuilder();
            htmlBuilder.Append(buildera.ToString(TagRenderMode.StartTag));
                htmlBuilder.Append(builderdiv.ToString(TagRenderMode.StartTag));
                    htmlBuilder.Append(builderimg.ToString(TagRenderMode.Normal));
                htmlBuilder.Append(builderdiv.ToString(TagRenderMode.EndTag));
                htmlBuilder.Append(builderp.ToString(TagRenderMode.Normal));
                htmlBuilder.Append(builderspan.ToString(TagRenderMode.Normal));
            htmlBuilder.Append(buildera.ToString(TagRenderMode.EndTag));
            builderli.InnerHtml = htmlBuilder.ToString();
            var html = builderli.ToString(TagRenderMode.Normal);
            return MvcHtmlString.Create(html);
        }
        public static IHtmlString MenuCatalogue(this HtmlHelper helper, string alt, int soluong, string src, string href)
        {
            var col = new TagBuilder("div");
            col.MergeAttribute("class", "col-md-3 col-6");

            var item = new TagBuilder("div");
            item.MergeAttribute("class", "item-collection");

            var a = new TagBuilder("a");
            a.MergeAttribute("href", href);

            var img = new TagBuilder("img");
            img.MergeAttribute("src", src);
            img.MergeAttribute("alt", alt);
            img.MergeAttribute("class", "img-responsive");

            var wapsale = new TagBuilder("div");
            wapsale.MergeAttribute("class", "wapsale");

            var span = new TagBuilder("span");
            span.SetInnerText(soluong.ToString());

            var wapfooter = new TagBuilder("div");
            wapfooter.MergeAttribute("class", "wapfooter");

            var p = new TagBuilder("p");
            p.MergeAttribute("class", "textname");
            p.SetInnerText(alt);

            StringBuilder htmlBuilder = new StringBuilder();
            htmlBuilder.Append(item.ToString(TagRenderMode.StartTag));
                htmlBuilder.Append(a.ToString(TagRenderMode.StartTag));
                    htmlBuilder.Append(img.ToString(TagRenderMode.Normal));
                    htmlBuilder.Append(wapsale.ToString(TagRenderMode.StartTag));
                        htmlBuilder.Append(span.ToString(TagRenderMode.Normal));
                        htmlBuilder.Append(wapsale.InnerHtml = "<aside>Nhà hàng</aside>");
                    htmlBuilder.Append(wapsale.ToString(TagRenderMode.EndTag));
                    htmlBuilder.Append(wapfooter.ToString(TagRenderMode.StartTag));
                        htmlBuilder.Append(p.ToString(TagRenderMode.Normal));
                    htmlBuilder.Append(wapfooter.ToString(TagRenderMode.EndTag));
                htmlBuilder.Append(a.ToString(TagRenderMode.EndTag));
            htmlBuilder.Append(item.ToString(TagRenderMode.EndTag));
            col.InnerHtml = htmlBuilder.ToString();
            var html = col.ToString(TagRenderMode.Normal);

            return MvcHtmlString.Create(html);
            
        }
        public static IHtmlString CarouselRating(this HtmlHelper helper, string name, string address, string href, int star, int dollar, string promotion, string feature, string src, int id )
        {
            var col = new TagBuilder("div");
            col.MergeAttribute("class", "col-md-3 col-6 float-left");

            var item = new TagBuilder("div");
            item.MergeAttribute("class", "wap-item");

            var a = new TagBuilder("a");
            a.MergeAttribute("href", href);

            var img = new TagBuilder("img");
            img.MergeAttribute("src", src);

            var row = new TagBuilder("div");
            row.MergeAttribute("class", "row");

            var wapfooter = new TagBuilder("div");
            wapfooter.MergeAttribute("class", "wapfooter");

            var wapsale = new TagBuilder("div");
            wapsale.MergeAttribute("class", "wap-sale");
            wapsale.SetInnerText(promotion);
            var waptag = new TagBuilder("div");
            waptag.MergeAttribute("class", "wap-tag");
            waptag.SetInnerText(feature);

            var wapbooking = new TagBuilder("div");
            wapbooking.MergeAttribute("class", "wap-booking");

            StringBuilder htmlBuilder = new StringBuilder();
            htmlBuilder.Append(a.ToString(TagRenderMode.StartTag));
                htmlBuilder.Append(img.ToString(TagRenderMode.Normal));
            htmlBuilder.Append(a.ToString(TagRenderMode.EndTag));
            htmlBuilder.Append(wapfooter.ToString(TagRenderMode.StartTag));
                htmlBuilder.Append(a.ToString(TagRenderMode.StartTag));
                    htmlBuilder.Append(a.InnerHtml = "<div><p style='margin-top: 10px;'>"+name+"</p></div>");
                htmlBuilder.Append(a.ToString(TagRenderMode.EndTag));
                htmlBuilder.Append(row.ToString(TagRenderMode.StartTag));
                htmlBuilder.Append(row.InnerHtml = "<div class='col-md-6 col-sm-6 col-6 wapstar'><div class='danhgia-ct shadow-outside'><div class='rating-star'><span class='rate'><span class='fa fa-star notchecked'></span><span class='fa fa-star notchecked'></span><span class='fa fa-star notchecked'></span><span class='fa fa-star notchecked'></span><span class='fa fa-star notchecked'></span> <span class='rate2' style='width: "
                                +star
                                + "%'><span class='fa fa-star checked'></span><span class='fa fa-star checked'></span><span class='fa fa-star checked'></span><span class='fa fa-star checked'></span><span class='fa fa-star checked'></span></span></span></div></div></div>");
                htmlBuilder.Append(row.InnerHtml = "<div class='col-md-6 col-sm-6 col-6 wapdollar'><div class='danhgia-ct shadow-outside'><div class='rating-container rating-md rating-animate'><div class='rating-money'><span class='rate'><span class='fa fa-usd notchecked' style='padding: 0 6px 0 4px;'></span><span class='fa fa-usd notchecked' style='padding: 0 6px 0 4px;'></span><span class='fa fa-usd notchecked' style='padding: 0 6px 0 4px;'></span><span class='fa fa-usd notchecked' style='padding: 0 6px 0 4px;'></span><span class='fa fa-usd notchecked' style='padding: 0 6px 0 4px;'></span> <span class='rate2' style='width: "
                                + dollar
                                + "%'><span class='fa fa-usd checked'style='padding: 0 6px 0 4px;'></span><span class='fa fa-usd checked'style='padding: 0 6px 0 4px;'></span><span class='fa fa-usd checked'style='padding: 0 6px 0 4px;'></span><span class='fa fa-usd checked'style='padding: 0 6px 0 4px;'></span><span class='fa fa-usd checked'style='padding: 0 6px 0 4px;'></span></span></span></div></div></div></div>");
                htmlBuilder.Append(row.ToString(TagRenderMode.EndTag));
            htmlBuilder.Append(wapfooter.InnerHtml = "<p class='text-address notranslate'>" + address+"</p>");
            htmlBuilder.Append(wapfooter.ToString(TagRenderMode.EndTag));
            htmlBuilder.Append(wapsale.ToString(TagRenderMode.Normal));
            htmlBuilder.Append(waptag.ToString(TagRenderMode.Normal));
            htmlBuilder.Append(wapbooking.ToString(TagRenderMode.StartTag));
                a.MergeAttribute("class", "btn-booking");
                htmlBuilder.Append(a.ToString(TagRenderMode.StartTag));
                htmlBuilder.Append(a.InnerHtml = "Đặt chỗ ngay");
                htmlBuilder.Append(a.ToString(TagRenderMode.EndTag));
            htmlBuilder.Append(wapbooking.ToString(TagRenderMode.EndTag));

            item.InnerHtml = htmlBuilder.ToString();
            col.InnerHtml = item.ToString(TagRenderMode.Normal);
            var html =col.ToString(TagRenderMode.Normal);

            return MvcHtmlString.Create(html);
        }
        public static IHtmlString KhamPha (this HtmlHelper helper, string name, string href, string src, string loc, string min, string max, string star, string promotion)
        {
            var a = new TagBuilder("a");
            a.MergeAttribute("href", href);

            var divimg = new TagBuilder("div");
            divimg.MergeAttribute("class", "col-md-12 col-sm-12 col-5 no-padding pasgo-khampha-item-img");
            divimg.MergeAttribute("style", "width: calc(100% - 10px); margin: 5px; overflow: hidden;");

            var divtitle = new TagBuilder("div");
            divtitle.MergeAttribute("class", "col-md-12 col-sm-12 col-7 no-padding pasgo-khampha-item-title");

            var divreview = new TagBuilder("div");
            divreview.MergeAttribute("class", "col-md-12 col-sm-12 col-12 no-padding pasgo-khampha-item-review");
            divreview.MergeAttribute("style", "overflow: auto;");

            var img = new TagBuilder("img");
            img.MergeAttribute("src", src);

            var p = new TagBuilder("p");
            p.SetInnerText(name);

            StringBuilder htmlBuilder = new StringBuilder();
            htmlBuilder.Append(a.ToString(TagRenderMode.StartTag));
                htmlBuilder.Append(divimg.ToString(TagRenderMode.StartTag));
                    htmlBuilder.Append(img.ToString(TagRenderMode.Normal));
                htmlBuilder.Append(divimg.ToString(TagRenderMode.EndTag));
                htmlBuilder.Append(divtitle.ToString(TagRenderMode.StartTag));
                    htmlBuilder.Append(p.ToString(TagRenderMode.Normal));
                htmlBuilder.Append(divtitle.ToString(TagRenderMode.EndTag));
            htmlBuilder.Append(a.ToString(TagRenderMode.EndTag));
                htmlBuilder.Append(divreview.ToString(TagRenderMode.StartTag));
                    htmlBuilder.Append(divreview.InnerHtml = "<div class='col-md-5 col-sm-5 col-5 float-left'><div class='pasgo-icondesktop-kp-pin float-left'></div><span class='pasgo-khampha-item-txt'>"
                                                            +loc+
                                                            "</span></div>");
                    htmlBuilder.Append(divreview.InnerHtml = "<div class='col-md-7 col-sm-7 col-7 float-left'><div class='pasgo-icondesktop-kp-usd float-left'></div><span class='pasgo-khampha-item-txt'>"
                                                            + min + "&nbsp;-&nbsp;" + max + "K/người"+
                                                            "</span></div>");
                    htmlBuilder.Append(divreview.InnerHtml = "<div class='col-md-5 col-sm-5 col-5 float-left'><div class='pasgo-icondesktop-kp-star float-left'></div><span class='pasgo-khampha-item-txt'>" 
                                                            +star+ 
                                                            "</span></div>");
                    htmlBuilder.Append(divreview.InnerHtml = "<div class='col-md-7 col-sm-7 col-7 float-left'><div class='pasgo-icondesktop-kp-sale float-left'></div><span class='pasgo-khampha-item-txt'>"
                                                            +promotion+
                                                            "</span></div>");
                htmlBuilder.Append(divreview.ToString(TagRenderMode.EndTag));

            a.InnerHtml = htmlBuilder.ToString();
            var html = htmlBuilder.ToString();

            return MvcHtmlString.Create(html);
        }
        public static IHtmlString ThuongHieu(this HtmlHelper helper, string name, string href, string src, string promotion)
        {
            var col = new TagBuilder("div");
            col.MergeAttribute("class", "col-md-3 col-6 float-left");

            var wap = new TagBuilder("div");
            wap.MergeAttribute("class", "wap-item");

            var a = new TagBuilder("a");
            a.MergeAttribute("href", href);

            var img = new TagBuilder("img");
            img.MergeAttribute("src", src);
            img.MergeAttribute("class", "img-responsive");

            var span = new TagBuilder("span");
            span.SetInnerText(promotion);

            var tag = new TagBuilder("div");
            tag.MergeAttribute("class", "wap-tag");
            tag.SetInnerText(name);

            var sale = new TagBuilder("div");
            sale.MergeAttribute("class", "wap-sale");

            var percent = new TagBuilder("div");
            percent.MergeAttribute("class", "pasgo-icondesktop-kp-sale");

            StringBuilder htmlBuilder = new StringBuilder();
            htmlBuilder.Append(a.ToString(TagRenderMode.StartTag));
                htmlBuilder.Append(img.ToString(TagRenderMode.Normal));
            htmlBuilder.Append(a.ToString(TagRenderMode.EndTag));
            htmlBuilder.Append(a.ToString(TagRenderMode.StartTag));
                htmlBuilder.Append(tag.ToString(TagRenderMode.Normal));
            htmlBuilder.Append(a.ToString(TagRenderMode.EndTag));
            htmlBuilder.Append(sale.ToString(TagRenderMode.StartTag));
                htmlBuilder.Append(percent.ToString(TagRenderMode.Normal));
                htmlBuilder.Append(span.ToString(TagRenderMode.Normal)); 
            htmlBuilder.Append(sale.ToString(TagRenderMode.EndTag));
            wap.InnerHtml = htmlBuilder.ToString();
            col.InnerHtml = wap.ToString(TagRenderMode.Normal);
            return MvcHtmlString.Create(col.ToString(TagRenderMode.Normal));
        }
        public static IHtmlString Blog1(this HtmlHelper helper, string title, string teaser, string href, string src, string date)
        {
            var a = new TagBuilder("a");
            a.MergeAttribute("href", href);

            var img = new TagBuilder("img");
            img.MergeAttribute("src", src);

            var divimg = new TagBuilder("div");
            divimg.MergeAttribute("class", "col-sm-12 col-5 no-padding");

            var divtitle = new TagBuilder("div");
            divtitle.MergeAttribute("class", "col-sm-12 col-7 no-padding");
            divtitle.InnerHtml= "<h2>"+title+"</h2>";

            var divteaser = new TagBuilder("div");
            divteaser.MergeAttribute("class", "col-sm-12 col-12 no-padding");
            divteaser.InnerHtml = "<div class='blog-teaser pc'>" + teaser +"</div>";

            var pdate = new TagBuilder("p");
            pdate.MergeAttribute("class", "blog-time");
            pdate.InnerHtml = "&nbsp;&nbsp;&nbsp;<i class='fa fa-clock-o' aria-hidden='true'></i>&nbsp;" + date;

            StringBuilder htmlBuilder = new StringBuilder();
            htmlBuilder.Append(divimg.ToString(TagRenderMode.StartTag));
                htmlBuilder.Append(img.ToString(TagRenderMode.Normal));
            htmlBuilder.Append(divimg.ToString(TagRenderMode.EndTag));
            htmlBuilder.Append(divtitle.ToString(TagRenderMode.Normal));
            htmlBuilder.Append(divteaser.ToString(TagRenderMode.Normal));
            htmlBuilder.Append(pdate.ToString(TagRenderMode.Normal));
            a.InnerHtml = htmlBuilder.ToString();

            return MvcHtmlString.Create(a.ToString(TagRenderMode.Normal));

        }
        public static IHtmlString Blog2(this HtmlHelper helper, string title, string href, string src, string date)
        {
            var a = new TagBuilder("a");
            a.MergeAttribute("href", href);

            var divimg = new TagBuilder("div");
            divimg.MergeAttribute("class", "col-5 col-sm-4 col-md-5 col-lg-4 no-padding");

            var img = new TagBuilder("img");
            img.MergeAttribute("src", src);
            img.MergeAttribute("alt", title);

            var div = new TagBuilder("div");
            div.MergeAttribute("class", "col-7 col-sm-8 col-md-7 col-lg-8 no-padding");
            div.InnerHtml = "<h4>" + title + "</h4>";

            var pdate = new TagBuilder("p");
            pdate.MergeAttribute("class", "blog-time");
            pdate.InnerHtml = "&nbsp;&nbsp;&nbsp;<i class='fa fa-clock-o' aria-hidden='true'></i>&nbsp;" + date;

            StringBuilder htmlBuilder = new StringBuilder();
            htmlBuilder.Append(divimg.ToString(TagRenderMode.StartTag));
                htmlBuilder.Append(img.ToString(TagRenderMode.Normal));
            htmlBuilder.Append(divimg.ToString(TagRenderMode.EndTag));
            htmlBuilder.Append(div.ToString(TagRenderMode.Normal));
            htmlBuilder.Append(pdate.ToString(TagRenderMode.Normal));
            a.InnerHtml = htmlBuilder.ToString();

            return MvcHtmlString.Create(a.ToString(TagRenderMode.Normal));
        }
        public static IHtmlString TinTuc1(this HtmlHelper helper, string title, string teaser, string href, string src, string date)
        {
            var a = new TagBuilder("a");
            a.MergeAttribute("href", href);

            var img = new TagBuilder("img");
            img.MergeAttribute("src", src);
            img.MergeAttribute("alt", title);

            StringBuilder htmlBuilder = new StringBuilder();
            htmlBuilder.Append(a.ToString(TagRenderMode.StartTag));
                htmlBuilder.Append(img.ToString(TagRenderMode.Normal));
                htmlBuilder.Append(a.InnerHtml = "<h2>" + title + "</h2>");
                htmlBuilder.Append(a.InnerHtml = "<div class='blog-teaser'>" + teaser + "</div>");
                htmlBuilder.Append(a.InnerHtml = "<span class='blog-time'>&nbsp;&nbsp;&nbsp;<i class='fa fa-clock-o' aria-hidden='true'></i>&nbsp;" + date + "</span>");
            htmlBuilder.Append(a.ToString(TagRenderMode.EndTag));

            return MvcHtmlString.Create(htmlBuilder.ToString());
        }
        public static IHtmlString TinTuc2(this HtmlHelper helper, string title, string teaser, string href, string src, string date)
        {
            var a = new TagBuilder("a");
            a.MergeAttribute("href", href);

            var divimg = new TagBuilder("div");
            divimg.MergeAttribute("class", "col-md-4 col-sm-4 col-5 no-padding");

            var img = new TagBuilder("img");
            img.MergeAttribute("src", src);
            img.MergeAttribute("alt", title);

            var divtitle = new TagBuilder("div");
            divtitle.MergeAttribute("class", "col-md-8 col-sm-8 col-7 no-padding");
            divtitle.InnerHtml = "<h2>" + title + "</h2>";

            StringBuilder htmlBuilder = new StringBuilder();
            htmlBuilder.Append(a.ToString(TagRenderMode.StartTag));
                htmlBuilder.Append(divimg.ToString(TagRenderMode.StartTag));
                    htmlBuilder.Append(img.ToString(TagRenderMode.Normal));
                htmlBuilder.Append(divimg.ToString(TagRenderMode.EndTag));
                htmlBuilder.Append(divtitle.ToString(TagRenderMode.Normal));
                htmlBuilder.Append(a.InnerHtml = "<span class='blog-time'>&nbsp&nbsp&nbsp<i class='fa fa-clock-o' aria-hidden='true'></i>&nbsp"+date+"</span>");
            htmlBuilder.Append(a.ToString(TagRenderMode.EndTag));

            return MvcHtmlString.Create(htmlBuilder.ToString());
        }
        public static IHtmlString Video1 (this HtmlHelper helper, string title, string href, string src, int view, string date, string tag, string taglink, string videotime)
        {
            var a = new TagBuilder("a");
            a.MergeAttribute("href", href);

            var img = new TagBuilder("img");
            img.MergeAttribute("src", src);
            img.MergeAttribute("alt", title);
            img.MergeAttribute("style", "width: 100%");

            var topimg = new TagBuilder("div");
            topimg.MergeAttribute("class", "col-sm-12 col-12 no-padding");

            var foot = new TagBuilder("div");
            foot.MergeAttribute("class", "col-sm-12 col-12 no-padding");
            foot.InnerHtml = "<p class='view-time'><span>" + view + "&nbsp;lượt&nbsp;xem</span> | <span>" + date 
                                + "</span></p><h2>" + title + "</h2>";

            var divtag = new TagBuilder("div");
            divtag.MergeAttribute("class", "col-sm-12 col-12 no-padding");
            divtag.InnerHtml = "<a class='pasgo-tag' href='" + taglink + " target='_blank'>" + tag + "</a>";

            StringBuilder htmlBuilder = new StringBuilder();
            htmlBuilder.Append(a.ToString(TagRenderMode.StartTag));
                htmlBuilder.Append(topimg.ToString(TagRenderMode.StartTag));
                htmlBuilder.Append(topimg.InnerHtml = "<span class='pasgo-icondesktop-playbutton'></span>");
                htmlBuilder.Append(img.ToString(TagRenderMode.Normal));
                htmlBuilder.Append(topimg.InnerHtml = "<span class='video-time'>"+ videotime +"</span>");
                htmlBuilder.Append(topimg.ToString(TagRenderMode.EndTag));
                htmlBuilder.Append(foot.ToString(TagRenderMode.Normal));
            htmlBuilder.Append(a.ToString(TagRenderMode.EndTag));
            htmlBuilder.Append(divtag.ToString(TagRenderMode.Normal));

            return MvcHtmlString.Create(htmlBuilder.ToString());
        }
        public static IHtmlString Video2(this HtmlHelper helper, string title, string href, string src, int view, string date, string tag, string taglink, string videotime, int i)
        {
            var a = new TagBuilder("a");
            a.MergeAttribute("href", href);

            var img = new TagBuilder("img");
            img.MergeAttribute("src", src);
            img.MergeAttribute("alt", title);
            img.MergeAttribute("style", "width: 100%");

            var topimg = new TagBuilder("div");
            if (i == 1)
                topimg.MergeAttribute("class", "col-lg-12 col-md-12 col-sm-4 col-4 no-padding sons");
            else
                topimg.MergeAttribute("class", "col-lg-4 col-md-6 col-sm-4 col-4 no-padding sons");

            var foot = new TagBuilder("div");
            if (i == 1)
                foot.MergeAttribute("class", "col-lg-12 col-md-12 col-sm-8 col-8 no-padding daughter");
            else
                foot.MergeAttribute("class", "col-lg-8 col-md-6 col-sm-8 col-8 no-padding daughter");
            foot.InnerHtml = "<p class='view-time'><span>" + view + "&nbsp;lượt&nbsp;xem</span> | <span>" + date
                                + "</span></p><h2>" + title + "</h2>";

            var divtag = new TagBuilder("div");
            divtag.MergeAttribute("class", "col-sm-12 col-12 no-padding");
            if(i!=1)
                divtag.MergeAttribute("style", "display: none;");
            divtag.InnerHtml = "<a class='pasgo-tag' href='" + taglink + " target='_blank'>" + tag + "</a>";

            StringBuilder htmlBuilder = new StringBuilder();
            htmlBuilder.Append(a.ToString(TagRenderMode.StartTag));
            htmlBuilder.Append(topimg.ToString(TagRenderMode.StartTag));
            htmlBuilder.Append(topimg.InnerHtml = "<span class='pasgo-icondesktop-playbutton'></span>");
            htmlBuilder.Append(img.ToString(TagRenderMode.Normal));
            htmlBuilder.Append(topimg.InnerHtml = "<span class='video-time'>" + videotime + "</span>");
            htmlBuilder.Append(topimg.ToString(TagRenderMode.EndTag));
            htmlBuilder.Append(foot.ToString(TagRenderMode.Normal));
            htmlBuilder.Append(a.ToString(TagRenderMode.EndTag));
            htmlBuilder.Append(divtag.ToString(TagRenderMode.Normal));

            return MvcHtmlString.Create(htmlBuilder.ToString());
        }

        public static IHtmlString KhachHang(this HtmlHelper helper, string name, string src, string job, string quote)
        {
            var divimg = new TagBuilder("div");
            divimg.MergeAttribute("class", "col-sm-12 col-3"); 
            divimg.InnerHtml = "<img src='" + src + "' alt='" + name + "'>";

            var divnj = new TagBuilder("div");
            divnj.MergeAttribute("class", "col-sm-12 col-9");
            divnj.InnerHtml = "<h1>" + name + "</h1><p>" + job + "</p>";

            var divquote = new TagBuilder("div");
            divquote.MergeAttribute("class", "col-12 quotes");
            divquote.InnerHtml = "<p>" + quote + "</p>";

            StringBuilder htmlBuilder = new StringBuilder();
            htmlBuilder.Append(divimg.ToString(TagRenderMode.Normal));
            htmlBuilder.Append(divnj.ToString(TagRenderMode.Normal));
            htmlBuilder.Append(divquote.ToString(TagRenderMode.Normal));

            return MvcHtmlString.Create(htmlBuilder.ToString());
        }

        public static IHtmlString DoiTac(this HtmlHelper helper, string href, string src, string name, string address)
        {
            var a = new TagBuilder("a");
            a.MergeAttribute("href", href);
            a.InnerHtml = "<img src="+ src +" class='img-responsive'>";

            var na = new TagBuilder("span");
            na.MergeAttribute("class", "name-address");
            na.InnerHtml = "<h1>"+ name + "</h1><br><p>"+ address +"</p>";

            StringBuilder htmlBuilder = new StringBuilder();
            htmlBuilder.Append(a.ToString(TagRenderMode.Normal));
            htmlBuilder.Append(na.ToString(TagRenderMode.Normal));
            return MvcHtmlString.Create(htmlBuilder.ToString());
        }

        public static IHtmlString AdminPicCarousel (this HtmlHelper helper, string src, string alt, long no, int type)
        {
            var div = new TagBuilder("div");
            if (no == 1)
                div.MergeAttribute("class", "item active");
            else
                div.MergeAttribute("class", "item");

            var divno = new TagBuilder("div");
            divno.MergeAttribute("class", "no");
            divno.InnerHtml = no.ToString();

            var divtype = new TagBuilder("div");
            divtype.MergeAttribute("class", "imgtype");
            if (type == 1)
                divtype.SetInnerText("Slide");
            if (type == 2)
                divtype.SetInnerText("Menu");
            if (type == 99)
                divtype.SetInnerText("Homepage Slide");

            var img = new TagBuilder("img");
            img.MergeAttribute("src", src);
            img.MergeAttribute("alt", alt);
            img.MergeAttribute("style", "width: 100%; margin-left: auto; margin-right: auto;");

            var divtitle = new TagBuilder("div");
            divtitle.MergeAttribute("class", "title");
            divtitle.InnerHtml = alt;

            StringBuilder htmlBuilder = new StringBuilder();
            htmlBuilder.Append(divno.ToString(TagRenderMode.Normal));
            htmlBuilder.Append(divtype.ToString(TagRenderMode.Normal));
            htmlBuilder.Append(img.ToString(TagRenderMode.Normal));
            htmlBuilder.Append(divtitle.ToString(TagRenderMode.Normal));
            div.InnerHtml = htmlBuilder.ToString();

            return MvcHtmlString.Create(div.ToString(TagRenderMode.Normal));
        }

        public static IHtmlString AdminPicIndicators (this HtmlHelper helper, string src, string alt, long no)
        {
            var img = new TagBuilder("img");
            if (no == 1)
                img.MergeAttribute("class", "img-indicators active");
            else
                img.MergeAttribute("class", "img-indicators");
            img.MergeAttribute("alt", alt);
            img.MergeAttribute("src", src);
            img.MergeAttribute("data-target", "#picCarousel");
            img.MergeAttribute("data-slide-to", (no - 1).ToString());

            return MvcHtmlString.Create(img.ToString(TagRenderMode.Normal));
        }

        public static IHtmlString AdminPicUpdateCarousel(this HtmlHelper helper, string src, string alt, long no, int? type, int id)
        {
            var divimg = new TagBuilder("div");
            divimg.MergeAttribute("class", "div-img");

            var img = new TagBuilder("img");
            if (no == 1)
                img.MergeAttribute("class", "img-indicators active");
            else
                img.MergeAttribute("class", "img-indicators");
            img.MergeAttribute("alt", alt);
            img.MergeAttribute("src", src);
            img.MergeAttribute("typename", type.ToString());
            img.MergeAttribute("name", "imgslide");
            img.MergeAttribute("data-target", "#picupdate2");
            img.MergeAttribute("data-slide-to", (no).ToString());
            img.MergeAttribute("idimg", id.ToString());
            img.MergeAttribute("onclick", "PicInfo()");

            StringBuilder htmlBuilder = new StringBuilder();
            htmlBuilder.Append(img.ToString(TagRenderMode.Normal));
            divimg.InnerHtml = htmlBuilder.ToString();

            return MvcHtmlString.Create(divimg.ToString(TagRenderMode.Normal));
        }

        public static IHtmlString AdminPicUpdateCarousel2(this HtmlHelper helper, string alt, string src, long no)
        {
            var img = new TagBuilder("img");
            img.MergeAttribute("class", "img-indicators2");
            img.MergeAttribute("alt", alt);
            img.MergeAttribute("src", src);

            return MvcHtmlString.Create(img.ToString(TagRenderMode.Normal));
        }

        public static IHtmlString CheckBoxTT (this HtmlHelper helper, string thuoctinh, Boolean val)
        {
            var checkbox = new TagBuilder("input");
            checkbox.MergeAttribute("type", "checkbox");
            checkbox.MergeAttribute("name", "thongtin");
            checkbox.MergeAttribute("value", val.ToString());
            if (val == true)
                checkbox.MergeAttribute("checked", "checked");

            var label = new TagBuilder("label");
            label.SetInnerText(thuoctinh);

            StringBuilder htmlBuilder = new StringBuilder();
            htmlBuilder.Append(label.ToString(TagRenderMode.Normal));
            htmlBuilder.Append(checkbox.ToString(TagRenderMode.Normal));

            return MvcHtmlString.Create(htmlBuilder.ToString());
        }

    }
}