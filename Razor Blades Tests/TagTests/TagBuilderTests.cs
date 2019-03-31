﻿using System.Collections.Generic;
using Connect.Razor.Blade;
using Connect.Razor.Internals;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Razor_Blades_Tests.TagTests
{
    [TestClass]
    public class TagBuilderTests
    {
        [TestMethod]
        public void CloseSimpleTags()
        {
            Assert.AreEqual("</p>", TagBuilder.Close("p"));
            Assert.AreEqual("</em>", TagBuilder.Close("em"));
            Assert.AreEqual("</EM>", TagBuilder.Close("EM"));
            Assert.AreEqual("</ng-template>", TagBuilder.Close("ng-template"));
        }

        [TestMethod]
        public void OpenSimpleTags()
        {
            Assert.AreEqual("<p>", TagBuilder.Open("p"));
            Assert.AreEqual("<em>", TagBuilder.Open("em"));
            Assert.AreEqual("<EM>", TagBuilder.Open("EM"));
            Assert.AreEqual("<ng-template>", TagBuilder.Open("ng-template"));
        }

        [TestMethod]
        public void OpenCloseEmptyTags()
        {
            Assert.AreEqual("", TagBuilder.Close(""));
            Assert.AreEqual("", TagBuilder.Close(" "));
            Assert.AreEqual("", TagBuilder.Close(null));
            Assert.AreEqual("", TagBuilder.Open(""));
            Assert.AreEqual("", TagBuilder.Open(" "));
            Assert.AreEqual("", TagBuilder.Open(null));
        }


        [TestMethod]
        public void OpenTagsWithId() 
            => Assert.AreEqual("<p id='myId'>",
                TagBuilder.Open("p", id:"myId"));

        [TestMethod]
        public void OpenTagsWithClass() 
            => Assert.AreEqual("<p class='my-class'>", 
                TagBuilder.Open("p", classes:"my-class"));

        [TestMethod]
        public void OpenTagsWithClasses() 
            => Assert.AreEqual("<p class='my-class float-right'>", 
                TagBuilder.Open("p", classes:"my-class float-right"));

        [TestMethod]
        public void OpenTagsWithIdAndClasses() 
            => Assert.AreEqual("<p id='myId' class='my-class float-right'>", 
                TagBuilder.Open("p", id: "myId", classes:"my-class float-right"));

        [TestMethod]
        public void OpenTagsWithAttributeString() 
            => Assert.AreEqual("<p data='xyz'>", 
                TagBuilder.Open("p", attributes: "data='xyz'"));

        [TestMethod]
        public void OpenTagsWithAttributeStringAndSpace() 
            => Assert.AreEqual("<p data='xyz'>", 
                TagBuilder.Open("p", attributes: " data='xyz'"));

        [TestMethod]
        public void OpenTagsWithAttributeList()
            => Assert.AreEqual("<p data='xyz' kitchen='black'>",
                TagBuilder.Open("p", attributes: new Dictionary<string, string>
                {
                    {"data", "xyz"},
                    {"kitchen", "black"}
                }));

        [TestMethod]
        public void OpenTagsWithClassIdAndAttributeString() 
            => Assert.AreEqual("<p id='myId' class='my-class float-right' data='xyz'>", 
                TagBuilder.Open("p", attributes: "data='xyz'", id: "myId", classes:"my-class float-right"));

        [TestMethod]
        public void OpenTagsWithClassIdAndAttributeList() 
            => Assert.AreEqual("<p id='myId' class='my-class float-right' data='xyz' kitchen='black'>", 
                TagBuilder.Open("p", attributes: new Dictionary<string, string>
                {
                    {"data", "xyz"},
                    {"kitchen", "black"}
                }, id: "myId", classes:"my-class float-right"));

        [TestMethod]
        public void OpenTagWithSelfClose()
            => Assert.AreEqual("<p/>", 
                TagBuilder.Open("p", options: new TagOptions {SelfClose = true}));

        [TestMethod]
        public void OpenTagsWithIdAndClassesSelfClose()
            => Assert.AreEqual("<p id='myId' class='my-class float-right'/>",
                TagBuilder.Open("p", id: "myId", 
                    classes: "my-class float-right",
                    options: new TagOptions { SelfClose = true }));


        [TestMethod]
        public void OpenTagsWithClassIdAndAttributeListOptionsQuote() 
            => Assert.AreEqual("<p id=\"myId\" class=\"my-class float-right\" data=\"xyz\" kitchen=\"black\">", 
                TagBuilder.Open("p", attributes: new Dictionary<string, string>
                {
                    {"data", "xyz"},
                    {"kitchen", "black"}
                }, id: "myId", classes:"my-class float-right", 
                    options: new TagOptions(new AttributeOptions {Quote = "\""}))
                );

    }
}
