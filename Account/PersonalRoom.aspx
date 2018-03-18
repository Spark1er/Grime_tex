<%@ Page Title="Личный кабинет" Language="C#" MasterPageFile="~/Site.master" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: Title %></h2>
    <div class="container">
        <div class="thumbnail">
            <div class="row">
                <div class="col-sm-6 col-md-4">
                    <div class="thumbnail">
                        <div class=" caption" style="height: 130px;">
                            <h3>Сменить пароль</h3>
                            <p>
                                <a class="btn btn-default" href="ChangePassword.aspx">Сменить пароль</a>
                            </p>
                        </div>
                    </div>
                </div>

                <div class="col-sm-6 col-md-4">
                    <div class="thumbnail" >
                        <div class=" caption" style="height: 130px;">
                            <h3>Загрузить файлы для компиляции</h3>
                            <p>
                                <a class="btn btn-default" href="DownloadFiles.aspx">Загрузить файлы</a>
                            </p>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
