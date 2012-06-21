<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>
<% Response.Write("("+Json.Encode(Model)+")"); %>