<%@ Page Title="Загрузить файлы" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="DownloadFiles.aspx.cs" Inherits="Account.Contact" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: Title %></h2>
    <h3></h3>
    <div>
        <asp:FileUpload ID="FileUpload1" runat="server" />
        <p>
            <br>
            <asp:Button ID="Button1" runat="server" OnClick="Upload" Text="Загрузить" class="btn btn-default" />
            <asp:Label ID="UploadStatusLabel" runat="server"></asp:Label>
        </p>
    </div>
    <h4>Введите название нового документа:</h4>
    <asp:TextBox ID="fileName" runat="server" placeholder="Название" class="form-control"></asp:TextBox>
    <br>
    <div>
       
        <br>
        <asp:Button ID="Button2" runat="server" OnClick="Compile" Text="Скомпилировать" data-loading-text="«Загрузка…»" class="btn btn-default" />
        &nbsp;
        <asp:Label ID="InProgramUploadStatusLabel" runat="server"></asp:Label>
        <br />
        <asp:Label ID="Label1" runat="server"></asp:Label>

    </div>
</asp:Content>
