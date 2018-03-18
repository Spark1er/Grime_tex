<%@ Page Title="Наши шаблоны" Language="C#" MasterPageFile="~/Site.master" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
        <h4>Перед использование советуем прочитать инструкцию по использованию шаблонов</h4>
        <a href="inst.rar">Инструкция </a>
        <h4>А так же инструкцию по созданию PDF документа из уже заполненного шаблона</h4>
        <a href="Сборка документа.rar">Инструкция </a>
    </div>
    <div class="container">
        <div class="thumbnail">
            <div class="row">
                <div class="col-sm-6 col-md-4">
                    <div class="thumbnail">
                        <div class="caption">
                            <h3>Реферат</h3>
                            <p>Данный шаблон предназначен для создания реферата по всем ГОСТ стандартам</p>
                            <p><a href="referat.rar" class="btn btn-default" role="button">Скачать</a> <a href="LaTex_FullWork.pdf" class="btn btn-default" role="button">Пример</a></p>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
