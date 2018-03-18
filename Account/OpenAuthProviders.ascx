<%@ Control Language="C#" AutoEventWireup="true" CodeFile="OpenAuthProviders.ascx.cs" Inherits="OpenAuthProviders" %>

<div id="socialLoginList">
    <hr />
    <asp:ListView runat="server" ID="providerDetails" ItemType="System.String"
        SelectMethod="GetProviderNames" ViewStateMode="Disabled">
        <ItemTemplate>
            <p>
                
                    <%#: Item %>
            </p>
        </ItemTemplate>
        <EmptyDataTemplate>
            <div>
            </div>
        </EmptyDataTemplate>
    </asp:ListView>
</div>